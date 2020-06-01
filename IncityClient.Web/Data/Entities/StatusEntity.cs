using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Data.Entities
{
    public class StatusEntity
    {
        public int Id { get; set; }

        [Display(Name = "Status Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }
}
