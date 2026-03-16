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
            /*Need to do the following
             * - Check if username exists in database
             * - If it does, check if password matches
             * - If either of those checks fail, show error message
             * - If it does, open main window and close login/register window when button is clicked
             */

            if (string.IsNullOrEmpty(usernameInput.Text) || string.IsNullOrEmpty(passwordInput.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            /*Need to do the following
             * - Check if username already exists in database
             * - If it doesn't, create new user object and add to database
             * - If it does, show error message
             * - If successful, open main window and close login/register window when button is clicked
             */
            if (string.IsNullOrEmpty(usernameInput.Text) || string.IsNullOrEmpty(passwordInput.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }
        }



    }
}
