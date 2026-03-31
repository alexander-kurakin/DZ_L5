using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaImpactDamageContactsDetectingSystem : IInitializableSystem, IDisposableSystem
    {
        private Transform _areaImpactPointTransform;
        
        private ReactiveVariable<float> _areaImpactRadius;
        
        private CapsuleCollider _body;
        private LayerMask _areaImpactMask;
        
        private Buffer<Collider> _areaImpactColliders;
        
        private ReactiveEvent _dealAreaImpactDamageRequest;
        private IDisposable _areaImpactDamageRequestDispose;

        public void OnInit(Entity entity)
        {
            _body = entity.BodyCollider;
            
            _areaImpactPointTransform = entity.Transform;
            _areaImpactRadius = entity.AreaImpactRadius;
            _areaImpactMask = entity.AreaImpactMask;
            _areaImpactColliders = entity.AreaImpactCollidersBuffer;
            
            _dealAreaImpactDamageRequest = entity.DealAreaImpactDamageRequest;
            _areaImpactDamageRequestDispose = _dealAreaImpactDamageRequest.Subscribe(OnDealAreaImpactDamageRequest);
        }

        private void OnDealAreaImpactDamageRequest()
        {
            _areaImpactColliders.Count = 0;
            
            int overlapHits = Physics.OverlapSphereNonAlloc(
                _areaImpactPointTransform.position,
                _areaImpactRadius.Value,
                _areaImpactColliders.Items,
                _areaImpactMask
            );
            
            _areaImpactColliders.Count = overlapHits;
            RemoveSelfFromContacts();
        }
        
        private void RemoveSelfFromContacts()
        {
            int indexToRemove = -1;

            for (int i = 0; i < _areaImpactColliders.Count; i++)
            {
                if (_areaImpactColliders.Items[i] == _body)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove >= 0)
            {
                for (int i = indexToRemove; i < _areaImpactColliders.Count - 1; i++)
                {
                    _areaImpactColliders.Items[i] = _areaImpactColliders.Items[i + 1];
                }

                _areaImpactColliders.Count--;
            }
        }

        public void OnDispose()
        {
            _areaImpactDamageRequestDispose.Dispose();
        }
    }
}