using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tabletop_0._1.GameElements
{
    class TermiEH : GameElement
    {
        public override void load(ContentManager Content)
        {
            //load(Content, "robot");
            //Leb Bew ST RW 
            load(Content, "TermiRot", 10, 1, 1, 15, 0.23f);
            team = "rot";
        }
    }
}
