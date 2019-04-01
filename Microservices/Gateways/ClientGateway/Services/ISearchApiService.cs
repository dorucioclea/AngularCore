using ClientGateway.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientGateway.Services
{
    public interface ISearchApiService
    {
        Task<IEnumerable<User>> SearchUsers(string phrase);
    }
}
