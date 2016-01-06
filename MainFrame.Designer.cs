namespace Projekat
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.canvas = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.colorButton = new System.Windows.Forms.Button();
            this.lightGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lightsInterval = new System.Windows.Forms.NumericUpDown();
            this.lightTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lightGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AccumBits = ((byte)(0));
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.AutoCheckErrors = false;
            this.canvas.AutoFinish = false;
            this.canvas.AutoMakeCurrent = true;
            this.canvas.AutoSwapBuffers = true;
            this.canvas.BackColor = System.Drawing.SystemColors.WindowText;
            this.canvas.ColorBits = ((byte)(32));
            this.canvas.DepthBits = ((byte)(16));
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(366, 364);
            this.canvas.StencilBits = ((byte)(0));
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.canvas_KeyDown);
            this.canvas.Resize += new System.EventHandler(this.canvas_Resize);
            // 
            // colorButton
            // 
            this.colorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButton.Location = new System.Drawing.Point(110, 182);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(75, 23);
            this.colorButton.TabIndex = 1;
            this.colorButton.Text = "Boja sijalica";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // lightGroupBox
            // 
            this.lightGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lightGroupBox.Controls.Add(this.numericUpDown1);
            this.lightGroupBox.Controls.Add(this.label2);
            this.lightGroupBox.Controls.Add(this.label1);
            this.lightGroupBox.Controls.Add(this.lightsInterval);
            this.lightGroupBox.Controls.Add(this.colorButton);
            this.lightGroupBox.Location = new System.Drawing.Point(391, 138);
            this.lightGroupBox.Name = "lightGroupBox";
            this.lightGroupBox.Size = new System.Drawing.Size(200, 211);
            this.lightGroupBox.TabIndex = 2;
            this.lightGroupBox.TabStop = false;
            this.lightGroupBox.Text = "Rad sa sijalicama";
            this.lightGroupBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Interval paljenja/gašenja sijalica (n/sec)";
            // 
            // lightsInterval
            // 
            this.lightsInterval.Location = new System.Drawing.Point(6, 50);
            this.lightsInterval.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lightsInterval.Name = "lightsInterval";
            this.lightsInterval.ReadOnly = true;
            this.lightsInterval.Size = new System.Drawing.Size(120, 20);
            this.lightsInterval.TabIndex = 2;
            this.lightsInterval.ValueChanged += new System.EventHandler(this.lightsInterval_ValueChanged);
            // 
            // lightTimer
            // 
            this.lightTimer.Tick += new System.EventHandler(this.lightTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Veličina sijalica";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(9, 101);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 361);
            this.Controls.Add(this.lightGroupBox);
            this.Controls.Add(this.canvas);
            this.Name = "MainFrame";
            this.Text = "Projekat";
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.lightGroupBox.ResumeLayout(false);
            this.lightGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl canvas;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.GroupBox lightGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown lightsInterval;
        private System.Windows.Forms.Timer lightTimer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
    }
}

