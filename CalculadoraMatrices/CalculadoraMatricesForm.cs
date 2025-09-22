﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalculadoraMatrices
{
    public partial class CalculadoraMatricesForm : Form
    {
        // Controles para matriz A
        private NumericUpDown spinFilasA;
        private NumericUpDown spinColumnasA;
        private DataGridView tablaMatrizA;

        // Controles para matriz B
        private NumericUpDown spinFilasB;
        private NumericUpDown spinColumnasB;
        private DataGridView tablaMatrizB;

        // Otros controles
        private RichTextBox pizarra;

        public CalculadoraMatricesForm()
        {
            InitializeComponent();
            ConfigurarEstilosGlobales();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Calculadora de Matrices";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Layout principal
            TableLayoutPanel layoutPrincipal = new TableLayoutPanel();
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.RowCount = 4;
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.Padding = new Padding(20);

            // Configurar filas del layout principal
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Banner
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 60)); // Matrices
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Botones
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 40)); // Resultados

            this.Controls.Add(layoutPrincipal);

            // 1. Crear banner
            CrearBanner(layoutPrincipal);

            // 2. Crear sección de matrices
            CrearSeccionMatrices(layoutPrincipal);

            // 3. Crear botones
            CrearBotonesOperacion(layoutPrincipal);

            // 4. Crear pizarra de resultados
            CrearPizarraResultados(layoutPrincipal);

            this.ResumeLayout(false);
        }

        private void CrearBanner(TableLayoutPanel layoutPrincipal)
        {
            Label banner = new Label();
            banner.Text = "Calculadora de Matrices";
            banner.Font = new Font("Arial", 20, FontStyle.Bold);
            banner.TextAlign = ContentAlignment.MiddleCenter;
            banner.Dock = DockStyle.Fill;
            banner.BackColor = Color.FromArgb(137, 207, 240);
            banner.ForeColor = Color.Black;

            layoutPrincipal.Controls.Add(banner, 0, 0);
        }

        private void CrearSeccionMatrices(TableLayoutPanel layoutPrincipal)
        {
            TableLayoutPanel layoutMatrices = new TableLayoutPanel();
            layoutMatrices.Dock = DockStyle.Fill;
            layoutMatrices.ColumnCount = 2;
            layoutMatrices.RowCount = 1;
            layoutMatrices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layoutMatrices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Crear grupos de matrices
            GroupBox grupoMatrizA = CrearGrupoMatriz("Primera Matriz", "A");
            GroupBox grupoMatrizB = CrearGrupoMatriz("Segunda Matriz", "B");

            layoutMatrices.Controls.Add(grupoMatrizA, 0, 0);
            layoutMatrices.Controls.Add(grupoMatrizB, 1, 0);

            layoutPrincipal.Controls.Add(layoutMatrices, 0, 1);
        }

        private GroupBox CrearGrupoMatriz(string titulo, string idMatriz)
        {
            GroupBox grupo = new GroupBox();
            grupo.Text = titulo;
            grupo.Dock = DockStyle.Fill;
            grupo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            grupo.BackColor = Color.FromArgb(250, 250, 250);
            grupo.Margin = new Padding(5);

            TableLayoutPanel layoutGrupo = new TableLayoutPanel();
            layoutGrupo.Dock = DockStyle.Fill;
            layoutGrupo.RowCount = 2;
            layoutGrupo.ColumnCount = 1;
            layoutGrupo.Padding = new Padding(10);
            layoutGrupo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layoutGrupo.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Panel de controles de dimensiones
            Panel panelDimensiones = new Panel();
            panelDimensiones.Dock = DockStyle.Fill;
            panelDimensiones.Height = 40;

            Label lblFilas = new Label();
            lblFilas.Text = "Filas:";
            lblFilas.Location = new Point(0, 12);
            lblFilas.Size = new Size(40, 20);
            lblFilas.ForeColor = Color.Black;

            NumericUpDown spinFilas = new NumericUpDown();
            spinFilas.Minimum = 1;
            spinFilas.Value = 2;
            spinFilas.Location = new Point(45, 10);
            spinFilas.Size = new Size(50, 20);

            Label lblColumnas = new Label();
            lblColumnas.Text = "Columnas:";
            lblColumnas.Location = new Point(105, 12);
            lblColumnas.Size = new Size(60, 20);
            lblColumnas.ForeColor = Color.Black;

            NumericUpDown spinColumnas = new NumericUpDown();
            spinColumnas.Minimum = 1;
            spinColumnas.Value = 2;
            spinColumnas.Location = new Point(170, 10);
            spinColumnas.Size = new Size(50, 20);

            Button btnCrear = new Button();
            btnCrear.Text = "Crear/Actualizar Matriz";
            btnCrear.Location = new Point(230, 8);
            btnCrear.Size = new Size(150, 25);
            btnCrear.BackColor = Color.FromArgb(137, 207, 240);
            btnCrear.ForeColor = Color.Black;
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.FlatAppearance.BorderSize = 0;

            panelDimensiones.Controls.AddRange(new Control[] { lblFilas, spinFilas, lblColumnas, spinColumnas, btnCrear });

            // Crear DataGridView para la matriz
            DataGridView tabla = new DataGridView();
            tabla.Dock = DockStyle.Fill;
            tabla.BackgroundColor = Color.White;
            tabla.GridColor = Color.FromArgb(204, 204, 204);
            tabla.BorderStyle = BorderStyle.Fixed3D;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.RowHeadersVisible = false;
            tabla.ColumnHeadersVisible = false;
            tabla.SelectionMode = DataGridViewSelectionMode.CellSelect;

            // Configurar tabla inicial (2x2)
            ActualizarTablaMatriz(2, 2, tabla);

            // Conectar eventos
            btnCrear.Click += (s, e) => ActualizarTablaMatriz((int)spinFilas.Value, (int)spinColumnas.Value, tabla);

            // Guardar referencias
            if (idMatriz == "A")
            {
                this.spinFilasA = spinFilas;
                this.spinColumnasA = spinColumnas;
                this.tablaMatrizA = tabla;
            }
            else
            {
                this.spinFilasB = spinFilas;
                this.spinColumnasB = spinColumnas;
                this.tablaMatrizB = tabla;
            }

            layoutGrupo.Controls.Add(panelDimensiones, 0, 0);
            layoutGrupo.Controls.Add(tabla, 0, 1);
            grupo.Controls.Add(layoutGrupo);

            return grupo;
        }

        private void ActualizarTablaMatriz(int filas, int columnas, DataGridView tabla)
        {
            tabla.Columns.Clear();
            tabla.Rows.Clear();

            for (int j = 0; j < columnas; j++)
            {
                tabla.Columns.Add($"Col{j}", $"");
                tabla.Columns[j].Width = tabla.Width / columnas - 2;
            }

            for (int i = 0; i < filas; i++)
            {
                tabla.Rows.Add();
                tabla.Rows[i].Height = Math.Max(25, (tabla.Height - 25) / filas);
            }
        }

        private void CrearBotonesOperacion(TableLayoutPanel layoutPrincipal)
        {
            Panel panelBotones = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };

            Button btnSumar = new Button
            {
                Text = "Sumar matrices",
                Size = new Size(150, 40),
                Location = new Point(50, 10),
                BackColor = Color.FromArgb(137, 207, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnSumar.FlatAppearance.BorderSize = 0;

            Button btnMultiplicar = new Button
            {
                Text = "Multiplicar matrices",
                Size = new Size(150, 40),
                Location = new Point(220, 10),
                BackColor = Color.FromArgb(137, 207, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnMultiplicar.FlatAppearance.BorderSize = 0;

            Button btnLimpiar = new Button
            {
                Text = "Limpiar",
                Size = new Size(150, 40),
                Location = new Point(390, 10),
                BackColor = Color.FromArgb(137, 207, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnLimpiar.FlatAppearance.BorderSize = 0;

            // Conectar eventos
            btnSumar.Click += RealizarSuma;
            btnMultiplicar.Click += RealizarMultiplicacion;
            btnLimpiar.Click += LimpiarTodo;

            // Efectos hover
            ConfigurarEfectosHover(btnSumar);
            ConfigurarEfectosHover(btnMultiplicar);
            ConfigurarEfectosHover(btnLimpiar);

            panelBotones.Controls.AddRange(new Control[] { btnSumar, btnMultiplicar, btnLimpiar });
            layoutPrincipal.Controls.Add(panelBotones, 0, 2);
        }

        private void ConfigurarEfectosHover(Button btn)
        {
            Color colorOriginal = btn.BackColor;
            Color colorHover = Color.FromArgb(122, 197, 224);
            Color colorPressed = Color.FromArgb(105, 180, 208);

            btn.MouseEnter += (s, e) => btn.BackColor = colorHover;
            btn.MouseLeave += (s, e) => btn.BackColor = colorOriginal;
            btn.MouseDown += (s, e) => btn.BackColor = colorPressed;
            btn.MouseUp += (s, e) => btn.BackColor = colorHover;
        }

        private void CrearPizarraResultados(TableLayoutPanel layoutPrincipal)
        {
            GroupBox grupoPizarra = new GroupBox
            {
                Text = "Pizarra de Resultados",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(250, 250, 250),
                Margin = new Padding(5)
            };

            this.pizarra = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Courier New", 11),
                BackColor = Color.White,
                ForeColor = Color.Black,
                Margin = new Padding(10)
            };

            grupoPizarra.Controls.Add(this.pizarra);
            layoutPrincipal.Controls.Add(grupoPizarra, 0, 3);
        }

        private double[,] LeerMatrizDesdeTabla(DataGridView tabla)
        {
            int filas = tabla.RowCount;
            int columnas = tabla.ColumnCount;
            double[,] matriz = new double[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    string valor = tabla[j, i].Value?.ToString() ?? "";
                    if (string.IsNullOrEmpty(valor))
                    {
                        matriz[i, j] = 0.0;
                    }
                    else if (Validacion.EsNumeroValido(valor))
                    {
                        matriz[i, j] = Validacion.ConvertirANumero(valor);
                    }
                    else
                    {
                        MostrarError("Valor no numérico", $"El valor en la celda [{i + 1}, {j + 1}] no es un número válido.");
                        return null;
                    }
                }
            }
            return matriz;
        }

        private string FormatearMatrizHtml(double[,] matriz, string titulo = "")
        {
            StringBuilder html = new StringBuilder();
            if (!string.IsNullOrEmpty(titulo))
            {
                html.AppendLine($"=== {titulo} ===");
            }

            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);

            for (int i = 0; i < filas; i++)
            {
                html.Append("[");
                for (int j = 0; j < columnas; j++)
                {
                    html.Append($"{matriz[i, j],8:F2}");
                    if (j < columnas - 1) html.Append("  ");
                }
                html.AppendLine("]");
            }
            html.AppendLine();
            return html.ToString();
        }

        private void RealizarSuma(object sender, EventArgs e)
        {
            double[,] matrizA = LeerMatrizDesdeTabla(this.tablaMatrizA);
            double[,] matrizB = LeerMatrizDesdeTabla(this.tablaMatrizB);

            if (matrizA == null || matrizB == null)
                return;

            if (!Suma.ValidarSuma(matrizA, matrizB))
            {
                MostrarError("Error de Dimensiones", "Las matrices deben tener las mismas dimensiones para poder sumarse.");
                return;
            }

            this.pizarra.Clear();

            // Usar el mismo formateo para todas las matrices
            this.pizarra.AppendText("=== Matriz A ===\n");
            this.pizarra.AppendText(Suma.MostrarMatriz(matrizA));
            this.pizarra.AppendText("\n");

            this.pizarra.AppendText("=== Matriz B ===\n");
            this.pizarra.AppendText(Suma.MostrarMatriz(matrizB));
            this.pizarra.AppendText("\n");

            string[,] matrizOperaciones = Suma.CrearMatrizOperacionesStr(matrizA, matrizB);
            this.pizarra.AppendText("=== Pasos intermedios de la suma ===\n");
            this.pizarra.AppendText(Suma.MostrarPasos(matrizOperaciones));
            this.pizarra.AppendText("\n");

            double[,] resultado = Suma.SumarMatriz(matrizA, matrizB);
            this.pizarra.AppendText("=== Resultado final de la operación ===\n");
            this.pizarra.AppendText(Suma.MostrarMatriz(resultado));
        }

        private void RealizarMultiplicacion(object sender, EventArgs e)
        {
            double[,] matrizA = LeerMatrizDesdeTabla(this.tablaMatrizA);
            double[,] matrizB = LeerMatrizDesdeTabla(this.tablaMatrizB);

            if (matrizA == null || matrizB == null)
                return;

            if (!Multiplicacion.ValidarMultiplicacion(matrizA, matrizB))
            {
                MostrarError("Error de Dimensiones",
                    "El número de columnas de la Matriz A debe ser igual al número de filas de la Matriz B.");
                return;
            }

            this.pizarra.Clear();

            // Usar el mismo formateo unificado para todas las matrices
            this.pizarra.AppendText("=== Matriz A ===\n");
            this.pizarra.AppendText(Suma.MostrarMatriz(matrizA));
            this.pizarra.AppendText("\n");

            this.pizarra.AppendText("=== Matriz B ===\n");
            this.pizarra.AppendText(Suma.MostrarMatriz(matrizB));
            this.pizarra.AppendText("\n");

            // Mostrar pasos del cálculo
            List<string> pasos = Multiplicacion.CrearPasosMultiplicacion(matrizA, matrizB);
            this.pizarra.AppendText("=== Pasos del Cálculo ===\n");
            foreach (string paso in pasos)
            {
                this.pizarra.AppendText(paso + "\n");
            }
            this.pizarra.AppendText("\n");

            // Mostrar resultado final
            double[,] resultado = Multiplicacion.MultiplicarMatrices(matrizA, matrizB);
            this.pizarra.AppendText("=== Resultado Final ===\n");
            this.pizarra.AppendText(Suma.MostrarMatriz(resultado));
        }

        private void LimpiarTodo(object sender, EventArgs e)
        {
            // Limpiar matriz A
            for (int i = 0; i < this.tablaMatrizA.RowCount; i++)
            {
                for (int j = 0; j < this.tablaMatrizA.ColumnCount; j++)
                {
                    this.tablaMatrizA[j, i].Value = "";
                }
            }

            // Limpiar matriz B
            for (int i = 0; i < this.tablaMatrizB.RowCount; i++)
            {
                for (int j = 0; j < this.tablaMatrizB.ColumnCount; j++)
                {
                    this.tablaMatrizB[j, i].Value = "";
                }
            }

            this.pizarra.Clear();
        }

        private void MostrarError(string titulo, string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ConfigurarEstilosGlobales()
        {
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ForeColor = Color.Black;
        }
    }
}