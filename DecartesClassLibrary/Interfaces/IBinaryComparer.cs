using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecartesClassLibrary.Interfaces
{
    public interface IBinaryComparer
    {
        string ErrorString { get; set; }
        byte[] Data { get; set; }
        void FromCSV(string csv);
        string CompareToJson(IBinaryComparer? otherBinary);
    }
}
