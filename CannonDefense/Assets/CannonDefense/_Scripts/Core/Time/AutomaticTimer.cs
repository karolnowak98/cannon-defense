using System;

namespace GlassyCode.CannonDefense.Core.Time
{
    public sealed class AutomaticTimer : ITimer
    {
        private readonly ITimeController _timeController;
        private readonly float _countdownTime;
        
        private bool _isRunning;
        private float _remainingTime;
        
        public event Action<float> OnTimerStarted;
        public event Action OnTimerStopped;
        public event Action OnTimerExpired;

        public AutomaticTimer(ITimeController timeController, float countdownTime)
        {
            _timeController = timeController;
            _countdownTime = countdownTime;
        }

        public void Tick()
        {
            if (!_isRunning) return;
            
            var deltaTime = _timeController.DeltaTime;

            _remainingTime -= deltaTime;

            if (_remainingTime <= 0f)
            {
                OnTimerExpired?.Invoke();
                Reset();
            }
        }
        
        public void Start()
        {
            _isRunning = true;
            OnTimerStarted?.Invoke(_remainingTime);
        }
        
        public void Stop()
        {
            _isRunning = false;
            OnTimerStopped?.Invoke();
        }

        public void Reset()
        {
            _remainingTime = _countdownTime;
        }
    }
}