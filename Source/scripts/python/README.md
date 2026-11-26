# Python tools

Run these commands from the repository root with Python 3.

- `python Source/scripts/python/update_guide_subtitles.py --all --check` checks guide subtitles against the version in `Source/Directory.Build.props`.
- `python Source/scripts/python/update_guide_subtitles.py da/SETUP.md en/SETUP.md de/SETUP.md` updates the guides you name. Use `--all` after reviewing the guides for a release.
- `python Source/scripts/python/render_guide_print_review.py <preview-folder>` checks the printed rules and combines page headers and footers into one PNG. Install Pillow first. Use Poppler’s `pdftoppm -f 1 -l 1 -singlefile -png -r 120` to render one `*-first.png` per guide. Pass `--dpi` if you render at a different resolution.

Keep one-off edit scripts and generated previews in a temporary folder.
