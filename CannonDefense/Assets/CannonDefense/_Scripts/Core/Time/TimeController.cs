using System;
using System.Collections;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Time
{
    public sealed class TimeController : MonoBehaviour, ITimeController
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime; 
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        
        public float RegularTime => UnityEngine.Time.time;
        public float FixedTime => UnityEngine.Time.fixedTime;
        public float UnscaledTime => UnityEngine.Time.unscaledTime;

        public event Action OnPaused;
        public event Action OnResumed;
        public bool IsPaused { get; private set; }
        
        public void Pause()
        {
            if (UnityEngine.Time.timeScale <= 0) return;
            
            UnityEngine.Time.timeScale = 0;
            IsPaused = true;
            OnPaused?.Invoke();
        }

        public void Resume()
        {
            if (UnityEngine.Time.timeScale != 0) return;
            
            UnityEngine.Time.timeScale = 1;
            IsPaused = false;
            OnResumed?.Invoke();
        }

        public void TogglePause()
        {
            if (UnityEngine.Time.timeScale > 0)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        public IEnumerator PauseIn(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Pause();
        }
    }
}