using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimerAvancePOO2.Models;

namespace PrimerAvancePOO2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        public UsuariosController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [AllowAnonymous]
        public IActionResult Registros()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Registros(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var Usuario = new IdentityUser(){Email = modelo.Email, UserName = modelo.Email};

            var resultado = await userManager.CreateAsync(Usuario, password: modelo.Password);
            if(resultado.Succeeded)
            {
                await signInManager.SignInAsync(Usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach(var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(modelo);
            }
        }

        
        [AllowAnonymous]
        public IActionResult InicioSesion(string mensaje = null)
        {
            if(mensaje is not null)
            {
                ViewData["mensaje"] = mensaje;
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> InicioSesion(LoginViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.Email,modelo.Password, modelo.Recuerdame, lockoutOnFailure: false);

            if(resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(modelo);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }
        
        
    }
}