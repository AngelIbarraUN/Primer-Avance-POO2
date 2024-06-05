
using System.Reflection.Metadata.Ecma335;
namespace PrimerAvancePOO2.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ComponenteController : Controller
{
    public ComponenteController()
    {
   
    }
    public IActionResult Componentes()
    {
        return View();  
    }

}
