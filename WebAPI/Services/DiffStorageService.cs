namespace WebAPI.Services
{
    public class DiffStorageItem
    {
        public int UserId { get; set; }
        internal byte[] blobLeft  { get; set; }
        internal byte[] blobRight { get; set; }
    }
    public class DiffStorageService
    {
        private List<DiffStorageItem> diffStorageItems { get; init; }
        public DiffStorageService()
        {
            diffStorageItems = new List<DiffStorageItem>(); 
        }

        public void Add(int userId, byte[] blob, bool isRight)
        {
            //It checks if there is data for a certain user and replce or add blob
            var v = diffStorageItems.FirstOrDefault(p => p.UserId == userId);
            if (v == null)
            {
                v = new DiffStorageItem() { UserId = userId };
                diffStorageItems.Add(v);
            }
            if (isRight) v.blobRight = blob; else v.blobLeft = blob;
        }

        public byte[] Find(int userId, bool isRight)
        {
            byte[] result = null;
            var v = diffStorageItems.FirstOrDefault(p => p.UserId == userId);
            if (v == null) return result;
            else
            {
                result = isRight ? v.blobRight : v.blobLeft;
            }
            return result;
        }
    }
}
