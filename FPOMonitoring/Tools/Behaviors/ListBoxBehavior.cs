using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.ViewModel;
using HandyControl.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FPOMonitoring.Tools.Behaviors
{
    public class ListBoxBehavior : Behavior<ListBox>
    {
        private readonly LogService logService = new LogService();
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
            AssociatedObject.KeyDown += AssociatedObject_KeyDown;
        }

        private void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AssociatedObject.SelectedItem != null)
                {
                    ViewModelLocator.Instance.NonClientAreaVM.IsTimezonePopupOpen = false;
                    ViewModelLocator.Instance.NonClientAreaVM.Timezone.ToList().ForEach(timezone =>
                    {
                        if (!ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Contains(timezone))
                        {
                            ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Add(timezone);
                        }
                    });
                }
            }
        }

        private void AssociatedObject_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModelLocator.Instance.NonClientAreaVM.IsTimezonePopupOpen = false;
            ViewModelLocator.Instance.NonClientAreaVM.Timezone.ToList().ForEach(timezone =>
            {
                if (!ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Contains(timezone))
                {
                    ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Add(timezone);
                }
            });
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssociatedObject.SelectedItem != null)
            {
                var temp_selectedtz = (TimeZoneInfo)AssociatedObject.SelectedItem;
                ViewModelLocator.Instance.NonClientAreaVM.SelectedTimezone = temp_selectedtz;
                TimezoneInfo temp_tz = new TimezoneInfo
                {
                    Id = temp_selectedtz.Id,
                    BaseUtcOffset = temp_selectedtz.BaseUtcOffset,
                    DaylightName = temp_selectedtz.DaylightName,
                    DisplayName = temp_selectedtz.DisplayName,
                    StandardName = temp_selectedtz.StandardName,
                    SupportsDaylightSavingTime = temp_selectedtz.SupportsDaylightSavingTime
                };
                logService.Serialize(ViewModelLocator.Instance.NonClientAreaVM.TimezonePath, temp_tz);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
            AssociatedObject.MouseDoubleClick -= AssociatedObject_MouseDoubleClick;
            AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
        }

    }
}
