using Logistic.Enums;
using Logistic.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Logistic.WpfClient
{
    public partial class MainWindow : Window
    {
        private AppBuilder app;
        private List<Vehicle> vehicleList;
        private int currentVehicleId;

        public MainWindow()
        {
            InitializeComponent();
            app = AppBuilder.GetInstance();
        }

        private bool CheckVehicleDataInput()
        {
            if (!numberOfVehicle.ValidateTextBox(FildType.IsString, 5, 10)) return false;
            if (!maxWeight.ValidateTextBox(FildType.Isint, 100, 50000)) return false;
            if (!maxVolume.ValidateTextBox(FildType.IsDouble, 1, 50)) return false;
            return true;
        }

        private void UpdateListView()
        {
            vehicleList = app.vehicleService.GetAll();
            lvVehicleList.ItemsSource = vehicleList;
            this.DeleteAndCargoButtonsIsEnable(false);
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckVehicleDataInput()) return;
            app.vehicleService.Create(new Vehicle
            {
                Type = (VehicleType)typeOfVehicle.SelectedIndex,
                Number = numberOfVehicle.Text.Trim(),
                MaxCargoWeightKg = int.Parse(maxWeight.Text.Trim()),
                MaxCargoVolume = double.Parse(maxVolume.Text.Trim())
            });
            UpdateListView();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            app.vehicleService.DeleteById(currentVehicleId);
            UpdateListView();
        }

        private void cargoBtn_Click(object sender, RoutedEventArgs e)
        {
            CargoWindow cargoWindow = new CargoWindow(currentVehicleId);
            cargoWindow.ShowDialog();
            UpdateListView();
        }

        private void lvVehicleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = e.AddedItems[0] as Vehicle;
                currentVehicleId = selectedItem.Id;
                typeOfVehicle.SelectedIndex = (int)selectedItem.Type;
                numberOfVehicle.Text = selectedItem.Number;
                maxWeight.Text = selectedItem.MaxCargoWeightKg.ToString();
                maxVolume.Text = selectedItem.MaxCargoVolume.ToString();
                this.DeleteAndCargoButtonsIsEnable(true);
            }
        }

        private void openFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                pathToFoder.Text = dialog.FileName;
                pathToFoder.IsEnabled = true;
            }
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            string pathToFile;
            if (Directory.Exists(pathToFoder.Text))
            {
                try
                {
                    pathToFile = typeOfReport.SelectedIndex switch
                    {
                        0 => app.vehicleReportService.CreateReport(vehicleList, ReportType.json, pathToFoder.Text),
                        1 => app.vehicleReportService.CreateReport(vehicleList, ReportType.xml, pathToFoder.Text),
                        _ => throw new NotImplementedException(),
                    };
                    MessageBox.Show($"Report files created\n{pathToFile}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    showExportFile.Text = File.ReadAllText(pathToFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("Folder does not selected or not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void openFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "report files (*.json, *.xml)|*.json;*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                pathToFile.Text = openFileDialog.FileName;
                pathToFile.IsEnabled = true;
                showImportFile.Text = File.ReadAllText(pathToFile.Text);
            }
        }

        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(pathToFile.Text))
            {
                try
                {
                    vehicleList = app.vehicleReportService.LoadReport(pathToFile.Text);
                    app.vehicleService.memoryRepositoryVehicle.DeleteAll();
                    vehicleList.ForEach(x => app.vehicleService.memoryRepositoryVehicle.Create(x));
                    MessageBox.Show($"Data successfully received from:\n{pathToFile.Text}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateListView();
                    tabControl.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("File does not selected or not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}