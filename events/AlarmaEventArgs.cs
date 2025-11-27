using System;

namespace events
{
    public class AlarmaEventArgs : EventArgs
    {
        public string Mensaje { get; }
        public AlarmaEventArgs(string mensaje) { Mensaje = mensaje; }
    }
}