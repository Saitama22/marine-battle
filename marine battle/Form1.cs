using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marine_battle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        (int, int) detBut()
        {
            (int, int) actBut = (0, 0);
            for (int i = 0; i < sizebox; i++)
            {
                for (int j = 0; j < sizebox; j++)
                {
                    if (cell[i, j].Focused == true|| enemyCell[i, j].Focused == true)
                        actBut = (i, j);
                }
            }
            return actBut;
        }
        void cellClick(object sender, EventArgs e)
        {
            (int, int) actBut = detBut();
            cell[actBut.Item1, actBut.Item2].PutShip(true);
            bool endCreate = Canminer();
            //if (endCreate == true)
                endcreate.Visible = true;
            //else
            //    endcreate.Visible = false;
        }
        bool Canminer()
        {
            int maxrowY = 0;
            int maxrowX = 0;
            int[] sundeck = new int[11];

            for (int i = 0; i < sizebox; i++)
            {
                for (int j = 0; j < sizebox; j++)
                {
                    if (cell[i, j].Cstatus == Cellstat.ship)
                        maxrowY++;
                    if (cell[i, j].Cstatus != Cellstat.ship || j == 9)
                    {
                        if (maxrowY == 1)
                        {
                            int are9 = 1;
                            if (j == 9 && cell[i, j].Cstatus == Cellstat.ship)
                                are9 = 0;
                            if ((i == 0 || cell[i - 1, j - are9].Cstatus == Cellstat.blank) && (i == 9 || cell[i + 1, j - are9].Cstatus == Cellstat.blank))
                                sundeck[maxrowY]++;
                        }
                        if (maxrowY > 1)
                            sundeck[maxrowY]++;
                        maxrowY = 0;
                    }

                    if (cell[j, i].Cstatus == Cellstat.ship)
                        maxrowX++;
                    if (cell[j, i].Cstatus != Cellstat.ship || j == 9)
                    {
                        if (maxrowX > 1)
                            sundeck[maxrowX]++;
                        maxrowX = 0;
                    }
                }
                maxrowY = 0;
                maxrowX = 0;
            }
            info.Text = "";
            for (int i = 1; i < sizebox - 1; i++)
            {
                for (int j = 1; j < sizebox - 1; j++)
                {
                    if (cell[i, j].Cstatus == Cellstat.ship)
                    {
                        if (cell[i + 1, j + 1].Cstatus == Cellstat.ship || cell[i + 1, j - 1].Cstatus == Cellstat.ship
                         || cell[i - 1, j + 1].Cstatus == Cellstat.ship || cell[i - 1, j + 1].Cstatus == Cellstat.ship)
                        {
                            info.Text += "Нельзя ставить корабли рядом по диагонали.";
                            return false;
                        }
                    }
                }
            }
            
            for (int i = 1; i < 5; i++)
            {
                info.Text += i + "-палубников: " + sundeck[i] + "\n";
            }

            for (int i = 1; i < sundeck.Length; i++)
            {
                if (i < 5)
                {
                    if (sundeck[i] != 5 - i)
                        return false;
                }
                else if (sundeck[i] != 0)
                    return false;
            }
            return true;
        }
        void endcreate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sizebox; i++)
            {
                for (int j = 0; j < sizebox; j++)
                {
                    cell[i, j].Click -= new EventHandler(cellClick);
                    Controls.Add(enemyCell[i, j]);
                }
            }
            //for (int i = 0; i < sizebox; i++)
            //{
            //    for (int j = 0; j < sizebox; j++)
            //    {
            //        enemyCell[i, j].Cstatus = Cellstat.blank;
            //        enemyCell[i, j].Image = Image.FromFile("data//blank.png");
            //    }
            //}
            List<Tuple<int, int>> reservCells = new List<Tuple<int, int>>();
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int randShipX, randShipY, vert0hor1;
                    while (true)
                    {
                        Random rand = new Random();
                        vert0hor1 = rand.Next(0, 2);
                        randShipX = rand.Next(0, 10);
                        randShipY = rand.Next(0, 10);

                        if (vert0hor1 == 0)                
                            randShipY = rand.Next(0, 10 - 4 + i);
                        else
                            randShipX = rand.Next(0, 10 - 4 + i);

                        if (enrandProof(randShipY, randShipX, i, reservCells, vert0hor1))
                            break;                
                    }
                    for (int z = 0; z < 5-i; z++)
                    {
                        if (vert0hor1 == 0)
                        {
                            Tuple<int, int> tempTurp = new Tuple<int, int>(randShipY+z, randShipX);
                            reservCells.Add(tempTurp);
                            enemyCell[randShipY + z, randShipX].PutShip(false);
                        }
                        else
                        {
                            Tuple<int, int> tempTurp = new Tuple<int, int>(randShipY, randShipX+z);
                            reservCells.Add(tempTurp);
                            enemyCell[randShipY, randShipX + z].PutShip(false);
                        }
                    }
                }                
            }
            Controls.Remove(info);
        }
        bool enrandProof(int x, int y, int i, List<Tuple<int, int>> reservCells, int horOrVert)
        {
            for (int z = 0; z < 5 - i; z++)
            {
                for (int o = -1; o < 2; o++)
                {
                    for (int g = -1; g < 2; g++)
                    {
                        Tuple<int, int> tempTurp;
                        if (horOrVert == 0)
                            tempTurp = new Tuple<int, int>(x + o+z, y + g);
                        else
                            tempTurp = new Tuple<int, int>(x + o, y + g+z);
                        if (reservCells.Contains(tempTurp))
                            return false;
                    }
                }
            }
            return true;
        }

        void enemycellClick(object sender, EventArgs e)
        {
            (int, int) actbut = detBut();
            if (!enemyCell[actbut.Item1, actbut.Item2].shot())
            {
                int x,  y;
                while (true)
                {
                    Random rand = new Random();
                    x = rand.Next(0, 10);
                    y= rand.Next(0, 10);
                    if (cell[x, y].Cstatus == Cellstat.ship || cell[x, y].Cstatus == Cellstat.blank)
                    {
                        if (cell[x, y].shot() == false)
                            break;
                    }
                }
            }

            bool endgame = true; 
            for (int i = 0; i < sizebox; i++)
            {
                for (int j = 0; j < sizebox; j++)
                {
                    if (enemyCell[i, j].Cstatus == Cellstat.ship || cell[i, j].Cstatus == Cellstat.ship)
                        endgame = false;
                }
            }
            if (endgame== true)
            {
                for (int i = 0; i < sizebox; i++)
                {
                    for (int j = 0; j < sizebox; j++)
                    {
                        enemyCell[i, j].Click -= enemycellClick;
                    }
                }
            }
        }
    }
}
