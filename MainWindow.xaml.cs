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
using Lab2.DAO;
using Lab2.DataSource;
using Lab2.Entities;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Danger> dangers;
        private List<ShortInfo> shortDangers;
        public MainWindow()
        {
            InitializeComponent();
            IDSManager<Danger> dsManager = DangerExcelManager.GetInstance();
            shortDangers = new List<ShortInfo>();
            if (!dsManager.ExistLocal())
            {
                showStartDialog();
                dsManager.Create();
            }

            dangers = dsManager.GetSourceAsList();
            foreach (var danger in dangers)
            {
                shortDangers.Add(danger.ShortInfo);
            }
            DangerDataGrid.ItemsSource = shortDangers;
        }

        void showStartDialog()
        {
            string messageBoxText = "На диске ничего нет, загрузить из интернета?";
            string caption = "А данных-то нет";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("ноо с чем мы будем работать? Короче, мне пофиг, загружаю.",
                        "Это, конечно, интересно...", MessageBoxButton.OK, MessageBoxImage.None);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

      

        private void ShowDangerDetailInfo(object sender, MouseButtonEventArgs e)
        {
            var selected = (ShortInfo) DangerDataGrid.SelectedItem;
            var danger = dangers[shortDangers.IndexOf(selected)];
            MessageBox.Show( danger.GetText(), $"{selected.ID} {selected.Name}", MessageBoxButton.OK, MessageBoxImage.None);
        }
    }
}
