using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int RateSum { get; set; }

        //foreign key
        public int AuthorId { get; set; }

        //navigation properties
        public User Author { get; set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
