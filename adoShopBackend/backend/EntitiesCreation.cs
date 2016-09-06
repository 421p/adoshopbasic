using backend.Entity;
using backend.Entity.User;
using static System.Configuration.ConfigurationManager;

namespace backend
{
    //temporary class just for testing
    public static class Entities
    {
        public static void Create()
        {
            using (ShopContext model = new ShopContext(ConnectionStrings["shop_ado"].ConnectionString))
            {
                User user = new User
                {
                    Name = "Alina",
                    Password = "alina",
                    Role = UserRole.Manager
                };
                model.Users.Add(user);

                model.Categories.Add(new Category()
                {
                    Name = "Drinks"
                });

                model.Suppliers.Add(new Supplier()
                {
                    Name = "Coca-Cola"
                });

                model.SaveChanges();

                Good kompot = new Good()
                {
                    Name = "Kompot",
                    SupplierId = 1,
                    CategoryId = 1,
                    Price = 10,
                    Count = 1
                };

                model.Goods.Add(kompot);

                model.SaveChanges();

                Order order = new Order()
                {
                    User = user
                };

                OrderRelatedGoods good = new OrderRelatedGoods()
                {
                    Good = kompot,
                    Count = 6
                };
                good.Sum = OrderFactory.CalculateGoodsSum(good);
                

                order.Goods.Add(good);

                model.Orders.Add(order);

                model.SaveChanges();
            }
        }
    }
}
