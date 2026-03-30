using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportationRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class CanTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class StartTeleportationRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class StartTeleportationEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class EndTeleportationEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class TeleportProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TeleportProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InTeleportProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class TeleportDelayTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TeleportDelayEndEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class TeleportCooldownProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class TeleportCooldownProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class InTeleportCooldownProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}