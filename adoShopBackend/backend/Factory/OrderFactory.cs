using System.Collections.Generic;
using backend.Entity;
using backend.Entity.User;

namespace backend.Factory
{
    public class OrderFactory
    {
        public static Order CreateOrder(User user, IEnumerable<OrderRelatedGoods> goodCountPairs)
        {
            var order = new Order {User = user};

            foreach (var pair in goodCountPairs) {
                var good = pair.Good;
                pair.Sum = pair.Count * good.Price;

                good.Count -= pair.Count;

                order.Goods.Add(pair);
            }

            return order;
        }
    }
}
