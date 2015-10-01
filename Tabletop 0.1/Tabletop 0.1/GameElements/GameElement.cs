using System;
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
        public Vector2 Position, movePoint, oldPosition;
        private Camera cam;
        public String team="stein";
        private int Leben, Bewegung, Staerke, Reichweite ;

        public float awayFromCom
        {
            get { return (cam.cameraPosition.X + cam.cameraPosition.Y) - (Position.X + Position.Y); }
        }


        public virtual void load(ContentManager Content) { }

        public void load(ContentManager Content, String xnb_Name, int Leb, int Bew ,int St, int Rw)
        {
            Leben = Leb;
            Bewegung = Bew;
            Staerke = St;
            Reichweite = Rw;
            model = Content.Load<Model>("Modelle/"+ xnb_Name);
        }

        public void update( GameTime gameTime, Camera camera)
        {
            //animation
            //angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
 
            Vector2 diff = movePoint-Position;
            if (diff.X < 0)
                diff.X *= -1;

            if (diff.Y < 0)
                diff.Y *= -1;

            float tolerance = 0.1f;
            if (diff.X > tolerance || diff.Y > tolerance)
            {
                    Position = Position + (movePoint - oldPosition) * 0.05f;
            }
            else
                oldPosition = Position;
            
            //camera update
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
