using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tabletop_0._1.GameElements;


namespace Tabletop_0._1.LogikElement
{
    class Spielstatus
    {
        public String Turn ="rot";
        public String Phase = "move";
        public Boolean GameOver
        {
            get { return Blaue == 0 || Rote == 0; }
        }
      
        public int Blaue=0, Rote=0;
        private static int BewProRunde=3, AttProRunde=3;
        public void nextTeam()
        {
            if (Turn == "rot")
                Turn = "blau";
            else
                Turn = "rot";
        }
        public void nextPhase()
        {
            if (Phase == "move")
                Phase = "shot";
            else
                Phase = "move";
        }

    }
}
