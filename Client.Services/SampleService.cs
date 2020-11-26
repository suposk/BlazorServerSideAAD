using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class SampleService : ISampleService
    {
        public Task<string> RandomText(string text = null)
        {
            var res = DateTime.Now.ToString() + (text == null ? null :$" with param {text}");
            return Task.FromResult(res);
        }
    }
}
