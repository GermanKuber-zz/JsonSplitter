using JsonSplitter.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonSplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDrop();
            ChangeCutButtomState();
        }
        private void InitializeDrop()
        {
            for (int i = 0; i < 500; i++)
            {
                drpCount.Items.Add(i);
            }
            drpCount.SelectedItem = 0;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Gat.Controls.OpenDialogView openDialog = new Gat.Controls.OpenDialogView();
            Gat.Controls.OpenDialogViewModel vm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;
            vm.IsDirectoryChooser = false;
            vm.StartupLocation = System.Windows.WindowStartupLocation.CenterScreen; ;
            vm.AddFileFilterExtension(".json");
            vm.Show();
            txtFileSource.Text = vm.SelectedFilePath;
            ChangeCutButtomState();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Gat.Controls.OpenDialogView openDialog = new Gat.Controls.OpenDialogView();
            Gat.Controls.OpenDialogViewModel vm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;
            vm.IsDirectoryChooser = true;
            vm.StartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            vm.Show();
            txtFolderDestination.Text = vm.SelectedFilePath;
            ChangeCutButtomState();
        }
        private void ChangeCutButtomState() {
            if (!string.IsNullOrWhiteSpace(txtFileSource.Text) && !string.IsNullOrWhiteSpace(txtFolderDestination.Text))
            {
                btnCut.IsEnabled = true;
            }
            else {
                btnCut.IsEnabled = false;
            }
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            var sourceFile = txtFileSource.Text;
            var destinationFolder = txtFileSource.Text;
            var maxFile = int.Parse( drpCount.SelectedItem.ToString());
            var oneObjectInFile = chkOneObject.IsChecked.Value ;
            var countGeneratedItem = 0;
            var countProcessItem = 0;
            var splitter = new Splitter(sourceFile, destinationFolder, oneObjectInFile, maxFile, out countGeneratedItem, out countProcessItem);
        }
    }
}
