using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Post : Message
    {
        public string Title { get; set; }

        //foreign key
        public int TopicId { get; set; }

        //navigation properties
        public Topic Topic { get; set; }
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}
