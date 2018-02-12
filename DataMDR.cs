using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class DataMDR : Data {

      /// <summary>
      /// Knoten für eine logische oder physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre">wenn null, wird das Objekt erzeugt</param>
      /// <param name="binreader"></param>
      public static void AppendChildNodes(TreeNode tn, GarminCore.Files.StdFile_MDR mdr, GarminCore.BinaryReaderWriter binreader) {
         DeleteDummyChildNode(tn);

         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminCommonHeader, mdr, "Garmin Common Header"));
         AppendNode(AppendNode(tn, NodeContent.NodeType.GarminSpecialHeader, mdr, "Garmin MDR-Header"));
         if (mdr.Mdr1.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR1, mdr, "MDR1");
         if (mdr.Mdr2.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR2, mdr, "MDR2");
         if (mdr.Mdr3.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR3, mdr, "MDR3");
         if (mdr.Mdr4.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR4, mdr, "MDR4");
         if (mdr.Mdr5.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR5, mdr, "MDR5");
         if (mdr.Mdr6.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR6, mdr, "MDR6");
         if (mdr.Mdr7.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR7, mdr, "MDR7");
         if (mdr.Mdr8.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR8, mdr, "MDR8");
         if (mdr.Mdr9.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR9, mdr, "MDR9");
         if (mdr.Mdr10.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR10, mdr, "MDR10");
         if (mdr.Mdr11.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR11, mdr, "MDR11");
         if (mdr.Mdr12.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR12, mdr, "MDR12");
         if (mdr.Mdr13.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR13, mdr, "MDR13");
         if (mdr.Mdr14.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR14, mdr, "MDR14");
         if (mdr.Mdr15.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR15, mdr, "MDR15");
         if (mdr.Mdr16.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR16, mdr, "MDR16");
         if (mdr.Mdr17.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR17, mdr, "MDR17");
         if (mdr.Mdr18.Length > 0) AppendNode(tn, NodeContent.NodeType.MDR_MDR18, mdr, "MDR18");

      }

      /// <summary>
      /// Knoten für eine Section anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      /// <param name="nodetype"></param>
      public static void AppendChildNodesOn_Sections(TreeNode tn, GarminCore.Files.StdFile_MDR mdr, NodeContent.NodeType nodetype) {
         DeleteDummyChildNode(tn);


      }

      /// <summary>
      /// Knoten für einen Header anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="tre"></param>
      public static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile_MDR mdr) {
      }

      public static void SpecialHeader(StringBuilder info, GarminCore.Files.StdFile_MDR mdr) {
         info.AppendLine("Codepage       (2 Byte): " + DecimalAndHexAndBinary(mdr.Codepage));
         info.AppendLine("SortId1        (2 Byte): " + DecimalAndHexAndBinary(mdr.SortId1));
         info.AppendLine("SortId2        (2 Byte): " + DecimalAndHexAndBinary(mdr.SortId2));
         info.AppendLine("Unknown 0x1B:  (" + mdr.Unknown_x1B.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x1B));
         info.AppendLine("Mdr1           (10 Byte): " + mdr.Mdr1.ToString());
         info.AppendLine("Unknown 0x27:  (" + mdr.Unknown_x27.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x27));
         info.AppendLine("Mdr2           (10 Byte): " + mdr.Mdr2.ToString());
         info.AppendLine("Unknown 0x35:  (" + mdr.Unknown_x35.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x35));
         info.AppendLine("Mdr3           (10 Byte): " + mdr.Mdr3.ToString());
         info.AppendLine("Unknown 0x43:  (" + mdr.Unknown_x43.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x43));
         info.AppendLine("Mdr4           (10 Byte): " + mdr.Mdr4.ToString());
         info.AppendLine("Unknown 0x51:  (" + mdr.Unknown_x51.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x51));
         info.AppendLine("Mdr5           (10 Byte): " + mdr.Mdr5.ToString());
         info.AppendLine("Unknown 0x5F:  (" + mdr.Unknown_x5F.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x5F));
         info.AppendLine("Mdr6           (10 Byte): " + mdr.Mdr6.ToString());
         info.AppendLine("Unknown 0x6D:  (" + mdr.Unknown_x6D.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x6D));
         info.AppendLine("Mdr7           (10 Byte): " + mdr.Mdr7.ToString());
         info.AppendLine("Unknown 0x7B:  (" + mdr.Unknown_x7B.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x7B));
         info.AppendLine("Mdr8           (10 Byte): " + mdr.Mdr8.ToString());
         info.AppendLine("Unknown 0x89:  (" + mdr.Unknown_x89.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x89));
         info.AppendLine("Mdr9           (10 Byte): " + mdr.Mdr9.ToString());
         info.AppendLine("Unknown 0x97:  (" + mdr.Unknown_x97.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x97));
         info.AppendLine("Mdr10          (8 Byte): " + mdr.Mdr10.ToString());
         info.AppendLine("Unknown 0xA3:  (" + mdr.Unknown_xA3.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xA3));
         info.AppendLine("Mdr11          (10 Byte): " + mdr.Mdr11.ToString());
         info.AppendLine("Unknown 0xB1:  (" + mdr.Unknown_xB1.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xB1));
         info.AppendLine("Mdr12          (10 Byte): " + mdr.Mdr12.ToString());
         info.AppendLine("Unknown 0xBF:  (" + mdr.Unknown_xBF.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xBF));
         info.AppendLine("Mdr13          (10 Byte): " + mdr.Mdr13.ToString());
         info.AppendLine("Unknown 0xCD:  (" + mdr.Unknown_xCD.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xCD));
         info.AppendLine("Mdr14          (10 Byte): " + mdr.Mdr14.ToString());
         info.AppendLine("Unknown 0xDB:  (" + mdr.Unknown_xDB.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xDB));
         info.AppendLine("Mdr15          (8 Byte): " + mdr.Mdr15.ToString());
         info.AppendLine("Unknown 0xE7   (1 Byte): " + DecimalAndHexAndBinary(mdr.Unknown_xE7));
         info.AppendLine("Mdr16          (10 Byte): " + mdr.Mdr16.ToString());
         info.AppendLine("Unknown 0xF2:  (" + mdr.Unknown_xF2.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xF2));
         info.AppendLine("Mdr17          (8 Byte): " + mdr.Mdr17.ToString());
         info.AppendLine("Unknown 0xFE:  (" + mdr.Unknown_xFE.Length.ToString() + " Byte): " + HexString(mdr.Unknown_xFE));
         info.AppendLine("Mdr18          (10 Byte): " + mdr.Mdr18.ToString());
         info.AppendLine("Unknown 0x10C: (" + mdr.Unknown_x10C.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x10C));
         if (mdr.Headerlength > 0x110)
            info.AppendLine("Unknown 0x110: (" + mdr.Unknown_x110.Length.ToString() + " Byte): " + HexString(mdr.Unknown_x110));

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
         GarminCore.Files.StdFile_MDR mdr = filedata.GetGarminFileAsMDR();
         int hexlen = 0;
         firsthexadr = 0;
         hex = null;

         switch (nodetype) {
            case NodeContent.NodeType.MDR_MDR1:
               firsthexadr = mdr.Mdr1.Offset;
               hexlen = (int)mdr.Mdr1.Length;
               info.AppendLine("Mdr1: " + mdr.Mdr1.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR2:
               firsthexadr = mdr.Mdr2.Offset;
               hexlen = (int)mdr.Mdr2.Length;
               info.AppendLine("Mdr2: " + mdr.Mdr2.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR3:
               firsthexadr = mdr.Mdr3.Offset;
               hexlen = (int)mdr.Mdr3.Length;
               info.AppendLine("Mdr3: " + mdr.Mdr3.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR4:
               firsthexadr = mdr.Mdr4.Offset;
               hexlen = (int)mdr.Mdr4.Length;
               info.AppendLine("Mdr4: " + mdr.Mdr4.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR5:
               firsthexadr = mdr.Mdr5.Offset;
               hexlen = (int)mdr.Mdr5.Length;
               info.AppendLine("Mdr5: " + mdr.Mdr5.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR6:
               firsthexadr = mdr.Mdr6.Offset;
               hexlen = (int)mdr.Mdr6.Length;
               info.AppendLine("Mdr6: " + mdr.Mdr6.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR7:
               firsthexadr = mdr.Mdr7.Offset;
               hexlen = (int)mdr.Mdr7.Length;
               info.AppendLine("Mdr7: " + mdr.Mdr7.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR8:
               firsthexadr = mdr.Mdr8.Offset;
               hexlen = (int)mdr.Mdr8.Length;
               info.AppendLine("Mdr8: " + mdr.Mdr8.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR9:
               firsthexadr = mdr.Mdr9.Offset;
               hexlen = (int)mdr.Mdr9.Length;
               info.AppendLine("Mdr9: " + mdr.Mdr9.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR10:
               firsthexadr = mdr.Mdr10.Offset;
               hexlen = (int)mdr.Mdr10.Length;
               info.AppendLine("Mdr10: " + mdr.Mdr10.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR11:
               firsthexadr = mdr.Mdr11.Offset;
               hexlen = (int)mdr.Mdr11.Length;
               info.AppendLine("Mdr11: " + mdr.Mdr11.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR12:
               firsthexadr = mdr.Mdr12.Offset;
               hexlen = (int)mdr.Mdr12.Length;
               info.AppendLine("Mdr12: " + mdr.Mdr12.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR13:
               firsthexadr = mdr.Mdr13.Offset;
               hexlen = (int)mdr.Mdr13.Length;
               info.AppendLine("Mdr13: " + mdr.Mdr13.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR14:
               firsthexadr = mdr.Mdr14.Offset;
               hexlen = (int)mdr.Mdr14.Length;
               info.AppendLine("Mdr14: " + mdr.Mdr14.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR15:
               firsthexadr = mdr.Mdr15.Offset;
               hexlen = (int)mdr.Mdr15.Length;
               info.AppendLine("Mdr15: " + mdr.Mdr15.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR16:
               firsthexadr = mdr.Mdr16.Offset;
               hexlen = (int)mdr.Mdr16.Length;
               info.AppendLine("Mdr16: " + mdr.Mdr16.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR17:
               firsthexadr = mdr.Mdr17.Offset;
               hexlen = (int)mdr.Mdr17.Length;
               info.AppendLine("Mdr17: " + mdr.Mdr17.ToString());
               break;

            case NodeContent.NodeType.MDR_MDR18:
               firsthexadr = mdr.Mdr18.Offset;
               hexlen = (int)mdr.Mdr18.Length;
               info.AppendLine("Mdr18: " + mdr.Mdr18.ToString());
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
