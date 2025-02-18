using System.Diagnostics.Metrics;

namespace WebAPI.Configs;

public static class DiagnosticsConfig
{
    public const string ServiceName = "CleanArchitecture";

    public static Meter Meter = new(ServiceName);

    public static Counter<int> SalesCounter = Meter.CreateCounter<int>("sales.count");
}