using System;
using System.Collections.Generic;

namespace IncityClient.Common.Models
{
    public class OrderDetailResponse
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ICollection<ItemResponse> Items { get; set; }
    }
}
