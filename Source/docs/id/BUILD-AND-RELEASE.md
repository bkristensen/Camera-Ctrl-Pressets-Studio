# Bangun dan rilis Camera Control Studio

*Ditulis untuk Camera Control Studio 1.1.0.*

Jalankan perintah dari direktori yang berisi `Camera-Control-Pressets-Studio.sln`. Gunakan Windows dan PowerShell untuk penginstal dan pengujian Windows.

## 1. Siapkan rilisnya

1. Mulai dari komit yang dipilih untuk rilis. Tutup semua instans Camera Control Studio yang berjalan sebelum mengkompilasi atau menguji peningkatan.
2. Instal .NET 8 SDK yang dipilih oleh `global.json`. Instal Inno Setup 6 atau 7 jika Anda berencana untuk mempublikasikan penginstal. Instal Windows SDK jika Anda berencana menandatangani file dengan SignTool atau membuat MSIX dengan MakeAppx.
3. Periksa prasyarat dengan `./scripts/setup-dev.ps1 -CheckOnly`. Untuk workstation baru, `./scripts/setup-dev.ps1` memulihkan paket yang disematkan dan menjalankan build portabel.
4. Atur versi rilis di `Directory.Build.props` dan perbarui `CHANGELOG.md`. Skrip penginstal membaca versi default dari properti build. Pisahkan versi Store empat bagian saat menyiapkan MSIX.

## 2. Bangun paket GitHub

Pilih satu perintah. Versi penginstal mencakup versi portabel.

| Apa yang harus dibangun | Perintah | Keluaran |
| --- | --- | --- |
| ZIP portabel | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| ZIP portabel dan penginstal Inno | `./scripts/build-installer.ps1` | ZIP portabel ditambah `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` memeriksa terjemahan, membuat Rilis dengan dependensi terkunci, menjalankan pengujian, dan membuat ZIP. Temukan file yang dapat dieksekusi di `CameraControlStudio/bin/Release/net48` dan file yang dipentaskan di `artifacts/portable/Camera-Control-Studio`. Publikasikan seluruh ZIP. Aplikasi ini memerlukan konfigurasi, perpustakaan, dan panduannya.

Untuk menggunakan sertifikat penandatanganan kode individu atau organisasi yang tersedia di penyimpanan sertifikat Windows pengguna saat ini, teruskan cap jempolnya dan jalur ke SignTool saat membuat. Perintah penginstal menandatangani aplikasi sebelum mengemasnya dan menandatangani penginstal yang telah selesai dengan stempel waktu.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

Untuk ZIP saja, berikan parameter yang sama pada `build.ps1`. Untuk penandatanganan bersponsor, ikuti alur kerja [SignPath](SIGNPATH.md) yang disetujui. Tandai unduhan yang tidak ditandatangani dengan jelas.

## 3. Periksa paket di Windows

1. Ekstrak ZIP ke direktori baru. Mulai aplikasi dari sana dan buka **?** dengan Pengaturan tertutup dan terbuka. Dengan [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) terinstal, verifikasi navigasi panduan dan cetak ke PDF untuk memeriksa judul, tanggal dan waktu pencetakan, versi terinstal, alamat proyek, dan nomor halaman. Verifikasi versi aplikasi, perilaku peluncuran, preset, kontrol kamera, dan pemilihan bahasa saat berpindah profil.
2. Jika Anda berencana menerbitkan penginstal, instal pada akun pengujian Windows yang bersih. Verifikasi aplikasi menu Mulai dan pintasan panduan pengguna. Periksa apakah ke-12 panduan ada, apakah pemutakhiran mempertahankan profil yang ada, dan pencopotan pemasangan selesai.
3. Kerjakan melalui daftar periksa rilis [](RELEASE-CHECKLIST.md). Untuk build yang ditandatangani, jalankan verifikasi SignTool pada aplikasi yang dapat dieksekusi dan penginstal. Hasilkan hash hanya setelah penandatanganan dan pengemasan ulang selesai.

## 4. Tahapan dan hash unduhan GitHub

Ganti versi dalam contoh ini untuk rilis berikutnya. Hilangkan baris penginstal untuk rilis khusus ZIP.

```powershell
$version = '1.0.0'
$release = "artifacts/releases/$version"
New-Item -ItemType Directory -Path $release -Force | Out-Null

$zip = "Camera-Control-Studio-$version-portable.zip"
$installer = "CamCtrlStudio-Setup-$version.exe"
Copy-Item 'artifacts/Camera-Control-Studio-portable.zip' (Join-Path $release $zip)
Copy-Item "artifacts/installers/$installer" $release
@($zip, $installer) | ForEach-Object {
    $hash = Get-FileHash -LiteralPath (Join-Path $release $_) -Algorithm SHA256
    '{0}  {1}' -f $hash.Hash.ToLowerInvariant(), $_
} | Set-Content -LiteralPath (Join-Path $release 'SHA256SUMS.txt') -Encoding ascii
```

Folder berversi berisi ZIP terakhir, penginstal, dan manifes SHA-256. `bin` dan direktori pementasan portabel merupakan masukan build untuk langkah ini.

## 5. Publikasikan di GitHub

1. Perbarui subtitle versi di setiap panduan yang Anda ulas untuk rilis ini. Jaga agar terjemahannya tetap sinkron. Komit versi rilis, log perubahan, panduan, dan perubahan sumber. Di [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases), buat draf dan gunakan tag seperti `v1.0.0` pada penerapan yang diuji.
2. Salin entri log perubahan yang cocok ke dalam catatan rilis, nyatakan apakah unduhan telah ditandatangani, dan pilih opsi prarilis hanya untuk versi pratinjau.
3. Lampirkan ZIP yang berversi, penginstal jika dibuat, dan `SHA256SUMS.txt` dari `artifacts/releases/<version>/`. Tinjau nama file, unduh dan verifikasi file yang sudah selesai, lalu publikasikan.

## Jalur pembangunan opsional

| Tujuan | naskah | Keluaran |
| --- | --- | --- |
| Tangkapan layar dokumentasi dengan data sampel | `./scripts/capture-docs.ps1` | PNG dalam `docs/images/screenshots/<version>/<language>/` untuk 12 panduan pengguna |
| Pembuatan Docker dan pengujian logika | `./scripts/build-docker.ps1` | ZIP portabel di `artifacts/docker/` |
| Kandidat pengiriman Microsoft Store | `./scripts/build-msix.ps1` dengan identitas Partner Center Anda, penerbit dan versi paket | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

Pembuatan Toko memerlukan identitas Partner Center asli dan mengikuti [MSIX dan daftar periksa Toko](MSIX-AND-STORE.md) yang terpisah. Outputnya adalah kandidat penyerahan yang tidak ditandatangani. `Install.ps1` adalah penginstal opsional per pengguna di dalam ZIP portabel yang diekstrak.
