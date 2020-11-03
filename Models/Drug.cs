using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Models
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime EpiryDate { get; set; }
        public string ManufacturerName { get; set; }
        
        public int DrugId { get; set; }

    }
}
