#nullable disable

namespace Domain.Entities
{
    public class File : BaseModel
    {
        public int ID { get; set; }
        public int fk_EntityID { get; set; }
        public int RecordID { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; } 

        public virtual Entity Entity { get; set; } 
    }
}