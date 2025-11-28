using System.Windows;

namespace UD3_3_Bran_Bouzas_Prado.utils
{
    public static class WindowHelper
    {
        public static void CentradoDerecha(Window window, double margen = 20)
        {
            window.ContentRendered += (_, __) =>
            {
                var screen = SystemParameters.WorkArea;
                window.Left = (screen.Width / 2) + margen;
                window.Top = (screen.Height - window.ActualHeight) / 2;
            };
        }
        public static void CentradoIzquierda(Window window, double margen = 20)
        {
            window.ContentRendered += (_, __) =>
            {
                var screen = SystemParameters.WorkArea;
                window.Left = (screen.Width / 2) - window.ActualWidth - margen;
                window.Top = (screen.Height - window.ActualHeight) / 2;
            };
        }
    }
}
