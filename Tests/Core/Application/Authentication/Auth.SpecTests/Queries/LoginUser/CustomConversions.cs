using Solen.Core.Application.Auth.Queries;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Auth.SpecTests.Queries.LoginUser
{
    [Binding]
    public class CustomConversions
    {
        [StepArgumentTransformation]
        public LoginUserQuery TableToLoginUserQuery(Table table)
        {
            return table.CreateInstance<LoginUserQuery>();
        }
    }
}
