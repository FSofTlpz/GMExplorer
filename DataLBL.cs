using System;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataLBL : Data {

      /// <summary>
      /// Knoten für eine logische oder physische LBL-Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_LBL lbl, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, lbl, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, lbl, "Garmin LBL-Header"));

         if (lbl.PostHeaderDataBlock != null && lbl.PostHeaderDataBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_PostHeaderData, lbl, "PostHeaderData"));
         if (lbl.TextBlock != null && lbl.TextBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_TextBlock, lbl, "TextBlock"));
         if (lbl.CountryBlock != null && lbl.CountryBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_CountryBlock, lbl, "CountryBlock"));
         if (lbl.RegionBlock != null && lbl.RegionBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_RegionBlock, lbl, "RegionBlock"));
         if (lbl.CityBlock != null && lbl.CityBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_CityBlock, lbl, "CityBlock"));
         if (lbl.POIPropertiesBlock != null && lbl.POIPropertiesBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_PointPropertiesBlock, lbl, "PointPropertiesBlock"));
         if (lbl.POIIndexBlock != null && lbl.POIIndexBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_PointIndexList4RGN, lbl, "PointIndexList4RGN"));
         if (lbl.POITypeIndexBlock != null && lbl.POITypeIndexBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_PointTypeIndexList4RGN, lbl, "POITypeIndexBlock"));
         if (lbl.ZipBlock != null && lbl.ZipBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_ZipBlock, lbl, "ZipBlock"));
         if (lbl.HighwayWithExitBlock != null && lbl.HighwayWithExitBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_HighwayWithExitBlock, lbl, "HighwayWithExitBlock"));
         if (lbl.ExitBlock != null && lbl.ExitBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_ExitBlock, lbl, "ExitBlock"));
         if (lbl.HighwayExitBlock != null && lbl.HighwayExitBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_HighwayExitBlock, lbl, "HighwayExitBlock"));
         if (lbl.SortDescriptorDefBlock != null && lbl.SortDescriptorDefBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_SortDescriptorDefBlock, lbl, "SortDescriptorDefBlock");
         if (lbl.Lbl13Block != null && lbl.Lbl13Block.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_Lbl13Block, lbl, "Lbl13Block");
         if (lbl.TidePredictionBlock != null && lbl.TidePredictionBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_TidePredictionBlock, lbl, "TidePredictionBlock");
         if (lbl.UnknownBlock_0xD0 != null && lbl.UnknownBlock_0xD0.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0xD0, lbl, "UnknownBlock_0xD0");
         if (lbl.UnknownBlock_0xDE != null && lbl.UnknownBlock_0xDE.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0xDE, lbl, "UnknownBlock_0xDE");
         if (lbl.UnknownBlock_0xEC != null && lbl.UnknownBlock_0xEC.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0xEC, lbl, "UnknownBlock_0xEC");
         if (lbl.UnknownBlock_0xFA != null && lbl.UnknownBlock_0xFA.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0xFA, lbl, "UnknownBlock_0xFA");
         if (lbl.UnknownBlock_0x108 != null && lbl.UnknownBlock_0x108.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x108, lbl, "UnknownBlock_0x108");
         if (lbl.UnknownBlock_0x116 != null && lbl.UnknownBlock_0x116.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x116, lbl, "UnknownBlock_0x116");
         if (lbl.UnknownBlock_0x124 != null && lbl.UnknownBlock_0x124.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x124, lbl, "UnknownBlock_0x124");
         if (lbl.UnknownBlock_0x132 != null && lbl.UnknownBlock_0x132.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x132, lbl, "UnknownBlock_0x132");
         if (lbl.UnknownBlock_0x140 != null && lbl.UnknownBlock_0x140.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x140, lbl, "UnknownBlock_0x140");
         if (lbl.UnknownBlock_0x14E != null && lbl.UnknownBlock_0x14E.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x14E, lbl, "UnknownBlock_0x14E");
         if (lbl.UnknownBlock_0x15A != null && lbl.UnknownBlock_0x15A.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x15A, lbl, "UnknownBlock_0x15A");
         if (lbl.UnknownBlock_0x168 != null && lbl.UnknownBlock_0x168.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x168, lbl, "UnknownBlock_0x168");
         if (lbl.UnknownBlock_0x176 != null && lbl.UnknownBlock_0x176.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x176, lbl, "UnknownBlock_0x176");
         if (lbl.UnknownBlock_0x184 != null && lbl.UnknownBlock_0x184.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x184, lbl, "UnknownBlock_0x184");
         if (lbl.UnknownBlock_0x192 != null && lbl.UnknownBlock_0x192.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x192, lbl, "UnknownBlock_0x192");
         if (lbl.UnknownBlock_0x19A != null && lbl.UnknownBlock_0x19A.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x19A, lbl, "UnknownBlock_0x19A");
         if (lbl.UnknownBlock_0x1A6 != null && lbl.UnknownBlock_0x1A6.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1A6, lbl, "UnknownBlock_0x1A6");
         if (lbl.UnknownBlock_0x1B2 != null && lbl.UnknownBlock_0x1B2.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1B2, lbl, "UnknownBlock_0x1B2");
         if (lbl.UnknownBlock_0x1BE != null && lbl.UnknownBlock_0x1BE.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1BE, lbl, "UnknownBlock_0x1BE");
         if (lbl.UnknownBlock_0x1CA != null && lbl.UnknownBlock_0x1CA.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1CA, lbl, "UnknownBlock_0x1CA");
         if (lbl.UnknownBlock_0x1D8 != null && lbl.UnknownBlock_0x1D8.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1D8, lbl, "UnknownBlock_0x1D8");
         if (lbl.UnknownBlock_0x1E6 != null && lbl.UnknownBlock_0x1E6.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1E6, lbl, "UnknownBlock_0x1E6");
         if (lbl.UnknownBlock_0x1F2 != null && lbl.UnknownBlock_0x1F2.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x1F2, lbl, "UnknownBlock_0x1F2");
         if (lbl.UnknownBlock_0x200 != null && lbl.UnknownBlock_0x200.Length > 0)
            AppendNode(tn, NodeContent.NodeType.LBL_UnknownBlock_0x200, lbl, "UnknownBlock_0x200");
      }

      /// <summary>
      /// Knoten für eine LBL-Section anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="lbl"></param>
      /// <param name="nodetype"></param>
      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_LBL lbl, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);

         switch (nodetype) {
            case NodeContent.NodeType.LBL_TextBlock:
               int[] offset = lbl.TextList.Offsets();
               for (int i = 0; i < offset.Length; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Offset " + DecimalAndHexAndBinary(offset[i]));
               break;

            case NodeContent.NodeType.LBL_CountryBlock:
               for (int i = 0; i < lbl.CountryDataList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Country " + i.ToString());
               break;
            case NodeContent.NodeType.LBL_RegionBlock:
               for (int i = 0; i < lbl.RegionAndCountryDataList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "RegionAndCountry " + i.ToString());
               break;
            case NodeContent.NodeType.LBL_CityBlock:
               for (int i = 0; i < lbl.CityAndRegionOrCountryDataList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "CityAndRegionOrCountry " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_PointIndexList4RGN:
               for (int i = 0; i < lbl.PointIndexList4RGN.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "PoiIndex " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_PointPropertiesBlock:
               for (int i = 0; i < lbl.PointPropertiesList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "POIProperties " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_PointTypeIndexList4RGN:
               for (int i = 0; i < lbl.PointTypeIndexList4RGN.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "PoiTypeIndex " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_ZipBlock:
               for (int i = 0; i < lbl.ZipDataList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Zip " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_HighwayWithExitBlock:
               for (int i = 0; i < lbl.HighwayWithExitList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "HighwayWithExit " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_ExitBlock:
               for (int i = 0; i < lbl.ExitList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "Exit " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_HighwayExitBlock:
               for (int i = 0; i < lbl.HighwayExitDefList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "HighwayExitDef " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_SortDescriptorDefBlock: break;
            case NodeContent.NodeType.LBL_Lbl13Block: break;
            case NodeContent.NodeType.LBL_TidePredictionBlock: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xD0: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xDE: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xEC: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xFA: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x108: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x116: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x124: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x132: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x140: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x14E: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x15A: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x168: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x176: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x184: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x192: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x19A: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1A6: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1B2: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1BE: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1CA: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1D8: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1E6: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1F2: break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x200: break;
            case NodeContent.NodeType.LBL_PostHeaderData: break;
         }
      }

      /// <summary>
      /// Knoten für einen LBL-Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_LBL lbl) {
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.TextBlock, 0x15, "TextBlock: " + lbl.TextBlock.ToString()), "TextBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.DataOffsetMultiplier, 0x1d, "DataOffsetMultiplier: " + DecimalAndHexAndBinary(lbl.DataOffsetMultiplier) + " -> " + (0x1 << lbl.DataOffsetMultiplier)), "DataOffsetMultiplier");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.EncodingType, 0x1e, "EncodingType: " + DecimalAndHexAndBinary(lbl.EncodingType)), "EncodingType");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.CountryBlock, 0x1f, "CountryBlock: " + lbl.CountryBlock.ToString()), "CountryBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x29, 0, lbl.Unknown_0x29.Length, NodeContent.Content4DataRange.DataType.Other, 0x29, "Unknown 0x29"), "Unknown 0x29");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.RegionBlock, 0x2d, "RegionBlock: " + lbl.RegionBlock.ToString()), "RegionBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x37, 0, lbl.Unknown_0x37.Length, NodeContent.Content4DataRange.DataType.Other, 0x37, "Unknown 0x37"), "Unknown 0x37");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.CityBlock, 0x3b, "CityBlock: " + lbl.CityBlock.ToString()), "CityBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x45, 0, lbl.Unknown_0x45.Length, NodeContent.Content4DataRange.DataType.Other, 0x45, "Unknown 0x45"), "Unknown 0x45");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.POIIndexBlock, 0x49, "POIIndexBlock: " + lbl.POIIndexBlock.ToString()), "POIIndexBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x53, 0, lbl.Unknown_0x53.Length, NodeContent.Content4DataRange.DataType.Other, 0x53, "Unknown 0x53"), "Unknown 0x53");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.POIPropertiesBlock, 0x57, "POIPropertiesBlock: " + lbl.POIPropertiesBlock.ToString()), "POIPropertiesBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.POIOffsetMultiplier, 0x5f, "POIOffsetMultiplier: " + DecimalAndHexAndBinary(lbl.POIOffsetMultiplier) + " -> " + (0x1 << lbl.POIOffsetMultiplier)), "POIOffsetMultiplier");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange((byte)lbl.POIGlobalMask, 0x60, "POIGlobalMask: " + DecimalAndHexAndBinary((byte)lbl.POIGlobalMask) + Environment.NewLine +
                                                                                                                        "   has_street_num (Bit 0): " + lbl.POIGlobal_has_street_num.ToString() + Environment.NewLine +
                                                                                                                        "   has_street     (Bit 1): " + lbl.POIGlobal_has_street.ToString() + Environment.NewLine +
                                                                                                                        "   has_city       (Bit 2): " + lbl.POIGlobal_has_city.ToString() + Environment.NewLine +
                                                                                                                        "   has_zip        (Bit 3): " + lbl.POIGlobal_has_zip.ToString() + Environment.NewLine +
                                                                                                                        "   has_phone      (Bit 4): " + lbl.POIGlobal_has_phone.ToString() + Environment.NewLine +
                                                                                                                        "   has_exit       (Bit 5): " + lbl.POIGlobal_has_exit.ToString() + Environment.NewLine +
                                                                                                                        "   has_tide_pred  (Bit 6): " + lbl.POIGlobal_has_tide_prediction.ToString() + Environment.NewLine +
                                                                                                                        "   unknown        (Bit 7): " + lbl.POIGlobal_has_unknown.ToString()), "POIGlobalMask");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x61, 0, lbl.Unknown_0x61.Length, NodeContent.Content4DataRange.DataType.Other, 0x61, "Unknown 0x61"), "Unknown 0x61");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.POITypeIndexBlock, 0x64, "POITypeIndexBlock: " + lbl.POITypeIndexBlock.ToString()), "POITypeIndexBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x6E, 0, lbl.Unknown_0x6E.Length, NodeContent.Content4DataRange.DataType.Other, 0x6e, "Unknown 0x6e"), "Unknown 0x6e");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.ZipBlock, 0x72, "ZipBlock: " + lbl.ZipBlock.ToString()), "ZipBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x7C, 0, lbl.Unknown_0x7C.Length, NodeContent.Content4DataRange.DataType.Other, 0x7c, "Unknown 0x7C"), "Unknown 0x7C");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.HighwayWithExitBlock, 0x80, "HighwayWithExitBlock: " + lbl.HighwayWithExitBlock.ToString()), "HighwayWithExitBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x8A, 0, lbl.Unknown_0x8A.Length, NodeContent.Content4DataRange.DataType.Other, 0x8a, "Unknown 0x8A"), "Unknown 0x8A");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.ExitBlock, 0x8e, "ExitBlock: " + lbl.ExitBlock.ToString()), "ExitBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x98, 0, lbl.Unknown_0x98.Length, NodeContent.Content4DataRange.DataType.Other, 0x98, "Unknown 0x98"), "Unknown 0x98");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.HighwayExitBlock, 0x9c, "HighwayExitBlock: " + lbl.HighwayExitBlock.ToString()), "HighwayExitBlock");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0xA6, 0, lbl.Unknown_0xA6.Length, NodeContent.Content4DataRange.DataType.Other, 0xa6, "Unknown 0xA6"), "Unknown 0xA6");
         if (lbl.Headerlength >= 0xAA) {
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Codepage, 0xAA, "Codepage: " + DecimalAndHexAndBinary(lbl.Codepage)), "Codepage");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.ID1, 0xAC, "ID1: " + DecimalAndHexAndBinary(lbl.ID1)), "ID1");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.ID2, 0xAE, "ID2: " + DecimalAndHexAndBinary(lbl.ID2)), "ID2");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.SortDescriptorDefBlock, 0xB0, "SortDescriptorDefBlock: " + lbl.SortDescriptorDefBlock.ToString()), "SortDescriptorDefBlock");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Lbl13Block, 0xB8, "Lbl13Block: " + lbl.Lbl13Block.ToString()), "Lbl13Block");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0xC2, 0, lbl.Unknown_0xC2.Length, NodeContent.Content4DataRange.DataType.Other, 0xC2, "Unknown 0xC2"), "Unknown 0xC2");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.TidePredictionBlock, 0xC4, "TidePredictionBlock: " + lbl.TidePredictionBlock.ToString()), "TidePredictionBlock");
            if (lbl.Headerlength >= 0xCE)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0xCE, 0, lbl.Unknown_0xCE.Length, NodeContent.Content4DataRange.DataType.Other, 0xCE, "Unknown 0xCE"), "Unknown 0xCE");
            if (lbl.Headerlength >= 0xD0)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0xD0, 0xD0, "UnknownBlock 0xD0: " + lbl.UnknownBlock_0xD0.ToString()), "UnknownBlock 0xD0");
            if (lbl.Headerlength >= 0xD8)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0xD8, 0, lbl.Unknown_0xD8.Length, NodeContent.Content4DataRange.DataType.Other, 0xD8, "Unknown 0xD8"), "Unknown 0xD8");
            if (lbl.Headerlength >= 0xDE)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0xDE, 0xDE, "UnknownBlock 0xDE: " + lbl.UnknownBlock_0xDE.ToString()), "UnknownBlock 0xDE");
            if (lbl.Headerlength >= 0xE6)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0xE6, 0, lbl.Unknown_0xE6.Length, NodeContent.Content4DataRange.DataType.Other, 0xE6, "Unknown 0xE6"), "Unknown 0xE6");
            if (lbl.Headerlength >= 0xEC)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0xEC, 0xEC, "UnknownBlock 0xEC: " + lbl.UnknownBlock_0xEC.ToString()), "UnknownBlock 0xEC");
            if (lbl.Headerlength >= 0xF4)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0xF4, 0, lbl.Unknown_0xF4.Length, NodeContent.Content4DataRange.DataType.Other, 0xF4, "Unknown 0xF4"), "Unknown 0xF4");
            if (lbl.Headerlength >= 0xFA)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0xFA, 0xFA, "UnknownBlock 0xFA: " + lbl.UnknownBlock_0xFA.ToString()), "UnknownBlock 0xFA");
            if (lbl.Headerlength >= 0x102)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x102, 0, lbl.Unknown_0x102.Length, NodeContent.Content4DataRange.DataType.Other, 0x102, "Unknown 0x102"), "Unknown 0x102");
            if (lbl.Headerlength >= 0x108)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x108, 0x108, "UnknownBlock 0x108: " + lbl.UnknownBlock_0x108.ToString()), "UnknownBlock 0x108");
            if (lbl.Headerlength >= 0x110)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x110, 0, lbl.Unknown_0x110.Length, NodeContent.Content4DataRange.DataType.Other, 0x110, "Unknown 0x110"), "Unknown 0x110");
            if (lbl.Headerlength >= 0x116)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x116, 0x116, "UnknownBlock 0x116: " + lbl.UnknownBlock_0x116.ToString()), "UnknownBlock 0x116");
            if (lbl.Headerlength >= 0x11E)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x11E, 0, lbl.Unknown_0x11E.Length, NodeContent.Content4DataRange.DataType.Other, 0x11E, "Unknown 0x11E"), "Unknown 0x11E");
            if (lbl.Headerlength >= 0x124)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x124, 0x124, "UnknownBlock 0x124: " + lbl.UnknownBlock_0x124.ToString()), "UnknownBlock 0x124");
            if (lbl.Headerlength >= 0x12C)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x12C, 0, lbl.Unknown_0x12C.Length, NodeContent.Content4DataRange.DataType.Other, 0x12C, "Unknown 0x12C"), "Unknown 0x12C");
            if (lbl.Headerlength >= 0x132)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x132, 0x132, "UnknownBlock 0x132: " + lbl.UnknownBlock_0x132.ToString()), "UnknownBlock 0x132");
            if (lbl.Headerlength >= 0x13A)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x13A, 0, lbl.Unknown_0x13A.Length, NodeContent.Content4DataRange.DataType.Other, 0x13A, "Unknown 0x13A"), "Unknown 0x13A");
            if (lbl.Headerlength >= 0x140)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x140, 0x140, "UnknownBlock 0x140: " + lbl.UnknownBlock_0x140.ToString()), "UnknownBlock 0x140");
            if (lbl.Headerlength >= 0x148)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x148, 0, lbl.Unknown_0x148.Length, NodeContent.Content4DataRange.DataType.Other, 0x148, "Unknown 0x148"), "Unknown 0x148");
            if (lbl.Headerlength >= 0x14E)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x14E, 0x14E, "UnknownBlock 0x14E: " + lbl.UnknownBlock_0x14E.ToString()), "UnknownBlock 0x14E");
            if (lbl.Headerlength >= 0x156)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x156, 0, lbl.Unknown_0x156.Length, NodeContent.Content4DataRange.DataType.Other, 0x156, "Unknown 0x156"), "Unknown 0x156");
            if (lbl.Headerlength >= 0x15A)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x15A, 0x15A, "UnknownBlock 0x15A: " + lbl.UnknownBlock_0x15A.ToString()), "UnknownBlock 0x15A");
            if (lbl.Headerlength >= 0x162)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x162, 0, lbl.Unknown_0x162.Length, NodeContent.Content4DataRange.DataType.Other, 0x162, "Unknown 0x162"), "Unknown 0x162");
            if (lbl.Headerlength >= 0x168)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x168, 0x168, "UnknownBlock 0x168: " + lbl.UnknownBlock_0x168.ToString()), "UnknownBlock 0x168");
            if (lbl.Headerlength >= 0x170)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x170, 0, lbl.Unknown_0x170.Length, NodeContent.Content4DataRange.DataType.Other, 0x170, "Unknown 0x170"), "Unknown 0x170");
            if (lbl.Headerlength >= 0x176)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x176, 0x176, "UnknownBlock 0x176: " + lbl.UnknownBlock_0x176.ToString()), "UnknownBlock 0x176");
            if (lbl.Headerlength >= 0x17E)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x17E, 0, lbl.Unknown_0x17E.Length, NodeContent.Content4DataRange.DataType.Other, 0x17E, "Unknown 0x17E"), "Unknown 0x17E");
            if (lbl.Headerlength >= 0x184)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x184, 0x184, "UnknownBlock 0x184: " + lbl.UnknownBlock_0x184.ToString()), "UnknownBlock 0x184");
            if (lbl.Headerlength >= 0x18C)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x18C, 0, lbl.Unknown_0x18C.Length, NodeContent.Content4DataRange.DataType.Other, 0x18C, "Unknown 0x18C"), "Unknown 0x18C");
            if (lbl.Headerlength >= 0x192)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x192, 0x192, "UnknownBlock 0x192: " + lbl.UnknownBlock_0x192.ToString()), "UnknownBlock 0x192");
            if (lbl.Headerlength >= 0x19A)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x19A, 0x19A, "UnknownBlock 0x19A: " + lbl.UnknownBlock_0x19A.ToString()), "UnknownBlock 0x19A");
            if (lbl.Headerlength >= 0x1A2)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1A2, 0, lbl.Unknown_0x1A2.Length, NodeContent.Content4DataRange.DataType.Other, 0x1A2, "Unknown 0x1A2"), "Unknown 0x1A2");
            if (lbl.Headerlength >= 0x1A6)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1A6, 0x1A6, "UnknownBlock 0x1A6: " + lbl.UnknownBlock_0x1A6.ToString()), "UnknownBlock 0x1A6");
            if (lbl.Headerlength >= 0x1AE)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1AE, 0, lbl.Unknown_0x1AE.Length, NodeContent.Content4DataRange.DataType.Other, 0x1AE, "Unknown 0x1AE"), "Unknown 0x1AE");
            if (lbl.Headerlength >= 0x1B2)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1B2, 0x1B2, "UnknownBlock 0x1B2: " + lbl.UnknownBlock_0x1B2.ToString()), "UnknownBlock 0x1B2");
            if (lbl.Headerlength >= 0x1BA)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1BA, 0, lbl.Unknown_0x1BA.Length, NodeContent.Content4DataRange.DataType.Other, 0x1BA, "Unknown 0x1BA"), "Unknown 0x1BA");
            if (lbl.Headerlength >= 0x1BE)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1BE, 0x1BE, "UnknownBlock 0x1BE: " + lbl.UnknownBlock_0x1BE.ToString()), "UnknownBlock 0x1BE");
            if (lbl.Headerlength >= 0x1C6)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1C6, 0, lbl.Unknown_0x1C6.Length, NodeContent.Content4DataRange.DataType.Other, 0x1C6, "Unknown 0x1C6"), "Unknown 0x1C6");
            if (lbl.Headerlength >= 0x1CA)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1CA, 0x1CA, "UnknownBlock 0x1CA: " + lbl.UnknownBlock_0x1CA.ToString()), "UnknownBlock 0x1CA");
            if (lbl.Headerlength >= 0x1D2)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1D2, 0, lbl.Unknown_0x1D2.Length, NodeContent.Content4DataRange.DataType.Other, 0x1D2, "Unknown 0x1D2"), "Unknown 0x1D2");
            if (lbl.Headerlength >= 0x1D8)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1D8, 0x1D8, "UnknownBlock 0x1D8: " + lbl.UnknownBlock_0x1D8.ToString()), "UnknownBlock 0x1D8");
            if (lbl.Headerlength >= 0x1E0)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1E0, 0, lbl.Unknown_0x1E0.Length, NodeContent.Content4DataRange.DataType.Other, 0x1E0, "Unknown 0x1E0"), "Unknown 0x1E0");
            if (lbl.Headerlength >= 0x1E6)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1E6, 0x1E6, "UnknownBlock 0x1E6: " + lbl.UnknownBlock_0x1E6.ToString()), "UnknownBlock 0x1E6");
            if (lbl.Headerlength >= 0x1EE)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1EE, 0, lbl.Unknown_0x1EE.Length, NodeContent.Content4DataRange.DataType.Other, 0x1EE, "Unknown 0x1EE"), "Unknown 0x1EE");
            if (lbl.Headerlength >= 0x1F2)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x1F2, 0x1F2, "UnknownBlock 0x1F2: " + lbl.UnknownBlock_0x1F2.ToString()), "UnknownBlock 0x1F2");
            if (lbl.Headerlength >= 0x1FA)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x1FA, 0, lbl.Unknown_0x1FA.Length, NodeContent.Content4DataRange.DataType.Other, 0x1FA, "Unknown 0x1FA"), "Unknown 0x1FA");
            if (lbl.Headerlength >= 0x200)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.UnknownBlock_0x200, 0x200, "UnknownBlock 0x200: " + lbl.UnknownBlock_0x200.ToString()), "UnknownBlock 0x200");
            if (lbl.Headerlength >= 0x208)
               AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(lbl.Unknown_0x208, 0, lbl.Unknown_0x208.Length, NodeContent.Content4DataRange.DataType.Other, 0x208, "Unknown 0x208"), "Unknown 0x208");
         }
      }

      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_LBL lbl) {
         int tab = 28;
         info.AppendLine(FillWithSpace("TextBlock", tab, false, lbl.TextBlock.ToString()));
         info.AppendLine(FillWithSpace("DataOffsetMultiplier", tab, false, DecimalAndHexAndBinary(lbl.DataOffsetMultiplier)));
         info.AppendLine(FillWithSpace("EncodingType", tab, false, DecimalAndHexAndBinary(lbl.EncodingType)));
         info.AppendLine(FillWithSpace("CountryBlock", tab, false, lbl.CountryBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x29", tab, false, HexString(lbl.Unknown_0x29)));
         info.AppendLine(FillWithSpace("RegionBlock", tab, false, lbl.RegionBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x37", tab, false, HexString(lbl.Unknown_0x37)));
         info.AppendLine(FillWithSpace("CityBlock", tab, false, lbl.CityBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x45", tab, false, HexString(lbl.Unknown_0x45)));
         info.AppendLine(FillWithSpace("PoiIndexDataList", tab, false, lbl.POIIndexBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x53", tab, false, HexString(lbl.Unknown_0x53)));
         info.AppendLine(FillWithSpace("POIPropertiesBlock", tab, false, lbl.POIPropertiesBlock.ToString()));
         info.AppendLine(FillWithSpace("POIOffsetMultiplier", tab, false, DecimalAndHexAndBinary(lbl.POIOffsetMultiplier) + " -> " + (0x1 << lbl.POIOffsetMultiplier)));
         info.AppendLine(FillWithSpace("POIGlobalMask", tab, false, DecimalAndHexAndBinary((byte)lbl.POIGlobalMask)));
         info.AppendLine(FillWithSpace("Unknown 0x61", tab, false, HexString(lbl.Unknown_0x61)));
         info.AppendLine(FillWithSpace("POITypeIndexBlock", tab, false, lbl.POITypeIndexBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x6E", tab, false, HexString(lbl.Unknown_0x6E)));
         info.AppendLine(FillWithSpace("ZipBlock", tab, false, lbl.ZipBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x7C", tab, false, HexString(lbl.Unknown_0x7C)));
         info.AppendLine(FillWithSpace("HighwayWithExitBlock", tab, false, lbl.HighwayWithExitBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x8A", tab, false, HexString(lbl.Unknown_0x8A)));
         info.AppendLine(FillWithSpace("ExitBlock", tab, false, lbl.ExitBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0x98", tab, false, HexString(lbl.Unknown_0x98)));
         info.AppendLine(FillWithSpace("HighwayExitBlock", tab, false, lbl.HighwayExitBlock.ToString()));
         info.AppendLine(FillWithSpace("Unknown 0xA6", tab, false, HexString(lbl.Unknown_0xA6)));
         if (lbl.Headerlength >= 0xAA) {
            info.AppendLine(FillWithSpace("Codepage", tab, false, DecimalAndHexAndBinary(lbl.Codepage)));
            info.AppendLine(FillWithSpace("ID1", tab, false, DecimalAndHexAndBinary(lbl.ID1)));
            info.AppendLine(FillWithSpace("ID2", tab, false, DecimalAndHexAndBinary(lbl.ID2)));
            info.AppendLine(FillWithSpace("SortDescriptorDefBlock", tab, false, lbl.SortDescriptorDefBlock.ToString()));
            info.AppendLine(FillWithSpace("Lbl13Block", tab, false, lbl.Lbl13Block.ToString()));
            info.AppendLine(FillWithSpace("Unknown 0xC2", tab, false, HexString(lbl.Unknown_0xC2)));
            info.AppendLine(FillWithSpace("TidePredictionBlock", tab, false, lbl.TidePredictionBlock.ToString()));
            if (lbl.Headerlength >= 0xCE)
               info.AppendLine(FillWithSpace("Unknown 0xCE", tab, false, HexString(lbl.Unknown_0xCE)));
            if (lbl.Headerlength >= 0xD0)
               info.AppendLine(FillWithSpace("UnknownBlock 0xD0", tab, false, lbl.UnknownBlock_0xD0.ToString()));
            if (lbl.Headerlength >= 0xD8)
               info.AppendLine(FillWithSpace("Unknown 0xD8", tab, false, HexString(lbl.Unknown_0xD8)));
            if (lbl.Headerlength >= 0xDE)
               info.AppendLine(FillWithSpace("UnknownBlock 0xDE", tab, false, lbl.UnknownBlock_0xDE.ToString()));
            if (lbl.Headerlength >= 0xE6)
               info.AppendLine(FillWithSpace("Unknown 0xE6", tab, false, HexString(lbl.Unknown_0xE6)));
            if (lbl.Headerlength >= 0xEC)
               info.AppendLine(FillWithSpace("UnknownBlock 0xEC", tab, false, lbl.UnknownBlock_0xEC.ToString()));
            if (lbl.Headerlength >= 0xF4)
               info.AppendLine(FillWithSpace("Unknown 0xF4", tab, false, HexString(lbl.Unknown_0xF4)));
            if (lbl.Headerlength >= 0xFA)
               info.AppendLine(FillWithSpace("UnknownBlock 0xFA", tab, false, lbl.UnknownBlock_0xFA.ToString()));
            if (lbl.Headerlength >= 0x102)
               info.AppendLine(FillWithSpace("Unknown 0x102", tab, false, HexString(lbl.Unknown_0x102)));
            if (lbl.Headerlength >= 0x108)
               info.AppendLine(FillWithSpace("UnknownBlock 0x108", tab, false, lbl.UnknownBlock_0x108.ToString()));
            if (lbl.Headerlength >= 0x110)
               info.AppendLine(FillWithSpace("Unknown 0x110", tab, false, HexString(lbl.Unknown_0x110)));
            if (lbl.Headerlength >= 0x116)
               info.AppendLine(FillWithSpace("UnknownBlock 0x116", tab, false, lbl.UnknownBlock_0x116.ToString()));
            if (lbl.Headerlength >= 0x11E)
               info.AppendLine(FillWithSpace("Unknown 0x11E", tab, false, HexString(lbl.Unknown_0x11E)));
            if (lbl.Headerlength >= 0x124)
               info.AppendLine(FillWithSpace("UnknownBlock 0x124", tab, false, lbl.UnknownBlock_0x124.ToString()));
            if (lbl.Headerlength >= 0x12C)
               info.AppendLine(FillWithSpace("Unknown 0x12C", tab, false, HexString(lbl.Unknown_0x12C)));
            if (lbl.Headerlength >= 0x132)
               info.AppendLine(FillWithSpace("UnknownBlock 0x132", tab, false, lbl.UnknownBlock_0x132.ToString()));
            if (lbl.Headerlength >= 0x13A)
               info.AppendLine(FillWithSpace("Unknown 0x13A", tab, false, HexString(lbl.Unknown_0x13A)));
            if (lbl.Headerlength >= 0x140)
               info.AppendLine(FillWithSpace("UnknownBlock 0x140", tab, false, lbl.UnknownBlock_0x140.ToString()));
            if (lbl.Headerlength >= 0x148)
               info.AppendLine(FillWithSpace("Unknown 0x148", tab, false, HexString(lbl.Unknown_0x148)));
            if (lbl.Headerlength >= 0x14E)
               info.AppendLine(FillWithSpace("UnknownBlock 0x14E", tab, false, lbl.UnknownBlock_0x14E.ToString()));
            if (lbl.Headerlength >= 0x156)
               info.AppendLine(FillWithSpace("Unknown 0x156", tab, false, HexString(lbl.Unknown_0x156)));
            if (lbl.Headerlength >= 0x15A)
               info.AppendLine(FillWithSpace("UnknownBlock 0x15A", tab, false, lbl.UnknownBlock_0x15A.ToString()));
            if (lbl.Headerlength >= 0x162)
               info.AppendLine(FillWithSpace("Unknown 0x162", tab, false, HexString(lbl.Unknown_0x162)));
            if (lbl.Headerlength >= 0x168)
               info.AppendLine(FillWithSpace("UnknownBlock 0x168", tab, false, lbl.UnknownBlock_0x168.ToString()));
            if (lbl.Headerlength >= 0x170)
               info.AppendLine(FillWithSpace("Unknown 0x170", tab, false, HexString(lbl.Unknown_0x170)));
            if (lbl.Headerlength >= 0x176)
               info.AppendLine(FillWithSpace("UnknownBlock 0x176", tab, false, lbl.UnknownBlock_0x176.ToString()));
            if (lbl.Headerlength >= 0x17E)
               info.AppendLine(FillWithSpace("Unknown 0x17E", tab, false, HexString(lbl.Unknown_0x17E)));
            if (lbl.Headerlength >= 0x184)
               info.AppendLine(FillWithSpace("UnknownBlock 0x184", tab, false, lbl.UnknownBlock_0x184.ToString()));
            if (lbl.Headerlength >= 0x18C)
               info.AppendLine(FillWithSpace("Unknown 0x18C", tab, false, HexString(lbl.Unknown_0x18C)));
            if (lbl.Headerlength >= 0x192)
               info.AppendLine(FillWithSpace("UnknownBlock 0x192", tab, false, lbl.UnknownBlock_0x192.ToString()));
            if (lbl.Headerlength >= 0x19A)
               info.AppendLine(FillWithSpace("UnknownBlock 0x19A", tab, false, lbl.UnknownBlock_0x19A.ToString()));
            if (lbl.Headerlength >= 0x1A2)
               info.AppendLine(FillWithSpace("Unknown 0x1A2", tab, false, HexString(lbl.Unknown_0x1A2)));
            if (lbl.Headerlength >= 0x1A6)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1A6", tab, false, lbl.UnknownBlock_0x1A6.ToString()));
            if (lbl.Headerlength >= 0x1AE)
               info.AppendLine(FillWithSpace("Unknown 0x1AE", tab, false, HexString(lbl.Unknown_0x1AE)));
            if (lbl.Headerlength >= 0x1B2)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1B2", tab, false, lbl.UnknownBlock_0x1B2.ToString()));
            if (lbl.Headerlength >= 0x1BA)
               info.AppendLine(FillWithSpace("Unknown 0x1BA", tab, false, HexString(lbl.Unknown_0x1BA)));
            if (lbl.Headerlength >= 0x1BE)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1BE", tab, false, lbl.UnknownBlock_0x1BE.ToString()));
            if (lbl.Headerlength >= 0x1C6)
               info.AppendLine(FillWithSpace("Unknown 0x1C6", tab, false, HexString(lbl.Unknown_0x1C6)));
            if (lbl.Headerlength >= 0x1CA)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1CA", tab, false, lbl.UnknownBlock_0x1CA.ToString()));
            if (lbl.Headerlength >= 0x1D2)
               info.AppendLine(FillWithSpace("Unknown 0x1D2", tab, false, HexString(lbl.Unknown_0x1D2)));
            if (lbl.Headerlength >= 0x1D8)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1D8", tab, false, lbl.UnknownBlock_0x1D8.ToString()));
            if (lbl.Headerlength >= 0x1E0)
               info.AppendLine(FillWithSpace("Unknown 0x1E0", tab, false, HexString(lbl.Unknown_0x1E0)));
            if (lbl.Headerlength >= 0x1E6)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1E6", tab, false, lbl.UnknownBlock_0x1E6.ToString()));
            if (lbl.Headerlength >= 0x1EE)
               info.AppendLine(FillWithSpace("Unknown 0x1EE", tab, false, HexString(lbl.Unknown_0x1EE)));
            if (lbl.Headerlength >= 0x1F2)
               info.AppendLine(FillWithSpace("UnknownBlock 0x1F2", tab, false, lbl.UnknownBlock_0x1F2.ToString()));
            if (lbl.Headerlength >= 0x1FA)
               info.AppendLine(FillWithSpace("Unknown 0x1FA", tab, false, HexString(lbl.Unknown_0x1FA)));
            if (lbl.Headerlength >= 0x200)
               info.AppendLine(FillWithSpace("UnknownBlock 0x200", tab, false, lbl.UnknownBlock_0x200.ToString()));
            if (lbl.Headerlength >= 0x208)
               info.AppendLine(FillWithSpace("Unknown 0x208", tab, false, HexString(lbl.Unknown_0x208)));
         }
      }

      /// <summary>
      /// Funktion für alle LBL-Datei-Infos
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="nodetype">"Thema" der Info</param>
      /// <param name="idx">wenn größer oder gleich 0, dann der Index auf ein Objekt einer Tabelle</param>
      /// <param name="tn"></param>
      public static void SectionAndIndex(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, NodeContent.NodeType nodetype, int idx, TreeViewData tvd) {
         GarminCore.Files.StdFile_LBL lbl = filedata.GetGarminFileAsLBL();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;
         int tab = 20;

         switch (nodetype) {
            case NodeContent.NodeType.LBL_PostHeaderData:
               firsthexadr = lbl.PostHeaderDataBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("PostHeaderDataBlock: " + lbl.PostHeaderDataBlock.ToString());
                  hexlen = (int)lbl.PostHeaderDataBlock.Length;
               }
               break;

            case NodeContent.NodeType.LBL_TextBlock:
               firsthexadr = lbl.TextBlock.Offset;
               if (idx < 0) {
                  tab = 12;
                  info.AppendLine(FillWithSpace("TextBlock", tab, false, lbl.TextList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.TextBlock.ToString()));
                  info.AppendLine(FillWithSpace("   Count", tab, false, lbl.TextList.Count.ToString()));
                  hexlen = (int)lbl.TextBlock.Length;
               } else {
                  tab = 12;
                  int[] offset = lbl.TextList.Offsets();
                  info.AppendLine(FillWithSpace("TextOffset", tab, false, DecimalAndHexAndBinary(offset[idx])));
                  info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText((uint)offset[idx], true)));
                  firsthexadr += offset[idx] * (0x1 << lbl.DataOffsetMultiplier);
                  hexlen = idx < offset.Length - 1 ?
                                    (offset[idx + 1] - offset[idx]) * (0x1 << lbl.DataOffsetMultiplier) :
                                    (int)(lbl.TextBlock.Offset + lbl.TextBlock.Length) - (int)firsthexadr;
               }
               break;

            case NodeContent.NodeType.LBL_CountryBlock:
               firsthexadr = lbl.CountryBlock.Offset;
               if (idx < 0) {
                  tab = 14;
                  info.AppendLine(FillWithSpace("CountryBlock", tab, false, lbl.CountryDataList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.CountryBlock.ToString()));
                  hexlen = (int)lbl.CountryBlock.Length;
               } else {
                  tab = 28;
                  GarminCore.Files.StdFile_LBL.CountryRecord record = lbl.CountryDataList[idx];
                  info.AppendLine(FillWithSpace("TextOffsetInLBL (3 Byte)", tab, false, DecimalAndHexAndBinary((ulong)record.TextOffsetInLBL)));
                  info.AppendLine(FillWithSpace("   Country", tab, true, record.GetText(lbl, false)));
                  firsthexadr += idx * lbl.CountryBlock.Recordsize;
                  hexlen = idx * lbl.CountryBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_RegionBlock:
               firsthexadr = lbl.RegionBlock.Offset;
               if (idx < 0) {
                  tab = 14;
                  info.AppendLine(FillWithSpace("RegionBlock", tab, false, lbl.RegionAndCountryDataList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.RegionBlock.ToString()));
                  hexlen = (int)lbl.RegionBlock.Length;
               } else {
                  tab = 28;
                  GarminCore.Files.StdFile_LBL.RegionAndCountryRecord record = lbl.RegionAndCountryDataList[idx];
                  info.AppendLine(FillWithSpace("CountryIndex (2 Byte)", tab, false, DecimalAndHexAndBinary(record.CountryIndex)));
                  int idxcountry = record.CountryIndex - 1; // 1-basiert
                  info.AppendLine(FillWithSpace("   TextOffsetInLBL", tab, false, DecimalAndHexAndBinary((ulong)lbl.CountryDataList[idxcountry].TextOffsetInLBL)));
                  info.AppendLine(FillWithSpace("   Region", tab, true, record.GetRegionText(lbl, false)));
                  info.AppendLine(FillWithSpace("TextOffset (3 Byte)", tab, false, DecimalAndHexAndBinary((ulong)record.TextOffset)));
                  info.AppendLine(FillWithSpace("   Country", tab, true, record.GetCountryText(lbl, false)));
                  firsthexadr += idx * lbl.RegionBlock.Recordsize;
                  hexlen = lbl.RegionBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_CityBlock:
               firsthexadr = lbl.CityBlock.Offset;
               if (idx < 0) {
                  tab = 14;
                  info.AppendLine(FillWithSpace("CityBlock", tab, false, lbl.CityAndRegionOrCountryDataList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.CityBlock.ToString()));
                  hexlen = (int)lbl.CityBlock.Length;
               } else {
                  tab = 47;
                  GarminCore.Files.StdFile_LBL.CityAndRegionOrCountryRecord record = lbl.CityAndRegionOrCountryDataList[idx];
                  if (record.IsPointInRGN) {
                     info.AppendLine(FillWithSpace("SubdivisionNumberInRGN (Bit 0..15) (3 Byte)", tab, false, DecimalAndHexAndBinary(record.SubdivisionNumberInRGN)));
                     info.AppendLine(FillWithSpace("PointIndexInRGN (Bit 16..23)", tab, false, DecimalAndHexAndBinary((ulong)record.PointIndexInRGN)));

                     GarminCore.Files.StdFile_RGN rgn = GetFirstRGNinTreeView(tvd, filedata.Basename);
                     if (rgn != null) {
                        GarminCore.Files.StdFile_RGN.RawPointData rpd = rgn.GetPoint1(record.SubdivisionNumberInRGN, record.PointIndexInRGN);
                        if (!(rpd is null))
                           info.AppendLine(FillWithSpace("   Text", tab, true, rpd.GetText(lbl, true)));
                     }
                  } else {
                     info.AppendLine(FillWithSpace("TextOffset (3 Byte)", tab, false, DecimalAndHexAndBinary((ulong)record.TextOffsetInLBL)));
                     info.AppendLine(FillWithSpace("   Text'", tab, true, record.GetCityText(lbl, tvd.GetRGN(filedata.Basename), false)));
                  }
                  info.AppendLine(FillWithSpace("RegionOrCountryIndex (Bit 0..13) (2 Byte)", tab, false, DecimalAndHexAndBinary(record.RegionOrCountryIndex)));
                  info.AppendLine(FillWithSpace("   IsCountry         (Bit 14)", tab, false, record.IsCountry.ToString()));
                  info.AppendLine(FillWithSpace("   IsPointInRGN      (Bit 15)", tab, false, record.IsPointInRGN.ToString()));
                  info.AppendLine(FillWithSpace("   Country", tab, true, record.GetCountryText(lbl, false)));
                  if (!record.IsCountry)
                     info.AppendLine(FillWithSpace("   Region", tab, true, record.GetRegionText(lbl, false)));
                  firsthexadr += idx * lbl.CityBlock.Recordsize;
                  hexlen = lbl.CityBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_PointPropertiesBlock:
               firsthexadr = lbl.POIPropertiesBlock.Offset;
               if (idx < 0) {
                  tab = 23;
                  info.AppendLine(FillWithSpace("PointPropertiesBlock", tab, false, lbl.PointPropertiesList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.POIPropertiesBlock.ToString()));
                  hexlen = (int)lbl.POIPropertiesBlock.Length;
               } else {
                  tab = 35;
                  GarminCore.Files.StdFile_LBL.PointDataRecord record = lbl.PointPropertiesList[idx];
                  info.AppendLine(FillWithSpace("RecordLength", tab, false, DecimalAndHexAndBinary(record.DataLength(lbl.POIGlobalMask))));
                  info.AppendLine(FillWithSpace("TextOffset (Bit 0..22) (3 Byte)", tab, false, DecimalAndHexAndBinary(record.TextOffset)));
                  if (record.TextOffset > 0)
                     info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.TextOffset, true)));
                  info.AppendLine(FillWithSpace("HasLocalProperties (Bit 23)", tab, false, record.HasLocalProperties.ToString()));
                  hexlen = 3;
                  if (!record.HasLocalProperties) {
                     info.AppendLine("GlobalProperties:");
                  } else {
                     hexlen += 1;
                  }
                  info.AppendLine(FillWithSpace("   (1 Byte):               ", tab, false, DecimalAndHexAndBinary((byte)record._internalPropMask)));
                  info.AppendLine(FillWithSpace("   has_street_num (Bit 0): ", tab, false, record.StreetNumberIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   has_street     (Bit 1): ", tab, false, record.StreetIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   has_city       (Bit 2): ", tab, false, record.CityIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   has_zip        (Bit 3): ", tab, false, record.ZipIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   has_phone      (Bit 4): ", tab, false, record.PhoneIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   has_exit       (Bit 5): ", tab, false, record.ExitIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   has_tide_pred  (Bit 6): ", tab, false, record.TidePredictionIsSet.ToString()));
                  info.AppendLine(FillWithSpace("   unknown        (Bit 7): ", tab, false, record.UnknownIsSet.ToString()));

                  if (record.StreetNumberIsSet) {
                     if (record.StreetNumberIsCoded) {
                        info.AppendLine(FillWithSpace(string.Format("StreetNumber ({0} Byte)", record._streetnumber_encoded.Length), tab, true, record.StreetNumber));
                        hexlen += record._streetnumber_encoded.Length;
                     } else {
                        info.AppendLine(FillWithSpace("StreetNumberOffset (3 Byte)", tab, false, DecimalAndHexAndBinary(record.StreetNumberOffset)));
                        info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.StreetNumberOffset, true)));
                        hexlen += 3;
                     }
                  }
                  if (record.StreetIsSet) {
                     info.AppendLine(FillWithSpace("StreetOffset (3 Byte)", tab, false, DecimalAndHexAndBinary(record.StreetOffset)));
                     info.AppendLine(FillWithSpace("   Text'", tab, true, lbl.GetText(record.StreetOffset, true)));
                     hexlen += 3;
                  }
                  if (record.CityIsSet) {
                     GarminCore.Files.StdFile_LBL.CityAndRegionOrCountryRecord cityrecord = lbl.CityAndRegionOrCountryDataList[record.CityIndex - 1];
                     info.AppendLine(FillWithSpace("CityIndex (" + (record.UseShortCityIndex ? "1" : "2") + " Byte)", tab, false, DecimalAndHexAndBinary(record.CityIndex)));
                     info.AppendLine(FillWithSpace("   City-Text" + (cityrecord.IsPointInRGN ? " (Point in RGN)" : ""), tab, true, cityrecord.GetCityText(lbl, tvd.GetRGN(filedata.Basename), false)));
                     info.AppendLine(FillWithSpace("   Country-Text", tab, true, cityrecord.GetCountryText(lbl, false)));
                     info.AppendLine(FillWithSpace("   Region-Text", tab, true, cityrecord.GetRegionText(lbl, false)));
                     hexlen += record.UseShortCityIndex ? 1 : 2;
                  }
                  if (record.ZipIsSet) {
                     info.AppendLine(FillWithSpace("ZipIndex (" + (record.UseShortZipIndex ? "1" : "2") + " Byte)", tab, false, DecimalAndHexAndBinary(record.ZipIndex)));
                     info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(lbl.ZipDataList[record.ZipIndex - 1].TextOffsetInLBL, true)));
                     hexlen += record.UseShortZipIndex ? 1 : 2;
                  }
                  if (record.PhoneIsSet) {
                     if (record.PhoneNumberIsCoded) {
                        info.AppendLine(FillWithSpace(string.Format("PhoneNumber ({0} Byte)", record._phonenumber_encoded.Length), tab, true, record.PhoneNumber));
                        hexlen += record._phonenumber_encoded.Length;
                     } else {
                        info.AppendLine(FillWithSpace("PhoneNumberOffset (3 Byte)", tab, false, DecimalAndHexAndBinary(record.PhoneNumberOffset)));
                        info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.PhoneNumberOffset, true)));
                        hexlen += 3;
                     }
                  }
                  if (record.ExitIsSet) {
                     info.AppendLine(FillWithSpace("ExitOffset (Bit 0..22) (3 Byte)", tab, false, DecimalAndHexAndBinary(record.ExitOffset)));
                     info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.ExitOffset, true)));
                     hexlen += 3;
                     info.AppendLine(FillWithSpace("ExitHighwayIndex (" + (record.UseShortExitHighwayIndex ? "1" : "2") + " Byte)", tab, false, DecimalAndHexAndBinary(record.ExitHighwayIndex)));
                     hexlen += record.UseShortExitHighwayIndex ? 1 : 2;
                     if (record.ExitIndexIsSet) {
                        info.AppendLine(FillWithSpace("ExitIndex (" + (record.UseShortExitIndex ? "1" : "2") + " Byte)", tab, false, DecimalAndHexAndBinary(record.ExitIndex)));
                        hexlen += record.UseShortExitIndex ? 1 : 2;
                     }
                  }

                  foreach (var item in lbl.PointPropertiesListOffsets) {
                     if (item.Value == idx) {
                        firsthexadr = lbl.POIPropertiesBlock.Offset + item.Key;
                        break;
                     }
                  }
               }
               break;

            case NodeContent.NodeType.LBL_PointIndexList4RGN:
               firsthexadr = lbl.POIIndexBlock.Offset;
               if (idx < 0) {
                  tab = 23;
                  info.AppendLine(FillWithSpace("PointIndexList4RGN", tab, false, lbl.PointIndexList4RGN.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.POIIndexBlock.ToString()));
                  hexlen = (int)lbl.POIIndexBlock.Length;
               } else {
                  tab = 35;
                  GarminCore.Files.StdFile_LBL.PointIndexRecord record = lbl.PointIndexList4RGN[idx];
                  info.AppendLine(FillWithSpace("PointIndexInRGN        (1 Byte)", tab, false, DecimalAndHexAndBinary(record.PointIndexInRGN)));
                  info.AppendLine(FillWithSpace("SubdivisionNumberInRGN (2 Byte)", tab, false, DecimalAndHexAndBinary(record.SubdivisionNumberInRGN)));
                  info.AppendLine(FillWithSpace("SubType                (1 Byte)", tab, false, DecimalAndHexAndBinary(record.SubType)));
                  firsthexadr += idx * lbl.POIIndexBlock.Recordsize;
                  hexlen = lbl.POIIndexBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_PointTypeIndexList4RGN:
               firsthexadr = lbl.POITypeIndexBlock.Offset;
               if (idx < 0) {
                  tab = 23;
                  info.AppendLine(FillWithSpace("POITypeIndexBlock", tab, false, lbl.PointTypeIndexList4RGN.Count.ToString()));
                  info.AppendLine(FillWithSpace("  Block", tab, false, lbl.POITypeIndexBlock.ToString()));
                  hexlen = (int)lbl.POITypeIndexBlock.Length;
               } else {
                  tab = 23;
                  GarminCore.Files.StdFile_LBL.PointTypeIndexRecord record = lbl.PointTypeIndexList4RGN[idx];
                  info.AppendLine(FillWithSpace("PointType (1 Byte)", tab, false, DecimalAndHexAndBinary(record.PointType)));
                  info.AppendLine(FillWithSpace("StartIdx  (3 Byte)", tab, false, DecimalAndHexAndBinary(record.StartIdx)));
                  firsthexadr += idx * lbl.POITypeIndexBlock.Recordsize;
                  hexlen = lbl.POITypeIndexBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_ZipBlock:
               firsthexadr = lbl.ZipBlock.Offset;
               if (idx < 0) {
                  tab = 13;
                  info.AppendLine(FillWithSpace("ZipBlock", tab, false, lbl.ZipDataList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.ZipBlock.ToString()));
                  hexlen = (int)lbl.ZipBlock.Length;
               } else {
                  tab = 25;
                  GarminCore.Files.StdFile_LBL.ZipRecord record = lbl.ZipDataList[idx];
                  info.AppendLine(FillWithSpace("TextOffset (3 Byte)", tab, false, DecimalAndHexAndBinary(record.TextOffsetInLBL)));
                  info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.TextOffsetInLBL, true)));
                  firsthexadr += idx * lbl.ZipBlock.Recordsize;
                  hexlen = lbl.ZipBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_HighwayWithExitBlock:
               firsthexadr = lbl.HighwayWithExitBlock.Offset;
               if (idx < 0) {
                  tab = 25;
                  info.AppendLine(FillWithSpace("HighwayWithExitBlock", tab, false, lbl.HighwayWithExitList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.HighwayWithExitBlock.ToString()));
                  hexlen = (int)lbl.HighwayWithExitBlock.Length;
               } else {
                  tab = 30;
                  GarminCore.Files.StdFile_LBL.HighwayWithExitRecord record = lbl.HighwayWithExitList[idx];
                  info.AppendLine(FillWithSpace("TextOffset      (3 Byte)", tab, false, DecimalAndHexAndBinary(record.TextOffset)));
                  info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.TextOffset, true)));
                  info.AppendLine(FillWithSpace("FirstExitOffset (2 Byte)", tab, false, DecimalAndHexAndBinary(record.FirstExitOffset)));
                  info.AppendLine(FillWithSpace("Unknown1        (1 Byte)", tab, false, DecimalAndHexAndBinary(record.Unknown1)));
                  firsthexadr += idx * lbl.HighwayWithExitBlock.Recordsize;
                  hexlen = lbl.HighwayWithExitBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_ExitBlock:
               firsthexadr = lbl.ExitBlock.Offset;
               if (idx < 0) {
                  tab = 25;
                  info.AppendLine(FillWithSpace("ExitBlock", tab, false, lbl.ExitList.Count.ToString()));
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.ExitBlock.ToString()));
                  hexlen = (int)lbl.HighwayWithExitBlock.Length;
               } else {
                  tab = 40;
                  GarminCore.Files.StdFile_LBL.ExitRecord record = lbl.ExitList[idx];
                  info.AppendLine(FillWithSpace("TextOffsetInLBL (Bit 0..21) (3 Byte)", tab, false, DecimalAndHexAndBinary(record.TextOffsetInLBL)));
                  info.AppendLine(FillWithSpace("   Text", tab, true, lbl.GetText(record.TextOffsetInLBL, true)));
                  info.AppendLine(FillWithSpace("LastFacilitie   (Bit 23)", tab, false, record.LastFacilitie.ToString()));
                  info.AppendLine(FillWithSpace("Type            (Bit 0..3)  (1 Byte)", tab, false, DecimalAndHexAndBinary(record.Type)));
                  info.AppendLine(FillWithSpace("Direction       (Bit 5..7)  (1 Byte)", tab, false, DecimalAndHexAndBinary(record.Direction)));
                  firsthexadr += idx * lbl.ExitBlock.Recordsize;
                  hexlen = lbl.ExitBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_HighwayExitBlock:
               firsthexadr = lbl.HighwayExitBlock.Offset;
               if (idx < 0) {
                  tab = 25;
                  info.AppendLine(FillWithSpace("HighwayWithExitBlock", tab, false, lbl.HighwayExitDefList.Count.ToString()));
                  info.AppendLine(FillWithSpace("  Block", tab, false, lbl.HighwayExitBlock.ToString()));
                  hexlen = (int)lbl.HighwayExitBlock.Length;
               } else {
                  tab = 25;
                  GarminCore.Files.StdFile_LBL.HighwayExitDefRecord record = lbl.HighwayExitDefList[idx];
                  info.AppendLine(FillWithSpace("Unknown1    (1 Byte)", tab, false, DecimalAndHexAndBinary(record.Unknown1)));
                  info.AppendLine(FillWithSpace("RegionIndex (2 Byte)", tab, false, DecimalAndHexAndBinary(record.RegionIndex)));
                  info.AppendLine(FillWithSpace("Exits", tab, false, DecimalAndHexAndBinary(record.ExitList.Count)));
                  firsthexadr += idx * lbl.HighwayExitBlock.Recordsize;
                  hexlen = 3 + 3 * record.ExitList.Count;
               }
               break;

            case NodeContent.NodeType.LBL_SortDescriptorDefBlock:
               firsthexadr = lbl.SortDescriptorDefBlock.Offset;
               if (idx < 0) {
                  tab = 25;
                  info.AppendLine("SortDescriptorDefBlock");
                  info.AppendLine(FillWithSpace("   Block", tab, false, lbl.SortDescriptorDefBlock.ToString()));
                  info.AppendLine(FillWithSpace("   SortDescriptor", tab, true, lbl.SortDescriptor));
                  hexlen = (int)lbl.SortDescriptorDefBlock.Length;
               }
               break;

            case NodeContent.NodeType.LBL_Lbl13Block:
               firsthexadr = lbl.Lbl13Block.Offset;
               if (idx < 0) {
                  info.AppendLine("Lbl13Block");
                  info.AppendLine("  Block: " + lbl.Lbl13Block.ToString());
                  hexlen = (int)lbl.Lbl13Block.Length;
               }
               break;

            case NodeContent.NodeType.LBL_TidePredictionBlock:
               firsthexadr = lbl.TidePredictionBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("TidePredictionBlock");
                  info.AppendLine("  Block: " + lbl.TidePredictionBlock.ToString());
                  hexlen = (int)lbl.TidePredictionBlock.Length;
               }
               break;

            case NodeContent.NodeType.LBL_UnknownBlock_0xD0: firsthexadr = lbl.UnknownBlock_0xD0.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0xD0"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0xD0.ToString()); hexlen = (int)lbl.UnknownBlock_0xD0.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xDE: firsthexadr = lbl.UnknownBlock_0xDE.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0xDE"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0xDE.ToString()); hexlen = (int)lbl.UnknownBlock_0xDE.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xEC: firsthexadr = lbl.UnknownBlock_0xEC.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0xEC"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0xEC.ToString()); hexlen = (int)lbl.UnknownBlock_0xEC.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0xFA: firsthexadr = lbl.UnknownBlock_0xFA.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0xFA"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0xFA.ToString()); hexlen = (int)lbl.UnknownBlock_0xFA.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x108: firsthexadr = lbl.UnknownBlock_0x108.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x108"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x108.ToString()); hexlen = (int)lbl.UnknownBlock_0x108.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x116: firsthexadr = lbl.UnknownBlock_0x116.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x116"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x116.ToString()); hexlen = (int)lbl.UnknownBlock_0x116.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x124: firsthexadr = lbl.UnknownBlock_0x124.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x124"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x124.ToString()); hexlen = (int)lbl.UnknownBlock_0x124.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x132: firsthexadr = lbl.UnknownBlock_0x132.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x132"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x132.ToString()); hexlen = (int)lbl.UnknownBlock_0x132.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x140: firsthexadr = lbl.UnknownBlock_0x140.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x140"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x140.ToString()); hexlen = (int)lbl.UnknownBlock_0x140.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x14E: firsthexadr = lbl.UnknownBlock_0x14E.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x14E"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x14E.ToString()); hexlen = (int)lbl.UnknownBlock_0x14E.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x15A: firsthexadr = lbl.UnknownBlock_0x15A.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x15A"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x15A.ToString()); hexlen = (int)lbl.UnknownBlock_0x15A.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x168: firsthexadr = lbl.UnknownBlock_0x168.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x168"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x168.ToString()); hexlen = (int)lbl.UnknownBlock_0x168.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x176: firsthexadr = lbl.UnknownBlock_0x176.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x176"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x176.ToString()); hexlen = (int)lbl.UnknownBlock_0x176.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x184: firsthexadr = lbl.UnknownBlock_0x184.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x184"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x184.ToString()); hexlen = (int)lbl.UnknownBlock_0x184.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x192: firsthexadr = lbl.UnknownBlock_0x192.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x192"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x192.ToString()); hexlen = (int)lbl.UnknownBlock_0x192.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x19A: firsthexadr = lbl.UnknownBlock_0x19A.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x19A"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x19A.ToString()); hexlen = (int)lbl.UnknownBlock_0x19A.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1A6: firsthexadr = lbl.UnknownBlock_0x1A6.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1A6"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1A6.ToString()); hexlen = (int)lbl.UnknownBlock_0x1A6.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1B2: firsthexadr = lbl.UnknownBlock_0x1B2.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1B2"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1B2.ToString()); hexlen = (int)lbl.UnknownBlock_0x1B2.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1BE: firsthexadr = lbl.UnknownBlock_0x1BE.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1BE"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1BE.ToString()); hexlen = (int)lbl.UnknownBlock_0x1BE.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1CA: firsthexadr = lbl.UnknownBlock_0x1CA.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1CA"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1CA.ToString()); hexlen = (int)lbl.UnknownBlock_0x1CA.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1D8: firsthexadr = lbl.UnknownBlock_0x1D8.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1D8"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1D8.ToString()); hexlen = (int)lbl.UnknownBlock_0x1D8.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1E6: firsthexadr = lbl.UnknownBlock_0x1E6.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1E6"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1E6.ToString()); hexlen = (int)lbl.UnknownBlock_0x1E6.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x1F2: firsthexadr = lbl.UnknownBlock_0x1F2.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x1F2"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x1F2.ToString()); hexlen = (int)lbl.UnknownBlock_0x1F2.Length; } break;
            case NodeContent.NodeType.LBL_UnknownBlock_0x200: firsthexadr = lbl.UnknownBlock_0x200.Offset; if (idx < 0) { info.AppendLine("UnknownBlock_0x200"); info.AppendLine("  Block:   " + lbl.UnknownBlock_0x200.ToString()); hexlen = (int)lbl.UnknownBlock_0x200.Length; } break;

            default:
               info.AppendLine("internal error: no info for nodetype '" + nodetype.ToString() + "'");
               break;
         }

         if (hexlen > 0)
            hex = HexRange(firsthexadr, filedata.BinaryReader, hexlen);
      }

   }
}
