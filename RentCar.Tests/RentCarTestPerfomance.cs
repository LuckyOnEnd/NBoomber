using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace RentCar.Tests
{
    public class RentCarTestPerfomance
    {
        ITestOutputHelper _output;
        public RentCarTestPerfomance(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void get_cars_should_handle_at_least_100_request_per_second()
        {
            const string url = "https://localhost:7097/cars";

            var getCarsStep = Step.Create("get cars", clientFactory: HttpClientFactory.Create(), async context =>
            {
                try
                {
                    //typ http request'u "GET"
                    var resuest = Http.CreateRequest("GET", url);
                    return await Http.Send(resuest, context);
                }
                catch (Exception ex)
                {
                    return Response.Fail();
                }
            });

            const int expectedRequestPerSecond = 100;
            const int durationInSeconds = 5;

            var scenario = ScenarioBuilder.CreateScenario("Cars API test", getCarsStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(Simulation.KeepConstant(100, during: TimeSpan.FromSeconds(durationInSeconds)));

           var stats =  NBomberRunner
                .RegisterScenarios(scenario) 
                .Run();

            _output.WriteLine($"Udało się wykonać {stats.OkCount} testów \n Nie udało się wykonać {stats.FailCount} testów");

            stats.OkCount.ShouldBeGreaterThanOrEqualTo(durationInSeconds * expectedRequestPerSecond);
        }
    }
}
