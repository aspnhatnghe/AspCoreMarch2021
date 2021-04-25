--- DEMO FUNCTION
--Function tính doanh thu theo khách hàng
CREATE FUNCTION DoanhThuTheoKhachHang
(
	@MaKH nvarchar(50),
	@Nam int
)
RETURNS float
AS BEGIN
	--B1: Khai báo
	DECLARE @DoanhSo float
	--B2: Tính toán
	SELECT @DoanhSo = SUM(cthd.DonGia * cthd.SoLuong * 
		(1 - cthd.GiamGia))
	FROM ChiTietHD cthd JOIN HoaDon hd ON hd.MaHD = cthd.MaHD
	WHERE hd.MaKH = @MaKH AND YEAR(hd.NgayDat) = @Nam
	--B3: Trả về
	RETURN @DoanhSo
END

SELECT dbo.DoanhThuTheoKhachHang('ANTON', 1996)

SELECT MaKH, HoTen, 
	dbo.DoanhThuTheoKhachHang(MaKH, 1996) as DoanhThu
FROM KhachHang
WHERE dbo.DoanhThuTheoKhachHang(MaKH, 1996) > 0
ORDER BY DoanhThu DESC


--- FUNCTION trả về table (có 2 cách viết)
--Thống kê doanh thu khách hàng theo năm
CREATE FUNCTION ThongKeDoanhThuTheoNam
(
	@Nam int
)
RETURNS TABLE AS RETURN
	SELECT hd.MaKH, kh.HoTen,
		SUM(cthd.DonGia * cthd.SoLuong * (1 - cthd.GiamGia))
		as DoanhThu
	FROM ChiTietHD cthd JOIN HoaDon hd ON hd.MaHD = cthd.MaHD
		JOIN KhachHang kh ON kh.MaKH = hd.MaKH
	WHERE YEAR(NgayDat) = @Nam
	GROUP BY hd.MaKH, kh.HoTen

SELECT * FROM dbo.ThongKeDoanhThuTheoNam(1996)

-----Tạo function trả về bảng - cách đầy đủ
-----Thống kê doanh thu theo loại, nhà cung cấp
CREATE FUNCTION ThongKeDoanhThuHangHoaTheoNam
(	@Nam int	)
RETURNS @tmp TABLE (
	TenLoai nvarchar(50), 
	NhaCungCap nvarchar(50),
	DoanhThu float
)
AS BEGIN
	INSERT INTO @tmp SELECT TenLoai, TenCongTy,
		SUM(cthd.DonGia * cthd.SoLuong * (1 - cthd.GiamGia))
	FROM ChiTietHD cthd JOIN HoaDon hd ON hd.MaHD = cthd.MaHD
		JOIN HangHoa hh ON hh.MaHH = cthd.MaHH
		JOIN Loai lo ON lo.MaLoai = hh.MaLoai
		JOIN NhaCungCap ncc ON ncc.MaNCC = hh.MaNCC
	WHERE YEAR(NgayDat) = @Nam
	GROUP BY TenLoai, TenCongTy
	RETURN
END

SELECT * FROM dbo.ThongKeDoanhThuHangHoaTheoNam(1996)


----------------DEMO TRIGGER
--Thêm cột số lượng cho bảng HangHoa
ALTER TABLE HangHoa ADD SoLuong int

UPDATE HangHoa SET SoLuong = MaHH % 20

SELECT MaHH, TenHH, SoLuong
FROM HangHoa

--Tạo trigger tự động trừ số lượng hàng hóa tồn
--khi thêm mới chi tiết hóa đơn
CREATE TRIGGER CapNhatSoLuongTon
ON ChiTietHd
AFTER INSERT
AS BEGIN
	DECLARE @MaHH int
	DECLARE @SoLuongMua int
	SELECT @MaHH = MaHH, @SoLuongMua = SoLuong FROM inserted

	UPDATE HangHoa SET SoLuong = SoLuong - @SoLuongMua
	WHERE MaHH = @MaHH
END

--DEMO
SELECT MaHH, TenHH, SoLuong FROM HangHoa WHERE MaHH = 1010
INSERT INTO ChiTietHD(MaHD, MaHH, SoLuong, DonGia, GiamGia)
	VALUES(10248, 1010, 3, 111, 0.2)

---Thêm cột thành tiền cho hóa đơn
ALTER TABLE HoaDon ADD ThanhTien float default 0

--Viết trigger tự động cập nhật thành tiền cho hóa đơn
--khi thêm/sửa/xóa chi tiết hóa đơn
CREATE TRIGGER CapNhatThanhTienHoaDon ON ChiTietHd
AFTER INSERT, UPDATE, DELETE
AS BEGIN
	DECLARE @MaHD int
	DECLARE @DoanhSo float;
	WITH tmp AS (
		SELECT MaHD FROM inserted
		UNION
		SELECT MaHD FROM deleted
	)
	SELECT @MaHD = MaHD FROM tmp

	SELECT @DoanhSo = SUM(cthd.DonGia * cthd.SoLuong * (1 - cthd.GiamGia))
	FROM ChiTietHD cthd WHERE MaHD = @MaHD
	UPDATE HoaDon SET ThanhTien = @DoanhSo WHERE MaHD = @MaHD
END