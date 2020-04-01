using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class RegimenContributivo : LiquidacionCuotaModerada
    { 
        public override decimal CalcularTope()
        {
            if (SalarioDevengado > 2)
            {
                Tope = 250000;
            }
            else
            {
                if(SalarioDevengado >=2 && SalarioDevengado <= 5){
                    Tope = 900000;


                }else
                {
                    if (SalarioDevengado > 5)
                    {
                        Tope = 15000000;
                    }
                }
            }

            return Tope;

        }

        public override decimal CalcularTarifa()
        {
            if (SalarioDevengado < 2)
            {
                Tarifa = 15;
            }
            else
            {
                if (SalarioDevengado >= 2 && SalarioDevengado <= 5)
                {
                    Tarifa=20;
                }
                else
                {
                    if (SalarioDevengado > 5)
                    {
                        Tarifa = 25;
                    }
                }
            }
            return Tarifa;
        }
           
        
    }
}
