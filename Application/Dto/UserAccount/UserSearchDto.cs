using Application.CustomValidators;
using FluentValidation;
using System.Collections.Generic;

namespace Application.Dto
{
    public class UserSearchDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public bool? Active { get; set; }
    }

    public class UserSearchDtoValidator : AbstractValidator<UserSearchDto>
    {
        public UserSearchDtoValidator()
        {
            List<string> sortedColumns = new List<string>
           {
               {"Email"},
               {"CreatedDate"}
           };
            RuleFor(x => x.PageIndex)
                .NotEmpty().WithMessage("PageIndex should be greater than 0");

            RuleFor(x => x.PageSize)
                .NotEmpty().WithMessage("PageSize should be greater than 0");

            RuleFor(x => x.SortDirection)
                .IsValidSortDirection();

            RuleFor(x => x.Email)
               .MaximumLength(150);

            RuleFor(x => x.SortColumn)
                .IsValidListMember(sortedColumns);
        }
    }
}