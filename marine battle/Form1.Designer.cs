namespace marine_battle
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        static public int sizebox=10;
        private Cell[,] cell = new Cell[sizebox, sizebox];
        private Cell[,] enemyCell = new Cell[sizebox, sizebox];
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";


            for (int i = 0; i < sizebox; i++)
            {
                for (int j = 0; j < sizebox; j++)
                {
                    int size=30;
                    cell[i, j] = new Cell();
                    cell[i, j].Size = new System.Drawing.Size(size, size);
                    cell[i, j].Location = new System.Drawing.Point(i * (size-1), j * (size-1));
                    cell[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    cell[i, j].Click += new System.EventHandler(cellClick);
                    Controls.Add(cell[i, j]);


                    enemyCell[i, j] = new Cell();
                    enemyCell[i, j].Size = new System.Drawing.Size(size, size);
                    enemyCell[i, j].Location = new System.Drawing.Point(350+i * (size - 1), j * (size - 1));
                    enemyCell[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    enemyCell[i, j].Click += new System.EventHandler(enemycellClick);
                }
            }

            endcreate.Size = new System.Drawing.Size(100, 40);
            endcreate.Location = new System.Drawing.Point(10, 400);
            endcreate.Text = "Сохранить";
            endcreate.Click += new System.EventHandler(endcreate_Click);
            endcreate.Visible = false;
            Controls.Add(endcreate);

            info.Size = new System.Drawing.Size(200, 100);
            info.Location = new System.Drawing.Point(10, 300);
            Controls.Add(info);
        }
        #endregion
        System.Windows.Forms.Button endcreate = new System.Windows.Forms.Button();
        System.Windows.Forms.Label info = new System.Windows.Forms.Label();
    }
}

