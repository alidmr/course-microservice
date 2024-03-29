using System.Threading.Tasks;
using FreeCourse.Web.Models.FakePayment;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput model);
    }
}