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

        public void Guardar (LiquidacionCuotaModerada liquidacionCuotaModerada)
        {
            liquidacionCuotaModeradaRepository.Guardar(liquidacionCuotaModerada);
        }

        public List <LiquidacionCuotaModerada> Consultar()
        {
            return liquidacionCuotaModeradaRepository.Consultar();
        }

        public void Eliminar(string numeroLiquidacion)
        {
            liquidacionCuotaModeradaRepository.Eliminar(numeroLiquidacion);
        }

        public void Modificar(LiquidacionCuotaModerada liquidacionCuotaModerada)
        {
            liquidacionCuotaModeradaRepository.Modificar(liquidacionCuotaModerada);
        }
        public LiquidacionCuotaModerada ConsultarIndividual(string numeroLiquidacion)
        {
            return liquidacionCuotaModeradaRepository.ConsultarIndividual(numeroLiquidacion);
        }
    }
}
