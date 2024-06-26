﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Objects
{
    public enum PlayerType
    {
        Opponent, MyPlayer
    }
    class Player : GameObject
    {
        private static int moves = 5;  //each tank starts a new game with 5 available moves 
        private Image Player_Image;// = Image.FromFile(@"resourcesnew\tank1\tank1.png");
        public float Width_Player;// = Player_Image.Width;
        public float Height_Player;// = Player_Image.Height;
        public GamePanel gp;
        //Label lbl = new Label();
        Pen pen;
        Pen dashed_pen;
        PlayerType PlayerType;


        private Fire fire;
        public int angle = 60; //angle of the shooted fire
        public Power power;//power of shooted fire
        protected bool turn = false;//the turn of this player to decide wether to shoot or not
        public bool fired = false;
        public bool isPowerAngle_Recieved;
        int turns_count = 1;//used to change the fire type every turn
        public int Health { get; set; }
        List<PointF> path_pts;
        float ground_Y;

        public Player(float x, float y, GamePanel gp, PlayerType playerType) : base(x, y)
        {

            if (playerType == PlayerType.Opponent)
            {
                Player_Image = Image.FromFile(@"resourcesnew\tank2\tank1.png");
            }
            else
            {
                Player_Image = Image.FromFile(@"resourcesnew\tank1\tank1.png");
            }
            Y -= Player_Image.Height;
            base.Height = Player_Image.Height;
            base.Width = Player_Image.Width;
            this.gp = gp;
            if (X > gp.Width / 2)
            {
                angle = 120;
            }
            pen = new Pen(Color.FromArgb(80, 68, 22), 5);
            dashed_pen = new Pen(Color.SandyBrown, 2.7f);
            dashed_pen.DashStyle = DashStyle.Dot;
            this.PlayerType = playerType;
            Health = 100;

        }
        public void Start_Turn()
        {
            isPowerAngle_Recieved = false;
            turn = true;
            fired = false;
            MouseManager.is_Left_Btn_Released = false;
            if (turns_count % 2 == 0)
            {
                fire = new Fire((X + (Width / 2) - 19), Y - 10, FireType.Single_Shot);
            }
            else
            {
                fire = new Fire((X + (Width / 2) - 19), Y - 10, FireType.Cutter);
            }
            fire.X += fire.Width / 2;
            turns_count++;

        }
        public void End_Turn()
        {
            turn = false;
            MouseManager.is_Left_Btn_Released = false;
            fire = null;
            isPowerAngle_Recieved = false;
            fired = false;
        }
        public bool isTurnFinished()
        {
            if (fire != null)
            {

                return fire.isFinished;
            }
            else
                return false;
        }
        public void Shoot()
        {
            if (turn && MouseManager.is_Left_Btn_Released && fire != null && PlayerType == PlayerType.MyPlayer && isPower_higher_lowest_val())
            {
                SoundPlayer cannon = new SoundPlayer(@"resourcesnew\audio\cannonpop.wav");
                cannon.Play();
                fire.ShootFire(angle, power);
                fired = true;
                Console.WriteLine("--------------PLAYER-------------------");
                Console.WriteLine("FIRED");
                Console.WriteLine("Angele: {0}     Power: {1}", angle, power);
                MouseManager.is_Left_Btn_Released = false;
                Console.WriteLine(fire);
                Console.WriteLine("------------------------------------");

                // gp.Controls.Remove(lbl);
                turn = false;
            }
            else if (PlayerType == PlayerType.Opponent && turn && isPowerAngle_Recieved)
            {

                SoundPlayer cannon = new SoundPlayer(@"resourcesnew\audio\cannonpop.wav");
                cannon.Play();
                fire.ShootFire(angle, power);
                fired = true;
                Console.WriteLine("--------------OPPONENT-------------------");
                Console.WriteLine("FIRED");
                Console.WriteLine("Angele: {0}     Power: {1}", angle, power);

                Console.WriteLine(fire);
                Console.WriteLine("------------------------------------");

                // gp.Controls.Remove(lbl);
                turn = false;
            }
            else if (MouseManager.is_Left_Btn_Released == true && fired == false)
            {
                MouseManager.is_Left_Btn_Released = false;
            }
        }
        public void Update(float ground_Y, double frame_no)
        {
            if (MouseManager.getMouseState(MouseButtons.Left) && turn && PlayerType == PlayerType.MyPlayer)
            {
                Calc_Angle_Power();
                int power_val = (int)power.getPower_Val();
            }
            Shoot();
            if (fire != null && fired)
            {
                fire.Update(ground_Y, frame_no);
                if (fire.Y - fire.Height > ground_Y)
                {
                    fire.Y = ground_Y - this.Height;
                    fire.Explode(ground_Y - this.Height);
                }
            }
            this.ground_Y = ground_Y;
        }
        private void Calc_Angle_Power()
        {
            int startX = MouseManager.X;
            int startY = MouseManager.Y;
            int endX = gp.PointToClient(Cursor.Position).X;
            int endY = gp.PointToClient(Cursor.Position).Y;
            int distY = startY - endY;
            int distX = startX - endX;
            double dist_Magn = Math.Sqrt((distX * distX) + (distY * distY));
            const int Max_Dist = 200;
            double angle_degree = Math.Atan2(-distY, distX) * (180.0 / Math.PI);
            angle = (int)angle_degree;
            power = new Power((dist_Magn / Max_Dist) * 100);
        }
        private bool isPower_higher_lowest_val()
        {
            return (power.getPower_Val() > 35 && (angle > 5 && angle < 175));
        }
        public Fire getShootedFire()
        {
            return fire;
        }
        public void Move_Right(Player p)
        {
            if (moves > 0)
            {
                p.X += 10;
                moves--;
            }
        }
        public void Move_Left(Player p)
        {
            if (moves > 0)
            {
                p.X -= 10;
                moves--;
            }
        }
        public int get_Remaining_Moves(Player p)
        {
            return moves;
        }

        private void DrawTankPipe(Graphics g)
        {
            const int len = 30;
            double angle_rad = angle * (Math.PI / 180.0);
            float x1 = X + Width / 2;
            float y1 = Y;
            float x2 = x1 + (float)(len * Math.Cos(angle_rad));
            float y2 = y1 - (float)(len * Math.Sin(angle_rad));
            g.DrawLine(pen, new PointF(x1, y1), new PointF(x2, y2));
        }
        public void DrawFire(Graphics g)
        {
            if (fire != null && fired)
            {
                fire.Draw(g);
            }// Draw Fire 
        }
        public override void Draw(Graphics g)
        {

            if (MouseManager.getMouseState(MouseButtons.Left) && turn && PlayerType == PlayerType.MyPlayer && isPower_higher_lowest_val())
            {
                Physics.DrawFirePath_movingBalls(angle, ground_Y, power, this, g);
            }
            g.DrawImage(Player_Image, new PointF(X, Y));
            DrawTankPipe(g);



        }

        public string GetAngleAndPower()
        {
            if (power != null)
            {
                return (angle + " " + power.ToString());
            }
            else
            {
                return null;
            }

        }

    }
}
