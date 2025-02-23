using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ResumeAnalyzer.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // GET: /Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Google");
        }
        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync("Cookies");
            if (result?.Principal == null)
            {
                return RedirectToAction("Index", "Home"); // Login failed
            }

            // User is authenticated! Access claims like email or name
            var email = result.Principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            // Optionally save user data or redirect
            return RedirectToAction("Index", "Home");
        }
    }
}
