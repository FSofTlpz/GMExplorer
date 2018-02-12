using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   public partial class HexWin : UserControl {

      /*
System.Text.Encoding.ASCII.GetString(new byte[]{0,1,2,3,4,5,6,7,8,9})
"\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t"
System.Text.Encoding.ASCII.GetString(new byte[]{10,11,12,13,14,15,16,17,18,19})
"\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013"
System.Text.Encoding.ASCII.GetString(new byte[]{20,21,22,23,24,25,26,27,28,29})
"\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d"
System.Text.Encoding.ASCII.GetString(new byte[]{30,31,32,33,34,35,36,37,38,39})
"\u001e\u001f !\"#$%&'"
System.Text.Encoding.ASCII.GetString(new byte[]{120,121,122,123,124,125,126,127,128,129})
"xyz{|}~\u007f??"
System.Text.Encoding.ASCII.GetString(new byte[]{130,131,132,133,134,135,136,137,138,139})
"??????????"


System.Text.Encoding.GetEncoding(1252).GetString(new byte[]{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35})
"\0\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f !\"#"
System.Text.Encoding.GetEncoding(1252).GetString(new byte[]{120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149})
"xyz{|}~\u007f€\u0081‚ƒ„…†‡ˆ‰Š‹Œ\u008dŽ\u008f\u0090‘’“”•"
System.Text.Encoding.GetEncoding(1252).GetString(new byte[]{150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199})
"–—˜™š›œ\u009džŸ ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇ"
System.Text.Encoding.GetEncoding(1252).GetString(new byte[]{200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255})})
"ÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ"

\0
\u0001
\u0002
\u0003
\u0004
\u0005
\u0006
\a\b\t\n\v\f\r
\u000e
\u000f
\u0010
\u0011
\u0012
\u0013
\u0014
\u0015
\u0016
\u0017
\u0018
\u0019
\u001a
\u001b
\u001c
\u001d
\u001e
\u001f

\u007f
\u0081
\u008d
\u008f
\u0090
       */

      const byte DUMMYBYTE = 0x2e;

      int _BytesPerLine;

      Encoding hexenc;


      public HexWin() {
         InitializeComponent();
         BytesPerLine = 16;
         ShowAsText = true;
         hexenc = Encoding.GetEncoding(1252);
      }

      private void HexWin_Load(object sender, EventArgs e) {
         textBoxHex.Clear();
      }

      /// <summary>
      /// Anzahl der angezeigten Bytes je Zeile (immer ein Vielfaches von 8)
      /// </summary>
      public int BytesPerLine {
         get {
            return _BytesPerLine;
         }
         set {
            _BytesPerLine = Math.Max(8, 8 * (value / 8));
         }
      }

      /// <summary>
      /// Bytes auch als Text anzeigen
      /// </summary>
      public bool ShowAsText { get; set; }

      /// <summary>
      /// Fensterinhalt löschen
      /// </summary>
      public void ClearContent() {
         textBoxHex.Clear();
      }

      /// <summary>
      /// neue Daten anzeigen
      /// </summary>
      /// <param name="content">Daten</param>
      /// <param name="startadr">wird als Start-Adr. angezeigt</param>
      public void SetContent(IList<byte> content, ulong startadr = 0) {
         ulong lastadr = startadr + (ulong)content.Count - 1;
         int RowHeadLength = lastadr.ToString("x").Length;

         StringBuilder line = new StringBuilder();
         line.Append(' ', RowHeadLength + 1);
         for (int i = 0; i < BytesPerLine; i++)
            line.AppendFormat("{0:X2} ", i);
         line.AppendLine();

         string lienadrfmt = "{0:X" + RowHeadLength.ToString() + "}";
         ulong lineadr = (ulong)BytesPerLine * (startadr / (ulong)BytesPerLine);
         for (int i = (int)(lineadr - startadr); i < content.Count;) {
            line.AppendFormat(lienadrfmt, lineadr);   // Startadr. der Zeile ausgeben

            for (int j = 0; j < BytesPerLine; j++, i++)  // Hex-Daten ausgeben
               if (0 <= i && i < content.Count)
                  line.AppendFormat(" {0:X2}", content[i]);
               else
                  line.Append("   ");

            if (ShowAsText) {
               i -= BytesPerLine;
               byte[] tmp = new byte[BytesPerLine];
               for (int j = 0; j < BytesPerLine; j++, i++) {
                  if (0 <= i && i < content.Count) {
                     if (content[i] >= 0x20) {
                        tmp[j] = content[i];
                        if (tmp[j] == 0x7f ||
                            tmp[j] == 0x81 ||
                            tmp[j] == 0x8d ||
                            tmp[j] == 0x8f ||
                            tmp[j] == 0x90 ||
                            tmp[j] == 0x9D)
                           tmp[j] = 0x20;
                     } else
                        tmp[j] = DUMMYBYTE;
                  } else
                     tmp[j] = 0x20;
               }
               line.Append(' ');
               line.Append(hexenc.GetString(tmp));
            }
            line.AppendLine();
            lineadr += (ulong)BytesPerLine;
         }

         textBoxHex.AppendText(line.ToString());
         textBoxHex.Select(0, 0);
         textBoxHex.ScrollToCaret();
      }

   }
}
