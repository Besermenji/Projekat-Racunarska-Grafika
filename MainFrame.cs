using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekat
{
    public partial class MainFrame : Form
    {
        World world = null;


        public MainFrame()
        {
            InitializeComponent();
            canvas.InitializeContexts();
            try {
                world = new World(@"C:\Users\Vladimir\Desktop\Racunarska grafika\Projekat\Projekat\Projekat\star wars x-wing", "star wars x-wing.3ds", canvas.Width,canvas.Height);
            }
            catch {
                MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            world.Draw();
        }

        private void canvas_Resize(object sender, EventArgs e)
        {
            base.OnResize(e);
            world.Height = canvas.Height;
            world.Width = canvas.Width;
            world.Resize();
        }

       

        private void MainFrame_Load(object sender, EventArgs e)
        {

        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10: this.Close(); break;
                case Keys.W: world.RotationX -= 5.0f; break;
                case Keys.S: world.RotationX += 5.0f; break;
                case Keys.A: world.RotationY -= 5.0f; break;
                case Keys.D: world.RotationY += 5.0f; break;
            }

            this.Refresh();
        }
    }
}
