using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reply : Message
    {
        //foreign keys
        public int? SourceId { get; set; }
        public int PostId { get; set; }

        //navigation properties
        public Reply Source { get; set; }
        public Post Post { get; set; }
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}
