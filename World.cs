using RacunarskaGrafika.Vezbe;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Projekat
{
    public class World : IDisposable
    {

        //sijalica crveno
        public byte light_red { get; set; }
        //sijalica zeleno
        public byte light_green { get; set; }
        //sijalica plavo
        public byte light_blue { get; set; }

        public float current_daljina = 0;

        public float light_diameter = 0.05f;

        /// <summary>
        ///	 Identifikatori OpenGL tekstura
        /// </summary>
        static int[] textures = null;

        /// <summary>
        ///	 Putanje do slika koje se koriste za teksture
        /// </summary>
        static string[] textureFiles = { "..//..//textures//asphalt.jpg", "..//..//textures//dirt.jpg" };


        /// <summary>
        ///	 Identifikatori tekstura za jednostavniji pristup teksturama
        /// </summary>
        private enum TextureObjects { Asphalt = 0, Dirt };
        private readonly int textureCount = Enum.GetNames(typeof(TextureObjects)).Length;

        // Box klasa
        private Box mf_box = null;

        //sijalice na pisti, 30 komada
        public List<Glu.GLUquadric> mf_sijalice;

        //objekat koji predstavlja izvor svetlosti
        //koristen za debagovanje
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
        internal float cameraControl;
        public bool lights_enabled { get; set; }

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

            try
            {
                textures = new int[textureCount];
            }
            catch (Exception)
            {
                MessageBox.Show("Neuspesno kreirana niz identifikatora za teksture", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void Resize()
        {
            Gl.glViewport(0, 0, mf_width, mf_height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Bitmap image;  // promenljiva u koju ucitavamo sararzaj slike

            //fov 60, near 1.5, far 30
            Glu.gluPerspective(60.0, (double)mf_width / (double)mf_height, 1.5, 30.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            Gl.glLoadIdentity();
            turnOnLights();




        }

        private void turnOnLights()
        {
            float[] light_pos = { 0f, 5f, -14f, 1f };
            float[] light_color = { 0, 0, 0, 0 };


            Gl.glPushMatrix();
            {
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_pos);
                Gl.glTranslatef(light_pos[0], light_pos[1], light_pos[2]);

                //Glu.gluSphere(light_source, 0.5f, 64, 64);
                //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, light_color);

            }
            Gl.glPopMatrix();



        }

        private void Initialize()
        {
            lights_enabled = false;
            //yellow lights
            light_red = 255;
            light_green = 255;
            light_blue = 77;

            //camera + -
            cameraControl = -5;

            float[] ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            //float[] ambiental = { 0.5f, 0.5f, 0.5f, 1.0f };
            Bitmap image;  // promenljiva u koju ucitavamo sadrzaj slike

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_CULL_FACE);
            Gl.glLoadIdentity();

            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, ambient);

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, ambient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, diffuse);
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, new float[] { 0, 1, 0 });
            //cut-off 30
            Gl.glLightf(Gl.GL_LIGHT0, Gl.GL_SPOT_CUTOFF, 30f);




            //ukljucivanje svetla i normalizacije
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            
            //Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_NORMALIZE);


            //color tracking
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glColorMaterial(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE);


            //teksture
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);

            // Ucitaj slike i kreiraj teksture
            Gl.glGenTextures(textureCount, textures);


            for (int i = 0; i < textureCount; ++i)
            {
                // Pridruzi teksturu odgovarajucem identifikatoru
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[i]);

                // Ucitaj sliku i podesi parametre teksture
                image = new Bitmap(textureFiles[i]);
                // rotiramo sliku zbog koordinantog sistema opengl-a
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                // RGBA format (dozvoljena providnost slike tj. alfa kanal)
                BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                      PixelFormat.Format32bppArgb);

                Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, Gl.GL_RGBA8, image.Width, image.Height, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, imageData.Scan0);

                // TODO 1: Podesiti texture filtre: max = linear, min=linear_mipmap_linear,
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);

                image.UnlockBits(imageData);
                image.Dispose();
            }






            //inicijalizacija 30 sijalica
            mf_sijalice = new List<Glu.GLUquadric>();
            int broj_sijalica = 30;
            for (int i = 0; i < broj_sijalica; i++)
            {
                Glu.GLUquadric tmp = Glu.gluNewQuadric();
                Glu.gluQuadricTexture(tmp, Gl.GL_TRUE);
                Glu.gluQuadricNormals(tmp, Glu.GLU_SMOOTH);
                mf_sijalice.Add(tmp);

            }
            light_source = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(light_source, Glu.GLU_SMOOTH);

            Draw();

        }

        public void Draw()
        {
            int envMode;
            Gl.glClear(16640);
            turnOnLights();
            Gl.glPushMatrix();


            //kamera
            Glu.gluLookAt(2.0f, 3.0f, cameraControl, //pogled sa kamere
                0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f);



            // Primeni transformacije nad svetom
            Gl.glTranslatef(0.0f, 0.0f, 1f);
            Gl.glRotatef(mf_xRotation, 1.0f, 0.0f, 0.0f);
            Gl.glRotatef(mf_yRotation, 0.0f, 1.0f, 0.0f);





            // Podloga nacrtana sa gl quads
            Gl.glPushMatrix();
            {


                Gl.glColor3ub(61, 61, 41);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[(int)TextureObjects.Dirt]);
                Gl.glMatrixMode(Gl.GL_TEXTURE);
                Gl.glPushMatrix();
                {
                    Gl.glScalef(10f, 10f, 1f);
                    //Gl.glTranslatef(-0.5f, -2f, 0f);

                    Gl.glMatrixMode(Gl.GL_MODELVIEW);
                    Gl.glScalef(4f, 0.5f, 15f);
                    Gl.glTranslatef(-0.5f, -2f, 0f);
                    Gl.glBegin(Gl.GL_QUADS);
                    {
                        //definisana normala
                        Gl.glNormal3f(0, 1, 0);
                        Gl.glTexCoord2f(1.0f, 1.0f);
                        Gl.glVertex3f(0f, 0f, 0.0f);
                        Gl.glTexCoord2f(1.0f, 0.0f);
                        Gl.glVertex3f(1.0f, 0f, 0.0f);
                        Gl.glTexCoord2f(0.0f, 0.0f);
                        Gl.glVertex3f(1.0f, 0f, -1.0f);
                        Gl.glTexCoord2f(0.0f, 1.0f);
                        Gl.glVertex3f(0f, 0f, -1.0f);
                    }
                    Gl.glEnd();
                    Gl.glMatrixMode(Gl.GL_TEXTURE);
                }
                Gl.glPopMatrix();
                Gl.glMatrixMode(Gl.GL_MODELVIEW);

            }
            Gl.glPopMatrix();

            // pista nacrana Box klasom
            Gl.glPushMatrix();
            {
                //normale definisane u box klasi
                //stapanje na gl_add
                Gl.glGetTexEnviv(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, out envMode);
                Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_ADD);

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[(int)TextureObjects.Asphalt]);
                Gl.glColor3ub(193, 193, 164);
                Gl.glScalef(2.5f, 0.1f, 15f);
                Gl.glTranslatef(0f, -9.1f, -0.5f);
                mf_box.Draw();
                // Vratiti rezim stapanja na prethodno stanje!
                Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, envMode);

            }
            Gl.glPopMatrix();

            //crtanje svetla
            drawLights();

            //crtanje svetla iznad aviona
            Gl.glPushMatrix();
            {


                float[] ambient = { 0, 0, 0, 1 };
                float[] light_color = { 0, 255, 0, 1 };
                float[] light_pos = { -1.4f, 0f, -10f, 1 };
                Gl.glScalef(0.1f, 0.1f, 0.1f);
                //zeleni tackasti izvor
                Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_AMBIENT, ambient);
                Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_DIFFUSE, light_color);
                Gl.glLightf(Gl.GL_LIGHT3, Gl.GL_SPOT_CUTOFF, 180f);

                //malo slabljenja svetlosti da ne bode toliko oci
                Gl.glLightf(Gl.GL_LIGHT3, Gl.GL_CONSTANT_ATTENUATION, 100.0f);
                Gl.glLightf(Gl.GL_LIGHT3, Gl.GL_LINEAR_ATTENUATION, 50.0f);
                Gl.glLightf(Gl.GL_LIGHT3, Gl.GL_QUADRATIC_ATTENUATION, 1.5f);
                //Gl.glLightf(Gl.GL_LIGHT3, Gl.GL_SPOT_EXPONENT, 120f);

                Gl.glEnable(Gl.GL_LIGHT3);
                Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_POSITION, light_pos);

                Gl.glTranslatef(light_pos[0],light_pos[1],light_pos[2]);

                //Glu.gluSphere(light_source, 5f, 64, 64);
                //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, light_color);


            }
            Gl.glPopMatrix();

            //crtanje aviona
            Gl.glPushMatrix();
            {
                
                Gl.glScalef(0.1f, 0.1f, 0.1f);
                Gl.glTranslatef(-1.4f, -8.5f, -10f);
                Gl.glRotatef(-147f, 0, 1, 0);
                mf_scene.Draw();
            }
            Gl.glPopMatrix();

            String[] message = new string[]{
                "Predmet:       Racunarska grafika",
                "Sk.god:        2014/15",
                "Ime:           Vladimir",
                "Prezime:       Besermenji",
                "Sifra zad:     6.2",
                "",
                "X rotation "+ mf_xRotation,
                "",
                "Y rotation "+ mf_yRotation,
                "",
                "ZOOM: "+ cameraControl

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
            //Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glFlush();


        }


      


        private void drawLights()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            float[] ambient = { 0, 0, 0, 1 };
            float[] light_color = { light_red, light_green, light_blue, 1 };

            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, ambient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, light_color);
            //Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 45f);


            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_AMBIENT, ambient);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_DIFFUSE, light_color);
            //Gl.glLightf(Gl.GL_LIGHT2, Gl.GL_SPOT_CUTOFF, 10f);


            //if (lights_enabled)
            //{
            //    Gl.glEnable(Gl.GL_LIGHT1);
            //    Gl.glEnable(Gl.GL_LIGHT2);
            //    //Gl.glEnable(Gl.GL_LIGHTING);
            //}
            //else
            //{
            //    Gl.glDisable(Gl.GL_LIGHT1);
            //    Gl.glDisable(Gl.GL_LIGHT2);
            //    //Gl.glDisable(Gl.GL_LIGHTING);
            //}

            //crtanje sijalica na pisti
            //Gl.glColor3ub(light_red, light_green, light_blue);
            float x_sijalice = 1.35f;
            float y_sijalice = -.95f;

            for (int i = 0; i < mf_sijalice.Count; i++)
            {
                Gl.glPushMatrix();
                //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, moonPos);
                //Gl.glTranslatef(moonPos[0], moonPos[1], moonPos[2]);
                //Gl.glScalef(0.5f, 0.5f, 0.5f);
                //Gl.glColor3ub(252, 252, 219); // bledo zuta boja
                //Gl.glGetMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, emission);
                //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, moonColor);

                double daljina = Math.Floor((double)i / 2);

                if (i % 2 == 0)
                {

                    {
                        //float[] pos = { x_sijalice, y_sijalice, -(float)daljina, 1 };
                        //Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, pos);
                        //Gl.glTranslatef(pos[0],pos[1],pos[2]);

                        float[] posBall = { x_sijalice, y_sijalice, -(float)daljina, 1 };
                        float[] posLight = { x_sijalice, y_sijalice + 0.1f, -(float)daljina, 1 };
                        if (i == current_daljina)
                        {
                            //Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, posLight);
                        }
                        Gl.glTranslatef(posBall[0], posBall[1], posBall[2]);
                        //Gl.glColor3b((byte)light_color[0], (byte)light_color[1], (byte)light_color[2]);
                        //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, light_color);

                    }

                }
                else
                {
                    float[] posBall = { -x_sijalice, y_sijalice, -(float)daljina, 1 };
                    float[] posLight = { -x_sijalice, y_sijalice + 0.1f, -(float)daljina, 1 };
                    if (i == current_daljina)
                    {
                        //Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_POSITION, posLight);
                    }
                    Gl.glTranslatef(posBall[0], posBall[1], posBall[2]);
                    //Gl.glColor3b();
                    //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, light_color);
                    //Gl.glTranslatef(-x_sijalice, y_sijalice, -(float)daljina);
                }
                if (current_daljina % 2 == 0)
                {
                    if (current_daljina == i || current_daljina + 1 == i)
                    {
                        Gl.glColor3ub(light_red, light_green, light_blue);
                    }
                    else
                    {
                        Gl.glColor3b(0, 0, 0);
                    }
                }
                else {
                    Gl.glColor3b(0, 0, 0);
                }
                
                //Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, light_color);
                Glu.gluSphere(mf_sijalice[i], light_diameter, 64, 64);

                Gl.glPopMatrix();
            }
            if (lights_enabled)
            {
                current_daljina++;
                if (current_daljina == 30)
                {
                    current_daljina = 0;
                }
            }
            else {
                //current_daljina = 0;
                }
            Gl.glEnable(Gl.GL_TEXTURE_2D);


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
        [HandleProcessCorruptedStateExceptionsAttribute]
        private void Terminate()
        {
            // Oslobodi alocirane identifikatore DL liste i VBO objekta
            for (int i = 0; i < mf_sijalice.Count; i++)
            {
                Glu.gluDeleteQuadric(mf_sijalice[i]);
            }
            Glu.gluDeleteQuadric(light_source);
            //Gl.glDeleteLists(m_treeDL, 1);
            try
            {
                Gl.glDeleteTextures(textureCount, textures);
            }
            catch { }
        }

        #endregion IDisposable metode

    }
}