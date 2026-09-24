# Conventions

- **Parent:** [index.md](./index.md)
- **Version:** 1
- **Git commit:** `1a33243`
- **Updated:** 2026-09-21

## Scope

Coding, DI, and testing conventions actually observed in the codebase. Does not cover module boundaries (see [modules.md](./modules.md)).

## Content

- **DI style:** manual, Editor-wired `[SerializeField]` fields for cross-component references; plain constructor parameters for non-MonoBehaviour collaborators (state machine factories).
- **Testing:** none present; no test conventions established.
- **Naming:** PascalCase types/methods, `_camelCase` private fields, `I`-prefixed interfaces. Consistent throughout.
- **Namespaces:** inconsistent — most types have no namespace; `Skill System` and `UI` folders use `Assets.Scripts.Gameplay.Skill_System` / `Assets.Scripts.UI` (note the literal space converted to `Skill_System`, and namespace not matching the `Gameplay` folder name exactly for other folders like `Health System`, which uses no namespace at all).
- **Folder naming:** some folders contain spaces (`Skill System`, `Health System`) — unusual for C# but tolerated by Unity/csproj generation.
- **State pattern:** states use template-method hooks (`OnUpdate`/`OnUpdating`/`OnFixedUpdate`/`OnFixedUpdating`) and transitions are added imperatively in factories; `Transition` and `State` are plain C# classes, not `MonoBehaviour`s, built via factories per character.
- **Events:** gameplay-to-UI communication uses public C# `event Action<...>` fields on `MonoBehaviour`s (e.g. `VampireSkill.StartedDuration`), subscribed/unsubscribed in `Awake`/`OnEnable`/`OnDisable`.
- **Coroutines:** used pervasively for time-based behavior (skill duration/cooldown, UI slider animation) instead of `async`/`await` or a tick/update-driven timer.

## Related

- ADRs: none yet.
- Other subfiles: [modules.md](./modules.md), [hotspots.md](./hotspots.md)

## Changelog

- 2026-09-21 — initial write (full orientation).
