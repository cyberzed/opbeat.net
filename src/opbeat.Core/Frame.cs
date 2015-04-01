using System.Collections.Generic;

namespace opbeat.Core
{
    public class Frame
    {
        public string Filename { get; set; }
        public string LineNo { get; set; }
        public string AbsPath { get; set; }
        public string Function { get; set; }
        public Dictionary<string, string> Vars { get; set; }
        public List<string> Pre_Context { get; set; }
        public string Context_Line { get; set; }
        public List<string> Post_Context { get; set; }
    }
}