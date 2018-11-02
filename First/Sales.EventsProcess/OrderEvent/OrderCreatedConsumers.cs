using MassTransit;
using Sales.Message.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.EventsProcess.OrderEvent
{
    public class OrderCreatedConsumers : IConsumer<OrderCreated>
    {
        public IBusControl _bus { set; get; }
        public OrderCreatedConsumers(IBusControl bus)
        {
            _bus = bus;
        }


        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"Receive Event message: {context.Message.OrderId}");
            Console.ResetColor();
        }
    }
}
