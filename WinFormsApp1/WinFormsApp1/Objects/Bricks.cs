using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Objects
{
    class Bricks : GameObject
    {
        Image drawed_img;
        public Bricks(float x, float y)
            : base(x, y)
        {
            try
            {
                drawed_img = Image.FromFile(@"resourcesnew/bricks/brick3.png");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception from Brick ctor: " + e.Message);
            }
            Width = drawed_img.Width;
            Height = drawed_img.Height;
        }
        public Bricks(float x, float y, float width, float height)
            : base(x, y)
        {
            try
            {
                drawed_img = Image.FromFile(@"resourcesnew/bricks/brick3.png");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception from Brick ctor: " + e.Message);
            }
            Width = width;
            Height = height;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(drawed_img, X, Y, Width, Height);
        }
    }
    class BricksBuilder
    {
        public static List<Bricks> brs = new List<Bricks>();
        public int Br_Width { get; }
        public int Br_Height { get; }
        public int Beginnig_Y { get; }
        public BricksBuilder(GamePanel gp, int br_rows)
        {
            Br_Width = 100;
            Br_Height = 50;
            Beginnig_Y = gp.Height - Br_Height * br_rows;
            for (int i = Beginnig_Y; i < gp.Height; i += Br_Height)
            {
                for (int j = 1; j < gp.Width - Br_Width; j += Br_Width)
                {
                    brs.Add(new Bricks(j, i, Br_Width, Br_Height));
                }
            }
        }
        public void draw(Graphics g)
        {
            foreach (Bricks br in brs)
            {
                br.Draw(g);
            }
        }
    }
}
