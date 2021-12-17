using System.Linq;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Models.Discount;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IDiscountService _discountService;

        public BasketController(ICatalogService catalogService, IBasketService basketService, IDiscountService discountService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetAsync();
            return View(basket);
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseIdAsync(courseId);

            var basketItem = new BasketItemViewModel()
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Price = course.Price
            };

            await _basketService.AddBasketItemAsync(basketItem);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteBasketItem(string courseId)
        {
            var result = await _basketService.DeleteBasketItemAsync(courseId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["DiscountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction(nameof(Index));
            }

            var discountStatus = await _basketService.ApplyDiscountAsync(discountApplyInput.Code);

            TempData["DiscountStatus"] = discountStatus;  //bir requestten başka bir requestte data taşıma yapar

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelApplyDiscount()
        {
            var result = await _basketService.CancelApplyDiscountAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
