using HandyControl.Controls;
using HandyControl.Interactivity;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace FPOMonitoring.Tools.Behaviors
{
    public class PopupBehavior : Behavior<Popup>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Opened += AssociatedObject_Opened;
        }

        private void AssociatedObject_Opened(object sender, EventArgs e)
        {
            var popup = sender as Popup;
            if (popup != null && popup.Child != null) 
            {
                var element = FindVisualChild<SearchBar>(popup.Child);
                if (element != null)
                {
                    element.Focus();
                    element.Text = string.Empty;
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Opened -= AssociatedObject_Opened;
        }
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }

                var result = FindVisualChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
