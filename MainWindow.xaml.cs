using System;
using System.Collections.Generic;
using System.IO;
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
using Lab2.DAO;
using Lab2.DataSource;
using Lab2.Entities;
using Microsoft.Win32;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Danger> dangers;
        private List<ShortInfo> shortDangers;
        private IDSManager<Danger> dsManager;

        public MainWindow()
        {
            InitializeComponent();
            dsManager = DangerExcelManager.GetInstance();
            shortDangers = new List<ShortInfo>();
            if (!dsManager.ExistLocal())
            {
                if (showStartDialog())
                {
                    while (startAction())
                    {
                    }
                }
            }

            dangers = dsManager.GetSourceAsList();
            foreach (var danger in dangers)
            {
                shortDangers.Add(danger.ShortInfo);
            }

            DangerDataGrid.ItemsSource = shortDangers;
        }

        bool startAction()
        {
            try
            {
                dsManager.Create();
                return false;
            }
            catch (Exception e)
            {
                return showFailDialog();
            }
        }

        bool showStartDialog()
        {
            string messageBoxText = "На диске ничего нет, загрузить из интернета?";
            string caption = "А данных-то нет";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default: return false;
            }
        }


        private void ShowDangerDetailInfo(object sender, MouseButtonEventArgs e)
        {
            var selected = (ShortInfo) DangerDataGrid.SelectedItem;
            var danger = dangers[shortDangers.IndexOf(selected)];
            MessageBox.Show(danger.GetText(), $"{selected.ID} {selected.Name}", MessageBoxButton.OK,
                MessageBoxImage.None);
        }

        private void SafeFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.Copy(((ExcelManager<Danger>) dsManager).GetLocalFile(), saveFileDialog.FileName + ".xlsx");
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            while (Update()) { }
        }

        private bool Update()
        {
            if (dsManager.UpdateFromRemote())
            {
                dangers = dsManager.GetSourceAsList();
                foreach (var danger in dangers)
                {
                    shortDangers.Add(danger.ShortInfo);
                }
                DangerDataGrid.ItemsSource = shortDangers;
                DangerDataGrid.UpdateLayout();
                showSuccessDialog();
                return false;
            }
            else
            {
               return showFailDialog();
            }
        }

        private void showSuccessDialog()
        {
            var newList = dsManager.GetSourceAsList();
            var oldList = dsManager.GetOldSourceAsList();

            /*oldList.Remove(oldList.Last());
            oldList.Remove(oldList.Last());
            var last = oldList.Last();
            oldList.Remove(last);
            last.IsIntegrityViolation = !last.IsIntegrityViolation;
            oldList.Add(last);*/

            var listComparator = new DangerListComparator(oldList, newList);

            string messageBoxText = $"Обновлено {listComparator.GetUpdatedCount()} записей\nИнтересуют подробности?";
            string caption = "Успех!";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    DifferentWindow secondWindow = new DifferentWindow(dsManager);
                    secondWindow.Show();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private bool showFailDialog()
        {
            string messageBoxText = "Не сложилось, не фартануло...\nПопробовать еще раз?";
            string caption = "Провал!";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default: return false;
            }
        }
    }
}