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

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public Task<bool> ApplyDiscountAsync(string discountCode)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CancelApplyDiscountAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}