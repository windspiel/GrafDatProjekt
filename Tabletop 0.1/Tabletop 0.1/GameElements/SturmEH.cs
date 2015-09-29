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
    class SturmEH: LoadedModel 
    {
        public void load(ContentManager Content)
        {
            load(Content, "MarieneVorab");
        }
    }
}
