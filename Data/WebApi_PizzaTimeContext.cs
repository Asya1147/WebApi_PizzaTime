using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi_PizzaTime;

namespace WebApi_PizzaTime.Data
{
    public class WebApi_PizzaTimeContext : DbContext
    {
        public WebApi_PizzaTimeContext (DbContextOptions<WebApi_PizzaTimeContext> options)
            : base(options)
        {
        }

        public DbSet<WebApi_PizzaTime.Pizza> Pizza { get; set; } = default!;
    }
}
