using System.Text;

namespace GMExplorer {
   class AllSubdivInfo {

      GarminCore.Files.StdFile_RGN rgn;
      GarminCore.BinaryReaderWriter binreader;
      int subdividx;

      public GarminCore.Files.StdFile_RGN.SubdivData SubdivData { get; private set; }
      public GarminCore.Files.StdFile_TRE.SubdivInfoBasic SubdivfInfo { get; private set; }
      public ushort[] OffsetTab { get; private set; }
      public int MaplevelNo { get; private set; }

      GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent[] OffsetContent;


      public AllSubdivInfo(GarminCore.Files.StdFile_RGN rgn, GarminCore.BinaryReaderWriter binreader, int subdividx) {
         this.subdividx = subdividx;
         this.binreader = binreader;
         this.rgn = rgn;

         SubdivData = rgn.SubdivList[subdividx];

         int offsets = 0; // Anzahl der 2-Byte-Offsets (die erste Objektart benötigt keinen Offset)
         if (rgn.TREFile != null &&
             rgn.TREFile.SubdivInfoList.Count == rgn.SubdivList.Count) {
            SubdivfInfo = rgn.TREFile.SubdivInfoList[subdividx];

            if (SubdivData.PointList.Count > 0)
               offsets++;
            if (SubdivData.IdxPointList.Count > 0)
               offsets++;
            if (SubdivData.LineList.Count > 0)
               offsets++;
            if (SubdivData.AreaList.Count > 0)
               offsets++;
            OffsetTab = new ushort[offsets];
            OffsetContent = new GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent[offsets];

            // Offsettabelle füllen
            if (offsets > 0)
               OffsetTab[0] = (ushort)(2 * (OffsetTab.Length - 1));
            binreader.Seek(rgn.SubdivContentBlock.Offset + SubdivfInfo.Data.Offset);
            for (int i = 1; i < offsets; i++)
               OffsetTab[i] = binreader.ReadUInt16();

            offsets = 0;
            if ((SubdivfInfo.Content & GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi) != 0)
               OffsetContent[offsets++] = GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi;
            if ((SubdivfInfo.Content & GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi) != 0)
               OffsetContent[offsets++] = GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi;
            if ((SubdivfInfo.Content & GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.line) != 0)
               OffsetContent[offsets++] = GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.line;
            if ((SubdivfInfo.Content & GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area) != 0)
               OffsetContent[offsets++] = GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area;
         }

         int count = 0;
         for (int i = 0; i < rgn.TREFile.MaplevelList.Count; i++) {
            count += rgn.TREFile.MaplevelList[i].SubdivInfos;
            if (subdividx < count) {
               MaplevelNo = i;
               break;
            }
         }

      }


      /// <summary>
      /// liefert den Datenbereich für den SubdivInfo-Tabelleneintrag (in TRE)
      /// </summary>
      /// <param name="idx"></param>
      /// <returns></returns>
      public GarminCore.DataBlock GetSubdivInfoBlock(int idx = -1) {
         if (idx < 0)
            idx = subdividx;
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         for (int i = 0; i < idx; i++)
            block.Offset += rgn.TREFile.SubdivInfoList[i] is GarminCore.Files.StdFile_TRE.SubdivInfo ?
                                                            GarminCore.Files.StdFile_TRE.SubdivInfo.DataLength :
                                                            GarminCore.Files.StdFile_TRE.SubdivInfoBasic.DataLength;
         block.Length = (rgn.TREFile.SubdivInfoList[idx]) is GarminCore.Files.StdFile_TRE.SubdivInfo ?
                                                            GarminCore.Files.StdFile_TRE.SubdivInfo.DataLength :
                                                            GarminCore.Files.StdFile_TRE.SubdivInfoBasic.DataLength;
         return block;
      }

      /// <summary>
      /// liefert den Datenbereich für den SubdivData-Bereich (in RGN)
      /// </summary>
      /// <param name="idx"></param>
      /// <returns></returns>
      public GarminCore.DataBlock GetSubdivDataBlock(int idx = -1) {
         if (idx < 0)
            idx = subdividx;
         return new GarminCore.DataBlock(rgn.TREFile.SubdivInfoList[idx].Data);
      }

      /// <summary>
      /// liefert den Datenbereich für die ExtPoint der Subdiv (Info aus TRE, Block in RGN)
      /// </summary>
      /// <param name="idx"></param>
      /// <returns></returns>
      public GarminCore.DataBlock GetExtPointBlock(int idx = -1) {
         if (idx < 0)
            idx = subdividx;
         if (rgn.TREFile != null && rgn.TREFile.ExtPointBlock4Subdiv.ContainsKey(idx))
            return new GarminCore.DataBlock(rgn.TREFile.ExtPointBlock4Subdiv[idx]);
         return null;
      }

      /// <summary>
      /// liefert den Datenbereich für die ExtLine der Subdiv (Info aus TRE, Block in RGN)
      /// </summary>
      /// <param name="idx"></param>
      /// <returns></returns>
      public GarminCore.DataBlock GetExtLineBlock(int idx = -1) {
         if (idx < 0)
            idx = subdividx;
         if (rgn.TREFile != null && rgn.TREFile.ExtLineBlock4Subdiv.ContainsKey(idx))
            return new GarminCore.DataBlock(rgn.TREFile.ExtLineBlock4Subdiv[idx]);
         return null;
      }

      /// <summary>
      /// liefert den Datenbereich für die ExtArea der Subdiv (Info aus TRE, Block in RGN)
      /// </summary>
      /// <param name="idx"></param>
      /// <returns></returns>
      public GarminCore.DataBlock GetExtAreaBlock(int idx = -1) {
         if (idx < 0)
            idx = subdividx;
         if (rgn.TREFile != null && rgn.TREFile.ExtAreaBlock4Subdiv.ContainsKey(idx))
            return new GarminCore.DataBlock(rgn.TREFile.ExtAreaBlock4Subdiv[idx]);
         return null;
      }


      /// <summary>
      /// liefert den Offset für einen Objekttyp oder einen negativen Wert
      /// </summary>
      /// <param name="contenttype"></param>
      /// <returns></returns>
      public int GetOffset4ObjectType(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent contenttype) {
         for (int i = 0; i < OffsetContent.Length; i++) {
            if (OffsetContent[i] == contenttype)
               return OffsetTab[i];
         }
         return -1;
      }

      /// <summary>
      /// liefert den Datenblock in RGN bzgl. des SubdivContentBlock in RGN für das Objekt
      /// </summary>
      /// <param name="contenttype"></param>
      /// <param name="idx"></param>
      /// <returns></returns>
      public GarminCore.DataBlock GetDataBlock4Object(GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent contenttype, int idx) {
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         block.Offset = SubdivfInfo.Data.Offset;
         block.Offset += (uint)GetOffset4ObjectType(contenttype);
         switch (contenttype) {
            case GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.poi:
               for (int i = 0; i < idx; i++)
                  block.Offset += SubdivData.PointList[i].DataLength;
               block.Length = SubdivData.PointList[idx].DataLength;
               break;

            case GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.idxpoi:
               for (int i = 0; i < idx; i++)
                  block.Offset += SubdivData.IdxPointList[i].DataLength;
               block.Length = SubdivData.IdxPointList[idx].DataLength;
               break;

            case GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.line:
               for (int i = 0; i < idx; i++)
                  block.Offset += SubdivData.LineList[i].DataLength;
               block.Length = SubdivData.LineList[idx].DataLength;
               break;

            case GarminCore.Files.StdFile_TRE.SubdivInfoBasic.SubdivContent.area:
               for (int i = 0; i < idx; i++)
                  block.Offset += SubdivData.AreaList[i].DataLength;
               block.Length = SubdivData.AreaList[idx].DataLength;
               break;

         }
         return block;
      }

      public GarminCore.DataBlock GetDataBlock4ExtPoint(int idx) {
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         block.Offset = 0;
         for (int i = 0; i < idx; i++)
            block.Offset += SubdivData.ExtPointList[i].DataLength;
         block.Length = SubdivData.ExtPointList[idx].DataLength;
         return block;
      }

      public GarminCore.DataBlock GetDataBlock4ExtLine(int idx) {
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         block.Offset = 0;
         for (int i = 0; i < idx; i++)
            block.Offset += SubdivData.ExtLineList[i].DataLength;
         block.Length = SubdivData.ExtLineList[idx].DataLength;
         return block;
      }

      public GarminCore.DataBlock GetDataBlock4ExtArea(int idx) {
         GarminCore.DataBlock block = new GarminCore.DataBlock();
         block.Offset = 0;
         for (int i = 0; i < idx; i++)
            block.Offset += SubdivData.ExtAreaList[i].DataLength;
         block.Length = SubdivData.ExtAreaList[idx].DataLength;
         return block;
      }

      public GarminCore.Files.StdFile_RGN.RawPointData GetPoint(int idx) {
         return SubdivData.PointList[idx];
      }

      public GarminCore.Files.StdFile_RGN.RawPointData GetIdxPoint(int idx) {
         return SubdivData.IdxPointList[idx];
      }

      public GarminCore.Files.StdFile_RGN.RawPolyData GetLine(int idx) {
         return SubdivData.LineList[idx];
      }

      public GarminCore.Files.StdFile_RGN.RawPolyData GetArea(int idx) {
         return SubdivData.AreaList[idx];
      }

      public GarminCore.Files.StdFile_RGN.ExtRawPointData GetExtPoint(int idx) {
         return SubdivData.ExtPointList[idx];
      }

      public GarminCore.Files.StdFile_RGN.ExtRawPolyData GetExtLine(int idx) {
         return SubdivData.ExtLineList[idx];
      }

      public GarminCore.Files.StdFile_RGN.ExtRawPolyData GetExtArea(int idx) {
         return SubdivData.ExtAreaList[idx];
      }


      /// <summary>
      /// liefert die Extrabits eines Polygons als Zeichenkette mit 0 und 1
      /// </summary>
      /// <param name="poly"></param>
      /// <returns></returns>
      static public string ExtraBits(GarminCore.Files.StdFile_RGN.RawPolyData poly) {
         StringBuilder sb = new StringBuilder();
         if (poly.ExtraBit != null)
            for (int i = 0; i < poly.ExtraBit.Count; i++)
               sb.Append(poly.ExtraBit[i] ? "1" : "0");
         return sb.ToString();
      }


   }
}
