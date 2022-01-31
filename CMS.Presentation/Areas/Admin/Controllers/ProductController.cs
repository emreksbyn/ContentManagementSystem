using CMS.Application.Models.DTOs;
using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }

        public async Task<IActionResult> Create() => View(new CreateProductDTO() { Categories = await _categoryService.GetCategories() });

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(model);
                TempData["Success"] = "Ürün eklendi.";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Ürün eklenemedi!";
                model.Categories = await _categoryService.GetCategories();
                return View(model);
            }
        }

        public async Task<IActionResult> List() => View(await _productService.GetProducts());

        public async Task<IActionResult> Update(int id) => View(await _productService.GetProducts());

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Ürün güncellendi.";
                await _productService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Ürün güncellenemedi";
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
