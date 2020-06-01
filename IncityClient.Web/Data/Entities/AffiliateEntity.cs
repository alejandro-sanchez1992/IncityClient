using System;
using System.Collections.Generic;

namespace IncityClient.Web.Data.Entities
{
    public class AffiliateEntity
    {
        public int Id { get; set; }

        public UserEntity User { get; set; }

        public ICollection<PlaceEntity> Places { get; set; }
    }
}
