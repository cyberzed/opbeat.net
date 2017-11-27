using System.Diagnostics;
using Xunit;

namespace opbeat.Core.Tests
{
    public class ExplicitFactAttribute : FactAttribute
    {
        public ExplicitFactAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Only running in interactive mode";
            }
        }
    }
}