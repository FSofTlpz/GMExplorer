using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataTDB : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.File_TDB tdb, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);
         bool TilemapNodeExist = false;

         for (int i = 0; i < tdb.BlockHeaderTypList.Count; i++) {
            switch (tdb.BlockHeaderTypList[i]) {
               case GarminCore.Files.File_TDB.BlockHeader.Typ.Copyright:
                  AppendNode(AppendNode(tn, NodeContent.NodeType.TDB_Copyright, tdb, "Copyright"));
                  break;

               case GarminCore.Files.File_TDB.BlockHeader.Typ.Crc:
                  AppendNode(tn, NodeContent.NodeType.TDB_Crc, tdb, "Crc");
                  break;

               case GarminCore.Files.File_TDB.BlockHeader.Typ.Description:
                  AppendNode(tn, NodeContent.NodeType.TDB_Description, tdb, "Description");
                  break;

               case GarminCore.Files.File_TDB.BlockHeader.Typ.Header:
                  AppendNode(tn, NodeContent.NodeType.TDB_Header, tdb, "Header");
                  break;

               case GarminCore.Files.File_TDB.BlockHeader.Typ.Overviewmap:
                  AppendNode(tn, NodeContent.NodeType.TDB_Overviewmap, tdb, "Overviewmap");
                  break;

               case GarminCore.Files.File_TDB.BlockHeader.Typ.Tilemap:
                  if (!TilemapNodeExist) {
                     AppendNode(AppendNode(tn, NodeContent.NodeType.TDB_Tilemap, tdb, "Tilemaps"));
                     TilemapNodeExist = true;
                  }
                  break;

               default:
                  AppendNode(tn, NodeContent.NodeType.TDB_Unknown, tdb, "Unknown");
                  break;
            }
         }
      }

      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.File_TDB tdb, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.TDB_Copyright:
               for (int i = 0; i < tdb.Copyright.Segments.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Segment " + i.ToString());
               break;

            case NodeContent.NodeType.TDB_Tilemap:
               for (int i = 0, idx = 0; i < tdb.BlockHeaderTypList.Count; i++) {
                  if (tdb.BlockHeaderTypList[i] == GarminCore.Files.File_TDB.BlockHeader.Typ.Tilemap) {
                     AppendNode(tn, NodeContent.NodeType.Index, idx, "Tilemap " + idx.ToString());
                     idx++;
                  }
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
         GarminCore.Files.File_TDB tdb = filedata.GetGarminFileAsTDB();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;
         GarminCore.DataBlock block;

         switch (nodetype) {
            case NodeContent.NodeType.TDB_Header:
               block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Header);
               firsthexadr = block.Offset;
               hexlen = (int)block.Length;

               info.AppendLine("ProductID             (2 Byte): " + DecimalAndHexAndBinary(tdb.Head.ProductID));
               info.AppendLine("FamilyID              (2 Byte): " + DecimalAndHexAndBinary(tdb.Head.FamilyID));
               info.AppendLine("TDBVersion            (2 Byte): " + DecimalAndHexAndBinary(tdb.Head.TDBVersion));
               info.AppendLine("MapSeriesName         (" + (tdb.Head.MapSeriesName.Length + 1).ToString() + " Byte): '" + tdb.Head.MapSeriesName + "'");
               info.AppendLine("ProductVersion        (2 Byte): " + DecimalAndHexAndBinary(tdb.Head.ProductVersion));
               info.AppendLine("MapFamilyName         (" + (tdb.Head.MapFamilyName.Length + 1).ToString() + " Byte): '" + tdb.Head.MapFamilyName + "'");
               if (tdb.Head.TDBVersion >= 407) {
                  info.AppendLine("Unknown1              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown1));
                  info.AppendLine("MaxCoordbits4Overview (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.MaxCoordbits4Overview));
                  info.AppendLine("Unknown2              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown2));
                  info.AppendLine("Unknown3              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown3));
                  info.AppendLine("Unknown4              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown4));
                  info.AppendLine("Unknown5              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown5));
                  info.AppendLine("Unknown6              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown6));
                  info.AppendLine("Unknown7              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown7));
                  info.AppendLine("Unknown8              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown8));
                  info.AppendLine("Unknown9              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown9));
                  info.AppendLine("HighestRoutable       (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.HighestRoutable));
                  info.AppendLine("Unknown10             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown10));
                  info.AppendLine("Unknown11             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown11));
                  info.AppendLine("Unknown12             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown12));
                  info.AppendLine("Unknown13             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown13));
                  info.AppendLine("Unknown14             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown14));
                  info.AppendLine("Unknown15             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown15));
                  info.AppendLine("Unknown16             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown16));
                  info.AppendLine("Unknown17             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown17));
                  info.AppendLine("Unknown18             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown18));
                  info.AppendLine("Unknown19             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown19));
                  info.AppendLine("Unknown20             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown20));
                  info.AppendLine("Unknown21             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown21));
                  info.AppendLine("Unknown22             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown22));
                  info.AppendLine("Unknown23             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown23));
                  info.AppendLine("Unknown24             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown24));
                  info.AppendLine("Unknown25             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown25));
                  info.AppendLine("Unknown26             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown26));
                  info.AppendLine("Unknown27             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown27));
                  info.AppendLine("Unknown28             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown28));
                  info.AppendLine("CodePage              (4 Byte): " + DecimalAndHexAndBinary(tdb.Head.CodePage));
                  info.AppendLine("Unknown29             (4 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown29));
                  info.AppendLine("Routable              (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Routable));
                  info.AppendLine("HasProfileInformation (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.HasProfileInformation));
                  info.AppendLine("HasDEM                (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.HasDEM));
                  if (tdb.Head.TDBVersion >= 411) {
                     info.AppendLine("Unknown30             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown30));
                     info.AppendLine("Unknown31             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown31));
                     info.AppendLine("Unknown32             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown32));
                     info.AppendLine("Unknown33             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown33));
                     info.AppendLine("Unknown34             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown34));
                     info.AppendLine("Unknown35             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown35));
                     info.AppendLine("Unknown36             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown36));
                     info.AppendLine("Unknown37             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown37));
                     info.AppendLine("Unknown38             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown38));
                     info.AppendLine("Unknown39             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown39));
                     info.AppendLine("Unknown40             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown40));
                     info.AppendLine("Unknown41             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown41));
                     info.AppendLine("Unknown42             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown42));
                     info.AppendLine("Unknown43             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown43));
                     info.AppendLine("Unknown44             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown44));
                     info.AppendLine("Unknown45             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown45));
                     info.AppendLine("Unknown46             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown46));
                     info.AppendLine("Unknown47             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown47));
                     info.AppendLine("Unknown48             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown48));
                     info.AppendLine("Unknown49             (1 Byte): " + DecimalAndHexAndBinary(tdb.Head.Unknown49));
                  }
               }
               break;

            case NodeContent.NodeType.TDB_Copyright:
               block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Copyright);
               firsthexadr = block.Offset;
               hexlen = (int)block.Length;
               if (idx < 0) {
                  info.AppendLine("Segmente: " + tdb.Copyright.Segments.Count.ToString());
               } else {
                  GarminCore.Files.File_TDB.SegmentedCopyright.Segment segment = tdb.Copyright.Segments[idx];
                  info.AppendLine("CopyrightCode   (1 Byte): " + segment.CopyrightCode.ToString() + " / " + DecimalAndHexAndBinary((byte)segment.CopyrightCode));
                  info.AppendLine("WhereCode       (1 Byte): " + segment.WhereCode.ToString() + " / " + DecimalAndHexAndBinary((byte)segment.WhereCode));
                  info.AppendLine("ExtraProperties (2 Byte): " + DecimalAndHexAndBinary(segment.ExtraProperties));
                  info.AppendLine("Copyright       (" + (segment.Copyright.Length + 1).ToString() + " Byte): '" + segment.Copyright + "'");
                  for (int i = 0; i < tdb.Copyright.Segments.Count && i < idx; i++)
                     firsthexadr += (uint)(4 + tdb.Copyright.Segments[i].Copyright.Length + 1);
                  hexlen = 4 + segment.Copyright.Length + 1;
               }
               break;

            case NodeContent.NodeType.TDB_Crc:
               block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Crc);
               firsthexadr = block.Offset;
               hexlen = (int)block.Length;
               info.AppendLine("Unknown1 (2 Byte): " + DecimalAndHexAndBinary(tdb.Crc.Unknown1));
               info.AppendLine("A        (1 Byte): " + DecimalAndHexAndBinary(tdb.Crc.A));
               info.AppendLine("Unknown2 (4 Byte): " + DecimalAndHexAndBinary(tdb.Crc.Unknown2));
               info.AppendLine("Unknown3 (2 Byte): " + DecimalAndHexAndBinary(tdb.Crc.Unknown3));
               info.AppendLine("B        (1 Byte): " + DecimalAndHexAndBinary(tdb.Crc.B));
               info.AppendLine("Unknown4 (2 Byte): " + DecimalAndHexAndBinary(tdb.Crc.Unknown4));
               info.AppendLine("C        (1 Byte): " + DecimalAndHexAndBinary(tdb.Crc.C));
               info.AppendLine("Unknown5 (4 Byte): " + DecimalAndHexAndBinary(tdb.Crc.Unknown5));
               info.AppendLine("D        (1 Byte): " + DecimalAndHexAndBinary(tdb.Crc.D));
               info.AppendLine("Unknown6 (2 Byte): " + DecimalAndHexAndBinary(tdb.Crc.Unknown6));
               break;

            case NodeContent.NodeType.TDB_Description:
               block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Description);
               firsthexadr = block.Offset;
               hexlen = (int)block.Length;
               info.AppendLine("Unknown1       (1 Byte): " + DecimalAndHexAndBinary(tdb.Mapdescription.Unknown1));
               info.AppendLine("Mapdescription (" + (tdb.Mapdescription.Text.Length + 1).ToString() + " Byte): '" + tdb.Mapdescription.Text + "'");
               break;

            case NodeContent.NodeType.TDB_Overviewmap:
               block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Overviewmap);
               firsthexadr = block.Offset;
               hexlen = (int)block.Length;
               info.AppendLine("Mapnumber       (4 Byte): " + DecimalAndHexAndBinary(tdb.Overviewmap.Mapnumber));
               info.AppendLine("ParentMapnumber (4 Byte): " + DecimalAndHexAndBinary(tdb.Overviewmap.ParentMapnumber));
               info.AppendLine("North           (4 Byte): " + DecimalAndHexAndBinary(tdb.Overviewmap.north) + "; " + tdb.Overviewmap.North.ToString() + "°");
               info.AppendLine("East            (4 Byte): " + DecimalAndHexAndBinary(tdb.Overviewmap.east) + "; " + tdb.Overviewmap.East.ToString() + "°");
               info.AppendLine("South           (4 Byte): " + DecimalAndHexAndBinary(tdb.Overviewmap.south) + "; " + tdb.Overviewmap.South.ToString() + "°");
               info.AppendLine("West            (4 Byte): " + DecimalAndHexAndBinary(tdb.Overviewmap.west) + "; " + tdb.Overviewmap.West.ToString() + "°");
               info.AppendLine("Description     (" + (tdb.Overviewmap.Description.Length + 1).ToString() + " Byte): '" + tdb.Overviewmap.Description + "'");
               break;

            case NodeContent.NodeType.TDB_Tilemap:
               if (idx < 0) {
                  info.AppendLine("Tilemaps: " + tdb.Tilemap.Count);
               } else {
                  block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Tilemap, idx);
                  firsthexadr = block.Offset;
                  hexlen = (int)block.Length;

                  int readlen = 0;
                  GarminCore.Files.File_TDB.TileMap tilemap = tdb.Tilemap[idx];
                  info.AppendLine("Mapnumber       (4 Byte): " + DecimalAndHexAndBinary(tilemap.Mapnumber));
                  info.AppendLine("ParentMapnumber (4 Byte): " + DecimalAndHexAndBinary(tilemap.ParentMapnumber));
                  info.AppendLine("North           (4 Byte): " + DecimalAndHexAndBinary(tilemap.north) + "; " + tilemap.North.ToString() + "°");
                  info.AppendLine("East            (4 Byte): " + DecimalAndHexAndBinary(tilemap.east) + "; " + tilemap.East.ToString() + "°");
                  info.AppendLine("South           (4 Byte): " + DecimalAndHexAndBinary(tilemap.south) + "; " + tilemap.South.ToString() + "°");
                  info.AppendLine("West            (4 Byte): " + DecimalAndHexAndBinary(tilemap.west) + "; " + tilemap.West.ToString() + "°");
                  readlen += 6 * 4;
                  info.AppendLine("Description     (" + (tilemap.Description.Length + 1).ToString() + " Byte): '" + tilemap.Description + "'");
                  readlen += tilemap.Description.Length + 1;
                  info.AppendLine("Unknown1        (2 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown1));
                  info.AppendLine("SubCount        (2 Byte): " + DecimalAndHexAndBinary(tilemap.SubCount));
                  info.AppendLine("Size:");
                  for (int i = 0; i < tilemap.DataSize.Count; i++)
                     info.AppendLine("                (4 Byte): " + DecimalAndHexAndBinary(tilemap.DataSize[i]));
                  readlen += 2 * 2 + tilemap.DataSize.Count * 4;
                  if (block.Length - readlen >= 7) {
                     info.AppendLine("HasCopyright    (1 Byte): " + DecimalAndHexAndBinary(tilemap.HasCopyright));
                     info.AppendLine("Unknown2        (1 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown2));
                     info.AppendLine("Unknown3        (1 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown3));
                     info.AppendLine("Unknown4        (1 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown4));
                     info.AppendLine("Unknown5        (1 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown5));
                     info.AppendLine("Unknown6        (1 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown6));
                     info.AppendLine("Unknown7        (1 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown7));
                     readlen += 7;
                     if (block.Length - readlen > 0) {
                        info.AppendLine("Name:");
                        for (int i = 0; i < tilemap.Name.Count; i++)
                           info.AppendLine("                (" + (tilemap.Name[i].Length + 1).ToString() + " Byte): '" + tilemap.Name[i] + "'");
                        if (block.Length - readlen >= 2) {
                           info.AppendLine("Unknown8        (8 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown8));
                           info.AppendLine("Unknown9        (9 Byte): " + DecimalAndHexAndBinary(tilemap.Unknown9));
                        }
                     }
                  }
               }
               break;

            case NodeContent.NodeType.TDB_Unknown:
               block = TDBBlock(tdb, GarminCore.Files.File_TDB.BlockHeader.Typ.Unknown);
               firsthexadr = block.Offset;
               hexlen = (int)block.Length;
               break;

            default:
               info.AppendLine("internal error: no info for nodetype '" + nodetype.ToString() + "'");
               break;
         }

         if (hexlen > 0)
            hex = HexRange(firsthexadr, filedata.BinaryReader, hexlen);
      }

      /// <summary>
      /// liefert den Datenbereich in der Datei für einen bestimmten Block
      /// </summary>
      /// <param name="tdb"></param>
      /// <param name="tdbblocktype"></param>
      /// <param name="count">0 für den 1. Block dieses Typs, 1 für den 2. usw.</param>
      /// <returns></returns>
      static GarminCore.DataBlock TDBBlock(GarminCore.Files.File_TDB tdb, GarminCore.Files.File_TDB.BlockHeader.Typ tdbblocktype, int count = 0) {
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         for (int i = 0; i < tdb.BlockHeaderTypList.Count; i++) {
            block.Offset += 3; // Def. des Blockes
            if (tdb.BlockHeaderTypList[i] == tdbblocktype) {
               if (count == 0) {
                  block.Length = (uint)tdb.BlockLength[i];
                  break;
               }
               count--;
            }
            block.Offset += (uint)tdb.BlockLength[i];
         }
         return block;
      }

   }
}
