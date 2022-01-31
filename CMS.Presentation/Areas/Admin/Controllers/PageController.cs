using CMS.Application.Models.DTOs;
using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService) =>
            this._pageService = pageService;

        public async Task<IActionResult> List() =>
            View(await _pageService.GetPages());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageDTO model)
        {
            if (ModelState.IsValid)
            {
                var slug = await _pageService.isPageExists(model.Slug);
                if (slug != false)
                {
                    TempData["Warning"] = $"{model.Title} sayfası zaten var!";
                    return View(model);
                }
                else
                {
                    await _pageService.Create(model);
                    TempData["Success"] = $"{model.Title} sayfası eklendi.";
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["Error"] = "Sayfa eklenemedi!";
                return View(model);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var page = await _pageService.GetById(id);
            return View(page);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePageDTO model)
        {
            if (ModelState.IsValid)
            {
                await _pageService.Update(model);
                TempData["Success"] = "Sayfa güncellendi.";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Sayfa güncellenirken bir hata oluştu!";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _pageService.Delete(id);
            return RedirectToAction("List");
        }
    }
}