using System;
using System.Threading.Tasks;
using MassTransit;

namespace HildenCo.MassTransit.InMemory
{
    public class CreateAccountConsumer
        : IConsumer<CreateAccount>
    {
        public async Task Consume(ConsumeContext<CreateAccount> ctx)
        {
            await Console.Out.WriteLineAsync($"[CONSUMER MSG RECEIVED] Email: {ctx.Message.Email}, Name: {ctx.Message.Name}");
        }
    }

}
