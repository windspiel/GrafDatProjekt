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
    class Plane
    {
        VertexPositionTexture[] bodenVerts;
        BasicEffect effect;


        public void Initialize(GraphicsDeviceManager graphics)
        {
            bodenVerts = new VertexPositionTexture[6];
            bodenVerts[0].Position = new Vector3(-20, -20, 0);
            bodenVerts[1].Position = new Vector3(-20, 20, 0);
            bodenVerts[2].Position = new Vector3(20, -20, 0);
            bodenVerts[3].Position = bodenVerts[1].Position;
            bodenVerts[4].Position = new Vector3(20, 20, 0);
            bodenVerts[5].Position = bodenVerts[2].Position;

            effect = new BasicEffect(graphics.GraphicsDevice);
        }
    }
}
