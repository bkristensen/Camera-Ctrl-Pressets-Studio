# MSIX dan distribusi Toko

*Ditulis untuk Camera Control Studio 1.1.0.*

Gunakan `scripts/build-msix.ps1` dan templat manifes untuk membuat paket MSIX. Microsoft meninjau paket tersebut sebelum dapat muncul di Store. Eksekusi portabel tidak ditandatangani.

## Pilih rute

| Rute | Penandatanganan |
| --- | --- |
| Microsoft Store dengan MSIX | Microsoft menandatangani paket Store yang diterima. Anda tidak membeli sertifikat penandatanganan untuk rute ini. |
| Microsoft Store dengan penginstal EXE/MSI | Penandatanganan penerbit diperlukan; Store tidak menandatangani ulang penginstal itu. |
| Unduh langsung MSIX | Paket tersebut harus memiliki tanda tangan yang dipercaya pada perangkat pengguna. |
| EXE portabel langsung | Bangunan yang tidak ditandatangani dapat menghasilkan peringatan. Penandatanganan mengidentifikasi penerbit tetapi tidak menjanjikan reputasi langsung. |

## Daftar periksa

- [ ] Daftarkan produk dan pesan namanya di Partner Center. Salin Nama paket, Publisher, dan PublisherDisplayName yang tepat.
- [ ] Instal alat Windows SDK dan jalankan perintah di bawah ini menggunakan nilai identitas tersebut.
- [ ] Validasi manifes dan uji akses kamera, instal, perbarui, hapus instalan, penyimpanan profil, dan bahasa di Windows. Templat mendeklarasikan operasi desktop/kepercayaan penuh dan akses webcam. Verifikasikan kecocokan ini dengan persyaratan sertifikasi saat ini.
- [ ] Siapkan daftar, kebijakan privasi, tautan dukungan, informasi lisensi, peringkat usia, dan tangkapan layar asli. Jelaskan kontrol driver kamera yang diperlukan. Jalankan pemeriksaan sertifikasi aplikasi Windows saat ini.
- [ ] Unggah kandidat ke Partner Center, perbaiki masalah validasi, kirimkan dan tunggu sertifikasi. Jangan mendeskripsikan aplikasi sebagai disetujui Toko sebelum disetujui.

```powershell
./scripts/build-msix.ps1 -IdentityName 'YOUR.PRODUCT.IDENTITY' -Publisher 'CN=YOUR-PARTNER-CENTER-ID' -PublisherDisplayName 'YOUR DISPLAY NAME'
```

Outputnya adalah kandidat pengiriman Toko yang tidak ditandatangani, bukan paket sideload publik yang siap dipasang. Skrip memvalidasi bidang identitas yang diperlukan dan menggunakan build x64 eksplisit. Pilih versi paket empat bagian baru untuk setiap rilis, dengan bagian akhir nol untuk pengiriman Store.

Referensi resmi, diperiksa 18 November 2026:

- [Opsi penandatanganan kode Microsoft](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options)
- [Daftar periksa kesiapan toko](https://learn.microsoft.com/en-us/windows/apps/publish/store-readiness-checklist)
- [MSIX panduan penandatanganan](https://learn.microsoft.com/en-us/windows/msix/package/sign-msix-package-guide)

Lihat juga [SignPath](SIGNPATH.md). Aturan kelayakan dan penyerahan saat ini harus diperiksa kembali sebelum dipublikasikan.
