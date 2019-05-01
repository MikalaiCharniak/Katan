//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Katan.Core.Extensions
//{

//    public class TimerModule
//    {
//        private static object _synclock = new object();
//        private static bool _testEnd = false;
//        private readonly Timer _timer;

//        public Stopwatch StopWatch = new Stopwatch();
//        public TimeSpan testTime;

//        public TimerModule(int IdResult, TimeSpan span)
//        {
//            this.IdResult = IdResult;
//            testTime = span;
//            _timer = new Timer(new TimerCallback(EndTimer), null, testTime.Minutes * 60000, 0);
//            StopWatch.Start();
//        }

//        public TimeSpan CurrentInterval()
//        {
//            return (testTime.Subtract(StopWatch.Elapsed));
//        }

//        public void StopTimer()
//        {
//            _timer.Dispose();
//            StopWatch.Stop();
//        }

//        public void EndTimer(object obj)
//        {
//            if (_testEnd == false)
//            {
//                StopTimer();
//            }
//        }
//    }
//}
