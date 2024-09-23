using System;

namespace CarterService.Entities;

public record FailedResponse
{
    public FailedResponse(Exception ex)
    {
        Message = ex.Message;
#if DEBUG
        StackTrace = ex.StackTrace ?? string.Empty;
        Source = ex.Source ??  string.Empty;
        TargetMethod = ex.TargetSite?.Name ?? string.Empty;
#endif
    }

    public string Message { get; set; }
#if DEBUG
    public string StackTrace { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string TargetMethod { get; set; } = string.Empty;
#endif
}
