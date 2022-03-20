using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Rating
    {
        public int Rate { get; set; }

        //composite primary key
        //=> foreign keys
        public int UserId { get; set; }
        public int MessageId { get; set; }

        //=> navigation properties
        public User User { get; set; }
        public Message Message { get; set; }
    }
}
