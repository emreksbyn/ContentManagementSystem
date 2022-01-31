using CMS.Application.Models.DTOs;
using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => this._categoryService = categoryService;

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Kategori eklendi.";
                await _categoryService.Create(model);
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Kategori eklenemedi !";
                return View(model);
            }
        }

        public async Task<IActionResult> List() => View(await _categoryService.GetCategories());

        public async Task<IActionResult> Update(int id) => View(await _categoryService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Kategori güncellendi.";
                await _categoryService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Kategori güncellenemedi !";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
