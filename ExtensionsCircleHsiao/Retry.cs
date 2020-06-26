using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtensionsCircleHsiao
{
    public static class Retry
    {
        public static void Do(
            Action action,
            TimeSpan retryInterval,
            int maxAttemptCount = 0)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, maxAttemptCount);
        }

        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int maxAttemptCount)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; maxAttemptCount == 0 ? true : attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        SpinWait.SpinUntil(() => false, retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }

        public static T Do<T>(
           Func<T> action,
           TimeSpan retryInterval)
        {
            var exceptions = new List<Exception>();

            while (true)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

                SpinWait.SpinUntil(() => false, retryInterval);
            }
            throw new AggregateException(exceptions);
        }
    }
}
