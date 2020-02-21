using System.Collections.Generic;

namespace PortalExample.API.Dtos
{
    public class UserForUpdateDto
    {
  
        public string City { get; set; }
        public string Country { get; set; }

        //Zakładka info

        public string Growth { get; set; }

        public string EyeColor { get; set; }

        public string HairColor { get; set; }
        public string MartialStatus { get; set; }

        public string Education { get; set; }
        public string Profestion { get; set; }

        public string Children { get; set; }
        public string Languages { get; set; }

        // zakład  o mnie
        public string Motto { get; set; }
        public string Description { get; set; }
        public string Personality { get; set; }
        public string LookingFor { get; set; }

        public string Inerests { get; set; }
        public string FreeTime { get; set; }
        public string Sport { get; set; }
        public string Movies { get; set; }

        public string Music { get; set; }

        //zakładka preferencje

        public string ILike { get; set; }
        public string IdoNotLike { get; set; }
        public string MakesMeLauggh { get; set; }

        public string ItFeelBestIn { get; set; }

        public string FriendeWouldDescribeMe { get; set; }

        /// zakładka zdjęcia
        // public ICollection<PhotoForDetailedDto> Photo { get; set; }
        // public string PhotoUrl { get; set; }
    }
}