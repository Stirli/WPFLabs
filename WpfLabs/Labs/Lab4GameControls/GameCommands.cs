using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4GameControls
{
    using System.Windows.Input;

   public static class GameCommands
    {
        static GameCommands()
        {
            Start = new RoutedUICommand("Старт", "Start", typeof(GameControl)) { InputGestures = { new KeyGesture(Key.F5) } };
            Stop = new RoutedUICommand("Стоп", "Stop", typeof(GameControl)) { InputGestures = { new KeyGesture(Key.F9) } };
            Fire = new RoutedUICommand("Огонь!", "Fire", typeof(GameControl)) { InputGestures = { new KeyGesture(Key.Space) } };
        }

        public static RoutedUICommand Start { get; set; }
        public static RoutedUICommand Stop { get; set; }
        public static RoutedUICommand Fire { get; set; }
    }
}
