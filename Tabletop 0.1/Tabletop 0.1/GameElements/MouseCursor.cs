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
    class MouseCursor
    {
  
            //Draw
            private Texture2D MouseTexture22, MouseTexture33, MouseTexture11;
            public Vector2 Position;
            public bool Active;

            public int Width
            {
                get { return MouseTexture11.Width; }
            }
            public int Height
            {
                get { return MouseTexture11.Height; }
            }

            public void Draw(SpriteBatch spriteBatch, ButtonState LeftMouse, Boolean mouseOverSomething)
            {
                if (LeftMouse == ButtonState.Pressed)
                    spriteBatch.Draw(MouseTexture22, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                
                else
                {
                    if (mouseOverSomething)
                        spriteBatch.Draw(MouseTexture33, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    else
                        spriteBatch.Draw(MouseTexture11, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }

            public void Initialize(ContentManager Content, Vector2 position)
            {

                MouseTexture11 = Content.Load<Texture2D>("Grafiken/Maus11");
                MouseTexture22 = Content.Load<Texture2D>("Grafiken/Maus22");
                MouseTexture33 = Content.Load<Texture2D>("Grafiken/Maus33");
                Position = position;
                Active = true;

            }

            public Ray MouseRay(Matrix view, Matrix projection, Viewport viewport)
            {
                //Berechnet die Theoretiche Mauskoordinate im 3D Raum
                Vector3 nearPoint = viewport.Unproject(new Vector3(Position.X,Position.Y, 0.0f), 
                    projection, 
                    view, 
                    Matrix.Identity);
 
                //Brechnet die Theoretische Mauskoordinate wenn der Bildschirm einen schritt weiter Forme waere
                Vector3 farPoint = viewport.Unproject(new Vector3(Position.X,Position.Y, 1.0f),
                    projection,
                    view,
                    Matrix.Identity);
 
                Vector3 direction = farPoint - nearPoint;
                direction.Normalize();

                return new Ray(nearPoint, direction);
            }
        }
    }

