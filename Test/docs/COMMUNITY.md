# GitHub community workflow

Before publishing, enable Issues in repository settings. The supplied issue forms
appear automatically when placed under .github/ISSUE_TEMPLATE on the default branch.
Enable Discussions separately if desired; templates do not switch it on.

| Content | Best location |
| --- | --- |
| Reproducible malfunction | Bug report issue |
| Concrete proposed improvement | Feature request issue |
| Working or failing hardware/software combination | Compatibility report issue |
| Open question or broad idea | Discussions: Q&A or Ideas |
| Stable recurring answer | faq.md via pull request |
| Accepted future work | ROADMAP.md plus linked issues |

Suggested labels (create manually): bug, enhancement, compatibility, documentation,
needs-info, good-first-issue, help-wanted, windows, msix. These are recommendations,
not labels already created by this archive.

A useful feature request explains the problem, intended user, proposed behaviour,
alternatives, UI impact, and how the primary workflow stays simple. Do not use issue
votes as a promise of implementation. Maintainers can mark requests as planned,
needs-design, deferred or out-of-scope and explain the reason.

Use Discussions Q&A for support if enabled. Convert proven recurring answers into
FAQ entries and link relevant issues. Keep changelogs factual and per release.
Never ask users to upload private meeting footage, credentials or entire personal
configuration files. Replace repository/support URLs before publishing Store text.
