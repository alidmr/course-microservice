using System.Threading.Tasks;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _catalogService.GetAllCategoryAsync();

                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

                return View();
            }

            model.UserId = _sharedIdentityService.GetUserId;

            var result = await _catalogService.CreateCourseAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseIdAsync(id);
            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", course.CategoryId);

            CourseUpdateInput updateModel = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Feature = course.Feature,
                Picture = course.Picture,
                CategoryId = course.CategoryId,
                UserId = course.UserId
            };

            return View(updateModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _catalogService.GetAllCategoryAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name", model.CategoryId);
                return View(model);
            }

            var result = await _catalogService.UpdateCourseAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
