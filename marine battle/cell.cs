using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace marine_battle
{
    enum Cellstat { blank, miss, damage, ship}
    class Cell:Button
    {
        public Cell()
        {
            Cstatus = Cellstat.blank;
            Image = Image.FromFile("data//blank.png");
        }
        public Cellstat Cstatus { get; private set; }        
        public bool shot()
        {
            if (Cstatus == Cellstat.blank)
            {
                Image = Image.FromFile("data//miss.png");
                Cstatus = Cellstat.miss;
                return false;
            }
            else if (Cstatus == Cellstat.ship)
            {
                Image = Image.FromFile("data//damage.png");
                Cstatus = Cellstat.damage;
                return true;
            }
            return true;
        }
        public void PutShip(bool vis)
        {
            if (Cstatus == Cellstat.blank)
            {
                Cstatus = Cellstat.ship;
                if (vis == true)
                    Image = Image.FromFile("data//ship.png");
            }
            else if (Cstatus == Cellstat.ship)
            {
                Cstatus = Cellstat.blank;
                if (vis == true)
                    Image = Image.FromFile("data//blank.png");
            }

        }
    }
}
