namespace _01_VisionPro_联合开发
{
    partial class frmCamera
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.cogAcqFifoEditV21 = new Cognex.VisionPro.CogAcqFifoEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.cogAcqFifoEditV21)).BeginInit();
            this.SuspendLayout();
            // 
            // cogAcqFifoEditV21
            // 
            this.cogAcqFifoEditV21.Location = new System.Drawing.Point(26, 65);
            this.cogAcqFifoEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogAcqFifoEditV21.Name = "cogAcqFifoEditV21";
            this.cogAcqFifoEditV21.Size = new System.Drawing.Size(1512, 1114);
            this.cogAcqFifoEditV21.SuspendElectricRuns = false;
            this.cogAcqFifoEditV21.TabIndex = 0;
            // 
            // frmCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1669, 1298);
            this.Controls.Add(this.cogAcqFifoEditV21);
            this.Name = "frmCamera";
            this.Text = "frmCamera";
            ((System.ComponentModel.ISupportInitialize)(this.cogAcqFifoEditV21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Cognex.VisionPro.CogAcqFifoEditV2 cogAcqFifoEditV21;
    }
}