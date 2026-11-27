# Terjemahkan Camera Control Studio

*Ditulis untuk Camera Control Studio 1.0.1.*

## Edit bahasa yang ada

1. Buka `Source/Shared/Locales/<code>.xml`. Gunakan `en.xml` sebagai referensi untuk konteks dan kunci.
2. Edit teks di antara tag `<text>`. Jaga agar kunci, placeholder seperti `{0}`, dan ID yang disimpan tetap utuh. Simpan sebagai UTF-8. Dalam XML, tulis ampersand literal sebagai `&amp;`.
3. Jika Anda juga mengubah bantuan, edit folder yang cocok di `Source/docs`. Uji tautan ke panduan dan gambar lain di Penampil Panduan.

## Tambahkan bahasa

1. Salin `en.xml` ke file yang diberi nama kode bahasa baru dan terjemahkan semua nilainya.
2. Daftarkan kode dan nama tampilan pada indeks yang sama di `LanguageCodes` dan `LanguageNames` di `Source/Shared/UiText.cs`. Perbarui penanganan kanan-ke-kiri jika memungkinkan.
3. Tambahkan bahasa aplikasi ke manifes MSIX. Jika Anda menambahkan panduan pengguna, ikuti struktur folder yang ada di bawah `Source/docs`, tambahkan ke `USER-GUIDES.md`, dan verifikasi bahwa Penampil Panduan membukanya.

## Verifikasi dan kirimkan

Dari `Source`, jalankan `powershell -File scripts/check-translations.ps1` dan build. Mulai aplikasi dengan `--lang=<code>`. Periksa dialog pada ukuran jendela terkecil dan navigasikan dengan keyboard. Uji label panjang, arah teks, dan pesan yang mengisi placeholder.

Ikuti panduan proyek [GitHub](GITHUB.md) untuk membuka permintaan penarikan. Beri nama bahasa dan kode, rangkum perubahannya, dan nyatakan apa yang Anda uji. Sertakan tangkapan layar jika itu membantu pengulas mengevaluasi tata letaknya. Tambahkan suntingan tindak lanjut ke cabang yang sama selama peninjauan.
