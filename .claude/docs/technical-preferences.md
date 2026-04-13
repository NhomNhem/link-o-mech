# Technical Preferences

<!-- Populated by /setup-engine. Updated as the user makes decisions throughout development. -->
<!-- All agents reference this file for project-specific standards and conventions. -->

## Engine & Language

- **Engine**: Unity 6.3 LTS (6000.3.0f1)
- **Language**: C#
- **Rendering**: URP (Universal Render Pipeline)
- **Physics**: Unity Physics (2D Rigidbody — no complex 3D physics per project scope)

## Input & Platform

<!-- Written by /setup-engine. Read by /ux-design, /ux-review, /test-setup, /team-ui, and /dev-story -->
<!-- to scope interaction specs, test helpers, and implementation to the correct input methods. -->

- **Target Platforms**: Web (WebGL — primary), PC (secondary/testing)
- **Input Methods**: Keyboard/Mouse
- **Primary Input**: Keyboard
- **Gamepad Support**: None (jam scope — may add post-jam)
- **Touch Support**: None
- **Platform Notes**: WebGL is the primary deployment target. Avoid realtime lighting — use baked or simple lighting only. Keep textures compressed (DXT/ETC2). Disable realtime shadows for WebGL builds. Browser memory limit ~512MB — keep assets lean.

## Naming Conventions

- **Classes**: PascalCase (e.g., `PlayerController`, `NetworkLinker`, `PistonModule`)
- **Public properties/fields**: PascalCase (e.g., `MoveSpeed`, `LinkDistance`)
- **Private fields**: _camelCase (e.g., `_moveSpeed`, `_isLinked`, `_leader`)
- **Methods**: PascalCase (e.g., `TakeDamage()`, `LinkToPlayer()`, `ActivateModule()`)
- **Files**: PascalCase matching class name (e.g., `PlayerController.cs`, `NetworkLinker.cs`)
- **Scenes**: PascalCase descriptive (e.g., `Bootstrap.unity`, `Level_01_Intro.unity`)
- **Prefabs**: PascalCase (e.g., `Player.prefab`, `PistonModule.prefab`)
- **Constants**: PascalCase or UPPER_SNAKE_CASE (e.g., `MaxLinkDistance`, `MAX_LINK_DISTANCE`)
- **Interfaces**: `I` prefix PascalCase (e.g., `IInteractable`, `IModule`)
- **Photon Fusion NetworkBehaviours**: PascalCase, suffix describing purpose (e.g., `NetworkLinker`, `PlayerSpawner`)

## Performance Budgets

- **Target Framerate**: 60fps
- **Frame Budget**: 16.6ms
- **Draw Calls**: < 50 per frame (WebGL constraint — batching is critical)
- **Memory Ceiling**: 512MB (browser tab practical limit)
- **Texture Budget**: DXT1/DXT5 compression; max 512×512 for sprites at standard scale
- **Build Size Target**: < 30MB compressed (fast WebGL load in Chrome/Edge)

## Testing

- **Framework**: NUnit (built into Unity) + Unity Test Runner
- **Minimum Coverage**: Logic systems (link calculations, module triggers) — 60% minimum for jam timeline
- **Required Tests**: Link distance clamping, leader assignment logic, module activation state, IInteractable contract
- **Note**: Jam timeline — prioritize gameplay-critical logic tests over UI/visual coverage

## Forbidden Patterns

<!-- Add patterns that should never appear in this project's codebase -->
- NO Unity Physics joints for the link system — use positional offset math only (`FollowerPos = LeaderPos + Offset`)
- NO parenting players to each other for linking
- NO deep inheritance hierarchies — prefer composition and interfaces (`IModule`, `IInteractable`)
- NO hardcoded gameplay values — all tunable values in public fields or ScriptableObjects
- NO `GameObject.Find()` at runtime — use VContainer dependency injection or cached references
- NO memory allocations in `FixedUpdateNetwork()` — avoid GC pressure in Fusion tick loop

## Allowed Libraries / Addons

<!-- Add approved third-party dependencies here -->
- **Photon Fusion 2** — multiplayer networking (host/client model, tick-based sync)
- **VContainer** — dependency injection (replaces singletons)
- **DOTween** — animation/tween (squash on jump, recoil on piston, camera shake)
- **Cinemachine** — camera system (CinemachineTargetGroup for 2-player framing)

## Architecture Decisions Log

<!-- Quick reference linking to full ADRs in docs/architecture/ -->
- [No ADRs yet — use /architecture-decision to create one]

## Engine Specialists

<!-- Written by /setup-engine when engine is configured. -->
<!-- Read by /code-review, /architecture-decision, /architecture-review, and team skills -->
<!-- to know which specialist to spawn for engine-specific validation. -->

- **Primary**: unity-specialist
- **Language/Code Specialist**: unity-specialist (C# review — primary covers it)
- **Shader Specialist**: unity-shader-specialist (Shader Graph, HLSL, URP/HDRP materials)
- **UI Specialist**: unity-ui-specialist (UI Toolkit UXML/USS, UGUI Canvas, runtime UI)
- **Additional Specialists**: unity-addressables-specialist (asset loading, memory management — invoke if Addressables are actively used), unity-dots-specialist (ECS/Jobs/Burst — invoke only if DOTS patterns are adopted; not default for this project)
- **Routing Notes**: Invoke primary for architecture and general C# code review. Invoke shader specialist for rendering and VFX. Invoke UI specialist for all interface implementation. Do NOT invoke DOTS specialist unless ECS is explicitly adopted — this is a MonoBehaviour/NetworkBehaviour project.

### File Extension Routing

<!-- Skills use this table to select the right specialist per file type. -->
<!-- If a row says [TO BE CONFIGURED], fall back to Primary for that file type. -->

| File Extension / Type | Specialist to Spawn |
|-----------------------|---------------------|
| Game code (.cs files) | unity-specialist |
| Shader / material files (.shader, .shadergraph, .mat) | unity-shader-specialist |
| UI / screen files (.uxml, .uss, Canvas prefabs) | unity-ui-specialist |
| Scene / prefab / level files (.unity, .prefab) | unity-specialist |
| Native extension / plugin files (.dll, native plugins) | unity-specialist |
| Photon Fusion NetworkBehaviour (.cs) | unity-specialist |
| General architecture review | unity-specialist |
