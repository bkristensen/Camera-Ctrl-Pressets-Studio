# Validering for 0.7.0-beta.3

*Skrevet til Camera Control Studio 1.1.1.*

Windows Release build bruger låste afhængigheder. Oversættelsesparitet og de automatiserede tests bestået. Testene dækker bevægelse, forudindstillinger, profilsprog, vedholdenhed, logning og det kamera-ikke-klare HRESULT rapporteret efter dvaletilstand.

Den ekstraherede bærbare ZIP blev testet i x86 og x64 Windows-processer. Både åbnede og genåbnede den daglige brug og opsætningsvejledninger, viste deres skærmbilleder og udskrev til PDF. x64 PDF havde titel, version, dato og korrekt sideantal på alle fem sider. **?**-knappen var til stede under WinForms-designtidsinitialisering. ARM64-indlæseren blev tjekket for den korrekte binære arkitektur.

Den engelske standardprofil blev optaget med et demokamera. De 12 skærmbilleder i fuld vindue dækker både hovedvinduets tilstande, hver indstillingsfane og hver underfane. De lokale links i vejledningerne og README'erne blev kontrolleret i forhold til de pakkede dokumentstier.

## Brio synsfelt og zoom

Brug en fysisk Brio, der tilbyder 65°, 78° og 90°. Bemærk modellen, firmwaren, driveren, appversionerne og hvilken videotilstand Zoom bruger.

1. Start Zoom og brug en fast scene med synlige referencepunkter nær kanterne. Bekræft den bredeste indramning ved 90° og minimum zoom ved hjælp af Logitech Brio Control.
2. Start Camera Control Studio med **Gør ingenting** og bekræft, at enheden beholder sin FOV. Genstart med **Bredest muligt**, og gentag derefter med en specificeret FOV og med streamingbekræftelse aktiveret.
3. Med alle tre apps kørende, skift panorering, tilt, zoom og FOV i Logitech Brio Control. Bekræft de opdaterede aflæsninger i Camera Control Studio med manual FOV deaktiveret og aktiveret.
4. Skift FOV i Camera Control Studio. Observer, når Logitech Brio Control opdaterer sin egen skærm. Gem og genkald forudindstillinger for hver FOV, inklusive en ældre forudindstilling uden FOV.
5. Fra et smalt, zoomet billede skal du vælge **Fuldt billede** og sammenligne billedet med referencen fra trin 1. Gentag under en forudindstillet overgang og med Logitech Brio Control allerede åben.
6. Luk Camera Control Studio og kontroller, om Logitech Brio Control stadig kan ændre kameraet. Gentag i omvendt applikationsstartrækkefølge. Vedhæft den diagnostiske log og noter enhver uoverensstemmelse mellem det levende billede og de rapporterede værdier.
7. Tryk på Zoom ud én gang ved 65° og minimum zoom. Tjek, at FOV ændres til 78°, og det næste trin zoomer ud. Gentag ved 78°. Tjek, at Zoom ud kun stopper ved 90° og minimum zoom. Test den omvendte vej for Zoom ind.
8. Ved en vandret panoreringskant skal du trykke på den samme pil igen. Tjek, om en bredere FOV og en zoomjustering tillader bevægelse uden at miste billedet. Hvis ingen visning hjælper, skal du kontrollere, at den oprindelige indramning vender tilbage. Sluk for funktionen, og bekræft, at FOV bliver siddende.

Inno Setup 7 kompilerede installationsprogrammet med guiderne til alle 12 sprog og x86, x64 og ARM64 loaderne. Begge pakker er usignerede, med SHA-256 hashes leveret. En ren-konto installation og afinstallation, en fysisk kamera dvale cyklus, og en ARM64 runtime test afventer. Se [-release-tjeklisten](RELEASE-CHECKLIST.md) og [build and release guide](BUILD-AND-RELEASE.md).
