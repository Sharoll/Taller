using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.IO;

namespace DAL
{
    public class LiquidacionCuotaModeradaRepository
    {
        string ruta = @"LiquidacionCuotaModerada.txt";
        List<LiquidacionCuotaModerada> liquidacionCuotaModeradas;

        public void Guardar(LiquidacionCuotaModerada liquidacionCuotaModerada)
        {
            FileStream file = new FileStream(ruta, FileMode.Append);
            StreamWriter escritor = new StreamWriter(file);
            escritor.WriteLine($"{liquidacionCuotaModerada.NumeroLiquidacion}+{liquidacionCuotaModerada.IdentificacionPaciente}+" +
                $"{liquidacionCuotaModerada.TipoAfiliacion}+{liquidacionCuotaModerada.SalarioDevengado}+{liquidacionCuotaModerada.ValorServicio}+" +
                $"{liquidacionCuotaModerada.Tope}+{liquidacionCuotaModerada.CuotaModerada}+{liquidacionCuotaModerada.Tarifa}");
            escritor.Close();
            file.Close();
        }

        public LiquidacionCuotaModerada Mapear(string linea)
        {
            LiquidacionCuotaModerada liquidacionCuotaModerada = new RegimenContributivo();
            char delimiter = '+';
            string[] Datos = linea.Split(delimiter);
            liquidacionCuotaModerada.NumeroLiquidacion = Datos[0];
            liquidacionCuotaModerada.IdentificacionPaciente = Datos[1];
            liquidacionCuotaModerada.TipoAfiliacion = Datos[2];
            liquidacionCuotaModerada.SalarioDevengado = Convert.ToDecimal(Datos[3]);
            liquidacionCuotaModerada.ValorServicio = Convert.ToDecimal(Datos[4]);
            liquidacionCuotaModerada.Tope = Convert.ToDecimal(Datos[5]);
            liquidacionCuotaModerada.CuotaModerada = Convert.ToDecimal(Datos[6]);
            liquidacionCuotaModerada.Tarifa = Convert.ToDecimal(Datos[7]);
            return liquidacionCuotaModerada;
        }
        public List<LiquidacionCuotaModerada> Consultar()
        {
            liquidacionCuotaModeradas = new List<LiquidacionCuotaModerada>();

            string Linea = string.Empty;
            FileStream file = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader escritor = new StreamReader(file);
            while ((Linea = escritor.ReadLine()) != null)
            {
                LiquidacionCuotaModerada liquidacionCuotaModerada = Mapear(Linea);
                liquidacionCuotaModeradas.Add(liquidacionCuotaModerada);
            }
            escritor.Close();

            return liquidacionCuotaModeradas;
        }

        public void Eliminar(string numeroLiquidacion)
        {
            liquidacionCuotaModeradas.Clear();
            liquidacionCuotaModeradas = Consultar();
            FileStream Source = new FileStream(ruta, FileMode.Create);
            Source.Close();

            foreach (var item in liquidacionCuotaModeradas)
            {
                if (item.NumeroLiquidacion != numeroLiquidacion)
                {
                    Guardar(item);
                }
            }

        }

        public void Modificar(LiquidacionCuotaModerada liquidacionCuotaModerada)
        {
            liquidacionCuotaModeradas.Clear();
            liquidacionCuotaModeradas = Consultar();
            FileStream fileStream = new FileStream(ruta, FileMode.Create);
            fileStream.Close();

            foreach (var item in liquidacionCuotaModeradas)
            {
                if (item.NumeroLiquidacion != liquidacionCuotaModerada.NumeroLiquidacion)
                {
                    Guardar(item);
                }
                else
                {
                    liquidacionCuotaModerada.CalcularTope();
                    liquidacionCuotaModerada.CalcularTarifa();
                    liquidacionCuotaModerada.CalcularCuotaModerada();

                    Guardar(liquidacionCuotaModerada);
                }
            }
        }
        public LiquidacionCuotaModerada ConsultarIndividual(string numeroLiquidacion)
        {
            liquidacionCuotaModeradas.Clear();
            liquidacionCuotaModeradas = Consultar();
            foreach(var item in liquidacionCuotaModeradas)
            {
                if(item.NumeroLiquidacion == numeroLiquidacion)
                {
                    return item;
                }
            }
            return null;
        }

    }

    //aca falta lo  de recalcular el valor, estoy pensando como hacerlo... Es llamar ptravez a los metodos /
    //Eso estaba pensando, recuerdo que una vez hicimos algo asi,es como si quisieras guardar de nuevo solamente que estavez
    //llamas a los metodos de nue

}