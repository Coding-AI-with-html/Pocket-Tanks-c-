﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Objects
{
    enum ExplosionType
    {
        nuke, small
    }
    class Explosion_Img
    {
        private List<Image> imgs = new List<Image>();
        public Explosion_Img(ExplosionType explosionType)
        {
            if (explosionType == ExplosionType.nuke)
            {
                for (int i = 0; i < 9; i++)
                {
                    int n = i + 1;
                    imgs.Add(Image.FromFile(@"resourcesnew/explosions/nuke_explosion/explosion0" + n + ".png"));
                }
                for (int i = 9; i < 50; i++)
                {
                    int n = i + 1;
                    imgs.Add(Image.FromFile(@"resourcesnew/explosions/nuke_explosion/explosion" + n + ".png"));
                }
            }
            else if (explosionType == ExplosionType.small)
            {
                for (int i = 0; i < 9; i++)
                {
                    int n = i + 1;
                    imgs.Add(Image.FromFile(@"resourcesnew/explosions/small_explosion/explosion0" + n + ".png"));
                }
                for (int i = 9; i < 31; i++)
                {
                    int n = i + 1;
                    imgs.Add(Image.FromFile(@"resourcesnew/explosions/small_explosion/explosion" + n + ".png"));
                }
            }
        }
        public List<Image> get_Explosion_imgs()
        {
            return imgs;
        }
    }

    class Explosion : GameObject
    {
        private Image Drawed_img;
        private List<Image> imgs;
        public bool isFinished = false;
        int img_indx = 0;
        double old_Height;
        ExplosionType explosionType;
        public Explosion(float x, float y, ExplosionType explosionType)
            : base(x, y)
        {
            Explosion_Img eim = new Explosion_Img(explosionType);
            imgs = eim.get_Explosion_imgs();
            Drawed_img = imgs[0];
            base.Height = Drawed_img.Height;
            base.Width = Drawed_img.Width;
            this.explosionType = explosionType;
        }
        public void StartExplosion(double frame_no)
        {

            if (explosionType == ExplosionType.nuke)
            {
                if (frame_no % 1 == 0)
                {
                    old_Height = imgs[img_indx].Height;
                    if (img_indx < imgs.Count - 1)
                    {
                        img_indx++;
                    }
                    else
                    {
                        isFinished = true;
                    }
                    Drawed_img = imgs[img_indx];
                    Height = imgs[img_indx].Height;
                    Width = imgs[img_indx].Width;
                    Y -= (int)(Drawed_img.Height - old_Height) / 2;
                    X -= (int)(Drawed_img.Height - old_Height) / 2;
                }
            }
            else
            {
                if (frame_no % 1 == 0)
                {
                    old_Height = imgs[img_indx].Height;
                    if (img_indx < imgs.Count - 1)
                    {
                        img_indx++;
                    }
                    else
                    {
                        isFinished = true;
                    }
                    Drawed_img = imgs[img_indx];
                    Height = imgs[img_indx].Height;
                    Width = imgs[img_indx].Width;
                    Y -= (int)(Drawed_img.Height - old_Height) / 2;
                    X -= (int)(Drawed_img.Height - old_Height) / 2;
                }
            }
        }


        public override void Draw(Graphics g)
        {
            g.DrawImage(Drawed_img, new PointF(X, Y));
        }

    }
}
