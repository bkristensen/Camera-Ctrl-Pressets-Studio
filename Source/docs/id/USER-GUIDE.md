# Penggunaan dan pengaturan

*Ditulis untuk Camera Control Studio 1.0.1.*

[Pemecahan masalah](TROUBLESHOOTING.md)

[Bahasa Indonesia · Camera Control Studio](../USER-GUIDES.md)

Mulai dulu rapat yang akan disiarkan, misalnya di Zoom. Setelah itu, buka **Camera Control Studio**.

![Jendela utama dengan roda arah, zoom, dan daftar prasetel](../images/screenshots/1.0.1/id/main.png)

Klik **?** untuk membuka bantuan. Arahkan kursor ke tombol untuk melihat fungsinya. Setiap arah berhenti di batas terakhir kamera.

1. **Mengaktifkan preset**

   Klik ▶ pada baris preset, klik dua kali namanya, atau pilih baris lalu tekan Enter.

   | Warna | Arti |
   | --- | --- |
   | Kuning | Kamera sedang bergerak menuju posisi yang disimpan. |
   | Hijau | Driver kamera melaporkan bahwa koordinat yang disimpan telah tercapai. |
   | Biru | Preset tidak ditandai aktif. |

   Hijau mengonfirmasi koordinat yang dilaporkan driver. Periksa gambar dalam siaran. Jika driver tidak mengonfirmasi posisi tepat waktu, baris berubah menjadi biru dan pesan muncul di area status. Ikon tetap terlihat.

2. **Menyesuaikan pembingkaian**

   **Gerakan horizontal**

   Gunakan cincin dalam untuk gerakan normal dan cincin luar untuk gerakan cepat. Panah diagonal menggerakkan dua sumbu pada arah 45°, dengan 45% kecepatan cincin pada tiap sumbu. Tahan panah untuk terus bergerak. Lepaskan untuk berhenti.

   ![Roda arah dengan dua cincin panah dan tombol kamera di tengah](../images/screenshots/control%20wheel%20only.png)

   **Tombol kamera di tengah roda**

   - **Klik dua kali untuk memilih kamera.** Daftar kamera terbuka.
   - **Tahan dan seret.** Mulai dari ikon kamera. Seret ke samping untuk menggeser kamera atau ke atas dan ke bawah untuk memiringkannya. Seret sedikit untuk gerakan lambat. Seret lebih jauh untuk mempercepat, hingga dua kali kecepatan tinggi. Lepaskan tombol atau kembalikan penunjuk ke asal untuk berhenti.

   Di **Pengaturan → Gerakan**, Anda dapat menonaktifkan gerakan seret atau memilih kamera dengan satu klik. Kedua pilihan disimpan di profil aktif.

   **Zoom**

   Gunakan tombol kaca pembesar untuk memperbesar atau memperkecil. Tahan tombol untuk mengulang. Zoom berubah saat penggeser diseret.

   **Pulihkan bingkai penuh**

   Klik tombol persegi berikon bingkai di atas tombol kaca pembesar. Kamera memperkecil zoom sejauh mungkin dan memilih bidang pandang terlebar jika mendukung pengaturan itu. Bingkai kamera berubah. Ukuran jendela aplikasi tetap sama.

   **Menghentikan gerakan horizontal**

   Tekan Esc untuk menghentikan gerakan horizontal yang sedang berlangsung.

   > Menggerakkan kamera secara horizontal atau vertikal atau mengubah zoom secara manual akan membatalkan perpindahan ke preset dan menghapus warna penanda aktifnya.

   **Tombol panah**

   Gunakan tombol panah saat roda arah mendapat fokus. Tahan Shift untuk langkah cepat.

3. **Menyimpan preset sementara**

   Klik **+** di atas daftar, masukkan nama, lalu simpan. Posisi saat ini muncul di bawah preset permanen. Preset sementara tidak memiliki ikon. Preset ini disimpan terpisah untuk setiap profil dan kamera hingga aplikasi ditutup.

   Untuk menghapus preset sementara, pilih barisnya lalu klik **×** di atas daftar. Semua preset sementara dihapus saat aplikasi ditutup. Untuk menggunakan kembali suatu posisi nanti, buat preset di **Pengaturan → Preset**. Preset permanen tidak dapat diedit atau dihapus dari daftar utama.

4. **Menutup aplikasi**

   Jika kontrol gagal, baca pesan di area status di bagian bawah. Kamera mungkin tidak mendukung fitur tersebut atau sedang tidak tersedia.

   Tutup aplikasi seperti biasa. Profil aktif dan tata letak jendela akan disimpan.

   **Melaporkan masalah**

   Laporkan masalah atau fitur yang belum tersedia melalui **Pengaturan → Laporan**.

5. **Pengaturan**

   Pilih profil di bagian bawah jendela. Untuk mengganti kamera, klik dua kali ikon kamera di tengah roda arah.

   Untuk menyimpan posisi secara permanen, buka ikon roda gigi di bagian bawah, pilih **Preset → Tambah preset permanen**, tentukan nama dan ikon, lalu simpan.

   ![Preset permanen dan tombol pengelolaannya](../images/screenshots/1.0.1/id/presets.png)

   Di Pengaturan, atur langkah, waktu transisi, dan pembalikan arah. Tab **Video** memuat kontrol yang tersedia pada driver. Gunakan tab **Profil** untuk membuat atau menyalin profil. Aktifkan konfirmasi siaran jika perangkat lunak perekaman perlu terhubung terlebih dahulu.

   ![Tab Gerakan dengan pengaturan langkah dan waktu transisi](../images/screenshots/1.0.1/id/settings.png)

   ![Tab Aplikasi dengan konfirmasi siaran dan bahasa profil](../images/screenshots/1.0.1/id/application.png)

6. Seret pemisah vertikal untuk mengatur lebar kedua area. Panel pengaturan selalu tertutup saat aplikasi dimulai. Pilih bahasa di **Pengaturan → Aplikasi → Bahasa profil**, lalu mulai ulang aplikasi. Arahkan kursor ke tombol kecil untuk melihat keterangannya.

## Bidang pandang saat kamera bergerak

**Ubah bidang pandang pada batas zoom dan geser**

Pilihan ini aktif secara bawaan. Pada Brio, tekan sekali lagi setelah zoom mencapai batas. Saat memperkecil, bidang pandang berubah dari 65° ke 78°, lalu ke 90°. Saat memperbesar, urutannya terbalik. Di antara perubahan, zoom kembali bergerak per langkah. Di batas geser, aplikasi mencoba bidang yang lebih lebar dan menyesuaikan zoom agar bingkai tetap serupa. Matikan pilihan untuk mempertahankan bidang saat ini.

Prasetel menyimpan bidang pandang jika driver dapat membacanya.

[Penginstalan dan SmartScreen (bahasa Inggris)](../en/SETUP.md)

<https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/blob/main/Source/docs/en/SETUP.md>

## Unduhan dan bantuan

**Unduhan resmi**\
<https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases>\
![Kode QR untuk mengunduh Camera Control Studio](../images/qr/downloads.png)

**Laporkan masalah atau usulkan fitur**\
<https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues?q=is%3Aissue>\
![Kode QR untuk daftar isu proyek di GitHub](../images/qr/issues.png)
