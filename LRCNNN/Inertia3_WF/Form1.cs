using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inertia3_WF
{
    public partial class Form1 : Form
    {
        public Bitmap PlayerTexure = Resource1.player, 
                      PrizeTexure = Resource1.Prize,                      
                      WallTexure = Resource1.wall,                      
                      StairsTexure = Resource1.Stairs,                      
                      EmptyTexure = Resource1.Empty;

        Map map = new Map(11, 11);
        Game game = new Game();
        Player player;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //char direction = Convert.ToChar(e.KeyValue);
            var theKeyAsAString = e.KeyCode.ToString();
            var theKeyAsAChar = Convert.ToChar(theKeyAsAString);
            //player.Move(theKeyAsAChar);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            player = new Player(map, game);
            map.Generate();
            //player.Draw();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*Graphics g = e.Graphics;
            
            g.DrawImage(PlayerTexure, new Rectangle(0,0,100,100));
            g.DrawImage(PrizeTexure, new Rectangle(0,0,100,100));
            */
            Graphics g = e.Graphics;
            player.Draw(e.Graphics);
        }

        public static void PictureDraw(Bitmap bitmap, int x, int y, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            g.DrawImage(bitmap, new Rectangle(x, y, 50, 50));
        }
    }
}
