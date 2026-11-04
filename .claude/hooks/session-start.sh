#!/bin/bash
# Prepares a Claude Code cloud session (Linux) for the web app's gate: the npm
# tooling, and the commit identity this repository uses (CLAUDE.md, Workflow).
set -euo pipefail

if [ "${CLAUDE_CODE_REMOTE:-}" != "true" ]; then
  exit 0
fi

cd "$(dirname "$0")/../.."

# Commits are the owner's, with Claude as co-author in the message.
git config user.name jurakovic
git config user.email 17744091+jurakovic@users.noreply.github.com

# Lint, types and tests. Playwright's Chromium is pre-installed in cloud
# sessions for the @playwright/test version package.json pins.
npm ci --no-audit --no-fund --loglevel=error

# .NET 8 and libfaketime regenerate the vectors (tools/spec-vectors). They are
# needed rarely and take a minute, so CLAUDE.md says how to install them on demand.
