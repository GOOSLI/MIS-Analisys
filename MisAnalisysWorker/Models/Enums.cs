namespace MisAnalisysWorker.Models
{
    public enum ProcessingStatus
    {
        NotProcessed = 0,
        InProgress = 1,
        Processed = 2,
        Error = 3
    }

    public enum EmployeeTypeEnum
    {
        Doctor = 1,
        Nurse = 2,
        Administrator = 3,
        LabTechnician = 4,
        Other = 5
    }
} 