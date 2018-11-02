using MassTransit;
using Sales.Message.Commands;
using Sales.Message.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.CommandsProcess.Orders
{
    public class PlaceOrderConsumers : IConsumer<PlaceOrder>
    {
        public IBusControl _bus { set; get; }
        public PlaceOrderConsumers(IBusControl bus)
        {
            _bus = bus;
        }
   

        public async Task Consume(ConsumeContext<PlaceOrder> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"Receive message: {context.Message.createTime}{context.Message.UserId}");
            OrderCreated orderCreated = new OrderCreated { EventCreatedTime=System.DateTime.Now, UserId=context.Message.UserId , OrderId=Guid.NewGuid().ToString()};

           await _bus.Publish(orderCreated);
            Console.ResetColor();
        }
    }
}
