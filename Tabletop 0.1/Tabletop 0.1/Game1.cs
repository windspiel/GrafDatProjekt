﻿using Microsoft.Xna.Framework;
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
        ButtonState 
            LeftButton, oldLeftButton,
            RightButton, oldRightButton; 
        float oldMausWheel;

        Table table = new Table();
        MouseCursor maus = new MouseCursor();

        Spielstatus GStatus= new Spielstatus();
        Camera camera = new Camera();

        GameElement[] teamRot = new GameElement[] { 
            new SturmEH(), new SturmEH(), new SturmEH(), new SturmEH(),
            new TermiEH(), new TermiEH(),new CptnEH(), new CptnEH(),
            new SturmBA(),new SturmBA(), new SturmBA(), new SturmBA(),
            new TermiBA(), new TermiBA(), new CptnBA(), new CptnBA()};
        GameElement selPlayer
        {
            get { return teamRot[selected]; }
        }

        GameElement othPlayer
        {
            get
            {
                if (isOver() != -1)
                    return teamRot[isOver()];
                return null;
            }
            set
            {
                teamRot[isOver()]=value;
            }
        }
        
        Circle circ = new Circle();
        RCircle rCirc = new RCircle();
        //Runden des spiels
        int selected = -1;
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
            int i = 0; int ii = 0;
            foreach (GameElement e in teamRot)
            {
                e.load(Content);
                if (e.team == "rot")
                {
                    e.Position = e.movePoint = new Vector2(i * 3-10, 19);
                    i++; GStatus.Rote++;
                }
                if (e.team == "blau")
                {
                    e.Position = e.movePoint = new Vector2(ii * 3-10, -19);
                    e.winkel(new Vector2(ii * 3-10, -18));
                    ii++; GStatus.Blaue++;
                }
            }
            table.Load(this.GraphicsDevice);
            circ.load(Content);
            circ.Position = new Vector2(0,0);
            rCirc.load(Content);
            rCirc.Position = new Vector2(0, 0);
//            font = Content.Load<SpriteFont>("Grafiken/Arial");
        }


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

            camera.steuern(Keyboard.GetState(), currentMouseState.ScrollWheelValue - oldMausWheel);

            LeftButton = currentMouseState.LeftButton;
            RightButton = currentMouseState.RightButton;
            maus.update(camera.View(), camera.Projection(), this.GraphicsDevice.Viewport);
            maus.Position = new Vector2(currentMouseState.X, currentMouseState.Y);

            if (RightButton == ButtonState.Released &&
                oldRightButton == ButtonState.Pressed)
            {
                GStatus.nextPhase();
                selected = -1;

            }

            if (LeftButton == ButtonState.Released && 
                oldLeftButton == ButtonState.Pressed)
            {
                /*
                 * keine Figur ausgewählt?
                 * dann wähle aus deinem team
                 */
                if (!isSelected()&&isOver()!=-1)
                {
                    if (othPlayer.team == GStatus.Turn)
                    {
                        selected = isOver();

                        if (GStatus.Phase == "move" && selPlayer.hasMoved)
                            selected = -1;
                    }

                }

                    /*
                     * wenn doch bewegen oder kämpfen
                     */
                else
                {
                    //Bewegen:  Kollisionen mit anderen figuren beachten
                    if (GStatus.Phase == "move")
                    {
                        if (isOver() < 0&&isSelected())
                        {


                                if (//nicht das spielfeld verlassen//
                                 -20 < maus.Position3d.X && 20 > maus.Position3d.X
                                && -20 < maus.Position3d.Y && 20 > maus.Position3d.Y
                                    //nicht den Kreis verlassen
                                && maus.awayFromMouse(selPlayer.Position) < circ.scale)
                                {
                                    selPlayer.movePoint = new Vector2(maus.Position3d.X, maus.Position3d.Y);
                                    GStatus.moved();
                                    selPlayer.hasMoved = true;
                                    selected = -1;
                                }
                            }
                        
                    }
                        //kill
                    else
                    {

                        if (isOver() != -1)
                        {//verbuendete
                            if ( othPlayer.team == selPlayer.team)
                            {
                                //beide noch nicht geschossen
                                if (
                                selPlayer.shots == 0
                                && othPlayer.shots == 0)
                                {
                                    selected = isOver();
                                }
                            }
                                //gegner
                            else if ( maus.awayFromMouse(selPlayer.Position) < rCirc.scale)
                            {
                                if (selPlayer.shots == selPlayer.Staerke)
                                { selected = -1; }
                                else
                                {
                                    othPlayer.Leben--;
                                    selPlayer.shots++;

                                    if (othPlayer.Leben < 1)
                                    {
                                        int zwispei = isOver();
                                        teamRot[zwispei].Position = othPlayer.movePoint = new Vector2(100, 0);
                                        GStatus.kill(teamRot[zwispei].team);

                                        if (GStatus.GameOver)
                                            Exit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            oldLeftButton = LeftButton;
            oldRightButton = RightButton;
            oldMausWheel = currentMouseState.ScrollWheelValue;
            #endregion
            #region: DrawUpdates

            foreach (GameElement e in teamRot)
            {
                e.update(gameTime, camera);
            }
            //Circlestuff
            circ.update(gameTime, camera);
            rCirc.update(gameTime, camera);
            if (isSelected())
            {
                circ.Position = selPlayer.Position;
                circ.scale = selPlayer.Bewegung*2.5f;

                rCirc.Position = selPlayer.Position;
                rCirc.scale = selPlayer.Reichweite;

            }


            table.update(camera);
            #endregion
            if (GStatus.needToRef)
            {
                refresh();
            }


            pickingSort();
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
                teamRot[i].draw();

            }
            if (isSelected())
            {
                if (GStatus.Phase == "move")
                    circ.draw();
                else
                    rCirc.draw();
            }

            // Gui und 2d Elemente

            spriteBatch.Begin();
            maus.Draw(spriteBatch, LeftButton, isOver()!=-1);
            spriteBatch.End();

            //Schrift
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //spriteBatch.DrawString(font, message, new Vector2(100, 100), Color.Black);
            //spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            base.Draw(gameTime);
        }
        //Picking
        #region:Picking
        public float? IntersectDistance(BoundingSphere sphere )
        {
            Ray mouseRay = maus.MouseRay();
            return mouseRay.Intersects(sphere);
        }
        public bool PickingCheck(Model model, Matrix world)
        {
            foreach (var mesh in model.Meshes)
            {
                BoundingSphere sphere = mesh.BoundingSphere;
                sphere = sphere.Transform(world);
                Ray mouseRay = maus.MouseRay();
                float? distance = mouseRay.Intersects(sphere);

                if (distance != null)
                    return true;                
            }
            return false;
        }
        int isOver ()
        {
            for (int i = 0; i < teamRot.Length;i++ )
            {
                if (PickingCheck(teamRot[i].model, teamRot[i].GetWorldMatrix()))
                    return i;
            }
            return -1;
        }
        public void pickingSort()
        {
            GameElement switcher;
                for (int i = 0; i < teamRot.Length-1; i++)
                {
                    if(teamRot[i].awayFromCamera>teamRot[i+1].awayFromCamera)
                    {
                        switcher=teamRot[i];
                        teamRot[i]=teamRot[i+1];
                        teamRot[i+1]=switcher;
                    }
                }           
        }
        #endregion
        public bool isSelected()
        {
            return selected != -1;
        }
        public void refresh()
        {
            foreach (GameElement e in teamRot)
            {
                e.refresh();
            }
            GStatus.needToRef = false;
        }
    }
}
