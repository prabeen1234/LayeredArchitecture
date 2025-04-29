using LayeredArchitecture.Models;
using Microsoft.AspNetCore.Identity;

namespace LayeredArchitecture.Data.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        
        private readonly UserManager<ApplicationUser> _manager;
        public AuthRepo(AppDbContext context, UserManager<ApplicationUser> manager)
        {
            
            _manager = manager;
        }

        public Task<IdentityResult> AuthenticateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool>CheckUserPassword(ApplicationUser user, string password)
        {
            return await _manager.CheckPasswordAsync(user, password);        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user,string password)
        {

           return await _manager.CreateAsync(user,password);
           
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _manager.FindByEmailAsync(email);  
        }

    }

    
}
