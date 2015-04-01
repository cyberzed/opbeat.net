using System.Collections.Generic;

namespace opbeat.Core.ErrorsModels
{
    public class StackTrace
    {
        public List<Frame> Frames { get; set; }
    }
}