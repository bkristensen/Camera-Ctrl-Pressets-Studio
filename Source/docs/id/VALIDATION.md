# Validasi untuk 0.7.0-beta.3

Versi Rilis Windows menggunakan dependensi terkunci. Paritas terjemahan dan pengujian otomatis berhasil. Pengujian tersebut mencakup gerakan, preset, bahasa profil, persistensi, logging, dan HRESULT kamera yang belum siap dilaporkan setelah hibernasi.

ZIP portabel yang diekstraksi telah diuji dalam proses Windows x86 dan x64. Keduanya membuka dan membuka kembali panduan penggunaan dan pengaturan sehari-hari, menampilkan tangkapan layarnya, dan mencetak ke PDF. X64 PDF memiliki judul, versi, tanggal, dan jumlah halaman yang benar di kelima halaman. Tombol **?** hadir selama inisialisasi waktu desain WinForms. Pemuat ARM64 telah diperiksa untuk arsitektur biner yang benar.

Profil Standar Bahasa Inggris diambil dengan kamera demo. 12 tangkapan layar jendela penuh mencakup status jendela utama, setiap tab pengaturan, dan setiap subtab. Tautan lokal dalam panduan dan README diperiksa berdasarkan jalur dokumen yang dikemas.

## Bidang pandang Brio dan Zoom

Gunakan Brio fisik yang menawarkan 65°, 78° dan 90°. Catat model, firmware, driver, versi aplikasi, dan mode video yang digunakan Zoom.

1. Mulai Zoom dan gunakan pemandangan tetap dengan titik referensi yang terlihat di dekat tepinya. Konfirmasikan framing terluas pada 90° dan zoom minimum menggunakan Logitech Brio Control.
2. Mulai Camera Control Studio dengan **Jangan ubah** dan konfirmasikan bahwa perangkat mempertahankan FOV-nya. Mulai ulang dengan **Terlebar yang tersedia**, lalu ulangi dengan FOV yang ditentukan dan dengan konfirmasi streaming diaktifkan.
3. Saat ketiga aplikasi berjalan, ubah geser, miringkan, zoom, dan FOV di Logitech Brio Control. Konfirmasikan pembacaan yang diperbarui di Camera Control Studio dengan manual FOV dinonaktifkan dan diaktifkan.
4. Ubah FOV di Camera Control Studio. Amati kapan Logitech Brio Control memperbarui tampilannya sendiri. Simpan dan panggil kembali preset untuk setiap FOV, termasuk preset lama tanpa FOV.
5. Dari bidikan sempit dan diperbesar, pilih **Bingkai penuh** dan bandingkan gambar dengan referensi dari langkah 1. Ulangi selama transisi preset dan dengan Logitech Brio Control sudah terbuka.
6. Tutup Camera Control Studio dan periksa apakah Logitech Brio Control masih dapat mengganti kamera. Ulangi dalam urutan peluncuran aplikasi terbalik. Lampirkan log diagnostik dan catat setiap perbedaan antara gambar langsung dan nilai yang dilaporkan.
7. Pada 65° dan zoom minimum, tekan Zoom out sekali. Periksa apakah FOV berubah menjadi 78° dan langkah selanjutnya memperkecil. Ulangi pada 78°. Periksa apakah Zoom out berhenti hanya pada 90° dan zoom minimum. Uji jalur sebaliknya untuk Memperbesar.
8. Di tepi panci horizontal, tekan lagi panah yang sama. Periksa apakah FOV yang lebih lebar dan penyesuaian zoom memungkinkan gerakan tanpa kehilangan bidikan. Jika tidak ada tampilan yang membantu, periksa apakah framing asli kembali. Matikan fitur tersebut dan konfirmasikan bahwa FOV tetap terpasang.

Inno Setup 7 mengkompilasi penginstal dengan panduan untuk 12 bahasa dan loader x86, x64 dan ARM64. Kedua paket tidak ditandatangani, dengan hash SHA-256 disediakan. Instalasi dan penghapusan instalasi akun bersih, siklus hibernasi kamera fisik, dan pengujian runtime ARM64 masih tertunda. Lihat daftar periksa rilis [](RELEASE-CHECKLIST.md) dan [panduan pembuatan dan rilis](BUILD-AND-RELEASE.md).
