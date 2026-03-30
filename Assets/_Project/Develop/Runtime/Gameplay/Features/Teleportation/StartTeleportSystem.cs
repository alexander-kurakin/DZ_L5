using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class StartTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startTeleportRequest;
        private ReactiveEvent _startTeleportEvent;
        private ReactiveVariable<bool> _inTeleportProcess;

        private ICompositeCondition _canStartTeleport;

        private IDisposable _teleportRequestDispose;


        public void OnInit(Entity entity)
        {
            _startTeleportEvent = entity.StartTeleportationEvent;
            _startTeleportRequest = entity.StartTeleportationRequest;
            _inTeleportProcess = entity.InTeleportProcess;
            _canStartTeleport = entity.CanTeleport;

            _teleportRequestDispose = _startTeleportRequest.Subscribe(OnTeleportRequest);
        }

        private void OnTeleportRequest()
        {
            if (_canStartTeleport.Evaluate())
            {
                _inTeleportProcess.Value = true;
                _startTeleportEvent.Invoke();
                Debug.Log("Teleport System Started");
            }
            else
            {
                Debug.Log("Cannot start Teleport System");
            }
        }

        public void OnDispose()
        {
            _teleportRequestDispose.Dispose();
        }
    }
}