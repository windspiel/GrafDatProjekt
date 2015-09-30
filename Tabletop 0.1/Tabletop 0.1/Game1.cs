using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//own namespaces
using Tabletop_0._1.GameElements;
using Tabletop_0._1.Content.GameElements;
using Tabletop_0._1.LogikElement;

namespace Tabletop_0._1
{

    /// This is the main type for your game
    public class Game1 : Game
    {
        #region: Globals
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ButtonState LeftButton;

        

        Table table = new Table();
        MouseCursor maus = new MouseCursor();

        Camera camera = new Camera();

        private Vector3 cameraPos
        {
            get { return camera.cameraPosition; }
            set { camera.cameraPosition = value; }
        }

        GameElement[] teamRot = new GameElement[] { new SturmEH(), new SturmEH(), new SturmEH(), new SturmEH() }, 
            teamBlau;

        //Runden des spiels
        int Round = 0;
        //string message = "Picking does not work yet.";
        //SpriteFont font;
        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {


            base.Initialize();
            maus.Initialize(Content, new Vector2(0, 0));
            table.Initialize(graphics);
            camera.initial(graphics);
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Load a lot of stuff
            foreach (GameElement e in teamRot)
            {
                e.load(Content);
            }
           // robo.load(Content);
            table.Load(this.GraphicsDevice);
//            font = Content.Load<SpriteFont>("Grafiken/Arial");
            
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

            foreach (GameElement e in teamRot)
            {
                e.update(gameTime, camera);
            }
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
            for (int i = 0;  i < teamRot.Length; i++)
            {
                teamRot[i].rad = i * 2;
                teamRot[i].draw();
            }


            // Gui und 2d Elemente

            spriteBatch.Begin();
            maus.Draw(spriteBatch, LeftButton, CheckAll());
            spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //spriteBatch.DrawString(font, message, new Vector2(100, 100), Color.Black);
            //spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;


            base.Draw(gameTime);
        }


        public float? IntersectDistance(BoundingSphere sphere )
        {
            Ray mouseRay = maus.MouseRay(camera.View(), camera.Projection(), this.GraphicsDevice.Viewport);
            return mouseRay.Intersects(sphere);
        }



        public bool PickingCheck(Model model, Matrix world)
        {
            foreach (var mesh in model.Meshes)
            {
                BoundingSphere sphere = mesh.BoundingSphere;
                sphere = sphere.Transform(world);
                Ray mouseRay = maus.MouseRay(camera.View(), camera.Projection(), this.GraphicsDevice.Viewport);

                float? distance = mouseRay.Intersects(sphere);

                if (distance != null)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckAll ()
        {
            foreach (GameElement e in teamRot)
            {
                if (PickingCheck(e.model, e.GetWorldMatrix()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
