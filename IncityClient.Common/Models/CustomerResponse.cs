using System;
using System.Collections.Generic;

namespace IncityClient.Common.Models
{
    public class CustomerResponse : UserResponse
    {
        public ICollection<OrderResponse> Orders { get; set; }
    }
}
