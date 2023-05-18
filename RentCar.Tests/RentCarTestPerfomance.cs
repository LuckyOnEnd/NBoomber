using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Tests
{
    public class RentCarTestPerfomance
    {
        [Fact]
        public void get_cars_should_handle_at_least_100_request_per_second()
        {
            const string url = "https://localhost:7097/cars";

            var getCarsStep = Step.Create("get cars", clientFactory: HttpClientFactory.Create(), async context =>
            {
                try
                {
                    var resuest = Http.CreateRequest("GET", url);

                    return await Http.Send(resuest, context);
                }
                catch (Exception ex)
                {
                    return Response.Fail();
                }
            });

            var scenario = ScenarioBuilder.CreateScenario("Videos API fetch", getCarsStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(Simulation.KeepConstant(1000, during: TimeSpan.FromSeconds(5)));

            NBomberRunner
                .RegisterScenarios(scenario)
                .Run();
        }
    }
}
