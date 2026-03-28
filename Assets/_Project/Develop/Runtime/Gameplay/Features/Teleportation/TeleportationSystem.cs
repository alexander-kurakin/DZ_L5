using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportationSystem : IInitializableSystem, IUpdatableSystem
    {
        private Transform _transform;
        private ReactiveVariable<float> _teleportationRadius;

        private ICompositeCondition _canTeleport;
        
        
        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _canTeleport = entity.CanTeleport;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canTeleport.Evaluate() == false)
                return;
        }
    }
}