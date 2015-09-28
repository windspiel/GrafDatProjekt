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
    class SturmEH: GameElement 
    {
        public void load(ContentManager Content)
        {
            load(Content, "MarieneVorab");
        }
    }
}
