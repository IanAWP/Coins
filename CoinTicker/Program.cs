using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace CoinTicker
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
   
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            MarketModel.MarketModelContext c = new MarketModel.MarketModelContext();
            List<ITriggeredJob> jobs = new List<ITriggeredJob>() { new  CoinMarketCapJob(c)};
            var tasks = new List<Task>();
            foreach (var job in jobs) {
                tasks.Add(job.RunJobAsync());
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
