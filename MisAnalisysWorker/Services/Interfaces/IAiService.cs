namespace MisAnalisysWorker.Services.Interfaces
{
    public interface IAiService
    {
        Task<List<int>> ExtractServicesFromText(string prescriptionText, List<KeyValuePair<int, string>> availableServices);
        
        Task<double> EvaluateServicesRelevance(string diagnosisDescription, List<string> prescribedServices);
        
        Task<List<string>> CheckForContraindications(string prescriptionText, List<string> prescribedServices);
        
        Task<CompletionAnalysis> AnalyzePrescriptionCompleteness(string diagnosisDescription, string prescriptionText);
    }

    public class CompletionAnalysis
    {
        public double CompletenessScore { get; set; }
        public List<string> MissingAspects { get; set; }
        public List<string> Recommendations { get; set; }
    }
} 