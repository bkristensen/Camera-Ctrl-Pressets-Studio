# Deskripsi proyek

*Ditulis untuk Camera Control Studio 1.1.0.*

Camera Control Studio menyimpan dan mengingat kembali hasil jepretan kamera di Windows. Gunakan di studio, ruang kelas, atau pertemuan langsung. Aplikasi pengambilan Anda menangani video tersebut.

## Perilaku inti

Gunakan ring bagian dalam untuk gerakan normal dan ring bagian luar untuk gerakan cepat. Setiap cincin memiliki panah diagonal. Klik dua kali tombol tengah untuk memilih kamera, atau seret untuk mengarahkan. Anda dapat mengubah kedua gerakan di bawah Pengaturan → Menggeser. Tombol zoom bergerak sesuai langkah yang ditetapkan. Penggeser mengatur zoom dalam FOV saat ini. Aplikasi ini tidak membuka pratinjau video. Gerakan [Set dan FOV](SETUP.md#atur-bidang-pandang)

Posisi permanen menyimpan nama, ID simbol stabil, dan geser, miringkan, zoom, dan FOV yang dilaporkan driver. Simbol gabungan menunjukkan subjek dari kiri ke kanan. Posisi permanen muncul pertama kali dalam huruf tebal. Posisi sementara tidak memiliki ikon dan bertahan hingga Anda menutup aplikasi. Gunakan Putar atau klik dua kali untuk mengingat salah satu jenis.

Interpolasi yang lancar meminta koordinat perantara. Satu baris berwarna kuning selama transisi. Warnanya hijau hanya jika pembacaan ulang sesuai dengan target. Tindakan manual, pembatalan, atau transisi yang gagal akan menghapus status tersebut. Pengemudi menentukan granularitas dan latensi gerakan sebenarnya.

Profil berisi pemilihan kamera, posisi permanen, nilai gambar, kecepatan pergerakan manual, durasi atau kecepatan transisi yang telah ditentukan, pembalikan arah, konfirmasi kamera, tata letak jendela, dan bahasa. Mengubah profil akan memulai ulang aplikasi dengan bahasa profil yang dipilih. Konfirmasi kamera opsional memblokir enumerasi dan koneksi hingga pengguna mengonfirmasi streaming telah dimulai.

## Arsitektur

`CameraControlStudio` adalah aplikasi .NET Framework 4.8 WinForms. `Forms/*.Designer.cs` memegang tata letak. `Forms/*.cs` mengadakan penanganan acara. `AppForm` adalah basis umum. `Controls` berisi rendering vektor. `Camera.cs` menggunakan kamera Windows DirectShow dan antarmuka video-proc tanpa grafik pengambilan.

`Shared` menampung model, persistensi, terjemahan, aritmatika pergerakan, posisi sesi, dan logika keadaan. `Tests` adalah rangkaian regresi konsol .NET 8. Profil menggunakan penggantian file atom, pencadangan, dan pemeriksaan revisi. Penyimpanan profil 0,6 yang ada dipertahankan; pengaturan awal program yang usang diabaikan dan dibuang saat disimpan.

## Ruang lingkup dan kompatibilitas

Pemiliknya telah melaporkan bahwa aplikasi sebelumnya berfungsi pada laptop dan pengaturan kameranya. Penulisan ulang 0,7 ini telah dikompilasi dan lulus uji logika tetapi belum dilakukan terhadap perangkat keras fisik di lingkungan build ini. Tidak ada kamera tambahan atau perangkat lunak pengambilan yang diklaim telah diuji.

Kamera Windows lainnya mungkin berfungsi ketika drivernya menerapkan kontrol standar yang diperlukan. Akses bersamaan dengan perangkat lunak pengambilan, rapat, atau streaming bergantung pada driver. Kamera virtual mungkin tidak memperlihatkan kontrol fisik. Ini bukan pengontrol jaringan-PTZ generik. Linux dan macOS dapat dijadikan host melalui alur kerja Docker, tetapi tidak dapat menjalankan UI Windows ini secara asli.

Silakan laporkan kombinasi yang berhasil, termasuk versi Windows, model kamera, versi driver, aplikasi pengambilan, dan kontrol mana yang berfungsi. Laporkan juga batasannya sehingga daftar kompatibilitas dapat didasarkan pada bukti.

## Kemungkinan pekerjaan di masa depan

Otomatisasi regresi UI Windows; aksesibilitas dan penemuan keyboard yang lebih baik; impor/ekspor opsional profil individual; matriks perangkat keras yang terverifikasi; rilis yang ditandatangani setelah disetujui; backend kamera tambahan jika mempertahankan alur kerja inti yang sederhana. Ini adalah ide, bukan fitur yang disampaikan.
