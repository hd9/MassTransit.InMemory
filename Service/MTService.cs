using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Transports.InMemory;

namespace HildenCo.MassTransit.InMemory
{
    public class MTService
    {
        private IBusControl bus;
        //private IBusControl bus;

        public void Init()
        {
            Console.WriteLine("Starting Backend...");

            IInMemoryHost host = null;
            bus = Bus.Factory.CreateUsingInMemory(cfg =>
            {
                host = cfg.Host;

                cfg.ReceiveEndpoint("queue_name", e => {
                    // configures subscriber (for publish commands)
                    //e.Handler<CreateAccount>(ctx =>
                    //{
                    //    return Console.Out.WriteLineAsync($"[Publisher Msg Received] Email: {ctx.Message.Email}, Name: {ctx.Message.Name}");
                    //});

                    // configures consumer (handler)
                    e.Consumer<CreateAccountConsumer>();
                    // different params...
                    //e.Consumer<CreateAccountConsumer>(() => new CreateAccountConsumer());
                    //e.Consumer<CreateAccountConsumer>( a => {
                    //    Console.WriteLine("aha!");
                    //});
                });
            });

            bus.Start();
        }

        /// Sends a message
        public async Task Send(CreateAccount msg)
        {
            if (msg == null)
                return;

            // sends a messaged to be consumed by CreateAccountConsumer
            var sendEndpoint = await bus.GetSendEndpoint(new Uri("loopback://localhost/queue_name"));
            await sendEndpoint.Send(msg);
        }

        /// Publishes a message
        public async Task Publish(CreateAccount msg)
        {
            if (msg == null)
                return;

            // 
            //bus.Publish(msg);

            // sends a messaged to be consumed by CreateAccountConsumer
            var sendEndpoint = await bus.GetSendEndpoint(new Uri("loopback://localhost/queue_name"));
            await sendEndpoint.Send(msg);
        }


        public void ShutDown(){
            Console.WriteLine("Shutting down backend...");
        }

    }
}