using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace Consumer.Test
{
    [TestFixture]
    public class PactConsumerTests
    {
        private IPactBuilder _pactBuilder;
        private IMockProviderService _mockProviderService;

        private const int MockServerPort = 9225;
        private static string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _pactBuilder = new PactBuilder(new PactConfig{PactDir = @"c:\temp\pact\pactDir", LogDir = @"c:\temp\pact\logs"});

            _pactBuilder
                .ServiceConsumer(nameof(PactConsumer))
                .HasPactWith("PactProducer");

            _mockProviderService = _pactBuilder.MockService(MockServerPort);
            _mockProviderService.ClearInteractions();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _pactBuilder.Build();
        }

        [Test]
        public async Task DoTheThing_Succeeds_WhenProvidedId()
        {
            // arrange
            const string id = "foo";
            const string expectedResponse = "bar";

            _mockProviderService
                .Given($"There is a thing with id '{id}'")
                .UponReceiving("A GET request to retrieve the thing")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/api/theThing/{id}",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object> // required
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        Value = expectedResponse
                    }
                });

            var consumer = new PactConsumer(MockProviderServiceBaseUri);

            // act
            var result = await consumer.DoTheThingAsync(id);

            // assert
            Assert.AreEqual(expectedResponse, result);
            _mockProviderService.VerifyInteractions();
        }
    }
}
