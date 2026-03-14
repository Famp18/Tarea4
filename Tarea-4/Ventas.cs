using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarea_4.DataBase_FIsico;

namespace Tarea_4
{
    public partial class FrmVentas : Form
    {
        private VentasContext _context;
        private DataGridView dgProductos; 
        private ComboBox cmbCategoria;
        private TextBox txtNombre;
        private TextBox txtPrecio;
        private TextBox txtDescripcion;
        private TextBox txtStock;
        private TextBox txtDescripcionActualizado; 
      

        private TextBox txtDescripcionAgregar; 

        public FrmVentas()
        {
            InitializeComponent();
        }

        private void cargarDatos()
        {
            var listaProductos = _context.Productos
                .Cast<Producto>()
                .Select(p => new {
                    ID = p.ProductoID,
                    Nombre = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = p.Categoria != null ? p.Categoria.NombreCategoria : string.Empty
                }).ToList();

            dgProductos.DataSource = listaProductos; 
        }

        private void cargarCmbCategorias()
        {
            var categorias = _context.Categorias
                .Cast<dynamic>() // Fix: Cast to dynamic to access properties
                .Select(c => new {
                    ID = c.CategoriaID,
                    Nombre = c.NombreCategoria
                }).ToList();

            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "ID";
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción está incorrecto o vacio.");
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtStock.Text))

                        // Then, update all usages of 'txtDescripcion' in the class that refer to the field to use the new name.

                        if (string.IsNullOrEmpty(txtDescripcionAgregar.Text))
                        {
                            MessageBox.Show("La descripción está incorrecto o vacio.");
                            return;
                        }
                    {
                        MessageBox.Show("El stock está incorrecta o vacia.");
                        return;
                    }
                    if (string.IsNullOrEmpty(cmbCategoria.SelectedValue.ToString()))
                    {
                        MessageBox.Show("La categoria está incorrecto o vacio.");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtPrecio.Text))
                    {
                        MessageBox.Show("El precio está incorrecta o vacia.");
                        return;
                    }

                    Productos productos = new Productos()
                    {
                        NombreProducto = txtNombre.Text,
                        Descripcion = txtDescripcion.Text,
                        Stock = Convert.ToInt32(txtStock.Text),
                        CategoriaID = Convert.ToInt32(cmbCategoria.SelectedValue),
                        Precio = Convert.ToDecimal(txtPrecio.Text),
                    };

                    var productosList = _context.Productos as IList<object>;
                    if (productosList != null)
                    {
                        productosList.Add(productos);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el producto. La colección de productos no es modificable.");
                    }

                    int rowsAffected = _context.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Se ha insertado el producto en la base de datos.");
                    }

                    this.cargarDatos();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            int productoID = Convert.ToInt32(txtID.Text);

            Productos productos = (Productos)_context.Productos.FirstOrDefault(q => (q as Productos)?.ProductoID == productoID);
            if (productos == null)
            {
                MessageBox.Show("Producto no existe.");
                return;
            }

            var productosList = _context.Productos as IList<object>;
            if (productosList != null)
            {
                productosList.Remove(productos);
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto. La colección de productos no es modificable.");
                return;
            }

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha eliminado el producto en la base de datos.");
            }

            this.cargarDatos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDActualizado.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            if (string.IsNullOrEmpty(txtNombreActualizado.Text))
            {
                MessageBox.Show("El nombre está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtDescripcionActualizado.Text))
            {
                MessageBox.Show("La descripción está incorrecto o vacio.");
                return;
            }

            if (string.IsNullOrEmpty(txtStockActualizado.Text))
            {
                MessageBox.Show("El stock de nacimiento estáincorrecta o vacia.");
                return;
            }
            if (string.IsNullOrEmpty(cmbCategoriaActualizado.SelectedValue.ToString()))
            {
                MessageBox.Show("La categoria está incorrecto o vacio.");
                return;
            }
            if (string.IsNullOrEmpty(txtPrecioActualizado.Text))
            {
                MessageBox.Show("El precio está incorrecta o vacia.");
                return;
            }

            int productoID = Convert.ToInt32(txtIDActualizado.Text);

            Productos productos = _context.Productos
                .Cast<Productos>()
                .FirstOrDefault(q => q.ProductoID.Equals(productoID));
            if (productos == null)
            {
                MessageBox.Show("Producto no existe.");
                return;
            }

            productos.NombreProducto = txtNombreActualizado.Text;
            productos.Descripcion = txtDescripcionActualizado.Text;
            productos.Stock = Convert.ToInt32(txtStockActualizado.Text);
            productos.CategoriaID = Convert.ToInt32(cmbCategoriaActualizado.SelectedValue);
            productos.Precio = Convert.ToDecimal(txtPrecioActualizado.Text);

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha actualizado el producto en la base de datos.");
            }

            this.cargarDatos();
        }


        private void Productos_Load(object sender, EventArgs e)
        {
            _context = new VentasContext();

            this.cargarCmbCategorias();
            this.cargarDatos();
        }
        private void Ventas_Load(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
    
}
