using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<CourseViewModel> GetByCourseIdAsync(string courseId);

        Task<bool> CreateCourseAsync(CourseCreateInput model);

        Task<bool> UpdateCourseAsync(CourseUpdateInput model);

        Task<bool> DeleteCourseAsync(string courseId);
    }
}

