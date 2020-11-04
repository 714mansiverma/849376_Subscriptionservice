using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SubscriptionService.Models;
using SubscriptionService.Controllers;
namespace SubscriptionService.Repository
{
    public class SubscribeDrugs : ISubscribeDrugs
    {
        List<SubscriptionDetails> details;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscribeController));
        public SubscribeDrugs()
        {
            details = new List<SubscriptionDetails>() {
            new SubscriptionDetails{ SubscriptionDate= Convert.ToDateTime("2020-12-01 01:01:00 AM"), Status=true, RefillOccurrence="monthly", Id=1, MemberId=101, MemberLocation="Delhi", PrescriptionId=201},
              new SubscriptionDetails{ SubscriptionDate= Convert.ToDateTime("2020-12-01 01:01:00 AM"), Status=true, RefillOccurrence="weekly", Id=2, MemberId=102, MemberLocation="Lucknow", PrescriptionId=202}
            };
        }
        public SubscriptionDetails PostSubscribe(PrescriptionDetails prescription)
        {
            _log4net.Info("DruApi si being called to check for the availability of the particular drug");
            // Drug drug = new Drug() { DrugId = 1, EpiryDate = new DateTime(1999, 12, 20), Id = 1, ManufactureDate = Convert.ToDateTime("2020-12-01 01:01:00 AM"), ManufacturerName = "XYZ", Name = "Paracetamol" };

              var drugs = "";
              var query = prescription.DrugName;
              HttpClient client = new HttpClient();
              HttpResponseMessage result = client.GetAsync("https://localhost:44393/api/DrugsApi/GetByName/"+query ).Result;
              if (result.IsSuccessStatusCode)
              {
                  drugs = result.Content.ReadAsStringAsync().Result;

              }
            if (drugs != "")
            {
                _log4net.Info("Drug Available");
                return new SubscriptionDetails { Id = 1, MemberId = 1, MemberLocation = "Delhi", PrescriptionId = prescription.Id, RefillOccurrence = "weekly", Status = true, SubscriptionDate = Convert.ToDateTime("2020-12-01 01:01:00 AM") };
            }
            else
            {
                _log4net.Info("Drug NotAvailable");
                return new SubscriptionDetails { Id = 0, MemberId = 0, MemberLocation = "", PrescriptionId = 0, RefillOccurrence = "", Status = false, SubscriptionDate = Convert.ToDateTime("2020-12-01 01:01:00 AM") };
            }
        }
        public SubscriptionDetails PostUnSubscribe(int id)
        {

            // Get the data from refill microservice 
            _log4net.Info("Checking for Subscription ");


            var subs = details.Find(p => p.Id == id);
            if(subs!=null)    
            {
                    _log4net.Info("interacting with refill microservice for the payment status of the partiular subscription id ");
                    RefillOrderDetails refill = new RefillOrderDetails() { Id = 1, DrugQuantity = 20, Payment = true, RefillDate = Convert.ToDateTime("2020-12-01 01:01:00 AM"), RefillDelivered = true };
                    if (refill.Id == id && refill.Payment == true)
                    {
                        _log4net.Info("Unsubscribe successfull ");
                        return new SubscriptionDetails { Id = id, MemberId = subs.MemberId, MemberLocation = subs.MemberLocation, PrescriptionId = subs.PrescriptionId, RefillOccurrence = subs.RefillOccurrence, Status = true, SubscriptionDate = subs.SubscriptionDate };

                    }
                    else
                    {
                    return new SubscriptionDetails { Id = id, MemberId = subs.MemberId, MemberLocation = subs.MemberLocation, PrescriptionId = subs.PrescriptionId, RefillOccurrence = subs.RefillOccurrence, Status = false, SubscriptionDate = subs.SubscriptionDate };
                    }
            }
            
            _log4net.Info("Payment Due! To Unscription please clear your due payments ");
            return new SubscriptionDetails { Id = 0 , MemberId = 0, MemberLocation = "", PrescriptionId =0, RefillOccurrence = "", Status = false, SubscriptionDate = Convert.ToDateTime("2020-12-01 01:01:00 AM") };

        }
    }
}
