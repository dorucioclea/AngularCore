using System.Collections.Generic;

namespace AngularCore.Data.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Friends { get; set; }
    }
}