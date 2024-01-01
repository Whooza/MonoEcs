namespace MonoEcs.Core;

public class EcsContainer
{
    public EcsGraphics Graphics { get; }
    public ContentManager Content => _game.Content;

    private readonly Game _game;
    private readonly int _maxEntityCount;
    private readonly Dictionary<Type, IEcsGlobalSystem> _globalSystems;
    private readonly Dictionary<Type, IEcsEntitySystem> _entitySystems;

    public EcsContainer(Game game, int maxEntityCount)
    {
        _game = game;
        _maxEntityCount = maxEntityCount;
        Graphics = new EcsGraphics(game);
        _globalSystems = new Dictionary<Type, IEcsGlobalSystem>();
        _entitySystems = new Dictionary<Type, IEcsEntitySystem>();
    }

    public TSystem GetGlobalSystem<TSystem>() where TSystem : IEcsGlobalSystem
    {
        return (TSystem)_globalSystems[typeof(TSystem)];
    }

    public TSystem GetEntitySystem<TSystem>() where TSystem : IEcsEntitySystem
    {
        return (TSystem)_entitySystems[typeof(TSystem)];
    }

    #region MonoGame

    public void Initialize()
    {
        _globalSystems.Add(typeof(GraphicsSystem), new GraphicsSystem(this));
        _globalSystems.Add(typeof(InputSystem), new InputSystem(this));
        _globalSystems.Add(typeof(NetworkSystem), new NetworkSystem(this));

        _entitySystems.Add(typeof(MovementSystem), new MovementSystem(this, new EcsEntitySystemConfig(_maxEntityCount, true, false)));
        _entitySystems.Add(typeof(SpriteRenderer), new SpriteRenderer(this, new EcsEntitySystemConfig(_maxEntityCount, true, true)));
    }

    public void LoadContent()
    {
        Graphics.LoadContent();
    }

    public void Update(IEcsEntity[] entities, float deltaTime)
    {
        foreach (IEcsGlobalSystem system in _globalSystems.Values)
        {
            system.Update(deltaTime);
        }

        foreach (IEcsEntitySystem system in _entitySystems.Values)
        {
            system.Update(entities, deltaTime);
        }
    }

    public void Draw(IEcsEntity[] entities, float deltaTime)
    {
        foreach (IEcsEntitySystem system in _entitySystems.Values)
        {
            system.Draw(entities, deltaTime);
        }
    }

    #endregion
}
