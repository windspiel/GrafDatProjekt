using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tabletop_0._1.GameElements
{
    class TermiBA : GameElement
    {
        public override void load(ContentManager Content)
        {
            //load(Content, "robot");
            //Leb Bew ST RW 
            load(Content, "TermiBlau", 10, 1, 1, 15, 0.23f);
            team = "blau";
        }
    }
}