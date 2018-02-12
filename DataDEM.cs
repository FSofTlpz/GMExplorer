using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataDEM : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_DEM dem, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, dem, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, dem, "Garmin DEM-Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.DEM_Zoomlevel, dem, "Zoomlevel"));
      }

      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_DEM dem, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.DEM_Zoomlevel:
               for (int i = 0; i < dem.ZoomlevelCount; i++) {
                  TreeNode tn2 = AppendNode(tn, NodeContent.NodeType.Index, i, "Zoomlevel " + i.ToString());
                  int subtiles = (dem.ZoomLevel[i].ZoomlevelItem.MaxIdxHoriz + 1) * (dem.ZoomLevel[i].ZoomlevelItem.MaxIdxVert + 1);
                  for (int j = 0; j < subtiles; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j, "Subtile " + j.ToString());
               }
               break;
         }
      }

      /// <summary>
      /// Knoten für einen Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_DEM dem) {
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(dem.Flags, 0x15, "Flags: " + DecimalAndHexAndBinary(dem.Flags), false), "Flags");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(dem.ZoomlevelCount, 0x19, "ZoomlevelCount: " + DecimalAndHexAndBinary(dem.ZoomlevelCount)), "ZoomlevelCount");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(dem.Unknown_0x1B, 0, dem.Unknown_0x1B.Length, NodeContent.Content4DataRange.DataType.Other, 0x1B, "Unknown 0x1B"), "Unknown 0x1B");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(dem.ZoomlevelRecordSize, 0x1F, "ZoomlevelRecordSize: " + DecimalAndHexAndBinary(dem.ZoomlevelRecordSize)), "ZoomlevelRecordSize");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(dem.PtrZoomlevel, 0x21, "ZoomlevelOffset: " + DecimalAndHexAndBinary(dem.PtrZoomlevel), false), "ZoomlevelOffset");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(dem.Unknown_0x25, 0, dem.Unknown_0x25.Length, NodeContent.Content4DataRange.DataType.Other, 0x25, "Unknown 0x25"), "Unknown 0x25");
      }

      /// <summary>
      /// dateitypischer Header
      /// </summary>
      /// <param name="info"></param>
      /// <param name="dem"></param>
      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_DEM dem) {
         info.AppendLine("Flags                (4 Byte):        " + DecimalAndHexAndBinary(dem.Flags, 32));
         info.AppendLine("   HeigthInFeet              (Bit 1): " + dem.HeigthInFeet.ToString());
         info.AppendLine("ZoomlevelCount       (2 Byte):        " + DecimalAndHexAndBinary(dem.ZoomlevelCount));
         info.AppendLine("Unknown 0x1B         (4 Byte):        " + HexString(dem.Unknown_0x1B));
         info.AppendLine("ZoomlevelRecordSize  (2 Byte):        " + DecimalAndHexAndBinary(dem.ZoomlevelRecordSize));
         info.AppendLine("ZoomlevelOffset      (4 Byte):        " + DecimalAndHexAndBinary(dem.PtrZoomlevel));
         info.AppendLine("Unknown 0x25         (" + dem.Unknown_0x25.Length.ToString() + " Byte):        " + HexString(dem.Unknown_0x25));
      }

      /// <summary>
      /// Funktion für alle Datei-Infos
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="nodetype">"Thema" der Info</param>
      /// <param name="idx">wenn größer oder gleich 0, dann der Index auf ein Objekt einer Tabelle</param>
      /// <param name="tn"></param>
      public static void SectionAndIndex(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, NodeContent.NodeType nodetype, int idx, TreeViewData tvd) {
         GarminCore.Files.StdFile_DEM dem = filedata.GetGarminFileAsDEM();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;

         switch (nodetype) {
            case NodeContent.NodeType.DEM_Zoomlevel:
               firsthexadr = dem.PtrZoomlevel;
               if (idx < 0) {
                  info.AppendLine("ZoomlevelCount:       " + DecimalAndHexAndBinary(dem.ZoomlevelCount));
                  info.AppendLine("ZoomlevelRecordSize:  " + DecimalAndHexAndBinary(dem.ZoomlevelRecordSize));
                  hexlen = dem.ZoomlevelRecordSize * dem.ZoomlevelCount;
               } else {
                  GarminCore.Files.DEM.ZoomlevelTableitem record = dem.ZoomLevel[idx].ZoomlevelItem;

                  info.AppendLine("SpecType                (1 Byte): " + DecimalAndHexAndBinary(record.SpecType));
                  info.AppendLine("Number                  (1 Byte): " + DecimalAndHexAndBinary(record.No));
                  info.AppendLine("Tilesize horizontal     (4 Byte): " + DecimalAndHexAndBinary(record.PointsHoriz));
                  info.AppendLine("Tilesize vertical       (4 Byte): " + DecimalAndHexAndBinary(record.PointsVert));
                  info.AppendLine("LastRowHeight           (4 Byte): " + DecimalAndHexAndBinary(record.LastRowHeight));
                  info.AppendLine("LastColWidth            (4 Byte): " + DecimalAndHexAndBinary(record.LastColWidth));
                  info.AppendLine("Unknown12               (2 Byte): " + DecimalAndHexAndBinary(record.Unknown12));
                  info.AppendLine("MaxIdxHoriz             (4 Byte): " + DecimalAndHexAndBinary(record.MaxIdxHoriz));
                  info.AppendLine("MaxIdxVert              (4 Byte): " + DecimalAndHexAndBinary(record.MaxIdxVert));
                  info.AppendLine("Structure               (2 Byte): " + DecimalAndHexAndBinary(record.Structure, 16));
                  info.AppendLine("   OffsetSize              (Bit 0,1) +1: " + record.Structure_OffsetSize + " Byte");
                  info.AppendLine("   BaseheightSize          (Bit 2) +1:   " + record.Structure_BaseheightSize + " Byte");
                  info.AppendLine("   DiffSize                (Bit 3) +1:   " + record.Structure_DiffSize + " Byte");
                  info.AppendLine("   CodingtypeSize          (Bit 4):      " + record.Structure_CodingtypeSize + " Byte");
                  info.AppendLine("SubtileTableitemSize    (2 Byte): " + DecimalAndHexAndBinary(record.SubtileTableitemSize));
                  info.AppendLine("SubtileTableOffset      (4 Byte): " + DecimalAndHexAndBinary(record.PtrSubtileTable));
                  info.AppendLine("HeightdataOffset        (4 Byte): " + DecimalAndHexAndBinary(record.PtrHeightdata));
                  info.AppendLine("Left                    (4 Byte): " + DecimalAndHexAndBinary(record.west) + "; " + record.West.ToString() + "°");
                  info.AppendLine("Top                     (4 Byte): " + DecimalAndHexAndBinary(record.north) + "; " + record.North.ToString() + "°");
                  info.AppendLine("PointDistanceVertical   (4 Byte): " + DecimalAndHexAndBinary(record.pointDistanceVert) + "; " + record.PointDistanceVert.ToString() + "°");
                  info.AppendLine("PointDistanceHorizontal (4 Byte): " + DecimalAndHexAndBinary(record.pointDistanceHoriz) + "; " + record.PointDistanceHoriz.ToString() + "°");
                  info.AppendLine("MinHeight               (2 Byte): " + DecimalAndHexAndBinary(record.MinHeight));
                  info.AppendLine("MaxHeight               (2 Byte): " + DecimalAndHexAndBinary(record.MaxHeight));

                  firsthexadr += idx * dem.ZoomlevelRecordSize;
                  hexlen = dem.ZoomlevelRecordSize;
               }
               break;

            case NodeContent.NodeType.Index:
               // etwas tricky, aber nc.Data des übergeordneten Knotens ist der ZoomLevel-Index
               if (tvd.TreeView.SelectedNode != null) {
                  int zoomlevelidxidx = -1;
                  NodeContent nc = NodeContent4TreeNode(tvd.TreeView.SelectedNode.Parent);
                  if (nc.Type == NodeContent.NodeType.Index)
                     zoomlevelidxidx = (int)nc.Data;
                  if (zoomlevelidxidx >= 0) {
                     GarminCore.Files.DEM.ZoomlevelTableitem zoomlevel = dem.ZoomLevel[zoomlevelidxidx].ZoomlevelItem;
                     GarminCore.Files.DEM.SubtileTableitem subtile = dem.ZoomLevel[zoomlevelidxidx].Subtiles[idx].Tableitem;
                     info.AppendLine("Offset     (" + zoomlevel.Structure_OffsetSize + " Byte): " + DecimalAndHexAndBinary(subtile.Offset));
                     info.AppendLine("Baseheight (" + zoomlevel.Structure_BaseheightSize + " Byte): " + DecimalAndHexAndBinary(subtile.Baseheight));
                     info.AppendLine("Diff       (" + zoomlevel.Structure_DiffSize + " Byte): " + DecimalAndHexAndBinary(subtile.Diff));
                     if (zoomlevel.Structure_CodingtypeSize > 0)
                        info.AppendLine("Type       (" + zoomlevel.Structure_CodingtypeSize + " Byte): " + DecimalAndHexAndBinary(subtile.Type));
                     info.AppendLine();
                     info.AppendLine("coded data (" + DecimalAndHexAndBinary(dem.ZoomLevel[zoomlevelidxidx].Subtiles[idx].DataLength) + " Bytes):");

                     firsthexadr = zoomlevel.PtrHeightdata + subtile.Offset;
                     hexlen = dem.ZoomLevel[zoomlevelidxidx].Subtiles[idx].DataLength;
                  }
               }
               break;

            default:
               info.AppendLine("internal error: no info for nodetype '" + nodetype.ToString() + "'");
               break;
         }

         if (hexlen > 0)
            hex = HexRange(firsthexadr, filedata.BinaryReader, hexlen);
      }


   }
}
