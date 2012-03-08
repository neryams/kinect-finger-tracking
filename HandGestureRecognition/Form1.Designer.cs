namespace HandGestureRecognition
{
    partial class MainWindow
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
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.dataOutput = new System.Windows.Forms.TextBox();
            this.bRecalibrate = new System.Windows.Forms.Button();
            this.mouseKeyEventProvider1 = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
            this.threshold = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sensitivity = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensitivity)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.BackColor = System.Drawing.SystemColors.Control;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(10, 10);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(480, 360);
            this.imageBoxFrameGrabber.TabIndex = 3;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // dataOutput
            // 
            this.dataOutput.Location = new System.Drawing.Point(518, 41);
            this.dataOutput.Name = "dataOutput";
            this.dataOutput.Size = new System.Drawing.Size(100, 20);
            this.dataOutput.TabIndex = 4;
            this.dataOutput.Text = "not watching";
            // 
            // bRecalibrate
            // 
            this.bRecalibrate.Location = new System.Drawing.Point(518, 10);
            this.bRecalibrate.Name = "bRecalibrate";
            this.bRecalibrate.Size = new System.Drawing.Size(100, 23);
            this.bRecalibrate.TabIndex = 5;
            this.bRecalibrate.Text = "Recalibrate";
            this.bRecalibrate.UseVisualStyleBackColor = true;
            this.bRecalibrate.Click += new System.EventHandler(this.bRecalibrate_Click);
            // 
            // mouseKeyEventProvider1
            // 
            this.mouseKeyEventProvider1.Enabled = true;
            this.mouseKeyEventProvider1.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
            // 
            // threshold
            // 
            this.threshold.LargeChange = 10;
            this.threshold.Location = new System.Drawing.Point(516, 106);
            this.threshold.Maximum = 150;
            this.threshold.Minimum = 80;
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(100, 45);
            this.threshold.SmallChange = 5;
            this.threshold.TabIndex = 7;
            this.threshold.TickFrequency = 10;
            this.threshold.Value = 100;
            this.threshold.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(516, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Finger Threshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(535, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sensitivity";
            // 
            // sensitivity
            // 
            this.sensitivity.Location = new System.Drawing.Point(516, 183);
            this.sensitivity.Maximum = 20;
            this.sensitivity.Minimum = 5;
            this.sensitivity.Name = "sensitivity";
            this.sensitivity.Size = new System.Drawing.Size(100, 45);
            this.sensitivity.TabIndex = 9;
            this.sensitivity.Value = 10;
            this.sensitivity.Scroll += new System.EventHandler(this.sensitivity_change);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 381);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sensitivity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.threshold);
            this.Controls.Add(this.bRecalibrate);
            this.Controls.Add(this.dataOutput);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Name = "MainWindow";
            this.Text = "Nontic";
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensitivity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private System.Windows.Forms.TextBox dataOutput;
        private System.Windows.Forms.Button bRecalibrate;
        private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider1;
        private System.Windows.Forms.TrackBar threshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar sensitivity;

    }
}

