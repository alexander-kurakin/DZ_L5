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

        private ReactiveEvent<float> _spendEnergyRequest;
        private ReactiveEvent<float> _spendEnergyEvent;
        
        private ReactiveVariable<float> _teleportEnergyCost;

        private IDisposable _teleportRequestDispose;
        private IDisposable _spendEnergyRequestDispose;


        public void OnInit(Entity entity)
        {
            _startTeleportEvent = entity.StartTeleportationEvent;
            _startTeleportRequest = entity.StartTeleportationRequest;
            _inTeleportProcess = entity.InTeleportProcess;
            _canStartTeleport = entity.CanTeleport;
            _teleportEnergyCost = entity.TeleportEnergyCost;
            
            _spendEnergyRequest = entity.SpendEnergyRequest;
            _spendEnergyEvent = entity.SpendEnergyEvent;

            _teleportRequestDispose = _startTeleportRequest.Subscribe(OnTeleportRequest);
            _spendEnergyRequestDispose = _spendEnergyEvent.Subscribe(OnEnergySpendSucceeded);
        }

        private void OnTeleportRequest()
        {
            if (_canStartTeleport.Evaluate())
            {
                Debug.Log("Can start Teleport System");
                _spendEnergyRequest.Invoke(_teleportEnergyCost.Value);
            }
            else
            {
                Debug.Log("Cannot start Teleport System");
            }
        }
        
        private void OnEnergySpendSucceeded(float teleportEnergyCost)
        {
            _inTeleportProcess.Value = true;
            _startTeleportEvent.Invoke();

            Debug.Log($"Started teleportation after spending {teleportEnergyCost} energy");
        }


        public void OnDispose()
        {
            _teleportRequestDispose.Dispose();
            _spendEnergyRequestDispose.Dispose();
        }
    }
}