using System;
using backend.Entity;
using backend.Entity.User;
using backend.Factory;

namespace backend.Faker {
    public static class Faker {
        public static void InjectEntities(ShopContext model)
        {
            var user = new User {
                Name = "Alina",
                Password = "alina",
                Role = UserRole.Manager
            };
            model.Users.Add(user);

            model.Categories.Add(new Category {
                Name = "Drinks"
            });

            model.Suppliers.Add(new Supplier {
                Name = "Coca-Cola"
            });

            model.SaveChanges();

            var kompot = new Good {
                Name = "Kompot",
                SupplierId = 1,
                CategoryId = 1,
                Price = 10,
                Count = 100
            };

            model.Goods.Add(kompot);

            model.SaveChanges();

            var order = OrderFactory.CreateOrder(user, new[] {
                new OrderRelatedGoods {
                    Good = kompot,
                    Count = 5
                }
            });

            model.Orders.Add(order);

            model.SaveChanges();
        }
    }
}