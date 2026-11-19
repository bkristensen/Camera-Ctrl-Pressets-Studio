# Security policy

## Status and scope

The project is beta. Only the latest published version is intended for fixes;
there is no long-term-support commitment. The settings code is an accidental-edit
lock, not a security boundary. Local users with profile-file access can alter it.
Startup paths/arguments can execute programs with the user's normal permissions.
Do not import or run configuration from an untrusted source without reviewing it.

## Reporting

After the repository is published, maintainers should enable GitHub private
vulnerability reporting under Settings > Code security. Use that channel for a
security issue; do not post exploitable details, credentials or personal data in a
public issue. If the private channel is unavailable, ask the maintainer for a private
contact without revealing the vulnerability. There is no configured private mailbox
or response-time guarantee in this source export.

Include affected version, impact, safe reproduction steps and proposed mitigation.
Do not disable Windows security to reproduce issues. Signing proves distribution
identity/integrity, not freedom from bugs or malicious behaviour.
