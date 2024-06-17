using System;

namespace GlassyCode.CannonDefense.Core.Time
{
    public abstract class Timer : ITimer
    {
        private readonly ITimeController _timeController;
        private readonly float _countdownTime;
        
        private bool _isRunning;
        private float _remainingTime;
        
        public event Action OnTimerReset;
        public event Action<float> OnTimerStarted;
        public event Action OnTimerStopped;
        public event Action OnTimerExpired;

        protected Timer(ITimeController timeController, float countdownTime)
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
                Stop();
                OnTimerExpired?.Invoke();
            }
        }
        
        public virtual void Start()
        {
            _isRunning = true;
            OnTimerStarted?.Invoke(_remainingTime);
        }
        
        public virtual void Stop()
        {
            _isRunning = false;
            OnTimerStopped?.Invoke();
        }

        public virtual void Reset()
        {
            _isRunning = false;
            _remainingTime = _countdownTime;
            OnTimerExpired = null;
            OnTimerReset?.Invoke();
        }
    }
}