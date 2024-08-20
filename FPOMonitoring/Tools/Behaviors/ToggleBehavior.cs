using HandyControl.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FPOMonitoring.Tools.Behaviors
{
    public class ToggleBehavior : Behavior<ToggleButton>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;
            switch (toggle.Name) 
            {
                case "btnTimezoneAscending":
                    if (sender is ToggleButton button && button.Tag is ItemsControl itemsControl)
                    {
                        if (button.IsChecked == true)
                        {
                            itemsControl.Items.SortDescriptions.Add(new SortDescription("DisplayName", ListSortDirection.Ascending));
                        }
                        else
                        {
                            itemsControl.Items.SortDescriptions.Clear();
                        }
                    }
                    break;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}
