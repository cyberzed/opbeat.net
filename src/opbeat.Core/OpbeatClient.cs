using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using opbeat.Core.ErrorsModels;
using opbeat.Core.ReleaseModels;

namespace opbeat.Core
{
    public class OpbeatClient
    {
        private readonly HttpClient client;
        private readonly string errorsUrl;
        //https://intake.opbeat.com/api/v1/organizations/<organization-id>/apps/<app-id>/releases/
        //https://intake.opbeat.com/api/v1/organizations/<organization-id>/apps/<app-id>/errors/

        private readonly string releasesUrl;

        public OpbeatClient(OpbeatConfiguration configuration)
        {
            client = new HttpClient();

            SetupClient(configuration);

            releasesUrl = string.Format("api/v1/organizations/{0}/apps/{1}/releases/",
                configuration.OrganizationId,
                configuration.ApplicationId
                );

            errorsUrl = string.Format("api/v1/organizations/{0}/apps/{1}/errors/",
                configuration.OrganizationId,
                configuration.ApplicationId
                );
        }

        private void SetupClient(OpbeatConfiguration configuration)
        {
            client.BaseAddress = new Uri("https://intake.opbeat.com");

            var defaultRequestHeaders = client.DefaultRequestHeaders;

            defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.AccessToken);

            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            defaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("opbeat.net", assemblyVersion));
        }

        public ServiceResponse Send(Release release)
        {
            var json = Serializer.Serialize(release);

            var result = PostToApi(json, releasesUrl).Result;

            if (result.StatusCode == HttpStatusCode.Accepted)
            {
                return ServiceResponse.Success;
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return ServiceResponse.MissingToken;
            }

            return ServiceResponse.Failure;
        }

        public ServiceResponse Send(Error error)
        {
            var json = Serializer.Serialize(error);

            var result = PostToApi(json, errorsUrl).Result;

            if (result.StatusCode == HttpStatusCode.Accepted)
            {
                return ServiceResponse.Success;
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return ServiceResponse.MissingToken;
            }

            return ServiceResponse.Failure;
        }

        private Task<HttpResponseMessage> PostToApi(string json, string url)
        {
            var content = new StringContent(json)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            };

            return client.PostAsync(url, content);
        }
    }
}