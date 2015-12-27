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

            int maxDistance = 21;
            switch (e.KeyCode)
            {
                case Keys.F2: this.Close(); break;
                case Keys.W: world.RotationX -= 4.0f; break;
                case Keys.S: world.RotationX += 4.0f; break;
                case Keys.A: world.RotationY -= 4.0f; break;
                case Keys.D: world.RotationY += 4.0f; break;
                case Keys.Subtract: {
                      
                            world.cameraControl -= .3f;
                       

                        break; }
                case Keys.OemMinus:
                    {

                        world.cameraControl -= .3f;


                        break;
                    }
                case Keys.Oemplus:
                    {

                        world.cameraControl += .3f;


                        break;
                    }
                case Keys.Add:
                    {
                        
                        world.cameraControl += .3f;


                        break;
                    }


            }

            this.Refresh();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            
            if (cd.ShowDialog() == DialogResult.OK)
            {
                world.light_red = cd.Color.R;
                world.light_green = cd.Color.G;
                world.light_blue = cd.Color.B;
                this.Refresh();
                //MessageBox.Show("RED: " + red);
            }
        }
    }
}
