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
    public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationDtoValidator(HotelDbContext dbContext)
        {
            RuleFor(x => x.StartDate).NotNull();

            RuleFor(x => x.EndDate).NotNull();

            RuleFor(x => x.UserId).NotNull();
        }
    }
}
