namespace MonoEcs.Core.Systems.Entity;

public class SpriteRenderer : EcsEntitySystem<SpriteRenderer, StaticSprite>
{
    public SpriteRenderer(EcsContainer systemContainer, EcsEntitySystemConfig configuration)
        : base(systemContainer, configuration)
    {
    }

    protected override void UpdateLogic(IEcsEntity entity, StaticSprite component, float deltaTime)
    {
        Transform transform = EcsContainer.GetEntitySystem<MovementSystem>().GetComponent(entity);
        component.PositionX = transform.PositionX;
        component.PositionY = transform.PositionY;
        Components[entity.Id] = component;
    }

    protected override void DrawLogic(IEcsEntity entity, StaticSprite component, float deltaTime)
    {
        Texture2D texture = EcsContainer.Content.Load<Texture2D>(component.Path);
        EcsContainer.Graphics.SpriteBatch.Draw(texture, component.GetPositionVector(), Color.White);
    }
}
