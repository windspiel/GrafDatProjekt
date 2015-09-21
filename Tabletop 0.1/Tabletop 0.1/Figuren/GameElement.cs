using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Tabletop_0._1.Figuren
{
    class GameElement
    {
        Model model;
        Vector3 modelPosition;
        public void load(ContentManager Content, String xnb_Name)
        {
            model = Content.Load<Model>("Modelle/"+xnb_Name);
        }

        public void draw(GraphicsDeviceManager graphics)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // We could set up custom lights, but this
                    // is the quickest way to get somethign on screen:
                    effect.EnableDefaultLighting();
                    // This makes lighting look more realistic on
                    // round surfaces, but at a slight performance cost:
                    effect.PreferPerPixelLighting = true;

                    // The world matrix can be used to position, rotate
                    // or resize (scale) the model. Identity means that
                    // the model is unrotated, drawn at the origin, and
                    // its size is unchanged from the loaded content file.
                    effect.World = Matrix.Identity;


                    /*
                     * hat hierdrin nix zu suchen oder??? 
                     */
                    // Move the camera 8 units away from the origin:
                    var cameraPosition = new Vector3(0, 8, 0);
                    // Tell the camera to look at the origin:
                    var cameraLookAtVector = Vector3.Zero;
                    // Tell the camera that positive Z is up
                    var cameraUpVector = Vector3.UnitZ;

                    effect.View = Matrix.CreateLookAt(
                        cameraPosition, cameraLookAtVector, cameraUpVector);

                    // We want the aspect ratio of our display to match
                    // the entire screen's aspect ratio:
                    float aspectRatio =
                        graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                    // Field of view measures how wide of a view our camera has.
                    // Increasing this value means it has a wider view, making everything
                    // on screen smaller. This is conceptually the same as "zooming out".
                    // It also 
                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    // Anything closer than this will not be drawn (will be clipped)
                    float nearClipPlane = 1;
                    // Anything further than this will not be drawn (will be clipped)
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }
                // Now that we've assigned our properties on the effects we can
                // draw the entire mesh
                mesh.Draw();
            }
        }
        public Vector3 position
        {
            get { return modelPosition; }
            set { modelPosition = value; }
        }
    }
}
