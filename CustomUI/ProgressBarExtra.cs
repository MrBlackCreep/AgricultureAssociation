using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace AgricultureAssociation.CustomUI
{
    class ProgressbarExtra : BaseMenuComponent
    {

        /*********
** Fields
*********/
        protected Rectangle Background;
        protected Rectangle Filler;
        protected int _Value;
        protected int MaxValue;
        protected int OffsetValue;
        protected Color BarBg;
        protected Color BarFill;
        protected int Height;


        /*********
        ** Accessors
        *********/
        public int Value
        {
            get => this._Value;
            set => this._Value = Math.Max(0, Math.Min(this.MaxValue, value));
        }


        /*********
        ** Public methods
        *********/
        public ProgressbarExtra(Point position, int value, int maxValue, int height = 6, Color? barBg = null, Color? barFill = null)
        {
            BarBg = barBg ?? Color.White;
            BarFill = barFill ?? Color.White;

            this.MaxValue = maxValue;
            this.Value = value;
            this.Background = new Rectangle(403, 383, 6, 6);
            this.Filler = new Rectangle(306, 320, 16, 16);
            this.OffsetValue = this.Value * Game1.pixelZoom;
            this.SetScaledArea(new Rectangle(position.X, position.Y, this.MaxValue + 2, height));
            Height = height;
        }


        public override void Draw(SpriteBatch b, Point o)
        {
            if (DateTime.Now.Millisecond % 5 == 0)
                this.OffsetValue += this.GetDiff();
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, Background, this.Area.X + o.X, this.Area.Y + o.Y, this.Area.Width, this.Area.Height, BarBg, Game1.pixelZoom, false);
            b.Draw(Game1.mouseCursors, new Rectangle(this.Area.X + o.X + Game1.pixelZoom, this.Area.Y + o.Y + Game1.pixelZoom, this.OffsetValue, Zoom4 + (int) (Game1.pixelZoom*(Height-6))), Filler, BarFill);
           
        }


        /*********
        ** Protected methods
        *********/
        protected int GetDiff()
        {
            int v = this._Value * Game1.pixelZoom;
            if (this.OffsetValue == v)
                return 0;
            if (this.OffsetValue > v)
                return -(int)Math.Floor((this.OffsetValue - v) / 10D + 1);
            return (int)Math.Floor((v - this.OffsetValue) / 10D + 1);
        }
    }
}
