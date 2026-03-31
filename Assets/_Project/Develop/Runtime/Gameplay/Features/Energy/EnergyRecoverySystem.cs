using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergyRecoverySystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _recoveryAmount;
        private ReactiveVariable<float> _recoveryFrequency;

        private ReactiveVariable<float> _currentEnergy;
        
        public void OnInit(Entity entity)
        {
            _recoveryAmount = entity.EnergyRecoveryAmount;
            //_recoveryFrequency = entity.
        }

        public void OnUpdate(float deltaTime)
        {
                _recoveryFrequency.Value -= deltaTime;

                if (_recoveryFrequency.Value <= 0)
                    Debug.Log("");
                    //TryRecoverEnergy();
        }
        
        
    }
}