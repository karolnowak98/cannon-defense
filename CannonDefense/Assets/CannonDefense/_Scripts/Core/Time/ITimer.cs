using System;

namespace GlassyCode.CannonDefense.Core.Time
{
    public interface ITimer
    {
        event Action OnTimerExpired;
        event Action<float> OnTimerStarted;
        event Action OnTimerStopped;
        void Tick();
        void Start();
        void Stop();
        void Reset();
    }
}