using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculadoraMatrices
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configuración global de estilo similar al Python
            Application.Run(new CalculadoraMatricesForm());
        }
    }
}   