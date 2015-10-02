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
        public Boolean needToRef = false;
        public int Blaue=0, Rote=0;
        private static int BewProRunde=3, AttProRunde=3;
        private int Bew=0,Shots=0;
        public void shoted()
        { 
            Shots+=1;
            if (Shots == AttProRunde)
            {
                Shots = 0;
                nextPhase();
            }
        }

        public void kill(String team)
        {
            if (team == "rot")
                Rote--;
            if (team == "blau")
                Blaue--;
        }

        public void moved()
        {
            Bew += 1;
            if (Bew == BewProRunde)
            {
                Bew = 0;
                nextPhase();
            }
        }
        private void nextTeam()
        {
            if (Turn == "rot")
            {
                Turn = "blau";
                needToRef = true;
                Bew = Shots = 0;
            }
            else
            {
                Turn = "rot";
                needToRef=true;
                Bew = Shots = 0;
            }
        }
        public void nextPhase()
        {
            if (Phase == "move")
                Phase = "shot";
            else
            {
                Phase = "move";
                nextTeam();
            }
        }

    }
}
