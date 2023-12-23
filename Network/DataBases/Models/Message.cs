using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBases.Models;

public class Message
{
    public int MessageId { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public bool IsSent { get; set; }
    public virtual User? SenderId { get; set; }
    public virtual User? RecieverId { get; set; }

    public int? UserToId { get; set; }
    public int? UserFromId { get; set; }

}
