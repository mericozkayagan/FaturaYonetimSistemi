using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validations
{
    public class HouseValidator : AbstractValidator<House>
    {
        public HouseValidator()
        {            
            RuleFor(x => x.HouseNumber).GreaterThan(0).WithMessage("Ev numarası 0'dan büyük olmalı");
            RuleFor(x => x.Block).GreaterThan(0).WithMessage("Blok numarası 0'dan büyük olmalı");            
        }
    }
}
