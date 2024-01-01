namespace MonoEcs.Core;

public class EcsGraphics
{
    public SpriteBatch SpriteBatch { get; private set; }
    public GraphicsDevice GraphicsDevice => _graphics.GraphicsDevice;
    public Rectangle WindowRectangle => _game.Window.ClientBounds;

    private int _width;
    private int _height;
    private bool _isFullscreen;
    private bool _isBorderless;

    private readonly Game _game;
    private readonly GraphicsDeviceManager _graphics;

    public EcsGraphics(Game game)
    {
        _game = game;
        SpriteBatch = default!;
        _graphics = new GraphicsDeviceManager(game)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720,
            SynchronizeWithVerticalRetrace = false,
            IsFullScreen = false
        };
    }

    public void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public void ToggleFullscreen()
    {
        bool oldIsFullscreen = _isFullscreen;

        if (_isBorderless)
        {
            _isBorderless = false;
        }
        else
        {
            _isFullscreen = !_isFullscreen;
        }

        ApplyFullscreenChange(oldIsFullscreen);
    }

    public void ToggleBorderless()
    {
        bool oldIsFullscreen = _isFullscreen;

        _isBorderless = !_isBorderless;
        _isFullscreen = _isBorderless;

        ApplyFullscreenChange(oldIsFullscreen);
    }

    private void ApplyFullscreenChange(bool oldIsFullscreen)
    {
        if (_isFullscreen)
        {
            if (oldIsFullscreen)
            {
                ApplyHardwareMode();
            }
            else
            {
                SetFullscreen();
            }
        }
        else
        {
            UnsetFullscreen();
        }
    }

    private void ApplyHardwareMode()
    {
        _graphics.HardwareModeSwitch = !_isBorderless;
        _graphics.ApplyChanges();
    }

    private void SetFullscreen()
    {
        _width = _game.Window.ClientBounds.Width;
        _height = _game.Window.ClientBounds.Height;

        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.HardwareModeSwitch = !_isBorderless;

        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }

    private void UnsetFullscreen()
    {
        _graphics.PreferredBackBufferWidth = _width;
        _graphics.PreferredBackBufferHeight = _height;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
    }
}
