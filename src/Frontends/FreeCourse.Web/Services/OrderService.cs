using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Models.Order;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService,
            ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckInfoInput model)
        {
            var basket = await _basketService.GetAsync();

            PaymentInfoInput paymentInfoInput = new PaymentInfoInput()
            {
                CardName = model.CardName,
                Expiration = model.Expiration,
                CardNumber = model.CardNumber,
                CVV = model.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Ödeme alınamadı", IsSuccess = false };
            }

            OrderCreateInput orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = model.Province,
                    District = model.District,
                    Line = model.Line,
                    Street = model.Street,
                    ZipCode = model.ZipCode
                }
            };

            basket.BasketItems.ForEach(x =>
            {
                OrderItemCreateInput orderItemCreateInput = new OrderItemCreateInput()
                {
                    ProductId = x.CourseId,
                    Price = x.GetCurrentPrice,
                    ProductName = x.CourseName,
                    PictureUrl = ""
                };

                orderCreateInput.OrderItems.Add(orderItemCreateInput);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Şipariş oluşturulamadı", IsSuccess = false };
            }

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccess = true;

            var deleteBasketResult = await _basketService.DeleteAsync();

            return orderCreatedViewModel.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckInfoInput model)
        {
            OrderCreateInput orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = model.Province,
                    District = model.District,
                    Line = model.Line,
                    Street = model.Street,
                    ZipCode = model.ZipCode
                }
            };

            var basket = await _basketService.GetAsync();

            basket.BasketItems.ForEach(x =>
            {
                OrderItemCreateInput orderItemCreateInput = new OrderItemCreateInput()
                {
                    ProductId = x.CourseId,
                    Price = x.GetCurrentPrice,
                    ProductName = x.CourseName,
                    PictureUrl = ""
                };

                orderCreateInput.OrderItems.Add(orderItemCreateInput);
            });

            PaymentInfoInput paymentInfoInput = new PaymentInfoInput()
            {
                CardName = model.CardName,
                Expiration = model.Expiration,
                CardNumber = model.CardNumber,
                CVV = model.CVV,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", IsSuccess = false };
            }

            var basketDeleteResult = await _basketService.DeleteAsync();

            return new OrderSuspendViewModel() { IsSuccess = true };
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response?.Data;
        }
    }
}