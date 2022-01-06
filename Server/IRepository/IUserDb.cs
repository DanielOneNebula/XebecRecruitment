using XebecPortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Shared.Security;

namespace Server.IRepository
{
    public interface IUserDb
    {
        Task<AppUser> AuthenticateUser(string email, string password);

        Task<AppUser> AddUser(string email, string password, string role);
    }
}
