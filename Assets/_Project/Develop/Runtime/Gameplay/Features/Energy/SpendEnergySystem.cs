using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class SpendEnergySystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<float> _spendRequest;
        private ReactiveEvent<float> _spendEvent;

        private ReactiveVariable<float> _energy;

        private ICompositeCondition _canSpendEnergy;

        private IDisposable _requestDisposable;
        
        public void OnInit(Entity entity)
        {
            _spendRequest = entity.SpendEnergyRequest;
            _spendEvent = entity.SpendEnergyEvent;

            _energy = entity.CurrentEnergy;

            _canSpendEnergy = entity.CanSpendEnergy;

            _requestDisposable = _spendRequest.Subscribe(OnSpendRequest);
        }

        public void OnDispose()
        {
            _requestDisposable.Dispose();
        }
        
        private void OnSpendRequest(float energyCost)
        {
            if (energyCost < 0)
                throw new ArgumentOutOfRangeException(nameof(energyCost));

            if (_canSpendEnergy.Evaluate() == false)
                return;

            if (_energy.Value < energyCost)
            {
                Debug.Log("Недостаточно энергии!");
                return;
            }

            _energy.Value -= energyCost;
            
            _spendEvent.Invoke(energyCost);
            Debug.Log($"Я потратил энергию! Текущее значение: {_energy.Value}");
        }
    }
}