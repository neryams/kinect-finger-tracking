namespace HandGestureRecognition
{
    partial class Form1
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
            this.testBox = new System.Windows.Forms.TextBox();
            this.mouseKeyEventProvider1 = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
            this.threshold = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(10, 12);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(640, 480);
            this.imageBoxFrameGrabber.TabIndex = 3;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // dataOutput
            // 
            this.dataOutput.Location = new System.Drawing.Point(671, 12);
            this.dataOutput.Name = "dataOutput";
            this.dataOutput.Size = new System.Drawing.Size(100, 20);
            this.dataOutput.TabIndex = 4;
            this.dataOutput.Text = "not watching";
            // 
            // bRecalibrate
            // 
            this.bRecalibrate.Location = new System.Drawing.Point(671, 65);
            this.bRecalibrate.Name = "bRecalibrate";
            this.bRecalibrate.Size = new System.Drawing.Size(100, 23);
            this.bRecalibrate.TabIndex = 5;
            this.bRecalibrate.Text = "Recalibrate";
            this.bRecalibrate.UseVisualStyleBackColor = true;
            this.bRecalibrate.Click += new System.EventHandler(this.bRecalibrate_Click);
            // 
            // testBox
            // 
            this.testBox.Location = new System.Drawing.Point(671, 38);
            this.testBox.Name = "testBox";
            this.testBox.Size = new System.Drawing.Size(100, 20);
            this.testBox.TabIndex = 6;
            // 
            // mouseKeyEventProvider1
            // 
            this.mouseKeyEventProvider1.Enabled = true;
            this.mouseKeyEventProvider1.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
            // 
            // threshold
            // 
            this.threshold.LargeChange = 20;
            this.threshold.Location = new System.Drawing.Point(671, 162);
            this.threshold.Maximum = 180;
            this.threshold.Minimum = 70;
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(100, 45);
            this.threshold.SmallChange = 10;
            this.threshold.TabIndex = 7;
            this.threshold.TickFrequency = 10;
            this.threshold.Value = 100;
            this.threshold.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 538);
            this.Controls.Add(this.threshold);
            this.Controls.Add(this.testBox);
            this.Controls.Add(this.bRecalibrate);
            this.Controls.Add(this.dataOutput);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private System.Windows.Forms.TextBox dataOutput;
        private System.Windows.Forms.Button bRecalibrate;
        private System.Windows.Forms.TextBox testBox;
        private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider1;
        private System.Windows.Forms.TrackBar threshold;

    }
}

