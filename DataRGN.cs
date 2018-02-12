using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataRGN : Data {

      public const int RGN_IDX_POINT = 0x1 << 30;
      public const int RGN_IDX_IDXPOINT = 0x1 << 29;
      public const int RGN_IDX_LINE = 0x1 << 28;
      public const int RGN_IDX_AREA = 0x1 << 27;
      public const int RGN_IDX_EXTPOINT = 0x1 << 26;
      public const int RGN_IDX_EXTLINE = 0x1 << 25;
      public const int RGN_IDX_EXTAREA = 0x1 << 24;
      public const int RGN_IDX_MASK = 0xFFFFFF;

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
            AppendNode(tn, NodeContent.NodeType.RGN_ExtAreasBlock, rgn, "ExtAreasBlock");
         if (rgn.ExtLinesBlock != null && rgn.ExtLinesBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.RGN_ExtLinesBlock, rgn, "ExtLinesBlock");
         if (rgn.ExtPointsBlock != null && rgn.ExtPointsBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.RGN_ExtPointsBlock, rgn, "ExtPointsBlock");
         if (rgn.UnknownBlock_0x71 != null && rgn.UnknownBlock_0x71.Length > 0)
            AppendNode(tn, NodeContent.NodeType.RGN_UnknownBlock_0x71, rgn, "UnknownBlock 0x71");
      }

      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_RGN rgn, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.RGN_SubdivContentBlock:
               for (int i = 0; i < rgn.SubdivList.Count; i++) {
                  TreeNode tn2 = AppendNode(tn, NodeContent.NodeType.Index, i, "SubdivContent " + i.ToString());

                  //GarminCore.Files.StdFile_TRE.SubdivInfoBasic subdivfinfo = rgn.TREFile.SubdivInfoList[i];
                  GarminCore.Files.StdFile_RGN.SubdivData subdivdata = rgn.SubdivList[i];

                  for (int j = 0; j < subdivdata.PointList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_POINT, "Point " + j.ToString());
                  for (int j = 0; j < subdivdata.IdxPointList.Count; j++)
                     AppendNode(tn2, NodeContent.NodeType.Index, j | RGN_IDX_IDXPOINT, "IdxPoint " + j.ToString());
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
         info.AppendLine("SubdivContent:     " + rgn.SubdivContentBlock.ToString());
         if (rgn.Headerlength > 0x1D) {
            info.AppendLine("ExtAreas:          " + rgn.ExtAreasBlock.ToString());
            info.AppendLine("Unknown_0x25:      " + HexString(rgn.Unknown_0x25));
            info.AppendLine("ExtLines:          " + rgn.ExtLinesBlock.ToString());
            info.AppendLine("Unknown_0x41:      " + HexString(rgn.Unknown_0x41));
            info.AppendLine("ExtPoints:         " + rgn.ExtPointsBlock.ToString());
            info.AppendLine("Unknown_0x5D:      " + HexString(rgn.Unknown_0x5D));
         }
         if (rgn.Headerlength >= 0x71)
            info.AppendLine("UnknownBlock 0x71: " + rgn.UnknownBlock_0x71.ToString());
         if (rgn.Headerlength >= 0x79)
            info.AppendLine("Unknown_0x79:      " + HexString(rgn.Unknown_0x79));
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
                  info.AppendLine("SubdivContent: " + rgn.SubdivList.Count.ToString());
                  info.AppendLine("  Block:       " + rgn.SubdivContentBlock.ToString());
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



               }
               break;

            case NodeContent.NodeType.RGN_ExtLinesBlock:
               firsthexadr = rgn.ExtLinesBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExtLines");
                  info.AppendLine("  Block:  " + rgn.ExtLinesBlock.ToString());
                  hexlen = (int)rgn.ExtLinesBlock.Length;
               } else {

               }
               break;

            case NodeContent.NodeType.RGN_ExtPointsBlock:
               firsthexadr = rgn.ExtPointsBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExtPoints");
                  info.AppendLine("  Block:  " + rgn.ExtPointsBlock.ToString());
                  hexlen = (int)rgn.ExtPointsBlock.Length;
               } else {

               }
               break;

            case NodeContent.NodeType.RGN_UnknownBlock_0x71:
               firsthexadr = rgn.UnknownBlock_0x71.Offset;
               if (idx < 0) {
                  info.AppendLine("UnknownBlock 0x71");
                  info.AppendLine("  Block:       " + rgn.UnknownBlock_0x71.ToString());
                  hexlen = (int)rgn.UnknownBlock_0x71.Length;
               }
               break;

            case NodeContent.NodeType.Index:
               // kommt von der Subdiv-Liste
               // etwas tricky, aber nc.Data des übergeordneten Knotens ist der Subdiv-Index
               if (tvd.TreeView.SelectedNode != null) {
                  int subdividx = -1;
                  NodeContent nc = NodeContent4TreeNode(tvd.TreeView.SelectedNode.Parent);
                  if (nc.Type == NodeContent.NodeType.Index)
                     subdividx = (int)nc.Data;
                  if (subdividx >= 0)
                     asi = new AllSubdivInfo(rgn, filedata.BinaryReader, subdividx);
               }

               if (asi != null) {
                  if ((idx & RGN_IDX_POINT) != 0) {
                     idx &= RGN_IDX_MASK;
                     GarminCore.Files.StdFile_RGN.RawPointData point = asi.GetPoint(idx);

                     info.AppendLine("Type           (1 Byte):             " + DecimalAndHexAndBinary(point.Type));
                     info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(point.LabelOffset));
                     if (!point.IsPoiOffset) {
                        GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                        if (lbl != null)
                           info.AppendLine("   Text (from LBL):                 '" + lbl.GetText(point.LabelOffset, true) + "'");
                     }
                     info.AppendLine("HasSubtype              (Bit 23):    " + point.HasSubtype.ToString());
                     info.AppendLine("IsPoiOffset             (Bit 22):    " + point.IsPoiOffset.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)point.RawDeltaLongitude.Value) + ", " + point.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)point.RawDeltaLatitude.Value) + ", " + point.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     if (point.HasSubtype)
                        info.AppendLine("Subtype        (1 Byte):             " + DecimalAndHexAndBinary(point.Subtype));

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_IDXPOINT) != 0) {
                     idx &= RGN_IDX_MASK;
                     GarminCore.Files.StdFile_RGN.RawPointData point = asi.GetIdxPoint(idx);

                     info.AppendLine("Type           (1 Byte):             " + DecimalAndHexAndBinary(point.Type));
                     info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(point.LabelOffset));
                     if (!point.IsPoiOffset) {
                        GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                        if (lbl != null)
                           info.AppendLine("   Text (from LBL):                 '" + lbl.GetText(point.LabelOffset, true) + "'");
                     }
                     info.AppendLine("HasSubtype              (Bit 23):    " + point.HasSubtype.ToString());
                     info.AppendLine("IsPoiOffset             (Bit 22):    " + point.IsPoiOffset.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)point.RawDeltaLongitude.Value) + ", " + point.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)point.RawDeltaLatitude.Value) + ", " + point.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     if (point.HasSubtype)
                        info.AppendLine("Subtype        (1 Byte):             " + DecimalAndHexAndBinary(point.Subtype));

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_LINE) != 0) {
                     idx &= RGN_IDX_MASK;
                     GarminCore.Files.StdFile_RGN.RawPolyData poly = asi.GetLine(idx);

                     info.AppendLine("Type           (1 Byte) (Bit 0..5):  " + DecimalAndHexAndBinary(poly.Type));
                     info.AppendLine("TwoByteLength           (Bit 7):     " + poly.TwoByteLength.ToString());
                     if (!poly.IsPolygon)
                        info.AppendLine("Direction               (Bit 6):     " + poly.DirectionIndicator.ToString());
                     info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(poly.LabelOffset));
                     info.AppendLine("LabelInNET              (Bit 23):    " + poly.LabelInNET.ToString());
                     info.AppendLine("WithExtraBit            (Bit 22):    " + poly.WithExtraBit.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     info.AppendLine("BitstreamInfo  (1 Byte):             " + DecimalAndHexAndBinary(poly.bitstreamInfo));
                     info.AppendLine("   basebits4lat:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo >> 4));
                     info.AppendLine("   basebits4lon:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF));
                     info.AppendLine("BitstreamLength (" + (poly.TwoByteLength ? "2" : "1") + " Byte):            " + DecimalAndHexAndBinary(poly.bitstream.Length));
                     if (poly.WithExtraBit)
                        info.AppendLine("ExtraBitList:                        " + AllSubdivInfo.ExtraBits(poly));
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

                     GarminCore.Files.StdFile_RGN.RawPolyData poly = asi.GetArea(idx);

                     info.AppendLine("Type           (1 Byte) (Bit 0..6):  " + DecimalAndHexAndBinary(poly.Type));
                     info.AppendLine("TwoByteLength           (Bit 7):     " + poly.TwoByteLength.ToString());
                     info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(poly.LabelOffset));
                     info.AppendLine("WithExtraBit            (Bit 22):    " + poly.WithExtraBit.ToString());
                     info.AppendLine("LabelInNET              (Bit 23):    " + poly.LabelInNET.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     info.AppendLine("BitstreamInfo  (1 Byte):             " + DecimalAndHexAndBinary(poly.bitstreamInfo));
                     info.AppendLine("   basebits4lat:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo >> 4));
                     info.AppendLine("   basebits4lon:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF));
                     info.AppendLine("BitstreamLength (" + (poly.TwoByteLength ? "2" : "1") + " Byte):            " + DecimalAndHexAndBinary(poly.bitstream.Length));
                     if (poly.WithExtraBit)
                        info.AppendLine("ExtraBitList:                        " + AllSubdivInfo.ExtraBits(poly));
                     info.AppendLine("Deltas:");
                     List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
                     for (int i = 0; i < points.Count; i++)
                        info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());

                     GarminCore.DataBlock block = asi.GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area, idx);
                     firsthexadr = rgn.SubdivContentBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                  } else if ((idx & RGN_IDX_EXTPOINT) != 0) {
                     idx &= RGN_IDX_MASK;

                     GarminCore.Files.StdFile_RGN.ExtRawPointData point = asi.GetExtPoint(idx);
                     GarminCore.DataBlock block = asi.GetDataBlock4ExtPoint(idx);

                     firsthexadr = rgn.ExtLinesBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                     info.AppendLine("Type           (1 Byte) (Bit 0..6):  " + DecimalAndHexAndBinary(point.Type));
                     info.AppendLine("Subtype        (1 Byte) (Bit 0..4):  " + DecimalAndHexAndBinary(point.Subtype));
                     info.AppendLine("HasLabel                (Bit 5):     " + point.HasLabel.ToString());
                     info.AppendLine("HasUnknownFlag          (Bit 6):     " + point.HasUnknownFlag.ToString());
                     info.AppendLine("HasExtraBytes           (Bit 7):     " + point.HasExtraBytes.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)point.RawDeltaLongitude.Value) + ", " + point.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)point.RawDeltaLatitude.Value) + ", " + point.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     if (point.HasLabel) {
                        info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(point.LabelOffset));
                        GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                        if (lbl != null)
                           info.AppendLine("   Text (from LBL):                  '" + lbl.GetText(point.LabelOffset, true) + "'");
                     }
                     if (point.HasExtraBytes) {
                        int len = 6;
                        if (point.HasLabel)
                           len += 3;
                        filedata.BinaryReader.Seek(firsthexadr + (uint)len);
                        info.AppendLine("ExtraBytes:                          " + HexString(filedata.BinaryReader.ReadBytes(point.ExtraBytes)));
                     }
                     if (point.HasUnknownFlag) {
                        info.AppendLine("UnknownKey:                          " + HexString(point.UnknownKey));
                        if (point.UnknownBytes != null && point.UnknownBytes.Length > 0)
                           info.AppendLine("UnknownBytes:                        " + HexString(point.UnknownBytes));
                     }

                  } else if ((idx & RGN_IDX_EXTLINE) != 0) {
                     idx &= RGN_IDX_MASK;

                     GarminCore.Files.StdFile_RGN.ExtRawPolyData poly = asi.GetExtLine(idx);
                     GarminCore.DataBlock block = asi.GetDataBlock4ExtLine(idx);

                     firsthexadr = rgn.ExtLinesBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                     info.AppendLine("Type           (1 Byte) (Bit 0..6):  " + DecimalAndHexAndBinary(poly.Type));
                     info.AppendLine("Subtype        (1 Byte) (Bit 0..4):  " + DecimalAndHexAndBinary(poly.Subtype));
                     info.AppendLine("HasLabel                (Bit 5):     " + poly.HasLabel.ToString());
                     info.AppendLine("HasUnknownFlag          (Bit 6):     " + poly.HasUnknownFlag.ToString());
                     info.AppendLine("HasExtraBytes           (Bit 7):     " + poly.HasExtraBytes.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     info.AppendLine("RawBitStreamLengthBytes (" + poly.RawBitStreamLengthBytes.Length.ToString() + " Byte):    " + HexString(poly.RawBitStreamLengthBytes));
                     info.AppendLine("   BitStreamLength:                  " + poly.BitstreamLength.ToString());
                     info.AppendLine("BitstreamInfo  (1 Byte):             " + DecimalAndHexAndBinary(poly.bitstreamInfo));
                     info.AppendLine("   basebits4lat:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo >> 4));
                     info.AppendLine("   basebits4lon:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF));
                     if (poly.HasLabel) {
                        info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(poly.LabelOffset));
                        GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                        if (lbl != null)
                           info.AppendLine("   Text (from LBL):                  '" + lbl.GetText(poly.LabelOffset, true) + "'");
                     }
                     if (poly.HasExtraBytes) {
                        int len = 7 + poly.RawBitStreamLengthBytes.Length;
                        if (poly.HasLabel)
                           len += 3;
                        filedata.BinaryReader.Seek(block.Offset + (uint)len);
                        info.AppendLine("ExtraBytes:                          " + HexString(filedata.BinaryReader.ReadBytes((int)block.Length - len)));
                     }
                     info.AppendLine("Deltas:");
                     List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
                     for (int i = 0; i < points.Count; i++)
                        info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());

                  } else if ((idx & RGN_IDX_EXTAREA) != 0) {
                     idx &= RGN_IDX_MASK;

                     GarminCore.Files.StdFile_RGN.ExtRawPolyData poly = asi.GetExtArea(idx);
                     GarminCore.DataBlock block = asi.GetDataBlock4ExtArea(idx);

                     firsthexadr = rgn.ExtAreasBlock.Offset;
                     firsthexadr += block.Offset;
                     hexlen = (int)block.Length;

                     info.AppendLine("Type           (1 Byte) (Bit 0..6):  " + DecimalAndHexAndBinary(poly.Type));
                     info.AppendLine("Subtype        (1 Byte) (Bit 0..4):  " + DecimalAndHexAndBinary(poly.Subtype));
                     info.AppendLine("HasLabel                (Bit 5):     " + poly.HasLabel.ToString());
                     info.AppendLine("HasUnknownFlag          (Bit 6):     " + poly.HasUnknownFlag.ToString());
                     info.AppendLine("HasExtraBytes           (Bit 7):     " + poly.HasExtraBytes.ToString());
                     info.AppendLine("DeltaLongitude (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLongitude.Value) + ", " + poly.RawDeltaLongitude.ValueDegree.ToString() + "°");
                     info.AppendLine("DeltaLatitude  (2 Byte):             " + DecimalAndHexAndBinary((short)poly.RawDeltaLatitude.Value) + ", " + poly.RawDeltaLatitude.ValueDegree.ToString() + "°");
                     info.AppendLine("RawBitStreamLengthBytes (" + poly.RawBitStreamLengthBytes.Length.ToString() + " Byte):    " + HexString(poly.RawBitStreamLengthBytes));
                     info.AppendLine("   BitStreamLength:                  " + poly.BitstreamLength.ToString());
                     info.AppendLine("BitstreamInfo  (1 Byte):             " + DecimalAndHexAndBinary(poly.bitstreamInfo));
                     info.AppendLine("   basebits4lat:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo >> 4));
                     info.AppendLine("   basebits4lon:                     " + DecimalAndHexAndBinary(poly.bitstreamInfo & 0xF));
                     if (poly.HasLabel) {
                        info.AppendLine("LabelOffset    (3 Byte) (Bit 0..21): " + DecimalAndHexAndBinary(poly.LabelOffset));
                        GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
                        if (lbl != null)
                           info.AppendLine("   Text (from LBL):                  '" + lbl.GetText(poly.LabelOffset, true) + "'");
                     }
                     if (poly.HasExtraBytes) {
                        int len = 7 + poly.RawBitStreamLengthBytes.Length;
                        if (poly.HasLabel)
                           len += 3;
                        filedata.BinaryReader.Seek(block.Offset + (uint)len);
                        info.AppendLine("ExtraBytes:                          " + HexString(filedata.BinaryReader.ReadBytes((int)block.Length - len)));
                     }
                     info.AppendLine("Deltas:");
                     List<GarminCore.Files.StdFile_RGN.GeoDataBitstream.RawPoint> points = poly.GetRawPoints();
                     for (int i = 0; i < points.Count; i++)
                        info.AppendLine("   " + points[i].ToString() + " / " + points[0].GetMapUnitPoint(rgn.TREFile.MaplevelList[asi.MaplevelNo].CoordBits, asi.SubdivfInfo.Center).ToString());

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
         if (asi.SubdivData.PointList.Count > 0) {
            info.AppendLine("PointList:       " + asi.SubdivData.PointList.Count.ToString());
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi);
            if (offset >= 0)
               info.AppendLine("   Offset:       " + DecimalAndHexAndBinary(offset));
         }
         if (asi.SubdivData.IdxPointList.Count > 0) {
            info.AppendLine("IdxPointList:    " + asi.SubdivData.IdxPointList.Count.ToString());
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi);
            if (offset >= 0)
               info.AppendLine("   Offset:       " + DecimalAndHexAndBinary(offset));
         }
         if (asi.SubdivData.LineList.Count > 0) {
            info.AppendLine("LineList:        " + asi.SubdivData.LineList.Count.ToString());
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.line);
            if (offset >= 0)
               info.AppendLine("   Offset:       " + DecimalAndHexAndBinary(offset));
         }
         if (asi.SubdivData.AreaList.Count > 0) {
            info.AppendLine("AreaList:        " + asi.SubdivData.AreaList.Count.ToString());
            offset = asi.GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area);
            if (offset >= 0)
               info.AppendLine("   Offset:       " + DecimalAndHexAndBinary(offset));
         }

         info.AppendLine();
         if (asi.SubdivData.ExtPointList.Count > 0) {
            info.AppendLine("ExtPointList:    " + asi.SubdivData.ExtPointList.Count.ToString());
            info.AppendLine("   Block (from TRE): " + asi.GetExtPointBlock());
         }
         if (asi.SubdivData.ExtLineList.Count > 0) {
            info.AppendLine("ExtLineList:     " + asi.SubdivData.ExtLineList.Count.ToString());
            info.AppendLine("   Block (from TRE): " + asi.GetExtLineBlock());
         }
         if (asi.SubdivData.ExtAreaList.Count > 0) {
            info.AppendLine("ExtAreaList:     " + asi.SubdivData.ExtAreaList.Count.ToString());
            info.AppendLine("   Block (from TRE): " + asi.GetExtAreaBlock());
         }

         return asi.GetSubdivDataBlock();
      }

   }
}
