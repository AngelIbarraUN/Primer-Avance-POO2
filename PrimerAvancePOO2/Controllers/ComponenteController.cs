using PrimerAvancePOO2.Models;
using System.Reflection.Metadata.Ecma335;
namespace PrimerAvancePOO2.Controllers;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult ComponentesAdd()
    {
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
