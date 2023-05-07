using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Linq;
using WebApi_PizzaTime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_PizzaTime.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class Pizza_Controller : ControllerBase
    {
        public List<Pizza> list_pizza = new List<Pizza>();
        public string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\ASYA\\Desktop\\WebApi_PizzaTime\\DataBase\\MyDataBase.mdf; Integrated Security = True; Connect Timeout = 30";
        public string text; 
        public SqlConnection connection; 
        public SqlCommand command;

        // GET: <GetPizza>/all
        [HttpGet]
        [Route("all")]
        public string Get()
        {
           
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            ReadBase(); 
            
            string jsonstring="[";
            for (int i = 0; i < list_pizza.Count; i++)
            {
                jsonstring+= JsonSerializer.Serialize<Pizza>(list_pizza[i], options);
                if (i < list_pizza.Count - 1) { jsonstring += ","; }
            }
            jsonstring+= "]";
            //ConnectionStrings: MyDataBase
            return jsonstring;
        }



        //GET <GetPizza>/2
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            ReadBase();
            for (int i = 0; i < list_pizza.Count; i++) { if (list_pizza[i].id == id)
                
            {return JsonSerializer.Serialize<Pizza>(list_pizza[i], options); } }

            return "Такой пиццы не существует";
        }

        
        async void ReadBase() {
            
            list_pizza.Clear();
            
            text = "SELECT * FROM Pizza";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(text, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                   while (reader.Read()) // построчно считываем данные
                    {
                       Pizza p = new Pizza(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), Convert.ToDouble(reader.GetValue(2)));
                       list_pizza.Add(p);
                    }
                }
                reader.Close();
            }
        }


        // POST <GetPizza>
        [HttpPost]
        public void Post(string name, double price)
        {

            connection = new SqlConnection(connectionString);

            text = $"Insert Into Pizza VALUES ('{name}',{price})";

            command = new SqlCommand(text, connection);

            connection.Open(); 
            command.ExecuteNonQuery(); 
            connection.Close(); 

        }

        //// PUT <GetPizza>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}


        // DELETE <GetPizza>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
            connection = new SqlConnection(connectionString);

            text = $"DELETE FROM Pizza WHERE id={id}";

            command = new SqlCommand(text, connection);

            connection.Open(); 
            command.ExecuteNonQuery(); 
            connection.Close(); 
            
        }
    }
}
