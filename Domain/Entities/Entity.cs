#nullable disable

namespace Domain.Entities
{
    public class Entity
    {
        public Entity()
        {
            Files = new HashSet<File>();
        }

        public int ID { get; set; }
        public string EntityName { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}