using System;
namespace IncityClient.Common.Models
{
    public class ItemResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public string Remarks { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://incityapp.azurewebsites.net{ImageUrl.Substring(1)}";

        public string ItemType { get; set; }
    }
}
