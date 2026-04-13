// Story 1-1: Project Boot — NetworkRunnerHandler
// Wraps Photon Fusion 2 NetworkRunner for host/client start and auto-join room.
// Implements INetworkRunnerCallbacks — all callbacks stubbed; logic added per story.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using LinkOMech.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LinkOMech.Networking
{
    /// <summary>
    /// Manages the Photon Fusion 2 NetworkRunner lifecycle.
    /// Attach to a persistent GameObject in the Bootstrap scene.
    /// Call <see cref="StartGame"/> to connect as host or client.
    /// </summary>
    public class NetworkRunnerHandler : MonoBehaviour, INetworkRunnerCallbacks
    {
        // ── Inspector ────────────────────────────────────────────────────────

        [Tooltip("Optional: drag a NetworkRunner prefab here. If null, a runner is created at runtime.")]
        [SerializeField] private NetworkRunner _runnerPrefab;

        // ── State ────────────────────────────────────────────────────────────

        private NetworkRunner _runner;
        private bool _isStarting;

        // ── Public API ───────────────────────────────────────────────────────

        /// <summary>
        /// The active <see cref="NetworkRunner"/> instance, or null if not yet started.
        /// </summary>
        public NetworkRunner Runner => _runner;

        /// <summary>
        /// Starts a Fusion session. Call from <see cref="GameBootstrapper"/> or a UI button.
        /// </summary>
        /// <param name="mode">
        /// <see cref="GameMode.AutoHostOrClient"/> for auto-detect (recommended for jam).
        /// </param>
        public async Task StartGame(GameMode mode = GameMode.AutoHostOrClient)
        {
            if (_isStarting)
            {
                Debug.LogWarning("[NetworkRunnerHandler] StartGame called while already starting — ignored.");
                return;
            }

            if (_runner != null && _runner.IsRunning)
            {
                Debug.LogWarning("[NetworkRunnerHandler] Runner is already running — call Shutdown first.");
                return;
            }

            _isStarting = true;

            _runner = CreateOrGetRunner();
            _runner.AddCallbacks(this);

            var args = new StartGameArgs
            {
                GameMode       = mode,
                SessionName    = GameConstants.DefaultRoomName,
                PlayerCount    = GameConstants.MaxPlayers,
                Scene          = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
                SceneManager   = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            };

            Debug.Log($"[NetworkRunnerHandler] Starting Fusion — mode: {mode}, room: {GameConstants.DefaultRoomName}");

            var result = await _runner.StartGame(args);

            _isStarting = false;

            if (result.Ok)
            {
                Debug.Log("[NetworkRunnerHandler] Session started successfully.");
            }
            else
            {
                Debug.LogError($"[NetworkRunnerHandler] Failed to start: {result.ShutdownReason}");
            }
        }

        /// <summary>Shuts down the active runner, if any.</summary>
        public async Task Shutdown()
        {
            if (_runner != null && _runner.IsRunning)
            {
                await _runner.Shutdown();
                Debug.Log("[NetworkRunnerHandler] Runner shut down.");
            }
        }

        // ── Private helpers ──────────────────────────────────────────────────

        private NetworkRunner CreateOrGetRunner()
        {
            if (_runnerPrefab != null)
            {
                return Instantiate(_runnerPrefab, transform);
            }

            // Create a runner on this GameObject if no prefab is configured.
            var existing = GetComponent<NetworkRunner>();
            return existing != null ? existing : gameObject.AddComponent<NetworkRunner>();
        }

        // ── INetworkRunnerCallbacks ──────────────────────────────────────────
        // All methods are required by the interface. Only key ones log — others
        // are stubs to be filled in by later stories.

        /// <inheritdoc/>
        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("[NetworkRunnerHandler] Connected to server.");
        }

        /// <inheritdoc/>
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            Debug.Log($"[NetworkRunnerHandler] Disconnected: {reason}");
        }

        /// <inheritdoc/>
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"[NetworkRunnerHandler] Player joined: {player}");
        }

        /// <inheritdoc/>
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"[NetworkRunnerHandler] Player left: {player}");
        }

        /// <inheritdoc/>
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Debug.Log($"[NetworkRunnerHandler] Shutdown: {shutdownReason}");
            _runner = null;
        }

        // ── Stubs (filled by later stories) ─────────────────────────────────

        /// <inheritdoc/>
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

        /// <inheritdoc/>
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

        /// <inheritdoc/>
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        /// <inheritdoc/>
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        /// <inheritdoc/>
        public void OnInput(NetworkRunner runner, NetworkInput input) { }

        /// <inheritdoc/>
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        /// <inheritdoc/>
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        /// <inheritdoc/>
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        /// <inheritdoc/>
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

        /// <inheritdoc/>
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

        /// <inheritdoc/>
        public void OnSceneLoadDone(NetworkRunner runner) { }

        /// <inheritdoc/>
        public void OnSceneLoadStart(NetworkRunner runner) { }

        /// <inheritdoc/>
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

        /// <inheritdoc/>
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    }
}
