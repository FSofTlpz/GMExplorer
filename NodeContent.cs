using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMExplorer {

   public class NodeContent {

      public enum NodeType {
         /// <summary>
         /// leerer Knoten
         /// </summary>
         Dummy,
         /// <summary>
         /// physische Datei (im Dateisystem)
         /// </summary>
         PhysicalFile,
         /// <summary>
         /// logische Datei (in einer Garmin-Datei)
         /// </summary>
         LogicalFile,
         /// <summary>
         /// Datenbereich in einer Datei
         /// </summary>
         DataRange,
         /// <summary>
         /// "sonstige" Daten eines <see cref="GarminCore.SimpleFilesystem"/>
         /// </summary>
         SimpleFilesystem,

         GarminCommonHeader,
         GarminSpecialHeader,
         /// <summary>
         /// Index 0, .. (i.A. für eine Liste)
         /// </summary>
         Index,

         TRE_DescriptionList,
         TRE_MaplevelBlock,
         TRE_SubdivisionBlock,
         TRE_CopyrightBlock,
         TRE_LineOverviewBlock,
         TRE_AreaOverviewBlock,
         TRE_PointOverviewBlock,
         TRE_ExtTypeOffsetsBlock,
         TRE_ExtTypeOverviewsBlock,
         TRE_UnknownBlock_xAE,
         TRE_UnknownBlock_xBC,
         TRE_UnknownBlock_xE3,

         LBL_TextBlock,
         LBL_CountryBlock,
         LBL_RegionBlock,
         LBL_CityBlock,
         LBL_POIIndexBlock,
         LBL_POIPropertiesBlock,
         LBL_POITypeIndexBlock,
         LBL_ZipBlock,
         LBL_HighwayWithExitBlock,
         LBL_ExitBlock,
         LBL_HighwayExitBlock,
         LBL_SortDescriptorDefBlock,
         LBL_Lbl13Block,
         LBL_TidePredictionBlock,
         LBL_UnknownBlock_0xD0,
         LBL_UnknownBlock_0xDE,
         LBL_UnknownBlock_0xEC,
         LBL_UnknownBlock_0xFA,
         LBL_UnknownBlock_0x108,
         LBL_UnknownBlock_0x116,
         LBL_UnknownBlock_0x124,
         LBL_UnknownBlock_0x132,
         LBL_UnknownBlock_0x140,
         LBL_UnknownBlock_0x14E,
         LBL_UnknownBlock_0x15A,
         LBL_UnknownBlock_0x168,
         LBL_UnknownBlock_0x176,
         LBL_UnknownBlock_0x184,
         LBL_UnknownBlock_0x192,
         LBL_UnknownBlock_0x19A,
         LBL_UnknownBlock_0x1A6,
         LBL_UnknownBlock_0x1B2,
         LBL_UnknownBlock_0x1BE,
         LBL_UnknownBlock_0x1CA,
         LBL_UnknownBlock_0x1D8,
         LBL_UnknownBlock_0x1E6,
         LBL_UnknownBlock_0x1F2,
         LBL_UnknownBlock_0x200,
         LBL_PostHeaderData,

         RGN_SubdivContentBlock,
         RGN_ExtAreasBlock,
         RGN_ExtLinesBlock,
         RGN_ExtPointsBlock,
         RGN_UnknownBlock_0x71,
         RGN_PostHeaderData,

         DEM_Zoomlevel,

         TDB_Header,
         TDB_Copyright,
         TDB_Overviewmap,
         TDB_Tilemap,
         TDB_Description,
         TDB_Crc,
         TDB_Unknown,

         MPS_MapEntry,

         MDX_MapEntry,

         SRT_ContentsBlock,
         SRT_DescriptionBlock,
         SRT_CharacterLookupTableBlock,
         SRT_CharTabBlock,
         SRT_ExpansionsBlock,

         MDR_MDR1,
         MDR_MDR2,
         MDR_MDR3,
         MDR_MDR4,
         MDR_MDR5,
         MDR_MDR6,
         MDR_MDR7,
         MDR_MDR8,
         MDR_MDR9,
         MDR_MDR10,
         MDR_MDR11,
         MDR_MDR12,
         MDR_MDR13,
         MDR_MDR14,
         MDR_MDR15,
         MDR_MDR16,
         MDR_MDR17,
         MDR_MDR18,

         TYP_PolygoneTableBlock,
         TYP_PolygoneTable,
         TYP_PolygoneDraworderTable,
         TYP_PolylineTableBlock,
         TYP_PolylineTable,
         TYP_PointTableBlock,
         TYP_PointTable,
         TYP_NT_PointDatabtable,
         TYP_NT_PointDatablock,
         TYP_NT_PointLabelblock,
         TYP_NT_LabelblockTable1,
         TYP_NT_LabelblockTable2,




      }

      /// <summary>
      /// Type des Nodes
      /// </summary>
      public NodeType Type { get; protected set; }

      /// <summary>
      /// Daten des Nodes
      /// </summary>
      public object Data { get; protected set; }

      /// <summary>
      /// Text des Nodes
      /// </summary>
      public string Text { get; protected set; }


      /// <summary>
      /// spezielle Daten für eine Datei
      /// </summary>
      public class Content4File : IDisposable {

         /// <summary>
         /// vollständiger Dateiname
         /// </summary>
         public string Filename { get; protected set; }

         /// <summary>
         /// Extension
         /// </summary>
         public string Extension { get; protected set; }

         /// <summary>
         /// Basisdateiname
         /// </summary>
         public string Basename { get; protected set; }

         /// <summary>
         /// Dateilänge
         /// </summary>
         public long Filelength { get; protected set; }

         /// <summary>
         /// <see cref="GarminCore.BinaryReaderWriter"/> zum Lesen der Datei
         /// </summary>
         public GarminCore.BinaryReaderWriter BinaryReader { get; protected set; }

         /// <summary>
         /// allgemeines Objekt für zusätzliche Daten für Garmin-Dateien
         /// </summary>
         public object GarminFile { get; set; }


         public Content4File(string file) {
            Filename = file;
            Extension = Path.GetExtension(file);
            Basename = Path.GetFileNameWithoutExtension(Filename);
            Filelength = 0;
            BinaryReader = null;
            GarminFile = null;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile"/>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile GetGarminFileAsStd() {
            if (GarminFile != null)
               return GarminFile as GarminCore.Files.StdFile;
            return null;
         }


         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_TRE"/>
         /// <para>Falls noch nicht erfolgt, wird die TRE-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_TRE"/> keine TRE-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_TRE GetGarminFileAsTRE() {
            GarminCore.Files.StdFile_TRE file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_TRE();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_TRE)
               file = GarminFile as GarminCore.Files.StdFile_TRE;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_LBL"/>
         /// <para>Falls noch nicht erfolgt, wird die LBL-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_LBL"/> keine LBL-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_LBL GetGarminFileAsLBL() {
            GarminCore.Files.StdFile_LBL file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_LBL();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_LBL)
               file = GarminFile as GarminCore.Files.StdFile_LBL;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_RGN"/>
         /// <para>Falls noch nicht erfolgt, wird die RGN-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_RGN"/> keine RGN-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <param name="tre"></param>
         /// <returns></returns>
         public GarminCore.Files.StdFile_RGN GetGarminFileAsRGN(GarminCore.Files.StdFile_TRE tre) {
            GarminCore.Files.StdFile_RGN file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_RGN(tre);
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_RGN)
               file = GarminFile as GarminCore.Files.StdFile_RGN;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_NET"/>
         /// <para>Falls noch nicht erfolgt, wird die NET-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_NET"/> keine NET-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_NET GetGarminFileAsNET() {
            GarminCore.Files.StdFile_NET file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_NET();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_NET)
               file = GarminFile as GarminCore.Files.StdFile_NET;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_NOD"/>
         /// <para>Falls noch nicht erfolgt, wird die NOD-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_NOD"/> keine NOD-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_NOD GetGarminFileAsNOD() {
            GarminCore.Files.StdFile_NOD file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_NOD();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_NOD)
               file = GarminFile as GarminCore.Files.StdFile_NOD;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_DEM"/>
         /// <para>Falls noch nicht erfolgt, wird die DEM-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_DEM"/> keine DEM-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_DEM GetGarminFileAsDEM() {
            GarminCore.Files.StdFile_DEM file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_DEM();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_DEM)
               file = GarminFile as GarminCore.Files.StdFile_DEM;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_GMP"/>
         /// <para>Falls noch nicht erfolgt, wird die GMP-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_GMP"/> keine GMP-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_GMP GetGarminFileAsGMP() {
            GarminCore.Files.StdFile_GMP file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_GMP();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_GMP)
               file = GarminFile as GarminCore.Files.StdFile_GMP;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_MAR"/>
         /// <para>Falls noch nicht erfolgt, wird die MAR-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_MAR"/> keine MAR-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_MAR GetGarminFileAsMAR() {
            GarminCore.Files.StdFile_MAR file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_MAR();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_MAR)
               file = GarminFile as GarminCore.Files.StdFile_MAR;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_MDR"/>
         /// <para>Falls noch nicht erfolgt, wird die MDR-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_MDR"/> keine MDR-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_MDR GetGarminFileAsMDR() {
            GarminCore.Files.StdFile_MDR file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_MDR();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_MDR)
               file = GarminFile as GarminCore.Files.StdFile_MDR;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_SRT"/>
         /// <para>Falls noch nicht erfolgt, wird die SRT-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_SRT"/> keine SRT-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_SRT GetGarminFileAsSRT() {
            GarminCore.Files.StdFile_SRT file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_SRT();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_SRT)
               file = GarminFile as GarminCore.Files.StdFile_SRT;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.StdFile_TYP"/>
         /// <para>Falls noch nicht erfolgt, wird die TYP-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.StdFile_TYP"/> keine TYP-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.StdFile_TYP GetGarminFileAsTYP() {
            GarminCore.Files.StdFile_TYP file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.StdFile_TYP();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.StdFile_TYP)
               file = GarminFile as GarminCore.Files.StdFile_TYP;
            return file;
         }


         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.File_TDB"/>
         /// <para>Falls noch nicht erfolgt, wird die TDB-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.File_TDB"/> keine TDB-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.File_TDB GetGarminFileAsTDB() {
            GarminCore.Files.File_TDB file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.File_TDB();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.File_TDB)
               file = GarminFile as GarminCore.Files.File_TDB;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.File_MDX"/>
         /// <para>Falls noch nicht erfolgt, wird die MDX-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.File_MDX"/> keine MDX-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.File_MDX GetGarminFileAsMDX() {
            GarminCore.Files.File_MDX file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.File_MDX();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.File_MDX)
               file = GarminFile as GarminCore.Files.File_MDX;
            return file;
         }

         /// <summary>
         /// liefert das <see cref="GarminFile"/> als <see cref="GarminCore.Files.File_MPS"/>
         /// <para>Falls noch nicht erfolgt, wird die MPS-Datei eingelesen.</para>
         /// <para>Falls <see cref="GarminCore.Files.File_MPS"/> keine MPS-Datei ist, wird null geliefert.</para>
         /// </summary>
         /// <returns></returns>
         public GarminCore.Files.File_MPS GetGarminFileAsMPS() {
            GarminCore.Files.File_MPS file = null;
            if (GarminFile == null) {
               GarminFile = file = new GarminCore.Files.File_MPS();
               file.Read(BinaryReader);
            } else if (GarminFile is GarminCore.Files.File_MPS)
               file = GarminFile as GarminCore.Files.File_MPS;
            return file;
         }


         public override string ToString() {
            return string.Format("{0}, open: {1}, GarminFile is {2}",
                                 Filename,
                                 BinaryReader != null,
                                 GarminFile == null ? "null" : GarminFile.GetType().ToString());
         }

         ~Content4File() {
            Dispose(false);
         }

         #region Implementierung der IDisposable-Schnittstelle

         /// <summary>
         /// true, wenn schon ein Dispose() erfolgte
         /// </summary>
         private bool _isdisposed = false;

         /// <summary>
         /// kann expliziet für das Objekt aufgerufen werden um interne Ressourcen frei zu geben
         /// </summary>
         public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
         }

         /// <summary>
         /// überschreibt die Standard-Methode
         /// <para></para>
         /// </summary>
         /// <param name="notfromfinalizer">falls, wenn intern vom Finalizer aufgerufen</param>
         protected virtual void Dispose(bool notfromfinalizer) {
            if (!this._isdisposed) {            // bisher noch kein Dispose erfolgt
               if (notfromfinalizer) {          // nur dann alle managed Ressourcen freigeben
                  if (BinaryReader != null)
                     BinaryReader.Dispose();
               }
               // jetzt immer alle unmanaged Ressourcen freigeben (z.B. Win32)

               _isdisposed = true;        // Kennung setzen, dass Dispose erfolgt ist
            }
         }

         #endregion

      }

      /// <summary>
      /// spezielle Daten für eine physische Datei
      /// </summary>
      public class Content4PhysicalFile : Content4File {

         /// <summary>
         /// letzter schreibender Zugriff
         /// </summary>
         public DateTime FileDateTime { get; private set; }

         /// <summary>
         /// zur Verwendung bei IMG-Dateien
         /// </summary>
         public GarminCore.DskImg.SimpleFilesystem SimpleFilesystem { get; protected set; }

         public Content4PhysicalFile(string file) : base(file) {
            FileInfo fo = new FileInfo(file);
            Filelength = fo.Length;
            FileDateTime = fo.LastWriteTime;
            BinaryReader = new GarminCore.BinaryReaderWriter(Filename, true, false, false);
            if (Extension.ToUpper() == ".IMG") {
               SimpleFilesystem = new GarminCore.DskImg.SimpleFilesystem();
               SimpleFilesystem.Read(BinaryReader);
            } else
               SimpleFilesystem = null;
         }

         public override string ToString() {
            return string.Format("{0}, open: {1}, GarminFile is {2}",
                                 Filename,
                                 BinaryReader != null,
                                 SimpleFilesystem != null ? SimpleFilesystem.FileCount : 0);
         }
      }

      /// <summary>
      /// nur für Dateien in einem <see cref="GarminCore.DskImg.SimpleFilesystem"/>
      /// </summary>
      public class Content4LogicalFile : Content4File {

         public Content4LogicalFile(GarminCore.DskImg.SimpleFilesystem sfs, int fileidx) : base(sfs.Filename(fileidx)) {
            Filelength = sfs.Filesize(fileidx);
            BinaryReader = sfs.GetBinaryReaderWriter4File(Filename);
            GarminFile = null;
         }

      }

      /// <summary>
      /// Daten für einen (i.A. kleinen) Datenbereich
      /// </summary>
      public class Content4DataRange {

         public enum DataType {
            Byte, Int16, UInt16, Int24, UInt24, Int32, UInt32,
            Ascii,
            DataBlock,
            DataBlockWithRecordsize,
            UInt32Array,
            Int32Array,
            Other
         }

         public DataType Type { get; private set; }

         /// <summary>
         /// Adr. des 1. Bytes (symbolisch)
         /// </summary>
         public long FirstAdr { get; private set; }

         /// <summary>
         /// Datenbereich
         /// </summary>
         public byte[] Bytes { get; private set; }

         /// <summary>
         /// Bereichslänge
         /// </summary>
         public long RangeLength {
            get {
               return Bytes == null ? 0 : Bytes.Length;
            }
         }

         public byte AsByte(int start = 0) {
            return Bytes[start];
         }

         public short AsInt16(int start = 0) {
            return (short)((Bytes[start] << 8) | Bytes[start + 1]);
         }

         public ushort AsUInt16(int start = 0) {
            return (ushort)((Bytes[start] << 8) | Bytes[start + 1]);
         }

         public int AsInt24(int start = 0) {
            return (int)((Bytes[start] << 16) | (Bytes[start + 1] << 8) | Bytes[start + 2]);
         }

         public uint AsUInt24(int start = 0) {
            return (uint)((Bytes[start] << 16) | (Bytes[start + 1] << 8) | Bytes[start + 2]);
         }

         public int AsInt32(int start = 0) {
            return (int)((Bytes[start] << 24) | (Bytes[start + 1] << 16) | (Bytes[start + 2] << 8) | Bytes[start + 3]);
         }

         public uint AsUInt32(int start = 0) {
            return (uint)((Bytes[start] << 24) | (Bytes[start + 1] << 16) | (Bytes[start + 2] << 8) | Bytes[start + 3]);
         }

         public GarminCore.DataBlock AsDataBlock(int start = 0) {
            return new GarminCore.DataBlock(AsUInt32(start), AsUInt32(start + 4));
         }

         public GarminCore.DataBlockWithRecordsize AsDataBlockWithRecordsize(int start = 0) {
            return new GarminCore.DataBlockWithRecordsize(new GarminCore.DataBlock(AsUInt32(start), AsUInt32(start + 4)), AsUInt16(start + 8));
         }

         public string Info { get; private set; }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="bytes">Folge von Bytes</param>
         /// <param name="rangestart">Startindex des Bereiches; wenn negativ, dann 0</param>
         /// <param name="rangelength">Länge des Bereiches; wenn negativ, dann der gesamte Bereich</param>
         /// <param name="type">Datentyp</param>
         /// <param name="rangeadr">symbolische Adresse für das 1. Byte</param>
         /// <param name="infotext">Infotext</param>
         public Content4DataRange(IList<byte> bytes, int rangestart, int rangelength, DataType type, long rangeadr, string infotext) {
            if (rangelength < 0)
               rangelength = bytes.Count;
            if (rangestart < 0)
               rangestart = 0;

            Bytes = new byte[rangelength];
            for (int i = 0, j = rangestart; i < rangelength; i++, j++)
               Bytes[i] = bytes[j];
            Type = type;
            FirstAdr = rangeadr;
            Info = string.IsNullOrEmpty(infotext) ? "" : infotext;
         }

         public Content4DataRange(byte v, long rangeadr, string infotext) :
            this(new byte[1] { v },
                 0,
                 1,
                 DataType.Byte,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(short v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.Int16,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(ushort v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.UInt16,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(int v, long rangeadr, string infotext, bool as24bit) :
            this(GetBytebufferWithValue(v, as24bit),
                 0,
                 -1,
                 as24bit ? DataType.Int24 : DataType.Int32,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(uint v, long rangeadr, string infotext, bool as24bit) :
            this(GetBytebufferWithValue(v, as24bit),
                 0,
                 -1,
                 as24bit ? DataType.UInt24 : DataType.UInt32,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(string ascii, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(ascii),
                 0,
                 -1,
                 DataType.Ascii,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(GarminCore.DataBlock v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.DataBlock,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(GarminCore.DataBlockWithRecordsize v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.DataBlockWithRecordsize,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(uint[] v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.UInt32Array,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(int[] v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.Int32Array,
                 rangeadr,
                 infotext) { }

         public Content4DataRange(DateTime v, long rangeadr, string infotext) :
            this(GetBytebufferWithValue(v),
                 0,
                 -1,
                 DataType.Int16,
                 rangeadr,
                 infotext) { }


         static void SetValueToBuffer(IList<byte> buff, short v, int start) {
            buff[start++] = (byte)(v & 0xff);
            buff[start++] = (byte)((v >> 8) & 0xff);
         }

         static void SetValueToBuffer(IList<byte> buff, ushort v, int start) {
            buff[start++] = (byte)(v & 0xff);
            buff[start++] = (byte)((v >> 8) & 0xff);
         }

         static void SetValueToBuffer(IList<byte> buff, int v, int start, bool as24bit) {
            buff[start++] = (byte)(v & 0xff);
            buff[start++] = (byte)((v >> 8) & 0xff);
            buff[start++] = (byte)((v >> 16) & 0xff);
            if (!as24bit)
               buff[start++] = (byte)((v >> 24) & 0xff);
         }

         static void SetValueToBuffer(IList<byte> buff, uint v, int start, bool as24bit) {
            buff[start++] = (byte)(v & 0xff);
            buff[start++] = (byte)((v >> 8) & 0xff);
            buff[start++] = (byte)((v >> 16) & 0xff);
            if (!as24bit)
               buff[start++] = (byte)((v >> 24) & 0xff);
         }

         static void SetValueToBuffer(IList<byte> buff, DateTime v, int start) {
            buff[start++] = (byte)((v.Year >> 8) & 0xff);
            buff[start++] = (byte)(v.Year & 0xff);
            buff[start++] = (byte)(v.Month & 0xff);
            buff[start++] = (byte)(v.Day & 0xff);
            buff[start++] = (byte)(v.Hour & 0xff);
            buff[start++] = (byte)(v.Minute & 0xff);
            buff[start++] = (byte)(v.Second & 0xff);
         }


         static public byte[] GetBytebufferWithValue(uint v, bool as24bit) {
            byte[] buff = new byte[as24bit ? 3 : 4];
            SetValueToBuffer(buff, v, 0, as24bit);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(int v, bool as24bit) {
            byte[] buff = new byte[as24bit ? 3 : 4];
            SetValueToBuffer(buff, v, 0, as24bit);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(ushort v) {
            byte[] buff = new byte[2];
            SetValueToBuffer(buff, v, 0);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(short v) {
            byte[] buff = new byte[2];
            SetValueToBuffer(buff, v, 0);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(string ascii) {
            return Encoding.ASCII.GetBytes(ascii);
         }

         static public byte[] GetBytebufferWithValue(GarminCore.DataBlock v) {
            byte[] buff = new byte[8];
            SetValueToBuffer(buff, v.Offset, 0, false);
            SetValueToBuffer(buff, v.Length, 4, false);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(GarminCore.DataBlockWithRecordsize v) {
            byte[] buff = new byte[10];
            SetValueToBuffer(buff, v.Offset, 0, false);
            SetValueToBuffer(buff, v.Length, 4, false);
            SetValueToBuffer(buff, v.Recordsize, 8);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(uint[] v) {
            byte[] buff = new byte[v.Length * 4];
            for (int i = 0; i < v.Length; i++)
               SetValueToBuffer(buff, v[i], i * 4, false);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(int[] v) {
            byte[] buff = new byte[v.Length * 4];
            for (int i = 0; i < v.Length; i++)
               SetValueToBuffer(buff, v[i], i * 4, false);
            return buff;
         }

         static public byte[] GetBytebufferWithValue(DateTime v) {
            byte[] buff = new byte[7];
            SetValueToBuffer(buff, v, 0);
            return buff;
         }

         public override string ToString() {
            return string.Format("Type: {0}, {1} Bytes, FirstAdr 0x{2:X}", Type, RangeLength, FirstAdr);
         }

      }


      public NodeContent() : this(NodeType.Dummy) { }

      public NodeContent(NodeType type, object data = null, string text = null) {
         Type = type;
         Data = data;
         Text = null;
      }

      public override string ToString() {
         return string.Format("Type: {0}, '{1}', Data: {2}", Type, Text, Data);
      }

   }
}
