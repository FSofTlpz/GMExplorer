using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataNET : Data {

      public const int NET_IDX_ROAD = 0x1 << 30;
      public const int NET_IDX_SORTEDOFFSET = 0x1 << 29;
      public const int NET_IDX_MASK = 0x00FFFFFF;


      /// <summary>
      /// Knoten für eine logische oder physische NET-Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="net">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_NET net, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, net, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, net, "Garmin NET-Header"));

         if (net.PostHeaderDataBlock != null && net.PostHeaderDataBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.RGN_PostHeaderData, net, "PostHeaderDataBlock");
         if (net.RoadDefinitionsBlock != null && net.RoadDefinitionsBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.NET_RoadDefinitionsBlock, net, "RoadDefinitionsBlock"));
         if (net.SegmentedRoadsBlock != null && net.SegmentedRoadsBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.NET_SegmentedRoadsBlock, net, "SegmentedRoadsBlock"));
         if (net.SortedRoadsBlock != null && net.SortedRoadsBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.NET_SortedRoadsBlock, net, "SortedRoadsBlock"));
      }

      /// <summary>
      /// an einige Knoten Sub-Nodes anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="net"></param>
      /// <param name="nodetype"></param>
      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_NET net, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.NET_RoadDefinitionsBlock:
               for (int i = 0; i < net.Roaddata.Count; i++) {
                  AppendNode(tn, NodeContent.NodeType.Index, i | NET_IDX_ROAD, "Roaddata " + i.ToString());
               }
               break;

            case NodeContent.NodeType.NET_SegmentedRoadsBlock:




               break;

            case NodeContent.NodeType.NET_SortedRoadsBlock:
               for (int i = 0; i < net.SortedOffsets.Count; i++) {
                  AppendNode(tn, NodeContent.NodeType.Index, i | NET_IDX_SORTEDOFFSET, "SortedOffset " + i.ToString());
               }
               break;

            case NodeContent.NodeType.RGN_PostHeaderData: break;
         }

      }

      /// <summary>
      /// Sub-Knoten für einen NET-Header anhängen (zur Hex-Darstellung der Daten)
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="net"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_NET net) {
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.RoadDefinitionsBlock, 0x15, "RoadDefinitionsBlock: " + net.RoadDefinitionsBlock.ToString()), "RoadDefinitionsBlock");
         //AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.RoadDefinitionsOffsetMultiplier, 0x1d, "RoadDefinitionsOffsetMultiplier"));
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.SegmentedRoadsBlock, 0x1E, "SegmentedRoadsBlock: " + net.SegmentedRoadsBlock.ToString()), "SegmentedRoadsBlock");
         //AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.SegmentedRoadsOffsetMultiplier, 0x26, "SegmentedRoadsOffsetMultiplier"));
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.SortedRoadsBlock, 0x27, "SortedRoadsBlock: " + net.SortedRoadsBlock.ToString()), "SortedRoadsBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x31, 0, net.Unknown_0x31.Length, NodeContent.Content4DataRange.DataType.Other, 0x31, "Unknown 0x31"), "Unknown 0x31");
         //AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x35, 0x35, "Unknown_0x35"));
         //AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x36, 0x36, "Unknown_0x36"));
         if (net.Headerlength > 0x37)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x37, 0, net.Unknown_0x37.Length, NodeContent.Content4DataRange.DataType.Other, 0x37, "Unknown 0x37"), "Unknown 0x37");
         if (net.Headerlength > 0x3b)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x3B, 0, net.Unknown_0x3B.Length, NodeContent.Content4DataRange.DataType.Other, 0x3B, "Unknown 0x3B"), "Unknown 0x3B");
         if (net.Headerlength > 0x43)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.UnknownBlock_0x43, 0x43, "UnknownBlock_0x43: " + net.UnknownBlock_0x43.ToString()), "UnknownBlock_0x43");
         //if (net.Headerlength > 0x4b)
         //   AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x4B, 0x4B, "Unknown_0x4B"));
         if (net.Headerlength > 0x4c)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.UnknownBlock_0x4C, 0x4C, "UnknownBlock_0x4C: " + net.UnknownBlock_0x4C.ToString()), "UnknownBlock_0x4C");
         if (net.Headerlength > 0x54)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x54, 0, net.Unknown_0x54.Length, NodeContent.Content4DataRange.DataType.Other, 0x54, "Unknown 0x54"), "Unknown 0x54");
         if (net.Headerlength > 0x56)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.UnknownBlock_0x56, 0x56, "UnknownBlock_0x56: " + net.UnknownBlock_0x56.ToString()), "UnknownBlock_0x56C");
         if (net.Headerlength > 0x5e)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(net.Unknown_0x5E, 0, net.Unknown_0x5E.Length, NodeContent.Content4DataRange.DataType.Other, 0x5E, "Unknown 0x5E"), "Unknown 0x5E");
      }

      /// <summary>
      /// liefert den Text für den speziellen Header
      /// </summary>
      /// <param name="info"></param>
      /// <param name="net"></param>
      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_NET net) {
         int tab = 36;
         info.AppendLine(FillWithSpace("RoadDefinitionsBlock", tab, false, net.RoadDefinitionsBlock.ToString()));
         info.AppendLine(FillWithSpace("RoadDefinitionsOffsetMultiplier", tab, false, DecimalAndHexAndBinary(net.RoadDefinitionsOffsetMultiplier)));
         info.AppendLine(FillWithSpace("SegmentedRoadsBlock", tab, false, net.SegmentedRoadsBlock.ToString()));
         info.AppendLine(FillWithSpace("SegmentedRoadsOffsetMultiplier", tab, false, DecimalAndHexAndBinary(net.SegmentedRoadsOffsetMultiplier)));
         info.AppendLine(FillWithSpace("SortedRoadsBlock", tab, false, net.SortedRoadsBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x31", tab, false, HexString(net.Unknown_0x31)));
         info.AppendLine(FillWithSpace("Unknown 0x35", tab, false, DecimalAndHexAndBinary(net.Unknown_0x35)));
         info.AppendLine(FillWithSpace("Unknown 0x36", tab, false, DecimalAndHexAndBinary(net.Unknown_0x36)));
         if (net.Headerlength > 0x37)
            info.AppendLine(FillWithSpace("Unknown 0x37", tab, false, HexString(net.Unknown_0x37)));
         if (net.Headerlength > 0x3B)
            info.AppendLine(FillWithSpace("Unknown 0x3B", tab, false, HexString(net.Unknown_0x3B)));
         if (net.Headerlength > 0x43)
            info.AppendLine(FillWithSpace("UnknownBlock 0x43", tab, false, net.UnknownBlock_0x43.ToString()));
         if (net.Headerlength > 0x4b)
            info.AppendLine(FillWithSpace("Unknown 0x4B", tab, false, DecimalAndHexAndBinary(net.Unknown_0x4B)));
         if (net.Headerlength > 0x4c)
            info.AppendLine(FillWithSpace("UnknownBlock 0x4C", tab, false, net.UnknownBlock_0x4C.ToString()));
         if (net.Headerlength > 0x54)
            info.AppendLine(FillWithSpace("Unknown 0x54", tab, false, HexString(net.Unknown_0x54)));
         if (net.Headerlength > 0x56)
            info.AppendLine(FillWithSpace("UnknownBlock 0x56", tab, false, net.UnknownBlock_0x56.ToString()));
         if (net.Headerlength > 0x5e)
            info.AppendLine(FillWithSpace("Unknown 0x5E", tab, false, HexString(net.Unknown_0x5E)));
      }

      /// <summary>
      /// Funktion für alle NET-Datei-Infos
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="nodetype"></param>
      /// <param name="idx"></param>
      /// <param name="tvd"></param>
      public static void SectionAndIndex(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, NodeContent.NodeType nodetype, int idx, TreeViewData tvd) {
         GarminCore.Files.StdFile_NET net = filedata.GetGarminFileAsNET(tvd.GetLBL(filedata.Basename));
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;
         int tab = 20;

         switch (nodetype) {
            case NodeContent.NodeType.RGN_PostHeaderData:
               firsthexadr = net.PostHeaderDataBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("PostHeaderDataBlock: " + net.PostHeaderDataBlock.ToString());
                  hexlen = (int)net.PostHeaderDataBlock.Length;
               }
               break;

            case NodeContent.NodeType.NET_RoadDefinitionsBlock:
               firsthexadr = net.RoadDefinitionsBlock.Offset;
               if (idx < 0) {
                  tab = 15;
                  info.AppendLine("RoadDefinitionsBlock");
                  info.AppendLine(FillWithSpace("  Block", tab, false, net.RoadDefinitionsBlock.ToString()));
                  info.AppendLine(FillWithSpace("  Roads", tab, false, net.Roaddata.Count.ToString()));
                  hexlen = (int)net.RoadDefinitionsBlock.Length;
               } else { // einzelnes Element
                  if ((idx & NET_IDX_ROAD) != 0) {
                     idx &= NET_IDX_MASK;
                     tab = 25;

                     GarminCore.Files.StdFile_NET.RoadData rd = net.Roaddata[idx];

                     //info.AppendLine(rd.ToString());
                     string labels = rd.LabelInfo.Count.ToString();
                     if (rd.LabelInfo.Count > 0) {
                        labels += " -> ";
                        for (int i = 0; i < rd.LabelInfo.Count; i++) {
                           if (i > 0)
                              labels += ", ";
                           labels += DecimalAndHexAndBinary(rd.LabelInfo[i]);
                        }
                     }
                     info.AppendLine(FillWithSpace("Labels", tab, false, labels));
                     for (int i = 0; i < rd.LabelInfo.Count; i++)
                        info.AppendLine(FillWithSpace("   Label (LBL)" + i.ToString(), tab, true, rd.GetText(net.Lbl, i, false)));

                     info.AppendLine(FillWithSpace("Roaddatainfos", tab, false, rd.Roaddatainfos.ToString()));
                     info.AppendLine(FillWithSpace("RoadLength", tab, false, rd.RoadLength.ToString() + "m"));

                     info.AppendLine(FillWithSpace("RgnIndexOverviews", tab, false, rd.RgnIndexOverview.Count.ToString()));
                     for (int i = 0, j = 0; i < rd.RgnIndexOverview.Count; i++) {
                        info.Append(FillWithSpace("   RgnIndexOverview " + i.ToString(), tab, false, rd.RgnIndexOverview[i].ToString()));
                        if (rd.RgnIndexOverview[i] > 0) {
                           info.Append(" -> ");
                           for (int k = 0; k < rd.RgnIndexOverview[i]; k++) {
                              info.Append(" " + rd.Indexdata[j++].ToString());
                           }
                        }
                        info.AppendLine();
                     }
                     info.AppendLine(FillWithSpace("SegmentedRoadOffsets", tab, false, rd.SegmentedRoadOffsets.Count.ToString()));
                     for (int i = 0; i < rd.SegmentedRoadOffsets.Count; i++) {
                        info.AppendLine(FillWithSpace("   SegmentedRoadOffsets " + i.ToString(), tab, false, DecimalAndHexAndBinary(rd.SegmentedRoadOffsets[i])));
                     }

                     if (rd.HasRoadDataInfo(GarminCore.Files.StdFile_NET.RoadData.RoadDataInfo.has_street_address_info)) {
                        // Bit 0..9 'NodeCount', Bits 10,11 für Zip; Bits 12,13 für City; Bits 14,15 für Nummbers
                        info.AppendLine(FillWithSpace("NodeCountAndFlags", tab, false, DecimalAndHexAndBinary(rd.NodeCountAndFlags, 16)));
                        info.AppendLine(FillWithSpace("   NodeCount", tab, false, rd.NodeCount.ToString()));
                        info.AppendLine(FillWithSpace("   ZipFlag", tab, false, DecimalAndHexAndBinary(rd.ZipFlag)));
                        for (int side = 0; side < rd.ZipIndex4Node.Count; side++) {
                           if (rd.ZipIndex4Node[side].Count > 0) {
                              info.AppendLine(FillWithSpace(side == 0 ?
                                                   "      Left (LBL)" :
                                                   "      Right (LBL)", tab, true,
                                              rd.GetZipText(net.Lbl,
                                                            side == 0 ?
                                                                  GarminCore.Files.StdFile_NET.RoadData.Side.Left :
                                                                  GarminCore.Files.StdFile_NET.RoadData.Side.Right,
                                                            false)));
                           }
                        }
                        info.AppendLine(FillWithSpace("   CityFlag", tab, false, DecimalAndHexAndBinary(rd.CityFlag)));
                        for (int side = 0; side < rd.CityIndex4Node.Count; side++) {
                           if (rd.CityIndex4Node[side].Count > 0) {
                              info.AppendLine(FillWithSpace(side == 0 ?
                                                   "      Left (LBL)" :
                                                   "      Right (LBL)", tab, true,
                                              rd.GetCityText(net.Lbl,
                                                             tvd.GetRGN(filedata.Basename),
                                                             side == 0 ?
                                                                  GarminCore.Files.StdFile_NET.RoadData.Side.Left :
                                                                  GarminCore.Files.StdFile_NET.RoadData.Side.Right,
                                                            false)));
                           }
                        }

                        string bin = "";
                        for (int i = 0; i < rd.NumberStream.Length; i++)
                           bin += Binary(rd.NumberStream[i]);

                        bin = Binary(rd.NumberStream);
                        info.AppendLine(FillWithSpace("   NumberFlag, -stream", tab, false, DecimalAndHexAndBinary(rd.NumberFlag) + "   " + HexString(rd.NumberStream) + "   " + bin));
                        if (rd.NumberStream.Length > 0) {

                           GarminCore.Files.StdFile_NET.RoadData.Housenumbers numbers = new GarminCore.Files.StdFile_NET.RoadData.Housenumbers(rd.NumberStream);
                           for (int n = 0; n < numbers.Numbers.Count; n++) {
                              info.AppendLine(new string(' ', tab + 3) + numbers.Numbers[n].ToString() + " (raw: " + numbers.Numbers[n].rawdata.ToString() + ")");
                           }


                        }

                     }




                     if (rd.HasRoadDataInfo(GarminCore.Files.StdFile_NET.RoadData.RoadDataInfo.has_nod_info)) {
                        info.AppendLine(FillWithSpace("NODFlag", tab, false, rd.NODFlag.ToString()));
                        info.AppendLine(FillWithSpace("NOD_Offset", tab, false, DecimalAndHexAndBinary(rd.NOD_Offset)));
                     }

                     // für die Hex-Anzeige
                     firsthexadr = net.RoadDefinitionsBlock.Offset;
                     for (int i = 0; i < idx; i++)
                        firsthexadr += net.Roaddata[i].RawBytes;
                     hexlen = (int)net.Roaddata[idx].RawBytes;
                  }
               }
               break;

            case NodeContent.NodeType.NET_SegmentedRoadsBlock:
               firsthexadr = net.RoadDefinitionsBlock.Offset;
               if (idx < 0) {
                  tab = 15;
                  info.AppendLine("SegmentedRoadsBlock");
                  info.AppendLine(FillWithSpace("  Block", tab, false, net.SegmentedRoadsBlock.ToString()));
                  hexlen = (int)net.SegmentedRoadsBlock.Length;




               } else { // einzelnes Element




               }
               break;

            case NodeContent.NodeType.NET_SortedRoadsBlock:
               firsthexadr = net.RoadDefinitionsBlock.Offset;
               if (idx < 0) {
                  tab = 15;
                  info.AppendLine("SortedRoadsBlock");
                  info.AppendLine(FillWithSpace("  Block", tab, false, net.SortedRoadsBlock.ToString()));
                  info.AppendLine(FillWithSpace("  Offsets", tab, false, net.SortedOffsets.Count.ToString()));
                  hexlen = (int)net.SortedRoadsBlock.Length;

                  List<uint> t1 = new List<uint>(net.SortedOffsets);
                  t1.Sort();

               } else { // einzelnes Element
                  tab = 20;
                  if ((idx & NET_IDX_SORTEDOFFSET) != 0) {
                     idx &= NET_IDX_MASK;
                     info.AppendLine(FillWithSpace("Offset", tab, false, DecimalAndHexAndBinary(net.SortedOffsets[idx])));
                     int offs = (int)net.SortedOffsets[idx];
                     for (int i = 0; i < net.Roaddata.Count; i++) {
                        offs -= net.Roaddata[i].RawBytes;
                        if (offs == 0) {
                           info.AppendLine(FillWithSpace("Roaddata-Index", tab, false, i.ToString()));
                           break;
                        } else if (offs < 0) {
                           info.AppendLine("Roaddata-Index not found -> error ?");
                           break;
                        }
                     }
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
