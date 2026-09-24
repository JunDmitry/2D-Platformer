# ADR-0001: Generalized Skill System (Skill + SkillConfig + ISkillEffect)

- **Status:** accepted
- **Date:** 2026-09-22
- **Deciders:** Dmitry Rysev (product owner), architect session
- **Related:** [Task 1](../tasks/1.md), [modules.md](../orientation/modules.md), [hotspots.md](../orientation/hotspots.md)

## Context

The project had exactly one skill, `VampireSkill`, implemented as a single
`MonoBehaviour` that hard-codes the execute/dedupe/duration-tick/cooldown/
event lifecycle, with duration and cooldown as raw `[SerializeField] float`
values and a direct reference to `NearestVampireDamageEffect`. Adding a
second skill would require copy-pasting `VampireSkill`'s coroutine logic
into a near-identical class, since there was no shared base type, no
skill registry, and no data-driven configuration. This was flagged in
orientation `hotspots.md` and requested for generalization by the product
owner (`docs/requests/2026-09-21.md#1`), who confirmed: support multiple
skills, make tuning data ScriptableObject-based, and give characters a
generic skill list rather than fixed named slots.

## Decision

We will replace `VampireSkill` with a generic `Skill` MonoBehaviour that
owns the lifecycle (execute/dedupe/duration-tick/cooldown/events), reads
duration/cooldown from an injected `SkillConfig` ScriptableObject, and
delegates the per-tick effect to an injected `ISkillEffect` implementation,
because this is the smallest change that removes the duplication risk
while staying inside the project's existing composition-over-inheritance,
all-`MonoBehaviour` convention.

Consequences:

- **Modules affected:** `Gameplay/Skill System` (new `Skill`, `SkillConfig`,
  `ISkillEffect`; `AuraSingleDetector` API changed), `Characters/Player`
  (`Player`, `InputReader`), `UI` (`VampireView` → `SkillView`),
  `Assets/Scenes/SampleScene.unity`.
- **New dependencies:** none (no new packages; `SkillConfig` is a plain
  Unity `ScriptableObject`).
- **Migration steps:** see below.
- **Out of scope:** making `InputReader`'s skill keys player-remappable
  (still hard-coded consts, just indexed now instead of a single named
  key) — flagged as a follow-up in `hotspots.md`, not part of this task.

## Options considered

### Option A — Composed `Skill` component (chosen)
- Pros: smallest change; matches the project's composition-over-inheritance
  convention; one class to reason about; no MonoBehaviour inheritance
  friction.
- Cons: no dedicated extension point if a future skill needs genuinely
  different lifecycle semantics (only different effects are supported
  cleanly) — acceptable since no such skill currently exists.
- Why chosen: covers the stated requirements (multiple skills, data-driven
  tuning, generic loadout) with the least structural change and the least
  deviation from existing conventions.

### Option B — `Skill` base class + subclasses
- Pros: clean extension point for skills with genuinely different
  lifecycle behavior.
- Cons: MonoBehaviour inheritance adds Inspector/Awake-chain friction; no
  current skill needs it.
- Why not chosen: speculative flexibility for a lifecycle variation that
  doesn't exist yet; adds friction to every future skill for a benefit
  only some future skill might need.

### Option C — Fully data-driven `SkillDefinition` assets + generic executor
- Pros: most reusable/shareable across prefabs; skills become near-pure
  data.
- Cons: biggest refactor; moves effect/detector logic out of
  MonoBehaviour-land, deviating further from the project's current
  all-MonoBehaviour convention; detector loses its scene-object Gizmo.
- Why not chosen: overshoots the current requirement and conflicts with
  the project's paradigm (`docs/architecture/paradigm.md`) more than
  necessary.

## Consequences

### Positive
- Adding a new skill is now: implement `ISkillEffect`, create a
  `SkillConfig` asset, wire a `Skill` component — no coroutine/cooldown/
  event boilerplate to duplicate.
- `Player` holds a generic `Skill[]` loadout instead of a fixed named
  field, matching the "generic skill list" requirement directly.
- Incidentally fixes the `AuraSingleDetector.FoundTargets` null-yield API
  oddity (already flagged in `hotspots.md`) since it sits in the file
  being touched: `FindNearestTarget` now returns a plain nullable
  `IDamageable` instead of a single-or-empty `IEnumerable`.

### Negative / trade-offs
- `Skill._effect` is a `[SerializeField] MonoBehaviour` validated at
  `OnValidate`/cast at `Awake` rather than a compile-time-checked
  `ISkillEffect` reference, because Unity's Inspector cannot serialize an
  interface-typed field directly. This is the standard Unity workaround
  and matches how `IDamageable`/`IReplenishable` are already consumed
  elsewhere via runtime `GetComponent`/cast patterns.
- No automated test covers the new lifecycle (project has no test
  suite yet — see `hotspots.md`); verified by `dotnet build` (green) and
  manual read-through of the coroutine logic against the original.

### Neutral
- `VampireSkillConfig.asset` is seeded with the scene's pre-existing
  values (duration=6s, cooldown=4s) so runtime behavior for the existing
  vampire skill instance is unchanged.

## Compliance with paradigm

- Consistent with `docs/architecture/paradigm.md`: composition over
  inheritance at the GameObject level, interface-driven contracts for
  cross-component collaboration (new `ISkillEffect` joins `IDamageable`/
  `IReplenishable`/etc.), event-driven UI communication (`SkillView`
  still consumes `Skill`'s C# events, unchanged pattern).
- Introduces one new element not previously in the paradigm file:
  ScriptableObject-based configuration (`SkillConfig`). This is additive,
  not a deviation — no exception entry needed, but worth noting for future
  orientation refreshes if more ScriptableObject configs appear.

## Migration plan

1. Add `ISkillEffect` interface. *(build green — new file, no callers yet)*
2. Add `SkillConfig` ScriptableObject + seed `VampireSkillConfig.asset`
   with duration=6, cooldown=4. *(build green — new files, no callers yet)*
3. Rename `VampireSkill` → `Skill`; swap raw duration/cooldown fields for
   `_config : SkillConfig`; retype `_effect` to a validated
   `MonoBehaviour` cast to `ISkillEffect`. *(build green after step 4)*
4. Fix `AuraSingleDetector.FoundTargets` → `FindNearestTarget` (nullable
   return instead of nullable yield); update `NearestVampireDamageEffect`
   to implement `ISkillEffect`. *(build green)*
5. Update `Player`: `_firstSkill : VampireSkill` → `_skills : Skill[]`,
   triggered by index. *(build green)*
6. Update `InputReader`: single `Alpha1` key → indexed `SkillKeys` table
   + `IsSkill(int)`. *(build green)*
7. Rename `VampireView` → `SkillView`; retype field to `Skill`.
   *(build green)*
8. Update `Assets/Scenes/SampleScene.unity`: `VampireSkill` component's
   `_durationInSeconds`/`_cooldownInSeconds` → `_config` asset reference;
   `Player._firstSkill` → one-element `_skills` array; `VampireView`'s
   field renamed to `_skill`. *(scene YAML hand-edited; verified by
   grepping for stale field/type references)*
9. Update orientation subfiles (`modules.md`, `hotspots.md`), write this
   ADR, bump orientation `.meta.json` version.

All steps verified green via `dotnet build "2D Platformer.sln"` (Unity's
generated, gitignored `.csproj` was stale from before the rename and was
patched locally, not committed, purely to run this verification).

## References

- Related code: `Assets/Scripts/Gameplay/Skill System/`,
  `Assets/Scripts/Characters/Player/Player.cs`,
  `Assets/Scripts/Characters/Player/InputReader.cs`,
  `Assets/Scripts/UI/SkillView.cs`, `Assets/Scenes/SampleScene.unity`
- Related docs: `docs/architecture/tasks/1.md`,
  `docs/architecture/orientation/modules.md`,
  `docs/architecture/orientation/hotspots.md`
