using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Auth;
using IdentityModel.Client;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput model);

        Task<TokenResponse> GetAccessTokenByRefreshToken();
        
        Task RevokeRefreshToken();
    }
}