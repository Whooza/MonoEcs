namespace MonoEcs.Core.Systems.Bases;

public abstract class EcsEntitySystem<TSystem, TComponent> : IEcsEntitySystem where TSystem : IEcsEntitySystem where TComponent : IEcsComponent, new()
{
    public bool Enabled { get; set; }

    protected readonly EcsContainer EcsContainer;
    protected readonly TComponent[] Components;

    private readonly EcsEntitySystemConfig _configuration;

    protected EcsEntitySystem(EcsContainer ecsContainer, EcsEntitySystemConfig configuration)
    {
        EcsContainer = ecsContainer;
        _configuration = configuration;
        Components = new TComponent[configuration.MaxComponentCount];
        Enabled = true;

        for (int i = 0; i < configuration.MaxComponentCount; i++)
        {
            Components[i] = new TComponent();
        }
    }

    #region Component

    public void AddComponent(int entityId, IEcsComponent component)
    {
        Components[entityId] = (TComponent)component;
    }

    public void AddComponent(IEcsEntity entity, IEcsComponent component)
    {
        Components[entity.Id] = (TComponent)component;
    }

    public TComponent GetComponent(int entityId)
    {
        return Components[entityId];
    }

    public TComponent GetComponent(IEcsEntity entity)
    {
        return Components[entity.Id];
    }

    public void RemoveComponent(int entityId)
    {
        Components[entityId] = new TComponent();
    }

    public void RemoveComponent(IEcsEntity entity)
    {
        Components[entity.Id] = new TComponent();
    }

    #endregion

    #region Update

    public void Update(IEcsEntity[] entities, float deltaTime)
    {
        if (Enabled && _configuration.ShouldUpdate)
        {
            for (int i = 0; i < _configuration.MaxComponentCount; i++)
            {
                IEcsEntity entity = entities[i];

                if (entity.Exists && entity.Enabled)
                {
                    UpdateLogic(entity, Components[i], deltaTime);
                }
            }
        }
    }

    protected virtual void UpdateLogic(IEcsEntity entity, TComponent component, float deltaTime) { }

    #endregion

    #region Draw

    public void Draw(IEcsEntity[] entities, float deltaTime)
    {
        if (Enabled && _configuration.ShouldDraw)
        {
            EcsContainer.Graphics.SpriteBatch.Begin();

            for (int i = 0; i < _configuration.MaxComponentCount; i++)
            {
                IEcsEntity entity = entities[i];

                if (entity.Exists && entity.Enabled)
                {
                    DrawLogic(entity, Components[i], deltaTime);
                }
            }

            EcsContainer.Graphics.SpriteBatch.End();
        }
    }

    protected virtual void DrawLogic(IEcsEntity entity, TComponent component, float deltaTime) { }

    #endregion
}
