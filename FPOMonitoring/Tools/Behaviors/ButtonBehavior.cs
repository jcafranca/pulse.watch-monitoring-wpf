using FPOMonitoring.ViewModel;
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
    public class ButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button btn) 
            { 
                switch (btn.Name)
                {
                    case "btnTimezoneConfig":
                        var value = ViewModelLocator.Instance.NonClientAreaVM.IsTimezonePopupOpen;
                        ViewModelLocator.Instance.NonClientAreaVM.IsTimezonePopupOpen = !value;
                        break;
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}
