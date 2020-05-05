using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTITY;

namespace BLL
{
    public class LiquidacionCuotaModeradaService
    {
        LiquidacionCuotaModeradaRepository liquidacionCuotaModeradaRepository;

        public LiquidacionCuotaModeradaService()
        {
            liquidacionCuotaModeradaRepository = new LiquidacionCuotaModeradaRepository();
        }

        public string  Guardar (LiquidacionCuotaModerada liquidacionCuotaModerada)
        {
            try
            {
                liquidacionCuotaModeradaRepository.Guardar(liquidacionCuotaModerada);
                return "Se guardaron los datos satisfactoriamente. ";
            }
            catch (Exception e)
            {
                return $"Error de la Aplicación: {e.Message}";
            }
           
        }

        public List <LiquidacionCuotaModerada> Consultar()
        {
            return liquidacionCuotaModeradaRepository.Consultar();
        }

        public string  Eliminar(string numeroLiquidacion)
        {
            try
            {
                liquidacionCuotaModeradaRepository.Eliminar(numeroLiquidacion);
                return "Se eliminaron los datos satisfactoriamente. ";

            }catch (Exception e)

            {
                return $"Error de la Aplicación: {e.Message}";
            }
            
        }

        public string  Modificar(LiquidacionCuotaModerada liquidacionCuotaModerada)
        {
            try
            {
                liquidacionCuotaModeradaRepository.Modificar(liquidacionCuotaModerada);
                return "Se modificaron los datos satisfactoriamente. ";

            }
            catch (Exception e)

            {
                return $"Error de la Aplicación: {e.Message}";
            }
            
        }

        public LiquidacionCuotaModerada ConsultarIndividual(string numeroLiquidacion)
        {
            return liquidacionCuotaModeradaRepository.ConsultarIndividual(numeroLiquidacion);
        }

        public List<LiquidacionCuotaModerada> ConsultarContributivo()
        {
            return liquidacionCuotaModeradaRepository.ConsultarContributivo();
        }

        public List<LiquidacionCuotaModerada> ConsultarSubsidiado()
        {
            return liquidacionCuotaModeradaRepository.ConsultarSubsidiado();
        }

        public int TotalizarLiquidaciones()
        {
            return liquidacionCuotaModeradaRepository.TotalizarLiquidaciones();
        }

        public int TotalizarLiquidacionesContributivas()
        {
            return liquidacionCuotaModeradaRepository.TotalizarLiquidacionesContributivas();
        }

        public int TotalizarLiquidacionesSubsidiadas()
        {
            return liquidacionCuotaModeradaRepository.TotalizarLiquidacionesSubsidiadas();
        }

        public decimal ValorTotalCuotasModeradas()
        {
            return liquidacionCuotaModeradaRepository.ValorTotalCuotasModeradas();
        }

        public decimal ValorTotalRegimenSubsidiado()
        {
            return liquidacionCuotaModeradaRepository.ValorTotalRegimenSubsidiado();
        }

        public decimal ValorTotalRegimenContributivo()
        {
            return liquidacionCuotaModeradaRepository.ValorTotalRegimenContributivo();
        }

        public IList<LiquidacionCuotaModerada> ConsultarFecha(DateTime FechaInicial, DateTime FechaFinal)
        {
            return liquidacionCuotaModeradaRepository.ConsultarFecha(FechaInicial, FechaFinal);
        }

        public int TotalCantidadFecha(DateTime FechaInicial, DateTime FechaFinal)
        {
            return liquidacionCuotaModeradaRepository.TotalCantidadFecha(FechaInicial,FechaFinal);
        }

        public decimal TotalValorFecha(DateTime FechaInicial, DateTime FechaFinal)
        {
            return liquidacionCuotaModeradaRepository.TotalValorFecha(FechaInicial, FechaFinal);
        }

        public IList<LiquidacionCuotaModerada> BuscarNombre (string Letras)
        {
            return liquidacionCuotaModeradaRepository.BuscarNombre(Letras);
        }
    }
}
