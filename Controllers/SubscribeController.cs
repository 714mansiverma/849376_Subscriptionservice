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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
       
        ISubscribeDrugs subscribeDrugs;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscribeController));
        
        public SubscribeController(ISubscribeDrugs subscribeDrugs1)
        {
            subscribeDrugs = subscribeDrugs1;
        }
        
        [HttpPost("{PolicyDetails}/{MemberId}")]
        public IActionResult PostSubscribe([FromBody]PrescriptionDetails details,[FromRoute] string PolicyDetails, int MemberId)
        {
            _log4net.Info("Subscription Request is raised from client side and input is prescription details");
            return Ok(subscribeDrugs.PostSubscribe(details,PolicyDetails,MemberId));
        }
     
        [HttpPost("{MemberId}/{SubscriptionId}")]
        public IActionResult PostUnsubscribe([FromRoute]int MemberId,int SubscriptionId)
        {
            _log4net.Info("UnSubscribe Request is raised from client side and input is Subscription id");
            return Ok(subscribeDrugs.PostUnSubscribe(MemberId, SubscriptionId));
        }
        
    }
}
