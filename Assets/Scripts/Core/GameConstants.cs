// Story 1-1: Project Boot — GameConstants
// Central tuning knob registry. All gameplay values live here — NEVER hardcoded
// in game logic. Edit values here and they propagate everywhere automatically.
// Per technical-preferences.md: NO hardcoded values in game code.

namespace LinkOMech.Core
{
    /// <summary>
    /// Project-wide constants and tuning values.
    /// All tunable gameplay parameters belong here.
    /// </summary>
    public static class GameConstants
    {
        // ── Link System ───────────────────────────────────────────────────────

        /// <summary>Maximum distance (units) before the link is force-broken.</summary>
        public const float MaxLinkDistance = 5f;

        /// <summary>Proximity radius (units) to detect a nearby player for linking.</summary>
        public const float LinkProximityRadius = 2f;

        /// <summary>Lerp speed for the follower catching up to the leader position.</summary>
        public const float FollowerLerpSpeed = 15f;

        // ── Player Movement ───────────────────────────────────────────────────

        /// <summary>Horizontal move speed (units/second).</summary>
        public const float MoveSpeed = 6f;

        /// <summary>Jump impulse force applied on Space press.</summary>
        public const float JumpForce = 12f;

        /// <summary>Raycast distance (units) for ground detection beneath the player.</summary>
        public const float GroundCheckDistance = 0.15f;

        // ── Modules ───────────────────────────────────────────────────────────

        /// <summary>How far (units) the PistonModule extends before retracting.</summary>
        public const float PistonExtendDistance = 3f;

        /// <summary>Time (seconds) for one full piston extend animation.</summary>
        public const float PistonDuration = 0.3f;

        /// <summary>Radius (units) within which the MagnetModule snaps to a metal surface.</summary>
        public const float MagnetActivationRadius = 1f;

        // ── Network ───────────────────────────────────────────────────────────

        /// <summary>Photon Fusion 2 session/room name for the jam build.</summary>
        public const string DefaultRoomName = "LinkOMech";

        /// <summary>Hard cap on simultaneous players. Guard checked in PlayerSpawner (Story 1-3).</summary>
        public const int MaxPlayers = 2;

        // ── Rendering ─────────────────────────────────────────────────────────

        /// <summary>Target framerate for the game (60fps). Applied at runtime in GameBootstrapper.</summary>
        public const int TargetFrameRate = 60;

        // ── Camera ────────────────────────────────────────────────────────────

        /// <summary>Minimum orthographic size (or FOV equivalent) when players are close.</summary>
        public const float CameraMinZoom = 5f;

        /// <summary>Maximum orthographic size (or FOV equivalent) when players are far apart.</summary>
        public const float CameraMaxZoom = 15f;
    }
}
