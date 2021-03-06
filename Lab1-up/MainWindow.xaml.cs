﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices.ComTypes;
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
        private ResolutionList resolutions;
        private IMoniker camera_moniker;
        private int res_index = 0;
        private string dir = "C:/Users/Aron/Desktop/FOLDER/";

        public MainWindow()
        {
            InitializeComponent();
        }

        //on loaded:
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //init
            _CameraChoice = new CameraChoice();

            _CameraChoice.UpdateDeviceList();

            //update combobox
            cameraCmbBox.ItemsSource = _CameraChoice.Devices;

        }

        // on window closed:
        private void Window_Closed(object sender, EventArgs e)
        {
            cameraControl.CameraControl.CloseCamera();
        }
        
        //on combo box camera selection:
        private void CmbBoxCameraChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_CameraChoice.Devices.Count > 0)
            {
                camera_moniker = _CameraChoice.Devices[cameraCmbBox.SelectedIndex].Mon;
                resolutions = Camera.GetResolutionList(camera_moniker);

                //update resolution combo box
                resolutionCmbBox.ItemsSource = resolutions;

                cameraControl.CameraControl.SetCamera(camera_moniker, resolutions[res_index]);
            }

        }

        //on combo box resolution selection:
        private void CmbBoxResolutionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_CameraChoice.Devices.Count > 0)
            {
                resolutions = Camera.GetResolutionList(camera_moniker);
                res_index = resolutionCmbBox.SelectedIndex;
                cameraControl.CameraControl.SetCamera(camera_moniker, resolutions[res_index]);
            }
        }

        //on screenshot button clicked
        private void OnScreenshotButtonClicked(object sender, RoutedEventArgs e)
        {
            Bitmap bitmap = cameraControl.CameraControl.SnapshotOutputImage();
            if (bitmap == null) return;
            bitmap.Save(dir + " screen.jpg", ImageFormat.Jpeg);
        }

        private void OnStartRecButtonClicked(object sender, RoutedEventArgs e)
        {
        }
        private void OnStopRecButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnOpenSettingsButtonClicked(object sender, RoutedEventArgs e)
        {
            Camera.DisplayPropertyPage_Device(camera_moniker, cameraControl.CameraControl.Handle);
        }

    }
}
