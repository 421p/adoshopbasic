using System;
using AdoShop.Entity;
using AdoShop.Entity.User;
using AdoShop.Factory;
using AdoShop.Utils;

namespace AdoShop.Faker {
    public static class Faker {
        public static void InjectEntities(ShopContext model)
        {
            var alina = new User {
                Name = "Alina",
                Password = Aes.Encrypt("alina", UserSalts.Default),
                Role = UserRole.Manager
            };

            var ira = new User {
                Name = "Ira",
                Password = Aes.Encrypt("ira", UserSalts.Default),
                Role = UserRole.Operator
            };
            model.Users.Add(alina);
            model.Users.Add(ira);

            var drinks = new Category {
                Name = "Drinks"
            };

            var food = new Category {
                Name = "Food"
            };

            model.Categories.Add(drinks);
            model.Categories.Add(food);

            model.SaveChanges();

            var nestle = new Supplier {
                Name = "Nestle"
            };

            var roshen = new Supplier {
                Name = "Roshen"
            };

            var stolichny = new Supplier {
                Name = "Ринок \"Столичний\""
            };

            model.Suppliers.Add(nestle);
            model.Suppliers.Add(roshen);
            model.Suppliers.Add(stolichny);

            model.SaveChanges();

            var kompot = new Good {
                Name = "Kompot",
                SupplierId = nestle.Id,
                CategoryId = drinks.Id,
                Price = 10,
                Count = 100
            };

            var salo = new Good {
                Name = "Salo Svynyache",
                SupplierId = stolichny.Id,
                CategoryId = food.Id,
                Price = 150,
                Count = 200
            };

            var smetana = new Good {
                Name = "Smetana",
                SupplierId = stolichny.Id,
                CategoryId = food.Id,
                Price = 25,
                Count = 200
            };

            var cheburek = new Good {
                Name = "Cheburek",
                SupplierId = stolichny.Id,
                CategoryId = food.Id,
                Price = 15,
                Count = 100
            };

            var candy = new Good {
                Name = "Candy \"Tuzik\"",
                SupplierId = roshen.Id,
                CategoryId = food.Id,
                Price = 150,
                Count = 200
            };

            var cake = new Good {
                Name = "Торт \"Київський\"",
                SupplierId = roshen.Id,
                CategoryId = food.Id,
                Price = 450,
                Count = 30
            };

            var kvas = new Good {
                Name = "Квас \"Тарас\"",
                SupplierId = nestle.Id,
                CategoryId = drinks.Id,
                Price = 30,
                Count = 150
            };

            var sweetSalo = new Good {
                Name = "Сало в шоколаді",
                SupplierId = roshen.Id,
                CategoryId = drinks.Id,
                Price = Convert.ToDecimal(39.99),
                Count = 150
            };

            var kebab = new Good {
                Name = "Люля-кебаб",
                SupplierId = stolichny.Id,
                CategoryId = drinks.Id,
                Price = Convert.ToDecimal(95.50),
                Count = 150
            };

            model.Goods.Add(kompot);
            model.Goods.Add(cheburek);
            model.Goods.Add(candy);
            model.Goods.Add(salo);
            model.Goods.Add(smetana);
            model.Goods.Add(kvas);
            model.Goods.Add(cake);
            model.Goods.Add(sweetSalo);
            model.Goods.Add(kebab);

            model.SaveChanges();

            var order = OrderFactory.CreateOrder(ira, new[] {
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