using System;
using AngularCore.Data.Models;

namespace AngularCore.Services
{
    public interface IAuthService
    {
        string GenerateJWTToken(User user);
        int TokenValidityPeriod { get; }
    }
}