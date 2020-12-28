using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using andriesk_api_htmltopdf.Models;
using System;
using System.Net;
using System.Net.Http;
using static RequestTests.WebRequests;

namespace RequestTests
{
    [TestClass]
    public class ConversionTest
    {
        public string _testingHostUrl { get; set; } = "";
        private readonly string _staticHtmlPage = "";
        private readonly string _bearerToken = "";
        [TestMethod]
        public void UrlToPDFTest()
        {
            var request = new ApiRequest()
            {
                RequestType = RequestType.POST,
                JsonBody = JsonConvert.SerializeObject(new WkHtmlToPdfDto()
                {
                    ConversionType = "url",
                    FileName = "output.pdf",
                    URL = "https://www.google.com"
                }),
                ResourceUrl = _testingHostUrl + "/api/htmltopdf/",
            };

            ValidateResult(request);
             
        }

        [TestMethod]
        public void HTMLToPDFTest()
        {
            var bytes = System.IO.File.ReadAllBytes(_staticHtmlPage);

            var request = new ApiRequest()
            {
                RequestType = RequestType.POST,
                JsonBody = JsonConvert.SerializeObject(new WkHtmlToPdfDto()
                {
                    ConversionType = "html",
                    FileName = "output.pdf",
                    HTMLPages = new System.Collections.Generic.List<HtmlPage>()
                    {
                         new HtmlPage() { Page = Convert.ToBase64String(bytes) }
                    },
                    Margins = new PrintMargin()
                    {
                        Left = "10mm",
                        Top = "10mm",
                        Right = "10mm",
                    },
                }),
                ResourceUrl = _testingHostUrl + "/api/htmltopdf/",
                AuthorizationBearerToken = _bearerToken,
            };

            ValidateResult(request);
        }

        [TestMethod]
        public void GrayscaleTest()
        {
            var bytes = System.IO.File.ReadAllBytes(_staticHtmlPage);
            
            var request = new ApiRequest()
            {
                RequestType = RequestType.POST,
                JsonBody = JsonConvert.SerializeObject(new WkHtmlToPdfDto()
                {
                    ConversionType = "html",
                    FileName = "output.pdf",
                    Grayscale = "-g",
                    HTMLPages = new System.Collections.Generic.List<HtmlPage>()
                     {
                         new HtmlPage() { Page = Convert.ToBase64String(bytes) }
                     }
                }),

                
                ResourceUrl = _testingHostUrl + "/api/htmltopdf/",
            };

            ValidateResult(request);
        }



        private static void ValidateResult(ApiRequest request)
        {
            var result = WebRequests.MakeRequest(request).GetAwaiter().GetResult();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }
 
    }
}
