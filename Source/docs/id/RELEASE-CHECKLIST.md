# Daftar periksa rilis

*Ditulis untuk Camera Control Studio 1.1.1.*

Gunakan [panduan build dan rilis](BUILD-AND-RELEASE.md) untuk perintah dan jalur output.

## Pemeriksaan otomatis

- [ ] Jalankan `scripts/build.ps1` pada checkout bersih dengan dependensi terkunci.
- [ ] Konfirmasikan bahwa setiap lokal memiliki kunci unik dan tidak kosong yang sama. Build menjalankan pemeriksaan terjemahan dan tes lokal.
- [ ] Tinjau versi sumber dan biner, identitas paket, dan catatan rilis.

## Pemeriksaan jendela dan kamera

- [ ] Luncurkan pada 900 × 500 dan verifikasi minimum 670 × 450. Ubah ukuran, maksimalkan, dan pulihkan pada skala tampilan 100%, 125%, 150%, dan 200%. Periksa footer, input, ikon gabungan, dan mesin terbang zoom.
- [ ] Buka pengaturan di sebelah kanan. Tarik pembatasnya. Tutup dan buka kembali. Uji tata letak yang disimpan setelah melepaskan sambungan monitor.
- [ ] Ingat kembali posisinya: kuning selama gerakan, hijau hanya setelah pembacaan kembali yang cocok. Geser/miringkan/zoom menghapus warna aktif. Putuskan sambungan selama transisi: tidak ada warna hijau palsu. Ikon tidak pernah hilang setelah konfirmasi.
- [ ] Uji setiap ikon gabungan, termasuk setiap urutan terbalik. Uji tombol zoom, penggeser zoom, dan pembalikan kedua arah.
- [ ] Pada Brio yang didukung, uji FOV otomatis pada kedua arah zoom dan pada tepi geser kiri dan kanan. Ikuti langkah pengujian [Brio](VALIDATION.md#bidang-pandang-brio-dan-zoom).
- [ ] Aktifkan konfirmasi kamera. Mulai ulang. Pastikan tidak ada enumerasi atau koneksi kamera sebelum Ya. Tes Tidak, lalu coba lagi dari tombol geser/zoom. Uji perubahan profil dan pemilihan sumber.

## Data dan pengeditan

- [ ] Cadangkan file profil 0.6 sebelum migrasi. Konfirmasikan preset dan ID ikon tetap ada. Konfirmasikan bahwa pengaturan program eksternal yang usang tidak ada setelah disimpan.
- [ ] Uji profil baru/salin/ganti nama, pulihkan tata letak, dan pertahankan perilaku tata letak saat ini. Pastikan posisi sementara tetap hanya untuk sesi.
- [ ] Uji pengaturan gambar otomatis/manual dan default dengan kamera sebenarnya.
- [ ] Buka setiap Formulir dengan Shift+F7 di Visual Studio. Sesuaikan properti tata letak, simpan dan bangun kembali. Gambar khusus tetap berada di kelas kontrol.
- [ ] Buka penampil panduan dari ZIP yang diekstrak dalam proses Windows 64-bit. Buka lagi dan cetak panduan ke PDF. Konfirmasikan penginstal dan ZIP menyertakan loader asli WebView2 yang cocok di bawah `runtimes` untuk setiap arsitektur yang didukung.
- [ ] Periksa indeks panduan pengguna [](USER-GUIDES.md) dan konfirmasikan bahwa setiap panduan bahasa ada di kedua paket. Periksa tangkapan layar panduan [](SCREENSHOTS.md) untuk cakupan tab dan subtab yang lengkap, pemangkasan, dan tumpang tindih.

## Distribusi

- [ ] Untuk rilis portabel, gunakan `scripts/build.ps1`. Untuk penginstal Inno Setup, gunakan `scripts/build-installer.ps1`, yang juga menjalankan build dan pengujian portabel. Verifikasi versi yang ditampilkan oleh penginstal dan aplikasi.
- [ ] Untuk rilis Store, ikuti daftar periksa [Store](MSIX-AND-STORE.md). Uji jenis paket yang Anda rencanakan untuk diterbitkan di Windows.
- [ ] Jika menandatangani, verifikasi tanda tangan Authenticode pada aplikasi yang dapat dieksekusi dan penginstal jika berlaku. Beri label unduhan yang tidak ditandatangani dengan jelas. Hasilkan hash SHA-256 setelah penandatanganan dan pengemasan akhir.
- [ ] Tautan ke [Kebijakan penandatanganan kode](SIGNPATH.md) di catatan rilis GitHub. Untuk rilis SignPath, verifikasi asal build dan persetujuan manual serta beri kredit SignPath untuk file yang ditandatangani.
- [ ] Publikasikan sumber, lisensi, catatan rilis, hash, dan binari secara bersamaan. Jangan mengklaim pengujian perangkat keras yang belum dilakukan atau persetujuan Toko.
