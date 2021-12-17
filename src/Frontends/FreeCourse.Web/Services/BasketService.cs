using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets", model);

            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetAsync()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel?.Data;
        }

        public async Task<bool> DeleteAsync()
        {
            var response = await _httpClient.DeleteAsync("baskets");
            return response.IsSuccessStatusCode;
        }

        public async Task AddBasketItemAsync(BasketItemViewModel model)
        {
            var basket = await GetAsync();
            if (basket != null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == model.CourseId))
                {
                    basket.BasketItems.Add(model);
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems.Add(model);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<bool> DeleteBasketItemAsync(string courseId)
        {
            var basket = await GetAsync();

            if (basket == null)
            {
                return false;
            }

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (deleteBasketItem == null)
            {
                return false;
            }
            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult)
            {
                return false;
            }

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscountAsync(string discountCode)
        {
            await CancelApplyDiscountAsync();

            var basket = await GetAsync();

            if (basket == null)
            {
                return false;
            }

            var hasDiscount = await _discountService.GetDiscountAsync(discountCode);

            if (hasDiscount == null)
            {
                return false;
            }

            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);

            var result = await SaveOrUpdate(basket);

            return result;
        }

        public async Task<bool> CancelApplyDiscountAsync()
        {
            var basket = await GetAsync();

            if (basket == null || basket.DiscountCode == null)
                return false;

            basket.CancelDiscount();

            var result = await SaveOrUpdate(basket);

            return result;
        }
    }
}