using CMS.Application.Extensions;
using CMS.Application.Models.DTOs;
using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.Presentation.Controllers
{
    // Authorize => Kullanıcının işlem yapabilmesi için login olmasını istiyorsak kullanmalıyız.
    // AutoValidateAntiforgeryToken => İstek atan kullanıcıya bir Token gönderir. Talep eden kullanıcı ile server arasında kimlik doğrulaması yapar. 

    [Authorize, AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            this._appUserService = appUserService;
        }

        // Authorize' yi kırmış olduk. Request atıldığında buraya erişilebilecek
        [AllowAnonymous]
        public IActionResult Register()
        {
            // Kullanıcı öncesinde giriş yapmışsa
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                // Ya Succeeded yada Failed olur
                var result = await _appUserService.Register(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Geçersiz Giriş Denemesi!");
            }
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Edit(string userName)
        {
            if (userName== User.Identity.Name)
            {
                // login olmuş kullanıcının id' sini yakaladık
                var user = await _appUserService.GetById(User.GetUserId());

                if (user==null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                TempData["Success"] = "Profiliniz güncellendi.";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                TempData["Error"] = "Profiliniz güncellenemedi !";
                return View(model);
            }
        }
    }
}
