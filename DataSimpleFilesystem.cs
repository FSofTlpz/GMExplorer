using System.Text;

namespace GMExplorer {
   class DataSimpleFilesystem : Data {

      public static void ShowData(StringBuilder info, out byte[] hex, out long firsthexadr, NodeContent.Content4PhysicalFile filedata, TreeViewData tvd) {
         info.AppendLine("Description:                    " + filedata.SimpleFilesystem.ImgHeader.Description);
         info.AppendLine("CreationDate:                   " + filedata.SimpleFilesystem.ImgHeader.CreationDate.ToString("F"));
         info.AppendLine("Files:                          " + filedata.SimpleFilesystem.FileCount.ToString());
         info.AppendLine("FATSize:                        " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.FATSize) + " Bytes");
         info.AppendLine();
         info.AppendLine("HeaderLength:                   " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.HeaderLength) + " Bytes");
         info.AppendLine("  XOR-Byte:                     " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.XOR));
         info.AppendLine("  UpdateMonth:                  " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.UpdateMonth));
         info.AppendLine("  UpdateYear:                   " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.UpdateYear));
         info.AppendLine("  MapsourceFlag:                " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.MapsourceFlag));
         info.AppendLine("  Checksum:                     " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.Checksum));
         info.AppendLine("  SectorsPerTrack:              " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.SectorsPerTrack));
         info.AppendLine("  HeadsPerCylinder:             " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.HeadsPerCylinder));
         info.AppendLine("  Cylinders:                    " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.Cylinders));
         info.AppendLine("  CreationDate:                 " + filedata.SimpleFilesystem.ImgHeader.CreationDate.ToString("F"));
         info.AppendLine("  HeadSectors:                  " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.HeadSectors));
         info.AppendLine("  Description1:                 '" + filedata.SimpleFilesystem.ImgHeader.Description1 + "'");
         info.AppendLine("  HeadsPerCylinder2:            " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.HeadsPerCylinder2));
         info.AppendLine("  SectorsPerTrack2:             " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.SectorsPerTrack2));
         info.AppendLine("  BlocksizeExp1:                " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.BlocksizeExp1));
         info.AppendLine("  BlocksizeExp2:                " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.BlocksizeExp2));
         info.AppendLine("  Blocks4Img:                   " + DecimalAndHexAndBinary(filedata.SimpleFilesystem.ImgHeader.Blocks4Img));
         info.AppendLine("  Description2:                 '" + filedata.SimpleFilesystem.ImgHeader.Description2 + "'");
         info.AppendLine("  StartHeadNumber4Partition:    " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.StartHeadNumber4Partition));
         info.AppendLine("  StartSectorNumber4Partition:  " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.StartSectorNumber4Partition));
         info.AppendLine("  StartHeadNumber4Partition:    " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.StartHeadNumber4Partition));
         info.AppendLine("  Systemtyp:                    " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.Systemtyp));
         info.AppendLine("  LastHeadNumber4Partition:     " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.LastHeadNumber4Partition));
         info.AppendLine("  LastSectorNumber4Partition:   " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.LastSectorNumber4Partition));
         info.AppendLine("  LastCylinderNumber4Partition: " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.LastCylinderNumber4Partition));
         info.AppendLine("  RelativeSectors:              " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.RelativeSectors));
         info.AppendLine("  LastSectorNumber4IMG:         " + DecimalAndHexAndBinary((ulong)filedata.SimpleFilesystem.ImgHeader.LastSectorNumber4IMG));

         firsthexadr = 0;
         hex = HexRange(firsthexadr, filedata.BinaryReader, filedata.SimpleFilesystem.FATSize);
      }

   }
}
