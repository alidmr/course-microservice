using System.Threading.Tasks;
using FreeCourse.Web.Models.Basket;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel model);
        Task<BasketViewModel> GetAsync();
        Task<bool> DeleteAsync();
        Task AddBasketItemAsync(BasketItemViewModel model);
        Task<bool> DeleteBasketItemAsync(string courseId);
        Task<bool> ApplyDiscountAsync(string discountCode);
        Task<bool> CancelApplyDiscountAsync();
    }
}