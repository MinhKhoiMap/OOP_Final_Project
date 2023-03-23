
using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace DeAn
{

    class program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

           
            

           Console.Write("hãy nhập tên khách sạn : ");
            string Tenks = Console.ReadLine();
            Console.Write("Hãy nhập địa chỉ khách sạn : ");
            string Diachi = Console.ReadLine();
            Console.Write("Hãy nhập số điện thoại của khách sạn : ");
            string KsSDt = Console.ReadLine();
            KhachSan ks = new KhachSan(Tenks, Diachi,KsSDt);
            Console.Clear();
            int maPhongKhachsan=1;
            int maPhieuDatPhong= 1;
            int n;
            do
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Write("                                       ");
                int POS1 = Console.GetCursorPosition().Left;
                Console.Write("ĐỒ Án : Quản lý khách sạn ");
                int POS2 = Console.GetCursorPosition().Left;
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(POS1 - (POS2 - POS1) / 2,
               Console.GetCursorPosition().Top);
       
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(POS1, Console.GetCursorPosition().Top);
                Console.WriteLine("********************");
                Console.WriteLine("1.Xem list người dùng ");
                Console.WriteLine("2.Xem list hóa đơn ");
                Console.WriteLine("3.Đặt phòng ");
                Console.WriteLine("4.Check in nhận phòng ");
                Console.WriteLine("5.Thêm tài khoản người dùng ");
                Console.Write(">Vui lòng chọn yêu cầu của bạn ? ");
                n = int.Parse(Console.ReadLine());
                if (n == 1) { Console.WriteLine("List người dùng : "); }
                else if (n == 2)
                {
                    Console.WriteLine("List hóa đơn : ");
                }
                switch (n)
                {
                    case 1:
                        foreach (var i in KhachSan.listnguoidung)

                        {
                            i.Display();
                            Console.WriteLine("====================================================");
                        }

                        break;
                    case 2:
                        foreach (var i in KhachSan.HoaDonList)

                        {
                            i.InThongTin();
                            Console.WriteLine("====================================================");
                        }
                        break;
                    case 3:
                  
                        int count = 0;
                        Console.Write("Bạn muốn đặt phòng với tài khoản có số CCCD là  : ");
                        string CCCDtemp = Console.ReadLine();
                        User temp = null;
                        
                        foreach(var i in KhachSan.listnguoidung)
                        {
                            if (i.GetCCCD() == CCCDtemp)
                            {    
                                count++;
                                temp = i;
                                break;
                            }

                        }
                        if (count == 0)
                        {
                            Console.WriteLine("Không có tài khoản nào có số CCCD trùng với dãy số bạn vừa nhập !");
                            break;
                        }
                        InterfacePhong phong = new Phong(maPhongKhachsan);
                        maPhongKhachsan++;
                        Console.Write("Bạn muốn đặt loại phòng nào : 1. Phòng 1 người     2. Phòng 2 người      3. Phòng 3 người");
                        int LoaiPhong = int.Parse(Console.ReadLine());
                        if (LoaiPhong == 1)
                        {
                            phong = new Phong1Nguoi(phong);
                        }else if (LoaiPhong == 2)
                        {
                            phong = new Phong2Nguoi(phong);
                        }
                        else
                        {
                            phong = new Phong3Nguoi(phong);
                        }


                        Console.WriteLine("Thông tin phòng bạn vừa đặt  : " );
                        Console.WriteLine("Số  của phòng bạn vừa đặt : " + phong.GetMaPhong());
                        Console.WriteLine("Loại phòng bạn vừa đặt : " + phong.GetLoaiPhong());
                        if (temp.GetLoaiKH() == "1") { 
                            Console.WriteLine("Do bạn là khách loại 1 nên sẽ không được giảm giá");
                            Console.WriteLine("Giá tiền của phòng bạn vừa đặt : " + phong.GetGia());
                        }else if (temp.GetLoaiKH() == "2")
                        {
                            Console.WriteLine("Do bạn là khách loại 2 nên sẽ  được giảm giá 10 %");
                            Console.WriteLine("Giá tiền của phòng bạn vừa đặt : " + (phong.GetGia() - (0.1 * phong.GetGia())));
                        }
                        else
                        {
                            Console.WriteLine("Do bạn là khách loại 3 nên sẽ  được giảm giá 20 %");
                            Console.WriteLine("Giá tiền của phòng bạn vừa đặt : " + (phong.GetGia() - (0.2 * phong.GetGia())));

                        }
                        Console.Write("Bạn có muốn thanh toán không : 1. Có      2. Không      ");
                        int QuyetDinhThanhToan = int.Parse(Console.ReadLine());
                        if (QuyetDinhThanhToan == 1)
                        {
                            DateTime now = DateTime.Now;
                            DateTime now1 = new DateTime(now.Year,now.Month,now.Day+1);
                            if (temp.GetLoaiKH() == "1")
                            {
                                PhieuDatPhong phieuDat = new PhieuDatPhong(maPhieuDatPhong, temp, now, now1, phong, phong.GetGia());
                                maPhieuDatPhong++;
                                KhachSan.SoHoaDon++;
                            }else if (temp.GetLoaiKH() == "2")
                            {
                                PhieuDatPhong phieuDat = new PhieuDatPhong(maPhieuDatPhong, temp, now, now1, phong, phong.GetGia()-(0.1*phong.GetGia()));
                                maPhieuDatPhong++;
                                KhachSan.SoHoaDon++;
                            }
                            else
                            {
                                PhieuDatPhong phieuDat = new PhieuDatPhong(maPhieuDatPhong, temp, now, now1, phong, phong.GetGia() - (0.2 * phong.GetGia()));
                                maPhieuDatPhong++;
                                KhachSan.SoHoaDon++;

                            }
                        }
                        else
                        {
                            break;
                        }




                        break;
                    case 4:
                        int xoa = 0;
                        Console.Write("Hãy đọc số CCCD của bạn : ");
                        string CCCDCHeck = Console.ReadLine();
                        Console.Write("Hãy đọc số phòng bạn đã đặt : ");
                        int PhongCHeck = int.Parse(Console.ReadLine());
                        int countcheck = 0;
                        foreach (var i in KhachSan.list)
                        {
                            if( i.Sophong== PhongCHeck)
                            {
                                countcheck++;
                                Console.WriteLine(i.Authenticate(CCCDCHeck));
                                Console.WriteLine(i.GetPhong());
                                if (i.Authenticate(CCCDCHeck) == "Số căn cước công dân đúng ") { KhachSan.list.RemoveAt(xoa); }
                                break;

                            }
                            xoa++;
                        }
                        if(countcheck == 0) { Console.WriteLine("Phòng chưa có người đặt"); }
                       
                        break;
                    case 5:
                        Console.Write("Nhập họ tên người dùng : ");
                        string FUllName = Console.ReadLine();
                        Console.Write("Số điện thoại : ");
                        string SDt = Console.ReadLine();
                        Console.Write("Tuổi : ");
                        int Tuoi = int.Parse((string)Console.ReadLine());
                        Console.Write("Email : ");
                        string Email = Console.ReadLine();
                        Console.Write("Căn cước công dân : ");
                        string CCCD = Console.ReadLine();
                        Console.Write("Loại khách hàng : ");
                        string LoaiKh = Console.ReadLine();

                        User user = new User(FUllName, SDt, Tuoi, Email, CCCD, LoaiKh);
                        KhachSan.listnguoidung.Add(user);
                        break;
                    default:
                        Console.WriteLine("Không tìm thấy dữ liệu");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (n <= 5 && n >= 1);

        }

    }

    public class TraoThe
    {
        public interface IThePhong
        {
            string GetPhong();
        }




        private class ThePhong
        {
          
            public ThePhong()
            {
               
            }

            public string GetPhong()
            {
                return "Xác nhận có thể cấp thẻ phòng cho khách";
            }
        }
        public class ProtectionProxy : IThePhong
        {

            // An authentication proxy first asks for a password
            ThePhong subject;
            protected string password = "";
           public int Sophong { get; }
            
            
            public ProtectionProxy(int sophong, string cccd)
            {
                this.password = cccd;
                this.Sophong = sophong;
            }
            public string Authenticate(string supplied)
            {
                if (supplied != password)
                    return "Số căn cước công dân không đúng";
                else
                    subject = new ThePhong();
                return "Số căn cước công dân đúng ";
            }

            public string GetPhong()
            {
                if (subject == null)
                    return "Hãy khai báo số căn cước công dân đúng để được cấp quyền truy cập phòng ";
                else return subject.GetPhong();
            }
        }
    }





    public class PhieuDatPhong
    {
        InterfacePhong phong = null;
        protected HoaDon HoaDon;
        protected int MaPhieu;
        protected User KhachHang;
        protected DateTime NgayDen;
        protected DateTime NgayDi;
        protected int SoPhong;
        protected double ThanhTien;
        public PhieuDatPhong(int ma, User ten, DateTime den, DateTime di,InterfacePhong phong, double tien)
        {
            // Khởi tạo một đối tượng HoaDon tương ứng khi tạo mới đối tượng PhieuDatPhong
            this.MaPhieu = ma;
            this.KhachHang = ten;
            this.NgayDen = den;
            this.NgayDi = di;
            this.SoPhong = phong.GetMaPhong();
            this.ThanhTien = tien;
            HoaDon = new HoaDon(MaPhieu, ThanhTien,KhachHang.GetCCCD());
            KhachSan.HoaDonList.Add(HoaDon);
            TraoThe.ProtectionProxy the = new TraoThe.ProtectionProxy(SoPhong, KhachHang.GetCCCD());
            KhachSan.list.Add(the);
        }
        public int soPhong()
        {
            return SoPhong;
        }
        public void InThongTin()
        {
            Console.WriteLine("Mã phiếu: {0}", MaPhieu);
            Console.WriteLine("Tên khách hàng: {0}", KhachHang);
            Console.WriteLine("Ngày đến: {0}", NgayDen.ToShortDateString());
            Console.WriteLine("Ngày đi: {0}", NgayDi.ToShortDateString());
            Console.WriteLine("Số phòng: {0}", SoPhong);
            Console.WriteLine("Thành tiền: {0}", ThanhTien);
        }
    }
    public class HoaDon
    {
        protected int MaHoaDon;
        protected int MaPhieu;
        protected double TongTien;
        protected string cccd;
        public HoaDon(int maphieu, double TongTien,string cccd)
        {
            this.MaHoaDon = KhachSan.SoHoaDon;
            this.cccd = cccd;
            this.MaPhieu = maphieu;
            this.TongTien = TongTien;
        }
        public void InThongTin()
        {
            Console.WriteLine("Mã hóa đơn: {0}", MaHoaDon);
            Console.WriteLine("Mã phiếu: {0}", MaPhieu);
            Console.WriteLine("SỐ căn cước công dân của người đật: {0}", cccd);
            Console.WriteLine("Tổng tiền: {0}", TongTien);
        }
    }
    public class KhachSan
    {
        public static List<User> listnguoidung = new List<User>();
        public static List<HoaDon> HoaDonList = new List<HoaDon>();
        public static int SoHoaDon = 1;
        public static List<TraoThe.ProtectionProxy> list = new List<TraoThe.ProtectionProxy>();
        protected string TenKs;
        protected string DiaChi;

        protected string SoDienThoai;
        public KhachSan(string tenKs, string diaChi, string soDienThoai)
        {
            TenKs = tenKs;
            DiaChi = diaChi;

            SoDienThoai = soDienThoai;
        }

        public string GetTenKs()
        {
            return TenKs;
        }

        public void SetTenKs(string tenKs)
        {
            TenKs = tenKs;
        }

        public string GetDiaChi()
        {
            return DiaChi;
        }

        public void SetDiaChi(string diaChi)
        {
            DiaChi = diaChi;
        }



        public string GetSoDienThoai()
        {
            return SoDienThoai;
        }

        public void SetSoDienThoai(string soDienThoai)
        {
            SoDienThoai = soDienThoai;
        }
    }
    public class User
    {
        protected string FullName;
        protected string SoDienThoai;
        protected int Tuoi;
        protected string Email;
        protected string CCCD;
        protected string LoaiKH;
        public User(string fullName, string soDienThoai, int tuoi, string email, string cccd, string loaiKH)
        {
            FullName = fullName;
            SoDienThoai = soDienThoai;
            Tuoi = tuoi;
            Email = email;
            CCCD = cccd;
            LoaiKH = loaiKH;
           }

        public string GetFullName()
        {
            return FullName;
        }

        public void SetFullName(string fullName)
        {
            FullName = fullName;
        }

        public string GetSoDienThoai()
        {
            return SoDienThoai;
        }

        public void SetSoDienThoai(string soDienThoai)
        {
            SoDienThoai = soDienThoai;
        }

        public int GetTuoi()
        {
            return Tuoi;
        }

        public void SetTuoi(int tuoi)
        {
            Tuoi = tuoi;
        }

        public string GetEmail()
        {
            return Email;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public string GetCCCD()
        {
            return CCCD;
        }

        public void SetCCCD(string cccd)
        {
            CCCD = cccd;
        }

        public string GetLoaiKH()
        {
            return LoaiKH;
        }

        public void SetLoaiKH(string loaiKH)
        {
            LoaiKH = loaiKH;
        }
        public void Display()
        {
            Console.WriteLine($"Họ tên : {FullName}");
            Console.WriteLine($"Số điện thoại : {SoDienThoai}");
            Console.WriteLine($"Tuổi: {Tuoi}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"CCCD : {CCCD}");
            Console.WriteLine($"Loại khách hàng : {LoaiKH}");
        }
    }

    public interface InterfacePhong
    {
        public virtual int GetMaPhong() { return -1; }
        public virtual double GetGia() { return -1; }
        public virtual int GetSoGiuong() { return -1; }
        public virtual string GetLoaiPhong() { return ""; }
    }

    public class Phong : InterfacePhong
    {

        public Phong(int ma)
        {
            this.MaPhong = ma;

        }
        protected int MaPhong;
        protected string LoaiPhong;
        protected   double Gia = 100;
        protected int SoGiuong;
        public virtual int GetMaPhong() { return MaPhong; }
        public virtual double GetGia() { return Gia; }
        public virtual int GetSoGiuong() { return -1; }
        public virtual string GetLoaiPhong() { return ""; }

    }

    public class BaseDec : InterfacePhong
    {
        InterfacePhong phong=null;

        public BaseDec(InterfacePhong phongg)
        {
            phong = phongg;
        }

        public virtual int GetMaPhong() { return phong.GetMaPhong(); }
        public virtual double GetGia() { return phong.GetGia(); }
        public virtual int GetSoGiuong() { return -1; }
        public virtual string GetLoaiPhong() { return ""; }


    }

    public class Phong1Nguoi : BaseDec
    {
        public Phong1Nguoi(InterfacePhong phong) : base(phong)
        { }
        public override int  GetMaPhong() { return base.GetMaPhong(); }
        public override double GetGia() { return base.GetGia() + 100; }
        public override int GetSoGiuong() { return 1; }
        public override string GetLoaiPhong() { return "Phòng 1 người"; }


    }
    public class Phong2Nguoi : BaseDec
    {
        public Phong2Nguoi(InterfacePhong phong) : base(phong)
        { }
        public override int GetMaPhong() { return base.GetMaPhong(); }
        public override double GetGia() { return base.GetGia() + 200; }
        public override int GetSoGiuong() { return 2; }
        public override string GetLoaiPhong() { return "Phòng 2 người"; }


    }
    public class Phong3Nguoi : BaseDec
    {
        public Phong3Nguoi(InterfacePhong phong) : base(phong)
        { }
        public override int GetMaPhong() { return base.GetMaPhong(); }
        public override double GetGia() { return base.GetGia() + 300; }
        public override int GetSoGiuong() { return 3; }
        public override string GetLoaiPhong() { return "Phòng 3 người"; }


    }





}