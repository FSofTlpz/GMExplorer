using System;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataTRE : Data {

      /// <summary>
      /// Knoten für eine logische oder physische TRE-Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_TRE tre, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, tre, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, tre, "Garmin TRE-Header"));
         if (tre.MapDescriptionList.Count > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_DescriptionList, tre, "DescriptionBlock"));
         if (tre.MaplevelBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_MaplevelBlock, tre, "MaplevelList"));
         if (tre.SubdivisionBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_SubdivisionBlock, tre, "SubdivisionBlock"));
         if (tre.CopyrightBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_CopyrightBlock, tre, "CopyrightBlock"));
         if (tre.LineOverviewBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_LineOverviewBlock, tre, "LineOverviewBlock"));
         if (tre.AreaOverviewBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_AreaOverviewBlock, tre, "AreaOverviewBlock"));
         if (tre.PointOverviewBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_PointOverviewBlock, tre, "PointOverviewBlock"));
         if (tre.ExtTypeOffsetsBlock != null && tre.ExtTypeOffsetsBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_ExtTypeOffsetsBlock, tre, "ExtTypeOffsetsBlock"));
         if (tre.ExtTypeOverviewsBlock != null && tre.ExtTypeOverviewsBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_ExtTypeOverviewsBlock, tre, "ExtTypeOverviewsBlock"));
         if (tre.UnknownBlock_xAE != null && tre.UnknownBlock_xAE.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_UnknownBlock_xAE, tre, "UnknownBlock_xAE"));
         if (tre.UnknownBlock_xBC != null && tre.UnknownBlock_xBC.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_UnknownBlock_xBC, tre, "UnknownBlock_xBC"));
         if (tre.UnknownBlock_xE3 != null && tre.UnknownBlock_xE3.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.TRE_UnknownBlock_xE3, tre, "UnknownBlock_xE3"));
      }

      /// <summary>
      /// Knoten für eine TRE-Section anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      /// <param name="nodetype"></param>
      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_TRE tre, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.TRE_DescriptionList:
               for (int i = 0; i < tre.MapDescriptionList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "MapDescription " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_MaplevelBlock:
               for (int i = 0; i < tre.MaplevelList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Maplevel " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_SubdivisionBlock:
               for (int i = 0; i < tre.SubdivInfoList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Info for Subdiv " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_CopyrightBlock:
               for (int i = 0; i < tre.CopyrightOffsetsList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Copyright " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_LineOverviewBlock:
               for (int i = 0; i < tre.LineOverviewList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "LineOverview " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_AreaOverviewBlock:
               for (int i = 0; i < tre.AreaOverviewList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "AreaOverview " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_PointOverviewBlock:
               for (int i = 0; i < tre.PointOverviewList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "PointOverview " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_ExtTypeOffsetsBlock:
               for (int i = 0; i < tre.ExtTypeOffsetsBlock.Length / tre.ExtTypeOffsetsBlock.Recordsize; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "ExtTypeOffset for Subdiv " + i.ToString());
               break;

            case NodeContent.NodeType.TRE_ExtTypeOverviewsBlock:
               for (int i = 0; i < tre.ExtTypeOverviewsBlock.Length / tre.ExtTypeOverviewsBlock.Recordsize; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "ExtTypeOverview for Subdiv " + i.ToString());
               break;

               //case NodeContent.NodeType.TRE_UnknownBlock_xAE: break;
               //case NodeContent.NodeType.TRE_UnknownBlock_xBC: break;
               //case NodeContent.NodeType.TRE_UnknownBlock_xE3: break;

         }
      }

      /// <summary>
      /// Knoten für einen TRE-Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_TRE tre) {
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.North.Value, 0x15, "North: " + DecimalAndHexAndBinary(tre.North.Value) + " (" + tre.North.ValueDegree + "°)", true), "North");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.East.Value, 0x18, "East: " + DecimalAndHexAndBinary(tre.East.Value) + " (" + tre.East.ValueDegree + "°)", true), "East");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.East.Value, 0x1B, "South: " + DecimalAndHexAndBinary(tre.South.Value) + " (" + tre.South.ValueDegree + "°)", true), "South");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.East.Value, 0x1E, "West: " + DecimalAndHexAndBinary(tre.West.Value) + " (" + tre.West.ValueDegree + "°)", true), "West");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.MaplevelBlock, 0x21, "MaplevelBlock: " + tre.MaplevelBlock.ToString()), "MaplevelBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.SubdivisionBlock, 0x29, "SubdivisionBlock: " + tre.SubdivisionBlock.ToString()), "SubdivisionBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.CopyrightBlock, 0x31, "CopyrightBlock: " + tre.CopyrightBlock.ToString()), "CopyrightBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x3B, 0, tre.Unknown_x3B.Length, NodeContent.Content4DataRange.DataType.Other, 0x3b, "Unknown 0x3B"), "Unknown 0x3B");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.POIDisplayFlags, 0x3f,
                                                                                          "POIDisplayFlags: " + DecimalAndHexAndBinary((ulong)tre.POIDisplayFlags, 8) + Environment.NewLine +
                                                                                          "  TransparentMap (1): " + tre.POIDisplay_TransparentMap.ToString() + Environment.NewLine +
                                                                                          "  ShowStreetBeforeNumber (2): " + tre.POIDisplay_ShowStreetBeforeNumber.ToString() + Environment.NewLine +
                                                                                          "  ShowZipBeforeCity (3): " + tre.POIDisplay_ShowZipBeforeCity.ToString() + Environment.NewLine +
                                                                                          "  DriveLeft (5): " + tre.POIDisplay_DriveLeft.ToString()),
                    "POIDisplayFlags");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.DisplayPriority, 0x40, "DisplayPriority: " + DecimalAndHexAndBinary(tre.DisplayPriority), true), "DisplayPriority");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x43, 0, tre.Unknown_x43.Length, NodeContent.Content4DataRange.DataType.Other, 0x43, "Unknown 0x43"), "Unknown 0x43");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.LineOverviewBlock, 0x4a, "LineOverviewBlock: " + tre.LineOverviewBlock.ToString()), "LineOverviewBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x54, 0, tre.Unknown_x54.Length, NodeContent.Content4DataRange.DataType.Other, 0x54, "Unknown 0x54"), "Unknown 0x54");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.AreaOverviewBlock, 0x58, "AreaOverviewBlock: " + tre.AreaOverviewBlock.ToString()), "AreaOverviewBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x62, 0, tre.Unknown_x62.Length, NodeContent.Content4DataRange.DataType.Other, 0x62, "Unknown 0x62"), "Unknown 0x62");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.PointOverviewBlock, 0x66, "PointOverviewBlock: " + tre.PointOverviewBlock.ToString()), "PointOverviewBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x70, 0, tre.Unknown_x70.Length, NodeContent.Content4DataRange.DataType.Other, 0x70, "Unknown 0x70"), "Unknown 0x70");
         if (tre.Headerlength > 0x74) {
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.MapID, 0x74, "MapID: " + DecimalAndHexAndBinary((ulong)tre.MapID), false), "MapID");
            if (tre.Headerlength > 0x78) {
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x78, 0, tre.Unknown_x78.Length, NodeContent.Content4DataRange.DataType.Other, 0x78, "Unknown 0x78"), "Unknown 0x78");
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.ExtTypeOffsetsBlock, 0x7c, "ExtTypeOffsetsBlock: " + tre.ExtTypeOffsetsBlock.ToString()), "ExtTypeOffsetsBlock");
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_x86, 0, tre.Unknown_x86.Length, NodeContent.Content4DataRange.DataType.Other, 0x86, "Unknown 0x86"), "Unknown 0x86");
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.ExtTypeOverviewsBlock, 0x8a, "ExtTypeOverviewsBlock: " + tre.ExtTypeOverviewsBlock.ToString()), "ExtTypeOverviewsBlock");
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.ExtLineCount, 0x94, "ExtLineCount: " + DecimalAndHexAndBinary(tre.ExtLineCount)), "ExtLineCount");
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.ExtAreaCount, 0x96, "ExtAreaCount: " + DecimalAndHexAndBinary(tre.ExtAreaCount)), "ExtAreaCount");
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.ExtPointCount, 0x98, "ExtPointCount: " + DecimalAndHexAndBinary(tre.ExtPointCount)), "ExtPointCount");
               if (tre.Headerlength > 0x9a) {
                  AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.MapValues, 0x9a, ""), "MapValues");
                  if (tre.Headerlength > 0xAA)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.MaplevelScrambleKey, 0xaa, "MaplevelScrambleKey", false), "MaplevelScrambleKey");
                  if (tre.Headerlength > 0xAE)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.UnknownBlock_xAE, 0xae, "UnknownBlock_xAE: " + tre.UnknownBlock_xAE.ToString()), "Unknown Block 0xAE");
                  if (tre.Headerlength > 0xB6)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_xB6, 0, tre.Unknown_xB6.Length, NodeContent.Content4DataRange.DataType.Other, 0xB6, "Unknown 0xB6"), "Unknown 0xB6");
                  if (tre.Headerlength > 0xBC)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.UnknownBlock_xBC, 0xBC, "UnknownBlock_xBC: " + tre.UnknownBlock_xBC.ToString()), "Unknown Block 0xBC");
                  if (tre.Headerlength > 0xC4)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_xC4, 0, tre.Unknown_xC4.Length, NodeContent.Content4DataRange.DataType.Other, 0xC4, "Unknown 0xC4"), "Unknown 0xC4");
                  if (tre.Headerlength > 0xE3)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.UnknownBlock_xE3, 0xe3, "UnknownBlock_xE3: " + tre.UnknownBlock_xE3.ToString()), "Unknown Block 0xE3");
                  if (tre.Headerlength > 0xEB)
                     AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(tre.Unknown_xEB, 0, tre.Unknown_xEB.Length, NodeContent.Content4DataRange.DataType.Other, 0xEB, "Unknown 0xEB"), "Unknown 0xEB");
               }
            }
         }
      }

      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_TRE tre) {
         info.AppendLine("North:                 " + DecimalAndHexAndBinary(tre.North.Value) + " (" + tre.North.ValueDegree + "°)");
         info.AppendLine("East:                  " + DecimalAndHexAndBinary(tre.East.Value) + " (" + tre.East.ValueDegree + "°)");
         info.AppendLine("South:                 " + DecimalAndHexAndBinary(tre.South.Value) + " (" + tre.South.ValueDegree + "°)");
         info.AppendLine("West:                  " + DecimalAndHexAndBinary(tre.West.Value) + " (" + tre.West.ValueDegree + "°)");
         info.AppendLine("Maplevel:              " + tre.MaplevelBlock.ToString());
         info.AppendLine("Subdivision:           " + tre.SubdivisionBlock.ToString());
         info.AppendLine("Copyrights:            " + tre.CopyrightBlock.ToString());
         info.AppendLine("Unknown 0x3B:          " + HexString(tre.Unknown_x3B));
         info.AppendLine("POIDisplayFlags:       " + DecimalAndHexAndBinary(tre.POIDisplayFlags, 8));
         info.AppendLine("DisplayPriority:       " + DecimalAndHexAndBinary(tre.DisplayPriority));
         info.AppendLine("Unknown 0x43:          " + HexString(tre.Unknown_x43));
         info.AppendLine("LineOverviewBlock:     " + tre.LineOverviewBlock.ToString());
         info.AppendLine("Unknown 0x54:          " + HexString(tre.Unknown_x54));
         info.AppendLine("AreaOverviewBlock:     " + tre.AreaOverviewBlock.ToString());
         info.AppendLine("Unknown 0x62:          " + HexString(tre.Unknown_x62));
         info.AppendLine("PointOverviewBlock:    " + tre.PointOverviewBlock.ToString());
         info.AppendLine("Unknown 0x70:          " + HexString(tre.Unknown_x70));
         if (tre.Headerlength > 0x74)
            info.AppendLine("MapID:                 " + DecimalAndHexAndBinary(tre.MapID));
         if (tre.Headerlength > 0x78)
            info.AppendLine("Unknown 0x78:          " + HexString(tre.Unknown_x78));
         if (tre.Headerlength > 0x7C)
            info.AppendLine("ExtTypeOffsetsBlock:   " + tre.ExtTypeOffsetsBlock.ToString());
         if (tre.Headerlength > 0x86)
            info.AppendLine("Unknown 0x86:          " + HexString(tre.Unknown_x86));
         if (tre.Headerlength > 0x8A)
            info.AppendLine("ExtTypeOverviewsBlock: " + tre.ExtTypeOverviewsBlock.ToString());
         if (tre.Headerlength > 0x94)
            info.AppendLine("ExtLineCount:          " + DecimalAndHexAndBinary(tre.ExtLineCount));
         if (tre.Headerlength > 0x96)
            info.AppendLine("ExtAreaCount:          " + DecimalAndHexAndBinary(tre.ExtAreaCount));
         if (tre.Headerlength > 0x98)
            info.AppendLine("ExtPointCount:         " + DecimalAndHexAndBinary(tre.ExtPointCount));
         if (tre.Headerlength > 0x9a) {
            for (int i = 0; i < tre.MapValues.Length; i++)
               info.AppendLine((i == 0 ? "MapValues:             " : "                       ") + DecimalAndHexAndBinary(tre.MapValues[i]));
            info.AppendLine("MaplevelScrambleKey:   " + DecimalAndHexAndBinary(tre.MaplevelScrambleKey));
            if (tre.Headerlength > 0xAE)
               info.AppendLine("UnknownBlock 0xAE:     " + tre.UnknownBlock_xAE.ToString());
            if (tre.Headerlength > 0xB6)
               info.AppendLine("Unknown 0xB6:          " + HexString(tre.Unknown_xB6));
            if (tre.Headerlength > 0xBC)
               info.AppendLine("UnknownBlock 0xBC:     " + tre.UnknownBlock_xBC.ToString());
            if (tre.Headerlength > 0xC4)
               info.AppendLine("Unknown 0xC4:          " + HexString(tre.Unknown_xC4));
            if (tre.Headerlength > 0xE3)
               info.AppendLine("UnknownBlock 0xE3:     " + tre.UnknownBlock_xE3.ToString());
            if (tre.Headerlength > 0xEB)
               info.AppendLine("Unknown 0xEB:          " + HexString(tre.Unknown_xEB));
         }

      }

      /// <summary>
      /// Funktion für alle TRE-Datei-Infos
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="nodetype">"Thema" der Info</param>
      /// <param name="idx">wenn größer oder gleich 0, dann der Index auf ein Objekt einer Tabelle</param>
      /// <param name="tn"></param>
      public static void SectionAndIndex(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, NodeContent.NodeType nodetype, int idx, TreeViewData tvd) {
         GarminCore.Files.StdFile_TRE tre = filedata.GetGarminFileAsTRE();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;
         GarminCore.Files.StdFile_LBL lbl;
         GarminCore.DataBlock block;

         switch (nodetype) {
            case NodeContent.NodeType.TRE_DescriptionList:
               firsthexadr = tre.MapDescriptionBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("MapDescriptionList: " + tre.MapDescriptionList.Count.ToString());
                  info.AppendLine("  Block:            " + tre.MapDescriptionBlock.ToString());
                  hexlen = (int)tre.MapDescriptionBlock.Length;
               } else {
                  string mapdescription = tre.MapDescriptionList[idx];
                  info.AppendLine("MapDescription: " + tre.MapDescriptionList[idx]);
                  for (int i = 0; i < idx; i++)
                     firsthexadr += tre.MapDescriptionList[i].Length + 1;
                  hexlen = tre.MapDescriptionList[idx].Length + 1;
               }
               break;

            case NodeContent.NodeType.TRE_MaplevelBlock:
               firsthexadr = tre.MaplevelBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("Maplevel: " + tre.MaplevelList.Count.ToString());
                  info.AppendLine("  Block:  " + tre.MaplevelBlock.ToString());
                  hexlen = (int)tre.MaplevelBlock.Length;
                  if (hexlen > 0 && tre.MaplevelList.Count == 0)
                     info.AppendLine("data propably encrypted");
               } else {
                  GarminCore.Files.StdFile_TRE.MapLevel maplevel = tre.MaplevelList[idx];
                  info.AppendLine("SymbolicScaleDenominator (Bit 0..3):         " + DecimalAndHexAndBinary((ulong)maplevel.SymbolicScaleDenominator));
                  info.AppendLine("(SymbolicScaleDenominator) Bit 4:            " + maplevel.Bit4.ToString());
                  info.AppendLine("(SymbolicScaleDenominator) Bit 5:            " + maplevel.Bit5.ToString());
                  info.AppendLine("(SymbolicScaleDenominator) Bit 6:            " + maplevel.Bit6.ToString());
                  info.AppendLine("(SymbolicScaleDenominator) Bit 7, Inherited: " + maplevel.Inherited.ToString());
                  info.AppendLine("CoordBits (1 Byte):                          " + DecimalAndHexAndBinary((ulong)maplevel.CoordBits));
                  info.AppendLine("SubdivInfos (2 Byte):                        " + DecimalAndHexAndBinary(maplevel.SubdivInfos));
                  firsthexadr += idx * 4;
                  hexlen = 4;
               }
               break;

            case NodeContent.NodeType.TRE_SubdivisionBlock:
               firsthexadr = tre.SubdivisionBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("Subdivision: " + tre.SubdivInfoList.Count.ToString());
                  info.AppendLine("  Block:     " + tre.SubdivisionBlock.ToString());
                  hexlen = (int)tre.SubdivisionBlock.Length;
                  if (hexlen > 0 && tre.MaplevelList.Count == 0)
                     info.AppendLine("data propably encrypted");
               } else {
                  block = SampleInfo4SubdivInfo(info, tre, idx);
                  firsthexadr += block.Offset;
                  hexlen = (int)block.Length;
               }
               break;

            case NodeContent.NodeType.TRE_CopyrightBlock:
               firsthexadr = tre.CopyrightBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("Copyrights: " + tre.CopyrightOffsetsList.Count.ToString());
                  info.AppendLine("  Block:    " + tre.CopyrightBlock.ToString());
                  hexlen = (int)tre.CopyrightBlock.Length;
               } else {
                  info.AppendLine("Offset in LBL (" + tre.CopyrightBlock.Recordsize.ToString() + " Byte): " + DecimalAndHexAndBinary(tre.CopyrightOffsetsList[idx]));
                  lbl = tvd.GetLBL(filedata.Basename);
                  if (lbl != null)
                     info.AppendLine("   Text (from LBL): '" + lbl.GetText(tre.CopyrightOffsetsList[idx], true) + "'");
                  firsthexadr += idx * tre.CopyrightBlock.Recordsize;
                  hexlen = tre.CopyrightBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.TRE_LineOverviewBlock:
               firsthexadr = tre.LineOverviewBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("LineOverview: " + (tre.LineOverviewBlock.Length / tre.LineOverviewBlock.Recordsize).ToString());
                  info.AppendLine("  Block:      " + tre.LineOverviewBlock.ToString());
                  hexlen = (int)tre.LineOverviewBlock.Length;
               } else {
                  GarminCore.Files.StdFile_TRE.OverviewObject2Byte ov = tre.LineOverviewList[idx];
                  info.AppendLine("Type     (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.Type));
                  info.AppendLine("MaxLevel (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.MaxLevel));
                  if (ov is GarminCore.Files.StdFile_TRE.OverviewObject3Byte ||
                      ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                     info.AppendLine("SubType  (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject3Byte).SubType));
                  if (ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                     info.AppendLine("Unknown  (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject4Byte).Unknown));

                  firsthexadr += idx * tre.LineOverviewBlock.Recordsize;
                  hexlen = tre.LineOverviewBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.TRE_AreaOverviewBlock:
               firsthexadr = tre.AreaOverviewBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("AreaOverview: " + (tre.AreaOverviewBlock.Length / tre.AreaOverviewBlock.Recordsize).ToString());
                  info.AppendLine("  Block:      " + tre.AreaOverviewBlock.ToString());
                  hexlen = (int)tre.AreaOverviewBlock.Length;
               } else {
                  GarminCore.Files.StdFile_TRE.OverviewObject2Byte ov = tre.AreaOverviewList[idx];
                  info.AppendLine("Type     (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.Type));
                  info.AppendLine("MaxLevel (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.MaxLevel));
                  if (ov is GarminCore.Files.StdFile_TRE.OverviewObject3Byte ||
                      ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                     info.AppendLine("SubType (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject3Byte).SubType));
                  if (ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                     info.AppendLine("Unknown (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject4Byte).Unknown));

                  firsthexadr = idx * tre.AreaOverviewBlock.Recordsize;
                  hexlen = tre.AreaOverviewBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.TRE_PointOverviewBlock:
               firsthexadr = tre.PointOverviewBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("PointOverview: " + (tre.PointOverviewBlock.Length / tre.PointOverviewBlock.Recordsize).ToString());
                  info.AppendLine("  Block:       " + tre.PointOverviewBlock.ToString());
                  hexlen = (int)tre.PointOverviewBlock.Length;
               } else {
                  GarminCore.Files.StdFile_TRE.OverviewObject2Byte ov = tre.PointOverviewList[idx];
                  info.AppendLine("Type     (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.Type));
                  info.AppendLine("MaxLevel (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.MaxLevel));
                  if (ov is GarminCore.Files.StdFile_TRE.OverviewObject3Byte ||
                      ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                     info.AppendLine("SubType (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject3Byte).SubType));
                  if (ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                     info.AppendLine("Unknown (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject4Byte).Unknown));

                  firsthexadr = idx * tre.PointOverviewBlock.Recordsize;
                  hexlen = tre.PointOverviewBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.TRE_ExtTypeOffsetsBlock:
               firsthexadr = tre.ExtTypeOffsetsBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExtTypeOffsets: " + (tre.ExtTypeOffsetsBlock.Length / tre.ExtTypeOffsetsBlock.Recordsize).ToString());
                  info.AppendLine("  Block:        " + tre.ExtTypeOffsetsBlock.ToString());
                  hexlen = (int)tre.ExtTypeOffsetsBlock.Length;
               } else {
                  firsthexadr += idx * tre.ExtTypeOffsetsBlock.Recordsize;
                  hexlen = tre.ExtTypeOffsetsBlock.Recordsize;

                  GarminCore.Files.StdFile_TRE.ExtendedTypeOffsets extoffs = new GarminCore.Files.StdFile_TRE.ExtendedTypeOffsets();
                  filedata.BinaryReader.Seek(firsthexadr);
                  extoffs.Read(filedata.BinaryReader, tre.ExtTypeOffsetsBlock.Recordsize);
                  info.AppendLine("Offsets in RGN for each Subdiv");
                  info.AppendLine("AreasOffset  (4 Byte): " + DecimalAndHexAndBinary((ulong)extoffs.AreasOffset));
                  info.AppendLine("LinesOffset  (4 Byte): " + DecimalAndHexAndBinary((ulong)extoffs.LinesOffset));
                  info.AppendLine("PointsOffset (4 Byte): " + DecimalAndHexAndBinary((ulong)extoffs.PointsOffset));
                  if (extoffs.DataLength > 12)
                     info.AppendLine("Kinds (1 Byte): " + DecimalAndHexAndBinary((ulong)extoffs.Kinds));
               }
               break;

            case NodeContent.NodeType.TRE_ExtTypeOverviewsBlock:
               firsthexadr = tre.ExtTypeOverviewsBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("TRE_ExtTypeOverviews: " + (tre.ExtTypeOverviewsBlock.Length / tre.ExtTypeOverviewsBlock.Recordsize).ToString());
                  info.AppendLine("  Block:              " + tre.ExtTypeOverviewsBlock.ToString());
                  info.AppendLine("ExtLineOverviewList:  " + tre.ExtLineOverviewList.Count.ToString());
                  info.AppendLine("ExtAreaOverviewList:  " + tre.ExtAreaOverviewList.Count.ToString());
                  info.AppendLine("ExtPointOverviewList: " + tre.ExtPointOverviewList.Count.ToString());
                  hexlen = (int)tre.ExtTypeOverviewsBlock.Length;
               } else {
                  firsthexadr += idx * tre.ExtTypeOverviewsBlock.Recordsize;
                  hexlen = tre.ExtTypeOverviewsBlock.Recordsize;

                  GarminCore.Files.StdFile_TRE.OverviewObject2Byte ov = null;
                  if (idx < tre.ExtLineCount && idx < tre.ExtLineOverviewList.Count) {
                     info.AppendLine("ExtLine");
                     ov = tre.ExtLineOverviewList[idx];
                  } else if (idx < tre.ExtLineCount + tre.ExtAreaCount && idx - tre.ExtLineOverviewList.Count < tre.ExtAreaOverviewList.Count) {
                     idx -= tre.ExtLineCount;
                     info.AppendLine("ExtArea");
                     ov = tre.ExtAreaOverviewList[idx];
                  } else if (idx - tre.ExtLineOverviewList.Count - tre.ExtAreaOverviewList.Count < tre.ExtPointOverviewList.Count) {
                     idx -= tre.ExtLineCount + tre.ExtAreaCount;
                     info.AppendLine("ExtPoint");
                     ov = tre.ExtPointOverviewList[idx];
                  }
                  if (ov != null) {
                     info.AppendLine("Type     (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.Type));
                     info.AppendLine("MaxLevel (1 Byte): " + DecimalAndHexAndBinary((ulong)ov.MaxLevel));
                     info.AppendLine("SubType  (1 Byte): " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject3Byte).SubType));
                     if (ov is GarminCore.Files.StdFile_TRE.OverviewObject4Byte)
                        info.AppendLine("Unknown (1 Byte):  " + DecimalAndHexAndBinary((ulong)(ov as GarminCore.Files.StdFile_TRE.OverviewObject4Byte).Unknown));
                  }
               }
               break;

            case NodeContent.NodeType.TRE_UnknownBlock_xAE:
               if (idx < 0) {
                  info.AppendLine("UnknownBlock 0xAE: " + tre.UnknownBlock_xAE.ToString());
                  firsthexadr = tre.UnknownBlock_xAE.Offset;
                  hexlen = (int)tre.UnknownBlock_xAE.Length;
               } else {
               }
               break;

            case NodeContent.NodeType.TRE_UnknownBlock_xBC:
               if (idx < 0) {
                  info.AppendLine("UnknownBlock 0xBC: " + tre.UnknownBlock_xBC.ToString());
                  firsthexadr = tre.UnknownBlock_xAE.Offset;
                  hexlen = (int)tre.UnknownBlock_xAE.Length;
               } else {
               }
               break;

            case NodeContent.NodeType.TRE_UnknownBlock_xE3:
               if (idx < 0) {
                  info.AppendLine("UnknownBlock 0xE3: " + tre.UnknownBlock_xE3.ToString());
                  firsthexadr = tre.UnknownBlock_xAE.Offset;
                  hexlen = (int)tre.UnknownBlock_xAE.Length;
               } else {
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
      /// schreibt Infos zur Subdiv aus der TRE-Datei in den StringBuilder und liefert den Datenblock (bzgl. des SubdivisionBlocks)
      /// </summary>
      /// <param name="info"></param>
      /// <param name="tre"></param>
      /// <param name="subdividx"></param>
      /// <returns></returns>
      public static GarminCore.DataBlock SampleInfo4SubdivInfo(StringBuilder info, GarminCore.Files.StdFile_TRE tre, int subdividx) {
         GarminCore.Files.StdFile_TRE.SubdivInfoBasic subdiv = tre.SubdivInfoList[subdividx];
         if (!(subdiv is GarminCore.Files.StdFile_TRE.SubdivInfo))
            info.AppendLine("SubdivInfoBasic");

         int count = 0;
         for (int i = 0; i < tre.MaplevelList.Count; i++) {
            count += tre.MaplevelList[i].SubdivInfos;
            if (subdividx < count) {
               info.AppendLine("SubdivInfo for maplevel:               " + i.ToString());
               break;
            }
         }
         info.AppendLine("Offset in RGN    (3 Byte):             " + DecimalAndHexAndBinary((ulong)subdiv.Data.Offset));
         info.AppendLine("   calculated length:                  " + DecimalAndHexAndBinary((ulong)subdiv.Data.Length));
         info.AppendLine("Content          (1 Byte):             " + DecimalAndHexAndBinary((ulong)(byte)subdiv.Content) + "; " + subdiv.Content.ToString());
         info.AppendLine("Center Longitude (3 Byte):             " + DecimalAndHexAndBinary(subdiv.Center.Longitude) + "; " + subdiv.Center.LongitudeDegree.ToString() + "°");
         info.AppendLine("Center Latitude  (3 Byte):             " + DecimalAndHexAndBinary(subdiv.Center.Latitude) + "; " + subdiv.Center.LatitudeDegree.ToString() + "°");
         info.AppendLine("HalfWidth        (2 Byte) (Bit 0..14): " + DecimalAndHexAndBinary(subdiv.HalfWidth) + "; " + GarminCore.Coord.MapUnits2Degree(subdiv.HalfWidth).ToString() + "°");
         info.AppendLine("LastSubdiv                (Bit 15):    " + subdiv.LastSubdiv.ToString());
         info.AppendLine("HalfHeight       (2 Byte):             " + DecimalAndHexAndBinary(subdiv.HalfHeight) + "; " + GarminCore.Coord.MapUnits2Degree(subdiv.HalfHeight).ToString() + "°");
         if (subdiv is GarminCore.Files.StdFile_TRE.SubdivInfo) {
            info.AppendLine("FirstChildSubdivIdx, 1 based (2 Byte): " + DecimalAndHexAndBinary((subdiv as GarminCore.Files.StdFile_TRE.SubdivInfo).FirstChildSubdivIdx1) + "; " + GarminCore.Coord.MapUnits2Degree(subdiv.HalfHeight).ToString() + "°");
            info.AppendLine("   calculated ChildSubdivCount:        " + (subdiv as GarminCore.Files.StdFile_TRE.SubdivInfo).ChildSubdivInfos.ToString());
         }

         GarminCore.DataBlock block = new GarminCore.DataBlock();
         for (int i = 0; i < subdividx; i++)
            block.Offset += tre.SubdivInfoList[i] is GarminCore.Files.StdFile_TRE.SubdivInfo ?
                                                            GarminCore.Files.StdFile_TRE.SubdivInfo.DataLength :
                                                            GarminCore.Files.StdFile_TRE.SubdivInfoBasic.DataLength;
         block.Length = (tre.SubdivInfoList[subdividx]) is GarminCore.Files.StdFile_TRE.SubdivInfo ?
                                                            GarminCore.Files.StdFile_TRE.SubdivInfo.DataLength :
                                                            GarminCore.Files.StdFile_TRE.SubdivInfoBasic.DataLength;
         return block;
      }

   }
}
