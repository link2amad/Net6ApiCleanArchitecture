namespace Application.Dto
{
    public class EmailTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Active { get; set; }
    }
}