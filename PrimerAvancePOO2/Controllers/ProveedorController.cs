
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
        List<ProveedoresModel> list=new List<ProveedoresModel>();
        list=_context.Proveedor.Select(b=>new ProveedoresModel()
        {
            Id=b.Id,
            Name=b.Name,
            Telefono=b.Telefono,
            Direccion=b.Direccion,
            Email=b.Email
        }).ToList();
        return View(list);
    }
    public IActionResult ProveedoresAdd(ProveedoresModel proveedor)
    {
        if(ModelState.IsValid)
       { Proveedor proveedorinfo =new Proveedor();
       proveedorinfo.Id =new Guid();
        proveedorinfo.Name = proveedor.Name;
        proveedorinfo.Telefono=proveedor.Telefono;
        proveedorinfo.Direccion=proveedor.Direccion;
        proveedorinfo.Email=proveedor.Email;
        this._context.Proveedor.Add(proveedorinfo);
        this._context.SaveChanges();
       }
        return View();
    }
   [HttpGet]
    public IActionResult ProveedoresEdit(Guid Id)
    {
       
      Proveedor proveedorActualizar = this._context.Proveedor.Where(c => c.Id == Id).FirstOrDefault();
    if (proveedorActualizar == null)
    {
        return RedirectToAction("ProveedoresList");
    }

    ProveedoresModel model = new ProveedoresModel
    {
        Id = proveedorActualizar.Id,
        Name = proveedorActualizar.Name,
        Telefono = proveedorActualizar.Telefono,
        Direccion = proveedorActualizar.Direccion,
        Email = proveedorActualizar.Email
    };
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ProveedoresEdit(ProveedoresModel model)
    {
        if (ModelState.IsValid)
    {
        Proveedor proveedorActualizar = this._context.Proveedor.Where(c => c.Id == model.Id).FirstOrDefault();
        if (proveedorActualizar == null)
        {
            return RedirectToAction("ProveedoresList");
        }

        proveedorActualizar.Name = model.Name;
        proveedorActualizar.Telefono = model.Telefono;
        proveedorActualizar.Direccion = model.Direccion;
        proveedorActualizar.Email = model.Email;

        this._context.Proveedor.Update(proveedorActualizar);
        this._context.SaveChanges();

        return RedirectToAction("ProveedoresList");

    }
        return View(model);
    }
     [HttpGet]
    public IActionResult ProveedoresDeleted(Guid Id)
    {
       
      Proveedor proveedoresborrado = this._context.Proveedor.Where(c => c.Id == Id).FirstOrDefault();
    if (proveedoresborrado == null)
    {
        return RedirectToAction("ProveedoresList");
    }

    ProveedoresModel model = new ProveedoresModel
    {
        Id = proveedoresborrado.Id,
        Name = proveedoresborrado.Name,
        Telefono = proveedoresborrado.Telefono,
        Direccion = proveedoresborrado.Direccion,
        Email = proveedoresborrado.Email
    };
        return View(model);
    }
   
    [HttpPost]
    public IActionResult ProveedoresDeleted(ProveedoresModel model)
    {
       Proveedor proveedoresborrado = _context.Proveedor.FirstOrDefault(c => c.Id == model.Id);
    if (proveedoresborrado == null)
    {
        return RedirectToAction("ProveedoresList");
    }

    _context.Proveedor.Remove(proveedoresborrado);
    _context.SaveChanges();

    return RedirectToAction("ProveedoresList");
    }
}
