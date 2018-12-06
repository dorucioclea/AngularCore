namespace AngularCore.Data.ViewModels
{
    public class LoginResponse
    {
        public LoggedUser User { get; set; }
        public string JwtToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}