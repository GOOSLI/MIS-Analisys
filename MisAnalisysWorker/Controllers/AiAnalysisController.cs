using Microsoft.AspNetCore.Mvc;
using MisAnalisysWorker.Services.Interfaces;

namespace MisAnalisysWorker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiAnalysisController : ControllerBase
    {
        private readonly IAiService _aiService;
        private readonly ILogger<AiAnalysisController> _logger;

        public AiAnalysisController(IAiService aiService, ILogger<AiAnalysisController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        [HttpPost("evaluate-relevance")]
        public async Task<ActionResult<double>> EvaluateServicesRelevance(
            [FromBody] ServicesRelevanceRequest request)
        {
            try
            {
                var score = await _aiService.EvaluateServicesRelevance(
                    request.DiagnosisDescription,
                    request.PrescribedServices);

                return Ok(score);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error evaluating services relevance");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("check-contraindications")]
        public async Task<ActionResult<List<string>>> CheckContraindications(
            [FromBody] ContraindicationsRequest request)
        {
            try
            {
                var contraindications = await _aiService.CheckForContraindications(
                    request.PrescriptionText,
                    request.PrescribedServices);

                return Ok(contraindications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking contraindications");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("analyze-completeness")]
        public async Task<ActionResult<CompletionAnalysis>> AnalyzeCompleteness(
            [FromBody] CompletenessRequest request)
        {
            try
            {
                var analysis = await _aiService.AnalyzePrescriptionCompleteness(
                    request.DiagnosisDescription,
                    request.PrescriptionText);

                return Ok(analysis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing prescription completeness");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class ServicesRelevanceRequest
    {
        public string DiagnosisDescription { get; set; }
        public List<string> PrescribedServices { get; set; }
    }

    public class ContraindicationsRequest
    {
        public string PrescriptionText { get; set; }
        public List<string> PrescribedServices { get; set; }
    }

    public class CompletenessRequest
    {
        public string DiagnosisDescription { get; set; }
        public string PrescriptionText { get; set; }
    }
} 