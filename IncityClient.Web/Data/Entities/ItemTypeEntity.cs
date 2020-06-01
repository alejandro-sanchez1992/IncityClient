using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Data.Entities
{
    public class ItemTypeEntity
    {
        public int Id { get; set; }

        [Display(Name = "Type Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<ItemEntity> Items { get; set; }
    }
}
