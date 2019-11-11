using System;
using System.Collections.Generic;
using System.Threading;

namespace iBlogs.Site.Core.Common
{
    public class BlogsTimer
    {
        private static Stack<int> _upFunnel;  //沙漏上部分
        private static Stack<int> _downFunnel;  //沙漏下部分
        private static readonly List<Action> TimerEvents;  //定时执行的事件
        private static bool _timerSwitch;  //沙漏开关
        private static readonly int Speed;  //每秒消费的数量
        private static Thread _timerThread;
        private static readonly object TimerLock;
        static BlogsTimer()
        {
            _upFunnel = new Stack<int>();
            _downFunnel = new Stack<int>();
            Speed = 1 * 1000;
            TimerEvents = new List<Action>();
            TimerLock = new object();
        }
        //计时器开始
        public static void Start(TimeSpan timeSpan)
        {
            lock (TimerLock)
            {
                _upFunnel.Clear();
                _downFunnel.Clear();
                for (var i = 0; i < timeSpan.TotalSeconds; i++)
                {
                    _upFunnel.Push(i);
                }
            }
            _timerSwitch = true;
            _timerThread = new Thread(Consume); //起一个线程消费桶里的令牌
            _timerThread.Start();
            LunchEvents(); // 触发事件
        }
        public static void Stop()
        {
            _timerSwitch = false;
        }

        //给沙漏注册定时执行事件
        public static void Register(Action timeEvent)
        {
            TimerEvents.Add(timeEvent);
            timeEvent.Invoke();
        }

        //把沙漏加速到指定的时间
        public static void AccelerateTo(TimeSpan timeSpan)
        {
            var accelerateSeconds = timeSpan.TotalSeconds;
            lock (TimerLock)
            {
                if (_upFunnel.Count < accelerateSeconds) //当前沙漏中剩余令牌小于设置中秒数,则返回不加速
                    return;
                while (_upFunnel.Count > accelerateSeconds && _upFunnel.Count > 1)  //令牌数大于秒数,则释放出多余令牌
                {
                    _downFunnel.Push(_upFunnel.Pop());
                }
            }
        }
        private static void LunchEvents()
        {
            TimerEvents.ForEach(a => a.Invoke());
        }
        private static void Consume()
        {
            while (_timerSwitch)
            {
                lock (TimerLock)
                {
                    if (_upFunnel.TryPop(out var item))
                    {
                        _downFunnel.Push(item);
                    }
                    else
                    {
                        LunchEvents();
                        var tempStack = _downFunnel;  //旋转沙漏
                        _downFunnel = _upFunnel;
                        _upFunnel = tempStack;
                    }
                }
                Thread.Sleep(Speed);
            }
        }
    }
}
