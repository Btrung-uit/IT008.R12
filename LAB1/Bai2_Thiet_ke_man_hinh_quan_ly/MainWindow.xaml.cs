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
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
namespace QuanLySanPham
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ObservableCollection<SanPham> dsSanPham = new ObservableCollection<SanPham>();
		public MainWindow()
		{
			Title="Quản lý sản phẩm";
			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			InitializeComponent();
			listSanPham.ItemsSource = dsSanPham;
			tbMaSP.Focus();
		}

		public class SanPham
		{
			public string? maSP { set; get; }
			public string? tenSP { set; get; }
			public int soLuong { set; get; }
			public long donGia { set; get; }
			public long tongTien => (long)soLuong * donGia;
		}


		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string maSP = tbMaSP.Text.Trim();
				string tenSP = tbTenSP.Text.Trim();
				string strSoLuong = tbSoLuong.Text.Trim();
				string strDonGia = tbDonGia.Text.Trim();
				if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(strSoLuong) || string.IsNullOrEmpty(strDonGia))
				{
					MessageBox.Show("Không được để trống! Vui lòng nhập đầy đủ!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				if (!(maSP.Length == 4))
				{
					MessageBox.Show("Mã sản phẩm phải có độ dài bằng 4! Vui lòng nhập lại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbMaSP.Focus();
					return;
				}
				int soLuong;
				long donGia;
				if (!int.TryParse(strSoLuong, out soLuong) || soLuong < 0)
				{
					MessageBox.Show("Số lượng sản phẩm không hợp lệ! Vui lòng nhập lại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbSoLuong.Focus();
					return;
				}
				if (!long.TryParse(strDonGia, out donGia) || donGia < 0)
				{
					MessageBox.Show("Đơn giá sản phẩm không hợp lệ! Vui lòng nhập lại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbDonGia.Focus();
					return;
				}
				SanPham? spHienTai = dsSanPham.FirstOrDefault(x => x.maSP == maSP);

				if (spHienTai == null)
				{
					SanPham spMoi = new SanPham()
					{
						maSP = maSP,
						tenSP = tenSP,
						soLuong = soLuong,
						donGia = donGia
					};
					dsSanPham.Add(spMoi);
					tbMaSP.Clear();
					tbTenSP.Clear();
					tbSoLuong.Clear();
					tbDonGia.Clear();
					tbMaSP.Focus();
					tbTimKiem.Clear();
					listSanPham.ItemsSource = dsSanPham;
				}
				else
				{
					MessageBox.Show("Mã sản phẩm đã tồn tại! Vui lòng nhập mã khác!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbMaSP.Focus();
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi! Vui lòng kiểm tra lại ô nhập liệu!\n" + ex.Message);
			}
		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			SanPham? spCanXoa = listSanPham.SelectedItem as SanPham;
			if (spCanXoa == null)
			{
				MessageBox.Show("Vui lòng chọn một dòng trên bảng để xóa!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			MessageBoxResult result = MessageBox.Show($"Bạn có muốn xóa sản phẩm có mã {spCanXoa.maSP} có tên \"{spCanXoa.tenSP}\" không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				dsSanPham.Remove(spCanXoa);
				listSanPham.Items.Refresh();
				tbMaSP.IsReadOnly = false;
				tbMaSP.Clear();
				tbTenSP.Clear();
				tbSoLuong.Clear();
				tbDonGia.Clear();
				tbTimKiem.Clear();
				listSanPham.ItemsSource = dsSanPham;
				MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

			}
		}

		private void listSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SanPham? sp = listSanPham.SelectedItem as SanPham;
			if (sp != null)
			{
				tbMaSP.Text = sp.maSP;
				tbTenSP.Text = sp.tenSP;
				tbSoLuong.Text = sp.soLuong.ToString();
				tbDonGia.Text = sp.donGia.ToString();

				tbMaSP.IsReadOnly = true;
			}
		}
		private void btnSua_Click(object sender, RoutedEventArgs e)
		{
			SanPham? spCanSua = listSanPham.SelectedItem as SanPham;
			if (spCanSua == null)
			{
				MessageBox.Show("Vui lòng chọn một dòng trên bảng để sửa!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (spCanSua.tenSP == tbTenSP.Text.Trim() && spCanSua.soLuong.ToString() == tbSoLuong.Text.Trim() && spCanSua.donGia.ToString() == tbDonGia.Text.Trim())
			{
				MessageBox.Show("Dữ liệu sản phẩm không có thay đổi để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			try
			{
				string tenSP = tbTenSP.Text.Trim();
				string maSP = tbMaSP.Text.Trim();
				string strSoLuong = tbSoLuong.Text.Trim();
				string strDonGia = tbDonGia.Text.Trim();
				if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(strSoLuong) || string.IsNullOrEmpty(strDonGia))
				{
					MessageBox.Show("Không được để trống! Vui lòng nhập đầy đủ!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				if (!(maSP.Length == 4))
				{
					MessageBox.Show("Mã sản phẩm phải có độ dài bằng 4! Vui lòng nhập lại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbMaSP.Focus();
					return;
				}
				int soLuong;
				long donGia;
				if (!int.TryParse(strSoLuong, out soLuong) || soLuong < 0)
				{
					MessageBox.Show("Số lượng sản phẩm không hợp lệ! Vui lòng nhập lại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbSoLuong.Focus();
					return;
				}
				if (!long.TryParse(strDonGia, out donGia) || donGia < 0)
				{
					MessageBox.Show("Đơn giá sản phẩm không hợp lệ! Vui lòng nhập lại!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					tbDonGia.Focus();
					return;
				}

				spCanSua.tenSP = tenSP;
				spCanSua.maSP = maSP;
				spCanSua.soLuong = soLuong;
				spCanSua.donGia = donGia;

				listSanPham.Items.Refresh();

				MessageBox.Show("Thay đổi thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

				tbMaSP.IsReadOnly = false;
				tbMaSP.Clear();
				tbTenSP.Clear();
				tbSoLuong.Clear();
				tbDonGia.Clear();
				listSanPham.SelectedItem = null;
				tbMaSP.Focus();
				tbTimKiem.Clear();
				listSanPham.ItemsSource = dsSanPham;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi sửa sản phẩm " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (sender == tbMaSP)
				{
					tbTenSP.Focus();
				}
				else if (sender == tbTenSP)
				{
					tbSoLuong.Focus();
				}
				else if (sender == tbSoLuong)
				{
					tbDonGia.Focus();
				}
				else if (sender == tbDonGia)
				{
					btnThem_Click(sender, e);
				}
				else if(sender == tbTimKiem)
				{
					btnTimKiem_Click(sender, e);
				}
			}
		}

		private void btnTimKiem_Click(object sender, RoutedEventArgs e)
		{
			string tuKhoa = tbTimKiem.Text.Trim();
			if (string.IsNullOrEmpty(tuKhoa))
			{
				listSanPham.ItemsSource = dsSanPham;
				if(sender is Button)
				{ 
					MessageBox.Show("Ô tìm kiếm đang trống! Vui lòng nhập thông tin để tìm kiếm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				return;
			}
			var KetQua = dsSanPham.Where(sp =>
				(!string.IsNullOrEmpty(sp.maSP) && sp.maSP.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
				(!string.IsNullOrEmpty(sp.tenSP) && sp.tenSP.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))).ToList();

			listSanPham.ItemsSource = KetQua;
		}
		private void tbTimKiem_TextChanged(object sender, TextChangedEventArgs e)
		{
			btnTimKiem_Click(sender, e);
		}
	}
}