using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Logica;
namespace TasadeCambios
{
    public partial class FormPrincipal : Form
    {

        //Instancia del servicio SOAP
        ref_bcn.Tipo_Cambio_BCN bcn = new ref_bcn.Tipo_Cambio_BCN();

        //Intancia capa logica
        Logica.AgregarTasa tasa = new Logica.AgregarTasa();
     
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Verificando criterio de busqueda
            if (cmbBusqueda.Text == "")
            {
                MessageBox.Show("Error, seleccione un criterio de busqueda");
            }
            else
            {
                DataGridViewRow fila = new DataGridViewRow();

                //Obtenemos los valores del metodo de busqueda
                DateTime fecha = dtpBusqueda.Value;
                int dia = dtpBusqueda.Value.Day;
                int mes = dtpBusqueda.Value.Month;
                int año = dtpBusqueda.Value.Year;
                if (cmbBusqueda.Text == "Día especifico")
                {
                    //Busqueda por día especifico
                    var resultadoDia = bcn.RecuperaTC_Dia(año, mes, dia);

                    //Mostrando valores en el DataGrid
                    dgvResultados.Rows.Clear();
                    fila.CreateCells(dgvResultados);
                    fila.Cells[0].Value = fecha;
                    fila.Cells[1].Value = resultadoDia;
                    dgvResultados.Rows.Add(fila);
                }
                if (cmbBusqueda.Text == "Mes específico")
                {
                    //Busqueda por día especifico
                    var resultadoMes = bcn.RecuperaTC_Mes(año, mes).OuterXml;

                    //OuterXml obtiene los resultados de la busqueda en formato XML guardando cada uno en una rama individual

                    //Recorriendo el XML

                    //xdoc crea un nuevo documento a parit de los resultados del anterior xml
                    XDocument xdoc = XDocument.Parse(resultadoMes);
                    dgvResultados.Rows.Clear();
                    fila.CreateCells(dgvResultados);

                    IEnumerable<XElement> data = xdoc.XPathSelectElements(".//Fecha").ToArray();
                    //Recorremos los datos de fecha y valor del xml y los agregamos al Datagrid                 
                    foreach (var el in data)
                    {
                        int rowEscribir = dgvResultados.Rows.Count - 1;
                        dgvResultados.Rows.Add(1);
                        dgvResultados.Rows[rowEscribir].Cells[0].Value = el.Value;
                        fila.Cells[1].Value = el.Value;
                        


                        
                    }

                    int i=0;
                    IEnumerable<XElement> dvalor = xdoc.XPathSelectElements(".//Valor").ToArray();
                    foreach (var el in dvalor)
                    {
                        dgvResultados.Rows[i].Cells[1].Value = el.Value;
                        fila.Cells[1].Value = el.Value;
                        i = i + 1;
                        Console.WriteLine(el.Value);
                    }

                    dgvResultados.Sort(dgvResultados.Columns[0], ListSortDirection.Ascending);
                   
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFecha_Click(object sender, EventArgs e)
        {
            int cantidadFechas = dgvResultados.RowCount - 1;
            double valor;
            DateTime fecha;
            Boolean comprobar = true;
            for (int i = 0; i < cantidadFechas; i++)
            {
                try
                {
                    valor =  Convert.ToDouble(dgvResultados.Rows[i].Cells[1].Value.ToString());
                    fecha = Convert.ToDateTime(dgvResultados.Rows[i].Cells[0].Value.ToString());
                    tasa.CrearTasa(valor,fecha);
                }
                catch
                {
                    comprobar = false;
                }
            }
            if (comprobar == true)
            {
                MessageBox.Show("Tasa agregada con exito");
            }
            else
            {
                MessageBox.Show("error");
            }
        }

  
    }
}
