using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Solen.SpecTests
{
    [Binding]
    public class CustomConversions
    {
        private readonly FeatureContext _featureContext;

        public CustomConversions(FeatureContext featureContext)
        {
            _featureContext = featureContext;
        }

        [StepArgumentTransformation]
        public string NullStringTransform(string stringValue)
        {
            return stringValue.Equals("null", System.StringComparison.OrdinalIgnoreCase) ? null : stringValue;
        }
    }
}
