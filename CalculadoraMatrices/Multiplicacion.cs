﻿using System;
using System.Collections.Generic;

namespace CalculadoraMatrices
{
    public static class Multiplicacion
    {
        public static bool ValidarMultiplicacion(double[,] matriz1, double[,] matriz2)
        {
            return matriz1.GetLength(1) == matriz2.GetLength(0);
        }

        public static double[,] MultiplicarMatrices(double[,] matrizA, double[,] matrizB)
        {
            int filasA = matrizA.GetLength(0);
            int columnasA = matrizA.GetLength(1);
            int columnasB = matrizB.GetLength(1);

            double[,] matrizResultado = new double[filasA, columnasB];

            for (int i = 0; i < filasA; i++)
            {
                for (int j = 0; j < columnasB; j++)
                {
                    double suma = 0;
                    for (int k = 0; k < columnasA; k++)
                    {
                        suma += matrizA[i, k] * matrizB[k, j];
                    }
                    matrizResultado[i, j] = suma;
                }
            }
            return matrizResultado;
        }

        public static List<string> CrearPasosMultiplicacion(double[,] matrizA, double[,] matrizB)
        {
            int filasA = matrizA.GetLength(0);
            int columnasA = matrizA.GetLength(1);
            int columnasB = matrizB.GetLength(1);

            List<string> pasosCalculo = new List<string>();

            for (int i = 0; i < filasA; i++)
            {
                for (int j = 0; j < columnasB; j++)
                {
                    string operacionStr = "";
                    string sumaStr = "";
                    double resultadoPaso = 0;
                    List<double> productosIntermedios = new List<double>();

                    for (int k = 0; k < columnasA; k++)
                    {
                        double valorA = matrizA[i, k];
                        double valorB = matrizB[k, j];
                        double producto = valorA * valorB;
                        productosIntermedios.Add(producto);

                        operacionStr += $"({valorA:F2} * {valorB:F2})";
                        if (k < columnasA - 1)
                            operacionStr += " + ";

                        resultadoPaso += producto;
                    }

                    for (int k = 0; k < productosIntermedios.Count; k++)
                    {
                        sumaStr += $"{productosIntermedios[k]:F2}";
                        if (k < productosIntermedios.Count - 1)
                            sumaStr += " + ";
                    }

                    string pasoCompleto = $"C{i + 1}, F{j + 1} = {operacionStr} -> {sumaStr} = {resultadoPaso:F2}";
                    pasosCalculo.Add(pasoCompleto);
                }
            }
            return pasosCalculo;
        }
    }
}