using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Order;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron İletişim - direk order mikroservisine istek yapılacak
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckInfoInput model);

        /// <summary>
        /// Asenkron İletişim - sipariş bilgileri rabbitmqya gönderilecek
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OrderSuspendViewModel> SuspendOrder(CheckInfoInput model);

        Task<List<OrderViewModel>> GetOrder();
    }
}