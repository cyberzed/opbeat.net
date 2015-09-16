using opbeat.Core.ErrorsModels;

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

        internal string ToJson()
        {
            using (var serializer = new Serializer())
            {
                serializer.Write("rev", CommitHash);
                serializer.Write("status", Status);
                serializer.Write("branch", Branch);
                serializer.Write("machine", MachineName);

                return serializer.ToJson();
            }
        }
    }
}