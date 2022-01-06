using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Shared;

namespace XebecPortal.Server.DTOs.ViewModels
{
    public class UserViewModel
    {
        public string Role { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
