using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionAdvisor.Api.Dtos;
using NutritionAdvisor.Api.Mappers;
using NutritionAdvisor.Api.Security;
using NutritionAdvisor.UseCases.Nutrition;

namespace NutritionAdvisor.Api.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v{version:apiVersion}/nutrition")]
    [ProtectedWithApiKey]
    [ApiController]
    public class NutritionControllerV2 : ControllerBase
    {
        private readonly INutritionServiceV2 _nutritionService;
        private readonly INutritionRequestMapper _mapper;

        public NutritionControllerV2(INutritionServiceV2 nutritionService, INutritionRequestMapper mapper)
        {
            _nutritionService = nutritionService;
            _mapper = mapper;
        }

        // Provide an example of a NutritionRequest using Swashbuckle
        [HttpPost]
        public async Task<ActionResult<NutritionResponse>> GetNutritionResponse(NutritionRequest request)
        {
            var mappedRequest = _mapper.Map(request);
            var response = await _nutritionService.GetNutritionResponse(mappedRequest);
            return Ok(response);
        }
    }
}
