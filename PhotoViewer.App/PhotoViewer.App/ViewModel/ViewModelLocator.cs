/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PhotoViewer.App"
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
using PhotoViewer.App.Services;
using PhotoViewer.App.Services.Navigation;
using System;

namespace PhotoViewer.App.ViewModel
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
            if (ViewModelBase.IsInDesignModeStatic) {
                // Create design time view services and models
                SimpleIoc.Default.Register<IDataService, DataService>();
            } else {
                // Create run time view services and models
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PhotoViewModel>();
            SimpleIoc.Default.Register<AlbumViewModel>();
            SimpleIoc.Default.Register<MapsViewModel>();

        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public PhotoViewModel Photo => ServiceLocator.Current.GetInstance<PhotoViewModel>();
        public AlbumViewModel Album => ServiceLocator.Current.GetInstance<AlbumViewModel>();
        public MapsViewModel Maps => ServiceLocator.Current.GetInstance<MapsViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}