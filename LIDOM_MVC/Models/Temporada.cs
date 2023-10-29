namespace LIDOM_MVC.Models
{
    public class Temporada
    {
        public int TemId { get; set; }

        public string TemNombre { get; set; } = null!;

        public DateTime TemFecInicio { get; set; }

        public DateTime TemFecFinal { get; set; }
    }
}
