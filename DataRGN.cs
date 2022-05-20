using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataRGN : Data {

      public const int RGN_IDX_POINT1 = 0x1 << 30;
      public const int RGN_IDX_POINT2 = 0x1 << 29;
      public const int RGN_IDX_LINE = 0x1 << 28;
      public const int RGN_IDX_AREA = 0x1 << 27;
      public const int RGN_IDX_EXTPOINT = 0x1 << 26;
      public const int RGN_IDX_EXTLINE = 0x1 << 25;
      public const int RGN_IDX_EXTAREA = 0x1 << 24;
      public const int RGN_IDX_MASK = 0x00FFFFFF;

      /// <summary>
      /// Knoten für eine logische oder physische RGN-Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="rgn">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_RGN rgn, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, rgn, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, rgn, "Garmin RGN-Header"));

         if (rgn.PostHeaderDataBlock != null && rgn.PostHeaderDataBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.RGN_PostHeaderData, rgn, "PostHeaderDataBlock");
         if (rgn.SubdivContentBlock != null && rgn.SubdivContentBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.RGN_SubdivContentBlock, rgn, "SubdivContentBlock"));
         if (rgn.ExtAreasBlock != null && rgn.ExtAreasBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.RGN_ExtAreasBlock, rgn, "ExtAreasBlock"));
         if (rgn.ExtLinesBlock != null && rgn.ExtLinesBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.RGN_ExtLinesBlock, rgn, "ExtLinesBlock"));
         if (rgn.ExtPointsBlock != null && rgn.ExtPointsBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.RGN_ExtPointsBlock, rgn, "ExtPointsBlock"));
         if (rgn.UnknownBlock_0x71 != null && rgn.UnknownBlock_0x71.Length > 0)
            AppendNode(tn, NodeContent.NodeType.RGN_UnknownBlock_0x71, rgn, "UnknownBlock 0x71");
      }

      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_RGN rgn, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);
         int count = 0;

         switch (nodetype) {
            case NodeContent.NodeType.RGN_SubdivContentBlock:
               for (int i = 0; i < rgn.SubdivList.Count; i++) {
                  TreeNode tn2 = AppendNode(tn, NodeContent.NodeType.Index, i, "SubdivContent " + i.ToString());

                  //GarminCore.Files.StdFile_TRE.SubdivInfoBasic subdivfinfo = rgn.TREFile.SubdivInfoList[i];
                  GarminCore.Files.StdFile_RGN.SubdivData subdivdata = rgn.SubdivList[i];

                  for (int j = 0; j < subdivdata.PointList1.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_POINT1, "Point1 " + j.ToString());
                  for (int j = 0; j < subdivdata.PointList2.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_POINT2, "Point2 " + j.ToString());
                  for (int j = 0; j < subdivdata.LineList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_LINE, "Line " + j.ToString());
                  for (int j = 0; j < subdivdata.AreaList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_AREA, "Area " + j.ToString());
                  for (int j = 0; j < subdivdata.ExtPointList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_EXTPOINT, "ExtPoint " + j.ToString());
                  for (int j = 0; j < subdivdata.ExtLineList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_EXTLINE, "ExtLine " + j.ToString());
                  for (int j = 0; j < subdivdata.ExtAreaList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_EXTAREA, "ExtArea " + j.ToString());

               }
               break;

            case NodeContent.NodeType.RGN_ExtAreasBlock:
               for (int i = 0; i < rgn.SubdivList.Count; i++)
                  count += rgn.SubdivList[i].ExtAreaList.Count;
               for (int i = 0; i < count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i | RGN_IDX_EXTAREA, "ExtArea " + i.ToString());
               break;

            case NodeContent.NodeType.RGN_ExtLinesBlock:
               for (int i = 0; i < rgn.SubdivList.Count; i++)
                  count += rgn.SubdivList[i].ExtLineList.Count;
               for (int i = 0; i < count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i | RGN_IDX_EXTLINE, "ExtLine " + i.ToString());
               break;

            case NodeContent.NodeType.RGN_ExtPointsBlock:
               for (int i = 0; i < rgn.SubdivList.Count; i++)
                  count += rgn.SubdivList[i].ExtPointList.Count;
               for (int i = 0; i < count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i | RGN_IDX_EXTPOINT, "ExtPoint " + i.ToString());
               break;



            case NodeContent.NodeType.RGN_PostHeaderData: break;
         }
      }

      /// <summary>
      /// Knoten für einen RGN-Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="rgn"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_RGN rgn) {
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.SubdivContentBlock, 0x15, "SubdivContentBlock: " + rgn.SubdivContentBlock.ToString()), "SubdivContentBlock");
         if (rgn.Headerlength >= 0x1D)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.ExtAreasBlock, 0x1D, "ExtAreasBlock: " + rgn.ExtAreasBlock.ToString()), "ExtAreasBlock");
         if (rgn.Headerlength >= 0x25)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.Unknown_0x25, 0, rgn.Unknown_0x25.Length, NodeContent.Content4DataRange.DataType.Other, 0x25, "Unknown 0x25"), "Unknown 0x25");
         if (rgn.Headerlength >= 0x39)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.ExtLinesBlock, 0x39, "ExtLinesBlock: " + rgn.ExtLinesBlock.ToString()), "ExtLinesBlock");
         if (rgn.Headerlength >= 0x41)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.Unknown_0x41, 0, rgn.Unknown_0x41.Length, NodeContent.Content4DataRange.DataType.Other, 0x41, "Unknown 0x41"), "Unknown 0x41");
         if (rgn.Headerlength >= 0x55)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.ExtLinesBlock, 0x55, "ExtPointsBlock: " + rgn.ExtPointsBlock.ToString()), "ExtPointsBlock");
         if (rgn.Headerlength >= 0x5D)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.Unknown_0x5D, 0, rgn.Unknown_0x5D.Length, NodeContent.Content4DataRange.DataType.Other, 0x5D, "Unknown 0x5D"), "Unknown 0x5D");
         if (rgn.Headerlength >= 0x71)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.UnknownBlock_0x71, 0x71, "UnknownBlock 0x71: " + rgn.UnknownBlock_0x71.ToString()), "UnknownBlock 0x71");
         if (rgn.Headerlength >= 0x79)
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(rgn.Unknown_0x79, 0, rgn.Unknown_0x79.Length, NodeContent.Content4DataRange.DataType.Other, 0x79, "Unknown 0x79"), "Unknown 0x79");
      }

      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_RGN rgn) {
         int tab = 22;
         info.AppendLine(FillWithSpace("SubdivContent", tab, false, rgn.SubdivContentBlock.ToString()));
         if (rgn.Headerlength > 0x1D) {
            info.AppendLine(FillWithSpace("ExtAreas", tab, false, rgn.ExtAreasBlock.ToString()));
            info.AppendLine(FillWithSpace("Unknown_0x25", tab, false, HexString(rgn.Unknown_0x25)));
            info.AppendLine(FillWithSpace("ExtLines", tab, false, rgn.ExtLinesBlock.ToString()));
            info.AppendLine(FillWithSpace("Unknown_0x41", tab, false, HexString(rgn.Unknown_0x41)));
            info.AppendLine(FillWithSpace("ExtPoints", tab, false, rgn.ExtPointsBlock.ToString()));
            info.AppendLine(FillWithSpace("Unknown_0x5D", tab, false, HexString(rgn.Unknown_0x5D)));
         }
         if (rgn.Headerlength >= 0x71)
            info.AppendLine(FillWithSpace("UnknownBlock 0x71", tab, false, rgn.UnknownBlock_0x71.ToString()));
         if (rgn.Headerlength >= 0x79)
            info.AppendLine(FillWithSpace("Unknown_0x79", tab, false, HexString(rgn.Unknown_0x79)));
      }

      /// <summary>
      /// Funktion für alle RGN-Datei-Infos
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="nodetype"></param>
      /// <param name="idx"></param>
      /// <param name="tvd"></param>
      public static void SectionAndIndex(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, NodeContent.NodeType nodetype, int idx, TreeViewData tvd) {
         GarminCore.Files.StdFile_RGN rgn = filedata.GetGarminFileAsRGN(tvd.GetTRE(filedata.Basename));
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;
         AllSubdivInfo asi = null;

         switch (nodetype) {
            case NodeContent.NodeType.RGN_PostHeaderData:
               firsthexadr = rgn.PostHeaderDataBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("PostHeaderDataBlock: " + rgn.PostHeaderDataBlock.ToString());
                  hexlen = (int)rgn.PostHeaderDataBlock.Length;
               }
               break;

            case NodeContent.NodeType.RGN_SubdivContentBlock:
               firsthexadr = rgn.SubdivContentBlock.Offset;
               if (idx < 0) {
                  int tab = 22;
                  info.AppendLine(FillWithSpace("SubdivContent", tab, false, rgn.SubdivList.Count.ToString()));
                  info.AppendLine(FillWithSpace("  Block", tab, false, rgn.SubdivContentBlock.ToString()));
                  hexlen = (int)rgn.SubdivContentBlock.Length;
                  if (hexlen > 0 && rgn.SubdivList.Count == 0)
                     info.AppendLine("data propably encrypted");

               } else {
                  asi = new AllSubdivInfo(rgn, filedata.BinaryReader, idx);

                  info.AppendLine("Info from TRE:");
                  DataTRE.SampleInfo4SubdivInfo(info, rgn.TREFile, idx);
                  info.AppendLine();

                  info.AppendLine("Info from RGN:");
                  GarminCore.DataBlock block = SampleInfo4SubdivData(info, rgn, asi);
                  firsthexadr += block.Offset;
                  hexlen = (int)block.Length;
               }
               break;

            case NodeContent.NodeType.RGN_ExtAreasBlock:
               firsthexadr = rgn.ExtAreasBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExtAreas");
                  info.AppendLine("  Block:  " + rgn.ExtAreasBlock.ToString());
                  hexlen = (int)rgn.ExtAreasBlock.Length;
               } else {
                  int subdividx = -1;
                  int count = 0;
                  idx &= RGN_IDX_MASK; // Gesamt-Index des ExtArea
                  for (subdividx = 0; subdividx < rgn.SubdivList.Count; subdividx++) {
                     count += rgn.SubdivList[subdividx].ExtAreaList.Count;
                     if (idx < count)
                        break;
                  }
                  if (subdividx < rgn.SubdivList.Count) {
                     asi = new AllSubdivInfo(rgn, filedata.BinaryReader, subdividx);
                     int idx2 = asi.SubdivData.ExtAreaList.Count - (count - idx);
                     info.AppendLine("ExtArea " + idx2.ToString() + " from Subdiv " + subdividx.ToString());
                     ShowExtAreaInfo(info, asi, rgn, tvd, filedata, idx2, 40, out hexlen, out firsthexadr);
                  }
               }
               break;

            case NodeContent.NodeType.RGN_ExtLinesBlock:
               firsthexadr = rgn.ExtLinesBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExtLines");
                  info.AppendLine("  Block:  " + rgn.ExtLinesBlock.ToString());
                  hexlen = (int)rgn.ExtLinesBlock.Length;
               } else {
                  int subdividx = -1;
                  int count = 0;
                  idx &= RGN_IDX_MASK; // Gesamt-Index des ExtArea
                  for (subdividx = 0; subdividx < rgn.SubdivList.Count; subdividx++) {
                     count += rgn.SubdivList[subdividx].ExtLineList.Count;
                     if (idx < count)
                        break;
                  }
                  if (subdividx < rgn.SubdivList.Count) {
                     asi = new AllSubdivInfo(rgn, filedata.BinaryReader, subdividx);
                     int idx2 = asi.SubdivData.ExtLineList.Count - (count - idx);
                     info.AppendLine("ExtLine " + idx2.ToString() + " from Subdiv " + subdividx.ToString());
                     ShowExtLineInfo(info, asi, rgn, tvd, filedata, idx2, 40, out hexlen, out firsthexadr);
                  }
               }
               break;

            case NodeContent.NodeType.RGN_ExtPointsBlock:
               firsthexadr = rgn.ExtPointsBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExtPoints");
                  info.AppendLine("  Block:  " + rgn.ExtPointsBlock.ToString());
                  hexlen = (int)rgn.ExtPointsBlock.Length;
               } else {
                  int subdividx = -1;
                  int count = 0;
                  idx &= RGN_IDX_MASK; // Gesamt-Index des ExtArea
                  for (subdividx = 0; subdividx < rgn.SubdivList.Count; subdividx++) {
                     count += rgn.SubdivList[subdividx].ExtPointList.Count;
                     if (idx < count)
                        break;
                  }
                  if (subdividx < rgn.SubdivList.Count) {
                     asi = new AllSubdivInfo(rgn, filedata.BinaryReader, subdividx);
                     int idx2 = asi.SubdivData.ExtPointList.Count - (count - idx);
                     info.AppendLine("ExtPoint " + idx2.ToString() + " from Subdiv " + subdividx.ToString());
                     ShowExtPointInfo(info, asi, rgn, tvd, filedata, idx2, 40, out hexlen, out firsthexadr);
                  }
               }
               break;

            case NodeContent.NodeType.RGN_UnknownBlock_0x71:
               firsthexadr = rgn.UnknownBlock_0x71.Offset;
               if (idx < 0) {
                  info.AppendLine("UnknownBlock 0x71");
                  info.AppendLine("  Block:  " + rgn.UnknownBlock_0x71.ToString());
                  hexlen = (int)rgn.UnknownBlock_0x71.Length;
               }
               break;

            case NodeContent.NodeType.Index:
               if (tvd.TreeView.SelectedNode != null) {
                  int subdividx = -1;

                  // etwas tricky ...
                  NodeContent nc = NodeContent4TreeNode(tvd.TreeView.SelectedNode.Parent);
                  switch (nc.Type) {
                     case NodeContent.NodeType.Index: // wenn der übergeordnete Knoten ein Index ist, dann ist es ein Subdiv-Index
                        subdividx = (int)nc.Data;
                        if (subdividx >= 0)
                           asi = new AllSubdivInfo(rgn, filedata.BinaryReader, subdividx);
                        break;
                  }

               }

               if (asi != null) {
                  if ((idx & RGN_IDX_POINT1) != 0) {
                     idx &= RGN_IDX_MASK;
                     int tab = 40;

                     GarminCore.Files.StdFile_RGN.RawPointData point = asi.GetPoint(idx);
                     GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);

                     info.AppendLine(FillWithSpace("Type             (1 Byte)", tab, false, DecimalAndHexAndBinary(point.Type)));
                     info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(point.LabelOffsetInLBL)));
                     if (lbl != null)
                        info.AppendLine(FillWithSpace("   Text (from LBL)", tab, true, point.GetText(lbl, true)));
                     info.AppendLine(FillWithSpace("HasSubtype                (Bit 23)", tab, false, point.HasSubtype.ToString()));
                     info.AppendLine(FillWithSpace("IsPoiOffset               (Bit 22)", tab, false, point.IsPoiOffset.ToString()));
                     info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)point.RawDeltaLongitude.Value) + ", " + point.RawDeltaLongitude.ValueDegree.ToString() + "°"));
                     info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)point.RawDeltaLatitude.Value) + ", " + point.RawDeltaLatitude.ValueDegree.ToString() + "°"));
                     if (point.HasSubtype)
                        info.AppendLine(FillWithSpace("Subtype        (1 Byte)", tab, false, DecimalAndHexAndBinary(point.Subtype)));

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_POINT2) != 0) {
                     idx &= RGN_IDX_MASK;
                     int tab = 40;

                     GarminCore.Files.StdFile_RGN.RawPointData point = asi.GetIdxPoint(idx);
                     GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);

                     info.AppendLine(FillWithSpace("Type             (1 Byte)", tab, false, DecimalAndHexAndBinary(point.Type)));
                     info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(point.LabelOffsetInLBL)));
                     if (lbl != null)
                        info.AppendLine(FillWithSpace("   Text (from LBL)", tab, true, point.GetText(lbl, true)));
                     info.AppendLine(FillWithSpace("HasSubtype                (Bit 23)", tab, false, point.HasSubtype.ToString()));
                     info.AppendLine(FillWithSpace("IsPoiOffset               (Bit 22)", tab, false, point.IsPoiOffset.ToString()));
                     info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)point.RawDeltaLongitude.Value) + ", " + point.RawDeltaLongitude.ValueDegree.ToString() + "°"));
                     info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)point.RawDeltaLatitude.Value) + ", " + point.RawDeltaLatitude.ValueDegree.ToString() + "°"));
                     if (point.HasSubtype)
                        info.AppendLine(FillWithSpace("Subtype          (1 Byte)", tab, false, DecimalAndHexAndBinary(point.Subtype)));

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_LINE) != 0) {
                     idx &= RGN_IDX_MASK;
                     int tab = 40;

                     GarminCore.Files.StdFile_RGN.RawPolyData poly = asi.GetLine(idx);
                     GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                     GarminCore.Files.StdFile_NET net = tvd.GetNET(filedata.Basename);

                     info.AppendLine(FillWithSpace("Type             (1 Byte) (Bit 0..5)", tab, false, DecimalAndHexAndBinary(poly.Type)));
                     info.AppendLine(FillWithSpace("TwoByteLength             (Bit 7)", tab, false, poly.TwoByteLength.ToString()));
                     if (!poly.IsPolygon)
                        info.AppendLine(FillWithSpace("Direction                 (Bit 6)", tab, false, poly.DirectionIndicator.ToString()));
                     info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(poly.LabelOffsetInLBL)));

                     if (!poly.LabelInNET) {
                        if (lbl != null)
                           info.AppendLine(FillWithSpace("Text (from LBL)", tab, true, poly.GetText(lbl, true)));
                     } else {
                        if (net != null) {
                           GarminCore.Files.StdFile_NET.RoadData rd = net.GetRoadData(poly.LabelOffsetInLBL);
                           if (rd != null) {
                              info.AppendLine(FillWithSpace("Text (from NET, LBL)", tab, true, rd.GetText(lbl, true)));
                           }
                        }
                     }

                     info.AppendLine(FillWithSpace("WithExtraBit              (Bit 22)", tab, false, poly.WithExtraBit.ToString()));
                     info.AppendLine(FillWithSpace("LabelInNET                (Bit 23)", tab, false, poly.LabelInNET.ToString()));
                     info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°"));
                     info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°"));
                     info.AppendLine(FillWithSpace("BitstreamInfo    (1 Byte)", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo)));
                     info.AppendLine(FillWithSpace("   basebits4lat", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo >> 4)));
                     info.AppendLine(FillWithSpace("   basebits4lon", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF)));
                     info.AppendLine(FillWithSpace("BitstreamLength (" + (poly.TwoByteLength ? "2" : "1") + " Byte)", tab, false, DecimalAndHexAndBinary(poly.bitstream.Length)));
                     if (poly.WithExtraBit)
                        info.AppendLine(FillWithSpace("ExtraBitList", tab, false, AllSubdivInfo.ExtraBits(poly)));
                     info.AppendLine("Deltas:");
                     List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
                     for (int i = 0; i < points.Count; i++)
                        info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.line, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_AREA) != 0) {
                     idx &= RGN_IDX_MASK;
                     int tab = 40;

                     GarminCore.Files.StdFile_RGN.RawPolyData poly = asi.GetArea(idx);
                     GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                     GarminCore.Files.StdFile_NET net = tvd.GetNET(filedata.Basename);

                     info.AppendLine(FillWithSpace("Type             (1 Byte) (Bit 0..6) ", tab, false, DecimalAndHexAndBinary(poly.Type)));
                     info.AppendLine(FillWithSpace("TwoByteLength             (Bit 7)", tab, false, poly.TwoByteLength.ToString()));
                     info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(poly.LabelOffsetInLBL)));

                     if (!poly.LabelInNET) {
                        if (lbl != null)
                           info.AppendLine(FillWithSpace("Text (from LBL)", tab, true, poly.GetText(lbl, true)));
                     } else {
                        if (net != null) {
                           GarminCore.Files.StdFile_NET.RoadData rd = net.GetRoadData(poly.LabelOffsetInLBL);
                           if (rd != null) {
                              info.AppendLine(FillWithSpace("Text (from NET, LBL)", tab, true, rd.GetText(lbl, true)));
                           }
                        }
                     }

                     info.AppendLine(FillWithSpace("WithExtraBit              (Bit 22)", tab, false, poly.WithExtraBit.ToString()));
                     info.AppendLine(FillWithSpace("LabelInNET                (Bit 23)", tab, false, poly.LabelInNET.ToString()));
                     info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°"));
                     info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°"));
                     info.AppendLine(FillWithSpace("BitstreamInfo    (1 Byte)", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo)));
                     info.AppendLine(FillWithSpace("   basebits4lat", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo >> 4)));
                     info.AppendLine(FillWithSpace("   basebits4lon", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF)));
                     info.AppendLine(FillWithSpace("BitstreamLength (" + (poly.TwoByteLength ? "2" : "1") + " Byte)", tab, false, DecimalAndHexAndBinary(poly.bitstream.Length)));
                     if (poly.WithExtraBit)
                        info.AppendLine(FillWithSpace("ExtraBitList", tab, false, AllSubdivInfo.ExtraBits(poly)));
                     info.AppendLine("Deltas:");
                     List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
                     for (int i = 0; i < points.Count; i++)
                        info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_EXTPOINT) != 0) {

                     ShowExtPointInfo(info, asi, rgn, tvd, filedata, idx & RGN_IDX_MASK, 40, out hexlen, out firsthexadr);

                  } else if ((idx & RGN_IDX_EXTLINE) != 0) {

                     ShowExtLineInfo(info, asi, rgn, tvd, filedata, idx & RGN_IDX_MASK, 40, out hexlen, out firsthexadr);

                  } else if ((idx & RGN_IDX_EXTAREA) != 0) {

                     ShowExtAreaInfo(info, asi, rgn, tvd, filedata, idx & RGN_IDX_MASK, 40, out hexlen, out firsthexadr);

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

      public static GarminCore.DataBlock SampleInfo4SubdivData(StringBuilder info, GarminCore.Files.StdFile_RGN rgn, AllSubdivInfo asi) {
         int offset = 0;
         int tab = 40;
         if (asi.SubdivData.PointList1.Count > 0) {
            info.AppendLine(FillWithSpace("PointList1", tab, false, asi.SubdivData.PointList1.Count.ToString()));
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi);
            if (offset >= 0)
               info.AppendLine(FillWithSpace("   Offset", tab, false, DecimalAndHexAndBinary(offset)));
         }
         if (asi.SubdivData.PointList2.Count > 0) {
            info.AppendLine(FillWithSpace("PointList2", tab, false, asi.SubdivData.PointList2.Count.ToString()));
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi);
            if (offset >= 0)
               info.AppendLine(FillWithSpace("   Offset", tab, false, DecimalAndHexAndBinary(offset)));
         }
         if (asi.SubdivData.LineList.Count > 0) {
            info.AppendLine(FillWithSpace("LineList", tab, false, asi.SubdivData.LineList.Count.ToString()));
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.line);
            if (offset >= 0)
               info.AppendLine(FillWithSpace("   Offset", tab, false, DecimalAndHexAndBinary(offset)));
         }
         if (asi.SubdivData.AreaList.Count > 0) {
            info.AppendLine(FillWithSpace("AreaList", tab, false, asi.SubdivData.AreaList.Count.ToString()));
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area);
            if (offset >= 0)
               info.AppendLine(FillWithSpace("   Offset", tab, false, DecimalAndHexAndBinary(offset)));
         }

         info.AppendLine();
         if (asi.SubdivData.ExtPointList.Count > 0) {
            info.AppendLine(FillWithSpace("ExtPointList", tab, false, asi.SubdivData.ExtPointList.Count.ToString()));
            info.AppendLine(FillWithSpace("   Block (from TRE)", tab, false, asi.GetExtPointBlock().ToString()));
         }
         if (asi.SubdivData.ExtLineList.Count > 0) {
            info.AppendLine(FillWithSpace("ExtLineList", tab, false, asi.SubdivData.ExtLineList.Count.ToString()));
            info.AppendLine(FillWithSpace("   Block (from TRE)", tab, false, asi.GetExtLineBlock().ToString()));
         }
         if (asi.SubdivData.ExtAreaList.Count > 0) {
            info.AppendLine(FillWithSpace("ExtAreaList", tab, false, asi.SubdivData.ExtAreaList.Count.ToString()));
            info.AppendLine(FillWithSpace("   Block (from TRE)", tab, false, asi.GetExtAreaBlock().ToString()));
         }

         return asi.GetSubdivDataBlock();
      }

      static void ShowExtAreaInfo(StringBuilder info,
                                  AllSubdivInfo asi,
                                  GarminCore.Files.StdFile_RGN rgn,
                                  TreeViewData tvd,
                                  NodeContent.Content4File filedata,
                                  int areaidx,
                                  int tab,
                                  out int hexlen,
                                  out long firsthexadr) {
         GarminCore.Files.StdFile_RGN.ExtRawPolyData poly = asi.GetExtArea(areaidx);
         GarminCore.DataBlock block = asi.GetDataBlock4ExtArea(areaidx);

         firsthexadr = rgn.ExtAreasBlock.Offset;
         firsthexadr += block.Offset;
         hexlen = (int)block.Length;

         info.AppendLine(FillWithSpace("Type             (1 Byte) (Bit 0..6)", tab, false, DecimalAndHexAndBinary(poly.Type)));
         info.AppendLine(FillWithSpace("Subtype          (1 Byte) (Bit 0..4)", tab, false, DecimalAndHexAndBinary(poly.Subtype)));
         info.AppendLine(FillWithSpace("HasLabel                  (Bit 5)", tab, false, poly.HasLabel.ToString()));
         info.AppendLine(FillWithSpace("HasUnknownFlag            (Bit 6)", tab, false, poly.HasUnknownFlag.ToString()));
         info.AppendLine(FillWithSpace("HasExtraBytes             (Bit 7)", tab, false, poly.HasExtraBytes.ToString()));
         info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°"));
         info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°"));
         info.AppendLine(FillWithSpace("RawBitStreamLengthBytes (" + poly.RawBitStreamLengthBytes.Length.ToString() + " Byte)", tab, false, HexString(poly.RawBitStreamLengthBytes)));
         info.AppendLine(FillWithSpace("   BitStreamLength", tab, false, poly.BitstreamLength.ToString()));
         info.AppendLine(FillWithSpace("BitstreamInfo    (1 Byte)", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo)));
         info.AppendLine(FillWithSpace("   basebits4lat", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo >> 4)));
         info.AppendLine(FillWithSpace("   basebits4lon", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF)));
         if (poly.HasLabel) {
            info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(poly.LabelOffsetInLBL)));
            GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
            if (lbl != null)
               info.AppendLine(FillWithSpace("   Text (from LBL)", tab, true, lbl.GetText(poly.LabelOffsetInLBL, true)));
         }
         if (poly.HasExtraBytes) {
            int len = 7 + poly.RawBitStreamLengthBytes.Length;
            if (poly.HasLabel)
               len += 3;
            filedata.BinaryReader.Seek(block.Offset + (uint)len);
            info.AppendLine(FillWithSpace("ExtraBytes", tab, false, HexString(filedata.BinaryReader.ReadBytes((int)block.Length - len))));
         }
         info.AppendLine("Deltas:");
         List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
         for (int i = 0; i < points.Count; i++)
            info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());
      }

      static void ShowExtLineInfo(StringBuilder info,
                                  AllSubdivInfo asi,
                                  GarminCore.Files.StdFile_RGN rgn,
                                  TreeViewData tvd,
                                  NodeContent.Content4File filedata,
                                  int lineidx,
                                  int tab,
                                  out int hexlen,
                                  out long firsthexadr) {
         GarminCore.Files.StdFile_RGN.ExtRawPolyData poly = asi.GetExtLine(lineidx);
         GarminCore.DataBlock block = asi.GetDataBlock4ExtLine(lineidx);

         firsthexadr = rgn.ExtLinesBlock.Offset;
         firsthexadr += block.Offset;
         hexlen = (int)block.Length;

         info.AppendLine(FillWithSpace("Type             (1 Byte) (Bit 0..6)", tab, false, DecimalAndHexAndBinary(poly.Type)));
         info.AppendLine(FillWithSpace("Subtype          (1 Byte) (Bit 0..4)", tab, false, DecimalAndHexAndBinary(poly.Subtype)));
         info.AppendLine(FillWithSpace("HasLabel                  (Bit 5)", tab, false, poly.HasLabel.ToString()));
         info.AppendLine(FillWithSpace("HasUnknownFlag            (Bit 6)", tab, false, poly.HasUnknownFlag.ToString()));
         info.AppendLine(FillWithSpace("HasExtraBytes             (Bit 7)", tab, false, poly.HasExtraBytes.ToString()));
         info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°"));
         info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°"));
         info.AppendLine(FillWithSpace("RawBitStreamLengthBytes (" + poly.RawBitStreamLengthBytes.Length.ToString() + " Byte)", tab, false, HexString(poly.RawBitStreamLengthBytes)));
         info.AppendLine(FillWithSpace("   BitStreamLength", tab, false, poly.BitstreamLength.ToString()));
         info.AppendLine(FillWithSpace("BitstreamInfo    (1 Byte)", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo)));
         info.AppendLine(FillWithSpace("   basebits4lat", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo >> 4)));
         info.AppendLine(FillWithSpace("   basebits4lon", tab, false, DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF)));
         if (poly.HasLabel) {
            info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(poly.LabelOffsetInLBL)));
            GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
            if (lbl != null)
               info.AppendLine(FillWithSpace("   Text (from LBL)", tab, true, lbl.GetText(poly.LabelOffsetInLBL, true) + "'"));
         }
         if (poly.HasExtraBytes) {
            int len = 7 + poly.RawBitStreamLengthBytes.Length;
            if (poly.HasLabel)
               len += 3;
            filedata.BinaryReader.Seek(block.Offset + (uint)len);
            info.AppendLine(FillWithSpace("ExtraBytes", tab, false, HexString(filedata.BinaryReader.ReadBytes((int)block.Length - len))));
         }
         info.AppendLine("Deltas:");
         List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
         for (int i = 0; i < points.Count; i++)
            info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());
      }

      static void ShowExtPointInfo(StringBuilder info,
                                  AllSubdivInfo asi,
                                  GarminCore.Files.StdFile_RGN rgn,
                                  TreeViewData tvd,
                                  NodeContent.Content4File filedata,
                                  int pointidx,
                                  int tab,
                                  out int hexlen,
                                  out long firsthexadr) {
         GarminCore.Files.StdFile_RGN.ExtRawPointData point = asi.GetExtPoint(pointidx);
         GarminCore.DataBlock block = asi.GetDataBlock4ExtPoint(pointidx);

         firsthexadr = rgn.ExtLinesBlock.Offset;
         firsthexadr += block.Offset;
         hexlen = (int)block.Length;

         info.AppendLine(FillWithSpace("Type             (1 Byte) (Bit 0..6)", tab, false, DecimalAndHexAndBinary(point.Type)));
         info.AppendLine(FillWithSpace("Subtype          (1 Byte) (Bit 0..4)", tab, false, DecimalAndHexAndBinary(point.Subtype)));
         info.AppendLine(FillWithSpace("HasLabel                  (Bit 5)", tab, false, point.HasLabel.ToString()));
         info.AppendLine(FillWithSpace("HasUnknownFlag            (Bit 6)", tab, false, point.HasUnknownFlag.ToString()));
         info.AppendLine(FillWithSpace("HasExtraBytes             (Bit 7)", tab, false, point.HasExtraBytes.ToString()));
         info.AppendLine(FillWithSpace("DeltaLongitude   (2 Byte)", tab, false, DecimalAndHexAndBinary((short)point.RawDeltaLongitude.Value) + ", " + point.RawDeltaLongitude.ValueDegree.ToString() + "°"));
         info.AppendLine(FillWithSpace("DeltaLatitude    (2 Byte)", tab, false, DecimalAndHexAndBinary((short)point.RawDeltaLatitude.Value) + ", " + point.RawDeltaLatitude.ValueDegree.ToString() + "°"));
         if (point.HasLabel) {
            info.AppendLine(FillWithSpace("LabelOffsetInLBL (3 Byte) (Bit 0..21)", tab, false, DecimalAndHexAndBinary(point.LabelOffsetInLBL)));
            GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
            if (lbl != null)
               info.AppendLine(FillWithSpace("   Text (from LBL)", tab, true, lbl.GetText(point.LabelOffsetInLBL, true)));
         }
         if (point.HasExtraBytes) {
            int len = 6;
            if (point.HasLabel)
               len += 3;
            filedata.BinaryReader.Seek(firsthexadr + (uint)len);
            info.AppendLine(FillWithSpace("ExtraBytes", tab, false, HexString(filedata.BinaryReader.ReadBytes(point.ExtraBytes))));
         }
         if (point.HasUnknownFlag) {
            info.AppendLine(FillWithSpace("UnknownKey", tab, false, HexString(point.UnknownKey)));
            if (point.UnknownBytes != null && point.UnknownBytes.Length > 0)
               info.AppendLine(FillWithSpace("UnknownBytes", tab, false, HexString(point.UnknownBytes)));
         }
      }

   }
}
