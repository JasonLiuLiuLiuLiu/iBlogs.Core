using System;
using System.Collections.Generic;
using System.Threading;

namespace iBlogs.Site.Core.Common
{
    public class BlogsTimer
    {
        private static Stack<int> _upFunnel;
        private static Stack<int> _downFunnel;
        private static readonly List<Action> TimerEvents;
        private static bool _timerSwitch;
        private static readonly int Speed;
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
            _timerThread = new Thread(Consume);
            _timerThread.Start();
            LunchEvents();
        }

        public static void Stop()
        {
            _timerSwitch = false;
        }

        public static void Register(Action timeEvent)
        {
            TimerEvents.Add(timeEvent);
            timeEvent.Invoke();
        }

        public static void AccelerateTo(TimeSpan timeSpan)
        {
            var accelerateSeconds = timeSpan.TotalSeconds;

            lock (TimerLock)
            {
                if (_upFunnel.Count < accelerateSeconds)
                    return;

                while (_upFunnel.Count > accelerateSeconds && _upFunnel.Count > 1)
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
                        var tempStack = _downFunnel;
                        _downFunnel = _upFunnel;
                        _upFunnel = tempStack;
                    }
                }
                Thread.Sleep(Speed);
            }
        }
    }
}
