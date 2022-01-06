using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class AppUserDTO
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string PasswordHash { get; set; }

        public AppUserDTO(int id, string email, string role)
        {
            this.Id = id;
            this.Email = email;
            this.Role = role;
        }

        public AppUserDTO(string email, string role, string passwordHash)
        {
            //this.Id = id;
            this.Email = email;
            this.Role = role;
            this.PasswordHash = passwordHash;
        }

        public AppUserDTO()
        {
            
        }


    }
}
