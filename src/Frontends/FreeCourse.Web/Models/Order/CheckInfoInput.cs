using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Order
{
    public class CheckInfoInput
    {
        [Display(Name = "İl")]
        public string Province { get; set; }
        [Display(Name = "İlçe")]
        public string District { get; set; }
        [Display(Name = "Cadde")]
        public string Street { get; set; }
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "Adres")]
        public string Line { get; set; }
        
        
        [Display(Name = "Kart İsim Soyisim")]
        public string CardName { get; set; }
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "Son Kullanma Tarihi")]
        public string Expiration { get; set; }
        [Display(Name = "CCV/CVC2 numarası")]
        public string CVV { get; set; }
    }
}