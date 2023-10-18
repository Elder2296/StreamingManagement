using Microsoft.AspNetCore.Authentication;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Data.Common;

namespace StreamingManagement.Utils.Consultas.PSS
{
    public class PSSConsulting
    {
        private readonly OracleConnection _connection;
        private readonly IConfiguration _config;

        public PSSConsulting() { 
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //_connection = new MySqlConnection(_config.GetValue<string>("ConnectionStrings:AUTH"));
            _connection = new OracleConnection(_config.GetValue<String>("ConnectionStrings:PSS"));


        }

        public DataTable? getAllSucriptions()
        {
            try { 
                this.conectionOpen();



                DataTable result = new DataTable();
                using (OracleCommand cmd = new OracleCommand("CONSULTING.get_servicios", _connection)) {

                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter resultParameter =  new OracleParameter("v_result", OracleDbType.RefCursor);
                    resultParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultParameter);

                    cmd.ExecuteNonQuery();

                    OracleDataReader reader = ((OracleRefCursor)resultParameter.Value).GetDataReader();

                    result.Load(reader);


                    /*cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {

                        
                        result.Load(reader);
                        
                    }*/

                }

                Console.WriteLine("validamos la conexion a oracle");

                this.conectionClose();
                return result;

            }
            catch (Exception ex) {
                return null;
            }
        }

        private void conectionOpen() { 
            _connection.Open();
        }

        private void conectionClose()
        {
            _connection.Close();
        }
    }
}
