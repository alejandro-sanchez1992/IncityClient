using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Data.Entities
{
    public class PlaceEntity
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        public double DeliveryCost { get; set; }

        public bool IsOpen { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Phone { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string City { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://myveterinary.azurewebsites.net{ImageUrl.Substring(1)}";

        public AffiliateEntity Affiliate { get; set; }

        public ICollection<ItemEntity> Items { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }

}
