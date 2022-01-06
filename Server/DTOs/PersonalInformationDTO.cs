using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class PersonalInformationDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string IdNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }


        //foreign key
        public int AppUserId { get; set; }

        public AppUserDTO AppUser { get; set; }

    }
}