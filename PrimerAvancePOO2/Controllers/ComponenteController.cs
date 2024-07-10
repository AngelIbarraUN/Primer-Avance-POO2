
using PrimerAvancePOO2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimerAvancePOO2.Entities;
using Microsoft.EntityFrameworkCore;

namespace PrimerAvancePOO2.Controllers;

public class ComponenteController : Controller
{
    private readonly ILogger<ComponenteController> _logger;
    private readonly ApplicationDbContext _context;
    public ComponenteController(ILogger<ComponenteController>logger,ApplicationDbContext context)
    {
        _logger=logger;
        _context=context;
    }
    public IActionResult ComponentesList()
    {
        List<ComponentesModel> componentelista
        =_context.Componentes.Select(componente=>new ComponentesModel()
        {
            Id=componente.Id,
            Name=componente.Name,
            Descripcion=componente.Descripcion,
            Precio=componente.precio,
            Cantidad=componente.cantidad
        }).ToList();
        return View(componentelista);
    }

   public IActionResult ComponentesAdd()
        {
            return View();
        }

    [HttpPost]
    public IActionResult ComponentesAdd(ComponentesModel componente)
    {
        if(!ModelState.IsValid)
       { 
       return View(componente);
       }
        var componentesinfo =new Componentes();
       componentesinfo.Id =new Guid();
        componentesinfo.Name = componente.Name;
        componentesinfo.Descripcion=componente.Descripcion;
        componentesinfo.precio=componente.Precio;
        componentesinfo.cantidad=componente.Cantidad;
        this._context.Componentes.Add(componentesinfo);
        this._context.SaveChanges();
        return RedirectToAction("ComponentesList","Componente");
    }
   
    public IActionResult ComponentesEdit(Guid Id)
    {
       
     var componenteActualizar = this._context.Componentes.Where(c => c.Id == Id).FirstOrDefault();
    if (componenteActualizar == null)
    {
        return RedirectToAction("ComponentesList,Componente");
    }

    ComponentesModel model = new ComponentesModel();
    
        model.Id = componenteActualizar.Id;
        model.Name = componenteActualizar.Name;
       model. Descripcion = componenteActualizar.Descripcion;
        model.Precio = componenteActualizar.precio;
        model.Cantidad = componenteActualizar.cantidad;
    
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ComponentesEdit(ComponentesModel componente)
    {

      var componenteActualizar = this._context.Componentes.Where(c => c.Id == componente.Id).First();
       
        if (componenteActualizar == null)
        {
            return View(componente);
        }

        if (!ModelState.IsValid)
            {
                return View(componente);
            }

        componenteActualizar.Name = componente.Name;
        componenteActualizar.Descripcion = componente.Descripcion;
        componenteActualizar.precio = componente.Precio;
        componenteActualizar.cantidad = componente.Cantidad;

        this._context.Componentes.Update(componenteActualizar);
        this._context.SaveChanges();

        return RedirectToAction("ComponentesList","Componente");

    }
     
    public IActionResult ComponentesDeleted(Guid Id)
    {
       
      var componentesborrado = this._context.Componentes.Where(c => c.Id == Id).FirstOrDefault();
    if (componentesborrado == null)
    {
        return RedirectToAction("ComponentesList","Componente");
    }

    var model = new ComponentesModel();
    
        model.Id = componentesborrado.Id;
        model.Name = componentesborrado.Name;
        model.Descripcion = componentesborrado.Descripcion;
        model.Precio = componentesborrado.precio;
        model.Cantidad = componentesborrado.cantidad;
    
        return View(model);
    }
   
    [HttpPost]
    public async Task<IActionResult> ComponentesDeleted(ComponentesModel componente)
    {
       bool componenteDeleted= await this._context.Componentes.AnyAsync(c => c.Id == componente.Id);
    if (!componenteDeleted )
    {
      
        return View(componente);
    }

    Componentes componenteentity =this._context.Componentes
    .Where(c => c.Id == componente.Id).First();

    this._context.Componentes.Remove(componenteentity);
    this._context.SaveChanges();

    return RedirectToAction("ComponentesList","Componente");
    }
}
