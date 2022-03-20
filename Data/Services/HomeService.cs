using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class HomeService
    {
        internal Context _context;
        public HomeService(Context context)
        {
            _context = context;
        }
    }
}
