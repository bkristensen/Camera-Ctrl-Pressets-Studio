[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Edit antarmuka dan terjemahannya

*Ditulis untuk Camera Control Studio 1.1.1.*

## Buka file yang tepat

Buka `Camera-Control-Pressets-Studio.sln`. Di Solution Explorer, buat solusinya, lalu buka `CameraControlStudio → Forms → MainForm.cs`. Tekan **Shift+F7** untuk Windows Forms Designer. Jangan pilih `MainForm.Designer.cs` sebagai permukaan desain. Gunakan F7 untuk kembali ke kode.

Instal beban kerja desktop Visual Studio .NET dari `.vsconfig`. Aplikasinya adalah WinForms di .NET Framework 4.8. Perancang WPF tidak digunakan.

## Ubah tata letak

Gunakan panel Properties untuk mengubah `Dock`, `Margin`, `Padding`, ukuran baris dan ukuran kolom. Gunakan Kerangka Dokumen untuk memilih panel bertumpuk. Tata letak utama menggunakan tabel dan `SplitContainer`; hindari koordinat layar yang dikodekan secara keras.

Setiap jendela mewarisi dari `AppForm` dan memiliki konstruktor publik tanpa parameter. Letakkan tata letak di `.Designer.cs` dan acara di file `.cs` yang cocok. Atur properti `Icon` formulir di Desainer. Periksa tata letak di Visual Studio dan di aplikasi yang sedang berjalan.

Roda kamera dan tombol biru digambar di `Controls/BlueControls.cs`. Pembagi vertikal dan kontrol zoom ada di `Controls/MainViewControls.cs`. Perancang dapat memindahkan dan mengukur kontrol ini. Gambar ikon preset dan judul netralnya ada di `Assets/PressetIcons.resx`, dan `Controls/PresetIcons.cs` memuatnya. Tab Video berisi bilah geser yang dibuat oleh desainer. Aplikasi yang berjalan menampilkan penggeser yang didukung oleh driver kamera.

## Tetapkan default

Edit `Shared/Models.cs`, di `SavedData.Defaults`, untuk pengaturan profil baru. Edit `ProfileLayout` untuk tata letak awal. Profil yang ada mempertahankan nilai tersimpannya. Buat profil baru untuk menguji default baru; jangan menghapus profil pengguna untuk memaksanya.

## Perbarui terjemahan

Edit nilai yang cocok dalam file bahasa yang relevan di bawah `Shared/Locales`. Jaga agar `key` tidak berubah. XML memerlukan `&amp;` untuk ampersand. Semua file harus memiliki kunci yang sama. Jaga agar ID kamera dan ikon yang tersimpan tidak berubah.

Jalankan `scripts/check-translations.ps1`, lalu build. Uji dengan `"Camera Control Studio.exe" --lang=de` atau pemilih bahasa di pengaturan. Mulai ulang setelah mengubah pilihan bahasa yang disimpan.

## Tambahkan bahasa

Salin `en.xml` ke file kode bahasa baru. Terjemahkan setiap nilai dan daftarkan kode dan nama di `Shared/UiText.cs`. Pemeriksa terjemahan dan tes lokal mengambil file baru dan registrasi secara otomatis. Deklarasikan bahasa aplikasi dalam manifes MSIX.

Pemilihan otomatis menggunakan awalan bahasa Windows. Bahasa yang tidak didukung akan kembali ke bahasa Inggris. Uji jendela sempit dan label panjang; terjemahan tidak boleh menyembunyikan kontrol.
