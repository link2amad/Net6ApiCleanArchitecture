using Application.Helper;
using FluentValidation;
using System;

namespace Application.CustomValidators
{
    public static class SSNValidator
    {
        public static IRuleBuilderOptions<T, TElement> IsSSN<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.Must((TElement value) =>
            {
                return value != null && typeof(TElement) == typeof(string) && !String.IsNullOrWhiteSpace(value.ToString()) ? value.ToString().IsSSN() : true;
            }).WithMessage("The {PropertyName} field is invalid");
        }
    }
}