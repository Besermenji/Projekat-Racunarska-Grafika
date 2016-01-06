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
        bool par = false;
        long animation_duration = 50;
        //promenjiva koju koristimo kada je animacija u toku
        bool animation_in_progress = false;

        public MainFrame()
        {
            InitializeComponent();
            canvas.InitializeContexts();
            try
            {
                world = new World(@"C:\Users\Vladimir\Desktop\Racunarska grafika\Projekat\Projekat\Projekat\star wars x-wing", "star wars x-wing.3ds", canvas.Width, canvas.Height);
            }
            catch
            {
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

            if (!animation_in_progress)
            {
                switch (e.KeyCode)
                {

                    case Keys.B:
                        {
                            airplaneTimer.Enabled = true;
                            animation_duration = 60;
                            break;
                        }
                    case Keys.F2: this.Close(); break;
                    case Keys.W:
                        {


                            if (world.RotationX > 0)
                            {
                                if (world.RotationX <= 19)
                                {
                                    world.RotationX -= 4.0f;
                                }
                            }
                            else
                            {

                                if (world.RotationX > -157)
                                {
                                    world.RotationX -= 4;
                                }
                            }


                            //world.RotationX -= 4.0f;



                            break;
                        }
                    case Keys.S:
                        {


                            if (world.RotationX > 0)
                            {
                                if (world.RotationX < 19)
                                {
                                    world.RotationX += 4.0f;
                                }
                            }
                            else
                            {

                                if (world.RotationX >= -157)
                                {
                                    world.RotationX += 4;
                                }
                            }


                            //world.RotationX -= 4.0f;



                            break;
                        }
                    case Keys.A: world.RotationY -= 4.0f; break;
                    case Keys.D: world.RotationY += 4.0f; break;
                    case Keys.Subtract:
                        {
                            if (Math.Floor(Math.Abs(world.cameraControl)) == 10)
                            {
                                //world.cameraControl = 10;
                            }
                            else
                            {
                                world.cameraControl -= .3f;
                            }
                            break;
                        }
                    case Keys.OemMinus:
                        {

                            if (Math.Floor(Math.Abs(world.cameraControl)) == 10)
                            {
                                //world.cameraControl = 10;
                            }
                            else
                            {
                                world.cameraControl -= .3f;
                            }
                            break;
                        }
                    case Keys.Oemplus:
                        {

                            if (Math.Floor(Math.Abs(world.cameraControl)) == 0)
                            {
                                //world.cameraControl = 0;
                            }
                            else
                            {
                                world.cameraControl += .3f;
                            }
                            break;
                        }
                    case Keys.Add:
                        {
                            if (Math.Floor(Math.Abs(world.cameraControl)) == 0)
                            {
                                //world.cameraControl = 0;
                            }
                            else
                            {
                                world.cameraControl += .3f;
                            }
                            break;
                        }


                }




                this.Refresh();
            }
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lightsInterval_ValueChanged(object sender, EventArgs e)
        {
            if (lightsInterval.Value == 0)
            {
                lightTimer.Enabled = false;
                world.lights_enabled = false;
            }
            else {
                lightTimer.Enabled = true;
                lightTimer.Interval = (int)Math.Round(500 / lightsInterval.Value);
                world.lights_enabled = true;
            }
        }

        private void lightTimer_Tick(object sender, EventArgs e)
        {
            ////test da li radi tajmer
            if (par)
            {
                world.lights_enabled = true;
                //par = false;
            }
            else {
                //par = true;
                world.lights_enabled = false;
            }

            par = !par;
            //Random r = new Random();
            //world.light_red = (byte)r.Next(0, 255);
            //world.light_blue = (byte)r.Next(0, 255);
            //world.light_green = (byte)r.Next(0, 255);
            this.Refresh();


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            world.light_diameter = (float)numericUpDown1.Value;
            this.Refresh();
        }

        private void airplaneTimer_Tick(object sender, EventArgs e)
        {
            if (animation_duration != 0)
            {
                world.airplane_pos[1] += 1f;
                world.airplane_pos[2] -= 5f;
                world.airplane_light_pos[1] += 1f;
                world.airplane_light_pos[2] -= 5f;
                //disejblovanje korisnicke interakcije
                animation_in_progress = true;
                lightGroupBox.Enabled = false;

                animation_duration--;

            }
            else {
                animation_in_progress = false;
                lightGroupBox.Enabled = true;
                airplaneTimer.Enabled = false;
                world.airplane_pos[1] = -8.5f;
                world.airplane_pos[2] = -10f;
                world.airplane_light_pos[1] = 0f;
                world.airplane_light_pos[2] = -10f;
            }
            this.Refresh();
            
            
        }
    }
}
