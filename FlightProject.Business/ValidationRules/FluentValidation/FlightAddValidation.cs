using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Entities.Dtos;
using FluentValidation;

namespace FlightProject.Business.ValidationRules.FluentValidation
{
    public class FlightAddValidation : AbstractValidator<FlightAddDto>
    {
        public FlightAddValidation()
        {
            RuleFor(x => x.DepartureDateTime).NotNull().WithMessage("Kalkış Zamanı Alanı Seçiniz!");
            RuleFor(x => x.ArrivalDateTime).NotNull().WithMessage("Varış Zamanı Alanı Seçiniz!");
            RuleFor(x => x.FlightNumber).MinimumLength(3).WithMessage("Uçuş Numarası Zorunlu Alan!");
            RuleFor(x => x.Price).NotNull().GreaterThan(0).WithMessage("Fiyat Alanı Zorunlu ve Sıfırdan Büyük Olmalı!");
            RuleFor(x => x.PassengerInformation.Name).MinimumLength(3).WithMessage("Ad Alanı Minimum 3 Karakter Olmalı.");
            RuleFor(x => x.PassengerInformation.Surname).MinimumLength(3).WithMessage("Soyad Alanı Minimum 3 Karakter Olmalı.");
            RuleFor(x => x.PassengerInformation.Email).EmailAddress().WithMessage("Lütfen Geçerli Bir Email Adresi Giriniz!");
            RuleFor(x => x.PassengerInformation.Address).MinimumLength(3).WithMessage("Adres Alanı Minimum 3 Karakter Olmalı.");
            RuleFor(x => x.PassengerInformation.PhoneNumber).NotNull().WithMessage("Telefon Alanı Minimum 3 Karakter Olmalı.");
        }
    }
}
