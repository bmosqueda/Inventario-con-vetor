using System;
using System.Windows.Forms;

namespace Inventario
{
    public partial class Form1 : Form
    {
        public class Producto
        {
            private static int ids = -1;

            private int codigo;
            private string nombre;
            private double costo;

            public int Codigo { get { return codigo; } }
            public string Nombre { get { return nombre; } }
            public double Costo { get { return costo; } }
  
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }

            public Producto(string name, double cost, string description, int amount)
            {
                ids++;
                this.codigo = ids;
                this.nombre = name;
                this.costo = cost;
                this.Descripcion = description;
                this.Cantidad = amount;
            }

            public override string ToString()
            {
                return "******* " + nombre + " *******\n" +
                        "Código: " + codigo + "\n" +
                        "Cantidad: " + Cantidad + "\n" +
                        "Costo: " + costo + "\n\n" +
                        "Descripción: " + Descripcion;
            }
        }

        public class Inventario
        {
            public Producto[] productos;
            public int Length { get { return productos.Length; } }
            private int lastElement;

            public Inventario(int numeroProductos)
            {
                productos = new Producto[numeroProductos];
                for (int i = 0; i < Length; i++)
                {
                    productos[i] = null;
                }
                lastElement = 0;
            }

            public string agregar(string name, double cost, string description, int amount)
            {
                if (lastElement < Length)
                {
                    productos[lastElement] = new Producto(name, cost, description, amount);
                    lastElement++;
                    return "Se agregó exitosamente el producto";
                }
                else
                {
                    return "Ya no se pueden agregar más productos";
                }
            }

            public Producto buscar(int codigo)
            {
                for(int i = 0; i < lastElement; i++)
                {
                    if (productos[i].Codigo == codigo)
                        return productos[i];
                }

                return null;
            }

            public string eliminar(int pos)
            {
                if (pos < 0)
                {
                    return "La posición debe ser mayor a 0";
                }
                else if (pos > lastElement)
                {
                    return "La posición debe ser menor a la cantidad actual de productos (" + lastElement + ")";
                }
                else
                {
                    for (int i = pos; i < lastElement - 1; i++)
                        productos[i] = new Producto(productos[i + 1].Nombre, productos[i + 1].Costo, productos[i + 1].Descripcion, productos[i + 1].Cantidad);

                    productos[lastElement - 1] = null;

                    lastElement--;
                    return "Se eliminó correctamente el producto";
                }
            }

            public string insertar(int posicion, Producto producto)
            {
                if (posicion < 0)
                {
                    return "La posición debe ser mayor a 0";
                }
                else if (posicion > lastElement)
                {
                    return "La posición debe ser menor a la cantidad actual de productos (" + lastElement + ")";
                }
                else
                {
                    Producto temporal = new Producto(productos[lastElement - 1].Nombre, productos[lastElement - 1].Costo, productos[lastElement - 1].Descripcion, productos[lastElement - 1].Cantidad);

                    for (int i = posicion; i < lastElement; i++)
                    {
                        Producto temp = new Producto(productos[i].Nombre, productos[i].Costo, productos[i].Descripcion, productos[i].Cantidad);
                        productos[i] = new Producto(producto.Nombre, producto.Costo, producto.Descripcion, producto.Cantidad);
                        producto = temp;
                    }

                    if (lastElement < Length)
                        productos[lastElement++] = temporal;

                    return "Se insertó correctamente el producto";
                }
            }

            public string listar()
            {
                string cadena = "";
                for (int i = 0; i < lastElement; i++)
                {
                    cadena += productos[i].ToString() + "\n__________________________________\n";
                }
                return cadena;
            }
        }

        Inventario inventario;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inventario = new Inventario(15);
            addProductos();
        }

        public void addProductos()
        {
            for(int i = 0; i < 10; i++)
            {
                inventario.agregar("producto " + i, i, "description " + i + " " + i + " " + " "+ i, i);
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            txtMostrar.Text = inventario.listar();
            lblEstado.Text = "Elementos listados";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                string agregar = inventario.agregar(txtNombre.Text, Convert.ToDouble(txtCantidad.Text), txtDescripcion.Text, Convert.ToInt32(txtCantidad.Text));
                lblEstado.Text = agregar;
            }
        }

        private void btnElimar_Click(object sender, EventArgs e)
        {
            int posicion = Convert.ToInt32(numPosicion.Value);
            string eliminar = inventario.eliminar(posicion); 
            lblEstado.Text = eliminar;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text != "")
            {
                int codigo = Convert.ToInt32(txtCodigo.Text);
                Producto buscar = inventario.buscar(codigo);
                if(buscar != null)
                {
                    lblEstado.Text = "Producto encontrado";
                    MessageBox.Show(buscar.ToString());
                }
                else
                {
                    lblEstado.Text = "Producto no encontrado";
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                int posicion = Convert.ToInt32(numPosicion.Value);
                string insertar = inventario.insertar(posicion, new Producto(txtNombre.Text, Convert.ToDouble(txtCantidad.Text), txtDescripcion.Text, Convert.ToInt32(txtCantidad.Text)));
                lblEstado.Text = insertar;
            }
        }

        public bool validar()
        {
            return txtNombre.Text != "" && txtCodigo.Text != "" && txtCantidad.Text != "" && txtCosto.Text != "" && txtDescripcion.Text != "";
        }
    }
}
