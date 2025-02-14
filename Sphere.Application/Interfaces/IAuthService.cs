using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphere.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string userName);
        string Authenticate(Models.UserModel model);
    }
}
