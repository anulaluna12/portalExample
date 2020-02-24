using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortalExample.API.Data;
using PortalExample.API.Dtos;
using PortalExample.API.Helpers;
using PortalExample.API.Models;

namespace PortalExample.API.Controllers
{
    [Authorize]
    [Route("api/user/{userId}/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<ClaudinarySettings> _claudinaryConfig;
        private Cloudinary _claudinary;

        public PhotoController(IUserRepository userRepository,
        IMapper mapper,
        IOptions<ClaudinarySettings> claudinaryConfig)
        {
            _claudinaryConfig = claudinaryConfig;
            _mapper = mapper;
            _userRepository = userRepository;
            Account account = new Account(
                _claudinaryConfig.Value.CloudName,
            _claudinaryConfig.Value.ApiKey,
             _claudinaryConfig.Value.ApiSecret);
            _claudinary = new Cloudinary(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto PhotoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _userRepository.GetUser(userId);
            var file = PhotoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("Fill").Gravity("face")
                    };
                    uploadResult = _claudinary.Upload(uploadParams);
                }
            }
            PhotoForCreationDto.Url = uploadResult.Uri.ToString();
            PhotoForCreationDto.PublicId = uploadResult.PublicId;
            var photo = _mapper.Map<Photo>(PhotoForCreationDto);
            if (!userFromRepo.Photo.Any(x => x.IsMain))
                photo.IsMain = true;

            userFromRepo.Photo.Add(photo);

            if (await _userRepository.SaveAll())
            {

                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);

            }

            return BadRequest("Nie można dodac zdjęcia");

        }
        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _userRepository.GetPhoto(id);
            var photoFromReturn = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photoFromReturn);

        }
        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            var userFromRepo = await _userRepository.GetUser(userId);
            if (!userFromRepo.Photo.Any(x => x.Id == id))
                return Unauthorized();

            var photoFromRepo = await _userRepository.GetPhoto(id);
            if (photoFromRepo.IsMain)
                return BadRequest("To już jest głowne zdjęcie");

            var currentMainPhoto = await _userRepository.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;

            photoFromRepo.IsMain = true;
            if (await _userRepository.SaveAll())
            {
                return NoContent();//status 204
            };

            return BadRequest("Nie można ustawić zdjęcia jako głownego");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            var userFromRepo = await _userRepository.GetUser(userId);
            if (!userFromRepo.Photo.Any(x => x.Id == id))
                return Unauthorized();

            var photoFromRepo = await _userRepository.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("Nie można usunąć zdjęcia głównego");

            if (photoFromRepo.public_id != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.public_id);
                var result = _claudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _userRepository.Delete(photoFromRepo);
                }
            }
            if (photoFromRepo.public_id == null)
            {
                _userRepository.Delete(photoFromRepo);
            }

            if (await _userRepository.SaveAll())
            {
                return Ok();
            }


            return BadRequest("Nie udało się usunąc zdjęcia");

        }
    }
}