using FPOMonitoring.Data.Class;
using FPOMonitoring.ViewModel;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FPOMonitoring.View.Window
{
    /// <summary>
    /// Interaction logic for BackupContent.xaml
    /// </summary>
    public partial class BackupContent
    {
        public BackupContent()
        {
            InitializeComponent();
        }

        private void Datagrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid1.SelectedItems.Count > 0)
            {
                var items = Datagrid1.SelectedItems.OfType<Production>().ToList();
                if (items != null)
                {
                    items.ToList().ToArray().ForEach(item =>
                    {
                        if (!ViewModelLocator.Instance.BackupVM.Datas.ToList().ToArray().Any(cnt => cnt.prod_id == item.id))
                        {
                            var documents = ViewModelLocator.Instance.BackupVM.FetchDocuments(item.id);
                            if (documents != null && documents is List<Document> docs)
                            {
                                docs.ToList().ToArray().ForEach(doc =>
                                {
                                    ViewModelLocator.Instance.BackupVM.Datas.Add(doc);
                                });
                            }   
                        }
                    }); 
                }

                //Remove in Count List
                ViewModelLocator.Instance.BackupVM.Datas?.ToList().ToArray().ForEach(item =>
                {
                    if (!items.ToList().ToArray().Any(cnt => cnt.id == item.prod_id))
                    {
                        ViewModelLocator.Instance.BackupVM.Datas.Remove(item);
                    }
                });
            } else ViewModelLocator.Instance.BackupVM.Datas.Clear();

            ViewModelLocator.Instance.BackupVM.SelectedItems.Clear();
            ViewModelLocator.Instance.BackupVM.SelectedItems.AddRange(Datagrid1.SelectedItems.OfType<Production>().ToList());

        }
    }
}
