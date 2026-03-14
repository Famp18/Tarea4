using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarea_4
{
    public partial class Menuprincipal : Form
    {
        public Menuprincipal()
        {
            InitializeComponent();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas frm = new Ventas();
            frm.MdiParent = this; // Establecer el formulario principal como padre
            frm.Show(); // Mostrar el formulario de ventas


        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicación
            Application.Exit();


        }
    }
}
