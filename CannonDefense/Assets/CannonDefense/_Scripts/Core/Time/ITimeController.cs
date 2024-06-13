using System;

namespace GlassyCode.CannonDefense.Core.Time
{
    public interface ITimeController
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
        float UnscaledDeltaTime { get; }
        float RegularTime { get; }
        float FixedTime { get; }
        float UnscaledTime { get; }
        bool IsPaused { get; }
        event Action OnPaused;
        event Action OnResumed;
        void Pause();
        void Resume();
        void TogglePause();
    }
}
