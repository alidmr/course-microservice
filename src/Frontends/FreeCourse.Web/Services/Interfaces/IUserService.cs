using System.Threading.Tasks;
using FreeCourse.Web.Models.User;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
