using AutoMapper;
using DALayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLayer.Models;
using System.Collections;

namespace PLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

    
        public async Task<IActionResult> Index(string email)
        { 

            if (string.IsNullOrEmpty(email))
            {
                var users =  _userManager.Users.Select(u => new UserVM
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToList();
                return View(users);

            }

            var user = await _userManager.FindByEmailAsync(email.Trim());
            if (user == null) return View(Enumerable.Empty<UserVM>());
            var mappeduser = new UserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(new List<UserVM> { mappeduser });

        }

        public async Task<IActionResult> Details(string id ,string viewName="Details")
        {
            if(string.IsNullOrEmpty(id)) 
                   return BadRequest();
           
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
                   return NotFound();
            var mappedUser = _mapper.Map<AppUser,UserVM>(user);
            mappedUser.Roles = await _userManager.GetRolesAsync(user);
            return  View(viewName,mappedUser);
        }
    
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id ,nameof(Edit));
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string id ,UserVM model)
        {
           if (id != model.Id) return BadRequest();  
           if (!ModelState.IsValid) return View(model);
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
               await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
               ModelState.AddModelError("" ,e.Message);   
            }
            return View(model);
            
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id,nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, UserVM model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                    return NotFound();

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(model);

        }


    }
}

