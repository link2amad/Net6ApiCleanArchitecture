using System;

namespace Application.Dto
{
    public class StatusDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public dynamic Object { get; set; }
        public Exception ErrorMessage { get; set; }
    }
}