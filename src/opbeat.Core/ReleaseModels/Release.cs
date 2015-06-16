namespace opbeat.Core.ReleaseModels
{
    public class Release
    {
        public string CommitHash { get; private set; }
        public ReleaseStatus Status { get; private set; }
        public string Branch { get; private set; }
        public string MachineName { get; private set; }

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
    }
}