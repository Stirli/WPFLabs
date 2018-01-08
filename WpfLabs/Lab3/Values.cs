using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lab3
{
    public class Values
    {
        public double XStart { get; set; }
        public double XStop { get; set; }
        public double Step { get; set; }
        public double N { get; set; }
        public ObservableCollection<string> Results { get; set; }

        public Values()
        {
            Results = new ObservableCollection<string>();
        }
    }

}
