using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class Promedio
    {
        [DisplayName("Id de equipo")]
        public int EquipoId { get; set; }
        
        [DisplayName("Equipo")]
        public string EquipoNombre { get; set; }
        
        
        [DisplayName("Promedio de carreras")]
        public decimal PromedioCarreras { get; set; }
        
        [DisplayName("Promedio de hits")]
        public decimal PromedioHits { get; set; }
        
        [DisplayName("Promedio de errores")]
        public decimal PromedioErrores { get; set; }
       
        [DisplayName("Promedio de juegos ganados")]
        public decimal PromedioJuegoGanado { get; set; }
        
        [DisplayName("Promedio de juegos perdidos")]
        public decimal PromedioJuegoPerdido { get; set; }
        
        [DisplayName("Promedio de juegos empatados")]
        public decimal PromedioJuegoEmpate { get; set; }
    }
}
