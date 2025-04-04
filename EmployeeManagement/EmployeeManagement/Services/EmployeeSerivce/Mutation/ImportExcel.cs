using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using OfficeOpenXml;
using System.Globalization;
using System.IO;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public partial class EmployeeService : IEmployeeService
    {
        public async Task<BaseResponse> ImportExcel(EmployeeImport request)
        {
            var response = new BaseResponse();

            try
            {

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var filePath = request.FilePath;

                var listPhongBan = _context.Departments.ToList();
                FileInfo fileInfo = new FileInfo(filePath);

                List<EmployeeImport> list = new List<EmployeeImport>();
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["DANHSACHNHANVIEN"];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        EmployeeImport newrow = new EmployeeImport();
                        var manv = worksheet.Cells[row, 1].Text;
                        if (!string.IsNullOrEmpty(manv))
                        {
                            newrow.MaNV = manv;
                        }
                        else
                        {
                            newrow.MaNV = null;
                            newrow.IsValid = true;
                            newrow.Message = "Mã nhân viên không được bỏ trống!";
                        }
                        var tennv = worksheet.Cells[row, 2].Text;
                        if (!string.IsNullOrEmpty(tennv))
                        {
                            newrow.TenNV = tennv;
                        }
                        else
                        {
                            newrow.TenNV = null;
                            newrow.IsValid = true;
                            newrow.Message = "Tên nhân viên không được bỏ trống!";
                        }
                        var gioitinh = worksheet.Cells[row, 3].Text;
                        if (!string.IsNullOrEmpty(gioitinh))
                        {
                            gioitinh = gioitinh.ToLower();
                            newrow.GioiTinh = gioitinh == "nam" ? true : false;
                        }
                        else
                        {
                            newrow.GioiTinh = null;
                        }
                        var ngaysinh = worksheet.Cells[row, 4].Text;
                        DateTime parsedDate;
                        if (!string.IsNullOrEmpty(ngaysinh) && DateTime.TryParseExact(ngaysinh, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            newrow.NgaySinh = parsedDate;
                        }
                        else
                        {
                            newrow.NgaySinh = null;
                            newrow.IsValid = false;
                            newrow.Message = "Ngày sinh không hợp lệ!";
                        }

                        var mabophan = worksheet.Cells[row, 5].Text;
                        if (!string.IsNullOrEmpty(mabophan))
                        {
                            newrow.MaBoPhan = mabophan;
                        }
                        else
                        {
                            newrow.MaBoPhan = null;
                            newrow.IsValid = true;
                            newrow.Message = "Mã bộ phận không được bỏ trống!";
                        }
                        var mucluong = worksheet.Cells[row, 6].Text;
                        if (!string.IsNullOrEmpty(mucluong))
                        {
                            newrow.MucLuong = double.Parse(mucluong);
                        }
                        else
                        {
                            newrow.MucLuong = null;
                        }
                        list.Add(newrow);
                    }
                }
                var listSuccess = list.Where(x => x.IsValid != true).ToList();
                var listFail = list.Where(x => x.IsValid == true).ToList();

                // sau khi validate thì em được danh sách list nhân viên hợp lệ, manv không rỗng hoặc null
                var duplicateManvs = listSuccess.GroupBy(x => x.MaNV)
                                                .Where(g => g.Count() > 1) // em thực hiện group manv lại nếu có bất kỳ manv nào lớn hơn 1 thì chắc chắn nó bị trùng
                                                .Select(g => g.Key) // sao đó em select lấy danh sách mã nhân viên Key lúc này mang giá trị là manv vì chúng ta group theo mã 
                                                .ToHashSet(); // mặc dù, ở bước trên có được danh sách mavn không bị trùng, hashset giúp cho cấu trúc dữ liệu được trùng

                var duplicateItems = listSuccess.Where(x => duplicateManvs.Contains(x.MaNV)).ToList(); // lọc danh sách thành viên lấy ra những phần từ bị trùng

                foreach (var item in duplicateItems) // đánh dấu những phần tử bị trùng, thêm nội dung thông báo 
                {
                    item.IsValid = true;
                    item.Message = "Mã nhân viên bị trùng";
                }
                listFail.AddRange(duplicateItems); // add những phần tử trùng vào list lỗi

                listSuccess.RemoveAll(x => duplicateManvs.Contains(x.MaNV)); // đồng thời cần phải xóa những phần tử trùng ra bên ngoài

                if (listSuccess.Count > 0)
                {
                    var listEmployee = _context.Employees.ToList(); // nếu dữ liệu thành công thì gọi 1 lần để lấy nhân viên(gọi 1 lần)
                    var listDepartment = _context.Departments.ToList(); // lấy danh sách phòng bàn để kiểm tra phòng có hợp lệ không (gọi 1 lần)

                    // chỗ này em khai bào một list để lưu giá trị phần tử được lưu
                    var employeesToAdd = new List<Employees>();

                    foreach (var item in listSuccess)
                    {
                        // với những phần tử có mã nhân viên không bị trùng trong danh sách nhưng bị trùng trong database, thì em cần kiểm tra
                        var check = listEmployee.FirstOrDefault(x => x.MaNV == item.MaNV);
                        // ở đây em có 2 hướn
                        // 1: nếu là null thì em thêm mới, nếu != null(tức là đã có trong database) thì em báo lỗi.
                        // 2: nếu là null thì em thêm mới, nếu != null thì em sẽ update lại trong database
                        if (check == null)
                        {
                            var checkMaPhong = listDepartment.FirstOrDefault(x => x.Name.ToLower() == item.MaBoPhan.ToLower() || (int.TryParse(item.MaBoPhan, out int maBoPhanId) && x.Id == maBoPhanId));
                            if (checkMaPhong != null)
                            {
                                var employee = new Employees
                                {
                                    MaNV = item.MaNV,
                                    TenNV = item.TenNV,
                                    GioiTinh = item.GioiTinh,
                                    NgaySinh = item.NgaySinh != null ? DateTime.SpecifyKind(item.NgaySinh.Value, DateTimeKind.Utc) : null,
                                    MaBoPhan = checkMaPhong.Id,
                                    MucLuong = item.MucLuong
                                };
                                employeesToAdd.Add(employee);
                            }
                            else
                            {
                                item.IsValid = true;
                                item.Message = "Phòng ban không hợp lệ";
                                listFail.Add(item);
                            }
                        }
                        else
                        {
                            item.IsValid = true;
                            item.Message = "Mã nhân viên bị trùng";
                            listFail.Add(item);
                        }
                    }
                    // nếu list lớn hơn 0 thì lưu, còn nếu không thì không có phần tử nào được lưu, không cần gọi đến database
                    if (employeesToAdd.Count > 0)
                    {
                        // nếu có thì gọi thêm lần nữa
                        _context.Employees.AddRange(employeesToAdd);
                        await _context.SaveChangesAsync();
                    }
                    // trong code trên e đã gọi về database ít nhất nhiều nhất 3 lần
                    response.Data = null;
                    response.Success = true;
                    response.Message = "Thêm thành công!";
                }

                if (listFail.Count > 0)
                {
                    var directoryPath = Path.Combine("Storage", "Excel");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string templateFilePath = Path.Combine(directoryPath, "Danhsachnhanvien.xlsx");

                    if (!File.Exists(templateFilePath))
                    {
                        response.Success = false;
                        response.Message = "Không thể tải template";
                        return response;
                    }

                    string newFileName = $"DanhSach_NhanVien_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    string newFilePath = Path.Combine(directoryPath, newFileName);
                    File.Copy(templateFilePath, newFilePath, true);
                    using (var package = new ExcelPackage(new FileInfo(newFilePath)))
                    {


                        // or LicenseContext.Commercial

                        var sheet = package.Workbook.Worksheets["DANHSACHNHANVIEN"];
                        if (sheet == null)
                        {
                            response.Success = false;
                            response.Message = "Không tìm thấy sheet";
                            return response;
                        }

                        int rowIndex = 3;
                        const int startColumn = 1;

                        foreach (var d in listFail)
                        {
                            int column = startColumn;
                            sheet.Cells[rowIndex, column++].Value = d.MaNV;
                            sheet.Cells[rowIndex, column++].Value = d.TenNV;
                            sheet.Cells[rowIndex, column++].Value = d.GioiTinh == true ? "Nam" : "Nữ";
                            sheet.Cells[rowIndex, column++].Value = d.NgaySinh;
                            sheet.Cells[rowIndex, column++].Value = d.MaBoPhan;
                            sheet.Cells[rowIndex, column++].Value = d.MucLuong;
                            sheet.Cells[rowIndex, column++].Value = d.Message;

                            rowIndex++;
                        }

                        package.Save();
                    }

                    response.Data = $"{newFileName}";
                    response.Success = true;
                    response.Message = "Dữ liệu bị lỗi!";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
