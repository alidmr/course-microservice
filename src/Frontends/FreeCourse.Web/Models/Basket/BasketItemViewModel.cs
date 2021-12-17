namespace FreeCourse.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }

        private decimal? _discountAppliedPrice;

        public decimal GetCurrentPrice => _discountAppliedPrice ?? Price;

        public void AppliedDiscount(decimal discountPrice)
        {
            _discountAppliedPrice = discountPrice;
        }
    }
}