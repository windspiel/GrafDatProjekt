using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//own namespaces
using Tabletop_0._1.Figuren;
using Tabletop_0._1.Content.Meshes;

namespace Tabletop_0._1
{

    /// This is the main type for your game
    public class Game1 : Game
    {
        #region: Globals
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ButtonState LeftButton;

        Vector3 cameraPos= new Vector3(15,10,10);

        Roboter robo= new Roboter();
        Table table = new Table();
        MouseCursor maus = new MouseCursor();
        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            maus.Initialize(Content, new Vector2(0, 0));
            table.Initialize(graphics);
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Load a lot of stuff
            robo.load(Content);
            table.Load(this.GraphicsDevice);
        }


        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        protected override void UnloadContent()
        {

        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
 

        protected override void Update(GameTime gameTime)
        {
            #region:Steuerung
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState currentMouseState = Mouse.GetState();
            LeftButton = currentMouseState.LeftButton;
            maus.Position = new Vector2(currentMouseState.X, currentMouseState.Y);
            #endregion
            #region: DrawUpdates
            robo.update(cameraPos, 0.0f, gameTime);
            table.update(cameraPos);
            #endregion
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            //3d Elements
            table.DrawGround(graphics);

            robo.draw(graphics, new Vector3 (0,0,0));



            // Gui und 2d Elemente
            spriteBatch.Begin();
            maus.Draw(spriteBatch, LeftButton, false);
            spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;


            base.Draw(gameTime);
        }

    }
}
