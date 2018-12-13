using System;
using System.Collections.Generic;

namespace AngularCore.Data.Models
{
    public class User : BaseModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set {
                _email = value;
                Modified();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set {
                _password = value;
                Modified();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set {
                _name = value;
                Modified();
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set {
                _surname = value;
                Modified();
            }
        }

        private List<User> _friends;
        public List<User> Friends
        {
            get => _friends;
            set {
                _friends = value;
                Modified();
            }
        }

        public User(string name, string surname, string email, string password) : base()
        {
            _name = name;
            _surname = surname;
            _email = email;
            _password = password;
            _friends = new List<User>();
        }
    }
}