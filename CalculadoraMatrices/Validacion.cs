using System;

namespace CalculadoraMatrices
{
    public static class Validacion
    {
        public static bool ValidarMatriz(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    // En C# los double ya son del tipo correcto, 
                    // pero podríamos validar si no son NaN o infinitos
                    if (double.IsNaN(matriz[i, j]) || double.IsInfinity(matriz[i, j]))
                        return false;
                }
            }
            return true;
        }

        public static bool EsNumeroValido(string texto)
        {
            // Permite tanto punto como coma decimal (como en el Python original)
            texto = texto.Replace(',', '.');
            return double.TryParse(texto, out _);
        }

        public static double ConvertirANumero(string texto)
        {
            // Reemplaza coma por punto para mantener compatibilidad
            texto = texto.Replace(',', '.');
            if (double.TryParse(texto, out double resultado))
                return resultado;
            return 0.0; // Valor por defecto como en el Python original
        }
    }
}