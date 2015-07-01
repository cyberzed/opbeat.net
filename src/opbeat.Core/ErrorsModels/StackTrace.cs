using System.Collections.Generic;
using System.Linq;

namespace opbeat.Core.ErrorsModels
{
    public class StackTrace
    {
        private readonly List<Frame> frames;

        public IReadOnlyList<Frame> Frames
        {
            get { return !frames.Any() ? null : frames.AsReadOnly(); }
        }

        public StackTrace()
        {
            frames = new List<Frame>();
        }

        public StackTrace(IEnumerable<Frame> frames)
        {
            this.frames = new List<Frame>(frames);
        }

        public void AddFrame(Frame frame)
        {
            frames.Add(frame);
        }
    }
}