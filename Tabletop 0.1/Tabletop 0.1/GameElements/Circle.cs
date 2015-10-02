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
    class Circle: GameElement
    {
        public override void load(ContentManager Content)
        {
            load(Content, "Circle", 1, 0, 0, 0, 1f);
        }       
    }    
}
