using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



//using Microsoft.Xna.Framework.Graphics.VertexPositionColor;
//using Microsoft.Xna.Framework.Graphics.VertexPositionColorTexture;
//using Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture;
//using Microsoft.Xna.Framework.Graphics.VertexPositionTexture;


namespace Tabletop_0._1.Content.Meshes
{
    class Table
    {
        VertexPositionTexture[] bodenVerts;
        BasicEffect effect;

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

            bodenVerts[0].TextureCoordinate = new Vector2(0, 0);
            bodenVerts[1].TextureCoordinate = new Vector2(0, 1);
            bodenVerts[2].TextureCoordinate = new Vector2(1, 0);

            bodenVerts[3].TextureCoordinate = bodenVerts[1].TextureCoordinate;
            bodenVerts[4].TextureCoordinate = new Vector2(1, 1);
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

        public void DrawGround(GraphicsDeviceManager graphics)
        {
            // The assignment of effect.View and effect.Projection
            // are nearly identical to the code in the Model drawing code.
            var cameraPosition = new Vector3(0, 40, 20);
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
