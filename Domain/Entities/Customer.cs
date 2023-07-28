namespace Domain.Entities
{
    public class Customer : BaseModel
    {
        public virtual int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}