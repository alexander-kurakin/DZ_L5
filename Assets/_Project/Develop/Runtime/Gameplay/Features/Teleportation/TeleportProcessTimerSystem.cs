using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportProcessTimerSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inTeleportProcess;
        private ReactiveEvent _startTeleportEvent;
        
        private IDisposable _startTeleportEventDisposable;
        
        public void OnInit(Entity entity)
        {
            _currentTime = entity.TeleportProcessCurrentTime;
            _inTeleportProcess = entity.InTeleportProcess;
            _startTeleportEvent = entity.StartTeleportationEvent;

            _startTeleportEventDisposable = _startTeleportEvent.Subscribe(OnStartTeleportProcess);
        }

        private void OnStartTeleportProcess()
        {
            _currentTime.Value = 0;
            
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_inTeleportProcess.Value == false)
                return;
            
            _currentTime.Value += deltaTime;
        }        

        public void OnDispose()
        {
            _startTeleportEventDisposable.Dispose();
        }


    }
}