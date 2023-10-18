using MySql.Data.MySqlClient;
using StreamingManagement.Models.dto.Authorization;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace StreamingManagement.Utils.Consultas.Auth
{
    public class FunctionPLSQL
    {
        private readonly MySqlConnection _connection;
        private readonly IConfiguration _config;
        
        public FunctionPLSQL()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _connection = new MySqlConnection(_config.GetValue<string>("ConnectionStrings:AUTH"));

        }

        private void openConection() { 
            _connection.Open();
        }

        private void closeConection()
        {
            _connection.Close();
        }

        public AuthenticatorRes validarInicioSesion(User user)
        {
            try { 
                this.openConection();

                using (var command = new MySqlCommand("validarSesion", _connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@user_i", user.username);
                    command.Parameters.AddWithValue("@pass", user.password);

                    MySqlParameter ret_param = new MySqlParameter("@return_value", MySqlDbType.String);
                    ret_param.Direction = System.Data.ParameterDirection.ReturnValue;

                    command.Parameters.Add(ret_param);

                    command.ExecuteNonQuery();

                    string res = ret_param.Value.ToString();

                    String[] val = res.Split(',');
                    return new AuthenticatorRes { 
                        authId = Int32.Parse(val[0]),
                        rolname = val[1]    

                    };

                }


            }
            catch (Exception ex) {
                return new AuthenticatorRes { authId = 0, rolname = ex.Message };
            }
        }

    }
}
