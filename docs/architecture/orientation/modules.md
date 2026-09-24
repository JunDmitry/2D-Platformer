# Modules

- **Parent:** [index.md](./index.md)
- **Version:** 1
- **Git commit:** `1a33243`
- **Updated:** 2026-09-21

## Scope

Logical modules by folder, their responsibility and key dependencies. Does not cover file-level structure (see [project-map.md](./project-map.md)).

## Content

### Characters
Player and Enemy `MonoBehaviour`s that compose gameplay mechanics (`Mover`, `Jumper`, `Health`, `Attacker`) and drive a `StateMachine` built by a factory. `Player` implements `IDamageable`, `IReplenishable`, `IKnockbackable`; `Enemy` implements `IDamageable`, `IKnockbackable`. Both delegate `IKnockbackable.ApplyKnockback(...)` to their own `Mover` (ADR-0002). Depends on `Gameplay`, `StateMachines`, `Utilities`.

### Gameplay
Mechanics: movement (`Mover`, `Jumper`), combat (`Attacker`, `AttackDetector`), health (`Health System/`), collectibles (`AidKitCollector`, `CoinCollector`), teleportation (`Teleporter`). `Mover` (abstract base, ADR-0002) owns a `protected IsOverridden` flag and an abstract `ApplyKnockback(Vector2, float, float)`; `PlayerMover`/`EnemyMover` guard their `Move` with `IsOverridden` and implement `ApplyKnockback` using their own physics model (Rigidbody2D velocity vs. transform.position lerp) — the two movers still diverge in physics model, `IsOverridden` only arbitrates who controls movement each frame.

**Skill System** (generalized, ADR-0001): `Skill` (MonoBehaviour, generic execute/dedupe/duration-tick/cooldown/event lifecycle; reads tuning from a `SkillConfig` ScriptableObject and delegates the per-tick effect to an injected `ISkillEffect`), `SkillConfig` (ScriptableObject holding duration/cooldown, e.g. `VampireSkillConfig.asset`), `ISkillEffect` (per-tick effect contract, `IEnumerator Run(ExecutorData)`), `AuraSingleDetector` (physics overlap query, `FindNearestTarget` returns a nullable `IDamageable`), `AuraAreaDetector` (physics overlap query, `FindTargetsInRadius` returns `List<IDamageable>`, used by multi-target effects), `NearestVampireDamageEffect` (implements `ISkillEffect`; applies damage-per-second to detected target and heals the executor via `IReplenishable`), `DoubleStrikeAreaEffect` (implements `ISkillEffect`; strikes all targets in `AuraAreaDetector`'s radius twice, separated by an interval), `ExecutorData` (value struct carrying the skill user's id/position/rotation/interfaces). `Player` holds a `Skill[]` loadout triggered by index via `InputReader.IsSkill(int)`, instead of a single named slot. Adding a new skill now means: new `ISkillEffect` implementation + new `SkillConfig` asset + wiring a `Skill` component — no coroutine/cooldown/event boilerplate to duplicate. A knockback effect can target anything the detector returns that also implements `IKnockbackable` (`Characters`, ADR-0002) — no detector/contract change needed for that half.

### StateMachines
Generic `State`/`Transition`/`StateMachine`/`IStateChanger` base types, plus per-character `PlayerStateMachine/` and `EnemyStateMachine/` state & transition implementations, assembled by `Factories/`. Decoupled from `Characters` via constructor injection of dependencies into the factory.

### UI
Presentation components that subscribe to gameplay events (e.g. `SkillView` listens to `Skill` events) or poll collectors (`CollectorViewer`). No two-way coupling back into gameplay.

### Utilities
Cross-cutting helpers: `CoroutineRunner`/`ICoroutineRunner` (run coroutines from non-MonoBehaviour context), `Flipper` (sprite flip), `Direction`, `IOffsetChanger`.

### World
Static/scene world objects: `Ground`, `GroundDetector`, `Coin`, `AidKit`, `IIntractable`.

## Related

- ADRs: [ADR-0001](../adr/0001-generalized-skill-system.md), [ADR-0002](../adr/0002-knockback-capability.md)
- Other subfiles: [project-map.md](./project-map.md), [hotspots.md](./hotspots.md)

## Changelog

- 2026-09-21 — initial write (full orientation).
- 2026-09-22 — Skill System generalized per ADR-0001: `VampireSkill` → `Skill` (+ `SkillConfig`, `ISkillEffect`), `VampireView` → `SkillView`, `Player._firstSkill` → `Player._skills[]`.
- 2026-09-24 — added undocumented `AuraAreaDetector`/`DoubleStrikeAreaEffect` (built on the ADR-0001 pattern by coder-role, not previously reflected here). Knockback capability added per ADR-0002: `Mover.IsOverridden` + abstract `ApplyKnockback`, `IKnockbackable` implemented by `Player`/`Enemy`.
