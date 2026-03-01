namespace Ventas.Models
{
    public class VentaMonto
    {
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public int CantidadTotalVendida { get; set; }
        public decimal ValorTotalVenta { get; set; }
    }
}
