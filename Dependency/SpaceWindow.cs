using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dependency
{
    public class SpaceWindow : Window
    {
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            set => SetValue(SpaceProperty, value);
            get => (int)GetValue(SpaceProperty);
        }

        static SpaceWindow()
        {
            FrameworkPropertyMetadata metaData = new FrameworkPropertyMetadata();
            metaData.Inherits = true;

            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof(SpaceWindow), metaData);
        }

    }
}
