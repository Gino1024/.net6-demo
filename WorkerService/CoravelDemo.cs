using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coravel.Invocable;

namespace WorkerService
{
    public class CoravelDemo : IInvocable
    {
        public Task Invoke()
        {
            Console.WriteLine($"Coravel run at: {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
