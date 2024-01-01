namespace MonoEcs.Client;

internal class Client : Game
{
    private readonly EcsWorld _world;
    private float currentDeltaTime;

    private const int _maxEntityCount = 25000;

    public Client()
    {
        _world = new EcsWorld(this, _maxEntityCount);
    }

    protected override void Initialize()
    {
        ConfigureEnvironment();

        _world.Initialize();

        for (int i = 0; i < _maxEntityCount; i++)
        {
            EcsEntity player = _world.CreateEntity<EcsEntity>("test");
            _world.UpdateEntity(player);
            _world.AddEntityToSystem<MovementSystem, Transform>(player, new Transform(player.Id, true, 250f, 250f, 0f));
            _world.AddEntityToSystem<SpriteRenderer, StaticSprite>(player, new StaticSprite(player.Id, "Mines/Mine", 1f, 1f, 1f, 1f, 1f, 1f));
        }

        base.Initialize();
    }

    private void ConfigureEnvironment()
    {
        Content.RootDirectory = "Content";
        Window.AllowUserResizing = true;
        IsFixedTimeStep = false;
        IsMouseVisible = false;
    }

    protected override void LoadContent()
    {
        _world.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        currentDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _world.Update(currentDeltaTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _world.Draw(currentDeltaTime);
        base.Draw(gameTime);
    }
}
