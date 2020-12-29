using Newtonsoft.Json;
using System.Collections.Generic;

namespace andriesk_api_htmltopdf.Models
{
    public class WkHtmlToPdfDto
    {

        [JsonProperty("h2p-allowJavascript")]
        public string AllowJavascript { get; set; }


        /// <summary>
        /// Specifies wether this is converted from url of a user provided html page
        /// </summary>
        [JsonProperty("h2p-conversionType")]
        public string ConversionType { get; set; }

        private string _dpi;
        [JsonProperty("h2p-dpi")]
        public string DPI { get => "--dpi " + (_dpi ?? "96"); set => _dpi = value; }

        [JsonProperty("h2p-enableHtmlFormRender")]
        public string EnableHtmlFormRender { get; set; }

        [JsonProperty("h2p-fileName")]
        public string FileName { get; set; }

        [JsonProperty("h2p-generateBookmarks")]
        public string GenerateBookmarks { get; set; }

        [JsonProperty("h2p-grayscale")]
        public string Grayscale { get; set; }

        /// <summary>
        /// User provided html
        /// </summary>
        public string HTML { get; set; }

        private string _layout;
        [JsonProperty("h2p-layout")]
        public string Layout { get => "--orientation " + (_layout ?? "Portrait"); set => _layout = value; }

        [JsonProperty("h2p-margins")]
        public PrintMargin Margins { get; set; } = new PrintMargin();

        private string _pageSize;
        [JsonProperty("h2p-pageSize")]
        public string PageSize { get => "--page-size " + (_pageSize ?? "A4"); set => _pageSize = value; }

        /// <summary>
        /// URL to download / convert to PDF
        /// </summary>
        [JsonProperty("h2p-url")]
        public string URL { get; set; }

        private string _viewportSize;
        /// <summary>
        /// Resolution of the screen when rendering the html
        /// </summary>
        [JsonProperty("h2p-viewportsize")]
        public string ViewPortSize { get => "--viewport-size " + (_viewportSize ?? "1920x1080"); set => _viewportSize = value; }

        private string _zoom;
        /// <summary>
        /// Default zoom level of the page
        /// </summary>
        [JsonProperty("h2p-zoom")]
        public string Zoom { get => "--zoom " + (_zoom ?? "1"); set => _zoom = value; }

    }

}
