using AngularCore.Microservices.Gateways.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularCore.Microservices.Gateways.Api.Services
{
    public interface IClientSearchApiService
    {
        Task<IEnumerable<User>> SearchUsers(string phrase);
    }
}
