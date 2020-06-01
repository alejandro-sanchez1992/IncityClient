using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Data.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }

        public UserEntity User { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }
}
