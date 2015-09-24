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
        Texture2D texture; 

        Vector3 modelPosition, CameraPosition;
      
        float angle;

        public void load(ContentManager Content, String xnb_Name)
        {
            model = Content.Load<Model>("Modelle/"+xnb_Name);
        }

        public void load(ContentManager Content, String xnb_Name, String texture_Name)
        {
            model = Content.Load<Model>("Modelle/" + xnb_Name);
            texture = Content.Load<Texture2D>("Grafiken/" + texture_Name);
        }

        public void update(Vector3 camPos, float aspectRatio, GameTime gameTime)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            CameraPosition = camPos;
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

                        effect.World = GetWorldMatrix();



                        //effect.World = Matrix.CreateTranslation(modelPosition);

                        var cameraLookAtVector = Vector3.Zero;
                        var cameraUpVector = Vector3.UnitZ;

                        effect.View = Matrix.CreateLookAt(
                            CameraPosition, cameraLookAtVector, cameraUpVector);

                        float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;

                        float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                        float nearClipPlane = 1;
                        float farClipPlane = 200;

                        effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                            fieldOfView, aspectRatio, nearClipPlane, farClipPlane);


                        effect.TextureEnabled = true;
                        effect.Texture = texture;
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
        Matrix GetWorldMatrix()
        {
            const float circleRadius = 8;
            const float heightOffGround = 3;

            // this matrix moves the model "out" from the origin
            Matrix translationMatrix = Matrix.CreateTranslation(
                circleRadius, 0, heightOffGround);

            // this matrix rotates everything around the origin
            Matrix rotationMatrix = Matrix.CreateRotationZ(angle);

            // this matrix scale the models to the same high
            
            Matrix scaleMatrix = Matrix.CreateScale(0.1f);

            // We combine the two to have the model move in a circle:
            Matrix combined = scaleMatrix * translationMatrix * rotationMatrix;

            return combined;
        }
    }
}
