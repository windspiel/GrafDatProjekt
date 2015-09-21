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

        public void draw(GraphicsDeviceManager graphics, Vector3 Position)
        {
            modelPosition = Position;
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = Matrix.CreateTranslation(modelPosition);
                    var cameraPosition = new Vector3(0, 10, 0);
                    var cameraLookAtVector = Vector3.Zero;
                    var cameraUpVector = Vector3.UnitZ;
                    effect.View = Matrix.CreateLookAt(
                        cameraPosition, cameraLookAtVector, cameraUpVector);
                    float aspectRatio =
                        graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    float nearClipPlane = 1;
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
