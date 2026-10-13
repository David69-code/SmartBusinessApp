using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SmartBusinessApp.Data;

namespace SmartBusinessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDatos();
            ConfigurarDiseñoGrid();
        }

        private void CargarDatos()
        {
            try
            {
                using (var context = new SmartBusinessContext())
                {
                    if (context.Database.CanConnect())
                    {
                        var productos = context.Productos
                            .Include(p => p.Categoria)
                            .Select(p => new
                            {
                                p.Nombre,
                                p.Precio,
                                p.Existencia,
                                Categoria = p.Categoria.Nombre
                            })
                            .ToList();

                        dgvProductos.DataSource = productos;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo conectar a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error:\n\n{ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDiseñoGrid()
        {
            dgvProductos.BackgroundColor = System.Drawing.Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvProductos.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dgvProductos.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvProductos.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41, 57, 85);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 35;

            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.ReadOnly = true;
        }
    }
}