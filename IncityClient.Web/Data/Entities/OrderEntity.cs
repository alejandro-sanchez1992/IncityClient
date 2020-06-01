using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime DateLocal => Date.ToLocalTime();

        public double Total { get; set; }

        public string Remarks { get; set; }

        public PlaceEntity Place { get; set; }

        public CustomerEntity Customer { get; set; }

        public StatusEntity Status { get; set; }

        public ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}
