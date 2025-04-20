using System.Diagnostics;

namespace OpenTelemetry.API
{
    public static class ActivitySourceProvider
    {
        public static ActivitySource ActivitySource = new("AppActivitySource");
    }
}
