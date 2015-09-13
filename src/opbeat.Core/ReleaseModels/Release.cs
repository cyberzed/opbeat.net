using System.IO;
using Newtonsoft.Json;

namespace opbeat.Core.ReleaseModels
{
    public class Release
    {
        public string CommitHash { get; }
        public ReleaseStatus Status { get; }
        public string Branch { get; private set; }
        public string MachineName { get; }

        public Release(string commitHash)
        {
            CommitHash = commitHash;
            Status = ReleaseStatus.Completed;
        }

        public Release(string commitHash, string machineName)
        {
            CommitHash = commitHash;
            Status = ReleaseStatus.MachineCompleted;
            MachineName = machineName;
        }

        public void SetBranchName(string branch)
        {
            Branch = branch;
        }

        public string ToJson()
        {
            using (var buffer = new StringWriter())
            using (var writer = new JsonTextWriter(buffer))
            {
                writer.WriteStartObject();

                writer.WritePropertyName("rev");
                writer.WriteValue(CommitHash);

                writer.WritePropertyName("status");
                writer.WriteValue(Status);

                writer.WritePropertyName("branch");
                writer.WriteValue(Branch);

                writer.WritePropertyName("machine");
                writer.WriteValue(MachineName);

                writer.WriteEndObject();

                return buffer.ToString();
            }
        }
    }
}