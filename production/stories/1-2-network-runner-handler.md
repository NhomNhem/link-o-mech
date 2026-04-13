# Story 1-2: NetworkRunnerHandler — host/client start, auto-join room

**Sprint**: 1
**Epic**: Foundation
**ID**: 1-2
**Priority**: Must-Have
**Status**: In Progress
**Estimate**: 0.5 days
**Layer**: Foundation
**Type**: Integration
**Manifest Version**: 2026-04-14
**TR-ID**: N/A (jam — no formal TR registry entries yet)
**ADR Governing Implementation**: N/A (jam — no ADRs written yet)
**Blocker**: 1-1 (Complete ✅)

---

## Story

As a developer, I need the NetworkRunnerHandler to successfully start a Photon Fusion 2
session in AutoHostOrClient mode so that 2 clients can connect to the same room in the
editor, proving the multiplayer foundation is functional.

---

## Acceptance Criteria

- [ ] `NetworkRunnerHandler.StartGame()` completes without errors in Play mode
- [ ] Console logs `[NetworkRunnerHandler] Session started successfully.` when connection succeeds
- [ ] In a 2-client scenario (editor + standalone or two editors), both clients join the same room (`DefaultRoomName = "LinkOMech"`)
- [ ] `OnPlayerJoined` fires and logs for each connected client
- [ ] `OnConnectedToServer` fires and logs on successful connect
- [ ] `GameBootstrapper._autoStart = true` causes session to start automatically on Bootstrap scene load
- [ ] No compile errors; 0 errors in Unity console on scene load (before Play)

---

## Implementation Notes

### Existing Implementation (from Story 1-1)

`Assets/Scripts/Networking/NetworkRunnerHandler.cs` already implements the full
Story 1-2 scope:

- `StartGame(GameMode mode = GameMode.AutoHostOrClient)` — builds `StartGameArgs` using
  `GameConstants.DefaultRoomName` and `GameConstants.MaxPlayers`, calls `_runner.StartGame(args)`
- `Shutdown()` — tears down the runner
- `CreateOrGetRunner()` — creates or reuses a `NetworkRunner` on the GameObject
- All 19 `INetworkRunnerCallbacks` methods implemented (key ones log, others are stubs)
- `OnPlayerJoined`, `OnPlayerLeft`, `OnConnectedToServer`, `OnDisconnectedFromServer`,
  `OnShutdown` all log with `[NetworkRunnerHandler]` prefix

### Pre-requisite: Photon AppID

The 2-client connection test **requires** a valid Photon AppID:

1. Go to [dashboard.photonengine.com](https://dashboard.photonengine.com)
2. Create an app (type: Fusion 2)
3. Copy the App ID
4. Open `Assets/Photon/Fusion/Resources/PhotonAppSettings.asset` in Inspector
5. Paste App ID into the `App Id Fusion` field

Without the AppID, `StartGame` will fail with an authentication error.

### `StartGameArgs` used

```csharp
var args = new StartGameArgs
{
    GameMode    = mode,                                      // AutoHostOrClient
    SessionName = GameConstants.DefaultRoomName,             // "LinkOMech"
    PlayerCount = GameConstants.MaxPlayers,                  // 2
    Scene       = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
    SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
};
```

### 2-client Test Procedure

1. Enter valid AppID in `PhotonAppSettings.asset`
2. Press Play in Unity Editor (becomes host or first client)
3. Build a standalone player OR open a second editor instance
4. Press Play in second editor — should auto-join the same room
5. Console should show `Player joined: 1` and `Player joined: 2` on both instances

---

## Out of Scope

- PlayerSpawner logic (story 1-3)
- Any player GameObject instantiation
- Input handling
- Scene loading beyond the Bootstrap scene

---

## Test Evidence

- **Type**: Integration (manual verification — 2-client Play-mode test)
- **Evidence**: `production/qa/evidence/1-2-network-runner-handler-evidence.md`
- **Automated tests**: None — network session start cannot be unit-tested headlessly
- **Manual check**: Play-mode test with valid Photon AppID; observe console logs for
  `[NetworkRunnerHandler] Session started successfully.` and `Player joined` messages

---

## Dependencies

- Story 1-1: Project Boot — **Complete** ✅

---

## Completed

Date:
Notes:
