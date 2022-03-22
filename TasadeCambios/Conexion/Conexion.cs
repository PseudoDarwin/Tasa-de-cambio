using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Conexion
{
    public class ClassCon
    {
        public SqlConnection CadenaConexion = new SqlConnection("Server=DESKTOP-GSD2R7J;DataBase=DB_TasaCambio;Integrated Security=true");
        public SqlConnection conectar = new SqlConnection();
        public SqlDataReader leer;
        public DataTable tabla = new DataTable();
        public SqlCommand comando = new SqlCommand();



        
    public SqlConnection AbrirConexion()
    {
        try
        {
            if (CadenaConexion.State == ConnectionState.Closed)
            {
                CadenaConexion.Open();
            }
        }
        catch
        {
            MessageBox.Show("No se abrió la conexión");
        }
        return CadenaConexion;
    }


        public SqlConnection CerrarConexion()
        {
            try
            {
                if (CadenaConexion.State == ConnectionState.Open)
                    CadenaConexion.Close();
            }
            catch
            {
                MessageBox.Show("No se cerró la conexión");
            }
            return CadenaConexion;
        }
    }
}
