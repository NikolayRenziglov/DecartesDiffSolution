using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DecartesClassLibrary.Classes
{
    public enum DiffResultTypes
    {
        Undefined=0,
        Equals,
        ContentDoNotMatch,
        SizeDoNotMatch
    }
    public record DiffResultItem
    {
        public int Offset { get;set; }
        public int Count { get;set; }
        public DiffResultItem(int _offset, int _count)
        {
            Offset = _offset;   
            Count = _count;
        }
    }
    public class DiffResult
    {
        [Newtonsoft.Json.JsonIgnore]
        public DiffResultTypes Result { get; set; }
        [Newtonsoft.Json.JsonProperty("diffResultType")]
        public string ResultString { get { return Result.ToString(); } }
        [Newtonsoft.Json.JsonProperty("diffs", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<DiffResultItem> Difference { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public DiffResultItem? LastDifference
        {
            get
            {
                return Difference.LastOrDefault();
            }
        }
        
    }
}
