using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    internal class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public List<Message>? messages { get; set; }

    }
}
