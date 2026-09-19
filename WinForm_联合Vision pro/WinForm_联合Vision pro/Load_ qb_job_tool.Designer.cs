namespace WinForm_联合Vision_pro
{
    partial class Load__qb_job_tool
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cogJobManagerEdit1 = new Cognex.VisionPro.QuickBuild.CogJobManagerEdit();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cogJobEdit1 = new Cognex.VisionPro.QuickBuild.CogJobEdit();
            this.cogPMAlignMultiEditV21 = new Cognex.VisionPro.PMAlign.CogPMAlignMultiEditV2();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cogImageConvertEdit1 = new Cognex.VisionPro.ImageProcessing.CogImageConvertEdit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogJobEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogPMAlignMultiEditV21)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(422, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(178, 88);
            this.button3.TabIndex = 1;
            this.button3.Text = "加载Tool";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(222, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(178, 88);
            this.button2.TabIndex = 2;
            this.button2.Text = "加载job";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 88);
            this.button1.TabIndex = 3;
            this.button1.Text = "加载qb";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(22, 131);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1182, 864);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cogJobManagerEdit1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1166, 817);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "QuickBuild";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cogJobManagerEdit1
            // 
            this.cogJobManagerEdit1.Location = new System.Drawing.Point(17, 42);
            this.cogJobManagerEdit1.Name = "cogJobManagerEdit1";
            this.cogJobManagerEdit1.ShowLocalizationTab = false;
            this.cogJobManagerEdit1.Size = new System.Drawing.Size(1091, 683);
            this.cogJobManagerEdit1.Subject = null;
            this.cogJobManagerEdit1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cogJobEdit1);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1166, 817);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "加载job";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cogPMAlignMultiEditV21);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1166, 817);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "加载tool";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cogJobEdit1
            // 
            this.cogJobEdit1.AllowDrop = true;
            this.cogJobEdit1.ContextMenuCustomizer = null;
            this.cogJobEdit1.Location = new System.Drawing.Point(30, 29);
            this.cogJobEdit1.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogJobEdit1.Name = "cogJobEdit1";
            this.cogJobEdit1.ShowNodeToolTips = true;
            this.cogJobEdit1.Size = new System.Drawing.Size(1095, 769);
            this.cogJobEdit1.SuspendElectricRuns = false;
            this.cogJobEdit1.TabIndex = 0;
            this.cogJobEdit1.ToolSyncObject = null;
            // 
            // cogPMAlignMultiEditV21
            // 
            this.cogPMAlignMultiEditV21.Location = new System.Drawing.Point(22, 48);
            this.cogPMAlignMultiEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogPMAlignMultiEditV21.Name = "cogPMAlignMultiEditV21";
            this.cogPMAlignMultiEditV21.Size = new System.Drawing.Size(1128, 751);
            this.cogPMAlignMultiEditV21.SuspendElectricRuns = false;
            this.cogPMAlignMultiEditV21.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cogImageConvertEdit1);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1166, 817);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "加载cogImageConvertToo";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cogImageConvertEdit1
            // 
            this.cogImageConvertEdit1.Location = new System.Drawing.Point(37, 43);
            this.cogImageConvertEdit1.Name = "cogImageConvertEdit1";
            this.cogImageConvertEdit1.Size = new System.Drawing.Size(1078, 736);
            this.cogImageConvertEdit1.TabIndex = 0;
            this.cogImageConvertEdit1.ToolSyncObject = null;
            // 
            // Load__qb_job_tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 1086);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Load__qb_job_tool";
            this.Text = "Load__qb_job_tool";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogJobEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogPMAlignMultiEditV21)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private Cognex.VisionPro.QuickBuild.CogJobManagerEdit cogJobManagerEdit1;
        private Cognex.VisionPro.QuickBuild.CogJobEdit cogJobEdit1;
        private Cognex.VisionPro.PMAlign.CogPMAlignMultiEditV2 cogPMAlignMultiEditV21;
        private System.Windows.Forms.TabPage tabPage4;
        private Cognex.VisionPro.ImageProcessing.CogImageConvertEdit cogImageConvertEdit1;
    }
}