using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //navigation property
        public ICollection<User> Subscribers { get; set; } = new List<User>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
