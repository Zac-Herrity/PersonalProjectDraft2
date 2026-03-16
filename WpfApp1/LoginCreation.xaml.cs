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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginCreation.xaml
    /// </summary>
    public partial class LoginCreation : Window
    {
        /*To do
         * - Create user class object from inputted username / password (Ensure it's not been taken)
         * - Add user to database
         * - Ensure login works too
         * - Both login and register if successful should open the main window and close the login/register window
         */
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

            // Ensure the username and password match an existing user in the database
            if (ValidateLogin(usernameInput.Text, passwordInput.Text))
            {
                // Open the main window and close the login/register window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(usernameInput.Text) || string.IsNullOrEmpty(passwordInput.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }
        }

        static bool ValidateLogin(string username, string password)
        {
            return true; //for now
        }

        private void usernameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameInput.Text = "";
        }

        private void usernameInput_LostFocus(object sender, RoutedEventArgs e)
        {
            usernameInput.Text = "Enter";
        }

        private void passwordInput_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordInput.Text = "";
        }

        private void passwordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordInput.Text = "Enter";
        }
    }
}
