using SuperTerminal.Const;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperTerminal.Utity
{
    public struct CupMetrics
    {
        /// <summary>
        /// cup可用%
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// cpu已使用%
        /// </summary>
        public decimal Used { get; set; }
    }

    internal class CPUMetricsHandler
    {
        internal CupMetrics GetWindowsMetrics()
        {
            var output = "";
            var info = new ProcessStartInfo()
            {
                FileName = "wmic",
                Arguments = "cpu get loadpercentage /Value",
                RedirectStandardOutput = true
            };
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }
            var lines = output.Trim();
            var LoadPercentage = lines.Split("=", StringSplitOptions.RemoveEmptyEntries);

            var metrics = new CupMetrics();
            metrics.Used = decimal.Parse(LoadPercentage[1]);
            metrics.Free = 100 - metrics.Used;
            return metrics;
        }

        internal CupMetrics GetUnixMetrics()
        {
            var output = "";
            string cmd = "";
            var info = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                Arguments = "-c \"top -bn 1 | grep Cpu | grep -o '[0-9]\\{1,\\}.[0-9]\\{1,\\} id\'\"",
                RedirectStandardOutput = true
            };
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
            Regex reg = new Regex("[0-9]{1,}\\.[0-9]{1,}");
            var metrics = new CupMetrics();
            var result = reg.Match(output);
            if (result.Success)
            {
                metrics.Free = decimal.Parse(result.Value);
                metrics.Used = 100 - metrics.Free;
            }
            return metrics;
        }
    }
}
