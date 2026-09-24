# Hotspots

- **Parent:** [index.md](./index.md)
- **Version:** 1
- **Git commit:** `1a33243`
- **Updated:** 2026-09-21

## Scope

Known problems, tech debt, and risk areas observed during orientation.

## Content

- **No asmdef boundaries**: single implicit assembly means no compiler-enforced module boundaries; any script can reference any other.
- **No tests**: `com.unity.test-framework` is installed but unused — no regression safety net for changes like the Skill System generalization (ADR-0001), which was verified by manual `dotnet build` + read-through only.
- **Namespace inconsistency**: see [conventions.md](./conventions.md) — most code has no namespace, a few folders do, spellings don't match folder names 1:1.
- **`InputReader`'s per-skill keys are still hard-coded consts** (`SkillKeys` array), not player-remappable. Follow-up only if remapping is ever needed (noted in ADR-0001 / task 1).
- **`PlayerMover`/`EnemyMover` use two incompatible physics models** (Rigidbody2D velocity vs. direct `transform.position` coroutine). ADR-0002 added a `Mover.IsOverridden` flag so both movers can safely cede control to an external push (knockback), but the underlying divergence itself remains.
- **Gitignored `Assembly-CSharp.csproj` drifts from the actual file set**: Unity regenerates it, but this repo's copy needed manual patching (ADR-0001 and this task) to build via `dotnet build` outside the Unity Editor. Not committed, so each session may need to re-patch it.

### Resolved

- ~~Skill System is not a system~~ — resolved by ADR-0001 (2026-09-22): `Skill` + `SkillConfig` + `ISkillEffect` replace the single hard-coded `VampireSkill` pipeline.
- ~~`AuraSingleDetector.FoundTargets` returns `IEnumerable<IDamageable>` via `yield return target;` where `target` can be `null`~~ — resolved by ADR-0001 (2026-09-22): replaced with `FindNearestTarget` returning a plain nullable `IDamageable`.
- ~~No contract lets a skill effect apply force/displacement to a target~~ — resolved by ADR-0002 (2026-09-24): new `IKnockbackable` interface implemented by `Player`/`Enemy`.

## Related

- ADRs: [ADR-0001](../adr/0001-generalized-skill-system.md), [ADR-0002](../adr/0002-knockback-capability.md)
- Other subfiles: [modules.md](./modules.md)

## Changelog

- 2026-09-21 — initial write (full orientation).
- 2026-09-22 — Skill System and `FoundTargets` API entries resolved per ADR-0001; added `InputReader` hard-coded-keys follow-up.
- 2026-09-24 — added two-physics-models and gitignored-csproj-drift hotspots; resolved "no force/displacement contract" per ADR-0002.
