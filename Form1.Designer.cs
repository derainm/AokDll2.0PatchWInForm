namespace DllPatchAok20
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPatch = new System.Windows.Forms.Button();
            this.BtnBrw = new System.Windows.Forms.Button();
            this.checkBoxWindowedMod = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonWideScreen1280 = new System.Windows.Forms.RadioButton();
            this.radioButtonNoWideScreen = new System.Windows.Forms.RadioButton();
            this.radioButtonWideScreenVoobly = new System.Windows.Forms.RadioButton();
            this.radioButtonWideScreenCentred = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxPortFowarding = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPatch
            // 
            this.buttonPatch.Location = new System.Drawing.Point(7, 230);
            this.buttonPatch.Name = "buttonPatch";
            this.buttonPatch.Size = new System.Drawing.Size(132, 66);
            this.buttonPatch.TabIndex = 0;
            this.buttonPatch.Text = "Patch ";
            this.buttonPatch.UseVisualStyleBackColor = true;
            this.buttonPatch.Click += new System.EventHandler(this.buttonBrowser_Click);
            // 
            // BtnBrw
            // 
            this.BtnBrw.Location = new System.Drawing.Point(332, 230);
            this.BtnBrw.Name = "BtnBrw";
            this.BtnBrw.Size = new System.Drawing.Size(143, 66);
            this.BtnBrw.TabIndex = 1;
            this.BtnBrw.Text = "Browser";
            this.BtnBrw.UseVisualStyleBackColor = true;
            this.BtnBrw.Click += new System.EventHandler(this.BtnPatch_Click);
            // 
            // checkBoxWindowedMod
            // 
            this.checkBoxWindowedMod.AutoSize = true;
            this.checkBoxWindowedMod.Location = new System.Drawing.Point(12, 207);
            this.checkBoxWindowedMod.Name = "checkBoxWindowedMod";
            this.checkBoxWindowedMod.Size = new System.Drawing.Size(101, 17);
            this.checkBoxWindowedMod.TabIndex = 2;
            this.checkBoxWindowedMod.Text = "Windowed Mod";
            this.checkBoxWindowedMod.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonWideScreen1280);
            this.panel1.Controls.Add(this.radioButtonNoWideScreen);
            this.panel1.Controls.Add(this.radioButtonWideScreenVoobly);
            this.panel1.Controls.Add(this.radioButtonWideScreenCentred);
            this.panel1.Location = new System.Drawing.Point(12, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 85);
            this.panel1.TabIndex = 7;
            // 
            // radioButtonWideScreen1280
            // 
            this.radioButtonWideScreen1280.AutoSize = true;
            this.radioButtonWideScreen1280.Location = new System.Drawing.Point(3, 1);
            this.radioButtonWideScreen1280.Name = "radioButtonWideScreen1280";
            this.radioButtonWideScreen1280.Size = new System.Drawing.Size(159, 17);
            this.radioButtonWideScreen1280.TabIndex = 3;
            this.radioButtonWideScreen1280.TabStop = true;
            this.radioButtonWideScreen1280.Text = "WideScreen static (Voobly)  ";
            this.radioButtonWideScreen1280.UseVisualStyleBackColor = true;
            // 
            // radioButtonNoWideScreen
            // 
            this.radioButtonNoWideScreen.AutoSize = true;
            this.radioButtonNoWideScreen.Location = new System.Drawing.Point(3, 65);
            this.radioButtonNoWideScreen.Name = "radioButtonNoWideScreen";
            this.radioButtonNoWideScreen.Size = new System.Drawing.Size(98, 17);
            this.radioButtonNoWideScreen.TabIndex = 2;
            this.radioButtonNoWideScreen.TabStop = true;
            this.radioButtonNoWideScreen.Text = "No wideScreen";
            this.radioButtonNoWideScreen.UseVisualStyleBackColor = true;
            // 
            // radioButtonWideScreenVoobly
            // 
            this.radioButtonWideScreenVoobly.AutoSize = true;
            this.radioButtonWideScreenVoobly.Location = new System.Drawing.Point(3, 22);
            this.radioButtonWideScreenVoobly.Name = "radioButtonWideScreenVoobly";
            this.radioButtonWideScreenVoobly.Size = new System.Drawing.Size(177, 17);
            this.radioButtonWideScreenVoobly.TabIndex = 1;
            this.radioButtonWideScreenVoobly.TabStop = true;
            this.radioButtonWideScreenVoobly.Text = "WideScreen Userpatch (Voobly)";
            this.radioButtonWideScreenVoobly.UseVisualStyleBackColor = true;
            // 
            // radioButtonWideScreenCentred
            // 
            this.radioButtonWideScreenCentred.AutoSize = true;
            this.radioButtonWideScreenCentred.Location = new System.Drawing.Point(3, 42);
            this.radioButtonWideScreenCentred.Name = "radioButtonWideScreenCentred";
            this.radioButtonWideScreenCentred.Size = new System.Drawing.Size(124, 17);
            this.radioButtonWideScreenCentred.TabIndex = 0;
            this.radioButtonWideScreenCentred.TabStop = true;
            this.radioButtonWideScreenCentred.Text = "WideScreen Centred";
            this.radioButtonWideScreenCentred.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Choise WideScreen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(233, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Be shure that you don\'t have any widescreen mod installed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(233, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "if you don\'t want a bug";
            // 
            // checkBoxPortFowarding
            // 
            this.checkBoxPortFowarding.AutoSize = true;
            this.checkBoxPortFowarding.Location = new System.Drawing.Point(12, 190);
            this.checkBoxPortFowarding.Name = "checkBoxPortFowarding";
            this.checkBoxPortFowarding.Size = new System.Drawing.Size(97, 17);
            this.checkBoxPortFowarding.TabIndex = 10;
            this.checkBoxPortFowarding.Text = "Port Fowarding";
            this.checkBoxPortFowarding.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 304);
            this.Controls.Add(this.checkBoxPortFowarding);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxWindowedMod);
            this.Controls.Add(this.BtnBrw);
            this.Controls.Add(this.buttonPatch);
            this.Name = "Form1";
            this.Text = "Dll Aok 20 Patch";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPatch;
        private System.Windows.Forms.Button BtnBrw;
        private System.Windows.Forms.CheckBox checkBoxWindowedMod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonNoWideScreen;
        private System.Windows.Forms.RadioButton radioButtonWideScreenVoobly;
        private System.Windows.Forms.RadioButton radioButtonWideScreenCentred;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonWideScreen1280;
        private System.Windows.Forms.CheckBox checkBoxPortFowarding;
    }
}

