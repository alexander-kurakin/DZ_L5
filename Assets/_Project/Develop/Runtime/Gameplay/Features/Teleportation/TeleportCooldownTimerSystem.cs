using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {

        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inTeleportCooldown;
        
        private ReactiveEvent _endTeleportEvent;
        
        private IDisposable _endTeleportEventDisposable;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.TeleportCooldownProcessCurrentTime;
            _initialTime = entity.TeleportCooldownProcessInitialTime;
            _inTeleportCooldown = entity.InTeleportCooldownProcess;
            _endTeleportEvent = entity.EndTeleportationEvent;

            _endTeleportEventDisposable = _endTeleportEvent.Subscribe(OnTeleportEnded);
        }

        private void OnTeleportEnded()
        {
            Debug.Log("Cooldown started");
            
            _currentTime.Value = _initialTime.Value;
            _inTeleportCooldown.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inTeleportCooldown.Value == false)
                return;
            
            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
            {
                _inTeleportCooldown.Value = false;
                Debug.Log("Cooldown ended");
            }
        }

        public bool CooldownIsOver() => _currentTime.Value <= 0;

        public void OnDispose()
        {
            _endTeleportEventDisposable.Dispose();
        }
    }
}