using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer.App.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string CreationTime { get; set; }
        public int Width { get; set; }
    }
}
