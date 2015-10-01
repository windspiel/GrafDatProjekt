using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tabletop_0._1.LogikElement
{
    class Camera
    {
        public Vector3 cameraPosition = new Vector3(15,10,10);
        private GraphicsDeviceManager graphMan;
        public Vector3 cameraLookAtVector = Vector3.Zero;
        float s = 0.5f;

        public Matrix View()
        {
            Vector3 cameraUpVector = Vector3.UnitZ;
            return Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector); 
        }

        public Matrix Projection()
        {
            float 
                aspectRatio = graphMan.PreferredBackBufferWidth / (float)graphMan.PreferredBackBufferHeight,
                fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4,
                nearClipPlane = 1,
                farClipPlane = 200;
             return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane); 
        }

        public void initial(GraphicsDeviceManager graphics)
        {
            update(new Vector3(15, 10, 10), graphics);
        }


        public void update(Vector3 position, GraphicsDeviceManager graphics)
        {
            cameraPosition = position;
            graphMan = graphics;
        }

        public void steuern(KeyboardState keyboard, float iwas)
        {
            //oben rechts
            if (keyboard.IsKeyDown(Keys.W))
            {
                cameraPosition.X-=s;
                cameraLookAtVector.X-=s;

                cameraPosition.Y-=s;
                cameraLookAtVector.Y-=s;
            }
            //untenlinks
            if (keyboard.IsKeyDown(Keys.S))
            {
                cameraPosition.X+=s;
                cameraLookAtVector.X+=s;

                cameraPosition.Y+=s;
                cameraLookAtVector.Y+=s;
            }
            //unten rechts
            if (keyboard.IsKeyDown(Keys.D))
            {
                cameraPosition.X-=s;
                cameraLookAtVector.X-=s;

                cameraPosition.Y+=s;
                cameraLookAtVector.Y+=s;
            }
            //oben links
            if (keyboard.IsKeyDown(Keys.A))
            {
                cameraPosition.X+=s;
                cameraLookAtVector.X+=s;

                cameraPosition.Y-=s;
                cameraLookAtVector.Y-=s;
            }

            if (cameraPosition.Z + iwas * 0.005f > 0)
            {
              cameraPosition.Z = cameraPosition.Z + iwas*0.01f;
            }
        }
    }
}
