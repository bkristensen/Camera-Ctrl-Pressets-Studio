from argparse import ArgumentParser
from pathlib import Path

from PIL import Image


def check_rule(page, center, dpi):
    radius = round(dpi * 0.11)
    rows = range(max(0, center - radius), min(page.height, center + radius + 1))
    gray = page.convert("L")
    dark_pixels = [tuple(x for x in range(gray.width) if gray.getpixel((x, y)) < 100) for y in rows]
    widest = max(dark_pixels, key=len)
    if len(widest) < 0.78 * gray.width or widest[-1] - widest[0] + 1 != len(widest):
        raise ValueError("A full-width, continuous print rule is missing.")


def main():
    parser = ArgumentParser(description="Check print rules and assemble a review image from GuideViewer print previews.")
    parser.add_argument("folder", type=Path, help="Folder containing *-first.png images")
    parser.add_argument("--dpi", type=int, default=120, help="Resolution of the input images")
    parser.add_argument("--output", type=Path, help="Output PNG path")
    args = parser.parse_args()
    if args.dpi < 72:
        parser.error("Use previews rendered at 72 dpi or higher.")
    images = sorted(args.folder.glob("*-first.png"))
    if not images:
        parser.error("No *-first.png images found.")

    strips = []
    for path in images:
        with Image.open(path) as page:
            margin = round(args.dpi * 0.7)
            strip_height = round(args.dpi * 0.86)
            try:
                check_rule(page, margin, args.dpi)
                check_rule(page, page.height - margin, args.dpi)
            except ValueError as error:
                parser.error(f"{path.name}. {error}")
            for top in (0, page.height - strip_height):
                strip = page.crop((0, top, page.width, top + strip_height)).convert("RGB")
                strip.thumbnail((900, strip_height))
                strips.append(strip)
        print(path.name)

    gap = 5
    review = Image.new("RGB", (max(strip.width for strip in strips),
                               sum(strip.height + gap for strip in strips) - gap), "#e0e3e5")
    y = 0
    for strip in strips:
        review.paste(strip, (0, y))
        y += strip.height + gap
    output = args.output or args.folder / "print-review.png"
    review.save(output)
    print(output)


if __name__ == "__main__":
    main()
