using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Objects;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {

        bool goLeft, goRight;
        int speed = 8;

        

        public static Form2 instance;
        public Label lab1;
        public Label lab2;
        public Form2()
        {
            InitializeComponent();
            instance = this;
            lab1 = label1;
            lab2 = label2;

            RestartGame();

        }

        private void MainGame(object sender, EventArgs e)
        {

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= 12;
               

            }

            if(goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 12;
                

            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;

            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;

            }

        }

        private void RestartGame()
        {

            
            goLeft = false;
            goRight = false; 

            GameTimer.Start();



        }
    }
}
