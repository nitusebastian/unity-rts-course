# Repository Guidelines

## Project Structure & Module Organization
- `Assets/`: game content and code.
  - `Assets/Scripts/Player/` and `Assets/Scripts/Units/` (e.g., `PlayerInput.cs`, `Worker.cs`).
  - `Assets/Scenes/` (e.g., `Scenes/Game`), `UI/`, `Materials/`, `Shaders/`, `Units/`.
- `Packages/`: Unity Package Manager config (Cinemachine, Input System, Test Framework).
- `ProjectSettings/`, `UserSettings/`: editor and project configuration.
- `Library/`, `Temp/`, `Logs/`: generated; do not commit.
- `unity-rts-course.sln`, `*.csproj`: IDE integration only.
- Unity version: `ProjectSettings/ProjectVersion.txt` (6000.2.0f1).

## Build, Test, and Development Commands
- Open: use Unity Hub with Editor 6000.2.0f1.
- Play-in-Editor: press Play to run the `Game` scene.
- Build (CLI examples):
  - Windows: `Unity -batchmode -nographics -quit -projectPath . -buildWindows64Player Builds/Windows/RTS.exe`
  - macOS: `Unity -batchmode -nographics -quit -projectPath . -buildOSXUniversalPlayer Builds/macOS/RTS.app`
- Run tests (CLI): `Unity -batchmode -projectPath . -runTests -testResults Logs/test-results.xml -testPlatform EditMode`

## Coding Style & Naming Conventions
- Language: C# for Unity; 4-space indentation; one public type per file.
- Filenames match class names; organize scripts under `Assets/Scripts/<Area>/`.
- Namespaces: `GameDevTV.RTS` and sub-namespaces (e.g., `GameDevTV.RTS.Player`).
- Classes/enums: PascalCase; fields: camelCase; properties: PascalCase; constants: PascalCase.
- Unity fields: prefer `[SerializeField] private` with no leading underscore; expose via properties as needed.

## Testing Guidelines
- Framework: Unity Test Framework (`com.unity.test-framework`).
- Location: create `Assets/Tests/EditMode/` and `Assets/Tests/PlayMode/` as needed.
- Naming: `FooTests.cs`; methods like `Should_DoThing_When_Condition`.
- Scope: cover input handling, camera behavior, unit selection/movement; keep tests deterministic.

## Commit & Pull Request Guidelines
- Branches: `feature/<short-desc>`, `fix/<issue>`, `refactor/<area>`.
- Commits: imperative mood; prefer Conventional Commits (`feat:`, `fix:`, `chore:`, `refactor:`, `docs:`).
- PRs: include purpose, linked issues, test steps, and a short clip/screenshot for gameplay/UI changes; keep diffs focused and reviewed by a peer.

## Security & Configuration Tips
- Do not commit `Library/`, `Temp/`, `Logs/`, or build artifacts; ensure `.gitignore` covers them.
- Manage packages via `Packages/manifest.json`; avoid manual edits to cache.
- Align contributors on Editor version from `ProjectVersion.txt` to prevent scene/prefab churn.

