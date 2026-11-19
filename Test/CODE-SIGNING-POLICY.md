# Code signing policy

Current beta releases are unsigned unless the specific release explicitly states
otherwise and provides verifiable signature information. No SignPath Foundation
approval, certificate or integration is currently configured. No publisher account
or Microsoft Store certification is implied by the packaging scripts.

For future signed releases, maintainers must identify the actual signing provider,
repository commit/tag, build record and signature-verification outcome. Never sign
arbitrary uploaded binaries. Review source and build-script changes and keep signing
approval separate from untrusted pull-request execution. Do not embed credentials
in source or distribute private keys.

Roles must be named before production signing:

| Responsibility | Current assignment |
| --- | --- |
| Maintainer / release owner | To be assigned in the public repository |
| Reviewers | To be assigned |
| Signing approvers | To be assigned after signing setup |

[Privacy policy](PRIVACY.md). If SignPath approves the project, update this policy
with its required attribution and actual team identities according to current terms.
Do not represent this draft as compliance approval by any provider.
