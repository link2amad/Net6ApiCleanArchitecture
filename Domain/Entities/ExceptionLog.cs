namespace Domain.Entities
{
    public class ExceptionLog : BaseModel
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public int EntityID { get; set; }
        public string Method { get; set; }
        public string JSON { get; set; }
        public string RequestUrl { get; set; }
        public string RequestJSON { get; set; }
        public string Exception { get; set; }
    }
}