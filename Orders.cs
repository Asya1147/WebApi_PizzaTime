namespace WebApi_PizzaTime
{
    public class Orders
    { public int id { get; set; }
        public string customer { get; set; } = "no name";
        public double summa { get; set; }

        public Orders(int id, string customer, double summa) 
        { this.id = id; this.customer = customer; this.summa = summa; }

    }
}
