using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataSRT : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_SRT srt, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, srt, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, srt, "Garmin SRT-Header"));
         if (srt.ContentsBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.SRT_ContentsBlock, srt, "ContentsBlock");
         if (srt.DescriptionBlock.Length > 0)
            AppendNode(tn, NodeContent.NodeType.SRT_DescriptionBlock, srt, "DescriptionBlock");
         if (srt.CharacterLookupTableBlock.Length > 0)
            AppendNode(AppendNode(tn, NodeContent.NodeType.SRT_CharacterLookupTableBlock, srt, "CharacterLookupTableBlock"));

      }

      /// <summary>
      /// Knoten für eine Section anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      /// <param name="nodetype"></param>
      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_SRT srt, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);
         int count;
         TreeNode parent;

         switch (nodetype) {
            case NodeContent.NodeType.SRT_CharacterLookupTableBlock:
               parent = AppendNode(tn, NodeContent.NodeType.SRT_CharTabBlock, srt, "CharTabBlock");
               count = (int)(srt.Sortheader.CharTabBlock.Length / srt.Sortheader.CharTabBlock.Recordsize);
               for (int i = 0; i < count; i++)
                  AppendNode(parent, NodeContent.NodeType.Index, i, "Char " + i.ToString());

               parent = AppendNode(tn, NodeContent.NodeType.SRT_ExpansionsBlock, srt, "ExpansionsBlock");
               count = (int)(srt.Sortheader.ExpansionsBlock.Length / srt.Sortheader.ExpansionsBlock.Recordsize);
               for (int i = 0; i < count; i++)
                  AppendNode(parent, NodeContent.NodeType.Index, i, "Expansion " + i.ToString());

               break;

         }
      }

      /// <summary>
      /// Knoten für einen Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_SRT srt) {
      }

      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_SRT srt) {
         info.AppendLine("Unknown 0x15:  (2 Byte): " + HexString(srt.Unknown_x15));
         info.AppendLine("ContentsBlock: (6 Byte): " + srt.ContentsBlock.ToString());
         info.AppendLine("Unknown 0x1D:  (" + srt.Unknown_x1D.Length.ToString() + " Byte): " + HexString(srt.Unknown_x1D));
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
         GarminCore.Files.StdFile_SRT srt = filedata.GetGarminFileAsSRT();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;

         switch (nodetype) {
            case NodeContent.NodeType.SRT_ContentsBlock:
               firsthexadr = srt.ContentsBlock.Offset;
               hexlen = srt.ContentsBlock.Length;
               info.AppendLine("ContentsBlock:                (6 Byte): " + srt.ContentsBlock.ToString());
               info.AppendLine("   DescriptionBlock:                    " + srt.DescriptionBlock.ToString());
               info.AppendLine("   CharacterLookupTableBlock:           " + srt.CharacterLookupTableBlock.ToString());
               break;

            case NodeContent.NodeType.SRT_DescriptionBlock:
               firsthexadr = srt.DescriptionBlock.Offset;
               hexlen = (int)srt.DescriptionBlock.Length;
               info.AppendLine("Description: (" + srt.DescriptionBlock.Length.ToString() + " Byte): '" + srt.Description + "'");
               break;

            case NodeContent.NodeType.SRT_CharacterLookupTableBlock:
               firsthexadr = srt.CharacterLookupTableBlock.Offset;
               hexlen = (int)srt.CharacterLookupTableBlock.Length;
               info.AppendLine("Headerlength    (2 Byte): " + DecimalAndHexAndBinary(srt.Sortheader.Headerlength));
               info.AppendLine("Id1             (2 Byte): " + DecimalAndHexAndBinary(srt.Sortheader.Id1));
               info.AppendLine("Id2             (2 Byte): " + DecimalAndHexAndBinary(srt.Sortheader.Id2));
               info.AppendLine("Codepage        (2 Byte): " + DecimalAndHexAndBinary(srt.Sortheader.Codepage));
               info.AppendLine("Unknown1        (" + srt.Sortheader.Unknown1.Length.ToString() + " Byte): " + HexString(srt.Sortheader.Unknown1));
               info.AppendLine("Unknown2        (" + srt.Sortheader.Unknown2.Length.ToString() + " Byte): " + HexString(srt.Sortheader.Unknown2));
               info.AppendLine("CharTabBlock    (10 Byte): " + srt.Sortheader.CharTabBlock.ToString());
               info.AppendLine("Unknown3        (" + srt.Sortheader.Unknown3.Length.ToString() + " Byte): " + HexString(srt.Sortheader.Unknown3));
               info.AppendLine("ExpansionsBlock (10 Byte): " + srt.Sortheader.ExpansionsBlock.ToString());
               info.AppendLine("Unknown4        (" + srt.Sortheader.Unknown4.Length.ToString() + " Byte): " + HexString(srt.Sortheader.Unknown4));
               info.AppendLine("CharTabOffset   (4 Byte): " + DecimalAndHexAndBinary(srt.Sortheader.CharTabOffset));
               info.AppendLine("Unknown5        (" + srt.Sortheader.Unknown5.Length.ToString() + " Byte): " + HexString(srt.Sortheader.Unknown5));
               info.AppendLine("Unknown6        (" + srt.Sortheader.Unknown6.Length.ToString() + " Byte): " + HexString(srt.Sortheader.Unknown6));
               break;

            case NodeContent.NodeType.SRT_CharTabBlock:
               firsthexadr = srt.Sortheader.CharTabBlock.Offset;
               hexlen = (int)srt.Sortheader.CharTabBlock.Length;
               if (idx < 0)
                  info.AppendLine("Count: " + (srt.Sortheader.CharTabBlock.Length / srt.Sortheader.CharTabBlock.Recordsize).ToString());
               else {
                  hexlen = srt.Sortheader.CharTabBlock.Recordsize;
                  firsthexadr = srt.Sortheader.CharTabBlock.Offset + idx * hexlen;
               }
               break;

            case NodeContent.NodeType.SRT_ExpansionsBlock:
               firsthexadr = srt.Sortheader.ExpansionsBlock.Offset;
               hexlen = (int)srt.Sortheader.ExpansionsBlock.Length;
               if (idx < 0)
                  info.AppendLine("Count: " + (srt.Sortheader.ExpansionsBlock.Length / srt.Sortheader.ExpansionsBlock.Recordsize).ToString());
               else {
                  hexlen = srt.Sortheader.ExpansionsBlock.Recordsize;
                  firsthexadr = srt.Sortheader.ExpansionsBlock.Offset + idx * hexlen;
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
