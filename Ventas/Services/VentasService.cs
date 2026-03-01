using Ventas.Models;
using Ventas.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Ventas.Services
{
    public class VentasService
    {
        private readonly VentasRepository _repository;
        public VentasService(VentasRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Venta>> GetAllVentas()
        {
            var ventas = await _repository.GetVentas();
            return ventas;
        }

        public async Task<IEnumerable<VentaCantidad>> GetThreeProductsMonthForAmount(int month, bool state)
        {
            var ventasTotales = await _repository.GetVentas();
            if(state)
            {
                var ventasMensuales = ventasTotales.Where(venta => venta.Fecha_compra.Month == month)
                                       .GroupBy(venta => venta.Producto)
                                       .Select(venta => {
                                           return new VentaCantidad()
                                           {
                                               Producto = venta.Key,
                                               Categoria = venta.First().Categoria,
                                               CantidadTotalVendida = venta.Sum(el => el.Cantidad)
                                           };

                                       }).OrderByDescending(venta => venta.CantidadTotalVendida).Take(10);
                return ventasMensuales;
            }
            else
            {
                var ventasMensuales = ventasTotales.Where(venta => venta.Fecha_compra.Month == month)
                                               .GroupBy(venta => venta.Producto)
                                               .Select(venta =>
                                               {
                                                   return new VentaCantidad()
                                                   {
                                                       Producto = venta.Key,
                                                       Categoria = venta.First().Categoria,
                                                       CantidadTotalVendida = venta.Sum(el => el.Cantidad)
                                                   };
                                               }).OrderBy(venta => venta.CantidadTotalVendida).Take(10);
                return ventasMensuales;
            }
            
        }
        public async Task<IEnumerable<VentaMonto>> GetThreeProductsMonthForValue(int month, bool state)
        {
                var ventasTotales = await _repository.GetVentas();
            if (state)
            {
                var ventasMensuales = ventasTotales.Where(venta => venta.Fecha_compra.Month == month)
                                                   .GroupBy(venta => venta.Producto)
                                                   .Select(venta =>
                                                   {
                                                       return new VentaMonto()
                                                       {
                                                           Producto = venta.Key,
                                                           Categoria = venta.First().Categoria,
                                                           CantidadTotalVendida = venta.Sum(el => el.Cantidad),
                                                           ValorTotalVenta = venta.Sum(el => el.Total_venta)
                                                       };
                                                   }).OrderByDescending(venta => venta.ValorTotalVenta).Take(10);
                return ventasMensuales;
            }
            else
            {
                var ventasMensuales = ventasTotales.Where(venta => venta.Fecha_compra.Month == month)
                                                   .GroupBy(venta => venta.Producto)
                                                   .Select(venta =>
                                                   {
                                                       return new VentaMonto()
                                                       {
                                                           Producto = venta.Key,
                                                           Categoria = venta.First().Categoria,
                                                           CantidadTotalVendida = venta.Sum(el => el.Cantidad),
                                                           ValorTotalVenta = venta.Sum(el => el.Total_venta)
                                                       };
                                                   }).OrderBy(venta => venta.ValorTotalVenta).Take(10);
                return ventasMensuales;
            }
            
        }
        public async Task<IEnumerable<VentaCantidad>> GetThreeProductsQuarterForAmount(int quarter, bool state)
        {
            var trimestres = new List<int[]>()
            {
                new int[] {1,2,3,4},
                new int[] {5,6,7,8},
                new int[] {9,10,11,12}
            };

            var ventas = await _repository.GetVentas();

            if (state)
            {
                var ventasTrimestrales = ventas.Where(venta => venta.Fecha_compra.Month >= trimestres[quarter - 1][0]
                                                       && venta.Fecha_compra.Month <= trimestres[quarter - 1][^1])
                                          .GroupBy(venta => venta.Producto)
                                          .Select(venta =>
                                          {
                                              return new VentaCantidad()
                                              {
                                                  Producto = venta.Key,
                                                  Categoria = venta.First().Categoria,
                                                  CantidadTotalVendida = venta.Sum(el => el.Cantidad)
                                              };
                                          }).OrderByDescending(venta => venta.CantidadTotalVendida).Take(10);
                return ventasTrimestrales;
            }
            else
            {
                var ventasTrimestrales = ventas.Where(venta => venta.Fecha_compra.Month >= trimestres[quarter - 1][0]
                                                        && venta.Fecha_compra.Month <= trimestres[quarter - 1][^1])
                                           .GroupBy(venta => venta.Producto)
                                           .Select(venta =>
                                           {
                                               return new VentaCantidad()
                                               {
                                                   Producto = venta.Key,
                                                   Categoria = venta.First().Categoria,
                                                   CantidadTotalVendida = venta.Sum(el => el.Cantidad)
                                               };
                                           }).OrderBy(venta => venta.CantidadTotalVendida).Take(10);
                return ventasTrimestrales;
            }

        }
        public async Task<IEnumerable<VentaMonto>> GetThreeProductsQuarterForValue(int quarter, bool state)
        {
            var trimestres = new List<int[]>()
            {
                new int[] {1,2,3,4},
                new int[] {5,6,7,8},
                new int[] {9,10,11,12}
            };
            var ventasTotales = await _repository.GetVentas();
            if (state)
            {
                var ventasTrimestrales = ventasTotales.Where(venta => venta.Fecha_compra.Month >= trimestres[quarter - 1][0]
                                                        && venta.Fecha_compra.Month <= trimestres[quarter - 1][^1])
                                           .GroupBy(venta => venta.Producto)
                                           .Select(venta =>
                                           {
                                               return new VentaMonto()
                                               {
                                                   Producto = venta.Key,
                                                   Categoria = venta.First().Categoria,
                                                   CantidadTotalVendida = venta.Sum(el => el.Cantidad),
                                                   ValorTotalVenta = venta.Sum(el => el.Total_venta)
                                               };
                                           }).OrderByDescending(venta => venta.ValorTotalVenta).Take(10);
                return ventasTrimestrales;
            }
            else
            {
                var ventasTrimestrales = ventasTotales.Where(venta => venta.Fecha_compra.Month >= trimestres[quarter - 1][0]
                                                        && venta.Fecha_compra.Month <= trimestres[quarter - 1][^1])
                                           .GroupBy(venta => venta.Producto)
                                           .Select(venta =>
                                           {
                                               return new VentaMonto()
                                               {
                                                   Producto = venta.Key,
                                                   Categoria = venta.First().Categoria,
                                                   CantidadTotalVendida = venta.Sum(el => el.Cantidad),
                                                   ValorTotalVenta = venta.Sum(el => el.Total_venta)
                                               };
                                           }).OrderBy(venta => venta.ValorTotalVenta).Take(10);
                return ventasTrimestrales;
            }

        }
        public async Task<IEnumerable<VentaCantidad>> GetThreeProducstGlobalForAmount(bool state)
        {
            var ventas = await _repository.GetVentas();
            if (state)
            {
                var global = ventas.GroupBy(venta => venta.Producto)
                               .Select(venta =>
                               {
                                   return new VentaCantidad()
                                   {
                                       Producto = venta.Key,
                                       Categoria = venta.First().Categoria,
                                       CantidadTotalVendida = venta.Sum(el => el.Cantidad)
                                   };
                               }).OrderByDescending(venta => venta.CantidadTotalVendida).Take(10);
                return global;
            }
            else
            {
                var global = ventas.GroupBy(venta => venta.Producto)
                               .Select(venta =>
                               {
                                   return new VentaCantidad()
                                   {
                                       Producto = venta.Key,
                                       Categoria = venta.First().Categoria,
                                       CantidadTotalVendida = venta.Sum(el => el.Cantidad)
                                   };
                               }).OrderBy(venta => venta.CantidadTotalVendida).Take(10);
                return global;
            }
        }
        public async Task<IEnumerable<VentaMonto>> GetThreeProducstGlobalForValue(bool state)
        {
            var ventas = await _repository.GetVentas();
            if (state)
            {
                var global = ventas.GroupBy(venta => venta.Producto)
                               .Select(venta =>
                               {
                                   return new VentaMonto()
                                   {
                                       Producto = venta.Key,
                                       Categoria = venta.First().Categoria,
                                       CantidadTotalVendida = venta.Sum(el => el.Cantidad),
                                       ValorTotalVenta = venta.Sum(el => el.Total_venta)
                                   };
                               }).OrderByDescending(venta => venta.ValorTotalVenta).Take(10);
                return global;
            }
            else
            {
                var global = ventas.GroupBy(venta => venta.Producto)
                               .Select(venta =>
                               {
                                   return new VentaMonto()
                                   {
                                       Producto = venta.Key,
                                       Categoria = venta.First().Categoria,
                                       CantidadTotalVendida = venta.Sum(el => el.Cantidad),
                                       ValorTotalVenta = venta.Sum(el => el.Total_venta)
                                   };
                               }).OrderBy(venta => venta.ValorTotalVenta).Take(10);
                return global;
            }
        }
        public async Task PostVenta(Venta venta)
        {
            var ingresoVenta = new Venta()
            {
                Id = null,
                Producto = venta.Producto,
                Categoria = venta.Categoria,
                Cliente = venta.Cliente,
                Precio_unitario = venta.Precio_unitario,
                Cantidad = venta.Cantidad,
                Total_venta = venta.Total_venta,
                Fecha_compra = venta.Fecha_compra,
            };
            await _repository.PostVenta(ingresoVenta);
        }

        public async Task PostVentas(IEnumerable<Venta> ventas)
        {
            List<Venta> ingresoVentas = new List<Venta>();
            foreach (var venta in ventas)
            {
                ingresoVentas.Add(new Venta()
                {
                    Id = null,
                    Producto = venta.Producto,
                    Categoria = venta.Categoria,
                    Cliente = venta.Cliente,
                    Precio_unitario = venta.Precio_unitario,
                    Cantidad = venta.Cantidad,
                    Total_venta = venta.Total_venta,
                    Fecha_compra = venta.Fecha_compra,
                });
            }
            await _repository.PostVentas(ingresoVentas);
        }
    }
}
