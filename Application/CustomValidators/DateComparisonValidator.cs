using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.CustomValidators
{
    public class DateComparisonValidator : ValidationAttribute
    {
        private string _errorMessage { get; set; }
        private DateTime _from;
        private DateTime _to;
        private DateTimeCompareOperator _comparisonOperator;

        public DateComparisonValidator(DateTime from, DateTime to, DateTimeCompareOperator camparisonOperator, string errorMessage)
        {
            _errorMessage = errorMessage;
            _to = to;
            _from = from;
            _comparisonOperator = camparisonOperator;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                if (DateTime.Compare(_from, _to) != _comparisonOperator.GetHashCode())
                {
                    return new ValidationResult(_errorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}