namespace opbeat.Core.ErrorsModels
{
    public class Exception
    {
        public string Type { get; private set; }
        public string Value { get; private set; }
        public string Module { get; private set; }

        public Exception(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public void AddModule(string module)
        {
            Module = module;
        }
    }
}