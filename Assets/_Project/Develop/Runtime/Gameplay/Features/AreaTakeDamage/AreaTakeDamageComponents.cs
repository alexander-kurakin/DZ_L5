using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaTakeDamage
{
    public class AreaDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}