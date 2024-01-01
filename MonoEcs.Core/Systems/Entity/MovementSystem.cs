namespace MonoEcs.Core.Systems.Entity;

public class MovementSystem : EcsEntitySystem<MovementSystem, Transform>
{
    private readonly Random random = new();

    public MovementSystem(EcsContainer systemContainer, EcsEntitySystemConfig configuration)
        : base(systemContainer, configuration)
    {
    }

    protected override void UpdateLogic(IEcsEntity entity, Transform component, float deltaTime)
    {
        component.VelocityX = random.Next(-50, 50);
        component.VelocityY = random.Next(-50, 50);
        component.PositionX += component.VelocityX * deltaTime;
        component.PositionY += component.VelocityY * deltaTime;
        Components[entity.Id] = component;
    }
}
