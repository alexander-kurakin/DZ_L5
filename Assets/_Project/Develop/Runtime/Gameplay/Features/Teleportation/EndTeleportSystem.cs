using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class EndTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _endTeleportEvent;
        private ReactiveVariable<bool> _inTeleportProcess;
        private ReactiveVariable<float> _teleportProcessInitialTime;
        private ReactiveVariable<float> _teleportProcessCurrentTime;

        private IDisposable _timerDisposable;

        public void OnInit(Entity entity)
        {
            _endTeleportEvent = entity.EndTeleportationEvent;
            _inTeleportProcess = entity.InTeleportProcess;
            _teleportProcessInitialTime = entity.TeleportProcessInitialTime;
            _teleportProcessCurrentTime = entity.TeleportProcessCurrentTime;

            _timerDisposable = _teleportProcessCurrentTime.Subscribe(OnTimerChanged);
        }

        private void OnTimerChanged(float arg1, float arg2)
        {
            if (TimerIsDone(_teleportProcessCurrentTime.Value))
            {
                Debug.Log("End teleportation");
                _inTeleportProcess.Value = false;
                _endTeleportEvent.Invoke();
            }
            
        }

        public void OnDispose()
        {
            _timerDisposable.Dispose();
        }
        
        private bool TimerIsDone(float currentTime) => currentTime >= _teleportProcessInitialTime.Value;
    }
}