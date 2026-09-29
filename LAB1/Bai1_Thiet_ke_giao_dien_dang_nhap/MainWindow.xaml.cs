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

namespace DangNhap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MinHeight = 300;
			MinWidth = 400;
		}

		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			string username = tbTen.Text.Trim();
			string password = tbMatKhau.Text;

			if(username == "admin" && password == "123456")
			{
				MessageBox.Show("Đăng nhập thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBox.Show("Tài khoản hoặc mật khẩu chưa đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}