using System.Diagnostics;

namespace OpenTelemetry2API;

public static class ActivitySourceProvider
{
    public static ActivitySource ActivitySource = new("AppActivitySource");
}