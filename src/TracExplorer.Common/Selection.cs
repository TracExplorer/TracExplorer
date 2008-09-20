using System;
using System.Collections.Generic;
using System.Text;

namespace TracExplorer.Common
{
    public class Selection
    {
        private string _delimiter;

        public string Delimiter
        {
            get { return _delimiter; }
            set { _delimiter = value; }
        }

        private string _format;

        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        private SortableBindingList<string> _items = new SortableBindingList<string>();

        public SortableBindingList<string> Items
        {
            get { return _items; }
            set { _items = value; }
        }

    }
}
