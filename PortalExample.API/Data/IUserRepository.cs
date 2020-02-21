using System.Collections.Generic;
using System.Threading.Tasks;
using PortalExample.API.Models;

namespace PortalExample.API.Data
{
    public interface IUserRepository :IGenericRepository
    {
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);

    }
}