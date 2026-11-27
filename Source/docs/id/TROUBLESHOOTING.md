# Pemecahan masalah

*Ditulis untuk Camera Control Studio 1.0.1.*

## Daftar isi

- [1. Aplikasi video dan bidang pandang](#1-aplikasi-video-dan-bidang-pandang)
  - [Bidang pandang terbatas di Zoom Workplace](#bidang-pandang-terbatas-di-zoom-workplace)
- [2. Koneksi dan kompatibilitas](#2-koneksi-dan-kompatibilitas)
  - [Kamera hilang atau tidak tersedia](#kamera-hilang-atau-tidak-tersedia)
  - [Kontrol hilang atau berbeda antar aplikasi](#kontrol-hilang-atau-berbeda-antar-aplikasi)
- [3. Gerakan dan posisi tersimpan](#3-gerakan-dan-posisi-tersimpan)
  - [Gerakan berhenti, tersendat, atau terbalik](#gerakan-berhenti-tersendat-atau-terbalik)
  - [Bidang pandang atau posisi tersimpan keliru](#bidang-pandang-atau-posisi-tersimpan-keliru)

## 1. Aplikasi video dan bidang pandang

### Bidang pandang terbatas di Zoom Workplace

**Gejala**\
Tepi adegan hilang. Zoom keluar dan gerakan ke samping berhenti terlalu cepat. Posisi tersimpan menampilkan bingkai berbeda. **Optimize for video sharing** nonaktif. Batas yang sama muncul di Logitech BRIO Control.

**Penyebab**\
**Dikonfirmasi pada kasus yang dilaporkan.** **Efficiency Mode** aktif di Zoom Workplace. Setelah mode dimatikan dan Zoom dibuka ulang, bidang pandang kembali. Camera Control Studio dan posisi tersimpan tidak diubah. Mekanisme teknisnya belum diketahui.

**Solusi**

1. Di Zoom Workplace, buka foto profil → **Settings → Meeting & webinar → Advanced**.
2. Di **Performance**, matikan **Efficiency Mode**. Jika terkunci, hubungi administrator Zoom.
3. Tutup Zoom Workplace sepenuhnya dan buka kembali. Pertahankan posisi tersimpan selama pengujian.

**Pemeriksaan**\
Pastikan tepi adegan terlihat kembali. Panggil posisi tersimpan dan bandingkan gambar langsung.

**Perangkat dan aplikasi terkait**\
Windows, Zoom Workplace, Logitech BRIO, Logitech BRIO Control, dan Camera Control Studio 0.7.0-beta.8. Versi Zoom, driver, dan firmware tidak dicatat.

**Referensi**\
[Zoom Support](https://support.zoom.com/hc/en/article?id=zm_kb&sysparm_article=KB0080877) · [GitHub #5](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/5)

## 2. Koneksi dan kompatibilitas

### Kamera hilang atau tidak tersedia

**Gejala**\
Kamera tidak ada dalam daftar atau berhenti merespons setelah mode tidur.

**Penyebab**\
**Kemungkinan penyebab.** Sambungan USB, izin Windows, aplikasi lain, atau konfirmasi siaran belum diberikan.

**Solusi**

1. Periksa kabel USB. Jika diminta konfirmasi, mulai siaran sebelum memilih **Ya**.
2. Periksa izin kamera Windows. Uji pratinjau di aplikasi Kamera Windows, lalu tutup aplikasi itu.
3. Coba satu kontrol untuk menyambungkan kembali. Jika kamera tersimpan tidak muncul, pilih kamera lewat tombol tengah. Secara bawaan, klik dua kali tombol tengah untuk membuka daftar kamera.

![Roda arah dengan tombol pemilihan kamera di tengah](../images/screenshots/control%20wheel%20only.png)

**Pemeriksaan**\
Cek nama kamera di bawah jendela dan coba satu kontrol pada gambar langsung.

**Perangkat dan aplikasi terkait**\
Windows serta kamera bawaan atau USB. Akses bersamaan bergantung pada driver.

**Referensi**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Panduan pengguna](USER-GUIDE.md)

### Kontrol hilang atau berbeda antar aplikasi

**Gejala**\
Pratinjau muncul, tetapi beberapa kontrol tidak ada atau hanya terlihat efeknya di satu aplikasi.

**Penyebab**\
**Dikonfirmasi.** Camera Control Studio memakai kontrol dari driver Windows. **Kemungkinan.** Driver, firmware, atau aplikasi lain bisa memengaruhi kontrol atau gambar.

**Solusi**

1. Pilih kamera fisik yang sama. Bandingkan pratinjau satu aplikasi pada satu waktu.
2. Tutup alat kamera lain, buka kembali aplikasi video tujuan, dan coba satu kontrol yang tersedia.
3. Cek Windows Update dan dukungan untuk model yang tepat. Ikuti petunjuk produsen untuk pembaruan driver atau firmware.

**Pemeriksaan**\
Pastikan kontrol mengubah gambar di aplikasi yang akan dipakai. Catat versi saat melaporkan masalah.

**Perangkat dan aplikasi terkait**\
Windows, driver, firmware, dan aplikasi video atau kamera.

**Referensi**\
[Microsoft Support](https://support.microsoft.com/en-us/windows/hardware/camera/camera-doesn-t-work-in-windows) · [Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K)

## 3. Gerakan dan posisi tersimpan

### Gerakan berhenti, tersendat, atau terbalik

**Gejala**\
Kamera atau zoom cepat mencapai batas. Peralihan tersendat atau tombol panah bergerak ke arah yang salah.

**Penyebab**\
**Dikonfirmasi.** Driver menentukan batas dan ukuran langkah. Aplikasi dapat membalik arah panah.

Jika arah berubah saat Logitech Camera Settings atau Logitech BRIO Control dibuka, aplikasi lain mungkin memengaruhi kontrol. Menurut laporan, gambar di Zoom tetap tegak. Sumbu yang terpengaruh dan penyebabnya belum diketahui.

**Solusi**

1. Gunakan tombol bingkai persegi di atas kaca pembesar untuk mengembalikan bidang terlebar dan zoom minimum. Uji lagi.
2. Buka **Pengaturan → Geser kamera** dan balik hanya sumbu yang salah.
3. Untuk gerakan lebih kecil, coba **Ctrl** dan tombol panah. Durasi peralihan tidak memperkecil langkah driver.
4. Saat tidak bersiaran, tutup alat Logitech. Uji kedua sumbu di aplikasi video yang sama dengan tingkat zoom yang sama. Buka satu alat Logitech setiap kali untuk melihat apakah arah kembali berubah.

**Pemeriksaan**\
Periksa arah panah dan batas pada gambar langsung.

**Perangkat dan aplikasi terkait**\
Kamera dengan kontrol gerak atau zoom di Windows. Batas bergantung pada model dan mode video.

**Referensi**\
[GitHub #3](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/3) · [GitHub #4](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/issues/4) · [Panduan pengguna](USER-GUIDE.md)

### Bidang pandang atau posisi tersimpan keliru

**Gejala**\
Gambar terlalu sempit, posisi tidak sesuai, atau pilihan sudut tidak tersedia.

**Penyebab**\
**Dikonfirmasi.** Posisi terkait dengan kamera dan profil. Posisi lama tanpa sudut tersimpan memakai sudut saat ini. Tidak semua driver menawarkan sudut.

**Solusi**

1. Periksa profil dan kamera. Kembalikan bingkai penuh dan lihat gambar langsung.
2. Periksa bidang pandang awal di **Video → Bidang pandang**. Jika sudut tersedia, izinkan perubahan manual dan bandingkan.
3. Panggil posisi. Jika gambar hanya menyempit saat Zoom terbuka, ikuti bagian tentang Zoom. Pertahankan posisi yang sudah tersimpan selama mencari penyebab.

**Pemeriksaan**\
Bandingkan gambar dengan bingkai yang diharapkan. Uji posisi lagi setelah bidang awal kembali.

**Perangkat dan aplikasi terkait**\
Profil dan posisi Camera Control Studio. Pilihan sudut bergantung pada kamera dan driver.

**Referensi**\
[Logitech BRIO 4K](https://support.logi.com/hc/en-us/articles/31952740030999-Specification-BRIO-4K) · [Panduan pengguna](USER-GUIDE.md)

---

Masih perlu bantuan? Periksa log sebelum membagikannya. Sertakan model, versi, langkah, dan hasil.

[Laporkan masalah](../en/ISSUES.md) · [Panduan pengguna](USER-GUIDES.md)
