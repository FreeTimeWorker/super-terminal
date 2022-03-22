using Norns.Urd;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Utity
{
    /// <summary>
    /// 发生异常就重试
    /// </summary>
    public class Retry : AbstractInterceptorAttribute
    {
        private readonly int retryCount;

        public Retry(int retryCount)
        {
            this.retryCount = retryCount;
        }

        public override async Task InvokeAsync(AspectContext context, AsyncAspectDelegate next)
        {
            await Policy.Handle<Exception>()
            .RetryAsync(retryCount)
            .ExecuteAsync(() => next(context));
        }
        public override void Invoke(AspectContext context, AspectDelegate next)
        {
            Policy.Handle<Exception>()
            .Retry(retryCount)
            .Execute(() => next(context));
        }
    }
}
