using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionAdvisor.Api.Dtos;
using NutritionAdvisor.Api.Mappers;
using NutritionAdvisor.Api.Security;
using NutritionAdvisor.UseCases.Nutrition;

namespace NutritionAdvisor.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/nutrition")]
    [ProtectedWithApiKey]
    [ApiController]
    public class NutritionControllerV1 : ControllerBase
    {
        private readonly INutritionServiceV1 _nutritionService;
        private readonly INutritionRequestMapper _mapper;

        public NutritionControllerV1(INutritionServiceV1 nutritionService, INutritionRequestMapper mapper)
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
