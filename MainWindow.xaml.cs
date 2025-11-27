using events;
using System.ComponentModel;
using System.Windows;
using UD3_3_Bran_Bouzas_Prado.utils;
using UD3_3_Bran_Bouzas_Prado.windows;

namespace UD3_3_Bran_Bouzas_Prado
{
    public partial class MainWindow : Window
    {
        private AlarmasWindow? alarmasWindow;
        public MainWindow()
        {
            InitializeComponent();
            WindowHelper.CentradoDerecha(this);
            MiReloj.AlarmaActivadaEvento += OnAlarma;
        }

        private void ButtonCambiarFormato_Click(object sender, RoutedEventArgs e)
        {
            MiReloj.Formato12Horas = !MiReloj.Formato12Horas;
            MiReloj.ActualizarHora();
        }

        private void ButtonProgramarAlarma_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtHoraAlarma.Text, out int h) || h < 0 || h > 23)
            {
                MessageBox.Show("La hora debe estar entre 0 y 23.");
                return;
            }

            if (!int.TryParse(txtMinutoAlarma.Text, out int m) || m < 0 || m > 59)
            {
                MessageBox.Show("El minuto debe estar entre 0 y 59.");
                return;
            }

            string mensaje = string.IsNullOrWhiteSpace(txtMensajeAlarma.Text) ? MiReloj.MensajeAlarma : txtMensajeAlarma.Text;

            bool exito = MiReloj.AgregarAlarma(h, m, mensaje);
            if (!exito)
            {
                MessageBox.Show("Ya hay una alarma para esa hora y minuto.");
                return;
            }

            if (alarmasWindow == null)
            {
                alarmasWindow = new AlarmasWindow(MiReloj.GetAlarmas()) { Owner = this };
                WindowHelper.CentradoIzquierda(alarmasWindow);
                alarmasWindow.Show();
            }
            else
            {
                alarmasWindow.ActualizarLista(); 
            }
        }

        private void OnAlarma(object? sender, AlarmaEventArgs e)
        {
            MessageBox.Show(e.Mensaje);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            MiReloj.DetenerTimer();

            if (alarmasWindow != null)
            {
                alarmasWindow.Close();
            }

            Application.Current.Shutdown();
        }
    }
}