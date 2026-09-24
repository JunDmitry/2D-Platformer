# Project Paradigm

## Primary paradigm
Unity component-based OOP

## Secondary / complementary
- Interface-driven contracts for cross-component collaboration (`IDamageable`, `IReplenishable`, `IStateChanger`, `IIntractable`, `ICoroutineRunner`)
- Custom State/Transition pattern for character behavior (`StateMachine`, `State`, `Transition`, per-character `*StateMachineFactory`)
- Event-driven communication from gameplay components to UI (C# `event Action<...>` on components like `VampireSkill`, consumed by `VampireView`)

## Layer rules
- No formal layering (no Domain/Application/Infrastructure split). Single assembly (`Assembly-CSharp`), organized by feature folder under `Assets/Scripts` (`Characters`, `Gameplay`, `StateMachines`, `UI`, `World`, `Utilities`).
- `MonoBehaviour` components are wired via `[SerializeField]` references set in the Unity Editor (composition over inheritance at the GameObject level).
- Cross-component contracts go through interfaces where the concrete type shouldn't be assumed (e.g. `IDamageable` for anything that can take damage).
- UI components read gameplay state via events rather than polling or direct coupling to gameplay internals.

## Boundaries
- Modules are split by feature/responsibility folder, not by technical layer.
- State machine logic (`StateMachines/`) is decoupled from the character MonoBehaviour via factories (`PlayerStateMachineFactory`, `EnemyStateMachineFactory`) that assemble states and transitions.
- `Gameplay/` holds mechanics (health, skills, movement, collectors); `Characters/` holds the player/enemy MonoBehaviours that compose gameplay pieces together.

## Exceptions
- None recorded yet.

## History
- 2026-09-21 — initial record (Unity component-based OOP), confirmed by user before processing docs/requests/2026-09-21.md.
