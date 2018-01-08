using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lab3_2
{
    class Info
    {
        public string LastName { get; set; }
        public double Salary { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public ObservableCollection<string> Positions { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<string> Streets { get; set; }

        public Info()
        {
            Positions = new ObservableCollection<string>
            {
                "Директор",
                "Продавец"
            };

            Cities = new ObservableCollection<string>
            {
                "Минск",
                "Гомель"
            };

            Streets = new ObservableCollection<string>
            {
                "Московская",
                "Могилевская"
            };
        }
    }
}
