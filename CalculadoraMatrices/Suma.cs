using System;
using System.Collections.Generic;

namespace CalculadoraMatrices
{
    public static class Suma
    {
        public static bool ValidarSuma(double[,] matriz1, double[,] matriz2)
        {
            return matriz1.GetLength(0) == matriz2.GetLength(0) &&
                   matriz1.GetLength(1) == matriz2.GetLength(1);
        }

        public static double[,] SumarMatriz(double[,] matriz1, double[,] matriz2)
        {
            int filas = matriz1.GetLength(0);
            int columnas = matriz1.GetLength(1);
            double[,] resultado = new double[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    resultado[i, j] = matriz1[i, j] + matriz2[i, j];
                }
            }
            return resultado;
        }

        public static string[,] CrearMatrizOperacionesStr(double[,] matriz1, double[,] matriz2)
        {
            int filas = matriz1.GetLength(0);
            int columnas = matriz1.GetLength(1);
            string[,] matrizConOperaciones = new string[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matrizConOperaciones[i, j] = $"{matriz1[i, j]:F2} + {matriz2[i, j]:F2}";
                }
            }
            return matrizConOperaciones;
        }

        public static string MostrarPasos(string[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            
            int longitudMaxima = 0;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (matriz[i, j].Length > longitudMaxima)
                        longitudMaxima = matriz[i, j].Length;
                }
            }

            string matrizComoTexto = "";
            for (int i = 0; i < filas; i++)
            {
                matrizComoTexto += "[";
                for (int j = 0; j < columnas; j++)
                {
                    matrizComoTexto += matriz[i, j].PadRight(longitudMaxima) + "   ";
                }
                matrizComoTexto = matrizComoTexto.TrimEnd() + "]\n";
            }
            return matrizComoTexto;
        }
    }
}