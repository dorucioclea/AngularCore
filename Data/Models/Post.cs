namespace AngularCore.Data.Models
{
    public class Post : BaseModel
    {
        private User _owner;
        public User Owner
        {
            get => _owner;
            set {
                _owner = value;
                Modified();
            }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set {
                _content = value;
                Modified();
            }
        }

        public Post(User owner, string content) : base()
        {
            Owner = owner;
            Content = content;
        }
    }
}