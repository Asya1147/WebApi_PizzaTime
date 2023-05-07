namespace WebApi_PizzaTime
{
    public class Orders_Pizza
    {
        public int id { get; set; }
        public int id_order { get; set; }
        public int id_pizza { get; set; }
        public int count { get; set; }

        public Orders_Pizza(int id, int id_order, int id_pizza, int count )
        { this.id = id; this.id_order = id_order; this.id_pizza = id_pizza; this.count = count; }

    }
}
