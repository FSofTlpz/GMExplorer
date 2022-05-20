using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataTYP : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_TYP typ, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, typ, "Garmin Common Header"));
         AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, typ, "Garmin TYP-Header");

         if (typ.PointTableBlock.Count > 0) {
            TreeNode tn2 = AppendNode(tn, NodeContent.NodeType.TYP_PointTableBlock, typ, "PointTableBlock");
            AppendNode(AppendNode(tn2, NodeContent.NodeType.TYP_PointTable, typ, "PointTable"));
         }
         if (typ.PolylineTableBlock.Count > 0) {
            TreeNode tn2 = AppendNode(tn, NodeContent.NodeType.TYP_PolylineTableBlock, typ, "PolylineTableBlock");
            AppendNode(AppendNode(tn2, NodeContent.NodeType.TYP_PolylineTable, typ, "PolylineTable"));
         }
         if (typ.PolygoneTableBlock.Count > 0) {
            TreeNode tn2 = AppendNode(tn, NodeContent.NodeType.TYP_PolygoneTableBlock, typ, "PolygoneTableBlock");
            AppendNode(AppendNode(tn2, NodeContent.NodeType.TYP_PolygoneTable, typ, "PolygoneTable"));
         }
         AppendNode(AppendNode(tn, NodeContent.NodeType.TYP_PolygoneDraworderTable, typ, "PolygoneDraworderTable"));

         if (typ.NT_PointTableBlock.Count > 0)
            AppendNode(tn, NodeContent.NodeType.TYP_NT_PointDatabtable, typ, "NT PointDatabtable");
         if (typ.NT_PointDatablock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.TYP_NT_PointDatablock, typ, "NT PointDatablock");
         if (typ.NT_PointLabelblock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.TYP_NT_PointLabelblock, typ, "NT PointLabelblock");
         if (typ.NT_LabelblockTable1.Length > 0)
            AppendNode(tn, NodeContent.NodeType.TYP_NT_LabelblockTable1, typ, "NT LabelblockTable1");
         if (typ.NT_LabelblockTable2.Length > 0)
            AppendNode(tn, NodeContent.NodeType.TYP_NT_LabelblockTable2, typ, "NT LabelblockTable2");

      }

      /// <summary>
      /// Knoten für eine Section anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      /// <param name="nodetype"></param>
      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_TYP typ, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.TYP_PolygoneTable:
               for (int i = 0; i < typ.PolygoneTableBlock.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Polygone " + i.ToString());
               break;

            case NodeContent.NodeType.TYP_PolygoneDraworderTable:
               if (typ.PolygoneDraworderTableBlock.Length > 0)
                  for (int i = 0; i < typ.PolygonDraworderTableItems.Count; i++)
                     AppendNode(tn, NodeContent.NodeType.Index, i, "Item " + i.ToString());
               break;

            case NodeContent.NodeType.TYP_PolylineTable:
               for (int i = 0; i < typ.PolylineTableBlock.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Polyline " + i.ToString());
               break;

            case NodeContent.NodeType.TYP_PointTable:
               for (int i = 0; i < typ.PointTableBlock.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Point " + i.ToString());
               break;

         }
      }

      /// <summary>
      /// Knoten für einen Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_TYP typ) {
         //AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.North.Value, 0x15, "North: " + DecimalAndHexAndBinary(tre.North.Value) + " (" + tre.North.ValueDegree + "°)", true), "North");
         //AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x70, 0, tre.Unknown_x70.Length, NodeContent.Content4DataRange.DataType.Other, 0x70, "Unknown 0x70"), "Unknown 0x70");
         //if (tre.Headerlength > 0x74) {
         //   AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.MapID, 0x74, "MapID: " + DecimalAndHexAndBinary((ulong)tre.MapID), false), "MapID");
         //}
      }

      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_TYP typ) {
         info.AppendLine("Codepage                     (2 Byte): " + DecimalAndHexAndBinary(typ.Codepage));
         info.AppendLine("PointDatablock               (8 Byte): " + typ.PointDatablock.ToString());
         info.AppendLine("PolylineDatablock            (8 Byte): " + typ.PolylineDatablock.ToString());
         info.AppendLine("PolygoneDatablock            (8 Byte): " + typ.PolygoneDatablock.ToString());
         info.AppendLine("FamilyID                     (2 Byte): " + DecimalAndHexAndBinary(typ.FamilyID));
         info.AppendLine("ProductID                    (2 Byte): " + DecimalAndHexAndBinary(typ.ProductID));
         info.AppendLine("PointDatatable              (10 Byte): " + typ.PointTableBlock.ToString());
         info.AppendLine("PolylineDatatable           (10 Byte): " + typ.PolylineTableBlock.ToString());
         info.AppendLine("PolygoneDatatable           (10 Byte): " + typ.PolygoneTableBlock.ToString());
         info.AppendLine("PolygoneDraworderTableBlock (10 Byte): " + typ.PolygoneDraworderTableBlock.ToString());
         if (typ.Headerlength > 0x5B) { // NT
            info.AppendLine();
            info.AppendLine("NT-Header");
            info.AppendLine("NT_PointDatabtable          (10 Byte): " + typ.NT_PointTableBlock.ToString());
            info.AppendLine("nt_unknown_0x65              (1 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x65));
            info.AppendLine("NT_PointDatablock            (8 Byte): " + typ.NT_PointDatablock.ToString());
            info.AppendLine("nt_unknown_0x6E              (4 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x6E));
            if (typ.Headerlength > 0x6E) {
               info.AppendLine("NT_PointLabelblock           (8 Byte): " + typ.NT_PointLabelblock.ToString());
               info.AppendLine("nt_unknown_0x7A              (4 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x7A));
               info.AppendLine("nt_unknown_0x7E              (4 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x7E));
               info.AppendLine("NT_LabelblockTable1          (8 Byte): " + typ.NT_LabelblockTable1.ToString());
               info.AppendLine("nt_unknown_0x8A              (4 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x8A));
               info.AppendLine("nt_unknown_0x8E              (4 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x8E));
               info.AppendLine("NT_LabelblockTable2          (8 Byte): " + typ.NT_LabelblockTable2.ToString());
               info.AppendLine("nt_unknown_0x9A              (2 Byte): " + DecimalAndHexAndBinary(typ.nt_unknown_0x9A));
               if (typ.Headerlength > 0x9C)
                  info.AppendLine("nt_unknown_0x9C              (" + typ.nt_unknown_0x9C.Length.ToString() + " Byte): " + HexString(typ.nt_unknown_0x9C));
               if (typ.Headerlength > 0xA4)
                  info.AppendLine("nt_unknown_0xA4              (" + typ.nt_unknown_0xA4.Length.ToString() + " Byte): " + HexString(typ.nt_unknown_0xA4));
               if (typ.Headerlength > 0xAE)
                  info.AppendLine("nt_unknown_0xAE              (" + typ.nt_unknown_0xAE.Length.ToString() + " Byte): " + HexString(typ.nt_unknown_0xAE));
            }
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
         GarminCore.Files.StdFile_TYP typ = filedata.GetGarminFileAsTYP();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;

         switch (nodetype) {
            case NodeContent.NodeType.TYP_PolygoneTableBlock:
               firsthexadr = 0x15 + 2 + 3 * 8 + 2 * 2 + 2 * 10; // Daten aus dem Header
               hexlen = 20;
               if (idx < 0) {
                  info.AppendLine("PolygoneTableBlock");
                  info.AppendLine("Offset:              (4 Byte):  " + DecimalAndHexAndBinary(typ.PolygoneTableBlock.Offset));
                  info.AppendLine("Recordsize:          (2 Byte):  " + DecimalAndHexAndBinary(typ.PolygoneTableBlock.Recordsize));
                  info.AppendLine("Length:              (4 Byte):  " + DecimalAndHexAndBinary(typ.PolygoneTableBlock.Length));
               }
               break;

            case NodeContent.NodeType.TYP_PolygoneTable:
               firsthexadr = typ.PolygoneTableBlock.Offset;
               hexlen = (int)typ.PolygoneTableBlock.Length;
               if (idx < 0) {
                  info.AppendLine("PolygoneTableBlock: " + typ.PolygoneTableBlock.ToString());
               } else {
                  GarminCore.Files.Typ.Polygone poly = typ.GetPolygone(idx);

                  GarminCore.Files.Typ.TableItem ti = typ.PolygonTableItems[idx];
                  info.AppendLine("TableItem " + idx.ToString() + " at position " + DecimalAndHexAndBinary(typ.PolygoneTableBlock.Offset + idx * typ.PolygoneTableBlock.Recordsize));
                  info.AppendLine("rawtype       (2 Byte):              " + DecimalAndHexAndBinary(ti.rawtype, 16));
                  info.AppendLine("   Subtype            (Bit 0..4):    " + DecimalAndHexAndBinary(ti.Subtype, 5));
                  info.AppendLine("   Type               (Bit 5..15):   " + DecimalAndHexAndBinary(ti.Type, 11));
                  info.AppendLine("Offset        (" + (typ.PolygoneTableBlock.Recordsize - 2).ToString() + " Byte):              " + DecimalAndHexAndBinary(ti.Offset));
                  info.AppendLine();

                  firsthexadr = typ.PolygoneDatablock.Offset + ti.Offset;
                  hexlen = idx < typ.PolygonCount - 1 ? typ.PolygonTableItems[idx + 1].Offset - ti.Offset :
                                                        (int)typ.PolygoneTableBlock.Offset - (ti.Offset + (int)typ.PolygoneDatablock.Offset);

                  info.AppendLine("Polygon " + idx.ToString() + " at position " + DecimalAndHexAndBinary(firsthexadr));
                  info.AppendLine("Options          (1 Byte):           " + DecimalAndHexAndBinary(poly.Options));
                  info.AppendLine("   Colortype             (Bit 0..4): " + poly.Colortype.ToString() + " / " + DecimalAndHexAndBinary((byte)poly.Colortype));
                  // Farbdef. des Polygontyps
                  switch (poly.Colortype) {
                     case GarminCore.Files.Typ.Polygone.ColorType.Day1:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.Day1_Night1:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("NightColor1      (3 Byte):           " + DecimalAndHex(poly.NightColor1));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.BM_Day2:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("DayColor2        (3 Byte):           " + DecimalAndHex(poly.DayColor2));
                        info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                        info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.BM_Day2_Night2:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("DayColor2        (3 Byte):           " + DecimalAndHex(poly.DayColor2));
                        info.AppendLine("NightColor1      (3 Byte):           " + DecimalAndHex(poly.NightColor1));
                        info.AppendLine("NightColor2      (3 Byte):           " + DecimalAndHex(poly.NightColor2));
                        info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                        info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                        info.AppendLine("XBitmapNight:");
                        info.AppendLine("   " + PixMapText(poly.XBitmapNight));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.BM_Day1_Night2:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("NightColor1      (3 Byte):           " + DecimalAndHex(poly.NightColor1));
                        info.AppendLine("NightColor2      (3 Byte):           " + DecimalAndHex(poly.NightColor2));
                        info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                        info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                        info.AppendLine("XBitmapNight:");
                        info.AppendLine("   " + PixMapText(poly.XBitmapNight));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.BM_Day2_Night1:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("DayColor2        (3 Byte):           " + DecimalAndHex(poly.DayColor2));
                        info.AppendLine("NightColor1      (3 Byte):           " + DecimalAndHex(poly.NightColor1));
                        info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                        info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                        info.AppendLine("XBitmapNight:");
                        info.AppendLine("   " + PixMapText(poly.XBitmapNight));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.BM_Day1:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                        info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                        break;

                     case GarminCore.Files.Typ.Polygone.ColorType.BM_Day1_Night1:
                        info.AppendLine("DayColor1        (3 Byte):           " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("NightColor1      (3 Byte):           " + DecimalAndHex(poly.NightColor1));
                        info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                        info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                        info.AppendLine("XBitmapNight:");
                        info.AppendLine("   " + PixMapText(poly.XBitmapNight));
                        break;
                  }

                  if (poly.WithString) {
                     GarminCore.Files.Typ.MultiText mt = poly.Text;
                     int len = mt.GetIdentificationLength() + mt.GetRealLength();
                     info.AppendLine("Text             (" + DecimalAndHexAndBinary(len) + " Byte):");
                     for (int i = 0; i < mt.Count; i++) {
                        GarminCore.Files.Typ.Text txt = mt.Get(i);
                        info.AppendLine("   " + txt.Language.ToString() + " / " + DecimalAndHexAndBinary((byte)txt.Language) + ": '" + txt.Txt + "'");
                     }
                  }

                  if (poly.WithExtendedOptions) {
                     info.AppendLine("ExtOptions       (1 Byte):           " + DecimalAndHexAndBinary(poly.ExtOptions));
                     info.AppendLine("   FontType              (Bit 0..2): " + poly.FontType.ToString() + " / " + DecimalAndHexAndBinary((byte)poly.FontType));
                     info.AppendLine("   FontColType           (Bit 3,4):  " + poly.FontColType.ToString() + " / " + DecimalAndHexAndBinary((byte)poly.FontColType));
                     switch (poly.FontColType) {
                        case GarminCore.Files.Typ.GraphicElement.FontColours.Day: // es folgt noch 1 Farbe
                           info.AppendLine("FontColor1       (3 Byte):           " + DecimalAndHex(poly.FontColor1));
                           break;
                        case GarminCore.Files.Typ.GraphicElement.FontColours.Night: // es folgt noch 1 Farbe
                           info.AppendLine("FontColor2       (3 Byte):           " + DecimalAndHex(poly.FontColor2));
                           break;
                        case GarminCore.Files.Typ.GraphicElement.FontColours.DayAndNight: // es folgen noch 2 Farben
                           info.AppendLine("FontColor1       (3 Byte):           " + DecimalAndHex(poly.FontColor1));
                           info.AppendLine("FontColor2       (3 Byte):           " + DecimalAndHex(poly.FontColor2));
                           break;
                     }
                  }
               }
               break;

            case NodeContent.NodeType.TYP_PolygoneDraworderTable:
               firsthexadr = typ.PolygoneDraworderTableBlock.Offset;
               hexlen = (int)typ.PolygoneDraworderTableBlock.Length;
               if (idx < 0) {
                  info.AppendLine("PolygoneDraworderTableBlock");
                  info.AppendLine("Offset:     (4 Byte):  " + DecimalAndHexAndBinary(typ.PolygoneDraworderTableBlock.Offset));
                  info.AppendLine("Recordsize: (2 Byte):  " + DecimalAndHexAndBinary(typ.PolygoneDraworderTableBlock.Recordsize));
                  info.AppendLine("Length:     (4 Byte):  " + DecimalAndHexAndBinary(typ.PolygoneDraworderTableBlock.Length));
               } else {
                  hexlen = typ.PolygoneDraworderTableBlock.Recordsize;
                  firsthexadr += idx * hexlen;

                  GarminCore.Files.Typ.PolygonDraworderTableItem doi = typ.PolygonDraworderTableItems[idx];
                  if (doi.Type > 0) {
                     info.AppendLine("Level:             " + doi.Level.ToString());
                     info.AppendLine("Type     (1 Byte): " + DecimalAndHexAndBinary(doi.Type));
                     filedata.BinaryReader.Seek(firsthexadr + 1);
                     byte[] st = filedata.BinaryReader.ReadBytes(typ.PolygoneDraworderTableBlock.Recordsize - 1);
                     uint styp = 0;
                     for (int i = 0; i < st.Length; i++) {
                        styp <<= 8;
                        styp += st[i];
                     }
                     info.AppendLine("Subtypes (" + st.Length.ToString() + " Byte): " + HexString(st) + " / 0b" + Binary(styp));
                     for (int j = 0; j < doi.Subtypes.Count; j++)
                        info.AppendLine("Subtype:           " + DecimalAndHexAndBinary(doi.Subtypes[j]));
                  }
               }
               break;

            case NodeContent.NodeType.TYP_PolylineTableBlock:
               firsthexadr = 0x15 + 2 + 3 * 8 + 2 * 2 + 1 * 10; // Daten aus dem Header
               hexlen = 10;
               if (idx < 0) {
                  info.AppendLine("PolylineTableBlock");
                  info.AppendLine("Offset:              (4 Byte):  " + DecimalAndHexAndBinary(typ.PolylineTableBlock.Offset));
                  info.AppendLine("Recordsize:          (2 Byte):  " + DecimalAndHexAndBinary(typ.PolylineTableBlock.Recordsize));
                  info.AppendLine("Length:              (4 Byte):  " + DecimalAndHexAndBinary(typ.PolylineTableBlock.Length));
               }
               break;

            case NodeContent.NodeType.TYP_PolylineTable:
               firsthexadr = typ.PolylineTableBlock.Offset;
               hexlen = (int)typ.PolylineTableBlock.Length;
               if (idx < 0) {
                  info.AppendLine("PolylineTableBlock: " + typ.PolylineTableBlock.ToString());
               } else {
                  GarminCore.Files.Typ.Polyline poly = typ.GetPolyline(idx);

                  GarminCore.Files.Typ.TableItem ti = typ.PolylineTableItems[idx];
                  info.AppendLine("TableItem " + idx.ToString() + " at position " + DecimalAndHexAndBinary(typ.PolylineTableBlock.Offset + idx * typ.PolylineTableBlock.Recordsize));
                  info.AppendLine("rawtype          (2 Byte):            " + DecimalAndHexAndBinary(ti.rawtype, 16));
                  info.AppendLine("   Subtype               (Bit 0..4):  " + DecimalAndHexAndBinary(ti.Subtype, 5));
                  info.AppendLine("   Type                  (Bit 5..15): " + DecimalAndHexAndBinary(ti.Type, 11));
                  info.AppendLine("Offset           (" + (typ.PolylineTableBlock.Recordsize - 2).ToString() + " Byte):            " + DecimalAndHexAndBinary(ti.Offset));
                  info.AppendLine();

                  firsthexadr = typ.PolylineDatablock.Offset + ti.Offset;
                  hexlen = idx < typ.PolylineCount - 1 ? typ.PolylineTableItems[idx + 1].Offset - ti.Offset :
                                                        (int)typ.PolylineTableBlock.Offset - (ti.Offset + (int)typ.PolylineDatablock.Offset);

                  info.AppendLine("Polyline " + idx.ToString() + " at position " + DecimalAndHexAndBinary(firsthexadr));
                  info.AppendLine("Options          (1 Byte):            " + DecimalAndHexAndBinary(poly.Options));
                  info.AppendLine("   Polylinetype          (Bit 0..2):  " + poly.Polylinetype.ToString() + " / " + DecimalAndHexAndBinary((byte)poly.Polylinetype));
                  info.AppendLine("   BitmapHeight          (Bit 3..7):  " + DecimalAndHexAndBinary(poly.BitmapHeight));
                  info.AppendLine("Options2         (1 Byte):            " + DecimalAndHexAndBinary(poly.Options2));
                  info.AppendLine("   WithString            (Bit 0):     " + poly.WithString.ToString());
                  info.AppendLine("   WithTextRotation      (Bit 1):     " + poly.WithTextRotation.ToString());
                  info.AppendLine("   WithExtendedOptions   (Bit 2):     " + poly.WithExtendedOptions.ToString());

                  switch (poly.Polylinetype) {
                     case GarminCore.Files.Typ.Polyline.PolylineType.Day2:
                        info.AppendLine("DayColor1        (3 Byte):            " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("DayColor2        (3 Byte):            " + DecimalAndHex(poly.DayColor2));
                        break;

                     case GarminCore.Files.Typ.Polyline.PolylineType.Day2_Night2:
                        info.AppendLine("DayColor1        (3 Byte):            " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("DayColor2        (3 Byte):            " + DecimalAndHex(poly.DayColor2));
                        info.AppendLine("NightColor1      (3 Byte):            " + DecimalAndHex(poly.NightColor1));
                        info.AppendLine("NightColor2      (3 Byte):            " + DecimalAndHex(poly.NightColor2));
                        break;

                     case GarminCore.Files.Typ.Polyline.PolylineType.Day1_Night2:
                        info.AppendLine("DayColor1        (3 Byte):            " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("NightColor1      (3 Byte):            " + DecimalAndHex(poly.NightColor1));
                        info.AppendLine("NightColor2      (3 Byte):            " + DecimalAndHex(poly.NightColor2));
                        break;

                     case GarminCore.Files.Typ.Polyline.PolylineType.NoBorder_Day2_Night1:
                        info.AppendLine("DayColor1        (3 Byte):            " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("DayColor2        (3 Byte):            " + DecimalAndHex(poly.DayColor2));
                        info.AppendLine("NightColor1      (3 Byte):            " + DecimalAndHex(poly.NightColor1));
                        break;

                     case GarminCore.Files.Typ.Polyline.PolylineType.NoBorder_Day1:
                        info.AppendLine("DayColor1        (3 Byte):            " + DecimalAndHex(poly.DayColor1));
                        break;

                     case GarminCore.Files.Typ.Polyline.PolylineType.NoBorder_Day1_Night1:
                        info.AppendLine("DayColor1        (3 Byte):            " + DecimalAndHex(poly.DayColor1));
                        info.AppendLine("NightColor1      (3 Byte):            " + DecimalAndHex(poly.NightColor1));
                        break;
                  }

                  if (poly.BitmapHeight == 0) {
                     switch (poly.Polylinetype) {
                        case GarminCore.Files.Typ.Polyline.PolylineType.Day2:
                        case GarminCore.Files.Typ.Polyline.PolylineType.Day2_Night2:
                        case GarminCore.Files.Typ.Polyline.PolylineType.Day1_Night2:
                           info.AppendLine("InnerWidth       (1 Byte):            " + DecimalAndHexAndBinary(poly.InnerWidth));
                           if (poly.InnerWidth > 0)
                              info.AppendLine("Width            (1 Byte):            " + DecimalAndHexAndBinary(2 * poly.BorderWidth + poly.InnerWidth));
                           break;

                        case GarminCore.Files.Typ.Polyline.PolylineType.NoBorder_Day2_Night1:
                        case GarminCore.Files.Typ.Polyline.PolylineType.NoBorder_Day1:
                        case GarminCore.Files.Typ.Polyline.PolylineType.NoBorder_Day1_Night1:
                           info.AppendLine("InnerWidth       (1 Byte):            " + DecimalAndHexAndBinary(poly.InnerWidth));
                           break;

                     }
                  } else {
                     info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poly.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                     info.AppendLine("   " + PixMapText(poly.XBitmapDay));
                  }

                  if (poly.WithString) {
                     GarminCore.Files.Typ.MultiText mt = poly.Text;
                     int len = mt.GetIdentificationLength() + mt.GetRealLength();
                     info.AppendLine("Text             (" + DecimalAndHexAndBinary(len) + " Byte):");
                     for (int i = 0; i < mt.Count; i++) {
                        GarminCore.Files.Typ.Text txt = mt.Get(i);
                        info.AppendLine("   " + txt.Language.ToString() + " / " + DecimalAndHexAndBinary((byte)txt.Language) + ": '" + txt.Txt + "'");
                     }
                  }

                  if (poly.WithExtendedOptions) {
                     info.AppendLine("ExtOptions       (1 Byte):            " + DecimalAndHexAndBinary(poly.ExtOptions));
                     info.AppendLine("   FontType              (Bit 0..2):  " + poly.FontType.ToString() + " / " + DecimalAndHexAndBinary((byte)poly.FontType));
                     info.AppendLine("   FontColType           (Bit 3,4):   " + poly.FontColType.ToString() + " / " + DecimalAndHexAndBinary((byte)poly.FontColType));
                     switch (poly.FontColType) {
                        case GarminCore.Files.Typ.GraphicElement.FontColours.Day: // es folgt noch 1 Farbe
                           info.AppendLine("FontColor1       (3 Byte):            " + DecimalAndHex(poly.FontColor1));
                           break;
                        case GarminCore.Files.Typ.GraphicElement.FontColours.Night: // es folgt noch 1 Farbe
                           info.AppendLine("FontColor2       (3 Byte):            " + DecimalAndHex(poly.FontColor2));
                           break;
                        case GarminCore.Files.Typ.GraphicElement.FontColours.DayAndNight: // es folgen noch 2 Farben
                           info.AppendLine("FontColor1       (3 Byte):            " + DecimalAndHex(poly.FontColor1));
                           info.AppendLine("FontColor2       (3 Byte):            " + DecimalAndHex(poly.FontColor2));
                           break;
                     }
                  }
               }
               break;

            case NodeContent.NodeType.TYP_PointTableBlock:
               firsthexadr = 0x15 + 2 + 3 * 8 + 2 * 2; // Daten aus dem Header
               hexlen = 10;
               if (idx < 0) {
                  info.AppendLine("PointTableBlock");
                  info.AppendLine("Offset:              (4 Byte):  " + DecimalAndHexAndBinary(typ.PointTableBlock.Offset));
                  info.AppendLine("Recordsize:          (2 Byte):  " + DecimalAndHexAndBinary(typ.PointTableBlock.Recordsize));
                  info.AppendLine("Length:              (4 Byte):  " + DecimalAndHexAndBinary(typ.PointTableBlock.Length));
               }
               break;

            case NodeContent.NodeType.TYP_PointTable:
               firsthexadr = typ.PointTableBlock.Offset;
               hexlen = (int)typ.PointTableBlock.Length;
               if (idx < 0) {
                  info.AppendLine("PointTableBlock: " + typ.PointTableBlock.ToString());
               } else {
                  GarminCore.Files.Typ.POI poi = typ.GetPoi(idx);

                  GarminCore.Files.Typ.TableItem ti = typ.PointTableItems[idx];
                  info.AppendLine("TableItem " + idx.ToString() + " at position " + DecimalAndHexAndBinary(typ.PointTableBlock.Offset + idx * typ.PointTableBlock.Recordsize));
                  info.AppendLine("rawtype          (2 Byte):            " + DecimalAndHexAndBinary(ti.rawtype, 16));
                  info.AppendLine("   Subtype               (Bit 0..4):  " + DecimalAndHexAndBinary(ti.Subtype, 5));
                  info.AppendLine("   Type                  (Bit 5..15): " + DecimalAndHexAndBinary(ti.Type, 11));
                  info.AppendLine("Offset           (" + (typ.PointTableBlock.Recordsize - 2).ToString() + " Byte):            " + DecimalAndHexAndBinary(ti.Offset));
                  info.AppendLine();

                  firsthexadr = typ.PointDatablock.Offset + ti.Offset;
                  hexlen = idx < typ.PoiCount - 1 ? typ.PointTableItems[idx + 1].Offset - ti.Offset :
                                                        (int)typ.PointTableBlock.Offset - (ti.Offset + (int)typ.PointDatablock.Offset);

                  info.AppendLine("Point " + idx.ToString() + " at position " + DecimalAndHexAndBinary(firsthexadr));
                  info.AppendLine("Options          (1 Byte):            " + DecimalAndHexAndBinary(poi.Options));
                  info.AppendLine("   NightXpmHasData       (Bit 0):     " + poi.NightXpmHasData.ToString());
                  info.AppendLine("   WithNightXpm          (Bit 1):     " + poi.WithNightXpm.ToString());
                  info.AppendLine("   WithString            (Bit 2):     " + poi.WithString.ToString());
                  info.AppendLine("   WithExtendedOptions   (Bit 3):     " + poi.WithExtendedOptions.ToString());
                  info.AppendLine("Width            (1 Byte):            " + DecimalAndHexAndBinary(poi.Width));
                  info.AppendLine("Height           (1 Byte):            " + DecimalAndHexAndBinary(poi.Height));
                  info.AppendLine("ColorsDay        (1 Byte):            " + DecimalAndHexAndBinary(poi.colsday));
                  info.AppendLine("ColormodeDay     (1 Byte):            " + poi.ColormodeDay.ToString() + " / " + DecimalAndHexAndBinary((byte)poi.ColormodeDay));
                  info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poi.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                  info.AppendLine("   " + PixMapText(poi.XBitmapDay));
                  if (poi.WithNightXpm) {
                     info.AppendLine("ColorsNight      (1 Byte):            " + DecimalAndHexAndBinary(poi.colsnight));
                     info.AppendLine("ColormodeDay     (1 Byte):            " + poi.ColormodeNight.ToString() + " / " + DecimalAndHexAndBinary((byte)poi.ColormodeNight));
                     info.AppendLine("BitmapDay        (" + DecimalAndHexAndBinary(poi.XBitmapDay.data.rawimgdata.Length) + " Byte):");
                     info.AppendLine("   " + PixMapText(poi.XBitmapDay));
                     if (poi.NightXpmHasData)
                        info.AppendLine("BitmapNight      (" + DecimalAndHexAndBinary(poi.XBitmapNight.data.rawimgdata.Length) + " Byte):");
                     else
                        info.AppendLine("BitmapNight:");
                     info.AppendLine("   " + PixMapText(poi.XBitmapNight));

                  }

                  if (poi.WithString) {
                     GarminCore.Files.Typ.MultiText mt = poi.Text;
                     int len = mt.GetIdentificationLength() + mt.GetRealLength();
                     info.AppendLine("Text             (" + DecimalAndHexAndBinary(len) + " Byte):");
                     for (int i = 0; i < mt.Count; i++) {
                        GarminCore.Files.Typ.Text txt = mt.Get(i);
                        info.AppendLine("   " + txt.Language.ToString() + " / " + DecimalAndHexAndBinary((byte)txt.Language) + ": '" + txt.Txt + "'");
                     }
                  }

                  if (poi.WithExtendedOptions) {
                     info.AppendLine("ExtOptions       (1 Byte):            " + DecimalAndHexAndBinary(poi.ExtOptions));
                     info.AppendLine("   FontType              (Bit 0..2):  " + poi.FontType.ToString() + " / " + DecimalAndHexAndBinary((byte)poi.FontType));
                     info.AppendLine("   FontColType           (Bit 3,4):   " + poi.FontColType.ToString() + " / " + DecimalAndHexAndBinary((byte)poi.FontColType));
                     switch (poi.FontColType) {
                        case GarminCore.Files.Typ.GraphicElement.FontColours.Day: // es folgt noch 1 Farbe
                           info.AppendLine("FontColor1       (3 Byte):            " + DecimalAndHex(poi.FontColor1));
                           break;
                        case GarminCore.Files.Typ.GraphicElement.FontColours.Night: // es folgt noch 1 Farbe
                           info.AppendLine("FontColor2       (3 Byte):            " + DecimalAndHex(poi.FontColor2));
                           break;
                        case GarminCore.Files.Typ.GraphicElement.FontColours.DayAndNight: // es folgen noch 2 Farben
                           info.AppendLine("FontColor1       (3 Byte):            " + DecimalAndHex(poi.FontColor1));
                           info.AppendLine("FontColor2       (3 Byte):            " + DecimalAndHex(poi.FontColor2));
                           break;
                     }
                  }
               }
               break;

            case NodeContent.NodeType.TYP_NT_PointDatabtable:
               firsthexadr = typ.NT_PointTableBlock.Offset;
               hexlen = (int)typ.NT_PointTableBlock.Length;
               info.AppendLine("NT PointTableBlock: " + typ.NT_PointTableBlock.ToString());
               break;

            case NodeContent.NodeType.TYP_NT_PointDatablock:
               firsthexadr = typ.NT_PointDatablock.Offset;
               hexlen = (int)typ.NT_PointDatablock.Length;
               info.AppendLine("NT PointDatablock: " + typ.NT_PointDatablock.ToString());
               break;

            case NodeContent.NodeType.TYP_NT_PointLabelblock:
               firsthexadr = typ.NT_PointLabelblock.Offset;
               hexlen = (int)typ.NT_PointLabelblock.Length;
               info.AppendLine("NT PointLabelblock: " + typ.NT_PointLabelblock.ToString());
               break;

            case NodeContent.NodeType.TYP_NT_LabelblockTable1:
               firsthexadr = typ.NT_LabelblockTable1.Offset;
               hexlen = (int)typ.NT_LabelblockTable1.Length;
               info.AppendLine("NT LabelblockTable1: " + typ.NT_LabelblockTable1.ToString());
               break;

            case NodeContent.NodeType.TYP_NT_LabelblockTable2:
               firsthexadr = typ.NT_LabelblockTable2.Offset;
               hexlen = (int)typ.NT_LabelblockTable2.Length;
               info.AppendLine("NT LabelblockTable2: " + typ.NT_LabelblockTable2.ToString());
               break;

            //case NodeContent.NodeType.TRE_CopyrightBlock:
            //   firsthexadr = tre.CopyrightBlock.Offset;
            //   if (idx < 0) {
            //      info.AppendLine("Copyrights: " + tre.CopyrightOffsetsList.Count.ToString());
            //      info.AppendLine("  Block:    " + tre.CopyrightBlock.ToString());
            //      hexlen = (int)tre.CopyrightBlock.Length;
            //   } else {
            //      info.AppendLine("Offset in LBL (" + tre.CopyrightBlock.Recordsize.ToString() + " Byte): " + DecimalAndHexAndBinary(tre.CopyrightOffsetsList[idx]));
            //      lbl = tvd.GetLBL(filedata.Basename);
            //      if (lbl != null)
            //         info.AppendLine("   Text (from LBL): '" + lbl.GetText(tre.CopyrightOffsetsList[idx], true) + "'");
            //      firsthexadr += idx * tre.CopyrightBlock.Recordsize;
            //      hexlen = tre.CopyrightBlock.Recordsize;
            //   }
            //   break;

            //case NodeContent.NodeType.TRE_UnknownBlock_xBC:
            //   if (idx < 0) {
            //      info.AppendLine("UnknownBlock 0xBC: " + tre.UnknownBlock_xBC.ToString());
            //      firsthexadr = tre.UnknownBlock_xAE.Offset;
            //      hexlen = (int)tre.UnknownBlock_xAE.Length;
            //   } else {
            //   }
            //   break;

            default:
               info.AppendLine("internal error: no info for nodetype '" + nodetype.ToString() + "'");
               break;
         }

         if (hexlen > 0)
            hex = HexRange(firsthexadr, filedata.BinaryReader, hexlen);
      }

      static string PixMapText(GarminCore.Files.Typ.PixMap pm) {
         StringBuilder sb = new StringBuilder();
         sb.AppendFormat("Size {0}x{1}, Bits/Pixel {2}, Colors {3}, Colormode {4}", pm.Width, pm.Height, pm.BpP, pm.Colors, pm.Colormode);
         return sb.ToString();
      }



   }
}
