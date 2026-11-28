# Camera Control Studio بنائیں اور جاری کریں۔

* Camera Control Studio 1.1.1.* کے لیے لکھا گیا

`Camera-Control-Pressets-Studio.sln` پر مشتمل ڈائریکٹری سے کمانڈز چلائیں۔ انسٹالر اور ونڈوز ٹیسٹنگ کے لیے Windows اور PowerShell استعمال کریں۔

## 1. ریلیز تیار کریں۔

1. رہائی کے لیے منتخب کردہ عہد سے شروع کریں۔ اپ گریڈ کو مرتب کرنے یا جانچنے سے پہلے چلنے والی Camera Control Studio مثال کو بند کریں۔
2. `global.json` کے ذریعے منتخب کردہ .NET 8 SDK انسٹال کریں۔ اگر آپ انسٹالر کو شائع کرنے کا ارادہ رکھتے ہیں تو Inno Setup 6 یا 7 انسٹال کریں۔ اگر آپ SignTool کے ساتھ فائلوں پر دستخط کرنے یا MakeAppx کے ساتھ MSIX بنانے کا ارادہ رکھتے ہیں تو ونڈوز SDK انسٹال کریں۔
3. `./scripts/setup-dev.ps1 -CheckOnly` کے ساتھ شرائط کو چیک کریں۔ ایک نئے ورک سٹیشن کے لیے، `./scripts/setup-dev.ps1` پن کیے ہوئے پیکجز کو بحال کرتا ہے اور پورٹیبل بلڈ کو چلاتا ہے۔
4. `Directory.Build.props` میں ریلیز ورژن سیٹ کریں اور `CHANGELOG.md` کو اپ ڈیٹ کریں۔ انسٹالر اسکرپٹ بلڈ پراپرٹیز سے ڈیفالٹ ورژن پڑھتا ہے۔ MSIX تیار کرتے وقت چار حصوں والے اسٹور ورژن کو الگ رکھیں۔

## 2. GitHub پیکجز بنائیں

ایک کمانڈ کا انتخاب کریں۔ انسٹالر کی تعمیر میں پورٹیبل تعمیر شامل ہے۔

| کیا تعمیر کرنا ہے | حکم | آؤٹ پٹ |
| --- | --- | --- |
| پورٹ ایبل ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| پورٹ ایبل ZIP اور Inno انسٹالر | `./scripts/build-installer.ps1` | پورٹیبل ZIP پلس `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` ترجمے کو چیک کرتا ہے، مقفل انحصار کے ساتھ ریلیز بناتا ہے، ٹیسٹ چلاتا ہے اور ZIP تخلیق کرتا ہے۔ `CameraControlStudio/bin/Release/net48` میں ایگزیکیوٹیبل اور `artifacts/portable/Camera-Control-Studio` میں اسٹیجڈ فائلز تلاش کریں۔ پورا ZIP شائع کریں۔ ایپ کو اپنی کنفیگریشن، لائبریریوں اور گائیڈز کی ضرورت ہے۔

موجودہ صارف کے ونڈوز سرٹیفکیٹ اسٹور میں دستیاب فرد یا تنظیم کوڈ پر دستخط کرنے والا سرٹیفکیٹ استعمال کرنے کے لیے، تعمیر کرتے وقت اس کے انگوٹھے کا نشان اور SignTool کا راستہ پاس کریں۔ انسٹالر کمانڈ ایپلیکیشن کو پیک کرنے سے پہلے اس پر دستخط کرتا ہے اور مکمل انسٹالر پر ٹائم اسٹیمپ کے ساتھ دستخط کرتا ہے۔

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

اکیلے ZIP کے لیے، وہی پیرامیٹرز `build.ps1` کو دیں۔ سپانسر شدہ دستخط کے لیے، منظور شدہ [SignPath workflow](SIGNPATH.md) کی پیروی کریں۔ غیر دستخط شدہ ڈاؤن لوڈز کو واضح طور پر نشان زد کریں۔

## 3. ونڈوز پر پیکجز کو چیک کریں۔

1. ZIP کو ایک نئی ڈائریکٹری میں نکالیں۔ وہاں سے ایپ شروع کریں اور **?** سیٹنگز کو بند اور کھلا کھولیں۔ [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) انسٹال ہونے کے ساتھ، گائیڈ نیویگیشن کی تصدیق کریں اور عنوان، پرنٹ کی تاریخ اور وقت، انسٹال شدہ ورژن، پروجیکٹ کا پتہ اور صفحہ نمبر چیک کرنے کے لیے PDF پر پرنٹ کریں۔ پروفائلز سوئچ کرتے وقت ایپ ورژن، لانچ رویے، پیش سیٹس، کیمرہ کنٹرولز اور زبان کے انتخاب کی تصدیق کریں۔
2. اگر آپ انسٹالر کو شائع کرنے کا ارادہ رکھتے ہیں، تو اسے صاف ونڈوز ٹیسٹ اکاؤنٹ پر انسٹال کریں۔ اسٹارٹ مینو ایپ اور صارف گائیڈ شارٹ کٹس کی تصدیق کریں۔ چیک کریں کہ تمام 12 گائیڈز موجود ہیں، کہ ایک اپ گریڈ موجودہ پروفائلز کو برقرار رکھتا ہے، اور یہ ان انسٹال مکمل ہو جاتا ہے۔
3. [ ریلیز چیک لسٹ](RELEASE-CHECKLIST.md) کے ذریعے کام کریں۔ دستخط شدہ تعمیرات کے لیے، ایگزیکیوٹیبل ایپ اور انسٹالر پر SignTool تصدیق چلائیں۔ کسی بھی دستخط اور دوبارہ پیکجنگ ختم ہونے کے بعد ہی ہیشز بنائیں۔

## 4. GitHub ڈاؤن لوڈز کو اسٹیج اور ہیش کریں۔

اس مثال میں ورژن کو بعد میں ریلیز کرنے کے لیے تبدیل کریں۔ ZIP صرف ریلیز کے لیے انسٹالر لائنوں کو چھوڑ دیں۔

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

ورژن والا فولڈر حتمی ZIP، انسٹالر اور SHA-256 مینی فیسٹ رکھتا ہے۔ `bin` اور پورٹیبل اسٹیجنگ ڈائرکٹری اس قدم کے لیے بلٹ ان پٹ ہیں۔

## 5. GitHub پر شائع کریں۔

1. اس ریلیز کے لیے آپ جس گائیڈ کا جائزہ لیتے ہیں اس میں ورژن سب ٹائٹل کو اپ ڈیٹ کریں۔ اس کے تراجم کو مطابقت پذیر رکھیں۔ ریلیز ورژن، چینج لاگ، گائیڈز اور سورس کی تبدیلیوں کا ارتکاب کریں۔ [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases) میں، ایک ڈرافٹ بنائیں اور ٹیسٹ شدہ کمٹ پر `v1.0.0` جیسا ٹیگ استعمال کریں۔
2. ریلیز نوٹس میں مماثل چینج لاگ انٹری کو کاپی کریں، یہ بتائیں کہ آیا ڈاؤن لوڈز پر دستخط ہیں، اور صرف پیش نظارہ کی تعمیر کے لیے پری ریلیز کا اختیار منتخب کریں۔
3. ورژن شدہ ZIP، انسٹالر اگر بنایا گیا ہو، اور `artifacts/releases/<version>/` سے `SHA256SUMS.txt` منسلک کریں۔ فائل ناموں کا جائزہ لیں، مکمل فائلوں کو ڈاؤن لوڈ اور تصدیق کریں، پھر شائع کریں۔

## اختیاری تعمیراتی راستے

| مقصد | اسکرپٹ | آؤٹ پٹ |
| --- | --- | --- |
| نمونہ ڈیٹا کے ساتھ دستاویزی اسکرین شاٹس | `./scripts/capture-docs.ps1` | تمام 12 صارف گائیڈز کے لیے `docs/images/screenshots/<version>/<language>/` میں PNGs |
| ڈوکر کی تعمیر اور منطق کے ٹیسٹ | `./scripts/build-docker.ps1` | `artifacts/docker/` میں پورٹیبل ZIP |
| مائیکروسافٹ اسٹور جمع کرانے کا امیدوار | `./scripts/build-msix.ps1` آپ کی Partner Center شناخت، پبلشر اور پیکیج ورژن کے ساتھ | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

اسٹور کی تعمیر کے لیے حقیقی Partner Center شناخت کی ضرورت ہوتی ہے اور علیحدہ [MSIX اور اسٹور چیک لسٹ](MSIX-AND-STORE.md) کی پیروی کرتا ہے۔ آؤٹ پٹ ایک غیر دستخط شدہ جمع کرانے والا امیدوار ہے۔ `Install.ps1` ایک ایکسٹریکٹ شدہ پورٹیبل ZIP کے اندر اختیاری فی صارف انسٹالر ہے۔
