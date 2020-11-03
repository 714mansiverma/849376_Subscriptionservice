using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubscriptionService.Models;
namespace SubscriptionService.Repository
{
   public interface ISubscribeDrugs
    {
        SubscriptionDetails PostSubscribe(PrescriptionDetails subscription);
        SubscriptionDetails PostUnSubscribe(int unsubscribe);
    }
}
