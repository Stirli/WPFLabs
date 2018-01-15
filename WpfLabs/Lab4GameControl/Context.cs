using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lab4GameControl.Annotations;

namespace Lab4GameControl
{
    public class Context : INotifyPropertyChanged
    {
        public Context()
        {
            GameObjects = new List<GameObject>
            {
                (Bomb = new Bomb { Y = 20}),
                (Bomber = new Bomber { Y = 20, Bomb = Bomb}),
            };
        }

        public List<GameObject> GameObjects { get; set; }

        public bool IsReady { get; set; }

       
        public Bomber Bomber { get; set; }
        public Bomb Bomb { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
