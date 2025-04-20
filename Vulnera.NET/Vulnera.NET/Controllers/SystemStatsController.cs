using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VulneraNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemStatsController : ControllerBase
    {
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var cpuUsage = GetCpuUsagePercent();
            var memoryUsage = GetMemoryUsageMB();

            return Ok(new
            {
                cpu = cpuUsage,
                memory = memoryUsage
            });
        }

        private double GetCpuUsagePercent()
        {
            var process = Process.GetCurrentProcess();
            TimeSpan startCpuUsage = process.TotalProcessorTime;
            DateTime startTime = DateTime.UtcNow;

            System.Threading.Thread.Sleep(500);

            TimeSpan endCpuUsage = process.TotalProcessorTime;
            DateTime endTime = DateTime.UtcNow;

            double cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            double totalMsPassed = (endTime - startTime).TotalMilliseconds;

            int processorCount = Environment.ProcessorCount;
            double cpuUsageTotal = (cpuUsedMs / (totalMsPassed * processorCount)) * 100;

            return Math.Round(cpuUsageTotal, 2);
        }

        private double GetMemoryUsageMB()
        {
            var mem = GC.GetGCMemoryInfo();
            long usedBytes = GC.GetTotalMemory(forceFullCollection: false);
            return Math.Round(usedBytes / 1024.0 / 1024.0, 2);
        }
    }
}
