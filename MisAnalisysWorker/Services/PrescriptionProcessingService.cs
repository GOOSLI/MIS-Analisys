using Microsoft.EntityFrameworkCore;
using MisAnalisysWorker.Data;
using MisAnalisysWorker.Models;

namespace MisAnalisysWorker.Services
{
    public class PrescriptionProcessingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly OpenAiService _openAiService;
        private readonly ILogger<PrescriptionProcessingService> _logger;

        public PrescriptionProcessingService(
            ApplicationDbContext dbContext,
            OpenAiService openAiService,
            ILogger<PrescriptionProcessingService> logger)
        {
            _dbContext = dbContext;
            _openAiService = openAiService;
            _logger = logger;
        }

        public async Task ProcessNewAppointments()
        {
            try
            {
                _logger.LogInformation("Starting processing of new appointments");

                var newAppointments = await _dbContext.Appointments
                    .Where(a => a.ProcessingStatus == (int)ProcessingStatus.NotProcessed || a.ProcessingStatus == null)
                    .ToListAsync();

                if (!newAppointments.Any())
                {
                    _logger.LogInformation("No new appointments found");
                    return;
                }

                _logger.LogInformation($"Found {newAppointments.Count} new appointments to process");

                var availableServices = await _dbContext.AvailableServices
                    .Select(s => new KeyValuePair<int, string>(s.ServiceId, s.Name))
                    .ToListAsync();

                if (!availableServices.Any())
                {
                    _logger.LogWarning("No available services found in the database");
                    return;
                }

                foreach (var appointment in newAppointments)
                {
                    try
                    {
                        _logger.LogInformation($"Processing appointment ID: {appointment.AppointmentId}, prescription text: {appointment.PrescriptionText}");

                        appointment.ProcessingStatus = (int)ProcessingStatus.InProgress;
                        await _dbContext.SaveChangesAsync();

                        var extractedServiceIds = await _openAiService.ExtractServicesFromText(
                            appointment.PrescriptionText,
                            availableServices);

                        _logger.LogInformation($"For appointment ID: {appointment.AppointmentId} extracted {extractedServiceIds.Count} services");

                        var validServiceIds = await _dbContext.AvailableServices
                            .Where(s => extractedServiceIds.Contains(s.ServiceId))
                            .Select(s => s.ServiceId)
                            .ToListAsync();

                        var existingPrescriptions = await _dbContext.PrescribedServicesParsed
                            .Where(p => p.AppointmentId == appointment.AppointmentId)
                            .Select(p => p.ServiceId)
                            .ToListAsync();

                        var newServiceIds = validServiceIds.Except(existingPrescriptions).ToList();

                        foreach (var serviceId in newServiceIds)
                        {
                            var prescribedService = new PrescribedServiceParsed
                            {
                                AppointmentId = appointment.AppointmentId,
                                ServiceId = serviceId
                            };

                            _dbContext.PrescribedServicesParsed.Add(prescribedService);
                        }

                        appointment.ProcessingStatus = (int)ProcessingStatus.Processed;
                        
                        await _dbContext.SaveChangesAsync();
                        _logger.LogInformation($"Appointment ID: {appointment.AppointmentId} processed successfully, added {newServiceIds.Count} services");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing appointment ID: {appointment.AppointmentId}");
                        appointment.ProcessingStatus = (int)ProcessingStatus.Error;
                        await _dbContext.SaveChangesAsync();
                    }
                }

                _logger.LogInformation("Processing of new appointments completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during appointment processing");
            }
        }
    }
} 