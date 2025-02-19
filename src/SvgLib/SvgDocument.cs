using System.IO;
using System.Xml;

namespace SvgLib
{
    public sealed class SvgDocument : SvgContainer
    {
        private readonly XmlDocument _document;

        private SvgDocument(XmlDocument document, XmlElement element)
            : base(element)
        {
            _document = document;
        }

        public static SvgDocument Create()
        {
            var document = new XmlDocument();
            var rootElement = document.CreateElement("svg");
            document.AppendChild(rootElement);
            rootElement.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            return new SvgDocument(document, rootElement);
        }

        public void Save(Stream stream) => _document.Save(stream);

        public void Save(string path) => _document.Save(path);

        public SvgViewBox ViewBox
        {
            get => Element.GetAttribute("viewBox", new SvgViewBox());
            set => Element.SetAttribute("viewBox", value.ToString());
        }
    }
}
