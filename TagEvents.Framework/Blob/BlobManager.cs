using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TegEvents.Framework.Blob
{
    public class BlobManager<TEntity> : Manager, IBlobManager<TEntity>
        where TEntity : class, IEntity
    {
        private readonly string _url = "https://teg-coding-challenge.s3.ap-southeast-2.amazonaws.com/events/event-data.json";
        public BlobManager(ILogger<BlobManager<TEntity>> logger)
        : base(logger)
        { }

        public async Task<TEntity> GetListOfData()
        {
            var httpClient = new HttpClient();
            string jsonResponse;

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                using HttpResponseMessage response = await httpClient.GetAsync(_url);
                response.EnsureSuccessStatusCode();
                jsonResponse = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                return string.IsNullOrWhiteSpace(jsonResponse) ? default : JsonSerializer.Deserialize<TEntity>(jsonResponse, options);
            }
            catch (HttpRequestException e)
            {
                Logger.LogError(e.Message, GetListOfData(), e);
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, GetListOfData(), e);
                throw;
            }
        }
    }
}
