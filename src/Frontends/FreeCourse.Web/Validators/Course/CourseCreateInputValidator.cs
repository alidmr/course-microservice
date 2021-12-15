﻿using FluentValidation;
using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Validators.Course
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("isim alanı boş olamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("açıklama alanı boş olamaz");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("süre alanı boş olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("fiyat alanı boş olamaz")
                .ScalePrecision(2, 6)
                .WithMessage("hatalı para formatı"); //virgülden önce 4 karakter virgülden sonra 2 karakter toplamda 6 karakter ÖRN : $$$$.$$

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("kategori seçiniz");
        }
    }
}
