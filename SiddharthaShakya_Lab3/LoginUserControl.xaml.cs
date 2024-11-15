using System.Windows;
using System.Windows.Controls;

namespace SiddharthaShakya_Lab3
{
    public partial class LoginUserControl : UserControl
    {
        public static readonly DependencyProperty UsernameProperty =
        DependencyProperty.Register("Username", typeof(string), typeof(LoginUserControl), new PropertyMetadata(string.Empty));

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public LoginUserControl()
        {
            InitializeComponent();
        }

        // Method to retrieve the password input from PasswordBox
        public string GetPassword()
        {
            return PasswordBox.Password;
        }
    }
}
