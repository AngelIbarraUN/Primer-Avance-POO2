
using PrimerAvancePOO2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimerAvancePOO2.Entities;

namespace PrimerAvancePOO2.Controllers;

public class ProveedorController : Controller
{
    private readonly ILogger<ProveedorController> _logger;
    private readonly ApplicationDbContext _context;
    public ProveedorController(ILogger<ProveedorController>logger,ApplicationDbContext context)
    {
        _logger=logger;
        _context=context;
    }
    public IActionResult ProveedoresList()
    {
        List<ProveedoresModel> proveedorlista=_context.Proveedor.Select(proveedor=>new ProveedoresModel()
        {
            Id=proveedor.Id,
            Name=proveedor.Name,
            Telefono=proveedor.Telefono,
            Direccion=proveedor.Direccion,
            Email=proveedor.Email
        }).ToList();
        return View(proveedorlista);
    }
    public IActionResult ProveedoresAdd()
        {
            return View();
        }
    
    [HttpPost]
    public IActionResult ProveedoresAdd(ProveedoresModel proveedor)
    {
        if(!ModelState.IsValid)
       {
        return View(proveedor);
       }
        var proveedorinfo =new Proveedor();
        proveedorinfo.Id =new Guid();
        proveedorinfo.Name = proveedor.Name;
        proveedorinfo.Telefono=proveedor.Telefono;
        proveedorinfo.Direccion=proveedor.Direccion;
        proveedorinfo.Email=proveedor.Email;
        this._context.Proveedor.Add(proveedorinfo);
        this._context.SaveChanges();
        return RedirectToAction("ProveedoresList","Proveedor");
    }
   
    public IActionResult ProveedoresEdit(Guid Id)
    {
       
    var proveedorActualizar = this._context.Proveedor.Where(c => c.Id == Id).FirstOrDefault();
    if (proveedorActualizar == null)
    {
        return RedirectToAction("ProveedoresList","Proveedor");
    }

    ProveedoresModel model = new ProveedoresModel();
    
        model.Id = proveedorActualizar.Id;
        model.Name = proveedorActualizar.Name;
        model.Telefono = proveedorActualizar.Telefono;
        model.Direccion = proveedorActualizar.Direccion;
        model.Email = proveedorActualizar.Email;
    
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ProveedoresEdit(ProveedoresModel proveedor)
    {
    
        Proveedor proveedorActualizar = this._context.Proveedor.Where(c => c.Id == proveedor.Id).First();
        if (proveedorActualizar == null)
        {
            return View(proveedor);
        }
        if (!ModelState.IsValid)
            {
                return View(proveedor);
            }


        proveedorActualizar.Name = proveedor.Name;
        proveedorActualizar.Telefono = proveedor.Telefono;
        proveedorActualizar.Direccion = proveedor.Direccion;
        proveedorActualizar.Email = proveedor.Email;

        this._context.Proveedor.Update(proveedorActualizar);
        this._context.SaveChanges();

        return RedirectToAction("ProveedoresList","Proveedor");
    
    }
    public IActionResult ProveedoresDeleted(Guid Id)
    {
       
      var proveedoresborrado = this._context.Proveedor.Where(c => c.Id == Id).FirstOrDefault();
    if (proveedoresborrado == null)
    {
        return RedirectToAction("ProveedoresList","Proveedor");
    }

    ProveedoresModel model = new ProveedoresModel();
        model.Id = proveedoresborrado.Id;
        model.Name = proveedoresborrado.Name;
        model.Telefono = proveedoresborrado.Telefono;
        model.Direccion = proveedoresborrado.Direccion;
        model.Email = proveedoresborrado.Email;
    
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ProveedoresDeleted(ProveedoresModel proveedor)
    {
       bool proveedordeleted = this._context.Proveedor.Any(c => c.Id == proveedor.Id);
    if (!proveedordeleted)
    {
        return View(proveedor);
    }

    Proveedor proveedorentity =this._context.Proveedor.
            Where(p => p.Id == proveedor.Id).First();
    this._context.Proveedor.Remove(proveedorentity);
    this._context.SaveChanges();

    return RedirectToAction("ProveedoresList","Proveedor");
    }
}
