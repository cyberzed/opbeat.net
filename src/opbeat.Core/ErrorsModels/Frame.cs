using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace opbeat.Core.ErrorsModels
{
    public class Frame
    {
        private readonly List<string> postContext;
        private readonly List<string> preContext;
        private readonly IDictionary<string, string> variables;
        public string Filename { get; private set; }
        public string LineNumber { get; private set; }
        public string AbsolutePath { get; private set; }
        public string MethodName { get; private set; }

        public IReadOnlyDictionary<string, string> Variables
        {
            get { return new ReadOnlyDictionary<string, string>(variables); }
        }

        public IReadOnlyList<string> PreContext
        {
            get { return preContext.AsReadOnly(); }
        }

        public string ContextLine { get; set; }

        public IReadOnlyList<string> PostContext
        {
            get { return postContext.AsReadOnly(); }
        }

        public Frame(string filename, string lineNumber)
        {
            Filename = filename;
            LineNumber = lineNumber;

            variables = new Dictionary<string, string>();
            preContext = new List<string>();
            postContext = new List<string>();
        }

        public void SetAbsolutePath(string path)
        {
            AbsolutePath = path;
        }

        public void AddMethodName(string methodName)
        {
            MethodName = methodName;
        }

        public void AddVariable(string name, string value)
        {
            if (variables.ContainsKey(name))
            {
                variables.Remove(name);
            }

            variables.Add(name, value);
        }

        public void AddPreContext(string line)
        {
            preContext.Add(line);
        }

        public void SetContext(string line)
        {
            ContextLine = line;
        }

        public void AddPostContext(string line)
        {
            postContext.Add(line);
        }
    }
}