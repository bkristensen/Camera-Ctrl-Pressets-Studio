# بناء وإطلاق Camera Control Studio

* مكتوب لـ Camera Control Studio 1.0.1.*

قم بتشغيل الأوامر من الدليل الذي يحتوي على `Camera-Control-Pressets-Studio.sln`. استخدم Windows وPowerShell لبرنامج التثبيت واختبار Windows.

## 1. قم بإعداد الإصدار

1. ابدأ من الالتزام المحدد للإصدار. قم بإغلاق أي مثيل Camera Control Studio قيد التشغيل قبل تجميع الترقية أو اختبارها.
2. قم بتثبيت .NET 8 SDK المحدد بواسطة `global.json`. قم بتثبيت Inno Setup 6 أو 7 إذا كنت تخطط لنشر برنامج التثبيت. قم بتثبيت Windows SDK إذا كنت تخطط لتوقيع الملفات باستخدام SignTool أو إنشاء MSIX باستخدام MakeAppx.
3. تحقق من المتطلبات الأساسية باستخدام `./scripts/setup-dev.ps1 -CheckOnly`. بالنسبة لمحطة عمل جديدة، يقوم `./scripts/setup-dev.ps1` باستعادة الحزم المثبتة وتشغيل البنية المحمولة.
4. قم بتعيين إصدار الإصدار في `Directory.Build.props` وقم بتحديث `CHANGELOG.md`. يقرأ البرنامج النصي للمثبت الإصدار الافتراضي من خصائص البناء. احتفظ بنسخة المتجر المكونة من أربعة أجزاء منفصلة عند إعداد MSIX.

## 2. قم ببناء حزم GitHub

اختر أمرًا واحدًا. تتضمن بنية المثبت البنية المحمولة.

| ماذا نبني | القيادة | الإخراج |
| --- | --- | --- |
| محمول ZIP | `./scripts/build.ps1` | `artifacts/Camera-Control-Studio-portable.zip` |
| مثبت ZIP المحمول وInno | `./scripts/build-installer.ps1` | ZIP المحمول بالإضافة إلى `artifacts/installers/CamCtrlStudio-Setup-<version>.exe` |

يتحقق `build.ps1` من الترجمات، ويبني الإصدار باستخدام التبعيات المقفلة، ويجري الاختبارات وينشئ ZIP. ابحث عن الملف القابل للتنفيذ في `CameraControlStudio/bin/Release/net48` والملفات المرحلية في `artifacts/portable/Camera-Control-Studio`. نشر ZIP بالكامل. يحتاج التطبيق إلى التكوين والمكتبات والأدلة الخاصة به.

لاستخدام شهادة توقيع التعليمات البرمجية الفردية أو المؤسسة المتوفرة في مخزن شهادات Windows الخاص بالمستخدم الحالي، قم بتمرير بصمة الإبهام والمسار إلى SignTool عند الإنشاء. يقوم أمر المثبت بتوقيع التطبيق قبل تعبئته ويوقع على المثبت المكتمل بطابع زمني.

```powershell
$thumbprint = '<certificate thumbprint>'
$signtool = '<full path to signtool.exe>'
./scripts/build-installer.ps1 -CertificateThumbprint $thumbprint -SignToolPath $signtool
```

بالنسبة إلى ZIP وحده، قم بإعطاء نفس المعلمات إلى `build.ps1`. للتوقيع المدعوم، اتبع سير عمل [SignPath المعتمد ](SIGNPATH.md). قم بتمييز التنزيلات غير الموقعة بشكل واضح.

## 3. التحقق من الحزم على نظام التشغيل Windows

1. قم باستخراج ZIP إلى دليل جديد. ابدأ تشغيل التطبيق من هناك وافتح **؟** مع إغلاق الإعدادات وفتحها. مع تثبيت [Microsoft Edge WebView2 Runtime](https://developer.microsoft.com/microsoft-edge/webview2/)، تحقق من التنقل في الدليل واطبع إلى PDF للتحقق من العنوان وتاريخ الطباعة ووقتها والإصدار المثبت وعنوان المشروع وأرقام الصفحات. تحقق من إصدار التطبيق، وسلوك التشغيل، والإعدادات المسبقة، وعناصر التحكم في الكاميرا، واختيار اللغة عند تبديل ملفات التعريف.
2. إذا كنت تخطط لنشر برنامج التثبيت، فقم بتثبيته على حساب اختبار Windows نظيف. تحقق من تطبيق قائمة ابدأ واختصارات دليل المستخدم. تأكد من وجود جميع الأدلة الـ 12، ومن أن الترقية تحتفظ بالملفات التعريفية الموجودة، ومن اكتمال عملية إلغاء التثبيت.
3. اعمل من خلال قائمة مراجعة الإصدار [](RELEASE-CHECKLIST.md). بالنسبة للإصدارات الموقعة، قم بتشغيل التحقق من SignTool على التطبيق القابل للتنفيذ والمثبت. لا يتم إنشاء التجزئة إلا بعد الانتهاء من التوقيع وإعادة التعبئة.

## 4. قم بتنظيم وتجزئة تنزيلات GitHub

استبدل الإصدار الموجود في هذا المثال بالإصدارات اللاحقة. احذف سطور التثبيت لإصدار ZIP فقط.

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

يحتوي المجلد الذي تم إصداره على ملف ZIP النهائي والمثبت وبيان SHA-256. يعد `bin` وأدلة التدريج المحمولة بمثابة مدخلات بناء لهذه الخطوة.

## 5. انشر على GitHub

1. قم بتحديث العنوان الفرعي للإصدار في كل دليل تقوم بمراجعته لهذا الإصدار. حافظ على مزامنة ترجماتها. تنفيذ إصدار الإصدار وسجل التغيير والأدلة وتغييرات المصدر. في [GitHub Releases](https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases)، قم بإنشاء مسودة واستخدم علامة مثل `v1.0.0` في الالتزام الذي تم اختباره.
2. انسخ إدخال سجل التغيير المطابق في ملاحظات الإصدار، وحدد ما إذا كانت التنزيلات موقعة، وحدد خيار الإصدار التجريبي فقط لإصدارات المعاينة.
3. قم بإرفاق الإصدار ZIP، وبرنامج التثبيت إذا كان مدمجًا، و`SHA256SUMS.txt` من `artifacts/releases/<version>/`. قم بمراجعة أسماء الملفات، وقم بتنزيل الملفات النهائية والتحقق منها، ثم قم بالنشر.

## مسارات بناء اختيارية

| الغرض | البرنامج النصي | الإخراج |
| --- | --- | --- |
| لقطات شاشة التوثيق مع بيانات العينة | `./scripts/capture-docs.ps1` | صور PNG في `docs/images/screenshots/<version>/<language>/` لجميع أدلة المستخدم الـ 12 |
| بناء عامل ميناء واختبارات المنطق | `./scripts/build-docker.ps1` | ZIP المحمولة في `artifacts/docker/` |
| مرشح تقديم متجر Microsoft | `./scripts/build-msix.ps1` مع هوية Partner Center والناشر وإصدار الحزمة | `artifacts/Camera-Control-Studio-<package-version>-x64.msix` |

يتطلب إنشاء المتجر هوية Partner Center الحقيقية ويتبع قائمة [MSIX المنفصلة وقائمة التحقق من المتجر ](MSIX-AND-STORE.md). الإخراج هو مرشح تقديم غير موقعة. `Install.ps1` هو برنامج التثبيت الاختياري لكل مستخدم داخل ZIP المحمول المستخرج.
