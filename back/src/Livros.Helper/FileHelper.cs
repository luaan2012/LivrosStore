// using ClosedXML.Excel;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.Linq;
// using System.Collections.Generic;
// using System.IO;
// using System.Text;

namespace TudoFarmaRep.CrossCutting.Helper
{
    public class FileHelper : IDisposable
    {
        public static void CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        // public static Dictionary<string, string> SaveFiles(IFormFileCollection files, string pathFile)
        // {
        //     var arquivos = new Dictionary<string, string>();
        //     foreach (var file in files)
        //     {
        //         var extensao = Path.GetExtension(file.FileName).Replace(".", "");
        //         var fileName = $"{file.Name}_{DateTime.Now.ToString("ddMMyyyyHHmmssFFF")}.{extensao}";
        //         var caminho = Path.Combine(pathFile, fileName);

        //         if (!Directory.Exists(pathFile))
        //             Directory.CreateDirectory(pathFile);

        //         using (var sw = File.Create(caminho))
        //         {
        //             file.CopyTo(sw);
        //             sw.Flush();
        //             arquivos.Add(file.Name, fileName);
        //         }
        //     }

        //     return arquivos;
        // }

        // public static string SaveFiles(IFormFileCollection files, string nomeArquivo, string diretorio)
        // {
        //     var pathFile = string.Empty;
        //     var fileName = string.Empty;
        //     foreach (var file in files)
        //     {
        //         var extensao = Path.GetExtension(file.FileName).Replace(".", "");
        //         fileName = $"{nomeArquivo}.{extensao}";
        //         pathFile = Path.Combine(diretorio, fileName);

        //         if (!Directory.Exists(diretorio))
        //             Directory.CreateDirectory(diretorio);

        //         using (var sw = File.Create(pathFile))
        //         {
        //             file.CopyTo(sw);
        //             sw.Flush();
        //         }
        //     }

        //     return fileName;
        // }

        public static void SaveFiles(Stream stream, string fileName, string path)
        {            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var sw = File.Create(Path.Combine(path, fileName)))
            {
                stream.CopyTo(sw);
                sw.Flush();
                sw.Close();
                stream.Position = 0;
            }
        }

        /// <summary>
        /// File is a object with params FileName and Base4
        /// </summary>
        /// <param name="files"></param>
        /// <param name="pathFile"></param>        
        public static void SaveFilesBase64(dynamic file, string pathFile)
        {            
            var fileName = $"{file.FileName}.{file.Extensao}";
            var fullPath = Path.Combine(pathFile, fileName);
            var stream = new MemoryStream(Convert.FromBase64String(file.Base64));

            if (!Directory.Exists(pathFile))
                Directory.CreateDirectory(pathFile);

            using (var sw = File.Create(fullPath))
            {                
                stream.CopyTo(sw);                
                sw.Flush();
            }
        }
      
        public static string ReadFile(string path, string fileName)
        {
            try
            {
                var pathFile = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                    return string.Empty;

                if (!File.Exists(pathFile))
                    return string.Empty;

                var contents = File.ReadAllText(pathFile);
                return contents.Replace("\r\n", "");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        // public static string ReadTempFile(IFormFile file)
        // {
        //     try
        //     {
        //         var pastaTemp = Path.GetTempPath();
        //         var diretorio = Path.Combine(pastaTemp, file.FileName);
        //         using (var sw = File.Create(diretorio))
        //         {
        //             file.CopyTo(sw);
        //             sw.Flush();
        //         }                

        //         return File.ReadAllText(diretorio)?.Replace("\r\n", "");
        //     }
        //     catch (Exception ex)
        //     {
        //         throw;
        //     }
        // }

        // public static string CreateTempFile(IFormFile file)
        // {
        //     try
        //     {
        //         var pastaTemp = Path.GetTempPath();
        //         var diretorio = Path.Combine(pastaTemp, file.FileName);
        //         using (var sw = File.Create(diretorio))
        //         {
        //             file.CopyTo(sw);
        //             sw.Flush();
        //         }

        //         return diretorio;                
        //     }
        //     catch (Exception ex)
        //     {
        //         throw;
        //     }
        // }


        // public static string ReadTempFileExcel(IFormFile file)
        // {
        //     try
        //     {
        //         var pasta = CreateTempFile(file);

        //         var xls = new XLWorkbook(pasta);
        //         var planilha = xls.Worksheets.FirstOrDefault();
        //         var totalLinhas = planilha.Rows().Count();

        //         var cnpjs = new StringBuilder();

        //         for (int l = 1; l <= totalLinhas; l++)
        //         {                    
        //             var cnpj = planilha.Cell($"A{l}").Value.ToString();
        //             if (!string.IsNullOrEmpty(cnpj))                    
        //             {
        //                 if (l <= totalLinhas && l > 1)
        //                     cnpjs.Append(",");

        //                 cnpjs.Append(cnpj);                        
        //             }                  
        //         }

        //         return cnpjs.ToString();
        //     }
        //     catch (Exception e)
        //     {
        //         throw;
        //     }
        // }

            public static bool RecordFile(string path, string text, string fileName, bool overwrite = true)
        {
            try
            {
                var pathFile = Path.Combine(path, fileName);

                if (!Directory.Exists(pathFile))
                    Directory.CreateDirectory(path);

                if (!File.Exists(pathFile))
                    File.Create(pathFile).Dispose();

                using (StreamWriter writer = new StreamWriter(pathFile, overwrite))
                {
                    writer.WriteLine(text);

                    writer.Flush();
                    writer.Close();
                    writer.Dispose();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
