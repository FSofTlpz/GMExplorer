using System.Collections.Generic;
using System.Windows.Forms;

namespace GMExplorer {

   class TreeViewData {

      class Tile {

         public GarminCore.Files.StdFile_LBL lbl;
         public GarminCore.Files.StdFile_TRE tre;
         public GarminCore.Files.StdFile_RGN rgn;
         public GarminCore.Files.StdFile_NET net;
         public GarminCore.Files.StdFile_NOD nod;

         public Tile(GarminCore.Files.StdFile_LBL lbl,
                     GarminCore.Files.StdFile_TRE tre,
                     GarminCore.Files.StdFile_RGN rgn,
                     GarminCore.Files.StdFile_NET net,
                     GarminCore.Files.StdFile_NOD nod) {
            this.lbl = lbl;
            this.tre = tre;
            this.rgn = rgn;
            this.net = net;
            this.nod = nod;
         }

      }

      SortedDictionary<string, Tile> TileFiles;

      public TreeView TreeView { get; private set; }


      public TreeViewData(TreeView tv) {
         TileFiles = new SortedDictionary<string, Tile>();
         TreeView = tv;
      }


      public bool Exist(string basename) {
         return TileFiles.ContainsKey(basename);
      }

      public void Register(string basename,
                           GarminCore.Files.StdFile_LBL lbl,
                           GarminCore.Files.StdFile_TRE tre,
                           GarminCore.Files.StdFile_RGN rgn,
                           GarminCore.Files.StdFile_NET net,
                           GarminCore.Files.StdFile_NOD nod) {
         Tile tile;
         if (!TileFiles.TryGetValue(basename, out tile))
            TileFiles.Add(basename, new Tile(lbl, tre, rgn, net, nod));
      }

      /// <summary>
      /// liefert, falls vorhanden, die TRE-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_TRE GetTRE(string basename) {
         Tile tile;
         return TileFiles.TryGetValue(basename, out tile) ? tile.tre : null;
      }

      /// <summary>
      /// liefert, falls vorhanden, die LBL-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_LBL GetLBL(string basename) {
         Tile tile;
         return TileFiles.TryGetValue(basename, out tile) ? tile.lbl : null;
      }

      /// <summary>
      /// liefert, falls vorhanden, die RGN-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_RGN GetRGN(string basename) {
         Tile tile;
         return TileFiles.TryGetValue(basename, out tile) ? tile.rgn : null;
      }

      /// <summary>
      /// liefert, falls vorhanden, die NET-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_NET GetNET(string basename) {
         Tile tile;
         return TileFiles.TryGetValue(basename, out tile) ? tile.net : null;
      }

      /// <summary>
      /// liefert, falls vorhanden, die NOD-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_NOD GetNOD(string basename) {
         Tile tile;
         return TileFiles.TryGetValue(basename, out tile) ? tile.nod : null;
      }

      /// <summary>
      /// liefert das <see cref="TreeViewData"/>-Objekt eines TreeView (oder null)
      /// </summary>
      /// <param name="tv"></param>
      /// <returns></returns>
      static public TreeViewData GetTreeViewData(TreeView tv) {
         if (tv.Tag != null &&
             tv.Tag is TreeViewData)
            return tv.Tag as TreeViewData;
         return null;
      }

      /// <summary>
      /// liefert das <see cref="TreeViewData"/>-Objekt des TreeView des TreeNode (oder null)
      /// </summary>
      /// <param name="tn"></param>
      /// <returns></returns>
      static public TreeViewData GetTreeViewData(TreeNode tn) {
         return tn != null && tn.TreeView != null ? GetTreeViewData(tn.TreeView) : null;
      }

   }

}
