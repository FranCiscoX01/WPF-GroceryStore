using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
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

namespace GrocerieStore
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void clearAddTextBox()
        {
            this.AddNameTextBox.Text = "";
            this.AddDescriptTextBox.Text = "";
            this.AddPriceTextBox.Text = "";
            this.AddQuantityTextBox.Text = "";
        }

        private bool AreAllValidNumericChars(string str)
        {
            foreach (char c in str)
            {
                if (c != '.')
                {
                    if (!Char.IsNumber(c))
                        return false;
                }

            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GrocerieStore.CrudWPF_FrameDataSet crudWPF_FrameDataSet = ((GrocerieStore.CrudWPF_FrameDataSet)(this.FindResource("crudWPF_FrameDataSet")));
            // Cargar datos en la tabla grocerie_store. Puede modificar este código según sea necesario.
            GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter crudWPF_FrameDataSetgrocerie_storeTableAdapter = new GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter();
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.Fill(crudWPF_FrameDataSet.grocerie_store);
            System.Windows.Data.CollectionViewSource grocerie_storeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("grocerie_storeViewSource")));
            grocerie_storeViewSource.View.MoveCurrentToFirst();
        }

        private void Add_Producto(object sender, RoutedEventArgs e)
        {
            GrocerieStore.CrudWPF_FrameDataSet crudWPF_FrameDataSet = ((GrocerieStore.CrudWPF_FrameDataSet)(this.FindResource("crudWPF_FrameDataSet")));

            // Cargar datos en la tabla grocerie_store. Puede modificar este código según sea necesario.
            GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter crudWPF_FrameDataSetgrocerie_storeTableAdapter = new GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter();

            // Insert Data
            var _price = AddPriceTextBox.Text.Replace(".", ",");
            decimal add_price = Convert.ToDecimal(_price);
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.InsertProduct(AddNameTextBox.Text, AddDescriptTextBox.Text, add_price, Convert.ToInt32(AddQuantityTextBox.Text));

            // Fill DataGrid
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.Fill(crudWPF_FrameDataSet.grocerie_store);
            System.Windows.Data.CollectionViewSource grocerie_storeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("grocerie_storeViewSource")));
            grocerie_storeViewSource.View.MoveCurrentToFirst();

            // Clear TexBox
            clearAddTextBox();

            // Notification
            var duration = 5;
            SnackbarAdd.MessageQueue.Enqueue(
                "Product Added!",
                "OK",
                param => Trace.WriteLine("Actioned: " + param),
                null,
                false,
                true,
                TimeSpan.FromSeconds(duration));
        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]{0,2}$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void Update_Product(object sender, RoutedEventArgs e)
        {
            GrocerieStore.CrudWPF_FrameDataSet crudWPF_FrameDataSet = ((GrocerieStore.CrudWPF_FrameDataSet)(this.FindResource("crudWPF_FrameDataSet")));

            // Cargar datos en la tabla grocerie_store. Puede modificar este código según sea necesario.
            GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter crudWPF_FrameDataSetgrocerie_storeTableAdapter = new GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter();

            // Row selected id
            DataRowView row = (DataRowView)grocerie_storeDataGrid.SelectedItems[0];
            var id = Convert.ToInt32(row["id"]);

            // Update
            var _price = EditPriceTextBox.Text.Replace(".", ",");
            decimal edit_price = Convert.ToDecimal(_price);
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.UpdateProduct(EditNameTextBox.Text, EditDescriptTextBox.Text, edit_price, Convert.ToInt32(EditQuantityTextBox.Text), id, id);

            // Fill DataGrid
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.Fill(crudWPF_FrameDataSet.grocerie_store);
            System.Windows.Data.CollectionViewSource grocerie_storeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("grocerie_storeViewSource")));
            grocerie_storeViewSource.View.MoveCurrentToFirst();

            // Notification
            var duration = 5;
            SnackbarUpdate.MessageQueue.Enqueue(
                "Product Updated!",
                "OK",
                param => Trace.WriteLine("Actioned: " + param),
                null,
                false,
                true,
                TimeSpan.FromSeconds(duration));
        }

        private void Delete_Product(object sender, RoutedEventArgs e)
        {
            GrocerieStore.CrudWPF_FrameDataSet crudWPF_FrameDataSet = ((GrocerieStore.CrudWPF_FrameDataSet)(this.FindResource("crudWPF_FrameDataSet")));

            // Cargar datos en la tabla grocerie_store. Puede modificar este código según sea necesario.
            GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter crudWPF_FrameDataSetgrocerie_storeTableAdapter = new GrocerieStore.CrudWPF_FrameDataSetTableAdapters.grocerie_storeTableAdapter();

            // Row selected id
            DataRowView row = (DataRowView)grocerie_storeDataGrid.SelectedItems[0];
            var id = Convert.ToInt32(row["id"]);

            // Deletequery
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.DeleteProduct(id);

            // Fill DataGrid
            crudWPF_FrameDataSetgrocerie_storeTableAdapter.Fill(crudWPF_FrameDataSet.grocerie_store);
            System.Windows.Data.CollectionViewSource grocerie_storeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("grocerie_storeViewSource")));
            grocerie_storeViewSource.View.MoveCurrentToFirst();
        }

        private void EditPriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = !AreAllValidNumericChars(e.Text);
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]{0,2}$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
    }
}
