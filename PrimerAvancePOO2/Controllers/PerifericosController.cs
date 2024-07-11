
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrimerAvancePOO2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimerAvancePOO2.Entities;

namespace PrimerAvancePOO2.Controllers
{
    public class PerifericosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PerifericosController> _logger;

        public PerifericosController(ApplicationDbContext context,ILogger<PerifericosController> logger)
        {
            this._context = context;
            this._logger =logger;
        }

        public IActionResult PerifericosList()
        {
            List<PerifericosModel> perifericos 
            = _context.Perifericos.Select(periferico => new PerifericosModel()
            {
                Id = periferico.Id,
                Name = periferico.Name,
                Tipo = periferico.Tipo,
                precio=periferico.precio
            }).ToList();

            return View(perifericos);
        }   

        public IActionResult PerifericosAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PerifericosAdd(PerifericosModel periferico)
        {
            if (!ModelState.IsValid)
            {
                return View(periferico);
            }

            var perifericosentity = new Perifericos();
            perifericosentity.Id = new Guid();
            perifericosentity.Name = periferico.Name;
            perifericosentity.Tipo = periferico.Tipo;
            perifericosentity.precio=periferico.precio;

            this._context.Perifericos.Add(perifericosentity);
            this._context.SaveChanges();
            
            return RedirectToAction("PerifericosList","Perifericos");
        }

        public IActionResult PerifericosEdit(Guid Id)
        {
            var periferico = this._context.Perifericos
                .Where(p => p.Id == Id).FirstOrDefault();
            if (periferico == null)
            {
                return RedirectToAction("PerifericosList","Perifericos");
            }

            PerifericosModel model = new PerifericosModel();
            model.Id = periferico.Id;
            model.Name =periferico.Name;
            model.Tipo = periferico.Tipo;
            model.precio = periferico.precio;
            return View(model);
        }

        [HttpPost]
        public IActionResult PerifericosEdit(PerifericosModel periferico)
        {
            Perifericos perifericosentity = this._context.Perifericos
             .Where(p => p.Id == periferico.Id).First();
            if (perifericosentity == null)
            {
                return View(periferico);
            }
            
            if (!ModelState.IsValid)
            {
                return View(periferico);
            }
            perifericosentity.Name = periferico.Name;
            perifericosentity.Tipo= periferico.Tipo;
            perifericosentity.precio=periferico.precio;
            this._context.Perifericos.Update(perifericosentity);
            this._context.SaveChanges();
            return RedirectToAction("PerifericosList","Perifericos");
        }

        public IActionResult PerifericosDeleted(Guid Id)
        {
            var periferico = this._context.Perifericos
            .Where(p => p.Id == Id).FirstOrDefault();
            
            if (periferico == null)
            {
                return RedirectToAction("PerifericosList","Periferico");
            }
            PerifericosModel model = new PerifericosModel();
            model.Id = periferico.Id;
            model.Name = periferico.Name;
            model.Tipo = periferico.Tipo;
            model.precio=periferico.precio;
            return View(model);
        }

        [HttpPost]
        public IActionResult PerifericosDeleted(PerifericosModel periferico)
        {
            bool perifericodeleted = this._context.Perifericos.Any(p => p.Id == periferico.Id);
            if (!perifericodeleted)
            {
                return View(periferico);
            }


            Perifericos perifericosentity = this._context.Perifericos
            .Where(p => p.Id == periferico.Id).First();

            this._context.Perifericos.Remove(perifericosentity);
            this._context.SaveChanges();
            
            return RedirectToAction("PerifericosList","Perifericos");
        }

    }
}
