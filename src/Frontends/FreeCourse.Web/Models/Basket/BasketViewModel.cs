using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Web.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }

        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    //Örnek kurs fiyatı 100 Tl indirim oranı %10
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal) DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2)); //90.00
                    });
                }

                return _basketItems;
            }
            set => _basketItems = value;
        }

        public decimal TotalPrice => BasketItems.Sum(x => x.GetCurrentPrice);

        public bool HasDiscount => !string.IsNullOrEmpty(DiscountCode);
    }
}