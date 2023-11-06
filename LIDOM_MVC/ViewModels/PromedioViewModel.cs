using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class PromedioViewModel
    {
        [DisplayName("Id de equipo")]
        public int EquipoId { get; set; }
        
        [DisplayName("Equipo")]
        public string EquipoNombre { get; set; }
        
        
        [DisplayName("Promedio de Carreras")]
        public decimal PromedioCarreras { get; set; }
        
        [DisplayName("Promedio de Hits")]
        public decimal PromedioHits { get; set; }
        
        [DisplayName("Promedio de Errores")]
        public decimal PromedioErrores { get; set; }
       
        [DisplayName("Promedio de Juegos Ganados")]
        public decimal PromedioJuegoGanado { get; set; }
        
        [DisplayName("Promedio de Juegos Perdidos")]
        public decimal PromedioJuegoPerdido { get; set; }
        
        [DisplayName("Promedio de Juegos Empatados")]
        public decimal PromedioJuegoEmpate { get; set; }
    }
}
