using ecpmmerceApp.Application.DTOs.User;
using ecpmmerceApp.Application.Services.AuthenticationService;
using ecpmmerceApp.Domain.Interface.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogApp.Models;

namespace ProductCatalogApp.Controllers
{
    public class AccountController(IAuthenticationService authenticationService,IRoleManagment roleManagment) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
    {
        // Process login form submission (POST)
        if (ModelState.IsValid)
        {
                var result = await authenticationService.LoginUser(loginUser);

                if (result.Success)
                {
                    var userRole =await roleManagment.GetUserRole(loginUser.Email);
                    if(userRole=="Admin")
                        return RedirectToAction("Index", "Dashboard");
                    else
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    
                        foreach (var err in result.Errors)
                        {
                        if(err.Contains("password"))
                            ModelState.AddModelError("password", err);
                        else
                            ModelState.AddModelError("Email", err);
                    }
                    
                    return View(loginUser);
                }
            }
            return View(loginUser);

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUser user)
        {

            if (ModelState.IsValid)
            {
                var result = await authenticationService.CreateUser(user);
                if (result.Success)
                {
                    return RedirectToAction("LoginUser");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        if (err.Contains("Username"))
                            ModelState.AddModelError("FullName", err);
                        else
                            ModelState.AddModelError("Email", err);
                    }
                    return View(user);
                }
            }
            return View(user);


        }
        public async Task<IActionResult> LogOut()
        {
            await authenticationService.LogOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
