namespace PrimerAvancePOO2.Models;
public class ComponentesModel
{
    public ComponentesModel()
    {
        
    }
    public Guid Id { get; set; }
    public string Name { get; set; }    
    public string Descripcion { get; set; }
    public int precio { get; set; } 
    public int cantidad { get; set; }

}