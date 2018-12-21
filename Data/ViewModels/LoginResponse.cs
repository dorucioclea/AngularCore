namespace AngularCore.Data.ViewModels
{
    public class LoginResponse
    {
        public DetailedUserVM User { get; set; }
        public string JwtToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}