using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Logistic.Models;

namespace Logistic.WpfClient
{
    public partial class CargoWindow : Window
    {
        private AppBuilder app;
        private int currentVehicleId;
        private Guid currentCargoId;

        public CargoWindow(int currentVehicleId)
        {
            InitializeComponent();
            app = AppBuilder.GetInstance();
            this.currentVehicleId = currentVehicleId;
            var vehicle = app.vehicleService.GetById(currentVehicleId);
            lvCargoList.ItemsSource = vehicle.Cargos;
        }

        private bool CheckCargoDataInput()
        {
            if (!weightOfCargo.ValidateTextBox(FildType.Isint, 1, 5000)) return false;
            if (!volumeOfCargo.ValidateTextBox(FildType.IsDouble, 0, 10)) return false;
            if (!codeOfCargo.ValidateTextBox(FildType.IsString, 5, 10)) return false;
            if (!recipientAddress.ValidateTextBox(FildType.IsString, 5, 30)) return false;
            if (!senderAddress.ValidateTextBox(FildType.IsString, 5, 30)) return false;
            if (!recipientPhone.ValidateTextBox(FildType.IsString, 13, 13)) return false;
            if (!senderPhone.ValidateTextBox(FildType.IsString, 13, 13)) return false;
            return true;
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckCargoDataInput()) return;
            var vehicle = app.vehicleService.GetById(currentVehicleId);
            vehicle.Cargos.Add(new Cargo
            {
                Id = Guid.NewGuid(),
                Weight = int.Parse(weightOfCargo.Text),
                Volume = double.Parse(volumeOfCargo.Text),
                Code = codeOfCargo.Text,
                Invoice = new Invoice
                {
                    Id = Guid.NewGuid(),
                    RecipientAddress = recipientAddress.Text.Trim(),
                    SenderAddress = senderAddress.Text.Trim(),
                    RecipientPhoneNumber = recipientPhone.Text.Trim(),
                    SenderPhoneNumber = senderPhone.Text.Trim()
                }
            });
            var isLoaded = app.vehicleService.LoadCargo(vehicle, currentVehicleId);
            if (isLoaded)
            {
                lvCargoList.ItemsSource = vehicle.Cargos;
                unloadBtn.IsEnabled = false;
            }
            else MessageBox.Show("Free weightKg or volume is not enough!",
                "Load cargo error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void unloadBtn_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = app.vehicleService.GetById(currentVehicleId);
            var cargo = vehicle.Cargos.FirstOrDefault(x => x.Id == currentCargoId);
            vehicle.Cargos.Remove(cargo);
            app.vehicleService.UnloadCargo(vehicle, currentVehicleId);
            lvCargoList.ItemsSource = vehicle.Cargos;
            unloadBtn.IsEnabled = false;
        }

        private void unloadAllBtn_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = app.vehicleService.GetById(currentVehicleId);
            vehicle.Cargos.Clear();
            app.vehicleService.UnloadAllCargos(vehicle, currentVehicleId);
            lvCargoList.ItemsSource = vehicle.Cargos;
        }

        private void lvCargoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = e.AddedItems[0] as Cargo;
                currentCargoId = selectedItem.Id;
                weightOfCargo.Text = selectedItem.Weight.ToString();
                volumeOfCargo.Text = selectedItem.Volume.ToString();
                codeOfCargo.Text = selectedItem.Code;
                recipientAddress.Text = selectedItem.Invoice.RecipientAddress;
                senderAddress.Text = selectedItem.Invoice.SenderAddress;
                recipientPhone.Text = selectedItem.Invoice.RecipientPhoneNumber;
                senderPhone.Text = selectedItem.Invoice.SenderPhoneNumber;
                unloadBtn.IsEnabled = true;
            }
        }
    }
}
