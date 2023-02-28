using FluentValidation;
using HotelAPI.Entities;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Validators
{
    public class CreateAdminDtoValidator : AbstractValidator<CreateAdminDto>
    {
        public CreateAdminDtoValidator(HotelDbContext dbContext)
        {
            RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(25);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(25);

            RuleFor(x => x.Nationality)
                .MaximumLength(20);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(30);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    var nameInUse = dbContext.Users.Any(u => u.Name == value);
                    if (nameInUse)
                    {
                        context.AddFailure("Name", "The email is taken");
                    }
                });

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "The email is taken");
                    }
                });
        }
    }
}
