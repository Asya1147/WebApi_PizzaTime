namespace WebApi_PizzaTime
{
    public class Pizza
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public double price { get; set; }

        public Pizza(int id, string name, double price) {
            this.id = id; this.name = name; this.price= price;
        }

    }
}
