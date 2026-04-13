// Story 1-1: Project Boot — GameBootstrapper
// Entry-point MonoBehaviour for the Bootstrap scene.
// Wires up the NetworkRunnerHandler and auto-starts the Fusion session.

using System.Threading.Tasks;
using Fusion;
using LinkOMech.Networking;
using UnityEngine;

namespace LinkOMech.Core
{
    /// <summary>
    /// Scene entry point for the Bootstrap scene.
    /// Attach to a persistent root GameObject (e.g., "GameBootstrapper").
    /// Drag <see cref="NetworkRunnerHandler"/> in the inspector before entering Play.
    /// </summary>
    public class GameBootstrapper : MonoBehaviour
    {
        // ── Inspector ────────────────────────────────────────────────────────

        [Tooltip("The NetworkRunnerHandler component in this scene.")]
        [SerializeField] private NetworkRunnerHandler _networkRunnerHandler;

        [Tooltip("If true, automatically starts the Fusion session on Start(). " +
                 "Disable for manual control (e.g., from a main menu button).")]
        [SerializeField] private bool _autoStart = true;

        // ── Unity Lifecycle ───────────────────────────────────────────────────

        private async void Start()
        {
            // Set target framerate for WebGL builds (editor default is -1/uncapped).
            Application.targetFrameRate = GameConstants.TargetFrameRate;

            if (!_autoStart)
            {
                Debug.Log("[GameBootstrapper] Auto-start disabled — call StartNetwork() manually.");
                return;
            }

            if (_networkRunnerHandler == null)
            {
                Debug.LogError("[GameBootstrapper] NetworkRunnerHandler reference is null! " +
                               "Drag it into the inspector on the GameBootstrapper object.");
                return;
            }

            await StartNetwork();
        }

        // ── Public API ───────────────────────────────────────────────────────

        /// <summary>
        /// Starts the Fusion session in AutoHostOrClient mode.
        /// Called automatically if <c>_autoStart</c> is true, or manually from UI.
        /// </summary>
        public async Task StartNetwork()
        {
            Debug.Log("[GameBootstrapper] Starting network session...");
            await _networkRunnerHandler.StartGame(GameMode.AutoHostOrClient);
        }
    }
}
