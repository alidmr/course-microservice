using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }

        [Display(Name = "Kurs İsmi")]
        public string Name { get; set; }

        [Display(Name = "Kurs Açıklaması")]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyatı")]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Kurs Kategorisi")]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs resmi")]
        public IFormFile PhotoFormFile { get; set; }

    }
}
