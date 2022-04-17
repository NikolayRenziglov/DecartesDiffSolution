using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecartesClassLibrary.Classes
{
    public class CsvValidator
    {
        public byte[] CsvToBlob(string s, out bool isNotValid)
        {
            isNotValid = false;
            byte[] result = null;
            try
            {
                string[] ss = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
                result = new byte[ss.Length];
                for (int i = 0; i < ss.Length; i++)
                {
                    checked
                    {
                        result[i] = Convert.ToByte(ss[i].Trim());
                    }

                }
            }
            catch
            {
                isNotValid = true;
            }
            return result;
        }
    }
}
