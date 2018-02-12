using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataMPS : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.File_MPS mps, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.MPS_MapEntry, mps, "MapEntrys"));

      }

      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.File_MPS mps, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.MPS_MapEntry:
               for (int i = 0; i < mps.Maps.Count; i++) {
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
         GarminCore.Files.File_MPS mps = filedata.GetGarminFileAsMPS();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;
         GarminCore.DataBlock block;

         switch (nodetype) {
            case NodeContent.NodeType.MPS_MapEntry:
               if (idx < 0) {
                  info.AppendLine("MapEntrys: " + mps.Maps.Count);
               } else {
                  block = MapEntryBlock(mps, idx);
                  firsthexadr = block.Offset;
                  hexlen = (int)block.Length;

                  GarminCore.Files.File_MPS.MapEntry mapentry = mps.Maps[idx];
                  info.AppendLine("Typ         (1 Byte): " + mapentry.Typ + " / " + DecimalAndHexAndBinary((byte)mapentry.Typ));
                  info.AppendLine("BlockLength (2 Byte): " + DecimalAndHexAndBinary(block.Length - 3));
                  switch (mapentry.Typ) {
                     case 'L':
                        info.AppendLine("ProductID   (2 Byte): " + DecimalAndHexAndBinary(mapentry.ProductID));
                        info.AppendLine("FamilyID    (2 Byte): " + DecimalAndHexAndBinary(mapentry.FamilyID));
                        info.AppendLine("MapNumber   (2 Byte): " + DecimalAndHexAndBinary(mapentry.MapNumber));
                        info.AppendLine("Names:");
                        for (int i = 0; i < mapentry.Name.Count; i++) 
                           info.AppendLine("            (" + (mapentry.Name[i].Length + 1).ToString() + " Byte): '" + mapentry.Name[i] + "'");
                        info.AppendLine("Unknown0    (4 Byte): " + DecimalAndHexAndBinary(mapentry.Unknown0));
                        info.AppendLine("Unknown1    (4 Byte): " + DecimalAndHexAndBinary(mapentry.Unknown1));
                        break;

                     case 'P':
                        info.AppendLine("ProductID   (2 Byte): " + DecimalAndHexAndBinary(mapentry.ProductID));
                        info.AppendLine("FamilyID    (2 Byte): " + DecimalAndHexAndBinary(mapentry.FamilyID));
                        info.AppendLine("Unknown2    (2 Byte): " + DecimalAndHexAndBinary(mapentry.Unknown2));
                        info.AppendLine("Unknown3    (2 Byte): " + DecimalAndHexAndBinary(mapentry.Unknown3));
                        info.AppendLine("Unknown4    (2 Byte): " + DecimalAndHexAndBinary(mapentry.Unknown4));
                        break;

                     case 'F':
                        info.AppendLine("ProductID   (2 Byte): " + DecimalAndHexAndBinary(mapentry.ProductID));
                        info.AppendLine("FamilyID    (2 Byte): " + DecimalAndHexAndBinary(mapentry.FamilyID));
                        info.AppendLine("Names:");
                        for (int i = 0; i < mapentry.Name.Count; i++) 
                           info.AppendLine("            (" + (mapentry.Name[i].Length + 1).ToString() + " Byte): '" + mapentry.Name[i] + "'");
                        break;

                     case 'V':
                        info.AppendLine("Names:");
                        for (int i = 0; i < mapentry.Name.Count; i++) 
                           info.AppendLine("            (" + (mapentry.Name[i].Length + 1).ToString() + " Byte): '" + mapentry.Name[i] + "'");
                        info.AppendLine("Unknown5    (1 Byte): " + DecimalAndHexAndBinary(mapentry.Unknown5));
                        break;

                     default:
                        break;

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

      /// <summary>
      /// liefert Pos. und Länge des Entrys
      /// </summary>
      /// <param name="mps"></param>
      /// <param name="idx"></param>
      /// <returns></returns>
      static GarminCore.DataBlock MapEntryBlock(GarminCore.Files.File_MPS mps, int idx) {
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         for (int i = 0; i < mps.Maps.Count && i < idx; i++) {
            GarminCore.Files.File_MPS.MapEntry mapentry = mps.Maps[i];
            block.Offset = block.Length;

            block.Length = 3; // Typ, Länge
            switch (mapentry.Typ) {
               case 'L':
                  block.Length += 3 * 2;
                  for (int j = 0; j < mapentry.Name.Count; j++)
                     block.Length += (uint)(mapentry.Name[j].Length + 1);
                  block.Length += 2 * 4;
                  break;

               case 'P':
                  block.Length += 8;
                  break;

               case 'F':
                  block.Length += 2 * 2;
                  for (int j = 0; j < mapentry.Name.Count; j++)
                     block.Length += (uint)(mapentry.Name[j].Length + 1);
                  break;

               case 'V':
                  for (int j = 0; j < mapentry.Name.Count; j++)
                     block.Length += (uint)(mapentry.Name[j].Length + 1);
                  break;
            }
         }
         return block;
      }

   }
}
