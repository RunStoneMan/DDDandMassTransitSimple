using Autofac;
using MassTransit;
using Sales.EventsProcess.OrderEvent;
using System;

namespace Sales.EventsProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MassTransit Server";

            var builder = new ContainerBuilder();

            // register a specific consumer
            builder.RegisterType<OrderCreatedConsumers>();

            // just register all the consumers
            //builder.RegisterConsumers(Assembly.GetExecutingAssembly());
            builder.Register(context =>
            {
                var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost"), hst =>
                    {
                        hst.Username("guest");
                        hst.Password("guest");
                    });

                    cfg.ReceiveEndpoint(host, "Events", ec =>
                    {
                        // if only one consumer in the consumer for this queue
                        ec.LoadFrom(context);

                        // otherwise, be smart, register explicitly
                        // ec.Consumer<UpdateCustomerConsumer>(context);
                    });
                });
                return bus;

            }).SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            var container = builder.Build();

            var bc = container.Resolve<IBusControl>();
            bc.Start();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            bc.Stop();


        }
    }
}
