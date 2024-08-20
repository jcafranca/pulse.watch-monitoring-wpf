/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:FPOMonitoring"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows;

namespace FPOMonitoring.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CommandViewModel>();
            SimpleIoc.Default.Register<ReportViewModel>();
            SimpleIoc.Default.Register<BackupViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<NonClientAreaViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<QueryViewModel>();


        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public CommandViewModel CommandVM => ServiceLocator.Current.GetInstance<CommandViewModel>();
        public ReportViewModel ReportVM => ServiceLocator.Current.GetInstance<ReportViewModel>();
        public BackupViewModel BackupVM => ServiceLocator.Current.GetInstance<BackupViewModel>();
        public LoginViewModel LoginVM => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public NonClientAreaViewModel NonClientAreaVM => ServiceLocator.Current.GetInstance<NonClientAreaViewModel>();
        public SettingViewModel SettingVM => ServiceLocator.Current.GetInstance<SettingViewModel>();
        public QueryViewModel QueryVM => ServiceLocator.Current.GetInstance<QueryViewModel>();
        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() => Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}