using LayeredArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArchitecture.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user); 
    }
}
