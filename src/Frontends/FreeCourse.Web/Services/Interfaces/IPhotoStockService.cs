using System.Threading.Tasks;
using FreeCourse.Web.Models.PhotoStock;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhoto(IFormFile photo);
        Task<bool> DeletePhoto(string photoUrl);
    }
}
