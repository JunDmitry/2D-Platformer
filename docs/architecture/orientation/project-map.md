# Project Map

- **Parent:** [index.md](./index.md)
- **Version:** 1
- **Git commit:** `1a33243`
- **Updated:** 2026-09-21

## Scope

Solution/project structure and script folder layout. Does not cover module responsibilities (see [modules.md](./modules.md)).

## Content

Unity 2022.3.46f1 project, 2D platformer. Single generated solution `2D Platformer.sln` with two generated projects:
- `Assembly-CSharp.csproj` — runtime scripts (`Assets/Scripts/**`)
- `Assembly-CSharp-Editor.csproj` — editor-only scripts

No `.asmdef` files — the entire runtime codebase is one implicit assembly. No test assembly is wired up despite `com.unity.test-framework` being present in `Packages/manifest.json`.

### `Assets/Scripts` folder layout

```
Assets/Scripts/
├── Characters/
│   ├── Enemy/            (Enemy.cs, EnemyMover, PlayerSearcher, Route, EnemyAnimationEvents)
│   ├── Player/            (Player.cs, PlayerMover, InputReader, PlayerAnimationEvents)
│   ├── IDamageable.cs
│   └── IReplenishable.cs
├── Gameplay/
│   ├── Health System/      (Health, HealthIndicator, IHealthChangeHandler, Indicators/*)
│   ├── Skill System/       (VampireSkill, AuraSingleDetector, NearestVampireDamageEffect, ExecutorData)
│   ├── AidKitCollector, AttackDetector, Attacker, CoinCollector, Jumper, Mover, Teleporter
├── StateMachines/
│   ├── EnemyStateMachine/  (States/, Transitions/)
│   ├── PlayerStateMachine/ (States/, Transitions/)
│   ├── Factories/          (EnemyStateMachineFactory, PlayerStateMachineFactory)
│   ├── State.cs, StateMachine.cs, Transition.cs, IStateChanger.cs
├── UI/                     (CollectorViewer, VampireView)
├── Utilities/               (CoinSpawner, CoroutineRunner, Direction, Flipper, ICoroutineRunner, IOffsetChanger)
└── World/                   (AidKit, Coin, Ground, GroundDetector, IIntractable)
```

62 `.cs` files total. Namespacing is inconsistent: most files use no namespace (global), a few use `Assets.Scripts.Gameplay.Skill_System` or `Assets.Scripts.UI`.

## Related

- ADRs: none yet.
- Other subfiles: [modules.md](./modules.md), [tech-stack.md](./tech-stack.md)

## Changelog

- 2026-09-21 — initial write (full orientation).
