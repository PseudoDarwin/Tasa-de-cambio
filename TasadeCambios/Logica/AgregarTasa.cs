using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Logica
{
    public class AgregarTasa
    {
        //Instanciamos la capa de conexion
        Conexion.ClassCon conexion = new Conexion.ClassCon();

        public void CrearTasa(double valor , DateTime fecha)
        {
            try
            {
                
                conexion.comando.Connection = conexion.AbrirConexion();
                conexion.comando.CommandText = "Agregar_Tasa";
                conexion.comando.CommandType = CommandType.StoredProcedure;
                conexion.comando.Parameters.AddWithValue("@valor", valor);
                conexion.comando.Parameters.AddWithValue("@fecha", fecha);
                conexion.comando.ExecuteNonQuery();
                conexion.comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al crear tasa -- "+e);
            }
        }
    }
}
