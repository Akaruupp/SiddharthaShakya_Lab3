using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SiddharthaShakya_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> _userCredentials = new Dictionary<string, string>();

        public ObservableCollection<OrderItem> OrderItems { get; set; } = new ObservableCollection<OrderItem>();

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the items in the ComboBoxes
            InitializeMenuItems();

            // Set the DataGrid ItemsSource to OrderItems collection
            BillDataGrid.ItemsSource = OrderItems;
        }
        private void ClearBillButton_Click(object sender, RoutedEventArgs e)
        {
            OrderItems.Clear();
            CalculateBill();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            OrderItem selectedOrderItem = comboBox.SelectedItem as OrderItem;

            if (selectedOrderItem != null)
            {
                // Check if the item is already in the OrderItems list and increase quantity if so
                var existingItem = OrderItems.FirstOrDefault(item => item.Name == selectedOrderItem.Name);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    OrderItems.Add(new OrderItem
                    {
                        Name = selectedOrderItem.Name,
                        Price = selectedOrderItem.Price,
                        Quantity = 1
                    });
                }
                CalculateBill();
            }
        }
        private void CalculateBill()
        {
            decimal subtotal = OrderItems.Sum(item => item.Price * item.Quantity);
            decimal tax = subtotal * 0.10m; // Assuming a 10% tax rate
            decimal total = subtotal + tax;

            SubtotalTextBlock.Text = subtotal.ToString("C");
            TaxTextBlock.Text = tax.ToString("C");
            TotalTextBlock.Text = total.ToString("C");
        }

        private void InitializeMenuItems()
        {
            // Populate Beverage ComboBox
            BeverageComboBox.ItemsSource = new List<OrderItem>
        {
            new OrderItem { Name = "Soda", Price = 1.50m },
            new OrderItem { Name = "Juice", Price = 2.50m },
            new OrderItem { Name = "Tea", Price = 1.75m },
            new OrderItem { Name = "Coffee", Price = 2.00m }
        };

            // Populate Appetizer ComboBox
            AppetizerComboBox.ItemsSource = new List<OrderItem>
        {
            new OrderItem { Name = "Fries", Price = 3.00m },
            new OrderItem { Name = "Nachos", Price = 4.00m },
            new OrderItem { Name = "Salad", Price = 3.50m }
        };

            // Populate Main Course ComboBox
            MainCourseComboBox.ItemsSource = new List<OrderItem>
        {
            new OrderItem { Name = "Burger", Price = 8.00m },
            new OrderItem { Name = "Pizza", Price = 10.00m },
            new OrderItem { Name = "Pasta", Price = 9.00m }
        };

            // Populate Dessert ComboBox
            DessertComboBox.ItemsSource = new List<OrderItem>
        {
            new OrderItem { Name = "Ice Cream", Price = 3.50m },
            new OrderItem { Name = "Cake", Price = 4.00m },
            new OrderItem { Name = "Pie", Price = 3.75m }
        };
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginControl.Username;
            string password = LoginControl.GetPassword();

            if (_userCredentials.ContainsKey(username))
            {
                // Username found, check password
                if (_userCredentials[username] == password)
                {
                    MessageBox.Show("Login successful!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginPanel.Visibility = Visibility.Collapsed;
                    OrderPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Password is incorrect. Try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Username not found
                MessageBox.Show("Username doesn't exist. Would you like to register?", "Login Failed", MessageBoxButton.YesNo, MessageBoxImage.Information);
            }
        }

        // Register button click handler
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginControl.Username;
            string password = LoginControl.GetPassword();

            // Ensure both username and password are provided
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if username already exists
            if (_userCredentials.ContainsKey(username))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Register new user by adding them to the dictionary
                _userCredentials[username] = password;
                MessageBox.Show("Registration successful! You can now log in.", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }

}