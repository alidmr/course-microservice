﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        [Display(Name = "Kurs İsmi")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Kurs Açıklaması")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyatı")]
        [Required]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Kurs Kategorisi")]
        [Required]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs resmi")]
        [Required]
        public IFormFile PhotoFormFile { get; set; }
    }
}
