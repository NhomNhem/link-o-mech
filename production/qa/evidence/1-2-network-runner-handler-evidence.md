# QA Evidence — Story 1-2: NetworkRunnerHandler

**Story**: `production/stories/1-2-network-runner-handler.md`
**Type**: Integration
**Date**: 2026-04-14
**Verdict**: PENDING PLAY-MODE TEST

---

## Automated Verification (Unity MCP — Editor State)

| Check | Result |
|-------|--------|
| Compile errors | ✅ 0 errors |
| `NetworkRunnerHandler.cs` exists | ✅ `Assets/Scripts/Networking/NetworkRunnerHandler.cs` |
| `INetworkRunnerCallbacks` implemented | ✅ All 19 methods present |
| `StartGame(GameMode)` implemented | ✅ Builds `StartGameArgs` from `GameConstants` |
| `GameConstants.DefaultRoomName` | ✅ `"LinkOMech"` |
| `GameConstants.MaxPlayers` | ✅ `2` |
| `NetworkRunner` prefab wired | ✅ `Assets/Prefabs/Core/NetworkRunner.prefab` (has `Fusion.NetworkRunner` component) |
| Bootstrap scene wired | ✅ `GameBootstrapper._networkRunnerHandler` → instanceID -126606 |
| `_autoStart = true` | ✅ Session starts automatically on Bootstrap scene load |
| Photon AppID set | ✅ `d7ca69e7-df62-4a80-b419-6e22eb2d5138` in `PhotonAppSettings.asset` |

---

## Manual Play-Mode Test (Required)

**Status**: ⏳ PENDING — requires entering Play mode

### Test Steps

1. Ensure Photon AppID is set in `Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` ✅ (already set)
2. Open `Assets/Scenes/Bootstrap.unity` in Unity Editor ✅ (already active scene)
3. Press **Play** in the Editor
4. Observe console for:
   - `[NetworkRunnerHandler] Starting Fusion — mode: AutoHostOrClient, room: LinkOMech`
   - `[NetworkRunnerHandler] Session started successfully.`
   - `[NetworkRunnerHandler] Connected to server.`
   - `[NetworkRunnerHandler] Player joined: 1`
5. Build a standalone player **OR** use Unity Multiplayer Play Mode (if configured)
6. Launch second instance and press Play
7. Observe both consoles for `[NetworkRunnerHandler] Player joined: 2`

### Expected Results

| Criterion | Expected Log |
|-----------|-------------|
| Session starts | `[NetworkRunnerHandler] Session started successfully.` |
| Server connect | `[NetworkRunnerHandler] Connected to server.` |
| Player 1 joins | `[NetworkRunnerHandler] Player joined: 1` |
| Player 2 joins | `[NetworkRunnerHandler] Player joined: 2` (both instances) |

### Actual Results

> ⏳ To be filled after Play-mode test

---

## Notes

- The `NetworkRunnerHandler.cs` implementation was created as part of Story 1-1 and
  satisfies all Story 1-2 acceptance criteria at the code level.
- The only gap is Play-mode runtime verification, which requires a valid Photon AppID
  and active network session — confirmed: AppID is already configured.
- 2-client test can be done with Unity's Multiplayer Play Mode package or by building
  a standalone and running alongside the Editor.
