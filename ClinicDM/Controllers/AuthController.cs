using ClinicDM.Models;
using ClinicDM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicDM.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string? returnUrl, LoginVM model)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {

                if (model.ProfilePicture.Length > 256 * 1024)
                {
                    ModelState.AddModelError("ProfilePicture", "Profile picture size should not exceed 256KB.");
                }

                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(model.ProfilePicture.ContentType))
                {
                    ModelState.AddModelError("ProfilePicture", "Only JPEG and PNG images are allowed.");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using var memoryStream = new MemoryStream();
                await model.ProfilePicture.CopyToAsync(memoryStream);
                user.ProfilePicture = memoryStream.ToArray();
            }

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Create user failed!!");
                return View(model);
            }

            result = await userManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "failed to add role!!");
                return View(model);
            }

            return RedirectToAction(nameof(CreateUser));
        }




    }
}