using System;
using System.Collections.Generic;
using opbeat.Core.ErrorsModels;
using opbeat.Core.ReleaseModels;
using Xunit;
using Exception = opbeat.Core.ErrorsModels.Exception;

namespace opbeat.Core.Tests
{
    public class OpbeatClientWorkbench
    {
        [Fact]
        public void SendTestRelease()
        {
            var configuration = new OpbeatConfiguration
            {
                AccessToken = "0d85cde89fadf8d18c057dd9572b6a2579aaa85e",
                ApplicationId = "554e269e6f",
                OrganizationId = "42173c5126aa4fdf8acdf368c8555f7c"
            };

            var client = new OpbeatClient(configuration);

            var release = new Release(Sha1Generator.RandomString(), ReleaseStatus.Completed);

            var response = client.Send(release);

            Assert.Equal(ServiceResponse.Success, response);
        }

        [Fact]
        public void SendTestError()
        {
            var configuration = new OpbeatConfiguration
            {
                AccessToken = "0d85cde89fadf8d18c057dd9572b6a2579aaa85e",
                ApplicationId = "554e269e6f",
                OrganizationId = "42173c5126aa4fdf8acdf368c8555f7c"
            };

            var client = new OpbeatClient(configuration);

            var error = new Error("test");

            var response = client.Send(error);

            Assert.Equal(ServiceResponse.Success, response);
        }

        [Fact]
        public void SerializeRelease()
        {
            var release = new Release(Sha1Generator.RandomString(), ReleaseStatus.MachineCompleted)
            {
                Branch = "feature/type",
                MachineName = Environment.MachineName
            };

            var output = Serializer.Serialize(release);
        }

        [Fact]
        public void SerializeError()
        {
            var error = new Error("Input 42")
            {
                Culprint = "opbeat.Core.Tests.OpbeatClientWorkbench.SerilizeError",
                Exception = new Exception
                {
                    Value = "test",
                    Module = "opbeat.Core.Tests",
                    Type = "UnitTest"
                },
                Extra = new Dictionary<string, string> {{"foo", "bar"}},
                Http = new Http(),
                Level = ErrorLevel.Fatal,
                Logger = "test",
                Machine = new Dictionary<string, string>
                {
                    {"hostname", Environment.MachineName},
                    {"UserDomainName", Environment.UserDomainName}
                },
                Param_Message = "Input {0}",
                StackTrace = new StackTrace(),
                Timestamp = DateTime.UtcNow,
                User = new Dictionary<string, string> {{"horse", "man"}}
            };

            var output = Serializer.Serialize(error);
        }
    }
}