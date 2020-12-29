using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using andriesk_api_htmltopdf.Models;
using andriesk_api_htmltopdf.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace andriesk_api_htmltopdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HtmlToPdfController : BaseController
    {
        private readonly ILogger<HtmlToPdfController> _logger;
        private readonly IConfiguration _configuration;

        public HtmlToPdfController(IConfiguration configuration, ILogger<HtmlToPdfController> logger)
        {
            this._logger = logger;
            this._configuration = configuration;
        }
          
        [HttpPost]
        public async Task<ActionResult<byte[]>> Convert()
        {
            var htmlString = "";
            var downloadName = "output.pdf";

            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    htmlString = await reader.ReadToEndAsync();
                }

                var headers = Request.Headers.Where(hh => hh.Key.StartsWith("H2P")).ToDictionary(k => k.Key, v => v.Value.ToString());
                var json = JsonConvert.SerializeObject(headers, Formatting.Indented);
                var options = JsonConvert.DeserializeObject<WkHtmlToPdfDto>(json);

                options.HTML = htmlString;

                if (!string.IsNullOrWhiteSpace(options.FileName))
                    downloadName = options.FileName;

                if (Log.IsEnabled(LogEventLevel.Debug))
                    _logger.LogDebug("body passed in:\n\n" + options.ToString());

                var byteArr = WkHtmlToPdf.GeneratePDF(options, _configuration).GetAwaiter().GetResult();
                var memStream = new System.IO.MemoryStream(byteArr);

                Response.ContentType = new MediaTypeHeaderValue("application/pdf").ToString();
                return new FileStreamResult(memStream, "application/pdf")
                {
                    FileDownloadName = downloadName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                return StatusCode(500, new { message = "Unable to convert the html to pdf" });
            }
        }
         
    }
}
