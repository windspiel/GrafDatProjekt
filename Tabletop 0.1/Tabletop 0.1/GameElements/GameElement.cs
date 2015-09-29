using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Tabletop_0._1.GameElements
{
    class GameElements
    {
        public Model model;      
        float angle;

        public void load(ContentManager Content, String xnb_Name)
        {
            model = Content.Load<Model>("Modelle/"+xnb_Name);
        }

        public void update( GameTime gameTime)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void draw(Vector3 camPos, GraphicsDeviceManager graphics, Vector3 Position)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = GetWorldMatrix();

                    //effect.World = Matrix.CreateTranslation(modelPosition);

                    var cameraLookAtVector = Vector3.Zero;
                    var cameraUpVector = Vector3.UnitZ;

                    effect.View = Matrix.CreateLookAt(
                        camPos, cameraLookAtVector, cameraUpVector);

                    float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;

                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }
                mesh.Draw();
            }
        }

        public Matrix GetWorldMatrix()
        {
            const float circleRadius = 0;
            const float heightOffGround = 0;

            Matrix translationMatrix = Matrix.CreateTranslation(circleRadius, 0, heightOffGround);
            Matrix rotationMatrix = Matrix.CreateRotationZ(-angle);
            Matrix scaleMatrix = Matrix.CreateScale(0.1f * angle);

            Matrix combined = scaleMatrix * translationMatrix * rotationMatrix;

            return combined;
        }
    }
}
