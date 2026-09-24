# ADR-0002: Knockback capability via `IKnockbackable` and `Mover.IsOverridden`

- **Status:** accepted
- **Date:** 2026-09-24
- **Deciders:** architector-role (user-approved), coder-role (originating request)
- **Related:** `docs/architecture/tasks/c2.md`, `docs/architecture/architect-inbox/c2.md`, ADR-0001

## Context

A coder-role task needed a skill effect that pushes a target away from a point over a fixed duration, at a rate expressed per second. The only cross-component contract a skill effect can use to affect a target is `IDamageable.TakeDamage(float) : float` — there is no way to apply force or displacement through an existing contract. Additionally, `PlayerMover` drives movement via `Rigidbody2D.velocity` set every `FixedUpdate` from the state machine's own `Move` calls, while `EnemyMover` drives movement by directly setting `transform.position` from a self-managed coroutine restarted on every `Move` call. An external push into either mover would be overwritten or fought within the same frame unless the mover is told to cede control for the push's duration.

## Decision

We will add a new `IKnockbackable` interface (`ApplyKnockback(Vector2 direction, float speedPerSecond, float durationInSeconds)`), implemented by `Player` and `Enemy`, each delegating to its own `Mover`. `Mover` (abstract base) gains a `protected bool IsOverridden` flag and an abstract `ApplyKnockback(...)` method; `PlayerMover.Move` and `EnemyMover.Move` become no-ops while `IsOverridden` is true, so the state machine's per-frame `Move` calls stop fighting an active knockback. Each mover implements `ApplyKnockback` using its own existing physics model.

- Modules affected: `Characters` (new `IKnockbackable.cs`, `Player.cs`, `Enemy.cs`), `Gameplay` (`Mover.cs` base class), `Characters/Player/PlayerMover.cs`, `Characters/Enemy/EnemyMover.cs`.
- New dependencies: none.
- Migration steps: additive only — `Player`/`Enemy` gained a promoted `_mover` field (was a local variable in `Awake`); no existing call sites changed behavior when `IsOverridden` is false (default).
- Out of scope: the actual knockback `ISkillEffect` implementation (mouse-point area resolution, max-range clamp) — left to coder-role, not architecturally blocked. Unifying `PlayerMover`/`EnemyMover` onto one physics model (Option B, rejected) — bigger, separate concern.

## Options considered

### Option A — `IKnockbackable` + `Mover.IsOverridden` flag (chosen)
- Pros: smallest change; matches the project's composition/interface-contract convention; reuses `AuraAreaDetector.FindTargetsInRadius`'s existing `List<IDamageable>` return type as-is (an effect casts `target is IKnockbackable` on the `IDamageable` it already has — no detector contract change); each mover keeps its own physics model.
- Cons: `Mover` gains a second abstract method; does not resolve the underlying two-physics-models split, only makes both movers cede control safely during an external push.
- Why chosen: solves exactly the blocking problem with the smallest, most reversible footprint; consistent with `paradigm.md`'s interface-driven contract rule.

### Option B — Unify movers on Rigidbody2D physics first
- Pros: permanently resolves the "two incompatible physics models" hotspot for any future external-force mechanic (wind, launch pads), not just knockback.
- Cons: much bigger, riskier change — rewrites `EnemyMover`'s core movement model, touches `EnemyStateMachine` behavior built around instant `transform.position` sets, no test safety net to catch regressions, well beyond this request's scope.
- Why not chosen: disproportionate to the request; the two-physics-models split remains a known hotspot to revisit only if a future need (beyond knockback) justifies it.

## Consequences

### Positive
- Skill effects can now push any `IDamageable` target that also implements `IKnockbackable`, without bypassing the interface-driven contract convention.
- The override mechanism (`IsOverridden`) is generic enough to support future external-movement mechanics (launch pads, stuns) without a new contract per mechanic.

### Negative / trade-offs
- `Mover` base class now carries two responsibilities (normal movement + override arbitration) instead of one.
- The two movers still diverge in physics model; `IsOverridden` masks the divergence for this use case but doesn't remove it.

### Neutral
- `Player`/`Enemy` each hold one additional private field (`_mover`) promoted from a constructor-local variable.

## Compliance with paradigm

- Complies with `paradigm.md`'s "Cross-component contracts go through interfaces where the concrete type shouldn't be assumed" rule (new `IKnockbackable` interface, same shape as `IDamageable`/`IReplenishable`).
- No deviation from the recorded paradigm; no new exception needed.

## Migration plan

1. Add `IKnockbackable.cs` (+ .meta).
2. Add `IsOverridden` + abstract `ApplyKnockback` to `Mover`.
3. Implement `ApplyKnockback` in `PlayerMover` and `EnemyMover`; guard `Move` with `IsOverridden`.
4. Implement `IKnockbackable` in `Player` and `Enemy`, delegating to a promoted `_mover` field.
5. Verify with `dotnet build` — green throughout, no intermediate broken state since all changes are additive.

## References

- Related code: `Assets/Scripts/Characters/IKnockbackable.cs`, `Assets/Scripts/Gameplay/Mover.cs`, `Assets/Scripts/Characters/Player/PlayerMover.cs`, `Assets/Scripts/Characters/Enemy/EnemyMover.cs`, `Assets/Scripts/Characters/Player/Player.cs`, `Assets/Scripts/Characters/Enemy/Enemy.cs`
- Related docs: `docs/architecture/tasks/c2.md`, `docs/architecture/architect-inbox/c2.md`, `docs/architecture/orientation/modules.md`
