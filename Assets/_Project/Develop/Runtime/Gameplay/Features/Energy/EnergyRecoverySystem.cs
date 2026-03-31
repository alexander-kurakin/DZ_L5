using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergyRecoverySystem : IInitializableSystem, IUpdatableSystem
    {
        
        private ReactiveVariable<float> _maxEnergy;
        private ReactiveVariable<float> _currentEnergy;
        
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inEnergyRecoveryProcess;
        
        public void OnInit(Entity entity)
        {
            _maxEnergy = entity.MaxEnergy;
            _currentEnergy = entity.CurrentEnergy;
            
            _initialTime = entity.EnergyRecoveryProcessInitialTime;
            _currentTime = entity.EnergyRecoveryProcessCurrentTime;
            _inEnergyRecoveryProcess = entity.InEnergyRecoveryProcess;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_currentEnergy.Value >= _maxEnergy.Value)
            {
                _inEnergyRecoveryProcess.Value = false;
                return;
            }

            if (_inEnergyRecoveryProcess.Value == false)
            {
                _currentTime.Value = _initialTime.Value;
                _inEnergyRecoveryProcess.Value = true;
            }
            
            _currentTime.Value -= deltaTime;

            if (_currentTime.Value > 0)
                return;

            _currentEnergy.Value = MathF.Min(
                _currentEnergy.Value + (0.1f * _maxEnergy.Value),
                _maxEnergy.Value);
            
            _currentTime.Value = _initialTime.Value;
            
            Debug.Log($"Energy regen tick successful: {_currentEnergy.Value}");

            if (_currentEnergy.Value >= _maxEnergy.Value)
            {
                _inEnergyRecoveryProcess.Value = false;
            }
        }
    }
}