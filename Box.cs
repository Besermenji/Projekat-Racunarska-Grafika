﻿// -----------------------------------------------------------------------
// <file>Box.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2012.</copyright>
// <author>Srdjan Mihic</author>
// <summary>Klasa koja enkapsulira OpenGL programski kod za iscrtavanje kvadra sa tezistem u koord.pocetku.</summary>
// -----------------------------------------------------------------------
namespace RacunarskaGrafika.Vezbe
{
    using Tao.OpenGl;

    /// <summary>
    ///  Klasa enkapsulira OpenGL kod za iscrtavanje kvadra.
    /// </summary>
    public class Box
    {
        #region Atributi

        /// <summary>
        ///	 Visina kvadra.
        /// </summary>
        double m_height = 1.0;

        /// <summary>
        ///	 Sirina kvadra.
        /// </summary>
        double m_width = 1.0;

        /// <summary>
        ///	 Dubina kvadra.
        /// </summary>
        double m_depth = 1.0;

        #endregion Atributi

        #region Properties

        /// <summary>
        ///	 Visina kvadra.
        /// </summary>
        public double Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        /// <summary>
        ///	 Sirina kvadra.
        /// </summary>
        public double Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        ///	 Dubina kvadra.
        /// </summary>
        public double Depth
        {
            get { return m_depth; }
            set { m_depth = value; }
        }

        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///		Konstruktor.
        /// </summary>
        public Box()
        {
        }

        /// <summary>
        ///		Konstruktor sa parametrima.
        /// </summary>
        /// <param name="width">Sirina kvadra.</param>
        /// <param name="height">Visina kvadra.</param>
        /// <param name="depth"></param>
        public Box(double width, double height, double depth)
        {
            this.m_width = width;
            this.m_height = height;
            this.m_depth = depth;
        }

        #endregion Konstruktori

        #region Metode

        public void Draw()
        {
            Gl.glBegin(Gl.GL_QUADS);

            float num1 = this.m_width > this.m_depth ? (float)this.m_width : (float)this.m_depth;
            float num2 = this.m_width > this.m_height ? (float)this.m_width : (float)this.m_height;
            float num3 = this.m_depth > this.m_height ? (float)this.m_depth : (float)this.m_height;

            // Zadnja
            Gl.glNormal3f(0.0f, 0.0f, -1f);
            Gl.glTexCoord2f((float)this.m_width / num2, 0.0f);
            Gl.glVertex3d(this.m_width / 2.0, -this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-this.m_width / 2.0, -this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, (float)this.m_height / num2);
            Gl.glVertex3d(-this.m_width / 2.0, this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_width / num2, (float)this.m_height / num2);
            Gl.glVertex3d(this.m_width / 2.0, this.m_height / 2.0, -this.m_depth / 2.0);


            // Desna
            Gl.glNormal3f(1f, 0.0f, 0.0f);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(this.m_width / 2.0, -this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_height / num3, 0.0f);
            Gl.glVertex3d(this.m_width / 2.0, this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_height / num3, (float)this.m_depth / num3);
            Gl.glVertex3d(this.m_width / 2.0, this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, (float)this.m_depth / num3);
            Gl.glVertex3d(this.m_width / 2.0, -this.m_height / 2.0, this.m_depth / 2.0);


            // Prednja
            Gl.glNormal3f(0.0f, 0.0f, 1f);
            Gl.glTexCoord2f((float)this.m_width / num2, 0.0f);
            Gl.glVertex3d(this.m_width / 2.0, -this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_width / num2, (float)this.m_height / num3);
            Gl.glVertex3d(this.m_width / 2.0, this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, (float)this.m_height / num3);
            Gl.glVertex3d(-this.m_width / 2.0, this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-this.m_width / 2.0, -this.m_height / 2.0, this.m_depth / 2.0);


            // Leva
            Gl.glNormal3f(-1f, 0.0f, 0.0f);
            Gl.glTexCoord2f(0.0f, (float)this.m_depth / num3);
            Gl.glVertex3d(-this.m_width / 2.0, -this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_height / num3, (float)this.m_depth / num3);
            Gl.glVertex3d(-this.m_width / 2.0, this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_height / num3, 0.0f);
            Gl.glVertex3d(-this.m_width / 2.0, this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-this.m_width / 2.0, -this.m_height / 2.0, -this.m_depth / 2.0);

            // Donja
            Gl.glNormal3f(0.0f, -1f, 0.0f);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-this.m_width / 2.0, -this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_width / num1, 0.0f);
            Gl.glVertex3d(this.m_width / 2.0, -this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_width / num1, (float)this.m_depth / num1);
            Gl.glVertex3d(this.m_width / 2.0, -this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, (float)this.m_depth / num1);
            Gl.glVertex3d(-this.m_width / 2.0, -this.m_height / 2.0, this.m_depth / 2.0);

            // Gornja
            Gl.glNormal3f(0.0f, 1f, 0.0f);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-this.m_width / 2.0, this.m_height / 2.0, -this.m_depth / 2.0);
            Gl.glTexCoord2f(0.0f, (float)this.m_depth / num1);
            Gl.glVertex3d(-this.m_width / 2.0, this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_width / num1, (float)this.m_depth / num1);
            Gl.glVertex3d(this.m_width / 2.0, this.m_height / 2.0, this.m_depth / 2.0);
            Gl.glTexCoord2f((float)this.m_width / num1, 0.0f);
            Gl.glVertex3d(this.m_width / 2.0, this.m_height / 2.0, -this.m_depth / 2.0);

            Gl.glEnd();
        }

        public void SetSize(double width, double height, double depth)
        {
            m_depth = depth;
            m_height = height;
            m_width = width;
        }

        #endregion Metode
    }
}
