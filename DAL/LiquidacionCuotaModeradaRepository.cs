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
                $"{liquidacionCuotaModerada.Fecha}+{liquidacionCuotaModerada.TipoAfiliacion}+{liquidacionCuotaModerada.SalarioDevengado}+" +
                $"{liquidacionCuotaModerada.ValorServicio}+{liquidacionCuotaModerada.Tope}+{liquidacionCuotaModerada.CuotaModerada}+" +
                $"{liquidacionCuotaModerada.Tarifa}+{liquidacionCuotaModerada.Nombre}");
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
            liquidacionCuotaModerada.Fecha = Convert.ToDateTime(Datos[2]);
            liquidacionCuotaModerada.TipoAfiliacion = Datos[3];
            liquidacionCuotaModerada.SalarioDevengado = Convert.ToDecimal(Datos[4]);
            liquidacionCuotaModerada.ValorServicio = Convert.ToDecimal(Datos[5]);
            liquidacionCuotaModerada.Tope = Convert.ToDecimal(Datos[6]);
            liquidacionCuotaModerada.CuotaModerada = Convert.ToDecimal(Datos[7]);
            liquidacionCuotaModerada.Tarifa = Convert.ToDecimal(Datos[8]);
            liquidacionCuotaModerada.Nombre = Datos[9];

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
                    Guardar(liquidacionCuotaModerada);
                }
            }
        }
        public LiquidacionCuotaModerada ConsultarIndividual(string numeroLiquidacion)
        {

            liquidacionCuotaModeradas = Consultar();
            foreach (var item in liquidacionCuotaModeradas)
            {
                if (item.NumeroLiquidacion == numeroLiquidacion)
                {
                    return item;
                }
            }
            return null;
        }

        public List<LiquidacionCuotaModerada> ConsultarContributivo()
        {

            liquidacionCuotaModeradas = Consultar();
            return liquidacionCuotaModeradas.Where(L => L.TipoAfiliacion.Equals("CONTRIBUTIVO")).ToList();
        }

        public List<LiquidacionCuotaModerada> ConsultarSubsidiado()
        {

            liquidacionCuotaModeradas = Consultar();
            return liquidacionCuotaModeradas.Where(L => L.TipoAfiliacion.Equals("SUBSIDIADO")).ToList();
        }

        public int TotalizarLiquidaciones()
        {

            return liquidacionCuotaModeradas.Count();
        }

        public int TotalizarLiquidacionesContributivas()
        {
            return liquidacionCuotaModeradas.Count(Tlc => Tlc.TipoAfiliacion.Equals("CONTRIBUTIVO"));

        }

        public int TotalizarLiquidacionesSubsidiadas()
        {
            return liquidacionCuotaModeradas.Count(Tls => Tls.TipoAfiliacion.Equals("SUBSIDIADO"));

        }

        public decimal ValorTotalCuotasModeradas()
        {
            return liquidacionCuotaModeradas.Sum(Cm => Cm.CuotaModerada);
        }

        public decimal ValorTotalRegimenSubsidiado()
        {
            return liquidacionCuotaModeradas.Where(Rs => Rs.TipoAfiliacion.Equals("SUBSIDIADO")).Sum(Rs => Rs.CuotaModerada);
        }

        public decimal ValorTotalRegimenContributivo()
        {
            return liquidacionCuotaModeradas.Where(Rc => Rc.TipoAfiliacion.Equals("CONTRIBUTIVO")).Sum(Rc => Rc.CuotaModerada);
        }

        public IList<LiquidacionCuotaModerada>ConsultarFecha(DateTime FechaInicial, DateTime FechaFinal)
        {
            liquidacionCuotaModeradas = Consultar();

            return liquidacionCuotaModeradas.Where(Lcm => (Lcm.Fecha.Month >= FechaInicial.Month && Lcm.Fecha.Month <= FechaFinal.Month) && 
            (Lcm.Fecha.Year >= FechaInicial.Year && Lcm.Fecha.Year <= FechaFinal.Year)).ToList();
            

        }

        public int  TotalCantidadFecha(DateTime FechaInicial, DateTime FechaFinal)
        {
            return liquidacionCuotaModeradas.Where(Lcm => (Lcm.Fecha.Month >= FechaInicial.Month && Lcm.Fecha.Month <= FechaFinal.Month) &&
            (Lcm.Fecha.Year >= FechaInicial.Year && Lcm.Fecha.Year <= FechaFinal.Year)).Count();
        }

        public decimal TotalValorFecha(DateTime FechaInicial, DateTime FechaFinal){

            return liquidacionCuotaModeradas.Where(Lcm => (Lcm.Fecha.Month >= FechaInicial.Month && Lcm.Fecha.Month <= FechaFinal.Month) &&
            (Lcm.Fecha.Year >= FechaInicial.Year && Lcm.Fecha.Year <= FechaFinal.Year)).Sum(Tv => Tv.CuotaModerada);
        }

        public IList<LiquidacionCuotaModerada> BuscarNombre(string Letras)
        {
            liquidacionCuotaModeradas = Consultar();
            return liquidacionCuotaModeradas.Where(Bn => Bn.Nombre.Contains(Letras)).ToList();
        }

    }
}