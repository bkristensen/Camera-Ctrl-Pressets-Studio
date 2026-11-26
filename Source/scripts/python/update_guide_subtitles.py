from argparse import ArgumentParser
from pathlib import Path
from xml.etree import ElementTree
import re


LABELS = {
    "ar": "كُتب هذا الدليل للإصدار {0} من Camera Control Studio.",
    "bn": "এই নির্দেশিকা Camera Control Studio {0} সংস্করণের জন্য লেখা।",
    "da": "Skrevet til Camera Control Studio {0}.",
    "de": "Geschrieben für Camera Control Studio {0}.",
    "en": "Written for Camera Control Studio {0}.",
    "es": "Escrita para Camera Control Studio {0}.",
    "fr": "Rédigé pour Camera Control Studio {0}.",
    "hi": "यह गाइड Camera Control Studio {0} के लिए लिखी गई है।",
    "id": "Ditulis untuk Camera Control Studio {0}.",
    "pt": "Guia escrito para Camera Control Studio {0}.",
    "ur": "یہ رہنما Camera Control Studio {0} کے لیے لکھا گیا ہے۔",
    "zh": "本指南适用于 Camera Control Studio {0}。",
}
GUIDE_NAMES = {
    "SETUP.md", "DAILY-USE.md", "USER-GUIDE.md", "TROUBLESHOOTING.md",
    "DEVELOPMENT.md", "BUILD-AND-RELEASE.md", "TRANSLATIONS.md", "GITHUB.md", "ISSUES.md",
}
VERSION = r"[0-9]+\.[0-9]+\.[0-9]+(?:-[0-9A-Za-z.-]+)?"
TITLE = re.compile(br"(?m)^(?:\xef\xbb\xbf)?# [^\r\n]+\r?\n")


def guide_subtitle(data, label, version):
    newline = b"\r\n" if b"\r\n" in data else b"\n"
    title = TITLE.search(data)
    if title is None:
        raise ValueError("The guide has no Markdown title.")
    subtitle = ("*" + label.format(version) + "*").encode("utf-8")
    after_title = data[title.end():]
    old_label = re.escape(("*" + label + "*").encode("utf-8")).replace(br"\{0\}", VERSION.encode())
    existing = re.match(re.escape(newline) + old_label + re.escape(newline), after_title)
    if existing:
        after_title = after_title[existing.end():]
    return data[:title.end()] + newline + subtitle + newline + after_title


def main():
    parser = ArgumentParser(description="Check or update the version subtitle in Camera Control Studio guides.")
    parser.add_argument("guides", nargs="*", help="Guide paths relative to Source/docs")
    parser.add_argument("--all", action="store_true", help="Select every guide")
    parser.add_argument("--check", action="store_true", help="Report differences without editing")
    args = parser.parse_args()
    if args.all == bool(args.guides):
        parser.error("Use --all or provide one or more guide paths.")

    source = Path(__file__).resolve().parents[2]
    docs = source / "docs"
    version = ElementTree.parse(source / "Directory.Build.props").findtext(".//Version")
    if not version or not re.fullmatch(VERSION, version):
        parser.error("Directory.Build.props has no supported app version.")

    guides = {docs / "USER-GUIDES.md": "en"}
    for language in LABELS:
        guides.update({path: language for path in (docs / language).glob("*.md") if path.name in GUIDE_NAMES})
    selected = sorted(guides) if args.all else dict.fromkeys((docs / name).resolve() for name in args.guides)
    unknown = [path for path in selected if path not in guides]
    if unknown:
        parser.error("Unknown guide: " + ", ".join(str(path) for path in unknown))

    changes = 0
    for path in selected:
        content = path.read_bytes()
        try:
            updated = guide_subtitle(content, LABELS[guides[path]], version)
        except ValueError as error:
            parser.error(f"{path.relative_to(docs)}. {error}")
        if updated == content:
            continue
        changes += 1
        print(path.relative_to(docs))
        if not args.check:
            path.write_bytes(updated)
    print(f"{len(selected)} guides checked for {version}. {changes} " + ("need an update." if args.check else "updated."))
    return 1 if args.check and changes else 0


if __name__ == "__main__":
    raise SystemExit(main())
