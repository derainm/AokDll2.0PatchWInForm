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
            this.checkBoxWideScreen = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonPatch
            // 
            this.buttonPatch.Location = new System.Drawing.Point(12, 257);
            this.buttonPatch.Name = "buttonPatch";
            this.buttonPatch.Size = new System.Drawing.Size(132, 66);
            this.buttonPatch.TabIndex = 0;
            this.buttonPatch.Text = "Patch ";
            this.buttonPatch.UseVisualStyleBackColor = true;
            this.buttonPatch.Click += new System.EventHandler(this.buttonBrowser_Click);
            // 
            // BtnBrw
            // 
            this.BtnBrw.Location = new System.Drawing.Point(355, 257);
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
            this.checkBoxWindowedMod.Location = new System.Drawing.Point(12, 210);
            this.checkBoxWindowedMod.Name = "checkBoxWindowedMod";
            this.checkBoxWindowedMod.Size = new System.Drawing.Size(101, 17);
            this.checkBoxWindowedMod.TabIndex = 2;
            this.checkBoxWindowedMod.Text = "Windowed Mod";
            this.checkBoxWindowedMod.UseVisualStyleBackColor = true;
            // 
            // checkBoxWideScreen
            // 
            this.checkBoxWideScreen.AutoSize = true;
            this.checkBoxWideScreen.Location = new System.Drawing.Point(12, 177);
            this.checkBoxWideScreen.Name = "checkBoxWideScreen";
            this.checkBoxWideScreen.Size = new System.Drawing.Size(85, 17);
            this.checkBoxWideScreen.TabIndex = 3;
            this.checkBoxWideScreen.Text = "WideScreen";
            this.checkBoxWideScreen.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "If you have 1280 x       resolution better to use no wideScreen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(121, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Be shure that you don\'t have any widescreen mod installed";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 335);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxWideScreen);
            this.Controls.Add(this.checkBoxWindowedMod);
            this.Controls.Add(this.BtnBrw);
            this.Controls.Add(this.buttonPatch);
            this.Name = "Form1";
            this.Text = "Dll Aok 20 Patch";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPatch;
        private System.Windows.Forms.Button BtnBrw;
        private System.Windows.Forms.CheckBox checkBoxWindowedMod;
        private System.Windows.Forms.CheckBox checkBoxWideScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

