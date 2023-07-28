namespace Domain.Entities
{
    public class ApiLog
    {
        public int ID { get; set; }
        public string RequestURL { get; set; }
        public string IPAddress { get; set; }
        public string RequestByURL { get; set; }
        public string RequestBody { get; set; }
        public string Response { get; set; }
        public int ResponseStatusCode { get; set; }
        public DateTime RequestAt { get; set; }
        public DateTime? ResponseAt { get; set; }
    }
}