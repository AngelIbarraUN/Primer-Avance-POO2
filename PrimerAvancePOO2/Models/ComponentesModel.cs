using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrimerAvancePOO2.Models;
public class ComponentesModel
{
    public ComponentesModel()
    {
       
    }
     public Guid Id { get; set; }
    public string Name { get; set; }    
    public string Descripcion { get; set; }
    public int Precio { get; set; } 
    public int Cantidad { get; set; }
    public Guid? ProveedorId { get; set; }    
    public string? ComponenteName { get; set; } 
    public List< SelectListItem> ListaProveedores { get; set; }
}