﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tabletop_0._1.LogikElement;


namespace Tabletop_0._1.GameElements
{
    class GameElement
    {
        public Model model;      
        public float angle;
        public Vector2 Position, movePoint;
        private Camera cam;

        public virtual void load(ContentManager Content) { }
        public void load(ContentManager Content, String xnb_Name)
        {
            model = Content.Load<Model>("Modelle/"+ xnb_Name);
        }

        public void update( GameTime gameTime, Camera camera)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (movePoint.X != Position.X && movePoint.Y != Position.Y)
            {
                Position = Position + (movePoint - Position) * 0.0001f;
            }
            cam = camera;
        }

        public void draw( )
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = GetWorldMatrix();

                    effect.View = cam.View();
                    effect.Projection = cam.Projection();
                }
                mesh.Draw();
            }
        }
        
        public Matrix GetWorldMatrix()
        {
            const float heightOffGround = 0;

            Matrix translationMatrix = Matrix.CreateTranslation(Position.X, Position.Y, heightOffGround);
            Matrix rotationMatrix = Matrix.CreateRotationZ(0);
            Matrix scaleMatrix = Matrix.CreateScale(0.03f );

            Matrix combined = scaleMatrix * translationMatrix * rotationMatrix;

            return combined;
        }
    }
}
