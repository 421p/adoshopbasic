
using System;

namespace backend.Entity
{
    public static class OrderFactory
    {
        public static void AssertGoodsCount(OrderRelatedGoods orderGoods)
        {
            if (orderGoods.Count > orderGoods.Good.Count)
                throw new Exception("Not enough goods to sell.");
        }

        public static decimal CalculateGoodsSum(OrderRelatedGoods orderGoods)
        {
            return orderGoods.Count * orderGoods.Good.Price;
        }
    }
}
