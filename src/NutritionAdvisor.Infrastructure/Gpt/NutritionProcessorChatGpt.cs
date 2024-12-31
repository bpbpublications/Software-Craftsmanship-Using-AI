using Newtonsoft.Json;
using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.UseCases.Nutrition;
using Rystem.OpenAi;
using Rystem.OpenAi.Chat;
using static System.Environment;

namespace NutritionAdvisor.Infrastructure.Gpt
{
    public class NutritionProcessorChatGpt : INutritionProcessorGpt
    {
        private readonly IOpenAiFactory _openAiFactory;

        public NutritionProcessorChatGpt(IOpenAiFactory openAiFactory)
        {
            _openAiFactory = openAiFactory;
        }

        public async Task<NutritionResponse> Process(NutritionRequest request)
        {
            var openAi = _openAiFactory.Create();
            var systemPrompt = ConstructSystemPrompt();
            var usePrompt = ConstructUserPrompt(request);
            var result = await openAi.Chat
                .RequestWithSystemMessage(systemPrompt)
                .AddUserMessage(usePrompt)
                .WithTemperature(0)
                .WithModel(ChatModelType.Gpt4)
                .ExecuteAsync();

            var resposne = ProcessChatResponse(result);

            return resposne;
        }

        private string ConstructSystemPrompt()
        {
            var apiSchema = File.ReadAllText(@"Resources/swagger.json");
            var exampleJson = File.ReadAllText(@"Resources/Example1.json");
            var prompt =
                $"- You are a RESTful web API which implements the following API schema:{NewLine}" +
                $"{NewLine}{apiSchema}{NewLine}{NewLine}" +
                $"- Example response:{NewLine}{exampleJson}{NewLine}" +
                $"- Response should be in JSON without the formatting tags. There should be no clarifying statements neither before or after the response.";

            return prompt;
        }

        private string ConstructUserPrompt(NutritionRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request, Formatting.Indented);

            var messageWithRequestBody = $"I have this request:{NewLine}{requestJson}{NewLine}";

            var messageWithResponseSpecification =
                "For dietComparison.daily calculate the total kcal, carbs, protein, fat and sugar consumed." +
                "For dietComparison.recommended calculate the recommendations based on person and their goal. Min and max values accordingly for each food property." +
                "For recommendedFood pick the best food based on person and their goal. " +
                "For message, give a tip for the person based on their goal. " +
                "Use -1 for numbers if not found";

            var prompt = $"{NewLine}{messageWithRequestBody}{NewLine}{NewLine}{messageWithResponseSpecification}";

            return prompt;
        }

        private NutritionResponse ProcessChatResponse(ChatResult result)
        {
            var firstResult = result.Choices.First();
            var resposne = JsonConvert.DeserializeObject<NutritionResponse>(firstResult.Message.Content);
            return resposne;
        }
    }

}
