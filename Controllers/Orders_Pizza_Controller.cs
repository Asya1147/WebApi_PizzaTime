using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_PizzaTime.Controllers
{
    

    [Route("[controller]")]
    [ApiController]
    public class Orders_Pizza_Controller : ControllerBase
    {
        public List<Orders_Pizza> list_op = new List<Orders_Pizza>();
        string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\ASYA\\Desktop\\WebApi_PizzaTime\\DataBase\\MyDataBase.mdf; Integrated Security = True; Connect Timeout = 30";
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;

        // GET: <Orders_Pizza_Controller>
        [HttpGet]
        public string Get()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            ReadBase();

            string jsonstring = "[";
            for (int i = 0; i < list_op.Count; i++)
            {
                jsonstring += JsonSerializer.Serialize<Orders_Pizza>(list_op[i], options);
                if (i < list_op.Count - 1) { jsonstring += ","; }
            }
            jsonstring += "]";
            return jsonstring;
        }

        // GET <Orders_Pizza_Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST <Orders_Pizza_Controller>
        [HttpPost]
        public async void Post( int idPizza,  int count, string customer)
        {
            string text; SqlConnection sql_conn; SqlCommand sql_comm;
  
            sql_conn = new SqlConnection(connectionString);

            text = $"Insert Into Orders VALUES ('{customer}', (select price*{count} from Pizza where id={idPizza}))";

            sql_comm = new SqlCommand(text, sql_conn);

            sql_conn.Open(); // открыть соединение
            sql_comm.ExecuteNonQuery(); // выполнить команду на языке SQL
            sql_conn.Close(); // закрыть соединение


            text = $"Insert Into OrdersPizza Values((SELECT MAX(id) FROM Orders), {idPizza},  {count})";
            //Orders_Pizza p = new Orders_Pizza(id, idOrder, idPizza, count);
            
            sql_comm = new SqlCommand(text, sql_conn);

            sql_conn.Open(); // открыть соединение
            sql_comm.ExecuteNonQuery(); // выполнить команду на языке SQL
            sql_conn.Close(); // закрыть соединение

            
            Get();
        }

        // PUT <Orders_Pizza_Controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE <Orders_Pizza_Controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        async void ReadBase()
        {

            list_op.Clear();
            string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\ASYA\\Desktop\\WebApi_PizzaTime\\DataBase\\MyDataBase.mdf; Integrated Security = True; Connect Timeout = 30";

            string sqlExpression = "SELECT * FROM OrdersPizza";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Orders_Pizza p = new Orders_Pizza(Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), Convert.ToInt32(reader.GetValue(2)), Convert.ToInt32(reader.GetValue(3)));
                        list_op.Add(p);
                    }
                }
                reader.Close();
            }
        }
    }
}
