# Project Orientation — Index

- **Version:** 3
- **Git commit:** `1a33243`
- **Updated:** 2026-09-24 (local)
- **Updated by session:** sess-2026-09-24-architect

## Summary

"2D Platformer" is a small Unity 2022.3.46f1 2D platformer game. Single
implicit `Assembly-CSharp` assembly (no asmdefs), ~62 scripts organized by
feature folder under `Assets/Scripts`: `Characters`, `Gameplay`,
`StateMachines`, `UI`, `Utilities`, `World`. Player/Enemy are
`MonoBehaviour`s composing mechanic components (movement, jumping, health,
attack) and driving a custom State/Transition state machine assembled by a
factory. No DI container, no tests, no formal layering — Unity
component-based OOP throughout.

## Subfiles

| File | What it covers |
|------|----------------|
| [project-map.md](./project-map.md) | Solution structure, projects, folder layout |
| [modules.md](./modules.md) | Key modules and their responsibilities, incl. Skill System |
| [paradigms.md](./paradigms.md) | Paradigm pointer (see `../paradigm.md`) |
| [tech-stack.md](./tech-stack.md) | Unity/C# version, packages, infrastructure |
| [conventions.md](./conventions.md) | Coding, DI, naming, testing conventions |
| [hotspots.md](./hotspots.md) | Known problems, tech debt, risks (incl. Skill System gaps) |

## How to use

- Load this file first in every session.
- Load a subfile only when the current task touches its area.
- Before major steps, re-check `.meta.json` for parallel updates.
- To refresh, see `references/orientation.md` in the skill.
