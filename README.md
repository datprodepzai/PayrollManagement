# 📊 Phần Mềm Quản Lý Lương - Payroll Management System

Ứng dụng WinForm C# quản lý lương nhân viên với lưu trữ JSON, không cần SQL Server.

## ✨ Tính Năng

### 1. 👥 Quản Lý Nhân Viên
- ➕ Thêm nhân viên mới
- ✎ Sửa thông tin nhân viên
- ✕ Xóa nhân viên
- 📋 Xem danh sách nhân viên

**Thông tin nhân viên:**
- Họ tên, vị trí, phòng ban
- Lương cơ bản, phụ cấp, khấu trừ
- Số điện thoại, email
- Ngày vào làm

### 2. 💰 Danh Sách Lương
- 📅 Lọc lương theo tháng/năm
- 👁️ Xem chi tiết lương của nhân viên
- ✅ Đánh dấu đã trả lương
- 📊 Xem trạng thái (Pending/Paid)

### 3. 🧮 Tính Lương
- 🤖 Tính lương tự động cho toàn bộ nhân viên
- 📆 Hỗ trợ tính lương cho bất kỳ tháng/năm nào
- 🧮 Công thức:
  - **Lương Tính** = Lương Cơ Bản + Phụ Cấp
  - **Lương Thực Nhận** = Lương Tính - Khấu Trừ

### 4. 📊 Báo Cáo Lương
- 📈 Thống kê tổng lương theo tháng
- 👥 Tổng số nhân viên được tính lương
- 💵 Tổng lương tính và lương thực nhận

## 🛠️ Công Nghệ Sử Dụng

- **C# WinForm** - Giao diện Windows
- **.NET 6.0+** - Framework
- **JSON** - Lưu trữ dữ liệu (Newtonsoft.Json)
- **Không SQL Server** - Đơn giản, dễ triển khai

## 📂 Cấu Trúc Thư Mục
