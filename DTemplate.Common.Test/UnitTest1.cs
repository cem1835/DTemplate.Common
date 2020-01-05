using MassTransit.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DTemplate.Common.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //var harness = new InMemoryTestHarness();
            //var consumerHarness = harness.Consumer<MyConsumer>();

            //await harness.Start();
            //try
            //{
            //    await harness.InputQueueSendEndpoint.Send(new MyMessage());

            //    // did the endpoint consume the message
            //    Assert.IsTrue(harness.Consumed.Select<MyMessage>().Any());

            //    // did the actual consumer consume the message
            //    Assert.IsTrue(consumerHarness.Consumed.Select<MyMessage>().Any());
            //}
            //finally
            //{
            //    await harness.Stop();
            //}
        }
    }
}
