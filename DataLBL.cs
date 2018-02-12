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
         if (lbl.POIIndexBlock != null && lbl.POIIndexBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_POIIndexBlock, lbl, "POIIndexBlock"));
         if (lbl.POIPropertiesBlock != null && lbl.POIPropertiesBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_POIPropertiesBlock, lbl, "POIPropertiesBlock"));
         if (lbl.POITypeIndexBlock != null && lbl.POITypeIndexBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.LBL_POITypeIndexBlock, lbl, "POITypeIndexBlock"));
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

            case NodeContent.NodeType.LBL_POIIndexBlock:
               for (int i = 0; i < lbl.PoiIndexDataList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "PoiIndex " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_POIPropertiesBlock:
               for (int i = 0; i < lbl.POIPropertiesList.Count; i++)
                  AppendNode(tn, NodeContent.NodeType.Index, i, "POIProperties " + i.ToString());
               break;

            case NodeContent.NodeType.LBL_POITypeIndexBlock:
               for (int i = 0; i < lbl.PoiTypeIndexDataList.Count; i++)
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
         info.AppendLine("TextBlock:              " + lbl.TextBlock.ToString());
         info.AppendLine("DataOffsetMultiplier:   " + DecimalAndHexAndBinary(lbl.DataOffsetMultiplier));
         info.AppendLine("EncodingType:           " + DecimalAndHexAndBinary(lbl.EncodingType));
         info.AppendLine("CountryBlock:           " + lbl.CountryBlock.ToString());
         info.AppendLine("Unknown 0x29:           " + HexString(lbl.Unknown_0x29));
         info.AppendLine("RegionBlock:            " + lbl.RegionBlock.ToString());
         info.AppendLine("Unknown 0x37:           " + HexString(lbl.Unknown_0x37));
         info.AppendLine("CityBlock:              " + lbl.CityBlock.ToString());
         info.AppendLine("Unknown 0x45:           " + HexString(lbl.Unknown_0x45));
         info.AppendLine("PoiIndexDataList:       " + lbl.POIIndexBlock.ToString());
         info.AppendLine("Unknown 0x53:           " + HexString(lbl.Unknown_0x53));
         info.AppendLine("POIPropertiesBlock:     " + lbl.POIPropertiesBlock.ToString());
         info.AppendLine("POIOffsetMultiplier:    " + DecimalAndHexAndBinary(lbl.POIOffsetMultiplier) + " -> " + (0x1 << lbl.POIOffsetMultiplier));
         info.AppendLine("POIGlobalMask:          " + DecimalAndHexAndBinary((byte)lbl.POIGlobalMask));
         info.AppendLine("Unknown 0x61:           " + HexString(lbl.Unknown_0x61));
         info.AppendLine("POITypeIndexBlock:      " + lbl.POITypeIndexBlock.ToString());
         info.AppendLine("Unknown 0x6E:           " + HexString(lbl.Unknown_0x6E));
         info.AppendLine("ZipBlock:               " + lbl.ZipBlock.ToString());
         info.AppendLine("Unknown 0x7C:           " + HexString(lbl.Unknown_0x7C));
         info.AppendLine("HighwayWithExitBlock:   " + lbl.HighwayWithExitBlock.ToString());
         info.AppendLine("Unknown 0x8A:           " + HexString(lbl.Unknown_0x8A));
         info.AppendLine("ExitBlock:              " + lbl.ExitBlock.ToString());
         info.AppendLine("Unknown 0x98:           " + HexString(lbl.Unknown_0x98));
         info.AppendLine("HighwayExitBlock:       " + lbl.HighwayExitBlock.ToString());
         info.AppendLine("Unknown 0xA6:           " + HexString(lbl.Unknown_0xA6));
         if (lbl.Headerlength >= 0xAA) {
            info.AppendLine("Codepage:               " + DecimalAndHexAndBinary(lbl.Codepage));
            info.AppendLine("ID1:                    " + DecimalAndHexAndBinary(lbl.ID1));
            info.AppendLine("ID2:                    " + DecimalAndHexAndBinary(lbl.ID2));
            info.AppendLine("SortDescriptorDefBlock: " + lbl.SortDescriptorDefBlock.ToString());
            info.AppendLine("Lbl13Block:             " + lbl.Lbl13Block.ToString());
            info.AppendLine("Unknown 0xC2:           " + HexString(lbl.Unknown_0xC2));
            info.AppendLine("TidePredictionBlock:    " + lbl.TidePredictionBlock.ToString());
            if (lbl.Headerlength >= 0xCE)
               info.AppendLine("Unknown 0xCE:           " + HexString(lbl.Unknown_0xCE));
            if (lbl.Headerlength >= 0xD0)
               info.AppendLine("UnknownBlock 0xD0:      " + lbl.UnknownBlock_0xD0.ToString());
            if (lbl.Headerlength >= 0xD8)
               info.AppendLine("Unknown 0xD8:           " + HexString(lbl.Unknown_0xD8));
            if (lbl.Headerlength >= 0xDE)
               info.AppendLine("UnknownBlock 0xDE:      " + lbl.UnknownBlock_0xDE.ToString());
            if (lbl.Headerlength >= 0xE6)
               info.AppendLine("Unknown 0xE6:           " + HexString(lbl.Unknown_0xE6));
            if (lbl.Headerlength >= 0xEC)
               info.AppendLine("UnknownBlock 0xEC:      " + lbl.UnknownBlock_0xEC.ToString());
            if (lbl.Headerlength >= 0xF4)
               info.AppendLine("Unknown 0xF4:           " + HexString(lbl.Unknown_0xF4));
            if (lbl.Headerlength >= 0xFA)
               info.AppendLine("UnknownBlock 0xFA:      " + lbl.UnknownBlock_0xFA.ToString());
            if (lbl.Headerlength >= 0x102)
               info.AppendLine("Unknown 0x102:          " + HexString(lbl.Unknown_0x102));
            if (lbl.Headerlength >= 0x108)
               info.AppendLine("UnknownBlock 0x108:     " + lbl.UnknownBlock_0x108.ToString());
            if (lbl.Headerlength >= 0x110)
               info.AppendLine("Unknown 0x110:          " + HexString(lbl.Unknown_0x110));
            if (lbl.Headerlength >= 0x116)
               info.AppendLine("UnknownBlock 0x116:     " + lbl.UnknownBlock_0x116.ToString());
            if (lbl.Headerlength >= 0x11E)
               info.AppendLine("Unknown 0x11E:          " + HexString(lbl.Unknown_0x11E));
            if (lbl.Headerlength >= 0x124)
               info.AppendLine("UnknownBlock 0x124:     " + lbl.UnknownBlock_0x124.ToString());
            if (lbl.Headerlength >= 0x12C)
               info.AppendLine("Unknown 0x12C:          " + HexString(lbl.Unknown_0x12C));
            if (lbl.Headerlength >= 0x132)
               info.AppendLine("UnknownBlock 0x132:     " + lbl.UnknownBlock_0x132.ToString());
            if (lbl.Headerlength >= 0x13A)
               info.AppendLine("Unknown 0x13A:          " + HexString(lbl.Unknown_0x13A));
            if (lbl.Headerlength >= 0x140)
               info.AppendLine("UnknownBlock 0x140:     " + lbl.UnknownBlock_0x140.ToString());
            if (lbl.Headerlength >= 0x148)
               info.AppendLine("Unknown 0x148:          " + HexString(lbl.Unknown_0x148));
            if (lbl.Headerlength >= 0x14E)
               info.AppendLine("UnknownBlock 0x14E:     " + lbl.UnknownBlock_0x14E.ToString());
            if (lbl.Headerlength >= 0x156)
               info.AppendLine("Unknown 0x156:          " + HexString(lbl.Unknown_0x156));
            if (lbl.Headerlength >= 0x15A)
               info.AppendLine("UnknownBlock 0x15A:     " + lbl.UnknownBlock_0x15A.ToString());
            if (lbl.Headerlength >= 0x162)
               info.AppendLine("Unknown 0x162:          " + HexString(lbl.Unknown_0x162));
            if (lbl.Headerlength >= 0x168)
               info.AppendLine("UnknownBlock 0x168:     " + lbl.UnknownBlock_0x168.ToString());
            if (lbl.Headerlength >= 0x170)
               info.AppendLine("Unknown 0x170:          " + HexString(lbl.Unknown_0x170));
            if (lbl.Headerlength >= 0x176)
               info.AppendLine("UnknownBlock 0x176:     " + lbl.UnknownBlock_0x176.ToString());
            if (lbl.Headerlength >= 0x17E)
               info.AppendLine("Unknown 0x17E:          " + HexString(lbl.Unknown_0x17E));
            if (lbl.Headerlength >= 0x184)
               info.AppendLine("UnknownBlock 0x184:     " + lbl.UnknownBlock_0x184.ToString());
            if (lbl.Headerlength >= 0x18C)
               info.AppendLine("Unknown 0x18C:          " + HexString(lbl.Unknown_0x18C));
            if (lbl.Headerlength >= 0x192)
               info.AppendLine("UnknownBlock 0x192:     " + lbl.UnknownBlock_0x192.ToString());
            if (lbl.Headerlength >= 0x19A)
               info.AppendLine("UnknownBlock 0x19A:     " + lbl.UnknownBlock_0x19A.ToString());
            if (lbl.Headerlength >= 0x1A2)
               info.AppendLine("Unknown 0x1A2:          " + HexString(lbl.Unknown_0x1A2));
            if (lbl.Headerlength >= 0x1A6)
               info.AppendLine("UnknownBlock 0x1A6:     " + lbl.UnknownBlock_0x1A6.ToString());
            if (lbl.Headerlength >= 0x1AE)
               info.AppendLine("Unknown 0x1AE:          " + HexString(lbl.Unknown_0x1AE));
            if (lbl.Headerlength >= 0x1B2)
               info.AppendLine("UnknownBlock 0x1B2:     " + lbl.UnknownBlock_0x1B2.ToString());
            if (lbl.Headerlength >= 0x1BA)
               info.AppendLine("Unknown 0x1BA:          " + HexString(lbl.Unknown_0x1BA));
            if (lbl.Headerlength >= 0x1BE)
               info.AppendLine("UnknownBlock 0x1BE:     " + lbl.UnknownBlock_0x1BE.ToString());
            if (lbl.Headerlength >= 0x1C6)
               info.AppendLine("Unknown 0x1C6:          " + HexString(lbl.Unknown_0x1C6));
            if (lbl.Headerlength >= 0x1CA)
               info.AppendLine("UnknownBlock 0x1CA:     " + lbl.UnknownBlock_0x1CA.ToString());
            if (lbl.Headerlength >= 0x1D2)
               info.AppendLine("Unknown 0x1D2:          " + HexString(lbl.Unknown_0x1D2));
            if (lbl.Headerlength >= 0x1D8)
               info.AppendLine("UnknownBlock 0x1D8:     " + lbl.UnknownBlock_0x1D8.ToString());
            if (lbl.Headerlength >= 0x1E0)
               info.AppendLine("Unknown 0x1E0:          " + HexString(lbl.Unknown_0x1E0));
            if (lbl.Headerlength >= 0x1E6)
               info.AppendLine("UnknownBlock 0x1E6:     " + lbl.UnknownBlock_0x1E6.ToString());
            if (lbl.Headerlength >= 0x1EE)
               info.AppendLine("Unknown 0x1EE:          " + HexString(lbl.Unknown_0x1EE));
            if (lbl.Headerlength >= 0x1F2)
               info.AppendLine("UnknownBlock 0x1F2:     " + lbl.UnknownBlock_0x1F2.ToString());
            if (lbl.Headerlength >= 0x1FA)
               info.AppendLine("Unknown 0x1FA:          " + HexString(lbl.Unknown_0x1FA));
            if (lbl.Headerlength >= 0x200)
               info.AppendLine("UnknownBlock 0x200:     " + lbl.UnknownBlock_0x200.ToString());
            if (lbl.Headerlength >= 0x208)
               info.AppendLine("Unknown 0x208:          " + HexString(lbl.Unknown_0x208));
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
                  info.AppendLine("TextBlock: " + lbl.TextList.Count.ToString());
                  info.AppendLine("  Block:   " + lbl.TextBlock.ToString());
                  info.AppendLine("  Count:   " + lbl.TextList.Count.ToString());
                  hexlen = (int)lbl.TextBlock.Length;
               } else {
                  int[] offset = lbl.TextList.Offsets();
                  info.AppendLine("TextOffset: " + DecimalAndHexAndBinary(offset[idx]));
                  info.AppendLine("   Text:    '" + lbl.GetText((uint)offset[idx], true) + "'");
                  firsthexadr += offset[idx] * (0x1 << lbl.DataOffsetMultiplier);
                  hexlen = idx < offset.Length - 1 ?
                                    (offset[idx + 1] - offset[idx]) * (0x1 << lbl.DataOffsetMultiplier) :
                                    (int)(lbl.TextBlock.Offset + lbl.TextBlock.Length) - (int)firsthexadr;
               }
               break;

            case NodeContent.NodeType.LBL_CountryBlock:
               firsthexadr = lbl.CountryBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("CountryBlock: " + lbl.CountryDataList.Count.ToString());
                  info.AppendLine("  Block:      " + lbl.CountryBlock.ToString());
                  hexlen = (int)lbl.CountryBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.CountryRecord record = lbl.CountryDataList[idx];
                  info.AppendLine("TextOffset (3 Byte): " + DecimalAndHexAndBinary((ulong)record.TextOffset));
                  info.AppendLine("   Text:             '" + lbl.GetText(record.TextOffset, true) + "'");
                  firsthexadr += idx * lbl.CountryBlock.Recordsize;
                  hexlen = idx * lbl.CountryBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_RegionBlock:
               firsthexadr = lbl.RegionBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("RegionBlock: " + lbl.RegionAndCountryDataList.Count.ToString());
                  info.AppendLine("  Block:     " + lbl.RegionBlock.ToString());
                  hexlen = (int)lbl.RegionBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.RegionAndCountryRecord record = lbl.RegionAndCountryDataList[idx];
                  info.AppendLine("CountryIndex (2 Byte): " + DecimalAndHexAndBinary(record.CountryIndex));
                  int idxcountry = record.CountryIndex - 1; // 1-basiert
                  info.AppendLine("   TextOffset:         " + DecimalAndHexAndBinary((ulong)lbl.CountryDataList[idxcountry].TextOffset));
                  info.AppendLine("   Text:               '" + lbl.GetText(lbl.CountryDataList[idxcountry].TextOffset, true) + "'");
                  info.AppendLine("TextOffset (3 Byte):   " + DecimalAndHexAndBinary((ulong)record.TextOffset));
                  info.AppendLine("   Text:               '" + lbl.GetText(record.TextOffset, true) + "'");
                  firsthexadr += idx * lbl.RegionBlock.Recordsize;
                  hexlen = lbl.RegionBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_CityBlock:
               firsthexadr = lbl.CityBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("CityBlock: " + lbl.CityAndRegionOrCountryDataList.Count.ToString());
                  info.AppendLine("  Block:   " + lbl.CityBlock.ToString());
                  hexlen = (int)lbl.CityBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.CityAndRegionOrCountryRecord record = lbl.CityAndRegionOrCountryDataList[idx];
                  if (record.IsPOI) {
                     info.AppendLine("SubdivisionNumber (Bit 0..15) (3 Byte):    " + DecimalAndHexAndBinary(record.SubdivisionNumber));
                     info.AppendLine("POIIndex          (Bit 16..23):            " + DecimalAndHexAndBinary((ulong)record.POIIndex));
                  } else {
                     info.AppendLine("TextOffset (3 Byte):                       " + DecimalAndHexAndBinary((ulong)record.TextOffset));
                     info.AppendLine("   Text:                                   '" + lbl.GetText(record.TextOffset, true) + "'");
                  }
                  info.AppendLine("RegionOrCountryIndex (Bit 0..13) (2 Byte): " + DecimalAndHexAndBinary(record.RegionOrCountryIndex));
                  info.AppendLine("   RegionIsCountry   (Bit 14):             " + record.RegionIsCountry.ToString());
                  info.AppendLine("   IsPOI             (Bit 15):             " + record.IsPOI.ToString());
                  info.AppendLine("   Text:                                   '" + lbl.GetText(record.RegionIsCountry ?
                                                                                                      lbl.CountryDataList[record.RegionOrCountryIndex - 1].TextOffset :
                                                                                                      lbl.RegionAndCountryDataList[record.RegionOrCountryIndex - 1].TextOffset, true) + "'");
                  firsthexadr += idx * lbl.CityBlock.Recordsize;
                  hexlen = lbl.CityBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_POIIndexBlock:
               firsthexadr = lbl.POIIndexBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("PoiIndexDataList: " + lbl.PoiIndexDataList.Count.ToString());
                  info.AppendLine("  Block:          " + lbl.POIIndexBlock.ToString());
                  hexlen = (int)lbl.POIIndexBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.PoiIndexRecord record = lbl.PoiIndexDataList[idx];
                  info.AppendLine("POIIndex          (1 Byte): " + DecimalAndHexAndBinary(record.POIIndex));
                  info.AppendLine("SubdivisionNumber (2 Byte): " + DecimalAndHexAndBinary(record.SubdivisionNumber));
                  info.AppendLine("SubType           (1 Byte): " + DecimalAndHexAndBinary(record.SubType));
                  firsthexadr += idx * lbl.POIIndexBlock.Recordsize;
                  hexlen = lbl.POIIndexBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_POIPropertiesBlock:
               firsthexadr = lbl.POIPropertiesBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("POIPropertiesBlock: " + lbl.POIPropertiesList.Count.ToString());
                  info.AppendLine("  Block:            " + lbl.POIPropertiesBlock.ToString());
                  hexlen = (int)lbl.POIPropertiesBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.PoiRecord record = lbl.POIPropertiesList[idx];
                  info.AppendLine("RecordLength: " + DecimalAndHexAndBinary(record.DataLength(lbl.POIGlobalMask)));
                  info.AppendLine("TextOffset (Bit 0..22) (3 Byte): " + DecimalAndHexAndBinary(record.TextOffset));
                  info.AppendLine("   Text:                         '" + lbl.GetText(record.TextOffset, true) + "'");
                  info.AppendLine("LocalProperties (Bit 23):        " + record.HasLocalProperties.ToString());
                  hexlen = 3;
                  if (!record.HasLocalProperties) {
                     info.AppendLine("GlobalProperties:");
                  } else {
                     hexlen += 1;
                  }
                  info.AppendLine("   (1 Byte):               " + DecimalAndHexAndBinary((byte)record._internalPropMask));
                  info.AppendLine("   has_street_num (Bit 0): " + record.StreetNumberIsSet.ToString());
                  info.AppendLine("   has_street     (Bit 1): " + record.StreetIsSet.ToString());
                  info.AppendLine("   has_city       (Bit 2): " + record.CityIsSet.ToString());
                  info.AppendLine("   has_zip        (Bit 3): " + record.ZipIsSet.ToString());
                  info.AppendLine("   has_phone      (Bit 4): " + record.PhoneIsSet.ToString());
                  info.AppendLine("   has_exit       (Bit 5): " + record.ExitIsSet.ToString());
                  info.AppendLine("   has_tide_pred  (Bit 6): " + record.TidePredictionIsSet.ToString());
                  info.AppendLine("   unknown        (Bit 7): " + record.UnknownIsSet.ToString());

                  if (record.StreetNumberIsSet) {
                     if (record.StreetNumberIsCoded) {
                        info.AppendLine(string.Format("StreetNumber ({0} Byte): '{1}'", record._streetnumber_encoded.Length, record.StreetNumber));
                        hexlen += record._streetnumber_encoded.Length;
                     } else {
                        info.AppendLine("StreetNumberOffset (3 Byte): " + DecimalAndHexAndBinary(record.StreetNumberOffset));
                        info.AppendLine("   Text:                     '" + lbl.GetText(record.StreetNumberOffset, true) + "'");
                        hexlen += 3;
                     }
                  }
                  if (record.StreetIsSet) {
                     info.AppendLine("StreetOffset (3 Byte): " + DecimalAndHexAndBinary(record.StreetOffset));
                     info.AppendLine("   Text:               '" + lbl.GetText(record.StreetOffset, true) + "'");
                     hexlen += 3;
                  }
                  if (record.CityIsSet) {
                     info.AppendLine("CityIndex (" + (record.UseShortCityIndex ? "1" : "2") + " Byte): " + DecimalAndHexAndBinary(record.CityIndex));
                     GarminCore.Files.StdFile_LBL.CityAndRegionOrCountryRecord cityrecord = lbl.CityAndRegionOrCountryDataList[record.CityIndex - 1];
                     if (!cityrecord.IsPOI)
                        info.AppendLine("   City-Text:          '" + lbl.GetText(cityrecord.TextOffset, true) + "'");
                     if (cityrecord.RegionIsCountry)
                        info.AppendLine("   Country-Text:       '" + lbl.GetText(lbl.CountryDataList[cityrecord.RegionOrCountryIndex - 1].TextOffset, true) + "'");
                     else
                        info.AppendLine("   Region-Text:        '" + lbl.GetText(lbl.RegionAndCountryDataList[cityrecord.RegionOrCountryIndex - 1].TextOffset, true) + "'");
                     hexlen += record.UseShortCityIndex ? 1 : 2;
                  }
                  if (record.ZipIsSet) {
                     info.AppendLine("ZipIndex (" + (record.UseShortZipIndex ? "1" : "2") + " Byte): " + DecimalAndHexAndBinary(record.ZipIndex));
                     info.AppendLine("   Text:               '" + lbl.GetText(lbl.ZipDataList[record.ZipIndex - 1].TextOffset, true) + "'");
                     hexlen += record.UseShortZipIndex ? 1 : 2;
                  }
                  if (record.PhoneIsSet) {
                     if (record.PhoneNumberIsCoded) {
                        info.AppendLine(string.Format("StreetNumber ({0} Byte): '{1}'", record._phonenumber_encoded.Length, record.PhoneNumber));
                        hexlen += record._phonenumber_encoded.Length;
                     } else {
                        info.AppendLine("PhoneNumberOffset (3 Byte): " + DecimalAndHexAndBinary(record.PhoneNumberOffset));
                        info.AppendLine("   Text:                    '" + lbl.GetText(record.PhoneNumberOffset, true) + "'");
                        hexlen += 3;
                     }
                  }
                  if (record.ExitIsSet) {
                     info.AppendLine("ExitOffset (Bit 0..22) (3 Byte): " + DecimalAndHexAndBinary(record.ExitOffset));
                     info.AppendLine("   Text:                         '" + lbl.GetText(record.ExitOffset, true) + "'");
                     hexlen += 3;
                     info.AppendLine("ExitHighwayIndex       (" + (record.UseShortExitHighwayIndex ? "1" : "2") + " Byte): " + DecimalAndHexAndBinary(record.ExitHighwayIndex));
                     hexlen += record.UseShortExitHighwayIndex ? 1 : 2;
                     if (record.ExitIndexIsSet) {
                        info.AppendLine("ExitIndex              (" + (record.UseShortExitIndex ? "1" : "2") + " Byte): " + DecimalAndHexAndBinary(record.ExitIndex));
                        hexlen += record.UseShortExitIndex ? 1 : 2;
                     }
                  }

                  foreach (var item in lbl.POIPropertiesListOffsets) {
                     if (item.Value == idx) {
                        firsthexadr = lbl.POIPropertiesBlock.Offset + item.Key;
                        break;
                     }
                  }
               }
               break;

            case NodeContent.NodeType.LBL_POITypeIndexBlock:
               firsthexadr = lbl.POITypeIndexBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("POITypeIndexBlock: " + lbl.PoiTypeIndexDataList.Count.ToString());
                  info.AppendLine("  Block:           " + lbl.POITypeIndexBlock.ToString());
                  hexlen = (int)lbl.POITypeIndexBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.PoiTypeIndexRecord record = lbl.PoiTypeIndexDataList[idx];
                  info.AppendLine("POIType  (1 Byte): " + DecimalAndHexAndBinary(record.POIType));
                  info.AppendLine("StartIdx (3 Byte): " + DecimalAndHexAndBinary(record.StartIdx));
                  firsthexadr += idx * lbl.POITypeIndexBlock.Recordsize;
                  hexlen = lbl.POITypeIndexBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_ZipBlock:
               firsthexadr = lbl.ZipBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ZipBlock: " + lbl.ZipDataList.Count.ToString());
                  info.AppendLine("  Block:  " + lbl.ZipBlock.ToString());
                  hexlen = (int)lbl.ZipBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.ZipRecord record = lbl.ZipDataList[idx];
                  info.AppendLine("TextOffset (3 Byte): " + DecimalAndHexAndBinary(record.TextOffset));
                  info.AppendLine("   Text:            '" + lbl.GetText(record.TextOffset, true) + "'");
                  firsthexadr += idx * lbl.ZipBlock.Recordsize;
                  hexlen = lbl.ZipBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_HighwayWithExitBlock:
               firsthexadr = lbl.HighwayWithExitBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("HighwayWithExitBlock: " + lbl.HighwayWithExitList.Count.ToString());
                  info.AppendLine("  Block:              " + lbl.HighwayWithExitBlock.ToString());
                  hexlen = (int)lbl.HighwayWithExitBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.HighwayWithExitRecord record = lbl.HighwayWithExitList[idx];
                  info.AppendLine("TextOffset      (3 Byte): " + DecimalAndHexAndBinary(record.TextOffset));
                  info.AppendLine("   Text:                  '" + lbl.GetText(record.TextOffset, true) + "'");
                  info.AppendLine("FirstExitOffset (2 Byte): " + DecimalAndHexAndBinary(record.FirstExitOffset));
                  info.AppendLine("Unknown1        (1 Byte): " + DecimalAndHexAndBinary(record.Unknown1));
                  firsthexadr += idx * lbl.HighwayWithExitBlock.Recordsize;
                  hexlen = lbl.HighwayWithExitBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_ExitBlock:
               firsthexadr = lbl.ExitBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("ExitBlock: " + lbl.ExitList.Count.ToString());
                  info.AppendLine("  Block:   " + lbl.ExitBlock.ToString());
                  hexlen = (int)lbl.HighwayWithExitBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.ExitRecord record = lbl.ExitList[idx];
                  info.AppendLine("TextOffset    (Bit 0..21) (3 Byte): " + DecimalAndHexAndBinary(record.TextOffset));
                  info.AppendLine("   Text:                            '" + lbl.GetText(record.TextOffset, true) + "'");
                  info.AppendLine("LastFacilitie (Bit 23):             " + record.LastFacilitie.ToString());
                  info.AppendLine("Type          (Bit 0..3)  (1 Byte): " + DecimalAndHexAndBinary(record.Type));
                  info.AppendLine("Direction     (Bit 5..7)  (1 Byte): " + DecimalAndHexAndBinary(record.Direction));
                  firsthexadr += idx * lbl.ExitBlock.Recordsize;
                  hexlen = lbl.ExitBlock.Recordsize;
               }
               break;

            case NodeContent.NodeType.LBL_HighwayExitBlock:
               firsthexadr = lbl.HighwayExitBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("HighwayWithExitBlock: " + lbl.HighwayExitDefList.Count.ToString());
                  info.AppendLine("  Block:              " + lbl.HighwayExitBlock.ToString());
                  hexlen = (int)lbl.HighwayExitBlock.Length;
               } else {
                  GarminCore.Files.StdFile_LBL.HighwayExitDefRecord record = lbl.HighwayExitDefList[idx];
                  info.AppendLine("Unknown1        (1 Byte): " + DecimalAndHexAndBinary(record.Unknown1));
                  info.AppendLine("RegionIndex     (2 Byte): " + DecimalAndHexAndBinary(record.RegionIndex));
                  info.AppendLine("Exits                   : " + DecimalAndHexAndBinary(record.ExitList.Count));
                  firsthexadr += idx * lbl.HighwayExitBlock.Recordsize;
                  hexlen = 3 + 3 * record.ExitList.Count;
               }
               break;

            case NodeContent.NodeType.LBL_SortDescriptorDefBlock:
               firsthexadr = lbl.SortDescriptorDefBlock.Offset;
               if (idx < 0) {
                  info.AppendLine("SortDescriptorDefBlock");
                  info.AppendLine("  Block:          " + lbl.SortDescriptorDefBlock.ToString());
                  info.AppendLine("  SortDescriptor: '" + lbl.SortDescriptor + "'");
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
