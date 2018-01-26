using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Lab5
{
    static class ProgramCommands
    {
        public static RoutedUICommand SetBrush { get; set; }
        public static RoutedUICommand Ok { get; set; }

        static ProgramCommands()
        {
            SetBrush = new RoutedUICommand("Кисть", "SetBrush", typeof(Window));
            SetBrush.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Control));
            Ok = new RoutedUICommand("OK", "Ok", typeof(BrushDialog));
            Ok.InputGestures.Add(new KeyGesture(Key.Return));
        }
    }
}
