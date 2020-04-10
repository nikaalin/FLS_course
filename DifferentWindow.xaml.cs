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
using System.Windows.Shapes;
using Lab2.DataSource;
using Lab2.Entities;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для DifferentWindow.xaml
    /// </summary>
    public partial class DifferentWindow : Window
    {
        public DifferentWindow(IDSManager<Danger> dsManager)
        {
            InitializeComponent();

            var newList = dsManager.GetSourceAsList();
            var oldList = dsManager.GetOldSourceAsList();
            oldList.Remove(oldList.Last());
            oldList.Remove(oldList.Last());

            var last = oldList.Last();
            oldList.Remove(last);
            last.IsIntegrityViolation = !last.IsIntegrityViolation;
            oldList.Add(last);

            var listComparator = new DangerListComparator(oldList,newList);
            var changedItems = listComparator.getChangedComponents();
            foreach (var danger in changedItems)
            {
                var item = new TreeViewItem();
                item.Header = danger.ShortInfo.ToString();
                ChangedItems.Items.Add(item);
                var changeList = listComparator.getChangeList(oldList[danger.Id - 1], danger);
                foreach (var ch in changeList)
                {
                    item.Items.Add($"{ch.AttributeName}: {ch.PreviousValue} --> {ch.Value}");
                }
            }

            var newItems = listComparator.getNewComponents();
            var newShortItems = new List<ShortInfo>();
            foreach (var danger in newItems)
            {
                NewItems.Items.Add(danger.ShortInfo.ToString());
            }


            
        }


        private void CloseSecondWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
