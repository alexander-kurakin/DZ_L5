using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class MaxEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class CurrentEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class SpendEnergyRequest : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class SpendEnergyEvent : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class EnergyRecoveryProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class EnergyRecoveryProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InEnergyRecoveryProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class CanSpendEnergy : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TeleportEnergyCost : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}