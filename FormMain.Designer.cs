namespace GMExplorer {
   partial class FormMain {
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

      #region Vom Windows Form-Designer generierter Code

      /// <summary>
      /// Erforderliche Methode für die Designerunterstützung.
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.tabPage1 = new System.Windows.Forms.TabPage();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.treeView1 = new System.Windows.Forms.TreeView();
         this.splitContainer2 = new System.Windows.Forms.SplitContainer();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.hexWin1 = new GMExplorer.HexWin();
         this.tabPage2 = new System.Windows.Forms.TabPage();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.ToolStripMenuItem_open = new System.Windows.Forms.ToolStripMenuItem();
         this.ToolStripMenuItem_openfolder = new System.Windows.Forms.ToolStripMenuItem();
         this.ToolStripMenuItem_recentlyUsed = new System.Windows.Forms.ToolStripMenuItem();
         this.ToolStripMenuItem_closeTab = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.ToolStripMenuItem_closeprog = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.toolStripButton_open = new System.Windows.Forms.ToolStripButton();
         this.toolStripButton_open_folder = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripButton_close_tab = new System.Windows.Forms.ToolStripButton();
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
         this.toolStripContainer1.ContentPanel.SuspendLayout();
         this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer1.SuspendLayout();
         this.tabControl1.SuspendLayout();
         this.tabPage1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
         this.splitContainer2.Panel1.SuspendLayout();
         this.splitContainer2.Panel2.SuspendLayout();
         this.splitContainer2.SuspendLayout();
         this.menuStrip1.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripContainer1
         // 
         // 
         // toolStripContainer1.ContentPanel
         // 
         this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
         this.toolStripContainer1.ContentPanel.Controls.Add(this.statusStrip1);
         this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(987, 801);
         this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
         this.toolStripContainer1.Name = "toolStripContainer1";
         this.toolStripContainer1.Size = new System.Drawing.Size(987, 850);
         this.toolStripContainer1.TabIndex = 0;
         this.toolStripContainer1.Text = "toolStripContainer1";
         // 
         // toolStripContainer1.TopToolStripPanel
         // 
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
         this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
         // 
         // tabControl1
         // 
         this.tabControl1.Controls.Add(this.tabPage1);
         this.tabControl1.Controls.Add(this.tabPage2);
         this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
         this.tabControl1.Location = new System.Drawing.Point(0, 0);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(987, 801);
         this.tabControl1.TabIndex = 1;
         this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
         this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
         this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
         // 
         // tabPage1
         // 
         this.tabPage1.Controls.Add(this.splitContainer1);
         this.tabPage1.Location = new System.Drawing.Point(4, 22);
         this.tabPage1.Name = "tabPage1";
         this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage1.Size = new System.Drawing.Size(979, 775);
         this.tabPage1.TabIndex = 0;
         this.tabPage1.Text = "tabPage1";
         this.tabPage1.UseVisualStyleBackColor = true;
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(3, 3);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.treeView1);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
         this.splitContainer1.Size = new System.Drawing.Size(973, 769);
         this.splitContainer1.SplitterDistance = 330;
         this.splitContainer1.TabIndex = 0;
         // 
         // treeView1
         // 
         this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.treeView1.Location = new System.Drawing.Point(0, 0);
         this.treeView1.Name = "treeView1";
         this.treeView1.Size = new System.Drawing.Size(330, 769);
         this.treeView1.TabIndex = 0;
         // 
         // splitContainer2
         // 
         this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer2.Location = new System.Drawing.Point(0, 0);
         this.splitContainer2.Name = "splitContainer2";
         this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer2.Panel1
         // 
         this.splitContainer2.Panel1.Controls.Add(this.textBox1);
         // 
         // splitContainer2.Panel2
         // 
         this.splitContainer2.Panel2.Controls.Add(this.hexWin1);
         this.splitContainer2.Size = new System.Drawing.Size(639, 769);
         this.splitContainer2.SplitterDistance = 384;
         this.splitContainer2.TabIndex = 1;
         // 
         // textBox1
         // 
         this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBox1.Location = new System.Drawing.Point(0, 0);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.ReadOnly = true;
         this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.textBox1.Size = new System.Drawing.Size(639, 384);
         this.textBox1.TabIndex = 0;
         this.textBox1.WordWrap = false;
         // 
         // hexWin1
         // 
         this.hexWin1.BytesPerLine = 16;
         this.hexWin1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.hexWin1.Location = new System.Drawing.Point(0, 0);
         this.hexWin1.Name = "hexWin1";
         this.hexWin1.ShowAsText = true;
         this.hexWin1.Size = new System.Drawing.Size(639, 381);
         this.hexWin1.TabIndex = 0;
         // 
         // tabPage2
         // 
         this.tabPage2.Location = new System.Drawing.Point(4, 22);
         this.tabPage2.Name = "tabPage2";
         this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage2.Size = new System.Drawing.Size(979, 775);
         this.tabPage2.TabIndex = 1;
         this.tabPage2.Text = "tabPage2";
         this.tabPage2.UseVisualStyleBackColor = true;
         // 
         // statusStrip1
         // 
         this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.statusStrip1.Location = new System.Drawing.Point(0, 803);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(202, 22);
         this.statusStrip1.TabIndex = 0;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // menuStrip1
         // 
         this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(987, 24);
         this.menuStrip1.TabIndex = 1;
         this.menuStrip1.Text = "menuStrip1";
         this.menuStrip1.MenuActivate += new System.EventHandler(this.menuStrip1_MenuActivate);
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_open,
            this.ToolStripMenuItem_openfolder,
            this.ToolStripMenuItem_recentlyUsed,
            this.ToolStripMenuItem_closeTab,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_closeprog});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // ToolStripMenuItem_open
         // 
         this.ToolStripMenuItem_open.Name = "ToolStripMenuItem_open";
         this.ToolStripMenuItem_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.ToolStripMenuItem_open.Size = new System.Drawing.Size(177, 22);
         this.ToolStripMenuItem_open.Text = "&open file";
         this.ToolStripMenuItem_open.Click += new System.EventHandler(this.ToolStripMenuItem_open_Click);
         // 
         // ToolStripMenuItem_openfolder
         // 
         this.ToolStripMenuItem_openfolder.Name = "ToolStripMenuItem_openfolder";
         this.ToolStripMenuItem_openfolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
         this.ToolStripMenuItem_openfolder.Size = new System.Drawing.Size(177, 22);
         this.ToolStripMenuItem_openfolder.Text = "open &folder";
         this.ToolStripMenuItem_openfolder.Click += new System.EventHandler(this.ToolStripMenuItem_open_folder_Click);
         // 
         // ToolStripMenuItem_recentlyUsed
         // 
         this.ToolStripMenuItem_recentlyUsed.Name = "ToolStripMenuItem_recentlyUsed";
         this.ToolStripMenuItem_recentlyUsed.Size = new System.Drawing.Size(177, 22);
         this.ToolStripMenuItem_recentlyUsed.Text = "&recently used";
         // 
         // ToolStripMenuItem_closeTab
         // 
         this.ToolStripMenuItem_closeTab.Name = "ToolStripMenuItem_closeTab";
         this.ToolStripMenuItem_closeTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
         this.ToolStripMenuItem_closeTab.Size = new System.Drawing.Size(177, 22);
         this.ToolStripMenuItem_closeTab.Text = "close &tab";
         this.ToolStripMenuItem_closeTab.Click += new System.EventHandler(this.ToolStripMenuItem_closeTab_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
         // 
         // ToolStripMenuItem_closeprog
         // 
         this.ToolStripMenuItem_closeprog.Name = "ToolStripMenuItem_closeprog";
         this.ToolStripMenuItem_closeprog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.ToolStripMenuItem_closeprog.Size = new System.Drawing.Size(177, 22);
         this.ToolStripMenuItem_closeprog.Text = "close";
         this.ToolStripMenuItem_closeprog.Click += new System.EventHandler(this.ToolStripMenuItem_close_Click);
         // 
         // toolStrip1
         // 
         this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_open,
            this.toolStripButton_open_folder,
            this.toolStripSeparator2,
            this.toolStripButton_close_tab});
         this.toolStrip1.Location = new System.Drawing.Point(3, 24);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(87, 25);
         this.toolStrip1.TabIndex = 0;
         // 
         // toolStripButton_open
         // 
         this.toolStripButton_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton_open.Image = global::GMExplorer.Properties.Resources.open;
         this.toolStripButton_open.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton_open.Name = "toolStripButton_open";
         this.toolStripButton_open.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton_open.Text = "open file";
         this.toolStripButton_open.Click += new System.EventHandler(this.toolStripButton_open_Click);
         // 
         // toolStripButton_open_folder
         // 
         this.toolStripButton_open_folder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton_open_folder.Image = global::GMExplorer.Properties.Resources.folder;
         this.toolStripButton_open_folder.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton_open_folder.Name = "toolStripButton_open_folder";
         this.toolStripButton_open_folder.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton_open_folder.Text = "open folder";
         this.toolStripButton_open_folder.Click += new System.EventHandler(this.toolStripButton_open_folder_Click);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripButton_close_tab
         // 
         this.toolStripButton_close_tab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton_close_tab.Image = global::GMExplorer.Properties.Resources.cross;
         this.toolStripButton_close_tab.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton_close_tab.Name = "toolStripButton_close_tab";
         this.toolStripButton_close_tab.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton_close_tab.Text = "close tab";
         this.toolStripButton_close_tab.Click += new System.EventHandler(this.toolStripButton_close_tab_Click);
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.AddExtension = false;
         this.openFileDialog1.Filter = "Garmin-Dateien|*.img;*.typ;*.dem|Alle Dateien|*.*";
         this.openFileDialog1.Title = "open file";
         // 
         // folderBrowserDialog1
         // 
         this.folderBrowserDialog1.Description = "open folder";
         this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyDocuments;
         this.folderBrowserDialog1.ShowNewFolderButton = false;
         // 
         // FormMain
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(987, 850);
         this.Controls.Add(this.toolStripContainer1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "FormMain";
         this.Text = "Form1";
         this.Load += new System.EventHandler(this.FormMain_Load);
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
         this.toolStripContainer1.ContentPanel.ResumeLayout(false);
         this.toolStripContainer1.ContentPanel.PerformLayout();
         this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer1.TopToolStripPanel.PerformLayout();
         this.toolStripContainer1.ResumeLayout(false);
         this.toolStripContainer1.PerformLayout();
         this.tabControl1.ResumeLayout(false);
         this.tabPage1.ResumeLayout(false);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.splitContainer2.Panel1.ResumeLayout(false);
         this.splitContainer2.Panel1.PerformLayout();
         this.splitContainer2.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
         this.splitContainer2.ResumeLayout(false);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ToolStripContainer toolStripContainer1;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripButton toolStripButton_open;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.TabPage tabPage2;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.TreeView treeView1;
      private System.Windows.Forms.ToolTip toolTip1;
      private HexWin hexWin1;
      private System.Windows.Forms.SplitContainer splitContainer2;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.ToolStripButton toolStripButton_open_folder;
      private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_open;
      private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_openfolder;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_closeprog;
      private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_closeTab;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripButton toolStripButton_close_tab;
      private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_recentlyUsed;
   }
}

