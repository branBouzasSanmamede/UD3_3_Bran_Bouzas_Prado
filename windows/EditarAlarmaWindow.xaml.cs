using System;
using System.Windows;
using UD3_3_Bran_Bouzas_Prado.models;

namespace UD3_3_Bran_Bouzas_Prado.windows
{
    public partial class EditarAlarmaWindow : Window
    {
        private readonly Alarma alarma;
        private readonly List<Alarma> alarmas;

        public EditarAlarmaWindow(Alarma alarma, List<Alarma> alarmas)
        {
            InitializeComponent();
            this.alarma = alarma;
            this.alarmas = alarmas;

            txtHora.Text = alarma.Hora.ToString("D2");
            txtMinuto.Text = alarma.Minuto.ToString("D2");
            txtMensaje.Text = alarma.Mensaje;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtHora.Text, out int h) || h < 0 || h > 23)
            {
                MessageBox.Show("La hora debe estar entre 0 y 23.");
                return;
            }

            if (!int.TryParse(txtMinuto.Text, out int m) || m < 0 || m > 59)
            {
                MessageBox.Show("El minuto debe estar entre 0 y 59.");
                return;
            }

            if (alarmas.Any(a => a != alarma && a.Hora == h && a.Minuto == m))
            {
                MessageBox.Show("Ya existe una alarma para esa hora y minuto.");
                return;
            }

            string mensaje = string.IsNullOrWhiteSpace(txtMensaje.Text) ? "¡ALARMA!" : txtMensaje.Text;

            alarma.Hora = h;
            alarma.Minuto = m;
            alarma.Mensaje = mensaje;

            DialogResult = true;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }
}