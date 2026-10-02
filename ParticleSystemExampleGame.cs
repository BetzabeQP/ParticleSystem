using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystemExercise;

public class ParticleSystemExampleGame : Game, IParticleEmitter
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private MouseState _priorMouse;
    private ExplosionParticleSystem _explosion;

    public Vector2 Postion{get;set;}
    public Vector2 Velocity {get;set;}

    private FireWorksParticleSystem _fireworks;

    public ParticleSystemExampleGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        RainParticleSystem rain = new RainParticleSystem(this, new Rectangle(100,-20, 500, 10));
        Components.Add(rain);

        _explosion = new ExplosionParticleSystem(this, 20);
        Components.Add(_explosion);
        _fireworks = new FireWorksParticleSystem(this, 20);
        Components.Add(_fireworks); 
        PixieParticleSystem pixie = new PixieParticleSystem(this, this);
        Components.Add(pixie);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        MouseState currentMouse = Mouse.GetState();
        Vector2 mousePosition = new Vector2(currentMouse.X, currentMouse.Y);
        if(currentMouse.LeftButton == ButtonState.Pressed && _priorMouse.LeftButton == ButtonState.Released)
        {
            _explosion.PlaceExplosion(mousePosition);
        }
        if(currentMouse.RightButton == ButtonState.Pressed && _priorMouse.RightButton == ButtonState.Released)
        {
            _fireworks.PlaceFireWork(mousePosition);
        }
        Velocity = mousePosition - Postion;
        Postion = mousePosition;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
