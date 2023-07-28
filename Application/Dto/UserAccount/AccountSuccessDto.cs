using System;

namespace Application.Dto
{
    public class AccountSuccessDto
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public Exception ErrorMessage { get; set; }
    }
}