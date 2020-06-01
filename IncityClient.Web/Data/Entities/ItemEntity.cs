using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Data.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public double Price { get; set; }

        [DefaultValue(0)]
        public double Discount { get; set; }

        public string Remarks { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://myveterinary.azurewebsites.net{ImageUrl.Substring(1)}";

        public ItemTypeEntity ItemType { get; set; }
    }
}
