namespace andriesk_api_htmltopdf.Models
{

    public class PrintMargin
    {
        private string _valueLeft;
        private string _valueTop;
        private string _valueBottom;
        private string _valueRight;

        public string Left { get => "-L " + (_valueLeft ?? "10mm"); set => _valueLeft = value; }                      
        public string Top { get => "-T " + (_valueTop ?? "10mm"); set => _valueTop = value;  }                       
        public string Right { get => "-R " + (_valueRight ?? "10mm"); set => _valueRight = value; }                   
        public string Bottom { get => "-B " + (_valueBottom ?? "10mm"); set => _valueBottom = value; }                
    }
   
}
