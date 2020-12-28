using Newtonsoft.Json;
using System.Collections.Generic;

namespace andriesk_api_htmltopdf.Models
{
    public class WkHtmlToPdfDto
    {
        [JsonProperty("h2p-fileName")]
        public string FileName { get; set; }                

        [JsonProperty("h2p-layout")]
        public string Layout { get; set; }                  

        [JsonProperty("h2p-pageSize")]
        public string PageSize { get; set; }                

        [JsonProperty("h2p-allowJavascript")]
        public string AllowJavascript { get; set; }          

        [JsonProperty("h2p-margins")]
        public PrintMargin Margins { get; set; } = new PrintMargin();

        [JsonProperty("h2p-grayscale")]
        public string Grayscale { get; set; }          
        
        [JsonProperty("h2p-generateBookmarks")]
        public string GenerateBookmarks { get; set; }  

        [JsonProperty("h2p-enableHtmlFormRender")]
        public string EnableHtmlFormRender { get; set;}
         
        /// <summary>
        /// URL to download / convert to PDF
        /// </summary>
        [JsonProperty("h2p-url")]
        public string URL { get; set; }                    

        /// <summary>
        /// User provided html
        /// </summary>
        public string HTML { get; set; }
 
        /// <summary>
        /// Specifies wether this is converted from url of a user provided html page
        /// </summary>
        [JsonProperty("h2p-conversionType")]
        public string ConversionType { get; set; }            
    }
      
}
