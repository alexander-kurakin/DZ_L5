using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportDelayEndTriggerSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _teleportDelayEndEvent;
        private ReactiveVariable<float> _delay;
        private ReactiveVariable<float> _teleportProcessCurrentTime;

        private ReactiveEvent _startTeleportEvent;

        private bool _alreadyTeleported;

        private IDisposable _timerDisposable;
        private IDisposable _startTeleportDisposable;
        
        public void OnInit(Entity entity)
        {
            _teleportDelayEndEvent = entity.TeleportDelayEndEvent;
            _delay = entity.TeleportDelayTime;
            _teleportProcessCurrentTime = entity.TeleportProcessCurrentTime;
            _startTeleportEvent = entity.StartTeleportationEvent;

            _timerDisposable = _teleportProcessCurrentTime.Subscribe(OnTimerChanged);
            _startTeleportDisposable = _startTeleportEvent.Subscribe(OnStartTeleport);
        }

        private void OnStartTeleport()
        {
            _alreadyTeleported = false;
        }

        private void OnTimerChanged(float arg1, float currentTime)
        {
            if (_alreadyTeleported)
                return;
            
            if (currentTime >= _delay.Value)
            {
                Debug.Log("Delay End");
                _teleportDelayEndEvent.Invoke();
                _alreadyTeleported = true;
            }
        }

        public void OnDispose()
        {
            _timerDisposable.Dispose();
            _startTeleportDisposable.Dispose();
        }
    }
}