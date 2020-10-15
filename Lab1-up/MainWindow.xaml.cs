using System;
using System.Windows;
using System.Windows.Controls;
using Camera_NET;
using DirectShowLib;

namespace Lab1_up
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CameraChoice _CameraChoice;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _CameraChoice = new CameraChoice();

            _CameraChoice.UpdateDeviceList();

            //update combobox
            cmbBox.ItemsSource = _CameraChoice.Devices;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            cameraControl.CameraControl.CloseCamera();
        }

        private void CmbBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_CameraChoice.Devices.Count > 0)
            {
                var camera_moniker = _CameraChoice.Devices[cmbBox.SelectedIndex].Mon;
                cameraControl.CameraControl.SetCamera(camera_moniker, null);
            }
        }
    }
}
