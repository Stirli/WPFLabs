using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lab3_2
{
    class Info
    {
        private double _salary;
        private string _lastName;
        private string _position;
        private string _city;
        private string _street;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Длина строки должна быть больше 3");
                _lastName = value;
            }
        }

        public double Salary
        {
            get { return _salary; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Значение должено быть больше 0.");
                _salary = value;
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Длина строки должна быть больше 3");
                _position = value;
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Длина строки должна быть больше 3");
                _city = value;
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Длина строки должна быть больше 3");
                _street = value;
            }
        }

        public int HouseNumber { get; set; }

        public ObservableCollection<string> Positions { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<string> Streets { get; set; }
        public ObservableCollection<string> Employees { get; set; }

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

            Employees = new ObservableCollection<string>();
        }

        public static implicit operator string(Info info)
        {
            return string.Format("Фамилия: {0}, З/П: {1}, Должность: {2}, Город: {3}, Улица: {4}, Дом: {5}", info.LastName, info.Salary, info.Position, info.City, info.Street, info.HouseNumber);
        }
    }
}
