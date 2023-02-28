using FluentValidation;
using HotelAPI.Entities;
using HotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Validators
{
    public class CreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
    {
        public CreateHotelDtoValidator(HotelDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(x => x.Description)
                .MaximumLength(25);

            RuleFor(x => x.Category)
                .MaximumLength(25);

            RuleFor(x => x.ContactEmail)
                .EmailAddress();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(25);
        }
    }
}
