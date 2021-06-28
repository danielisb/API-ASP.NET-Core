using System;
using System.ComponentModel.DataAnnotations;

namespace challenge_OLX.Models
{
    public class Imoveis
    {
        [Key]
        //public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]

        // public string Title { get; set; }

        public int usableAreas { get; set; }
        public string listingType { get; set; }
        public string createdAt { get; set; }


        public string listingStatus { get; set; }
        public string id { get; set; }
        public int parkingSpaces { get; set; }
        public string updatedAt { get; set; }
        public bool owner { get; set; }

        //public List<string> images        { get; set; }

        //public struct Adress // ?
        //{
        //    public string City         { get; set; }
        //    public string neighborhood { get; set; }
        //    // geoLocation

        //        public string precision { get; set; }
        //        // location
        //            public float lon { get; set; }
        //            public float lat { get; set; }
        //}; //

        public int bathrooms { get; set; }
        public int bedrooms { get; set; }

        // pricingInfos
        //public double yearlyIptu     { get; set; }
        //public double price          { get; set; }
        //public string businessType   { get; set; }
        //public float monthlyCondoFee { get; set; }

    }
}
