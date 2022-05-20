using System.Collections.Generic;
using System.Windows.Forms;

namespace GMExplorer {

   /// <summary>
   /// Daten für den gesamten TreeView
   /// <para>Für jeden Basisnamen können eine Reihe von Datenobjekten registriert werden und sind damit bei Bedarf leicht abrufbar.</para>
   /// </summary>
   class TreeViewData {

      /// <summary>
      /// symbolische Datentypen
      /// </summary>
      enum DataType {
         LBL, TRE, RGN, NOD, NET
      }


      /// <summary>
      /// für jeden Basisnamen existiert ein Verzeichnis mit Datentyp und Datenobjekt
      /// </summary>
      SortedDictionary<string, SortedDictionary<DataType, object>> database;


      /// <summary>
      /// der <see cref="TreeView"/> auf den sich die Daten beziehen
      /// </summary>
      public TreeView TreeView { get; private set; }


      public TreeViewData(TreeView tv) {
         TreeView = tv;
         database = new SortedDictionary<string, SortedDictionary<DataType, object>>();
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
         return tn != null && tn.TreeView != null ?
                                    GetTreeViewData(tn.TreeView) :
                                    null;
      }

      /// <summary>
      /// Ex. Daten zu dem Basisnamen?
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public bool Exist(string basename) {
         return database.ContainsKey(basename);
      }

      /// <summary>
      /// liefert das Datenobjekt zum Basisnamen und der Extension (i.A. nur intern verwendet)
      /// </summary>
      /// <param name="basename"></param>
      /// <param name="datatype"></param>
      /// <returns></returns>
      object GetData(string basename, DataType datatype) {
         if (database.TryGetValue(basename, out SortedDictionary<DataType, object> dict)) {
            if (dict.TryGetValue(datatype, out object data))
               return data;
         }
         return null;
      }

      /// <summary>
      /// liefert, falls vorhanden, die TRE-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_TRE GetTRE(string basename) {
         return GetData(basename, DataType.TRE) as GarminCore.Files.StdFile_TRE;
      }

      /// <summary>
      /// liefert, falls vorhanden, die LBL-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_LBL GetLBL(string basename) {
         return GetData(basename, DataType.LBL) as GarminCore.Files.StdFile_LBL;
      }

      /// <summary>
      /// liefert, falls vorhanden, die RGN-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_RGN GetRGN(string basename) {
         return GetData(basename, DataType.RGN) as GarminCore.Files.StdFile_RGN;
      }

      /// <summary>
      /// liefert, falls vorhanden, die NET-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_NET GetNET(string basename) {
         return GetData(basename, DataType.NET) as GarminCore.Files.StdFile_NET;
      }

      /// <summary>
      /// liefert, falls vorhanden, die NOD-Datei zum Basisname
      /// </summary>
      /// <param name="basename"></param>
      /// <returns></returns>
      public GarminCore.Files.StdFile_NOD GetNOD(string basename) {
         return GetData(basename, DataType.NOD) as GarminCore.Files.StdFile_NOD;
      }


      /// <summary>
      /// registriert das Datenobjekt zum Basisnamen und der Extension (i.A. nur intern verwendet)
      /// </summary>
      /// <param name="basename"></param>
      /// <param name="datatype"></param>
      /// <param name="data"></param>
      void Register(string basename, DataType datatype, object data) {
         if (!database.TryGetValue(basename, out SortedDictionary<DataType, object> dict)) {
            dict = new SortedDictionary<DataType, object>();
            database.Add(basename, dict);
         }
         if (!dict.ContainsKey(datatype))
            dict.Add(datatype, data);
         else
            dict[datatype] = data;
      }

      public void Register(string basename, GarminCore.Files.StdFile_LBL lbl) {
         Register(basename, DataType.LBL, lbl);
      }

      public void Register(string basename, GarminCore.Files.StdFile_TRE tre) {
         Register(basename, DataType.TRE, tre);
      }

      public void Register(string basename, GarminCore.Files.StdFile_RGN rgn) {
         Register(basename, DataType.RGN, rgn);
      }

      public void Register(string basename, GarminCore.Files.StdFile_NET net) {
         Register(basename, DataType.NET, net);
      }

      public void Register(string basename, GarminCore.Files.StdFile_NOD nod) {
         Register(basename, DataType.NOD, nod);
      }



   }

}
