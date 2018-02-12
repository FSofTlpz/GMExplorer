using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataMDX : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.File_MDX mdx, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.MDX_MapEntry, mdx, "MapEntrys"));

      }

      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.File_MDX mdx, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.MDX_MapEntry:
               for (int i = 0; i < mdx.Maps.Count; i++) {
                  AppendNode(tn, NodeContent.NodeType.Index, i, "MapEntry " + i.ToString());
               }
               break;

         }
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
         GarminCore.Files.File_MDX mdx = filedata.GetGarminFileAsMDX();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;

         switch (nodetype) {
            case NodeContent.NodeType.MDX_MapEntry:
               if (idx < 0) {
                  firsthexadr = 0;
                  hexlen = 14;
                  info.AppendLine("           (6 Bytes):  'Midxd'");
                  info.AppendLine("Unknown1   (2 Bytes): " + DecimalAndHexAndBinary(mdx.Unknown1));
                  info.AppendLine("Unknown2   (2 Bytes): " + DecimalAndHexAndBinary(mdx.Unknown2));
                  info.AppendLine("MapEntrys  (4 Bytes): " + DecimalAndHexAndBinary(mdx.Maps.Count));
               } else {
                  firsthexadr = 14 + idx * 12;
                  hexlen = 12;

                  GarminCore.Files.File_MDX.MapEntry mapentry = mdx.Maps[idx];
                  info.AppendLine("MapID       (4 Byte): " + DecimalAndHexAndBinary(mapentry.MapID));
                  info.AppendLine("ProductID   (2 Byte): " + DecimalAndHexAndBinary(mapentry.ProductID));
                  info.AppendLine("FamilyID    (2 Byte): " + DecimalAndHexAndBinary(mapentry.FamilyID));
                  info.AppendLine("MapNumber   (4 Byte): " + DecimalAndHexAndBinary(mapentry.MapNumber));
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
