using Application.Helper;
using FluentValidation;
using System;

namespace Application.CustomValidators
{
    public static class PhoneNumberValidator
    {
        public static IRuleBuilderOptions<T, TElement> IsPhoneNumber<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.Must((TElement value) =>
            {
                return value != null && typeof(TElement) == typeof(string) && !String.IsNullOrWhiteSpace(value.ToString()) ? value.ToString().IsPhoneNumber() : true;
            }).WithMessage("The {PropertyName} field is invalid");
        }
    }
}