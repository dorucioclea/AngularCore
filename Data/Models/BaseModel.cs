using System;

namespace AngularCore.Data.Models
{
    public class BaseModel
    {
        public string Id { get; private set; }
        public string CreatedAt { get; private set; }
        public string ModifiedAt { get; private set; }

        public BaseModel(){
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now.ToString();
        }

        protected void Modified()
        {
            this.ModifiedAt = DateTime.Now.ToString();
        }
    }
}