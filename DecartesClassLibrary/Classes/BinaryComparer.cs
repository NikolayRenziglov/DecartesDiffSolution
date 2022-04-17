using DecartesClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DecartesClassLibrary.Classes
{
    public class BinaryComparer : IBinaryComparer
    {
        public string ErrorString { get; set; } = String.Empty;
        public byte[] Data { get; set; }

        public string CompareToJson(IBinaryComparer? otherBinary)
        {
            ErrorString = (otherBinary==null) ? "Not Found" : String.Empty;
            string result = String.Empty;
            if (!String.IsNullOrEmpty(ErrorString)) return result;

            DiffResult diffResult = new DiffResult();
            if (Data.Length != otherBinary!.Data.Length) diffResult.Result = DiffResultTypes.SizeDoNotMatch;    
            else if (Data.SequenceEqual(otherBinary!.Data)) diffResult.Result = DiffResultTypes.Equals;
            else
            {
                bool inCount = false;
                for (int i=0; i < Data.Length; i++)
                {
                    if (Data[i] != otherBinary!.Data[i])
                    {
                        if (!inCount)
                        {
                            if (diffResult.Difference == null) diffResult.Difference = new List<DiffResultItem>();
                            diffResult.Difference.Add(new DiffResultItem(_offset: i, _count: 1));
                            inCount = true;
                        }
                        else diffResult.LastDifference!.Count++;
                    }
                    else inCount = false;
                }
                if (diffResult.Difference.Count > 0) diffResult.Result = DiffResultTypes.ContentDoNotMatch;
            }

            //convert to Json string
            result = Newtonsoft.Json.JsonConvert.SerializeObject(diffResult);
            return result;
        }
        
        public void FromCSV(string csv)
        {
            ErrorString = String.Empty;
            csv = csv.Trim();
            if (string.IsNullOrEmpty(csv)) ErrorString = "Csv is empty";
            else
            {
                try
                {
                    string[] ss = csv.Split('\u002C', StringSplitOptions.RemoveEmptyEntries);
                    Data = new byte[ss.Length];
                    for (int i = 0; i < ss.Length; i++)
                    {
                        checked
                        {
                            Data[i] = Convert.ToByte(ss[i].Trim());
                        }
                        
                    }
                }
                catch 
                {
                    ErrorString = "Incorrect data";
                }
                
            }
        }

    }
}
