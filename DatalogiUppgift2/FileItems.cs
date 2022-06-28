using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogiUppgift2
{
    public class FileItems
    {
        // Creates an File-item with data
        public int Id { get; set; }
        public string Title { get; set; }
        public int Searchresult { get; set; }
        public string[] text { get; set; }

        public FileItems()
        {

        }
    }
}
