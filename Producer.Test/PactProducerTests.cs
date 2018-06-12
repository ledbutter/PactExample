using Microsoft.Owin.Hosting;
using NUnit.Framework;
using PactNet;

namespace Producer.Test
{
    [TestFixture]
    public class PactProducerTests
    {
        [Test]
        public void EnsureTheThingHonorsPactWithConsumer()
        {
            // arrange
            const string serviceUri = "http://localhost:9224/";
            var config = new PactVerifierConfig
            {
                //Outputters = new List<IOutput> //NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output, so a custom outputter is required.
                //{
                    
                //    //new XUnitOutput(_output)
                //},
                //CustomHeader = new KeyValuePair<string, string>("Authorization", "Basic VGVzdA=="), //This allows the user to set a request header that will be sent with every request the verifier sends to the provider
                Verbose = true //Output verbose verification logs to the test output
            };

            using (WebApp.Start<TestStartup>(serviceUri))
            {
                var pactVerifier = new PactVerifier(config);
                pactVerifier
                    .ProviderState($"{serviceUri}/provider-states")
                    .ServiceProvider("PactProducer", serviceUri)
                    .HonoursPactWith("PactConsumer")
                    .PactUri(@"C:\temp\pact\pactDir\pactconsumer-pactproducer.json")
                    .Verify();
            }
        }
    }
}