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
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginCreation.xaml
    /// </summary>
    public partial class LoginCreation : Window
    {
        public LoginCreation()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        { 
            if (string.IsNullOrEmpty(usernameInput.Text) || string.IsNullOrEmpty(passwordInput.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }
            else
            {
                Database db = new Database();
                bool isValid = db.UserValidation(usernameInput.Text, passwordInput.Text);
                if (isValid)
                {
                    MessageBox.Show("Login successful!");
                    new User { Username = usernameInput.Text, Password = passwordInput.Text }; 
                    MainWindow mainWindow = new MainWindow(); 
                    mainWindow.Show();
                    this.Close(); //Close the login window
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.");
                }
            }
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(usernameInput.Text) || string.IsNullOrEmpty(passwordInput.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }
            else
            {
                Database db = new Database();
                bool isCreated = db.CreateUser(usernameInput.Text, passwordInput.Text);
                if (isCreated)
                {
                    MessageBox.Show("Account created successfully! You can now log in.");
                }
                else
                {
                    MessageBox.Show("Failed to create account. Please try again.");
                }
            }
        }
    }
}
