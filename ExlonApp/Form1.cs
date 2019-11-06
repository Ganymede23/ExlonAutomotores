using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ExlonApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"DESKTOP-BL144A7";
            builder.InitialCatalog = "Exlon";
            builder.IntegratedSecurity = true;

            var conditionstring = builder.ToString();

            //var query = @"SELECT * FROM ListadoOperacionesUltimoAnio";

            //var query2 = @"EXECUTE AltaOperaciones 'VALOR-PARAMETRO1','VALOR-PARAMETRO2';";

            //var query = @"select*
            //                from Personas
            //                where nro_doc = '37421451'";

            //using (SqlConnection sql = new SqlConnection(conditionstring))
            //{
            //    using (SqlCommand cmd = new SqlCommand(query, sql))
            //    {
            //        DataTable dt = new DataTable(); //Tabla de donde se levantan los datos.
            //        SqlDataAdapter da = new SqlDataAdapter(cmd); //Lee el query. Se usa cuando lees datos.

            //        sql.Open();
            //        da.Fill(dt); //da recibe los datos del dt.

            //        int c = 0;
            //        c++;
            //    }
            //}

            using (SqlConnection sql = new SqlConnection(conditionstring))
            {
                using (SqlCommand cmd = new SqlCommand("AltaOperaciones", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id_vehiculo", textBox1.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@fecha_op", textBox2.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@tipo_operacion", checkbox1.SelectedItem.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@monto_total", textBox6.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@tipo_doc_empleado", textBox4.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@nro_doc_empleado", textBox9.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@tipo_doc_cliente", textBox7.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@nro_doc_cliente", textBox3.Text.ToString()));

                    sql.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && checkbox1.CheckedItems.Count > 0)
            {
                checkbox1.ItemCheck -= CheckedListBox1_ItemCheck;
                checkbox1.SetItemChecked(checkbox1.CheckedIndices[0], false);
                checkbox1.ItemCheck += CheckedListBox1_ItemCheck;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"DESKTOP-BL144A7";
            builder.InitialCatalog = "Exlon";
            builder.IntegratedSecurity = true;

            var conditionstring = builder.ToString();

            var query = @"SELECT * FROM ListadoOperacionesUltimoAnio";

            //var query = @"select*
            //                from Personas
            //                where nro_doc = '37421451'";

            using (SqlConnection sql = new SqlConnection(conditionstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    DataTable dt = new DataTable(); //Tabla de donde se levantan los datos.
                    SqlDataAdapter da = new SqlDataAdapter(cmd); //Lee el query. Se usa cuando lees datos.

                    sql.Open();
                    da.Fill(dt); //da recibe los datos del dt.

                    dataGridView1.DataSource = dt;
                }
            }
        }
    }
}
