using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Repository;
using SubscriptionService.Models;
namespace SubscriptionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
       
        ISubscribeDrugs subscribeDrugs;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscribeController));
        public SubscribeController(ISubscribeDrugs subscribeDrugs1)
        {
            subscribeDrugs = subscribeDrugs1;
        }
        [HttpPost]

        public SubscriptionDetails PostSubscribe(PrescriptionDetails details)
        {
            _log4net.Info("Subscription Request is raised from client side and input is prescription details");
            return subscribeDrugs.PostSubscribe(details);

        }
     
        [HttpPost("{id}")]
        public SubscriptionDetails PostUnsubscribe(int id)
        {
            _log4net.Info("UnSubscribe Request is raised from client side and input is Subscription id");
            return subscribeDrugs.PostUnSubscribe(id);
        }
        
        
    }
}
