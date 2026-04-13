# Story 1-1: Project Boot

**Sprint**: 1
**Epic**: Foundation
**ID**: 1-1
**Priority**: Must-Have
**Status**: Complete
**Estimate**: 0.5 days
**Layer**: Foundation
**Type**: Config/Data + Integration
**Manifest Version**: 2026-04-14
**TR-ID**: N/A (jam — no formal TR registry entries yet)
**ADR Governing Implementation**: N/A (jam — no ADRs written yet)
**Blocker**: —

---

## Story

As a developer, I need the Unity project configured with all required packages
(Photon Fusion 2, VContainer, DOTween, Cinemachine) and the correct folder structure
so that all subsequent stories have a stable foundation to build on.

---

## Acceptance Criteria

- [x] Unity 6.3 LTS project opens without errors
- [x] Photon Fusion 2 SDK imported and visible in Package Manager
- [x] VContainer imported and visible in Package Manager
- [x] DOTween imported and visible in Package Manager
- [x] Cinemachine imported and visible in Package Manager
- [x] Folder structure matches GDD layout (src/, assets/, design/, tests/, production/)
- [x] Bootstrap scene created in Assets/Scenes/
- [x] URP (Universal Render Pipeline) configured as the active render pipeline
- [x] Project settings: WebGL as build target, target framerate 60fps
- [?] Two clients can connect in the editor using Photon Fusion 2 (NetworkRunner starts) — DEFERRED: requires valid Photon AppID + Play mode test

---

## Implementation Notes

This is the foundation story. No game logic — just project scaffolding.

### Folder structure to create under Assets/:
```
Assets/
├── Scenes/
│   └── Bootstrap.unity
├── Scripts/
│   ├── Core/
│   ├── Networking/
│   ├── Player/
│   ├── Modules/
│   ├── Interaction/
│   └── UI/
├── Prefabs/
│   ├── Player/
│   └── Modules/
├── Settings/
│   └── (URP settings assets)
└── Resources/
```

### Package import order:
1. URP — via Package Manager (com.unity.render-pipelines.universal)
2. Cinemachine — via Package Manager (com.unity.cinemachine)
3. DOTween — via Asset Store or Package Manager
4. VContainer — via Package Manager (com.hadashigames.vcontainer)
5. Photon Fusion 2 — manual import from Photon dashboard SDK download

### NetworkRunnerHandler stub:
Create a minimal `NetworkRunnerHandler.cs` in `Assets/Scripts/Networking/` that
can start a Fusion session (host or client mode). This will be expanded in story 1-2.

---

## Out of Scope

- PlayerSpawner logic (story 1-2)
- Any gameplay code
- Level design
- Input system configuration (handled per-story)

---

## Test Evidence

- **Type**: Config/Data + Integration (manual verification)
- **Evidence**: `production/qa/evidence/1-1-project-boot-evidence.md`
- **Automated tests**: None — package import and scene creation cannot be unit-tested headlessly
- **Manual check**: Open project, verify all packages appear in Package Manager, verify Bootstrap scene exists

---

## Dependencies

None — first story.

---

## Completed

Date: 2026-04-14
Notes: —

---

## Completion Notes

**Completed**: 2026-04-14  
**Criteria**: 9/10 auto-verified; 1 deferred (AC-10: 2-client Photon connect — requires AppID + Play mode)  
**Deviations**: None from stated story scope. Three extra files created for VContainer 1.17.0 ScriptableObject-based root scope setup (`RootLifetimeScope.cs`, `RootLifetimeScope.prefab`, `VContainerSettings.asset`) — directed by user, matches current VContainer API.  
**Test Evidence**: Config/Data + Integration — no automated unit tests required. Manual verification via Unity MCP (0 compile errors, all packages confirmed, Bootstrap scene wired, URP active, build target WebGL). Evidence doc: `production/qa/evidence/1-1-project-boot-evidence.md`.  
**Code Review**: Skipped (lean mode).  
**Deferred item**: Enter Photon AppID in `Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` and run Play-mode 2-client test before Story 1-2 is marked complete.
