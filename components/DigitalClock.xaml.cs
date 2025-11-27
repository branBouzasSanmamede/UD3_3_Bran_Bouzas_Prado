using events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UD3_3_Bran_Bouzas_Prado.models;

namespace components
{
    public partial class DigitalClock : UserControl
    {
        private readonly DispatcherTimer timer;
        private readonly List<Alarma> alarmas = new List<Alarma>();

        public bool Formato12Horas { get; set; } = false;

        public static readonly DependencyProperty MensajeAlarmaProperty = DependencyProperty.Register("MensajeAlarma", typeof(string), typeof(DigitalClock), new PropertyMetadata("¡ALARMA!"));

        public string MensajeAlarma { get => (string)GetValue(MensajeAlarmaProperty); set => SetValue(MensajeAlarmaProperty, value); }

        public event EventHandler<AlarmaEventArgs>? AlarmaActivadaEvento;
        public List<Alarma> GetAlarmas() => alarmas;

        public int HoraAlarma { get; set; } = 0;
        public int MinutoAlarma { get; set; } = 0;

        public DigitalClock()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            txtFormato.Text = Formato12Horas ? "12h" : "24h";
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            ActualizarHora();
            DateTime now = DateTime.Now;

            foreach (var alarma in alarmas)
            {
                if (now.Hour == alarma.Hora && now.Minute == alarma.Minuto && now.Second == 0)
                {
                    AlarmaActivadaEvento?.Invoke(this, new AlarmaEventArgs(alarma.Mensaje));
                }
            }
        }
        public void ActualizarHora()
        {
            DateTime now = DateTime.Now;
            txtClock.Text = Formato12Horas ? now.ToString("hh:mm:ss") : now.ToString("HH:mm:ss");
            txtFormato.Text = Formato12Horas ? "12h" : "24h";
        }
        public bool AgregarAlarma(int hora, int minuto, string mensaje)
        {
            if (alarmas.Any(a => a.Hora == hora && a.Minuto == minuto)) return false; 

            alarmas.Add(new Alarma { Hora = hora, Minuto = minuto, Mensaje = mensaje });

            HoraAlarma = hora;
            MinutoAlarma = minuto;
            return true;
        }
        public void DetenerTimer() => timer?.Stop();
    }
}