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
    public partial class FormLiquidaciones : Form
    {
        public FormLiquidaciones()
        {
            InitializeComponent();
        }
        LiquidacionCuotaModeradaService liquidacionCuotaModeradaService = new LiquidacionCuotaModeradaService();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            LiquidacionCuotaModerada liquidacionCuotaModerada = Registrar();
            string MensajeLiquidacionCuota = liquidacionCuotaModeradaService.Guardar(liquidacionCuotaModerada);
            MessageBox.Show(MensajeLiquidacionCuota, "Mensaje Guardado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            liquidacionCuotaModerada.CalcularTarifa();
            liquidacionCuotaModerada.CalcularTope();
            liquidacionCuotaModerada.CalcularCuotaModerada();

            Limpiar();
        }

        private LiquidacionCuotaModerada Registrar()
        {
            LiquidacionCuotaModerada liquidacionCuotaModerada;

            if (comboBoxTipo.Text == "CONTRIBUTIVO")
            {

                liquidacionCuotaModerada = new RegimenContributivo();
                liquidacionCuotaModerada.SalarioDevengado = Convert.ToDecimal (textSalario.Text);
            }
            else
            {
                liquidacionCuotaModerada = new RegimenSubsidiado();
                liquidacionCuotaModerada.SalarioDevengado = 0;
            }

            liquidacionCuotaModerada.IdentificacionPaciente = textIdPaciente.Text;
            liquidacionCuotaModerada.NumeroLiquidacion = textNumeroLiquidacion.Text;
            liquidacionCuotaModerada.Nombre = textNombre.Text;
            liquidacionCuotaModerada.Fecha = DateTime.Now;
            liquidacionCuotaModerada.TipoAfiliacion = comboBoxTipo.SelectedItem.ToString();
            liquidacionCuotaModerada.ValorServicio = Convert.ToDecimal(textValorServicio.Text);

            liquidacionCuotaModerada.CalcularTarifa();
            liquidacionCuotaModerada.CalcularTope();
            liquidacionCuotaModerada.CalcularCuotaModerada();

            Limpiar();

            return liquidacionCuotaModerada;

        } 

        public void Modificar()
        {
            LiquidacionCuotaModerada liquidacionNueva;
            LiquidacionCuotaModerada liquidacion;
            liquidacion = liquidacionCuotaModeradaService.ConsultarIndividual(textNumeroLiquidacion.Text);

            if (comboBoxTipo.Text == "CONTRIBUTIVO")
            {
                liquidacionNueva = new RegimenContributivo();
                liquidacion.SalarioDevengado = Convert.ToDecimal(textSalario.Text);
            }
            else
            {
                liquidacionNueva = new RegimenSubsidiado();
                liquidacion.SalarioDevengado = 0;
            }

            liquidacionNueva.NumeroLiquidacion = liquidacion.NumeroLiquidacion;
            liquidacionNueva.IdentificacionPaciente = liquidacion.IdentificacionPaciente;
            liquidacionNueva.Nombre = liquidacion.Nombre;
            liquidacionNueva.TipoAfiliacion = comboBoxTipo.Text;
            liquidacionNueva.ValorServicio = liquidacion.ValorServicio;
            liquidacionNueva.Fecha = liquidacion.Fecha;

            liquidacionNueva.CalcularTarifa();
            liquidacionNueva.CalcularTope();
            liquidacionNueva.CalcularCuotaModerada();

            string MensajeLiquidacionCuota = liquidacionCuotaModeradaService.Modificar(liquidacionNueva);
            MessageBox.Show(MensajeLiquidacionCuota, "Mensaje Modificado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            Limpiar();

        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxTipo.Text == "CONTRIBUTIVO") {

                textSalario.Enabled = true;
            }
            else
            {
                textSalario.Enabled = false;
            }
        
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LiquidacionCuotaModerada liquidacionCuotaModerada;
            string NumeroLiquidacion;

            NumeroLiquidacion = textNumeroLiquidacion.Text;
            liquidacionCuotaModerada = liquidacionCuotaModeradaService.ConsultarIndividual(NumeroLiquidacion);

            if (liquidacionCuotaModerada == null)
            {
                MessageBox.Show("No existe");
            }
            else
            {
                textIdPaciente.Text = liquidacionCuotaModerada.IdentificacionPaciente;
                textNombre.Text = liquidacionCuotaModerada.Nombre;
                comboBoxTipo.Text = liquidacionCuotaModerada.TipoAfiliacion;
                textSalario.Text = Convert.ToString(liquidacionCuotaModerada.SalarioDevengado);
                textValorServicio.Text = Convert.ToString(liquidacionCuotaModerada.ValorServicio);

            }

        }

       private void Limpiar()
        {
            textNumeroLiquidacion.Text = ("");
            textIdPaciente.Text = ("");
            textNombre.Text = ("");
            comboBoxTipo.Text = ("");
            textSalario.Text = ("");
            textValorServicio.Text = ("");
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string NumeroLiquidacion = textNumeroLiquidacion.Text.Trim();

            string mensaje = liquidacionCuotaModeradaService.Eliminar(NumeroLiquidacion);
            MessageBox.Show(mensaje, "MensajeEliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Limpiar();
        }
    }
}
