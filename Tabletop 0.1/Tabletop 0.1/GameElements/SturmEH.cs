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
    class SturmEH: GameElement 
    {
        public override void load(ContentManager Content)
        {
            //load(Content, "robot");
            load(Content, "MarieneVorab", 3, 2, 2, 50, 0.03f);
            team = "rot";
        }
    }
}
