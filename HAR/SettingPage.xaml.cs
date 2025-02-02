namespace HAR;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
        BindingContext = new SettingsViewModel();
    }

    public class SettingsViewModel : BindableObject
    {
        private readonly IAdapter _adapter;
        private readonly IBluetoothLE _bluetoothLE;

        public ObservableCollection<IDevice> DiscoveredDevices { get; set; }
        public IDevice SelectedDevice { get; set; }

        public ICommand ScanCommand { get; }
        public ICommand ConnectCommand { get; }

        private bool _enableNotifications;
        public bool EnableNotifications
        {
            get => _enableNotifications;
            set
            {
                _enableNotifications = value;
                OnPropertyChanged();
            }
        }

        private int _volumeLevel;
        public int VolumeLevel
        {
            get => _volumeLevel;
            set
            {
                _volumeLevel = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            _bluetoothLE = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;

            DiscoveredDevices = new ObservableCollection<IDevice>();

            // Commands
            ScanCommand = new Command(StartScanning);
            ConnectCommand = new Command(ConnectToDevice);
        }

        private async void StartScanning()
        {
            DiscoveredDevices.Clear();
            if (_bluetoothLE.State == BluetoothState.On)
            {
                _adapter.DeviceDiscovered += (s, a) =>
                {
                    if (!DiscoveredDevices.Contains(a.Device))
                        DiscoveredDevices.Add(a.Device);
                };

                await _adapter.StartScanningForDevicesAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Bluetooth is off. Please enable Bluetooth.", "OK");
            }
        }

        private async void ConnectToDevice()
        {
            if (SelectedDevice != null)
            {
                try
                {
                    await _adapter.ConnectToDeviceAsync(SelectedDevice);
                    await Application.Current.MainPage.DisplayAlert("Success", $"Connected to {SelectedDevice.Name}", "OK");
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to connect to device.", "OK");
                }
            }
        }
    }
}