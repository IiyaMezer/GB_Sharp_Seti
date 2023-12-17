using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    internal class Message
    {
        public int MessageID { get; set; }
        public string Text {  get; set; }
        public DateTime Time { get; set; }
        public bool IsSent {  get; set; }
        public  User Sender { get; set; }
        public User Reciever { get; set; }


    }
}
