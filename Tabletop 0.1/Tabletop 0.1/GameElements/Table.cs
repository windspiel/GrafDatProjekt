using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tabletop_0._1.LogikElement;


namespace Tabletop_0._1.Content.GameElements
{
    class Table
    {
        VertexPositionTexture[] bodenVerts;
        BasicEffect effect;
        private Camera cam;   
        Texture2D tableTexture;

        public void Initialize(GraphicsDeviceManager graphics)
        {
            bodenVerts = new VertexPositionTexture[6];

            bodenVerts[0].Position = new Vector3(-20, -20, 0);
            bodenVerts[1].Position = new Vector3(-20, 20, 0);
            bodenVerts[2].Position = new Vector3(20, -20, 0);


            bodenVerts[3].Position = bodenVerts[1].Position;
            bodenVerts[4].Position = new Vector3(20, 20, 0);
            bodenVerts[5].Position = bodenVerts[2].Position;
            //Texture
            int repeat = 20;

            bodenVerts[0].TextureCoordinate = new Vector2(0, 0);
            bodenVerts[1].TextureCoordinate = new Vector2(0, repeat);
            bodenVerts[2].TextureCoordinate = new Vector2(repeat, 0);

            bodenVerts[3].TextureCoordinate = bodenVerts[1].TextureCoordinate;
            bodenVerts[4].TextureCoordinate = new Vector2(repeat, repeat);
            bodenVerts[5].TextureCoordinate = bodenVerts[2].TextureCoordinate;

            effect = new BasicEffect(graphics.GraphicsDevice);
        }

        public void Load(GraphicsDevice graphicsDevice)
        {
            // We aren't using the content pipeline, so we need
            // to access the stream directly:
            using (var stream = TitleContainer.OpenStream("Content/Grafiken/Waben.png"))
            {
                tableTexture = Texture2D.FromStream(graphicsDevice, stream);
            }
        }

        public void update(Camera Camera)
        {
            cam = Camera;
        }

        public void DrawGround(GraphicsDeviceManager graphics)
        {
            // The assignment of effect.View and effect.Projection
            // are nearly identical to the code in the Model drawing code.
            //var cameraPosition = new Vector3(0, 40, 20);

            effect.View = cam.View();

            effect.Projection = cam.Projection();

            effect.TextureEnabled = true;
            effect.Texture = tableTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, bodenVerts, 0, 2);
            }
        }
    }
}
