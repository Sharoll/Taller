using ENTITY;
using System;
using BLL;
using System.Collections.Generic;

namespace IPS
{
    class Program
    {
        public static LiquidacionCuotaModeradaService liquidacionCuotaModeradaService = new LiquidacionCuotaModeradaService();
        public static List<LiquidacionCuotaModerada> liquidacionCuotaModeradas;
        static void Main(string[] args)
        {
            int Opcion = 0;
            do
            {
                Menu();
                Opcion = Convert.ToInt32(Console.ReadLine());
                switch (Opcion)
                {
                    case 1:
                        LiquidacionCuotaModerada liquidacionCuota;
                        string Numero, Identificacion, Tipo;
                        decimal SalarioDevengado, ValorServicio;

                        Console.WriteLine("Digite numero deLiquidacion");
                        Numero = Console.ReadLine();
                        Console.WriteLine("Digite numero de Identificacion");
                        Identificacion = Console.ReadLine();
                        Console.WriteLine("Digite tipo de afiliacion CONTRIBUTIVO/SUBSIDIADO");
                        Tipo = Console.ReadLine();
                        Console.WriteLine("Digite Valor del Servicio");
                        ValorServicio = Convert.ToDecimal(Console.ReadLine());
                        if(Tipo == "CONTRIBUTIVO")
                        {

                            Console.WriteLine("Digite Salario Devengado");
                            SalarioDevengado = Convert.ToDecimal(Console.ReadLine());

                            liquidacionCuota = new RegimenContributivo();
                            
                            liquidacionCuota.SalarioDevengado = SalarioDevengado;
                            
                        }
                        else
                        {
                            liquidacionCuota = new RegimenSubsidiado();
                            liquidacionCuota.SalarioDevengado = 0;
                        }
                        liquidacionCuota.NumeroLiquidacion = Numero;
                        liquidacionCuota.IdentificacionPaciente = Identificacion;
                        liquidacionCuota.TipoAfiliacion = Tipo;
                        liquidacionCuota.ValorServicio = ValorServicio;
                        liquidacionCuota.CalcularTarifa();
                        liquidacionCuota.CalcularTope();
                        liquidacionCuota.CalcularCuotaModerada();
                        liquidacionCuotaModeradaService.Guardar(liquidacionCuota);

                        break;
                    case 2:
                        liquidacionCuotaModeradas = liquidacionCuotaModeradaService.Consultar();
                        foreach(LiquidacionCuotaModerada liquidacion in liquidacionCuotaModeradas){
                            Console.WriteLine($"Numero : {liquidacion.NumeroLiquidacion}");
                            Console.WriteLine($"Identificacion: {liquidacion.IdentificacionPaciente}");
                            Console.WriteLine($"Tipo De Afiliacion: {liquidacion.TipoAfiliacion}");
                            Console.WriteLine($"Salario Devengado: {liquidacion.SalarioDevengado}");
                            Console.WriteLine($"Valor Del Servicio: {liquidacion.ValorServicio}");
                            Console.WriteLine($"Tope: {liquidacion.Tope}");
                            Console.WriteLine($"Cuota Moderada: {liquidacion.CuotaModerada}");
                            Console.WriteLine($"Tarifa: {liquidacion.Tarifa}");
                            Console.WriteLine($"_________________________________________________________________");
                        }
                        break;
                    case 3:
                        Console.WriteLine($"Digite Numero de Liquidacion a Eliminar :");
                        string numeroLiquidacion = Console.ReadLine();
                        liquidacionCuotaModeradaService.Eliminar(numeroLiquidacion);
                        break;
                    case 4:
                        Console.WriteLine($"Digite Numero de Liquidacion a Modificar :");
                        numeroLiquidacion = Console.ReadLine();
                        LiquidacionCuotaModerada liquidacionCuotaModerada = liquidacionCuotaModeradaService.ConsultarIndividual(numeroLiquidacion);

                        if(liquidacionCuotaModerada == null)
                        {
                            Console.WriteLine($"No se encontro ");
                        }
                        else
                        {
                            Console.WriteLine($"Digite Numero valor de Servicio  :");
                            liquidacionCuotaModerada.ValorServicio = Convert.ToDecimal(Console.ReadLine());

                            liquidacionCuotaModeradaService.Modificar(liquidacionCuotaModerada);
                        }
                        break;
                    case 5:
                        break;
                }

            } while (Opcion != 5);

        }

        public static void Menu()
        {
            Console.WriteLine("***MENU**");
            Console.WriteLine("1. REgistrar y Realizar Liqidacion");
            Console.WriteLine("2. Consultar");
            Console.WriteLine("3. Eliminar");
            Console.WriteLine("4. Modificar");
            Console.WriteLine("5. Salir");
            Console.WriteLine("Digite Opcion: ");
        }
    }
}

