using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class RegimenSubsidiado : LiquidacionCuotaModerada
    {
        public override decimal CalcularTarifa()
        {
            Tarifa=5;
            return Tarifa;
        }

        public override decimal CalcularTope()
        {
           Tope=200000;
           return Tope;
        }
    }
}
