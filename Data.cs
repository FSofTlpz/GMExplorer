using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GMExplorer {
   class Data {

      /// <summary>
      /// max. Anzahl der im Hex-Fenster angezeigten Bytes
      /// </summary>
      public static int MaxBytes4Hex = 0x4000;


      /// <summary>
      /// ein neuer <see cref="TreeNode"/> wird (auf oberster Ebene) an den <see cref="TreeView"/> angehängt
      /// </summary>
      /// <param name="tv"></param>
      /// <param name="type"></param>
      /// <param name="data"></param>
      /// <param name="text"></param>
      /// <returns></returns>
      public static TreeNode AppendNode(TreeView tv, NodeContent.NodeType type = NodeContent.NodeType.Dummy, object data = null, string text = null) {
         TreeNode newtn = new TreeNode(text) {
            Tag = new NodeContent(type, data, text)
         };
         tv.Nodes.Add(newtn);
         return newtn;
      }

      /// <summary>
      /// ein neuer <see cref="TreeNode"/> wird unter dem angegeben <see cref="TreeNode"/> angehängt
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="type"></param>
      /// <param name="data"></param>
      /// <param name="text"></param>
      /// <returns></returns>
      public static TreeNode AppendNode(TreeNode tn, NodeContent.NodeType type = NodeContent.NodeType.Dummy, object data = null, string text = null) {
         TreeNode newtn = new TreeNode(text) {
            Tag = new NodeContent(type, data, text)
         };
         tn.Nodes.Add(newtn);
         return newtn;
      }


      /// <summary>
      /// liefert true, wenn genau ein ChildNode ex. und den Typ <see cref="NodeContent.NodeType.Dummy"/> hat
      /// <para>Das ist die Kennung, dass die "echten" ChildNodes noch nicht erzeugt wurden.</para>
      /// </summary>
      /// <param name="tn"></param>
      /// <returns></returns>
      protected static bool HasOnlyDummyChildNode(TreeNode tn) {
         if (tn.Nodes.Count == 1)
            if (NodeContent4TreeNode(tn.Nodes[0]).Type == NodeContent.NodeType.Dummy)
               return true;
         return false;
      }

      /// <summary>
      /// wenn genau ein ChildNode ex. und den Typ <see cref="NodeContent.NodeType.Dummy"/> hat, wird dieser ChildNode gelöscht und true geliefert
      /// <para>Das ist die Vorbereitung, um danach "echte" <see cref="TreeNode"/> anzuhängen.</para>
      /// </summary>
      /// <param name="tn"></param>
      /// <returns></returns>
      protected static void DeleteDummyChildNode(TreeNode tn) {
         if (HasOnlyDummyChildNode(tn))
            tn.Nodes.Clear();
      }


      /// <summary>
      /// liefert den Content eines TreeNode
      /// </summary>
      /// <param name="tn"></param>
      /// <returns></returns>
      public static NodeContent NodeContent4TreeNode(TreeNode tn) {
         return tn.Tag as NodeContent;
      }

      /// <summary>
      /// sucht ausgehend vom akt. <see cref="TreeNode"/> "nach oben" nach einem Knoten, der den Typ <see cref="NodeContent.Content4File"/> hat und liefert dessen Inhalt
      /// <para>Der akt. <see cref="TreeNode"/> gehört damit zu der so gefundenen logischen oder physischen Datei.</para>
      /// </summary>
      /// <param name="tn"></param>
      /// <returns>null, wenn kein solcher Knoten ex.</returns>
      public static NodeContent.Content4File GetNextContent4File(TreeNode tn) {
         NodeContent.Content4File content4file = null;
         if (tn != null)
            do {
               NodeContent nc = NodeContent4TreeNode(tn);
               if (nc.Data is NodeContent.Content4PhysicalFile ||
                   nc.Data is NodeContent.Content4LogicalFile)
                  content4file = nc.Data as NodeContent.Content4File;
               tn = tn.Parent;
            } while (content4file == null &&
                     tn != null);
         return content4file;
      }


      /// <summary>
      /// Suche im gesamten <see cref="TreeView"/> nach einem Node mit dem Dateinamen (case insensitive)
      /// </summary>
      /// <param name="tv"></param>
      /// <param name="file"></param>
      /// <returns>null, wenn nicht gefunden</returns>
      static TreeNode GetTreeNode4File(TreeView tv, string file) {
         return checkTreeNodes4File(tv.Nodes, file.ToUpper());
      }

      /// <summary>
      /// rekursive Suche nach einem Node mit dem Dateinamen (case insensitive) (Hilfsfkt. für <see cref="GetTreeNode4File"/>())
      /// </summary>
      /// <param name="tnc"></param>
      /// <param name="fileupper"></param>
      /// <returns></returns>
      static TreeNode checkTreeNodes4File(TreeNodeCollection tnc, string fileupper) {
         foreach (TreeNode tn in tnc) {
            NodeContent nc = NodeContent4TreeNode(tn);
            if (nc.Data is NodeContent.Content4File) {
               if ((nc.Data as NodeContent.Content4File).Filename.ToUpper() == fileupper)
                  return tn;
               else {
                  TreeNode tn2 = checkTreeNodes4File(tn.Nodes, fileupper);
                  if (tn2 != null)
                     return tn2;
               }
            }
         }
         return null;
      }

      #region ChildNodes anhängen

      /// <summary>
      /// hängt ev. zusätzliche ChildNodes an den akt. TreeNode an
      /// <para>aus dem akt. und ev. den darüber liegenden Nodes wird bestimmt, welche ChildNodes das sind</para>
      /// </summary>
      /// <param name="tn"></param>
      public static void AppendChildNodes(TreeNode tn) {
         if (HasOnlyDummyChildNode(tn)) { // wenn möglich, echte ChildNodes erzeugen
            NodeContent nc = NodeContent4TreeNode(tn);
            TreeViewData tvd = TreeViewData.GetTreeViewData(tn);

            Cursor.Current = Cursors.WaitCursor;

            switch (nc.Type) {
               case NodeContent.NodeType.SimpleFilesystem:
                  AppendChildNodesOn_SimpleFilesystem(tn, (NodeContent4TreeNode(tn.Parent).Data as NodeContent.Content4PhysicalFile).SimpleFilesystem);
                  break;

               case NodeContent.NodeType.PhysicalFile:
                  AppendChildNodesOn_PhysicalFile(tn, nc.Data as NodeContent.Content4PhysicalFile);
                  break;

               case NodeContent.NodeType.LogicalFile:
                  AppendChildNodesOn_LogicalFile(tn, nc.Data as NodeContent.Content4LogicalFile);
                  break;

               case NodeContent.NodeType.GarminCommonHeader:
                  AppendChildNodesOn_GarminCommonHeader(tn, nc.Data as GarminCore.Files.StdFile);
                  break;

               case NodeContent.NodeType.GarminSpecialHeader:
                  AppendChildNodesOn_GarminSpecialHeader(tn, nc.Data as GarminCore.Files.StdFile);
                  break;

               case NodeContent.NodeType.TRE_DescriptionList:
               case NodeContent.NodeType.TRE_MaplevelBlock:
               case NodeContent.NodeType.TRE_SubdivisionBlock:
               case NodeContent.NodeType.TRE_CopyrightBlock:
               case NodeContent.NodeType.TRE_LineOverviewBlock:
               case NodeContent.NodeType.TRE_AreaOverviewBlock:
               case NodeContent.NodeType.TRE_PointOverviewBlock:
               case NodeContent.NodeType.TRE_ExtTypeOffsetsBlock:
               case NodeContent.NodeType.TRE_ExtTypeOverviewsBlock:
               case NodeContent.NodeType.TRE_UnknownBlock_xAE:
               case NodeContent.NodeType.TRE_UnknownBlock_xBC:
               case NodeContent.NodeType.TRE_UnknownBlock_xE3:
                  DataTRE.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_TRE, nc.Type);
                  break;

               case NodeContent.NodeType.LBL_TextBlock:
               case NodeContent.NodeType.LBL_CountryBlock:
               case NodeContent.NodeType.LBL_RegionBlock:
               case NodeContent.NodeType.LBL_CityBlock:
               case NodeContent.NodeType.LBL_PointIndexList4RGN:
               case NodeContent.NodeType.LBL_PointPropertiesBlock:
               case NodeContent.NodeType.LBL_PointTypeIndexList4RGN:
               case NodeContent.NodeType.LBL_ZipBlock:
               case NodeContent.NodeType.LBL_HighwayWithExitBlock:
               case NodeContent.NodeType.LBL_ExitBlock:
               case NodeContent.NodeType.LBL_HighwayExitBlock:
               case NodeContent.NodeType.LBL_SortDescriptorDefBlock:
               case NodeContent.NodeType.LBL_Lbl13Block:
               case NodeContent.NodeType.LBL_TidePredictionBlock:
               case NodeContent.NodeType.LBL_UnknownBlock_0xD0:
               case NodeContent.NodeType.LBL_UnknownBlock_0xDE:
               case NodeContent.NodeType.LBL_UnknownBlock_0xEC:
               case NodeContent.NodeType.LBL_UnknownBlock_0xFA:
               case NodeContent.NodeType.LBL_UnknownBlock_0x108:
               case NodeContent.NodeType.LBL_UnknownBlock_0x116:
               case NodeContent.NodeType.LBL_UnknownBlock_0x124:
               case NodeContent.NodeType.LBL_UnknownBlock_0x132:
               case NodeContent.NodeType.LBL_UnknownBlock_0x140:
               case NodeContent.NodeType.LBL_UnknownBlock_0x14E:
               case NodeContent.NodeType.LBL_UnknownBlock_0x15A:
               case NodeContent.NodeType.LBL_UnknownBlock_0x168:
               case NodeContent.NodeType.LBL_UnknownBlock_0x176:
               case NodeContent.NodeType.LBL_UnknownBlock_0x184:
               case NodeContent.NodeType.LBL_UnknownBlock_0x192:
               case NodeContent.NodeType.LBL_UnknownBlock_0x19A:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1A6:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1B2:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1BE:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1CA:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1D8:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1E6:
               case NodeContent.NodeType.LBL_UnknownBlock_0x1F2:
               case NodeContent.NodeType.LBL_UnknownBlock_0x200:
               case NodeContent.NodeType.LBL_PostHeaderData:
                  DataLBL.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_LBL, nc.Type);
                  break;

               case NodeContent.NodeType.RGN_SubdivContentBlock:
               case NodeContent.NodeType.RGN_ExtAreasBlock:
               case NodeContent.NodeType.RGN_ExtLinesBlock:
               case NodeContent.NodeType.RGN_ExtPointsBlock:
               case NodeContent.NodeType.RGN_UnknownBlock_0x71:
               case NodeContent.NodeType.RGN_PostHeaderData:
                  DataRGN.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_RGN, nc.Type);
                  break;

               case NodeContent.NodeType.NET_RoadDefinitionsBlock:
               case NodeContent.NodeType.NET_SegmentedRoadsBlock:
               case NodeContent.NodeType.NET_SortedRoadsBlock:
                  DataNET.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_NET, nc.Type);
                  break;

               case NodeContent.NodeType.DEM_Zoomlevel:
                  DataDEM.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_DEM, nc.Type);
                  break;

               case NodeContent.NodeType.TDB_Header:
               case NodeContent.NodeType.TDB_Copyright:
               case NodeContent.NodeType.TDB_Overviewmap:
               case NodeContent.NodeType.TDB_Tilemap:
               case NodeContent.NodeType.TDB_Description:
               case NodeContent.NodeType.TDB_Crc:
               case NodeContent.NodeType.TDB_Unknown:
                  DataTDB.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.File_TDB, nc.Type);
                  break;

               case NodeContent.NodeType.MPS_MapEntry:
                  DataMPS.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.File_MPS, nc.Type);
                  break;

               case NodeContent.NodeType.MDX_MapEntry:
                  DataMDX.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.File_MDX, nc.Type);
                  break;

               case NodeContent.NodeType.SRT_ContentsBlock:
               case NodeContent.NodeType.SRT_DescriptionBlock:
               case NodeContent.NodeType.SRT_CharacterLookupTableBlock:
                  DataSRT.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_SRT, nc.Type);
                  break;

               case NodeContent.NodeType.MDR_MDR1:
               case NodeContent.NodeType.MDR_MDR2:
               case NodeContent.NodeType.MDR_MDR3:
               case NodeContent.NodeType.MDR_MDR4:
               case NodeContent.NodeType.MDR_MDR5:
               case NodeContent.NodeType.MDR_MDR6:
               case NodeContent.NodeType.MDR_MDR7:
               case NodeContent.NodeType.MDR_MDR8:
               case NodeContent.NodeType.MDR_MDR9:
               case NodeContent.NodeType.MDR_MDR10:
               case NodeContent.NodeType.MDR_MDR11:
               case NodeContent.NodeType.MDR_MDR12:
               case NodeContent.NodeType.MDR_MDR13:
               case NodeContent.NodeType.MDR_MDR14:
               case NodeContent.NodeType.MDR_MDR15:
               case NodeContent.NodeType.MDR_MDR16:
               case NodeContent.NodeType.MDR_MDR17:
               case NodeContent.NodeType.MDR_MDR18:
                  DataMDR.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_MDR, nc.Type);
                  break;

               case NodeContent.NodeType.TYP_PolygoneTableBlock:
               case NodeContent.NodeType.TYP_PolygoneTable:
               case NodeContent.NodeType.TYP_PolygoneDraworderTable:
               case NodeContent.NodeType.TYP_PolylineTableBlock:
               case NodeContent.NodeType.TYP_PolylineTable:
               case NodeContent.NodeType.TYP_PointTableBlock:
               case NodeContent.NodeType.TYP_PointTable:
               case NodeContent.NodeType.TYP_NT_PointDatabtable:
               case NodeContent.NodeType.TYP_NT_PointDatablock:
               case NodeContent.NodeType.TYP_NT_PointLabelblock:
               case NodeContent.NodeType.TYP_NT_LabelblockTable1:
               case NodeContent.NodeType.TYP_NT_LabelblockTable2:
                  DataTYP.AppendChildNodesOn_Sections(tn, nc.Data as GarminCore.Files.StdFile_TYP, nc.Type);
                  break;


            }

            Cursor.Current = Cursors.Default;

         }
      }

      /// <summary>
      /// Knoten für ein <see cref="GarminCore.DskImg.SimpleFilesystem"/> anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="sfs"></param>
      static void AppendChildNodesOn_SimpleFilesystem(TreeNode tn, GarminCore.DskImg.SimpleFilesystem sfs) {
         DeleteDummyChildNode(tn);

         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.XOR, 0, "XOR-Byte: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.XOR)), "XOR");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x01, 0, sfs.ImgHeader.Unknown_x01.Length, NodeContent.Content4DataRange.DataType.Other, 1, "Unknown 0x1"), "Unknown 0x1");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.UpdateMonth, 0xa, "UpdateMonth: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.UpdateMonth)), "UpdateMonth");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange((byte)(sfs.ImgHeader.UpdateYear - 1900), 0xb, "UpdateYear (-1900): " + DecimalAndHexAndBinary(sfs.ImgHeader.UpdateYear - 1900)), "UpdateYear");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x0c, 0, sfs.ImgHeader.Unknown_x0c.Length, NodeContent.Content4DataRange.DataType.Other, 0xc, "Unknown 0xC"), "Unknown 0xC");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.MapsourceFlag, 0xe, "MapsourceFlag: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.MapsourceFlag)), "MapsourceFlag");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Checksum, 0xf, "Checksum: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.Checksum)), "Checksum");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(GarminCore.DskImg.Header.SIGNATURE, 0x10, "Signature: " + GarminCore.DskImg.Header.SIGNATURE), "Signature"); // Fake
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x16, 0, sfs.ImgHeader.Unknown_x16.Length, NodeContent.Content4DataRange.DataType.Other, 0x16, "Unknown 0x16"), "Unknown 0x16");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.SectorsPerTrack, 0x18, "SectorsPerTrack: " + DecimalAndHexAndBinary(sfs.ImgHeader.SectorsPerTrack)), "SectorsPerTrack");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.SectorsPerTrack, 0x1a, "HeadsPerCylinder: " + DecimalAndHexAndBinary(sfs.ImgHeader.HeadsPerCylinder)), "HeadsPerCylinder");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Cylinders, 0x1c, "Cylinders: " + DecimalAndHexAndBinary(sfs.ImgHeader.Cylinders)), "Cylinders");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x1e, 0, sfs.ImgHeader.Unknown_x1e.Length, NodeContent.Content4DataRange.DataType.Other, 0xc, "Unknown 0x1E"), "Unknown 0x1E");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.CreationDate, 0x39, "CreationDate: " + sfs.ImgHeader.CreationDate.ToString("F")), "CreationDate");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.HeadSectors, 0x40, "HeadSectors: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.HeadSectors)), "HeadSectors");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(GarminCore.DskImg.Header.MAPFILEIDENTIFIER, 0x41, "MapfileIdentifier: " + GarminCore.DskImg.Header.MAPFILEIDENTIFIER), "MapfileIdentifier"); // Fake
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x47, 0, sfs.ImgHeader.Unknown_x47.Length, NodeContent.Content4DataRange.DataType.Other, 0x47, "Unknown 0x47"), "Unknown 0x47");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Description1, 0x49, "Description1: '" + sfs.ImgHeader.Description1 + "'"), "Description1");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.HeadsPerCylinder2, 0x5d, "HeadsPerCylinder2: " + DecimalAndHexAndBinary(sfs.ImgHeader.HeadsPerCylinder2)), "HeadsPerCylinder2");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.SectorsPerTrack2, 0x5f, "SectorsPerTrack2: " + DecimalAndHexAndBinary(sfs.ImgHeader.SectorsPerTrack2)), "SectorsPerTrack2");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.BlocksizeExp1, 0x61, "BlocksizeExp1: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.BlocksizeExp1)), "BlocksizeExp1");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.BlocksizeExp2, 0x62, "BlocksizeExp2: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.BlocksizeExp2)), "BlocksizeExp2");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Blocks4Img, 0x63, "Blocks4Img: " + DecimalAndHexAndBinary(sfs.ImgHeader.Blocks4Img)), "Blocks4Img");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Description2, 0x65, "Description2: '" + sfs.ImgHeader.Description2 + "'"), "Description2");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x83, 0, sfs.ImgHeader.Unknown_x83.Length, NodeContent.Content4DataRange.DataType.Other, 0x83, "Unknown 0x83"), "Unknown 0x83");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.StartHeadNumber4Partition, 0x1bf, "StartHeadNumber4Partition: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.StartHeadNumber4Partition)), "StartHeadNumber4Partition");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.StartSectorNumber4Partition, 0x1c0, "StartSectorNumber4Partition: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.StartSectorNumber4Partition)), "StartSectorNumber4Partition");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.StartCylinderNumber4Partition, 0x1c1, "StartHeadNumber4Partition: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.StartHeadNumber4Partition)), "StartHeadNumber4Partition");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Systemtyp, 0x1c2, "Systemtyp: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.Systemtyp)), "Systemtyp");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.LastHeadNumber4Partition, 0x1c3, "LastHeadNumber4Partition: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.LastHeadNumber4Partition)), "LastHeadNumber4Partition");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.LastSectorNumber4Partition, 0x1c4, "LastSectorNumber4Partition: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.LastSectorNumber4Partition)), "LastSectorNumber4Partition");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.LastCylinderNumber4Partition, 0x1c5, "LastCylinderNumber4Partition: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.LastCylinderNumber4Partition)), "LastCylinderNumber4Partition");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.RelativeSectors, 0x1c6, "RelativeSectors: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.RelativeSectors), false), "RelativeSectors");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.LastSectorNumber4IMG, 0x1ca, "LastSectorNumber4IMG: " + DecimalAndHexAndBinary((ulong)sfs.ImgHeader.LastSectorNumber4IMG), false), "LastSectorNumber4IMG");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x1ce, 0, sfs.ImgHeader.Unknown_x1ce.Length, NodeContent.Content4DataRange.DataType.Other, 0x1ce, "Unknown 0x1CE"), "Unknown 0x1CE");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(GarminCore.DskImg.Header.BOOTSIGNATURE, 0x1fe, "Bootsignature: " + DecimalAndHexAndBinary(GarminCore.DskImg.Header.BOOTSIGNATURE)), "Bootsignature");
         AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(sfs.ImgHeader.Unknown_x200, 0, sfs.ImgHeader.Unknown_x200.Length, NodeContent.Content4DataRange.DataType.Other, 0x200, "Unknown 0x200"), "Unknown 0x200");
      }

      /// <summary>
      /// Knoten für eine physische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="filedata"></param>
      static void AppendChildNodesOn_PhysicalFile(TreeNode tn, NodeContent.Content4PhysicalFile filedata) {
         if (filedata.BinaryReader != null) {
            if (filedata.Extension.ToUpper() == ".IMG") {
               if (HasOnlyDummyChildNode(tn)) {
                  DeleteDummyChildNode(tn);
                  AppendNode(AppendNode(tn, NodeContent.NodeType.SimpleFilesystem, filedata.SimpleFilesystem, "Filesystem"));
                  for (int i = 0; i < filedata.SimpleFilesystem.FileCount; i++) {
                     NodeContent.Content4LogicalFile c4lf = new NodeContent.Content4LogicalFile(filedata.SimpleFilesystem, i);
                     AppendNode(AppendNode(tn, NodeContent.NodeType.LogicalFile, c4lf, c4lf.Filename));
                  }
                  RegisterCoreFiles(TreeViewData.GetTreeViewData(tn), filedata.Basename);
               }
            } else {
               AppendChildNodesOn_File(tn, filedata);
            }
         }
      }

      /// <summary>
      /// Knoten für eine logische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="filedata"></param>
      static void AppendChildNodesOn_LogicalFile(TreeNode tn, NodeContent.Content4LogicalFile filedata) {
         AppendChildNodesOn_File(tn, filedata);
      }

      /// <summary>
      /// Knoten für eine physische oder logische Datei anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="filedata"></param>
      static void AppendChildNodesOn_File(TreeNode tn, NodeContent.Content4File filedata) {
         if (filedata.BinaryReader != null) {
            string extensionupper = filedata.Extension.ToUpper();

            RegisterCoreFiles(TreeViewData.GetTreeViewData(tn), filedata.Filename);

            if (extensionupper == ".TRE") {

               DataTRE.AppendChildNodes(tn, filedata.GetGarminFileAsTRE(), filedata.BinaryReader);

            } else if (extensionupper == ".LBL") {

               DataLBL.AppendChildNodes(tn, filedata.GetGarminFileAsLBL(), filedata.BinaryReader);

            } else if (extensionupper == ".RGN") {

               TreeViewData tvd = TreeViewData.GetTreeViewData(tn);
               GarminCore.Files.StdFile_TRE tre = tvd.GetTRE(filedata.Basename);
               if (tre == null)
                  tre = GetFirstTREinTreeView(tvd, filedata.Basename);
               DataRGN.AppendChildNodes(tn, filedata.GetGarminFileAsRGN(tre), filedata.BinaryReader);

            } else if (extensionupper == ".NET") {

               TreeViewData tvd = TreeViewData.GetTreeViewData(tn);
               GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
               if (lbl == null)
                  lbl = GetFirstLBLinTreeView(tvd, filedata.Basename);
               DataNET.AppendChildNodes(tn, filedata.GetGarminFileAsNET(lbl), filedata.BinaryReader);

            } else if (extensionupper == ".DEM") {

               DataDEM.AppendChildNodes(tn, filedata.GetGarminFileAsDEM(), filedata.BinaryReader);

            } else if (extensionupper == ".TYP") {

               DataTYP.AppendChildNodes(tn, filedata.GetGarminFileAsTYP(), filedata.BinaryReader);

            } else if (extensionupper == ".SRT") {

               DataSRT.AppendChildNodes(tn, filedata.GetGarminFileAsSRT(), filedata.BinaryReader);

            } else if (extensionupper == ".MDR") {

               DataMDR.AppendChildNodes(tn, filedata.GetGarminFileAsMDR(), filedata.BinaryReader);

            } else if (extensionupper == ".TDB") {

               DataTDB.AppendChildNodes(tn, filedata.GetGarminFileAsTDB(), filedata.BinaryReader);

            } else if (extensionupper == ".MPS") {

               DataMPS.AppendChildNodes(tn, filedata.GetGarminFileAsMPS(), filedata.BinaryReader);

            } else if (extensionupper == ".MDX") {

               DataMDX.AppendChildNodes(tn, filedata.GetGarminFileAsMDX(), filedata.BinaryReader);

            }

            // weitere Dateitypen

         }
      }

      /// <summary>
      /// Knoten für den Garmin CommonHeader anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="stdfile"></param>
      static void AppendChildNodesOn_GarminCommonHeader(TreeNode tn, GarminCore.Files.StdFile stdfile) {
         DeleteDummyChildNode(tn);
         if (stdfile != null) {
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(stdfile.Headerlength, 0, "Headerlength: " + DecimalAndHexAndBinary(stdfile.Headerlength)), "Headerlength");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange("GARMIN " + stdfile.Type, 0x2, "Garmin-Type: 'GARMIN " + stdfile.Type + "'"), "Garmin-Type"); // Fake
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(stdfile.Unknown_0x0C, 0xc, "Unknown_0xC: " + DecimalAndHexAndBinary((ulong)stdfile.Unknown_0x0C)), "Unknown 0xC");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(stdfile.Locked, 0xd, "Locked: " + DecimalAndHexAndBinary((ulong)stdfile.Locked)), "Locked");
            AppendNode(tn, NodeContent.NodeType.DataRange, new NodeContent.Content4DataRange(stdfile.CreationDate, 0xe, "CreationDate: " + stdfile.CreationDate.ToString("F")), "CreationDate");
         }
      }

      /// <summary>
      /// Knoten für einen Garmin SpecialHeader anhängen
      /// </summary>
      /// <param name="tn"></param>
      /// <param name="stdfile"></param>
      static void AppendChildNodesOn_GarminSpecialHeader(TreeNode tn, GarminCore.Files.StdFile stdfile) {
         DeleteDummyChildNode(tn);

         if (stdfile is GarminCore.Files.StdFile_TRE) {
            DataTRE.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_TRE);
         } else if (stdfile is GarminCore.Files.StdFile_LBL) {
            DataLBL.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_LBL);
         } else if (stdfile is GarminCore.Files.StdFile_RGN) {
            DataRGN.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_RGN);
         } else if (stdfile is GarminCore.Files.StdFile_NET) {
            DataNET.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_NET);
         } else if (stdfile is GarminCore.Files.StdFile_DEM) {
            DataDEM.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_DEM);
         } else if (stdfile is GarminCore.Files.StdFile_TYP) {
            DataTYP.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_TYP);
         } else if (stdfile is GarminCore.Files.StdFile_SRT) {
            DataSRT.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_SRT);
         } else if (stdfile is GarminCore.Files.StdFile_MDR) {
            DataMDR.AppendChildNodesOn_GarminSpecialHeader(tn, stdfile as GarminCore.Files.StdFile_MDR);
         }


         // weitere Dateitypen


      }

      #endregion

      /// <summary>
      /// alle zusammengehörenden "Kerndateien" für die schon ein <see cref="TreeNode"/> existiert registrieren und einlesen
      /// </summary>
      /// <param name="tvd"></param>
      /// <param name="filename"></param>
      public static void RegisterCoreFiles(TreeViewData tvd, string filename) {
         string basename = Path.GetFileNameWithoutExtension(filename);
         if (!tvd.Exist(basename)) { // Basisname ist noch nicht registriert
            string filenamewithoutext = filename.Substring(0, filename.Length - Path.GetExtension(filename).Length);

            TreeNode tn_lbl = GetTreeNode4File(tvd.TreeView, filenamewithoutext + ".LBL");
            if (tn_lbl != null) // Node ex.
               tvd.Register(basename, (NodeContent4TreeNode(tn_lbl).Data as NodeContent.Content4File).GetGarminFileAsLBL()); // wenn das Objekt noch nicht ex. wird es erzeugt und die Daten werden eingelesen

            TreeNode tn_tre = GetTreeNode4File(tvd.TreeView, filenamewithoutext + ".TRE");
            if (tn_tre != null)
               tvd.Register(basename, (NodeContent4TreeNode(tn_tre).Data as NodeContent.Content4File).GetGarminFileAsTRE());

            TreeNode tn_rgn = GetTreeNode4File(tvd.TreeView, filenamewithoutext + ".RGN");
            if (tn_rgn != null)
               tvd.Register(basename, (NodeContent4TreeNode(tn_rgn).Data as NodeContent.Content4File).GetGarminFileAsRGN(tvd.GetTRE(basename)));

            TreeNode tn_net = GetTreeNode4File(tvd.TreeView, filenamewithoutext + ".NET");
            if (tn_net != null)
               tvd.Register(basename, (NodeContent4TreeNode(tn_net).Data as NodeContent.Content4File).GetGarminFileAsNET(tvd.GetLBL(basename)));

            TreeNode tn_nod = GetTreeNode4File(tvd.TreeView, filenamewithoutext + ".NOD");
            if (tn_nod != null)
               tvd.Register(basename, (NodeContent4TreeNode(tn_nod).Data as NodeContent.Content4File).GetGarminFileAsNOD());
         }
      }


      /// <summary>
      /// im gesamten <see cref="TreeView"/> nach einem bestimmten <see cref="GarminCore.Files.StdFile_TRE"/>-Objekt suchen
      /// </summary>
      /// <param name="tvd"></param>
      /// <param name="basename"></param>
      /// <returns></returns>
      protected static GarminCore.Files.StdFile_TRE GetFirstTREinTreeView(TreeViewData tvd, string basename) {
         GarminCore.Files.StdFile_TRE tre = null;
         foreach (TreeNode node in tvd.TreeView.Nodes) {
            tre = GetFirstTREinSubTree(node, basename.ToUpper() + ".TRE");
            if (tre != null)
               break;
         }
         return tre;
      }

      /// <summary>
      /// rekursive Suche nach dem gewünschten <see cref="GarminCore.Files.StdFile_TRE"/>-Objekt im SubTree
      /// </summary>
      /// <param name="node"></param>
      /// <param name="lblfilename"></param>
      /// <returns></returns>
      static GarminCore.Files.StdFile_TRE GetFirstTREinSubTree(TreeNode node, string trefilename) {
         GarminCore.Files.StdFile_TRE tre = GetTREfromTreeNode(node, trefilename);
         if (tre == null)
            foreach (TreeNode subnode in node.Nodes) {
               tre = GetTREfromTreeNode(subnode, trefilename);
               if (tre != null)
                  break;
            }
         return tre;
      }

      /// <summary>
      /// liefert, falls vorhanden, das gewünschte <see cref="GarminCore.Files.StdFile_TRE"/>-Objekt aus diesem <see cref="TreeNode"/>
      /// </summary>
      /// <param name="node"></param>
      /// <param name="lblfilename"></param>
      /// <returns></returns>
      static GarminCore.Files.StdFile_TRE GetTREfromTreeNode(TreeNode node, string trefilename) {
         NodeContent nc = NodeContent4TreeNode(node);
         if (nc.Data is NodeContent.Content4LogicalFile) {
            NodeContent.Content4LogicalFile c4lf = nc.Data as NodeContent.Content4LogicalFile;
            if (c4lf.Filename.ToUpper() == trefilename) {
               TreeViewData tvd = TreeViewData.GetTreeViewData(node);
               RegisterCoreFiles(tvd, (nc.Data as NodeContent.Content4LogicalFile).Filename);
               return c4lf.GetGarminFileAsTRE(); // erzeugt bei Bedarf das StdFile_TRE-Objekt
            }
         }
         return null;
      }


      /// <summary>
      /// im gesamten <see cref="TreeView"/> nach einem bestimmten <see cref="GarminCore.Files.StdFile_LBL"/>-Objekt suchen
      /// </summary>
      /// <param name="tvd"></param>
      /// <param name="basename"></param>
      /// <returns></returns>
      protected static GarminCore.Files.StdFile_LBL GetFirstLBLinTreeView(TreeViewData tvd, string basename) {
         GarminCore.Files.StdFile_LBL lbl = null;
         foreach (TreeNode node in tvd.TreeView.Nodes) {
            lbl = GetFirstLBLinSubTree(node, basename.ToUpper() + ".LBL");
            if (lbl != null)
               break;
         }
         return lbl;
      }

      /// <summary>
      /// rekursive Suche nach dem gewünschten <see cref="GarminCore.Files.StdFile_LBL"/>-Objekt im SubTree
      /// </summary>
      /// <param name="node"></param>
      /// <param name="lblfilename"></param>
      /// <returns></returns>
      static GarminCore.Files.StdFile_LBL GetFirstLBLinSubTree(TreeNode node, string lblfilename) {
         GarminCore.Files.StdFile_LBL tre = GetLBLfromTreeNode(node, lblfilename);
         if (tre == null)
            foreach (TreeNode subnode in node.Nodes) {
               tre = GetLBLfromTreeNode(subnode, lblfilename);
               if (tre != null)
                  break;
            }
         return tre;
      }

      /// <summary>
      /// liefert, falls vorhanden, das gewünschte <see cref="GarminCore.Files.StdFile_LBL"/>-Objekt aus diesem <see cref="TreeNode"/>
      /// </summary>
      /// <param name="node"></param>
      /// <param name="lblfilename"></param>
      /// <returns></returns>
      static GarminCore.Files.StdFile_LBL GetLBLfromTreeNode(TreeNode node, string lblfilename) {
         NodeContent nc = NodeContent4TreeNode(node);
         if (nc.Data is NodeContent.Content4LogicalFile) {
            NodeContent.Content4LogicalFile c4lf = nc.Data as NodeContent.Content4LogicalFile;
            if (c4lf.Filename.ToUpper() == lblfilename) {
               TreeViewData tvd = TreeViewData.GetTreeViewData(node);
               RegisterCoreFiles(tvd, (nc.Data as NodeContent.Content4LogicalFile).Filename);
               return c4lf.GetGarminFileAsLBL(); // erzeugt bei Bedarf das StdFile_TRE-Objekt
            }
         }
         return null;
      }


      /// <summary>
      /// im gesamten <see cref="TreeView"/> nach einem bestimmten <see cref="GarminCore.Files.StdFile_LBL"/>-Objekt suchen und liest die Daten bei Bedarf ein
      /// </summary>
      /// <param name="tvd"></param>
      /// <param name="basename"></param>
      /// <returns></returns>
      protected static GarminCore.Files.StdFile_RGN GetFirstRGNinTreeView(TreeViewData tvd, string basename) {
         GarminCore.Files.StdFile_RGN rgn = null;
         foreach (TreeNode node in tvd.TreeView.Nodes) {
            rgn = GetFirstRGNinSubTree(node, basename.ToUpper() + ".RGN");
            if (rgn != null)
               break;
         }
         return rgn;
      }

      /// <summary>
      /// rekursive Suche nach dem gewünschten <see cref="GarminCore.Files.StdFile_RGN"/>-Objekt im SubTree und liest die Daten bei Bedarf ein
      /// </summary>
      /// <param name="node"></param>
      /// <param name="rgnfilename">upper case !</param>
      /// <returns></returns>
      static GarminCore.Files.StdFile_RGN GetFirstRGNinSubTree(TreeNode node, string rgnfilename) {
         GarminCore.Files.StdFile_RGN tre = GetRGNfromTreeNode(node, rgnfilename);
         if (tre == null)
            foreach (TreeNode subnode in node.Nodes) {
               tre = GetRGNfromTreeNode(subnode, rgnfilename);
               if (tre != null)
                  break;
            }
         return tre;
      }

      /// <summary>
      /// liefert, falls vorhanden, das gewünschte <see cref="GarminCore.Files.StdFile_RGN"/>-Objekt aus diesem <see cref="TreeNode"/> und liest die Daten bei Bedarf ein
      /// </summary>
      /// <param name="node"></param>
      /// <param name="rgnfilename">upper case !</param>
      /// <returns></returns>
      static GarminCore.Files.StdFile_RGN GetRGNfromTreeNode(TreeNode node, string rgnfilename) {
         NodeContent nc = NodeContent4TreeNode(node);
         if (nc.Data is NodeContent.Content4LogicalFile) {
            NodeContent.Content4LogicalFile c4lf = nc.Data as NodeContent.Content4LogicalFile;
            if (c4lf.Filename.ToUpper() == rgnfilename) {
               TreeViewData tvd = TreeViewData.GetTreeViewData(node);
               RegisterCoreFiles(tvd, (nc.Data as NodeContent.Content4LogicalFile).Filename);
               return c4lf.GetGarminFileAsRGN(GetFirstTREinTreeView(tvd, Path.GetFileNameWithoutExtension(rgnfilename)));
            }
         }
         return null;
      }




      #region Info anzeigen

      public static void ShowData4Node_PhysicalFile(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4PhysicalFile filedata, TreeViewData tvd) {
         info.AppendLine(filedata.Filename);
         info.AppendLine("File length:       " + DecimalAndHexAndBinary(filedata.Filelength) + " Byte");
         info.AppendLine("last write access: " + filedata.FileDateTime.ToString("F"));

         firsthexadr = 0;
         hex = HexRange(firsthexadr, filedata.BinaryReader, filedata.Filelength);
      }

      public static void ShowData4Node_LogicalFile(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4LogicalFile filedata, TreeViewData tvd) {
         info.AppendLine(filedata.Filename);
         info.AppendLine("File length:       " + DecimalAndHexAndBinary(filedata.Filelength) + " Byte");

         string extensionupper = filedata.Extension.ToUpper();

         firsthexadr = 0;
         hex = new byte[0];

         try {

            if (extensionupper == ".TRE") {

               GarminCore.Files.StdFile_TRE tre = filedata.GetGarminFileAsTRE(); // erzeugt bei Bedarf das StdFile_TRE-Objekt
               info.AppendLine("CreationDate: " + tre.CreationDate.ToString("F"));
               tvd.Register(filedata.Basename, tre);

            } else if (extensionupper == ".LBL") {

               GarminCore.Files.StdFile_LBL lbl = filedata.GetGarminFileAsLBL(); // erzeugt bei Bedarf das StdFile_LBL-Objekt
               info.AppendLine("CreationDate: " + lbl.CreationDate.ToString("F"));
               tvd.Register(filedata.Basename, lbl);

            } else if (extensionupper == ".RGN") {

               GarminCore.Files.StdFile_TRE tre = tvd.GetTRE(filedata.Basename);
               if (tre == null)
                  tre = GetFirstTREinTreeView(tvd, filedata.Basename);
               GarminCore.Files.StdFile_RGN rgn = filedata.GetGarminFileAsRGN(tre); // erzeugt bei Bedarf das StdFile_RGN-Objekt
               info.AppendLine("CreationDate: " + rgn.CreationDate.ToString("F"));
               tvd.Register(filedata.Basename, rgn);

            } else if (extensionupper == ".NET") {

               GarminCore.Files.StdFile_LBL lbl = tvd.GetLBL(filedata.Basename);
               if (lbl == null)
                  lbl = GetFirstLBLinTreeView(tvd, filedata.Basename);
               GarminCore.Files.StdFile_NET net = filedata.GetGarminFileAsNET(lbl); // erzeugt bei Bedarf das StdFile_NET-Objekt
               info.AppendLine("CreationDate: " + net.CreationDate.ToString("F"));
               tvd.Register(filedata.Basename, lbl);

            } else if (extensionupper == ".DEM") {

               GarminCore.Files.StdFile_DEM dem = filedata.GetGarminFileAsDEM(); // erzeugt bei Bedarf das StdFile_DEM-Objekt
               info.AppendLine("CreationDate: " + dem.CreationDate.ToString("F"));



               // weitere Dateitypen



            } else
               info.AppendLine("not implemented or unknown filetype");


            hex = HexRange(firsthexadr, filedata.BinaryReader, filedata.Filelength);

         } catch (Exception ex) {
            ShowExceptionExt("exception on showing data for logical file: ", ex);
         }
      }

      /// <summary>
      /// zeigt die Daten des Standard-Headers an
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="tvd"></param>
      public static void ShowData4Node_GarminCommonHeader(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, TreeViewData tvd) {
         GarminCore.Files.StdFile stdfile = filedata.GetGarminFileAsStd();
         if (stdfile != null) {
            int tab = 20;
            info.AppendLine(FillWithSpace("Headerlength", tab, false, DecimalAndHexAndBinary(stdfile.Headerlength)));
            info.AppendLine(FillWithSpace("Typ", tab, true, "GARMIN " + stdfile.Type));
            info.AppendLine(FillWithSpace("Unknown 0x0C", tab, false, DecimalAndHexAndBinary(stdfile.Unknown_0x0C)));
            info.AppendLine(FillWithSpace("Locked", tab, false, DecimalAndHexAndBinary(stdfile.Locked)));
            info.AppendLine(FillWithSpace("Headerlength", tab, false, DecimalAndHexAndBinary(stdfile.Headerlength)));
            info.AppendLine(FillWithSpace("CreationDate", tab, false, stdfile.CreationDate.ToString("F")));
         }

         firsthexadr = 0;
         hex = HexRange(firsthexadr, filedata.BinaryReader, Math.Min(0x15, filedata.Filelength));
      }

      /// <summary>
      /// zeigt die Daten für den speziellen Header einer Standard-Garmin-Datei an
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      public static void ShowData4Node_GarminSpecialHeader(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, TreeViewData tvd) {
         GarminCore.Files.StdFile stdfile = filedata.GetGarminFileAsStd();
         firsthexadr = 0x15;
         hex = HexRange(firsthexadr, filedata.BinaryReader, stdfile.Headerlength - 0x15);
         if (stdfile != null) {
            info.AppendLine("Headerlength (with common header): " + DecimalAndHexAndBinary(stdfile.Headerlength));
            info.AppendLine();

            try {
               if (filedata.GarminFile is GarminCore.Files.StdFile_TRE)
                  DataTRE.SpecialHeader(info, filedata.GetGarminFileAsTRE());
               else if (filedata.GarminFile is GarminCore.Files.StdFile_LBL)
                  DataLBL.SpecialHeader(info, filedata.GetGarminFileAsLBL());
               else if (filedata.GarminFile is GarminCore.Files.StdFile_RGN)
                  DataRGN.SpecialHeader(info, filedata.GetGarminFileAsRGN(tvd.GetTRE(filedata.Basename)));
               else if (filedata.GarminFile is GarminCore.Files.StdFile_NET)
                  DataNET.SpecialHeader(info, filedata.GetGarminFileAsNET(tvd.GetLBL(filedata.Basename)));
               else if (filedata.GarminFile is GarminCore.Files.StdFile_DEM)
                  DataDEM.SpecialHeader(info, filedata.GetGarminFileAsDEM());
               else if (filedata.GarminFile is GarminCore.Files.StdFile_TYP)
                  DataTYP.SpecialHeader(info, filedata.GetGarminFileAsTYP());
               else if (filedata.GarminFile is GarminCore.Files.StdFile_SRT)
                  DataSRT.SpecialHeader(info, filedata.GetGarminFileAsSRT());
               else if (filedata.GarminFile is GarminCore.Files.StdFile_MDR)
                  DataMDR.SpecialHeader(info, filedata.GetGarminFileAsMDR());



               // weitere Dateitypen



            } catch (Exception ex) {
               ShowExceptionExt("exception on showing data: ", ex);
            }
         }
      }

      /// <summary>
      /// "Verteilerfunktion" auf die einzelnen Funktionen für die jeweilige Standarddatei
      /// </summary>
      /// <param name="info"></param>
      /// <param name="hex"></param>
      /// <param name="firsthexadr"></param>
      /// <param name="filedata"></param>
      /// <param name="nodetype">"Thema" der Info</param>
      /// <param name="idx">wenn größer oder gleich 0, dann der Index auf ein Objekt einer Tabelle</param>
      /// <param name="tn"></param>
      public static void ShowData4Node_GarminStdFile(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4File filedata, NodeContent.NodeType nodetype, int idx, TreeViewData tvd) {
         hex = null;
         firsthexadr = 0;

         if (filedata != null &&
             filedata.GarminFile != null) {
            try {
               if (filedata.GarminFile is GarminCore.Files.StdFile_TRE)
                  DataTRE.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_LBL)
                  DataLBL.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_RGN)
                  DataRGN.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_NET)
                  DataNET.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_DEM)
                  DataDEM.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_TYP)
                  DataTYP.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_SRT)
                  DataSRT.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.StdFile_MDR)
                  DataMDR.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);


               else if (filedata.GarminFile is GarminCore.Files.File_TDB)
                  DataTDB.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.File_MPS)
                  DataMPS.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);
               else if (filedata.GarminFile is GarminCore.Files.File_MDX)
                  DataMDX.SectionAndIndex(info, out hex, out firsthexadr, filedata, nodetype, idx, tvd);

               //else
               //   MessageBox.Show("internal error: unknown file");

            } catch (Exception ex) {
               ShowExceptionExt("exception on showing data: ", ex);
            }
         } else
            info.AppendLine("internal error: don't find a file");
         //MessageBox.Show("internal error: don't find a file");
      }

      #endregion



      /// <summary>
      /// Textformatierung
      /// </summary>
      /// <param name="txt">Text der 1. Spalte</param>
      /// <param name="newlength">Start der 2. Spalte</param>
      /// <param name="paramsastext">2. Spalte in "'" setzen</param>
      /// <param name="txtlst">Text/e der 2. Spalte</param>
      /// <returns></returns>
      protected static string FillWithSpace(string txt, int newlength, bool paramsastext, params string[] txtlst) {
         txt += ":";
         if (newlength - txt.Length > 0)
            txt += new string(' ', newlength - txt.Length);
         if (paramsastext)
            return txt + "'" + string.Join("", txtlst) + "'";
         else
            return txt + string.Join("", txtlst);
      }



      #region Hilfsfkt. für die Datenausgabe als Text

      protected static string StreetOffsetAndText(GarminCore.Files.StdFile_LBL lbl, int offset) {
         return string.Format("{0} ({1})", offset, lbl.GetText((uint)offset, false));
      }

      protected static string DecimalAndHexAndBinary(byte v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary(v, binlen) : "");
      }

      protected static string DecimalAndHexAndBinary(int v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary((ulong)v, binlen) : "");
      }

      protected static string DecimalAndHexAndBinary(short v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary((ulong)v, binlen) : "");
      }

      protected static string DecimalAndHexAndBinary(long v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary((ulong)v, binlen) : "");
      }

      protected static string DecimalAndHexAndBinary(ushort v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary(v, binlen) : "");
      }

      protected static string DecimalAndHexAndBinary(uint v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary(v, binlen) : "");
      }

      protected static string DecimalAndHexAndBinary(ulong v, int binlen = 0) {
         return v.ToString() + " / 0x" + v.ToString("X") + (binlen > 0 ? " / 0b" + Binary(v, binlen) : "");
      }

      protected static string DecimalAndHex(System.Drawing.Color v, bool bWithAlpha = false) {
         return (bWithAlpha ? "A=[" + DecimalAndHexAndBinary(v.A) + "], " : "") +
                              "B=[" + DecimalAndHexAndBinary(v.B) + "], " +
                              "G=[" + DecimalAndHexAndBinary(v.G) + "], " +
                              "R=[" + DecimalAndHexAndBinary(v.R) + "]";
      }


      protected static string Binary(ulong v, int lengt = 8) {
         string bin = "";
         for (int i = 0; i < lengt; i++) {
            bin = ((v & 0x1) != 0 ? "1" : "0") + bin;
            v >>= 1;
         }
         return bin;
      }

      protected static string Binary(byte[] b) {
         string bin = "";
         for (int i = 0; i < b.Length; i++) {
            byte v = b[i];
            for (int j = 0; j < 8; j++) {
               bin += (v & 0x1) != 0 ? "1" : "0";
               v >>= 1;
            }
         }
         return bin;
      }

      protected static string HexString(byte[] v) {
         StringBuilder sb = new StringBuilder();
         if (v != null)
            for (int i = 0; i < v.Length; i++) {
               if (i > 0)
                  sb.Append(" ");
               sb.AppendFormat("{0:X2}", v[i]);
            }
         return sb.ToString();
      }

      /// <summary>
      /// erzeugt einen Byte-Bereich aus dem <see cref="GarminCore.BinaryReaderWriter"/>, der max. auf <see cref="MaxBytes4Hex"/> bzw.
      /// durch das Ende des <see cref="GarminCore.BinaryReaderWriter"/> begrenzt ist
      /// </summary>
      /// <param name="firsthexadr"></param>
      /// <param name="binreader"></param>
      /// <param name="length"></param>
      protected static byte[] HexRange(long firsthexadr, GarminCore.BinaryReaderWriter binreader, long length) {
         byte[] hex = null;
         if (binreader != null) {
            binreader.Seek(firsthexadr);
            if (firsthexadr + length > binreader.Length)
               length = (int)(binreader.Length - firsthexadr);
            hex = binreader.ReadBytes(Math.Min(MaxBytes4Hex, (int)Math.Min(length, int.MaxValue)));
         }
         return hex;
      }

      #endregion

      static void ShowExceptionExt(string txt, Exception ex) {
         MessageBox.Show(txt + Environment.NewLine +
                         ex.Message + Environment.NewLine + Environment.NewLine +
                         ex.StackTrace,
                         "Error",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
      }


   }
}
