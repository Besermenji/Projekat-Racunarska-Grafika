using RacunarskaGrafika.Vezbe;
using RacunarskaGrafika.Vezbe.AssimpNetSample;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Projekat
{
    public class World
    {

        // Box klasa
        private Box mf_box = null;

        //sijalice na pisti, 30 komada
        public List<Glu.GLUquadric> mf_sijalice;
        public Glu.GLUquadric light_source;
        //asimp scena
        private AssimpScene mf_scene;
        public AssimpScene Scene
        {
            get { return mf_scene; }
            set { mf_scene = value; }
        }

        //teks
        private BitmapFont mf_font = null;



        private int mf_width;
        private int mf_height;
        
        private float mf_yRotation = 0.0f;

       
        private float mf_xRotation = 15.0f;

        
        public float RotationY
        {
            get { return mf_yRotation; }
            set { mf_yRotation = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        public float RotationX
        {
            get { return mf_xRotation; }
            set { mf_xRotation = value; }
        }



        public int Width
        {
            get { return mf_width; }
            set { mf_width = value; }
        }

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        public int Height
        {
            get { return mf_height; }
            set { mf_height = value; }
        }

        public World(string path, string file_name, int width, int height)
        {

            


            //setovanje visine i sirine za resize
            this.mf_width = width;
            this.mf_height = height;

            


            try
            {
                this.mf_scene = new AssimpScene(path, file_name);
                this.mf_font = new BitmapFont("Tahoma", 14f, false, true, true, false);
                this.mf_box = new Box();
            }
            catch (Exception)
            {
                MessageBox.Show("Neuspesno kreirana instanca klase BOX", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Initialize();
            this.Resize();
        }
        /// <summary>
        ///  Dispose metoda.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///  Destruktor.
        /// </summary>
        ~World()
        {
            this.Dispose(false);
        }

        public void Resize() {
            Gl.glViewport(0, 0, mf_width, mf_height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //fov 60, near 1.5, far 30
            Glu.gluPerspective(60.0, (double)mf_width / (double)mf_height, 1.5, 30.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            
            Gl.glLoadIdentity();
            turnOnLights();
            

          
        }

        private void turnOnLights() {
            float[] light_pos = { 0f, 3f, -12f, 1f };
            float[] light_color = { 0, 0, 0, 1 };


            Gl.glPushMatrix();
            {
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_pos);
                Gl.glTranslatef(light_pos[0], light_pos[1], light_pos[2]);
                //Glu.gluSphere(light_source, 0.5f, 64, 64);
                //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, light_color);

            }
            Gl.glPopMatrix();
            //pozicija svetla
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_pos);

            //boja svetla
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, new float[] { 0, 1, 0, 1 });

            
        }

        private void Initialize()
        {

            float[] ambient = { 0.0f, 0.0f, 0.0f, 1.0f };
            float[] diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] ambiental = { 0.2f, 0.2f, 0.2f, 1.0f };

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_CULL_FACE);


            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, ambient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, diffuse);
            //cut-off 30
            Gl.glLightf(Gl.GL_LIGHT0, Gl.GL_SPOT_CUTOFF, 30f);

            //ukljucivanje svetla i normalizacije
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_NORMALIZE);


            //color tracking
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glColorMaterial(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE);

            Gl.glLoadIdentity();


            //inicijalizacija 30 sijalica
            mf_sijalice = new List<Glu.GLUquadric>();
            int broj_sijalica = 30;
            for (int i = 0; i < broj_sijalica; i++)
            {
                Glu.GLUquadric tmp = Glu.gluNewQuadric();
                Glu.gluQuadricNormals(tmp, Glu.GLU_SMOOTH);
                mf_sijalice.Add(tmp);
                
            }
            light_source = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(light_source, Glu.GLU_SMOOTH);

            

        }

        public void Draw()
        {
            Gl.glClear(16640);
            turnOnLights();
            Gl.glPushMatrix();
            
            //float[] white_color = { 1f, 1f, 1f, 1f };
            //float[] light_pos = { 1.5f, 1.5f, 1.0f, 15.0f };

            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_pos);


            // Primeni transformacije nad svetom
            Gl.glTranslatef(0.0f, 0.0f, -3.5f);
            Gl.glRotatef(mf_xRotation, 1.0f, 0.0f, 0.0f);
            Gl.glRotatef(mf_yRotation, 0.0f, 1.0f, 0.0f);

            
            


            // Podloga nacrtana sa gl quads
            Gl.glPushMatrix();
            {
                Gl.glScalef(4f, 0.5f, 15f);
                Gl.glTranslatef(-0.5f, -2f, 0f);

                Gl.glColor3ub(61, 61, 41);

                Gl.glBegin(Gl.GL_QUADS);
                {
                    //definisana normala
                    Gl.glNormal3f(0, 1, 0);
                    Gl.glVertex3f(0f, 0f, 0.0f);
                    Gl.glVertex3f(1.0f, 0f, 0.0f);
                    Gl.glVertex3f(1.0f, 0f, -1.0f);
                    Gl.glVertex3f(0f, 0f, -1.0f);
                }
                Gl.glEnd();
            }
            Gl.glPopMatrix();

            // Podloga nacrana Box klasom
            Gl.glPushMatrix();
            {
                Gl.glColor3ub(193, 193, 164);
                //definisana normala
                Gl.glNormal3f(0, 1, 0);
                Gl.glScalef(2.5f, 0.1f, 15f);
                Gl.glTranslatef(0f, -9.1f, -0.5f);
                mf_box.Draw();
            }
            Gl.glPopMatrix();


            //crtanje sijalica na pisti
            Gl.glColor3ub(255, 255, 77);
            float x_sijalice = 1.35f;
            float y_sijalice = -.95f;

            for (int i = 0; i < mf_sijalice.Count; i++)
            {
                Gl.glPushMatrix();

                double daljina = Math.Floor((double)i / 2);

                if (i % 2 == 0)
                {

                    {
                        Gl.glTranslatef(x_sijalice, y_sijalice, -(float)daljina);


                    }
                    
                }
                else {
                    Gl.glTranslatef(-x_sijalice, y_sijalice, -(float)daljina);
                }

                Glu.gluSphere(mf_sijalice[i], 0.05f, 64, 64);
                
                    

                Gl.glPopMatrix();
            }


            //crtanje aviona
            Gl.glPushMatrix();
            {
                Gl.glScalef(0.1f, 0.1f, 0.1f);
                Gl.glTranslatef(-1.4f, -8.5f, -10f);
                Gl.glRotatef(-147f, 0, 1, 0);
                mf_scene.Draw();
            }
            Gl.glPopMatrix();

            String[] message = new string[5]{
                "Predmet:       Racunarska grafika",
                "Sk.god:        2014/15",
                "Ime:           Vladimir",
                "Prezime:       Besermenji",
                "Sifra zad:     6.2"
              };
            //crtanje teksta
            
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            {
                Gl.glDisable(Gl.GL_DEPTH_TEST);
                Gl.glDisable(Gl.GL_CULL_FACE);
                Gl.glViewport(0, 0, mf_width, mf_height);
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Glu.gluOrtho2D(-mf_width / 2.0, mf_width / 2.0, -mf_height / 2.0, mf_height / 2.0);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glColor3ub(0, 0, 255);
                
                for (int i = 0; i < message.Length; i++)
                {
                    
                    Gl.glRasterPos2f(-mf_width / 2.0f, mf_height / 2.0f - mf_font.Height * (i + 1) - 1.5f);
                    mf_font.DrawText(message[i]);

                }
                Gl.glEnable(Gl.GL_CULL_FACE);
                Gl.glEnable(Gl.GL_DEPTH_TEST);

                //Gl.glTranslatef(0, 0, 10f);

                Gl.glPopMatrix();
            }
            Gl.glViewport(0, 0, mf_width, mf_height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //fov 60, near 1.5, far 30
            Glu.gluPerspective(60.0, (double)mf_width / (double)mf_height, 1.5, 30.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();


            Gl.glFlush();
            

        }


        #region IDisposable metode

        /// <summary>
        ///  Implementacija IDisposable interfejsa.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //  // Oslodi managed resurse
            //}

            // Oslobodi OpenGL resurse
            Terminate();
        }

        /// <summary>
        ///  Korisnicko oslobadjanje OpenGL resursa.
        /// </summary>
        private void Terminate()
        {
            // Oslobodi alocirane identifikatore DL liste i VBO objekta
            for (int i = 0; i < mf_sijalice.Count; i++)
            {
                Glu.gluDeleteQuadric(mf_sijalice[i]);
            }
            Glu.gluDeleteQuadric(light_source);
            //Gl.glDeleteLists(m_treeDL, 1);
        }

        #endregion IDisposable metode

    }
}