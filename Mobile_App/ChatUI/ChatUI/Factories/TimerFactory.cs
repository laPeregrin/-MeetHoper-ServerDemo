using ChatUI.Abstractions;
using System.Timers;

namespace ChatUI.Factories
{
    public class TimerFactory : IFactory<int, Timer>
    {
        public Timer GetValue(int input) => new Timer(input);

    }
}
