USE eStore20
GO

--Thêm mới loại
INSERT INTO Loai(Hinh, MoTa, TenLoai)
	VALUES (NULL, N'Bàn ghế', N'Bàn ghế')

INSERT INTO Loai(TenLoai, MoTa)
	VALUES (N'Điện máy', N'Các mặt hàng điện máy')

SELECT * FROM Loai

--Thêm Hàng hóa
INSERT INTO HangHoa(TenHH, DonGia, MoTaDonVi, MaLoai,
	MaNCC, MoTa) VALUES(N'Tủ lạnh Panasonic', 11199,
	N'cái', 1008, 'NK', N'328 lít, BH 3 năm')

SELECT TOP 10 * 
FROM HangHoa
ORDER BY NgaySX DESC

-- Giảm giá 5% cho mặt hàng có giá > 500$ 
-- và sản xuất cách đây hơn 5 năm
UPDATE HangHoa
SET DonGia = DonGia * 0.95
WHERE DonGia > 500 
	AND (YEAR(GETDATE()) - YEAR(NgaySX) >=  5)

--Thống kê hàng hóa
SELECT MIN(DonGia) as GiaNhoNhat, SUM(SoLanXem),
	AVG(DonGia) as GiaTrungBinh,
	COUNT(MaHH) as SoLuong
FROM HangHoa

--Thống kê hàng hóa theo loại
SELECT TenLoai, MIN(DonGia) as GiaNhoNhat, SUM(SoLanXem),
	AVG(DonGia) as GiaTrungBinh,
	COUNT(MaHH) as SoLuong
FROM HangHoa as hh JOIN Loai as lo 
	ON hh.MaLoai = lo.MaLoai
GROUP BY lo.MaLoai, TenLoai
HAVING AVG(DonGia) >= 30

---------------VIEW
CREATE VIEW vHangHoa AS
SELECT MaHH, TenHH, DonGia, lo.MaLoai, TenLoai,
	ncc.MaNCC, ncc.TenCongTy as NhaCungCap
FROM HangHoa hh JOIN Loai lo ON lo.MaLoai = hh.MaLoai
	JOIN NhaCungCap ncc ON ncc.MaNCC = hh.MaNCC

SELECT * FROM vHangHoa 
WHERE DonGia BETWEEN 100 AND 999

--Tạo view lấy thông hóa đơn
CREATE VIEW vHoaDon AS
SELECT cthd.MaHH, TenHH, cthd.DonGia, cthd.SoLuong,
	cthd.MaHD, NgayDat, MaKH, TenLoai
FROM HoaDon hd JOIN ChiTietHD cthd ON cthd.MaHD = hd.MaHD
	JOIN HangHoa hh ON hh.MaHH = cthd.MaHH
	JOIN Loai lo on lo.MaLoai = hh.MaLoai

SELECT * FROM vHoaDon WHERE MaHD = 10248

------STORE PROCEDURE
-- Thêm loại
CREATE PROC spThemLoai
	@MaLoai int output,
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN
	INSERT INTO Loai(TenLoai, MoTa, Hinh)
		VALUES(@TenLoai, @MoTa, @Hinh)
	
	SET @MaLoai = @@IDENTITY
END

--DEMO
DECLARE @Loai int
exec spThemLoai @Loai output, N'Nước giải khát', NULL, NULL
PRINT CONCAT(N'Loại vừa thêm có mã: ', @Loai)

CREATE PROC spSuaLoai 
	@MaLoai int,
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN
	UPDATE Loai SET TenLoai = @TenLoai, MoTa = @MoTa,
		Hinh=@Hinh WHERE MaLoai = @MaLoai
END

exec spSuaLoai 1011, N'Bia', N'Bia đủ loại', NULL