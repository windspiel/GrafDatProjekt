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
        float doof;

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

        private float ControlX
        {
            set {
                if (value + cameraLookAtVector.X > -20 && value + cameraLookAtVector.X < 20)
                {
                    cameraPosition.X += value;
                    cameraLookAtVector.X += value;
                }
            }
        }

        private float ControlY
        {
            set
            {
                if (value + cameraLookAtVector.Y > -20 && value + cameraLookAtVector.Y < 20)
                {
                    cameraPosition.Y += value;
                    cameraLookAtVector.Y += value;
                }
                else
                {
                }

            }
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
                ControlX=-s;

                ControlY=-s;
            }
            //untenlinks
            if (keyboard.IsKeyDown(Keys.S))
            {
                ControlX=s;

                ControlY=s;

            }
            //unten rechts
            if (keyboard.IsKeyDown(Keys.D))
            {
                ControlX=-s;

                ControlY=s;

            }
            //oben links
            if (keyboard.IsKeyDown(Keys.A))
            {
                ControlX=s;

                ControlY=-s;
            }

            if (cameraPosition.Z + iwas * 0.005f > 0)
            {
              cameraPosition.Z = cameraPosition.Z + iwas*0.01f;
            }
        }
    }
}
