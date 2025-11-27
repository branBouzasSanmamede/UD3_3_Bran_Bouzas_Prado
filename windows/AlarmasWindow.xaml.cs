using components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using UD3_3_Bran_Bouzas_Prado.models;

namespace UD3_3_Bran_Bouzas_Prado.windows
{
    public partial class AlarmasWindow : Window
    {
        private readonly List<Alarma> alarmas;

        public AlarmasWindow(List<Alarma> alarmas)
        {
            InitializeComponent();
            this.alarmas = alarmas;
            CargarLista();
            lvAlarmas.SelectionChanged += LvAlarmas_SelectionChanged;
        }

        private void CargarLista()
        {
            lvAlarmas.ItemsSource = alarmas.Select(a => new
            {
                HoraMinuto = $"{a.Hora:D2}:{a.Minuto:D2}",
                Mensaje = a.Mensaje,
                Original = a
            }).ToList();
            btnEditar.IsEnabled = btnEliminar.IsEnabled = false;
        }
        public void ActualizarLista()
        {
            lvAlarmas.ItemsSource = null;
            lvAlarmas.ItemsSource = alarmas.Select(a => new { HoraMinuto = $"{a.Hora:D2}:{a.Minuto:D2}", Mensaje = a.Mensaje, Original = a }).ToList();
            btnEditar.IsEnabled = btnEliminar.IsEnabled = false;
        }
        private void LvAlarmas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool itemSeleccionado = lvAlarmas.SelectedItem != null;
            btnEditar.IsEnabled = btnEliminar.IsEnabled = itemSeleccionado;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvAlarmas.SelectedItem is not null)
            {
                dynamic seleccionado = lvAlarmas.SelectedItem;
                Alarma alarma = seleccionado.Original;

                EditarAlarmaWindow editarWindow = new EditarAlarmaWindow(alarma, alarmas) { Owner = this };

                if (editarWindow.ShowDialog() == true)
                {
                    ActualizarLista();
                }
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lvAlarmas.SelectedItem is not null)
            {
                dynamic seleccionado = lvAlarmas.SelectedItem;
                Alarma alarma = seleccionado.Original;
                if (MessageBox.Show($"¿Eliminar alarma {alarma.Hora:D2}:{alarma.Minuto:D2} - {alarma.Mensaje}?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    alarmas.Remove(alarma);
                    ActualizarLista();
                }
            }
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }

    }
}