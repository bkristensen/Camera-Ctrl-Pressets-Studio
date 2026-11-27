# Kebijakan penandatanganan kode

SignPath Foundation menawarkan penandatanganan gratis untuk proyek sumber terbuka yang memenuhi syarat. Penerimaan diperlukan. Sertifikat tersebut mengidentifikasi SignPath Foundation sebagai penerbit; tautan layanan yang diverifikasi dibangun ke repositori sumber. Proyek ini telah mengajukan penandatanganan Yayasan SignPath. Aplikasi tertunda, dan unduhan GitHub tetap tidak ditandatangani.

Sebelum Anda melamar, publikasikan sumber di bawah lisensinya. Jaga agar proyek tetap terkini. Publikasikan rilis, dokumentasikan aplikasi, dan buat build dapat diverifikasi. Tanda tangani hanya file proyek Anda sendiri. Tetap keluarkan kredensial dari repositori.

Kebijakan privasi [](../../PRIVACY.md) mencakup aplikasi, pemeriksaan pembaruan GitHub, dan waktu proses WebView2 GuideViewer.

## Sementara permohonannya masih tertunda

- Periksa apakah repositori publik memiliki build Windows yang berhasil di bawah **Actions**. Gunakan alur kerja tersebut sebagai dasar integrasi SignPath.
- Konfirmasikan orang-orang yang bertanggung jawab atas komitmen, peninjauan, dan persetujuan penandatanganan. Aktifkan autentikasi multifaktor untuk semua anggota tim di GitHub dan SignPath.
- Minta SignPath untuk mengonfirmasi bagaimana biner WebView2 SDK dan penginstal Inno Setup harus ditangani. Paket-paket tersebut mencakup binari dan pemuat SDK Microsoft. Aplikasi ini menggunakan Runtime WebView2 yang diinstal pada PC pengguna. Cantumkan dependensi ini dalam aplikasi dan simpan pemberitahuannya.
- Tambahkan tautan **Kebijakan penandatanganan kode** ke catatan rilis GitHub untuk halaman unduh yang disediakan dalam aplikasi. Nyatakan status penandatanganan sebenarnya dari unduhan tersebut.

## Setelah SignPath menerima proyek tersebut

- Tambahkan atribusi SignPath yang disetujui dan nama atau grup izin yang dikonfirmasi untuk pembuat komitmen, peninjau, dan pemberi persetujuan ke halaman ini.
- Konfigurasikan SignPath untuk menerima artefak dari build GitHub Actions tepercaya. Batasi penandatanganan pada biner proyek. Tetapkan dan terapkan nama produk dan versi produk yang cocok di seluruh file yang dikirimkan bersama.
- Untuk ZIP, tanda tangani aplikasi yang dapat dieksekusi sebelum dikemas. Untuk penginstal, masukkan aplikasi yang ditandatangani dan dapat dieksekusi ke dalam penginstal sebelum menandatangani penginstal yang sudah selesai. Dapatkan persetujuan manual untuk setiap permintaan penandatanganan rilis.
- Verifikasi tanda tangan dan stempel waktu Authenticode, lalu buat checksum SHA-256. Sertakan tautan **Kebijakan penandatanganan kode** dan status penandatanganan di catatan rilis. Verifikasi file dan hash yang diunggah sebelum dipublikasikan.

Untuk unduhan GitHub langsung, tanda tangan yang valid mengidentifikasi penerbit dan melindungi integritas biner. Pengunduhan baru masih dapat memicu SmartScreen saat reputasi dibangun. Untuk distribusi pertama kali melalui Store, Microsoft menandatangani paket MSIX yang diterima.

Untuk sertifikat penandatanganan kode tepercaya secara publik yang tersedia melalui penyimpanan sertifikat Windows pengguna saat ini, gunakan `scripts/build.ps1 -CertificateThumbprint <thumbprint> -SignToolPath <path>` untuk ZIP portabel. Gunakan parameter yang sama dengan `scripts/build-installer.ps1` untuk menandatangani aplikasi yang dapat dieksekusi dan penginstal yang dihasilkan. Persetujuan SignPath memerlukan alur kerja penandatanganannya sendiri.

Baca persyaratan resmi [](https://signpath.org/terms.html), situs web [Foundation](https://signpath.org/), dan panduan [SmartScreen Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation). Diperiksa 24 November 2026. Untuk rute Store, lihat [MSIX dan Store](MSIX-AND-STORE.md).
