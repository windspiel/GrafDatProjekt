using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tabletop_0._1.GameElements
{
    class CptnBA : GameElement
    {
        public override void load(ContentManager Content)
        {
            //load(Content, "robot");
            //Leb Bew ST RW 
            load(Content, "cptblau", 5, 8, 6, 3, 0.17f);
            team = "blau";
        }
    }
}