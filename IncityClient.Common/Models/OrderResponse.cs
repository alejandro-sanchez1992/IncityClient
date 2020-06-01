using System;
using System.Collections.Generic;

namespace IncityClient.Common.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Total { get; set; }

        public string Remarks { get; set; }

        public string Place { get; set; }

        public string Customer { get; set; }

        public string Status { get; set; }

        public ICollection<OrderDetailResponse> OrderDetails { get; set; }
    }
}
