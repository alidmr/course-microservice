using System.Threading.Tasks;
using FreeCourse.Web.Models.Order;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.GetAsync();
            ViewBag.Basket = basket;


            return View(new CheckInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckInfoInput checkInfoInput)
        {
            var orderStatus = await _orderService.CreateOrder(checkInfoInput);

            if (!orderStatus.IsSuccess)
            {
                var basket = await _basketService.GetAsync();
                ViewBag.Basket = basket;

                ViewBag.Error = orderStatus.Error;
                return View();
            }

            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}
