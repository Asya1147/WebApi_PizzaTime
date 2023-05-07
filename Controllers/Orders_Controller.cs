using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_PizzaTime.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Orders_Controller : ControllerBase
    {
        public List<Orders> list_orders = new List<Orders>();
        string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\ASYA\\Desktop\\WebApi_PizzaTime\\DataBase\\MyDataBase.mdf; Integrated Security = True; Connect Timeout = 30";
        // GET: <Orders_Controller>
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
            for (int i = 0; i < list_orders.Count; i++)
            {
                jsonstring += JsonSerializer.Serialize<Orders>(list_orders[i], options);
                if (i < list_orders.Count - 1 ) { jsonstring += ","; }
            }
            jsonstring += "]";
            //ConnectionStrings: MyDataBase
            return jsonstring;
        }

        // GET <Orders_Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        async void ReadBase()
        {

            list_orders.Clear();
           

            string sqlExpression = "SELECT * FROM Orders";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Orders p = new Orders(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), Convert.ToDouble(reader.GetValue(2)));
                        list_orders.Add(p);
                    }
                }
                reader.Close();
            }
        }
        //POST<Orders_Controller>
       //[HttpPost]
       // public void Post(int id, string customer, double summa)
       // {
       //     string text = $"Insert Into OrdersPizza (id, customer, summa) Values({id}, {customer}, {summa})";
       //     //Orders_Pizza p = new Orders_Pizza(id, idOrder, idPizza, count);

       //     SqlConnection sql_conn = new SqlConnection(connectionString);
       //     SqlCommand sql_comm = new SqlCommand(text, sql_conn);

       //     sql_conn.Open(); // открыть соединение
       //     sql_comm.ExecuteNonQuery(); // выполнить команду на языке SQL
       //     sql_conn.Close(); // закрыть соединение

       //     Get();
       // }

        //// PUT <Orders_Controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE <Orders_Controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
