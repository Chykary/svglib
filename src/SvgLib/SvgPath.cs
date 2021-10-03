using System.Collections.Generic;
using System.Xml;

namespace SvgLib
{
    public sealed class SvgPath : SvgElement
    {
        private SvgPath(XmlElement element)
            : base(element)
        {
        }

        internal static SvgPath Create(XmlElement parent)
        {
            var element = parent.OwnerDocument.CreateElement("path");
            parent.AppendChild(element);
            return new SvgPath(element);
        }

        private string D
        {
            get => Element.GetAttribute("d");
            set => Element.SetAttribute("d", value);
            
        }

        public double Length
        {
            get => Element.GetAttribute("pathLength", 0.0);
            set => Element.SetAttribute("pathLength", value);
        }

        public bool Closed;

        private List<SvgPathPoint> _points = new List<SvgPathPoint>();
        public void AddPoint(int x, int y, SvgPathPointSpec type)
        {
            if (_points.Count == 0)
            {
                type = SvgPathPointSpec.Initial;
            }

            _points.Add(new SvgPathPoint() { X = x, Y = y, Type = type });
        }

        public void CompilePath()
        {
            string path = string.Empty;

            int i = 0;

            foreach (SvgPathPoint p in _points)
            {
                path += p.PointString;
            }

            if (Closed)
            {
                path += "z";
            }

            D = path;
        }
    }

    public class SvgPathPoint
    {
        public SvgPathPointSpec Type;

        public int X;
        public int Y;

        public string PointString
        {
            get => $"{SvgPointIdentifier} {X},{Y} \n";
        }

        private string SvgPointIdentifier
        {
            get
            {
                switch(Type)
                {                    
                    case SvgPathPointSpec.MoveAbsolute:
                        return "L";
                    case SvgPathPointSpec.MoveRelative:
                        return "l";
                    default:
                    case SvgPathPointSpec.Initial:
                        return "M";
                }
            }
        }
    }

    public enum SvgPathPointSpec
    {
        Initial, MoveAbsolute, MoveRelative
    }
}
