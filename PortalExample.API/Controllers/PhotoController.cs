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
    [Route("api/users/{userId}/photos")]
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
        public async Task<IActionResult> AddPhotoForUser(int userId, PhotoForCreationDto PhotoForCreationDto)
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
                return Ok();

            return BadRequest("Nie można dodac zdjęcia");

        }
    }
}