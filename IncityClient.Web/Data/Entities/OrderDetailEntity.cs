using System;
using System.Collections.Generic;

namespace IncityClient.Web.Data.Entities
{
    public class OrderDetailEntity
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public OrderEntity Order { get; set; }

        public ICollection<ItemEntity> Items { get; set; }
    }
}
