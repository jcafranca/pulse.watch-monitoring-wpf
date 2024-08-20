using FPOMonitoring.ViewModel;
using HandyControl.Controls;
using HandyControl.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Tools.Behaviors
{
    public class SearchBarBehavior : Behavior<SearchBar>
    {
        private string _searchKey;
        protected override void OnAttached()
        {
            AssociatedObject.SearchStarted += AssociatedObject_OnSearchStarted;
        }

        private void AssociatedObject_OnSearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            _searchKey = e.Info;
            FilterItems();
        }
        private void FilterItems()
        {
            if (string.IsNullOrEmpty(_searchKey))
            {
                ViewModelLocator.Instance.NonClientAreaVM.Timezone.ToList().ForEach(timezone =>
                {
                    if (!ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Contains(timezone))
                    {
                        ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Add(timezone);
                    }
                });
            }
            else
            {
                var key = _searchKey.ToLower();
                foreach (var item in ViewModelLocator.Instance.NonClientAreaVM.Timezone)
                {
                    if (item.DisplayName.ToLower().Contains(key))
                    {
                        if (!ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Contains(item))
                        {
                            ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Add(item);
                        }
                    }
                    else
                    {
                        var name = item.DisplayName;
                        if (!string.IsNullOrEmpty(name) && name.ToLower().Contains(key))
                        {
                            if (!ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Contains(item))
                            {
                                ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Add(item);
                            }
                        }
                        else
                        {
                            if (ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Contains(item))
                            {
                                ViewModelLocator.Instance.NonClientAreaVM.TimeZones.Remove(item);
                            }
                        }
                    }
                }
            }
        }
        protected override void OnDetaching()
        {
            AssociatedObject.SearchStarted -= AssociatedObject_OnSearchStarted;
        }
    }
}
