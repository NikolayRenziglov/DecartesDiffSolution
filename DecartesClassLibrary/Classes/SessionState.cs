using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecartesClassLibrary.Classes
{
    public class SessionState
    {
        public int? UserId { get; set; }
        public string BinaryString1 { get; set; }
        public string BinaryString2 { get; set; }

        public SessionState()
        {
            UserId = 1;
        }
    }
}
