using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

        //navigation property
        public ICollection<Topic> Subscriptions { get; set; } = new List<Topic>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
