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
        public float angle=0,scale;
        public Vector2 Position, movePoint, oldPosition;
        private Camera cam;
        public String team="stein";
        public int Leben, Bewegung, Staerke, Reichweite, shots=0 ;
        public Boolean hasShot = false, hasMoved = false;

        public float awayFromCamera
        {
            get { return (cam.cameraPosition.X + cam.cameraPosition.Y) - (Position.X + Position.Y); }
        }
        public Vector3 Position3d
        {
            get{ return new Vector3(Position.X, Position.Y,0.001f) ;}
        }

        public virtual void load(ContentManager Content) { }

        public void load(ContentManager Content, String xnb_Name, int Leb, int Bew ,int St, int Rw, float scal)
        {
            Leben = Leb;
            Bewegung = Bew;
            Staerke = St;
            Reichweite = Rw;
            model = Content.Load<Model>("Modelle/"+ xnb_Name);
            scale = scal;
        }
        private void winkel()
        {
            Vector2 spuck = movePoint - Position;
            spuck.Normalize();
            angle = (float)(Math.Atan2(spuck.Y,spuck.X)+Math.PI/2);
        }



        public void update( GameTime gameTime, Camera camera)
        {
            //animation
 
            //Walk

            Vector2 diff = movePoint-Position;
            if (diff.X < 0)
                diff.X *= -1;

            if (diff.Y < 0)
                diff.Y *= -1;
            float tolerance = 0.1f;
            if (diff.X > tolerance || diff.Y > tolerance)
            {
                    winkel();
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
                    //effect.Alpha = 0.5f;
                }
                mesh.Draw();
            }
        }
        
        public Matrix GetWorldMatrix()
        {
            const float heightOffGround = 0;

            Matrix translationMatrix = Matrix.CreateTranslation(Position.X, Position.Y, heightOffGround+0.001f);
            Matrix rotationMatrix = Matrix.CreateRotationZ(angle);            
            Matrix scaleMatrix = Matrix.CreateScale(scale );

            Matrix combined = scaleMatrix *  rotationMatrix*translationMatrix ;

            return combined;
        }
    }
}
