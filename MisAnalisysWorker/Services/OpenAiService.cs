using MisAnalisysWorker.Models.OpenAI;
using Newtonsoft.Json;
using System.Text;

namespace MisAnalisysWorker.Services
{
    public class OpenAiService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenAiService> _logger;
        private readonly HttpClient _httpClient;

        public OpenAiService(IConfiguration configuration, ILogger<OpenAiService> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
            
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["OpenAI:ApiKey"]}");
        }

        public async Task<List<int>> ExtractServicesFromText(string prescriptionText, List<KeyValuePair<int, string>> availableServices)
        {
            try
            {
                var servicesData = string.Join("\n", availableServices.Select(s => $"ID: {s.Key}, Name: {s.Value}"));
                var prompt = $@"
You are a medical analyst who extracts mentions of medical services from physician's prescription text.

Below is a list of available medical services with their identifiers:
{servicesData}

Read the following prescription/referral text and determine which services from the list above are mentioned in it:
""{prescriptionText}""

Return only the service IDs in JSON format. For example:
{{
  ""service_ids"": [1, 3, 5]
}}

If no services are mentioned in the text, return an empty array:
{{
  ""service_ids"": []
}}";

                var request = new ChatGptRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        new ChatMessage("system", "You are a medical analyst who extracts mentions of medical services from physician's prescription text. Respond only in JSON format."),
                        new ChatMessage("user", prompt)
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("chat/completions", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ChatGptResponse>(responseString);

                if (apiResponse?.Choices == null || apiResponse.Choices.Count == 0)
                {
                    _logger.LogWarning("Empty response received from OpenAI API");
                    return new List<int>();
                }

                var jsonResponse = apiResponse.Choices[0].Message.Content.Trim();
                
                _logger.LogInformation($"Received response from OpenAI: {jsonResponse}");
                
                try
                {
                    var extractedServices = JsonConvert.DeserializeObject<ExtractedServicesResponse>(jsonResponse);
                    return extractedServices?.ServiceIds ?? new List<int>();
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, $"Error parsing JSON response from OpenAI: {jsonResponse}");
                    return new List<int>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error requesting OpenAI API");
                return new List<int>();
            }
        }
    }
} 