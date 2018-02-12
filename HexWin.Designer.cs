namespace GMExplorer {
   partial class HexWin {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Verwendete Ressourcen bereinigen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Vom Komponenten-Designer generierter Code

      /// <summary> 
      /// Erforderliche Methode für die Designerunterstützung. 
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent() {
         this.textBoxHex = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // textBoxHex
         // 
         this.textBoxHex.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textBoxHex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxHex.Location = new System.Drawing.Point(0, 0);
         this.textBoxHex.MaxLength = 327670;
         this.textBoxHex.Multiline = true;
         this.textBoxHex.Name = "textBoxHex";
         this.textBoxHex.ReadOnly = true;
         this.textBoxHex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.textBoxHex.Size = new System.Drawing.Size(423, 291);
         this.textBoxHex.TabIndex = 0;
         this.textBoxHex.WordWrap = false;
         // 
         // HexWin
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.textBoxHex);
         this.Name = "HexWin";
         this.Size = new System.Drawing.Size(423, 291);
         this.Load += new System.EventHandler(this.HexWin_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox textBoxHex;
   }
}
