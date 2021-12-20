using System;
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
            // birinci yol - senkron iletişim
            //var orderStatus = await _orderService.CreateOrder(checkInfoInput);

            // ikinci yol -  asenkron iletişim
            var orderStatus = await _orderService.SuspendOrder(checkInfoInput);

            if (!orderStatus.IsSuccess)
            {
                var basket = await _basketService.GetAsync();
                ViewBag.Basket = basket;

                ViewBag.Error = orderStatus.Error;
                return View();
            }

            // birinci yol - senkron iletişim
            //return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });

            // ikinci yol -  asenkron iletişim
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1, 1000) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            var result = await _orderService.GetOrder();
            return View(result);
        }
    }
}
