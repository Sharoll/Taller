using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using ENTITY;

namespace UIIPS
{
    public partial class FormConsulta : Form
    {
        public FormConsulta()
        {
            InitializeComponent();
        }

        LiquidacionCuotaModeradaService liquidacionCuotaModeradaService = new LiquidacionCuotaModeradaService();
        

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            TablaConsulta.DataSource = null;


            if (comboBoxTipo2.Text == "CONTRIBUTIVO")
            {
                TablaConsulta.DataSource = liquidacionCuotaModeradaService.ConsultarContributivo();
                textLiquidaciones.Text = Convert.ToString(liquidacionCuotaModeradaService.TotalizarLiquidacionesContributivas());
                textValorTotal.Text = Convert.ToString(liquidacionCuotaModeradaService.ValorTotalRegimenContributivo());
                
            }
            else
            {
                if (comboBoxTipo2.Text == "SUBSIDIADO" ){
                    TablaConsulta.DataSource = liquidacionCuotaModeradaService.ConsultarSubsidiado();
                    textLiquidaciones.Text = Convert.ToString(liquidacionCuotaModeradaService.TotalizarLiquidacionesSubsidiadas());
                    textValorTotal.Text = Convert.ToString(liquidacionCuotaModeradaService.ValorTotalRegimenSubsidiado());

                }
                else
                {
                    if(comboBoxTipo2.Text == "TODOS")
                    {
                        TablaConsulta.DataSource = liquidacionCuotaModeradaService.Consultar();
                        textLiquidaciones.Text = Convert.ToString(liquidacionCuotaModeradaService.TotalizarLiquidaciones());
                        textValorTotal.Text = Convert.ToString(liquidacionCuotaModeradaService.ValorTotalCuotasModeradas());
                    }
                }

            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FiltroTotalLiquidaciones_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConsultarFecha_Click(object sender, EventArgs e)
        {
            DateTime FechaInicio = monthCalendar1.SelectionStart;
            DateTime FechaFinal = monthCalendar1.SelectionEnd;

            labelInicio.Text = FechaInicio.ToString();
            labelFinal.Text = FechaFinal.ToString();

            TablaConsulta.DataSource = liquidacionCuotaModeradaService.ConsultarFecha(FechaInicio, FechaFinal);
            textLiquidaciones.Text = Convert.ToString (liquidacionCuotaModeradaService.TotalCantidadFecha(FechaInicio, FechaFinal));
            textLiquidaciones.Text = Convert.ToString(liquidacionCuotaModeradaService.TotalValorFecha(FechaInicio, FechaFinal));
            textValorTotal.Text = Convert.ToString(liquidacionCuotaModeradaService.TotalCantidadFecha(FechaInicio, FechaFinal));
            textValorTotal.Text = Convert.ToString(liquidacionCuotaModeradaService.TotalValorFecha(FechaInicio, FechaFinal));


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TablaConsulta.DataSource = liquidacionCuotaModeradaService.BuscarNombre(textBox1.Text);
        }
    }
}
