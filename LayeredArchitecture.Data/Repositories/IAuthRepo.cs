using LayeredArchitecture.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Data.Repositories
{
    public interface IAuthRepo
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(ApplicationUser user,string password);
        Task<IdentityResult> AuthenticateUser(string email, string password);
        Task<bool> CheckUserPassword(ApplicationUser user, string password);
        Task<IList<string>> GetUserRole(ApplicationUser user);


    }
}
