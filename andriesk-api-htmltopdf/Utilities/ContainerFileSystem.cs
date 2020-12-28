using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace andriesk_api_htmltopdf.Utilities
{
    public static class ContainerFileSystem
    {

        public static async Task<byte[]> GetOutputFile(CancellationToken token)
        {
            var currentDir = Path.Combine(Directory.GetCurrentDirectory(), "pdf");
            var pdfs = new DirectoryInfo(currentDir).GetFiles("*.pdf", SearchOption.AllDirectories);

            if (pdfs.Length > 0 && pdfs[0] is FileInfo fi)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(fi.FullName);

                File.Delete(fi.FullName);   //delete to prevent sniffing

                return bytes;
            }

            return null;
        }
  
        public static FileInfo CreateHtmlFile(string filename, string htmlContent)
        { 
            try
            {
                File.WriteAllText($"pdf/{filename}", htmlContent);
                return new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "pdf")).GetFiles(filename)[0];
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException($"Could not create the html file {filename}", ex);
            }
        }
    }
}
