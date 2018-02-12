using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   public partial class FormMain : Form {

      /*
       * ..\..\Test1\70060025.IMG
       * ..\..\Test1\70060062.IMG
       * ..\..\Test2.gmap
       */


      string[] Args = null;

      TabPage dummytabpage = null;


      /// <summary>
      /// zusätzliche Daten für eine TabPage
      /// </summary>
      class TabPageData {
         /// <summary>
         /// zugehörige TabPage
         /// </summary>
         public TabPage Page { get; protected set; }
         /// <summary>
         /// zugehöriger TreeView
         /// </summary>
         public TreeView Tv { get; protected set; }
         /// <summary>
         /// zugehörige TextBox für Infos
         /// </summary>
         public TextBox Info { get; protected set; }
         /// <summary>
         /// zugehöriges Control für Hex-Ausgabe
         /// </summary>
         public HexWin Hexwin { get; protected set; }
         /// <summary>
         /// Verzeichnis oder Datei
         /// </summary>
         public string Path { get; protected set; }


         public TabPageData(TabPage page, TreeView tv, TextBox info, HexWin hexwin, string path) {
            Page = page;
            Tv = tv;
            Info = info;
            Hexwin = hexwin;
            Path = path;
         }

      }

      /// <summary>
      /// Liste der zuletzt verwendeten Karten
      /// </summary>
      List<string> RecentUsedPaths = new List<string>();



      public FormMain() {
         InitializeComponent();
      }

      public FormMain(string[] args) : this() {
         Args = args;
      }

      private void FormMain_Load(object sender, EventArgs e) {
         SetCaptionText();

         openFileDialog1.Filter = "Garmin-Dateien|*.img;*.typ;*.tdb;*.mdx;*.dem|Alle Dateien|*.*";

         ImageList myImages = new ImageList();
         myImages.ImageSize = new Size(16, 16);
         myImages.TransparentColor = Color.Transparent;
         myImages.Images.Add(new Bitmap(16, 16));     // Dummy-Image, damit der Platz im TabKopf einkalkuliert wird
         tabControl1.ImageList = myImages;
         tabControl1.TabPages[0].ImageIndex = 0;

         dummytabpage = tabControl1.TabPages[0];
         tabControl1.TabPages.Clear();

         for (int i = 0; i < Args.Length; i++)
            NewTabPageWithFile(Args[i].Trim());

         if (Args.Length > 0) {
            TabPageData tpd = TabPageData4Page(tabControl1.TabPages[0]);
            tpd.Tv.Select();
         }
      }

      void SetCaptionText(string txt = null) {
         Assembly a = Assembly.GetExecutingAssembly();
         string text =
            ((AssemblyProductAttribute)(Attribute.GetCustomAttribute(a, typeof(AssemblyProductAttribute)))).Product + ", Version vom " +
            ((AssemblyInformationalVersionAttribute)(Attribute.GetCustomAttribute(a, typeof(AssemblyInformationalVersionAttribute)))).InformationalVersion + ", " +
            ((AssemblyCopyrightAttribute)(Attribute.GetCustomAttribute(a, typeof(AssemblyCopyrightAttribute)))).Copyright;
         if (!string.IsNullOrEmpty(txt))
            text += " - " + txt;
         Text = text;
      }

      private void toolStripButton_open_Click(object sender, EventArgs e) {
         ToolStripMenuItem_open_Click(null, null);
      }

      private void toolStripButton_open_folder_Click(object sender, EventArgs e) {
         ToolStripMenuItem_open_folder_Click(null, null);
      }

      private void toolStripButton_close_tab_Click(object sender, EventArgs e) {
         ToolStripMenuItem_closeTab_Click(null, null);
      }

      private void ToolStripMenuItem_open_Click(object sender, EventArgs e) {
         if (openFileDialog1.ShowDialog() == DialogResult.OK)
            NewTabPageWithFile(openFileDialog1.FileName);
      }

      private void ToolStripMenuItem_open_folder_Click(object sender, EventArgs e) {
         if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            NewTabPageWithFile(folderBrowserDialog1.SelectedPath);
      }

      private void ToolStripMenuItem_closeTab_Click(object sender, EventArgs e) {
         CloseTabPage();
      }

      private void ToolStripMenuItem_close_Click(object sender, EventArgs e) {
         Close();
      }

      private void FormMain_DragEnter(object sender, DragEventArgs e) {
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
         else
            e.Effect = DragDropEffects.None;
      }

      private void FormMain_DragDrop(object sender, DragEventArgs e) {
         foreach (string file in e.Data.GetData(DataFormats.FileDrop, false) as string[]) {
            NewTabPageWithFile(file);
         }
      }

      private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
         Data.AppendChildNodes(e.Node);
      }

      private void TreeView_AfterSelect(object sender, TreeViewEventArgs e) {
         TreeView tv = sender as TreeView;

         // zugehörige TabPage ermitteln
         Control testctrl = tv;
         while (testctrl.Parent != null &&
                !(testctrl is TabPage)) {
            testctrl = testctrl.Parent;
         }
         if (!(testctrl is TabPage))
            throw new Exception("internal error: no TabPage for TreeView");

         ShowData4Node(tv.SelectedNode, TabPageData4Page(testctrl as TabPage));
      }

      private void tabControl1_MouseDown(object sender, MouseEventArgs e) {
         Rectangle tabaera = tabControl1.GetTabRect(tabControl1.SelectedIndex);
         Rectangle closeButton = new Rectangle(tabaera.Right - 15, tabaera.Top + 4, tabaera.Height / 2, tabaera.Height / 2);
         if (closeButton.Contains(e.Location))
            CloseTabPage();
      }

      private void tabControl1_DrawItem(object sender, DrawItemEventArgs e) {
         TabControl tc = sender as TabControl;
         Rectangle tabrect = tc.GetTabRect(e.Index);
         Graphics g = e.Graphics;

         //e.DrawBackground();

         // "Close-Button" zeichnen
         Rectangle closerectangle = new Rectangle(tabrect.Right - 15, tabrect.Top + 4, tabrect.Height / 2, tabrect.Height / 2);
         g.FillRectangle(new SolidBrush(Color.Red), closerectangle);
         Pen pen1 = new Pen(Color.White, 1);
         g.DrawLine(pen1, closerectangle.Left + 1, closerectangle.Top + 1, closerectangle.Right - 1, closerectangle.Bottom - 1);
         g.DrawLine(pen1, closerectangle.Left + 1, closerectangle.Bottom - 1, closerectangle.Right - 1, closerectangle.Top + 1);

         // Beschriftung
         StringFormat sf = new StringFormat();
         sf.Alignment = StringAlignment.Near;
         sf.LineAlignment = StringAlignment.Center;
         g.DrawString(tc.TabPages[e.Index].Text,
                      e.Font,
                      new SolidBrush(tc.TabPages[e.Index].ForeColor),
                      tabrect,
                      sf);
      }

      void ToolStripMenuItem_recentused(object sender, EventArgs e) {
         NewTabPageWithFile((sender as ToolStripMenuItem).Tag as string);
      }

      private void menuStrip1_MenuActivate(object sender, EventArgs e) {
         ToolStripMenuItem mi = (sender as MenuStrip).Items[0] as ToolStripMenuItem;
         for (int i = 0; i < mi.DropDownItems.Count; i++) {
            ToolStripMenuItem mir = mi.DropDownItems[i] as ToolStripMenuItem;
            if (mir.Name == "ToolStripMenuItem_recentlyUsed") {
               ToolStripDropDown dd = mir.DropDown;
               dd.Items.Clear();
               for (int j = 0; j < RecentUsedPaths.Count; j++) {
                  dd.Items.Add("&" + (j + 1).ToString() + ". " + RecentUsedPaths[j], null, new EventHandler(ToolStripMenuItem_recentused));
                  dd.Items[dd.Items.Count - 1].Tag = RecentUsedPaths[j];
               }
               break;
            }
         }
      }

      private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
         TabPage tp = (sender as TabControl).SelectedTab;
         string txt = null;
         if (tp != null) {
            TabPageData tpd = TabPageData4Page(tp);
            if (tpd != null)
               txt = tpd.Path;
         }
         SetCaptionText(txt);
      }

      void NewTabPageWithFile(string newfile) {
         string[] files = new string[] { "" };

         if (File.Exists(newfile)) {
            files[0] = newfile;
         } else if (Directory.Exists(newfile)) {
            files = Directory.GetFiles(newfile, "*.*", SearchOption.AllDirectories);
            Array.Sort(files);
         } else {
            MessageBox.Show("'" + newfile + "' not exist!", "error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
         }

         for (int i = RecentUsedPaths.Count - 1; i >= 0; i--)
            if (RecentUsedPaths[i] == newfile)
               RecentUsedPaths.RemoveAt(i);

         TabPageData tpd = AppendNewTabPage(Path.GetFileName(newfile), newfile);
         foreach (var file in files) {
            NodeContent.Content4PhysicalFile content4file = new NodeContent.Content4PhysicalFile(file);
            TreeNode tn = Data.AppendNode(tpd.Tv, NodeContent.NodeType.PhysicalFile, content4file, Path.GetFileName(content4file.Filename));
            Data.AppendNode(tn); // damit der Knoten ausklappbar ist
            //tpd.Tv.SelectedNode = tn;
         }
         SetCaptionText(tpd.Path);

         tabControl1.SelectedTab = tpd.Page; // TabPage akt.
         tpd.Tv.Select();     // Treeview akt.
         if (tpd.Tv.Nodes.Count > 0)
            tpd.Tv.SelectedNode = tpd.Tv.Nodes[0]; // 1. Node akt.
      }

      /// <summary>
      /// erzeugt einen Clone der <see cref="dummytabpage"/> und hängt ihn an das TabControl an
      /// </summary>
      /// <param name="text"></param>
      /// <param name="path"></param>
      /// <returns>neue TabPage</returns>
      TabPageData AppendNewTabPage(string text, string path) {
         TabPage newtabpage = new TabPage(text);
         TreeView newtv = null;
         TextBox newinfo = null;
         HexWin newhexwin = null;

         foreach (var dummytabpagectrl in dummytabpage.Controls) {
            if (dummytabpagectrl is SplitContainer) {

               SplitContainer sc = dummytabpagectrl as SplitContainer;
               SplitContainer newsc = sc.Clone();
               newtabpage.Controls.Add(newsc);

               foreach (var item in sc.Panel1.Controls) {
                  if (item is TreeView) {
                     newtv = (item as TreeView).Clone();
                     newsc.Panel1.Controls.Add(newtv);
                     newsc.SplitterDistance = newsc.Width / 3;

                     newtv.AfterSelect += TreeView_AfterSelect;
                     newtv.BeforeExpand += TreeView_BeforeExpand;

                     newtv.Tag = new TreeViewData(newtv);
                  }
               }

               foreach (var item in sc.Panel2.Controls) {
                  if (item is SplitContainer) {
                     SplitContainer sc2 = item as SplitContainer;
                     SplitContainer newsc2 = sc2.Clone();
                     newsc.Panel2.Controls.Add(newsc2);
                     newsc2.SplitterDistance = newsc2.Height / 2;

                     foreach (var item2 in sc2.Panel1.Controls) {
                        if (item2 is TextBox) {
                           newinfo = (item2 as TextBox).Clone();
                           newsc2.Panel1.Controls.Add(newinfo);
                        }
                     }
                     foreach (var item3 in sc2.Panel2.Controls) {
                        if (item3 is HexWin) {
                           newhexwin = (item3 as HexWin).Clone();
                           newsc2.Panel2.Controls.Add(newhexwin);
                        }
                     }
                  }
               }
            }
         }

         newtabpage.Tag = new TabPageData(newtabpage, newtv, newinfo, newhexwin, path);
         newtabpage.ImageIndex = 0;
         tabControl1.TabPages.Add(newtabpage);

         return TabPageData4Page(newtabpage);
      }

      void CloseTabPage() {
         if (tabControl1.TabPages.Count > 0)
            if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
               TabPage page = tabControl1.SelectedTab;
               tabControl1.TabPages.Remove(page);

               // ev. geöffnete Dateien schließen
               TabPageData tpd = TabPageData4Page(page);
               if (tpd != null) {
                  foreach (TreeNode tn in tpd.Tv.Nodes) {  // alle TreeNodes der 1. Ebene untersuchen
                     NodeContent nc = Data.NodeContent4TreeNode(tn);
                     GarminCore.BinaryReaderWriter br = null;
                     switch (nc.Type) {
                        case NodeContent.NodeType.PhysicalFile:
                        case NodeContent.NodeType.LogicalFile:
                           br = (nc.Data as NodeContent.Content4File).BinaryReader;
                           break;
                     }
                     if (br != null)
                        br.Dispose();
                  }

                  for (int i = RecentUsedPaths.Count - 1; i >= 0; i--)
                     if (RecentUsedPaths[i] == tpd.Path)
                        RecentUsedPaths.RemoveAt(i);
                  RecentUsedPaths.Insert(0, tpd.Path);

               }
            }
      }

      /// <summary>
      /// liefert die <see cref="TabPageData"/> für die <see cref="TabPage"/>
      /// </summary>
      /// <param name="page"></param>
      /// <returns></returns>
      TabPageData TabPageData4Page(TabPage page) {
         return page.Tag as TabPageData;
      }

      /// <summary>
      /// Daten für den akt. Node anzeigen
      /// <para>aus dem akt. und ev. den darüber liegenden Nodes wird bestimmt, welche Daten anzuzeigen sind</para>
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tpd"></param>
      void ShowData4Node(TreeNode tn, TabPageData tpd) {
         byte[] hex = null;
         long firsthexadr = 0;

         tpd.Hexwin.ClearContent();
         tpd.Info.Clear();
         StringBuilder info = new StringBuilder();

         NodeContent nc = Data.NodeContent4TreeNode(tn);
         TreeViewData tvd = TreeViewData.GetTreeViewData(tn);
         switch (nc.Type) {
            case NodeContent.NodeType.PhysicalFile:
               if (nc.Data is NodeContent.Content4PhysicalFile) {
                  Data.RegisterCoreFiles(tvd, (nc.Data as NodeContent.Content4PhysicalFile).Filename);
                  Data.ShowData4Node_PhysicalFile(info, out hex, out firsthexadr, nc.Data as NodeContent.Content4PhysicalFile, tvd);
               }
               break;

            case NodeContent.NodeType.SimpleFilesystem:
               nc = Data.NodeContent4TreeNode(tn.Parent);  // es muss eine physische Datei übergeordnet sein
               if (nc.Data is NodeContent.Content4PhysicalFile)
                  DataSimpleFilesystem.ShowData(info, out hex, out firsthexadr, nc.Data as NodeContent.Content4PhysicalFile, tvd);
               break;

            case NodeContent.NodeType.LogicalFile:
               if (nc.Data is NodeContent.Content4LogicalFile) {
                  Data.RegisterCoreFiles(tvd, (nc.Data as NodeContent.Content4LogicalFile).Filename);
                  Data.ShowData4Node_LogicalFile(info, out hex, out firsthexadr, nc.Data as NodeContent.Content4LogicalFile, tvd);
               }
               break;

            case NodeContent.NodeType.GarminCommonHeader:
               nc = Data.NodeContent4TreeNode(tn.Parent);  // es muss eine physische oder logische Datei übergeordnet sein
               if (nc.Data is NodeContent.Content4File)
                  Data.ShowData4Node_GarminCommonHeader(info, out hex, out firsthexadr, nc.Data as NodeContent.Content4File, tvd);
               break;

            case NodeContent.NodeType.GarminSpecialHeader:
               nc = Data.NodeContent4TreeNode(tn.Parent);  // es muss eine physische oder logische Datei übergeordnet sein
               if (nc.Data is NodeContent.Content4File)
                  Data.ShowData4Node_GarminSpecialHeader(info, out hex, out firsthexadr, nc.Data as NodeContent.Content4File, tvd);
               break;

            case NodeContent.NodeType.Index:
               if (tn.Parent != null)
                  Data.ShowData4Node_GarminStdFile(info, out hex, out firsthexadr, Data.GetNextContent4File(tn), Data.NodeContent4TreeNode(tn.Parent).Type, (int)nc.Data, tvd);
               break;

            case NodeContent.NodeType.DataRange:
               NodeContent.Content4DataRange c4dr = nc.Data as NodeContent.Content4DataRange;
               info.AppendLine(c4dr.Info);
               hex = c4dr.Bytes;
               firsthexadr = c4dr.FirstAdr;
               break;

            default:
               NodeContent.Content4File c4f = Data.GetNextContent4File(tn);
               Data.RegisterCoreFiles(tvd, c4f.Filename);
               Data.ShowData4Node_GarminStdFile(info, out hex, out firsthexadr, c4f, nc.Type, -1, tvd);
               break;

         }

         tpd.Info.AppendText(info.ToString());
         tpd.Info.Select(0, 0);
         tpd.Info.ScrollToCaret();

         if (hex != null)
            tpd.Hexwin.SetContent(hex, (ulong)firsthexadr);
      }

   }

}
