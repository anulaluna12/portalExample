using System.ComponentModel.DataAnnotations;

namespace PortalExample.API.Dtos
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagana")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Hasło ,usi skłafdać się z 4 do 8 znaków")]
        public string Password { get; set; }
    }
}