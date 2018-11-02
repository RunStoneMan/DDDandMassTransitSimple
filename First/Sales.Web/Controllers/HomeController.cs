using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Sales.Message.Commands;
using Sales.Web.Models;

namespace Sales.Web.Controllers
{
    public class HomeController : Controller
    {

        IBusControl provider;
        static int messagesSent;

        public HomeController(IBusControl bus)
        {
            provider = bus;
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var uId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceOrder { UserId = uId, createTime = System.DateTime.Now, ProductIds = new List<string> { "abc", "efd", "xyz" } };

            //provider.GetSendEndpoint();
            // Send the command
            await provider.GetSendEndpoint(new Uri("rabbitmq://localhost/order-service")).Result.Send(command);

            dynamic model = new ExpandoObject();
            model.OrderId = uId;
            model.MessagesSent = Interlocked.Increment(ref messagesSent);

            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
