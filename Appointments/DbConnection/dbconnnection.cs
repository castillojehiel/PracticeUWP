using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace Appointments
{
    public class dbconnnection
    {

        static SqlConnection conn = new SqlConnection();

        //DataTable dt = new DataTable();
        static string connString = null;
        static string server, dbname, dbuser, dbpass;

        //constructor
        public dbconnnection()
        {

        }


        //open connection to db
        public static void connOpen()
        {
            //use if connection authentication is sql authenticate
            /*
            server = "192.168.1.200";
            dbname = "USLS";
            dbuser = "sa";
            dbpass = "Hybrain2018";
            connString = "Data Source=" + server + ";Initial Catalog =" + dbname + ";User ID =" + dbuser +
                            ";Password=" + dbpass + "";// +192.168.1.200; Initial Catalog=USLS;User ID=sa;Password=Hybrain2018";
            conn = new SqlConnection(connString);
            conn.Open();
            */

            //use if authentication is windows authenticate
            connString = "Data Source=CASTILLO\\SQLEXPRESS;Initial Catalog=mockupAppoinments;Integrated Security=False;Persist Security Info=True;User ID=userAppTest;Password="+ "pass123"+"";
            conn = new SqlConnection(connString);
            conn.Open();
        }

        /// <summary>
        /// close connection to db
        ///</summary>
        private static void connClose(){
            conn.Close();
        }

         /// <summary>
        /// execute non query (to insert or update data for db)
        /// </summary>
        /// <param name="cmd">partial sql command from controller</param>
        /// <returns></returns>
        public static bool ExecNonQuery(SqlCommand cmd)
        {
            try
            {
                bool result;
                //connection.Open();
                connOpen();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                //connection.Close();
                connClose();
                return result;
            }
            catch(Exception ex)
            {
                message(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// execute non scalar (to insert and get the primary key or return value.)
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ExecuteScalar(SqlCommand cmd)
        {
            try
            {
                int result;
                connOpen();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                connClose();
                return result;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                message(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// to read all the data with parameters
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                connOpen();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                connClose();
                return dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                message(ex.ToString());
                return dt;
            }
        }


        /// <summary>
        /// to read data without parameters
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteReaderNoParams(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                connOpen();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                connClose();
                return dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                message(ex.ToString());
                return dt;
            }
        }

        private async static void message(string err)
        {
            MessageDialog cd = new MessageDialog(err);
            await cd.ShowAsync();
        }



        //end of the public class
    }
}
