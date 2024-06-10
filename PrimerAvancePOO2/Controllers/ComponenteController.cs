using PrimerAvancePOO2.Models;
using System.Reflection.Metadata.Ecma335;
namespace PrimerAvancePOO2.Controllers;
using Microsoft.AspNetCore.Mvc;
using PrimerAvancePOO2.Entities;

public class ComponenteController : Controller
{
    public ComponenteController()
    {
   
    }
    public IActionResult ComponentesList()
    {
        List<ComponentesModel> list =new List<ComponentesModel>();
        return View(list);
    }
    public IActionResult ComponentesAdd(ComponentesModel componente)
    {
        if (ModelState.IsValid)
        {
            Componentes componenteadd =new Componentes();
            componenteadd.Id= new Guid ();
            componenteadd.Name=componente.Name;
            componenteadd.Descripcion=componente.Descripcion;
            componenteadd.precio=componente.Precio;
            componenteadd.cantidad=componente.Cantidad;
            

        }
        return View();
    }
    public IActionResult ComponentesDeleted()
    {
        return View();
    }
    public IActionResult ComponentesEdit()
    {
        return View();
    }
}
