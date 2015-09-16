using opbeat.Core.ErrorsModels;
using opbeat.Core.ReleaseModels;
using Xunit;

namespace opbeat.Core.Tests
{
    public class OpbeatClientWorkbench
    {
        [ExplicitFact]
        public void SendTestRelease()
        {
            var configuration = new OpbeatConfiguration
                                {
                                    AccessToken = "0d85cde89fadf8d18c057dd9572b6a2579aaa85e",
                                    ApplicationId = "554e269e6f",
                                    OrganizationId = "42173c5126aa4fdf8acdf368c8555f7c"
                                };

            var client = new OpbeatClient(configuration);

            var release = new Release(Sha1Generator.RandomString());

            var response = client.Send(release);

            Assert.Equal(ServiceResponse.Success, response);
        }

        [ExplicitFact]
        public void SendTestError()
        {
            var configuration = new OpbeatConfiguration
                                {
                                    AccessToken = "0d85cde89fadf8d18c057dd9572b6a2579aaa85e",
                                    ApplicationId = "554e269e6f",
                                    OrganizationId = "42173c5126aa4fdf8acdf368c8555f7c"
                                };

            var client = new OpbeatClient(configuration);

            var error = new Error("Input 42");

            var response = client.Send(error);

            Assert.Equal(ServiceResponse.Success, response);
        }
    }
}