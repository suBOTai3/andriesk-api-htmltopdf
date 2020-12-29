using andriesk_api_htmltopdf.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace andriesk_api_htmltopdf.Utilities
{
    public static class WkHtmlToPdf
    {
        public static async Task<byte[]> GeneratePDF(WkHtmlToPdfDto requestOptions, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(BashExecute.ThisCommand("wkhtmltopdf --version")))
                throw new FileNotFoundException("wkhtmltopdf not installed in container or could not be found in $PATH");

            var commandlineArgs = GetCommandlineArgs(requestOptions, configuration);
            var result = "";

            if (requestOptions.ConversionType == "url" || !string.IsNullOrWhiteSpace(requestOptions.URL))
                result = BashExecute.ThisCommand($"wkhtmltopdf {commandlineArgs} {requestOptions.URL} pdf/output.pdf");

            if (requestOptions.ConversionType == "html" || !string.IsNullOrWhiteSpace( requestOptions.HTML))
            {
                BashExecute.ThisCommand("mkdir pdf && chmod a+w pdf");

                BashExecute.ThisCommand("rm -f pdf/*.html");

                ContainerFileSystem.CreateHtmlFile("html" + 1.ToString().PadLeft(2, '0') + ".html", requestOptions.HTML);

                BashExecute.ThisCommand($"wkhtmltopdf {commandlineArgs} --enable-local-file-access <<< ls pdf/*.html pdf/{requestOptions.FileName}");
            }

            Console.WriteLine(result);

            return await ContainerFileSystem.GetOutputFile(new System.Threading.CancellationToken());
        }

        /// <summary>
        /// Request options take preference over configuration options, EXCEPT for the 'allow javascript' option
        /// which can present a security vulnerability
        /// </summary>
        /// <param name="requestOptions">List of options passed by the user</param>
        /// <param name="configuration">Appsettings.config configuration</param>
        /// <returns>Single string with the value of the config options</returns>
        private static string GetCommandlineArgs(WkHtmlToPdfDto requestOptions, IConfiguration configuration)
        {
            var commandlineArgs = "";

            commandlineArgs += " " + configuration["wkHtmlToPdf:AllowJavascript"] ?? "-n --disable-javascript";
            commandlineArgs += " " + requestOptions.DPI ?? configuration["wkHtmlToPdf:DPI"] ?? "96";               
            commandlineArgs += " " + requestOptions.EnableHtmlFormRender ?? configuration["wkHtmlToPdf:EnableHtmlFormRender"] ?? "";            // default = no  (see --enable-forms)
            commandlineArgs += " " + requestOptions.GenerateBookmarks ?? configuration["wkHtmlToPdf:GenerateBookmarks"] ?? "";                  // default = yes (see --no-outline)
            commandlineArgs += " " + requestOptions.Grayscale ?? configuration["wkHtmlToPdf:Grayscale"] ?? "";                                  // default = no  (see --grayscale)
            commandlineArgs += " " + requestOptions.Layout ?? configuration["wkHtmlToPdf:Layout"] ?? "Portrait";
            commandlineArgs += " " + requestOptions.Margins.Left ?? configuration["wkHtmlToPdf:Margins:Left"] ?? "-L 10mm";
            commandlineArgs += " " + requestOptions.Margins.Top ?? configuration["wkHtmlToPdf:Margins:Top"] ?? "-T 10mm";
            commandlineArgs += " " + requestOptions.Margins.Right ?? configuration["wkHtmlToPdf:Margins:Right"] ?? "-R 10mm";
            commandlineArgs += " " + requestOptions.Margins.Bottom ?? configuration["wkHtmlToPdf:Margins:Bottom"] ?? "-B 10mm";
            commandlineArgs += " " + requestOptions.PageSize ?? configuration["wkHtmlToPdf:PageSize"] ?? "A4";
            commandlineArgs += " " + requestOptions.ViewPortSize ?? configuration["wkHtmlToPdf:ViewPortSize"] ?? "1920x1080";               // default = no  (see --enable-forms)

            return commandlineArgs;
        }
    }
}
