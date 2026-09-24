# Tech Stack

- **Parent:** [index.md](./index.md)
- **Version:** 1
- **Git commit:** `1a33243`
- **Updated:** 2026-09-21

## Scope

Engine version, language version, key packages, infrastructure. Does not cover code conventions (see [conventions.md](./conventions.md)).

## Content

- **Engine:** Unity 2022.3.46f1 (LTS)
- **C# language version:** 9.0 (`LangVersion` in generated `.csproj`), target framework `netstandard2.1` / `v4.7.1` per Unity's generated project
- **Key packages** (`Packages/manifest.json`): `com.unity.feature.2d` (2D toolset), `com.unity.textmeshpro`, `com.unity.timeline`, `com.unity.visualscripting`, `com.unity.test-framework` (present but no test assembly wired up), `com.unity.ide.rider`/`com.unity.ide.visualstudio`
- **DI:** none — no DI container. Dependencies are wired manually via `[SerializeField]` (Editor-assigned references) or constructor parameters passed explicitly (e.g. `PlayerStateMachineFactory.Create(...)`).
- **Persistence/Infra:** none observed (no save system, no networking, no backend).
- **Testing:** `com.unity.test-framework` package is installed but no `Tests` assembly definition or test files exist in `Assets/`.

## Related

- ADRs: none yet.
- Other subfiles: [project-map.md](./project-map.md)

## Changelog

- 2026-09-21 — initial write (full orientation).
