﻿using CMS.Application.Models.DTOs;
using CMS.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public IActionResult List()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([MinLength(2), Required] string roleName)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol oluşturuldu.";
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Min 2 karakter olması gerekir.");
            return View();
        }

        public async Task<IActionResult> AssignedRoleToUsers(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNoRole = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNoRole;
                list.Add(user);
            }
            return View(new AssignedRoleToUserDTO
            {
                Role = role,
                HasRole = hasRole,
                HasNoRole = hasNoRole
            });
        }

        [HttpPost]
        public async Task<IActionResult> AssignedRoleToUsers(AssignedRoleToUserDTO model)
        {
            IdentityResult result;

            foreach (string userId in model.AddIds ?? new string[] { })
            {
                // Rol atanacak user' ı id'sinden yakaladık.
                AppUser user = await _userManager.FindByIdAsync(userId);
                // Yukarıda yakaladığımız user' a modelden bize gelen role ismini atadık
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            foreach (string userId in model.DeleteIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }
            return RedirectToAction("List");
        }
    }
}
