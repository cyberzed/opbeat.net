namespace opbeat.Core
{
    public class Release
    {
        public string Rev { get; set; }
        public ReleaseStatus Status { get; set; }

        public string Branch { get; set; }
        public string MachineName { get; set; }
    }
}