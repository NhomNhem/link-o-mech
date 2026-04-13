# QA Evidence: Story 1-1 — Project Boot

**Story**: 1-1 Project Boot  
**Date**: 2026-04-14  
**Tester**: Solo dev  
**Build**: Editor (not yet built — Day 1 setup)

---

## Acceptance Criteria Checklist

### Package Verification (check in Window → Package Manager)

- [ ] **URP** — `com.unity.render-pipelines.universal` 17.3.0 visible in Package Manager
- [ ] **Cinemachine** — `com.unity.cinemachine` 3.1.6 visible in Package Manager
- [ ] **VContainer** — `jp.hadashikick.vcontainer` 1.17.0 visible in Package Manager
- [ ] **New Input System** — `com.unity.inputsystem` 1.19.0 visible in Package Manager
- [ ] **DOTween** — visible in Assets/Plugins/Demigiant/DOTween, DLL importable
- [ ] **Photon Fusion 2** — visible in Assets/Photon/Fusion, build 2.0.12 confirmed

### Project Setup

- [ ] Project opens in Unity 6.3 LTS without console errors
- [ ] URP is set as the active render pipeline (Edit → Project Settings → Graphics → Scriptable Render Pipeline Settings)
- [ ] Build target is WebGL (File → Build Settings → WebGL selected)

### Folder Structure (check in Project window)

- [ ] `Assets/Scripts/Core/` exists
- [ ] `Assets/Scripts/Networking/` exists
- [ ] `Assets/Scripts/Player/` exists
- [ ] `Assets/Scripts/Modules/` exists
- [ ] `Assets/Scripts/Interaction/` exists
- [ ] `Assets/Scripts/UI/` exists
- [ ] `Assets/Prefabs/Player/` exists
- [ ] `Assets/Prefabs/Modules/` exists

### Scripts Compile

- [ ] `GameConstants.cs` compiles with no errors (check Console)
- [ ] `GameBootstrapper.cs` compiles with no errors
- [ ] `NetworkRunnerHandler.cs` compiles with no errors

### Bootstrap Scene

- [ ] Bootstrap scene created at `Assets/Scenes/Bootstrap.unity` (manual step — create in Unity Editor via File → New Scene)
- [ ] `GameBootstrapper` GameObject added to Bootstrap scene
- [ ] `NetworkRunnerHandler` component on same or child GameObject
- [ ] `GameBootstrapper._networkRunnerHandler` field wired in inspector

### Network Connection Test (requires Photon AppID)

- [ ] AppID configured in `Assets/Photon/Fusion/Resources/PhotonAppSettings.asset`
- [ ] Enter Play mode in editor — Console logs: "[NetworkRunnerHandler] Starting Fusion..."
- [ ] Open second editor instance (or build and run) — second client connects
- [ ] Console shows: "[NetworkRunnerHandler] Player joined: X" for each client

---

## Notes / Issues

_Fill in during manual verification._

---

## Result

- [ ] **PASS** — all criteria met
- [ ] **PASS WITH CONDITIONS** — minor issues, acceptable for Day 1
- [ ] **FAIL** — blocking issue, must fix before story 1-2

**Signed off by**: ___________  
**Date**: ___________
