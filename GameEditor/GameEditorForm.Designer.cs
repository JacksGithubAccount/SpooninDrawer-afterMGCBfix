namespace MonoGame.Forms.DX
{
    partial class GameEditorForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.sampleControl = new MonoGame.Forms.DX.Controls.SampleControl();
            this.gameControl1 = new GameEditor.GameControl();
            this.SuspendLayout();
            // 
            // sampleControl
            // 
            this.sampleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sampleControl.Location = new System.Drawing.Point(0, 0);
            this.sampleControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sampleControl.MouseHoverUpdatesOnly = false;
            this.sampleControl.Name = "sampleControl";
            this.sampleControl.Size = new System.Drawing.Size(757, 550);
            this.sampleControl.TabIndex = 0;
            this.sampleControl.Text = "Sample Control";
            this.sampleControl.Click += new System.EventHandler(this.sampleControl_Click);
            // 
            // gameControl1
            // 
            this.gameControl1.Location = new System.Drawing.Point(192, 152);
            this.gameControl1.MouseHoverUpdatesOnly = false;
            this.gameControl1.Name = "gameControl1";
            this.gameControl1.Size = new System.Drawing.Size(248, 195);
            this.gameControl1.TabIndex = 1;
            this.gameControl1.Text = "gameControl1";
            // 
            // GameEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 550);
            this.Controls.Add(this.gameControl1);
            this.Controls.Add(this.sampleControl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GameEditorForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SampleControl sampleControl;
        private GameEditor.GameControl gameControl1;
    }
}

