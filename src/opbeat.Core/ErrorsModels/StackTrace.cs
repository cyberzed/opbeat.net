using System;
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
            if (frames == null)
            {
                throw new ArgumentNullException("frames");
            }

            this.frames = new List<Frame>(frames);
        }

        public void AddFrame(Frame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException("frame");
            }

            frames.Add(frame);
        }
    }
}