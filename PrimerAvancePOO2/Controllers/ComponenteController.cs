
using PrimerAvancePOO2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimerAvancePOO2.Entities;

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
        List<ComponentesModel> list=new List<ComponentesModel>();
        list=_context.Componentes.Select(b=>new ComponentesModel()
        {
            Id=b.Id,
            Name=b.Name,
            Descripcion=b.Descripcion,
            Precio=b.precio,
            Cantidad=b.cantidad
        }).ToList();
        return View(list);
    }


    [HttpGet]
    public IActionResult ComponentesAdd()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ComponentesAdd(ComponentesModel componentes)
    {
        if(ModelState.IsValid)
       { Componentes componentesinfo =new Componentes();
       componentesinfo.Id =new Guid();
        componentesinfo.Name = componentes.Name;
        componentesinfo.Descripcion=componentes.Descripcion;
        componentesinfo.precio=componentes.Precio;
        componentesinfo.cantidad=componentes.Cantidad;
        this._context.Componentes.Add(componentesinfo);
        this._context.SaveChanges();
        return RedirectToAction("ComponentesList");
       }
        return View();
    }
   [HttpGet]
    public IActionResult ComponentesEdit(Guid Id)
    {
       
      Componentes componenteActualizar = this._context.Componentes.Where(c => c.Id == Id).FirstOrDefault();
    if (componenteActualizar == null)
    {
        return RedirectToAction("ComponentesList");
    }

    ComponentesModel model = new ComponentesModel
    {
        Id = componenteActualizar.Id,
        Name = componenteActualizar.Name,
        Descripcion = componenteActualizar.Descripcion,
        Precio = componenteActualizar.precio,
        Cantidad = componenteActualizar.cantidad
    };
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ComponentesEdit(ComponentesModel model)
    {
        if (ModelState.IsValid)
    {
        Componentes componenteActualizar = this._context.Componentes.Where(c => c.Id == model.Id).FirstOrDefault();
        if (componenteActualizar == null)
        {
            return RedirectToAction("ComponentesList");
        }

        componenteActualizar.Name = model.Name;
        componenteActualizar.Descripcion = model.Descripcion;
        componenteActualizar.precio = model.Precio;
        componenteActualizar.cantidad = model.Cantidad;

        this._context.Componentes.Update(componenteActualizar);
        this._context.SaveChanges();

        return RedirectToAction("ComponentesList");

    }
        return View(model);
    }
     [HttpGet]
    public IActionResult ComponentesDeleted(Guid Id)
    {
       
      Componentes componentesborrado = this._context.Componentes.Where(c => c.Id == Id).FirstOrDefault();
    if (componentesborrado == null)
    {
        return RedirectToAction("ComponentesList");
    }

    ComponentesModel model = new ComponentesModel
    {
        Id = componentesborrado.Id,
        Name = componentesborrado.Name,
        Descripcion = componentesborrado.Descripcion,
        Precio = componentesborrado.precio,
        Cantidad = componentesborrado.cantidad
    };
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ComponentesDeleted(ComponentesModel model)
    {
       Componentes componentesborrado = _context.Componentes.FirstOrDefault(c => c.Id == model.Id);
    if (componentesborrado == null)
    {
        return RedirectToAction("ComponentesList");
    }

    _context.Componentes.Remove(componentesborrado);
    _context.SaveChanges();

    return RedirectToAction("ComponentesList");
    }
}
