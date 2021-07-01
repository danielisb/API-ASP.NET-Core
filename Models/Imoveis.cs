using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace challenge_OLX.Models
{
    public class Imoveis
    {
        [Key]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]

        public int    usableAreas   { get; set; }
        public string listingType   { get; set; }
        public string createdAt     { get; set; }
        public string listingStatus { get; set; }
        public string id            { get; set; }
        public int    parkingSpaces { get; set; }
        public string updatedAt     { get; set; }
        public bool   owner         { get; set; }

        public pricingInfos pricingInfos { get; set; }

        public int    bathrooms     { get; set; }
        public int    bedrooms      { get; set; }

        public static implicit operator Imoveis(char v)
        {
            throw new NotImplementedException();
        }
    }

    public class pricingInfos
    {
        public Guid   id               { get; set; }
        public string period           { get; set; }
        public double yearlyIptu       { get; set; }
        public double price            { get; set; }
        public double rentalTotalPrice { get; set; }
        public string businessType     { get; set; }
        public float  monthlyCondoFee  { get; set; }

        public pricingInfos()
        {
            this.id = Guid.NewGuid();
        }
    }
}
