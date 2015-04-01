using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using opbeat.Core.ReleaseModels;

namespace opbeat.Core
{
    public class OpbeatClient
    {
        private readonly HttpClient client;

        //https://opbeat.com/api/v1/organizations/<organization-id>/apps/<app-id>/releases/
        //https://opbeat.com/api/v1/organizations/<organization-id>/apps/<app-id>/errors/

        public OpbeatClient(OpbeatConfiguration configuration)
        {
            client = new HttpClient();

            SetupClient(configuration);
        }

        private void SetupClient(OpbeatConfiguration configuration)
        {
            var baseUri = string.Format("https://opbeat.com/api/v1/organizations/{0}/apps/{1}/",
                configuration.OrganizationId,
                configuration.ApplicationId
                );

            client.BaseAddress = new Uri(baseUri);

            var defaultRequestHeaders = client.DefaultRequestHeaders;

            defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.AccessToken);

            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            defaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("opbeat.net", assemblyVersion));
        }

        public bool Send(Release release)
        {
            return false;
        }
    }
}