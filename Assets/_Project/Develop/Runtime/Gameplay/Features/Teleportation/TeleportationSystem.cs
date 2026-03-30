using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportationSystem : IInitializableSystem, IDisposableSystem
    {
        private Transform _transform;
        private ReactiveVariable<float> _teleportationRadius;

        private ReactiveEvent _teleportDelayEndEvent;

        private IDisposable _teleportDelayEndDisposable;
        
        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _teleportationRadius = entity.TeleportationRadius;
            
            _teleportDelayEndEvent = entity.TeleportDelayEndEvent;

            _teleportDelayEndDisposable = _teleportDelayEndEvent.Subscribe(OnTeleportDelayEnd);
        }

        private void OnTeleportDelayEnd()
        {
            Vector2 random2DPoint = _teleportationRadius.Value * UnityEngine.Random.insideUnitCircle;
            Vector3 newPosition = new Vector3(random2DPoint.x, 0, random2DPoint.y);
            
            _transform.position = newPosition;
            
            Debug.Log($"Teleported Transform: {_transform.name}, in a random position:  {newPosition}" );
        }

        public void OnDispose()
        {
            _teleportDelayEndDisposable.Dispose();
        }
    }
}