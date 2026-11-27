# Camera Control Studio তৈরি করুন এবং প্রকাশ করুন

* Camera Control Studio 1.0.1.* এর জন্য লেখা

`Camera-Control-Pressets-Studio.sln` ধারণকারী ডিরেক্টরি থেকে কমান্ড চালান। ইনস্টলার এবং উইন্ডোজ পরীক্ষার জন্য Windows এবং PowerShell ব্যবহার করুন।

## 1. রিলিজ প্রস্তুত করুন

1. মুক্তির জন্য নির্বাচিত প্রতিশ্রুতি থেকে শুরু করুন। একটি আপগ্রেড সংকলন বা পরীক্ষা করার আগে চলমান Camera Control Studio উদাহরণ বন্ধ করুন।
2. `global.json` দ্বারা নির্বাচিত .NET 8 SDK ইনস্টল করুন৷ আপনি যদি ইনস্টলার প্রকাশ করার পরিকল্পনা করেন তবে Inno Setup 6 বা 7 ইনস্টল করুন৷ আপনি যদি SignTool দিয়ে ফাইল সাইন করার পরিকল্পনা করেন বা MakeAppx দিয়ে একটি MSIX তৈরি করার পরিকল্পনা করেন তাহলে Windows SDK ইনস্টল করুন৷
3. `./scripts/setup-dev.ps1 -CheckOnly` এর সাথে পূর্বশর্তগুলি পরীক্ষা করুন৷ একটি নতুন ওয়ার্কস্টেশনের জন্য, `./scripts/setup-dev.ps1` পিন করা প্যাকেজগুলি পুনরুদ্ধার করে এবং পোর্টেবল বিল্ড চালায়।
4. `Directory.Build.props`-এ রিলিজ সংস্করণ সেট করুন এবং `CHANGELOG.md` আপডেট করুন। ইনস্টলার স্ক্রিপ্ট বিল্ড বৈশিষ্ট্য থেকে ডিফল্ট সংস্করণ পড়ে। MSIX প্রস্তুত করার সময় চার-অংশের স্টোর সংস্করণটি আলাদা রাখুন।

## 2. GitHub প্যাকেজ তৈরি করুন

একটি কমান্ড নির্বাচন করুন. ইনস্টলার বিল্ড পোর্টেবল বিল্ড অন্তর্ভুক্ত.

| কি নির্মাণ করতে হবে | আদেশ | আউটপুট |
| --- | --- | --- |
| পোর্টেবল ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| পোর্টেবল ZIP এবং ইনো ইনস্টলার | `./scripts/build-installer.ps1` | পোর্টেবল ZIP প্লাস `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

`build.ps1` অনুবাদগুলি পরীক্ষা করে, লক করা নির্ভরতা সহ রিলিজ তৈরি করে, পরীক্ষা চালায় এবং ZIP তৈরি করে। `CameraControlStudio/bin/Release/net48`-এ এক্সিকিউটেবল এবং `artifacts/portable/Camera-Control-Studio`-এ স্টেজ করা ফাইলগুলি খুঁজুন। পুরো ZIP প্রকাশ করুন। অ্যাপটির কনফিগারেশন, লাইব্রেরি এবং গাইড প্রয়োজন।

বর্তমান ব্যবহারকারীর Windows সার্টিফিকেট স্টোরে উপলব্ধ একটি ব্যক্তি বা সংস্থার কোড-সাইনিং শংসাপত্র ব্যবহার করতে, নির্মাণের সময় এর থাম্বপ্রিন্ট এবং SignTool এর পথটি পাস করুন৷ ইনস্টলার কমান্ডটি প্যাক করার আগে অ্যাপ্লিকেশনটিতে স্বাক্ষর করে এবং একটি টাইমস্ট্যাম্প সহ সম্পূর্ণ ইনস্টলারকে স্বাক্ষর করে।

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

একা ZIP-এর জন্য, `build.ps1`-এ একই পরামিতি দিন। স্পনসরড সাইনিংয়ের জন্য, অনুমোদিত [SignPath workflow](SIGNPATH.md) অনুসরণ করুন। স্বাক্ষরবিহীন ডাউনলোডগুলি পরিষ্কারভাবে চিহ্নিত করুন।

## 3. Windows এ প্যাকেজ চেক করুন

1. একটি নতুন ডিরেক্টরিতে ZIP বের করুন। সেখান থেকে অ্যাপটি শুরু করুন এবং সেটিংস বন্ধ এবং খোলার সাথে **?** খুলুন। [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/) ইনস্টল করে, গাইড নেভিগেশন যাচাই করুন এবং শিরোনাম, মুদ্রণের তারিখ এবং সময়, ইনস্টল করা সংস্করণ, প্রকল্পের ঠিকানা এবং পৃষ্ঠা নম্বরগুলি পরীক্ষা করতে PDF এ মুদ্রণ করুন। প্রোফাইল স্যুইচ করার সময় অ্যাপ সংস্করণ, লঞ্চ আচরণ, প্রিসেট, ক্যামেরা নিয়ন্ত্রণ এবং ভাষা নির্বাচন যাচাই করুন।
2. আপনি যদি ইনস্টলারটি প্রকাশ করার পরিকল্পনা করেন তবে এটি একটি পরিষ্কার Windows পরীক্ষা অ্যাকাউন্টে ইনস্টল করুন। স্টার্ট মেনু অ্যাপ এবং ব্যবহারকারী-গাইড শর্টকাট যাচাই করুন। পরীক্ষা করুন যে সমস্ত 12টি গাইড উপস্থিত রয়েছে, একটি আপগ্রেড বিদ্যমান প্রোফাইলগুলিকে ধরে রাখে এবং এটি আনইনস্টল সম্পূর্ণ হয়।
3. [ রিলিজ চেকলিস্ট](RELEASE-CHECKLIST.md) এর মাধ্যমে কাজ করুন। স্বাক্ষরিত বিল্ডগুলির জন্য, অ্যাপ এক্সিকিউটেবল এবং ইনস্টলারে SignTool যাচাইকরণ চালান। যেকোনো সাইনিং এবং রিপ্যাকেজিং শেষ হলেই হ্যাশ তৈরি করুন।

## 4. GitHub ডাউনলোডগুলি স্টেজ করুন এবং হ্যাশ করুন৷

পরবর্তী রিলিজের জন্য এই উদাহরণে সংস্করণটি প্রতিস্থাপন করুন। শুধুমাত্র ZIP-এর জন্য ইনস্টলার লাইনগুলি বাদ দিন৷

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

সংস্করণযুক্ত ফোল্ডারে চূড়ান্ত ZIP, ইনস্টলার এবং SHA-256 ম্যানিফেস্ট রয়েছে। `bin` এবং পোর্টেবল স্টেজিং ডিরেক্টরিগুলি এই ধাপের জন্য বিল্ড ইনপুট।

## 5. GitHub এ প্রকাশ করুন৷

1. এই রিলিজের জন্য আপনি পর্যালোচনা করা প্রতিটি গাইডের সংস্করণের সাবটাইটেল আপডেট করুন। এর অনুবাদগুলিকে সিঙ্কে রাখুন। রিলিজ সংস্করণ, চেঞ্জলগ, গাইড এবং উৎস পরিবর্তনের প্রতিশ্রুতি দিন। [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases)-এ, একটি খসড়া তৈরি করুন এবং পরীক্ষিত প্রতিশ্রুতিতে `v1.0.0` এর মতো ট্যাগ ব্যবহার করুন।
2. রিলিজ নোটগুলিতে ম্যাচিং চেঞ্জলগ এন্ট্রিটি অনুলিপি করুন, ডাউনলোডগুলি স্বাক্ষরিত কিনা তা উল্লেখ করুন এবং শুধুমাত্র পূর্বরূপ বিল্ডগুলির জন্য প্রিরিলিজ বিকল্পটি নির্বাচন করুন৷
3. সংস্করণযুক্ত ZIP, নির্মিত হলে ইনস্টলার এবং `artifacts/releases/<version>/` থেকে `SHA256SUMS.txt` সংযুক্ত করুন। ফাইলের নামগুলি পর্যালোচনা করুন, সমাপ্ত ফাইলগুলি ডাউনলোড এবং যাচাই করুন, তারপর প্রকাশ করুন৷

## ঐচ্ছিক বিল্ড পাথ

| উদ্দেশ্য | স্ক্রিপ্ট | আউটপুট |
| --- | --- | --- |
| নমুনা ডেটা সহ ডকুমেন্টেশন স্ক্রিনশট | `./scripts/capture-docs.ps1` | `docs/images/screenshots/<version>/<language>/`-এ PNGs সব 12টি ব্যবহারকারীর গাইডের জন্য |
| ডকার বিল্ড এবং লজিক পরীক্ষা | `./scripts/build-docker.ps1` | `artifacts/docker/` এ পোর্টেবল ZIP |
| মাইক্রোসফট স্টোর জমা প্রার্থী | আপনার Partner Center পরিচয়, প্রকাশক এবং প্যাকেজ সংস্করণ সহ `./scripts/build-msix.ps1` | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

স্টোর বিল্ডের জন্য আসল Partner Center পরিচয় প্রয়োজন এবং আলাদা [MSIX এবং স্টোর চেকলিস্ট](MSIX-AND-STORE.md) অনুসরণ করে। আউটপুট একটি স্বাক্ষরবিহীন জমা প্রার্থী. `Install.ps1` হল একটি নিষ্কাশিত পোর্টেবল ZIP-এর ভিতরে ঐচ্ছিক প্রতি-ব্যবহারকারী ইনস্টলার।
