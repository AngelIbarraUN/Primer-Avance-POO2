
namespace PrimerAvancePOO2.Models;

public class ProveedoresModel
{
    public ProveedoresModel()
    {
    }
    public Guid Id { get; set; }
    public string Name { get; set; }    
    public string Telefono { get; set; }
    public string Direccion { get; set; } 
    public string Email { get; set; }   
}