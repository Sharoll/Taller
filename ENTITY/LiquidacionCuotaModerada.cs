using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public abstract class LiquidacionCuotaModerada
    {
        public string NumeroLiquidacion { get; set; }
        public string IdentificacionPaciente { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoAfiliacion { get; set; }
        public decimal SalarioDevengado { get; set; }
        public decimal ValorServicio { get; set; }
        public decimal Tope { get; set; }
        public decimal CuotaModerada { get; set; }
        public decimal Tarifa { get; set; }

        public decimal CalcularCuotaModerada()
        {
            CuotaModerada = ValorServicio * (Tarifa/100) + ValorServicio;
            CuotaModerada = Validar();
            return CuotaModerada;
        }

        public decimal Validar()
        {
            if (CuotaModerada > Tope)
            {
                CuotaModerada = Tope;
            }
            return CuotaModerada;
        }

        public abstract decimal CalcularTope();
        public abstract decimal CalcularTarifa();
    }
}
