using DALayer.Entities;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PLayer.Helper;
using PLayer.Models;

namespace PLayer.Controllers
{
	//[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM register)
		{
			if (!ModelState.IsValid)
				return View(register);
			var user = new AppUser()
			{
				FirstName = register.FirstName,
				LastName = register.LastName,
				Email = register.Email,
				Agree = register.Agree,
				// UserName=register.FirstName+register.LastName,
				UserName = register.Email.Split("@")[0],
			};
			var result = await _userManager.CreateAsync(user, register.Password);
			if (result.Succeeded)
				return RedirectToAction(nameof(Login));
			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);
			return View(register);
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(SignInVM signIn)
		{

			if (!ModelState.IsValid)
				return View(signIn);

			var user =await _userManager.FindByEmailAsync(signIn.Email);
			if (user != null)
			{
				if( await _userManager.CheckPasswordAsync(user, signIn.Password))
				{
					var result = await _signInManager.PasswordSignInAsync(user, signIn.Password ,signIn.Rememberme ,false);

					if (result.Succeeded)
						return RedirectToAction("Index", "Home");

				}
			}
			ModelState.AddModelError("", "Error in Password or Email");
			return View();
		}


		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login)) ;
		}

		public IActionResult ForgetPassword()
		{
			return View();	
		}
		[HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
		{
			if(!ModelState.IsValid)	
				return View(model);
			var user =await _userManager.FindByEmailAsync(model.Email);
			if (user != null)
			{   var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var url =Url.Action("ResetPassword" ,"Account" , new {email=model.Email ,token} ,Request.Scheme);	
				var email = new Email()
				{
					To = model.Email,
					Subject = "Reset Password",
					Body = url

				};
				MailSetting.SendEmail(email); 

				return RedirectToAction(nameof(CheckYourInbox));

			}
			ModelState.AddModelError("", "Email Doesnt Exist");
			return View();
		}

		public IActionResult CheckYourInbox()
		{
			return View();	
		}
		public IActionResult ResetPassword (string email ,string token)
		{
             TempData["email"]  =email;
             TempData["token"]  = token;
            return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword (ResetPasswordVM passwordVM)
		{
			if (!ModelState.IsValid)
				return View(passwordVM);
			var email = TempData["email"] as string;
			var token = TempData["token"] as string;
			var user =await _userManager.FindByEmailAsync(email);

			if (user != null)
			{
               var result =await _userManager.ResetPasswordAsync(user, token, passwordVM.Password);
				if (result.Succeeded)
				return RedirectToAction(nameof(Login));

				foreach (var item in result.Errors)
					ModelState.AddModelError("", item.Description);
			}
			
			return View(passwordVM);
		}
    }
}
