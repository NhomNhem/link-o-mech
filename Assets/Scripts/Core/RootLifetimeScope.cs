// Story 1-1: Project Boot — RootLifetimeScope
// Project-wide VContainer dependency injection root.
// This prefab is registered in VContainerSettings (Assets -> Create -> VContainer -> VContainer Settings)
// and is instantiated automatically as DontDestroyOnLoad before any scene loads.
// All project-wide singleton services are registered here.

using VContainer;
using VContainer.Unity;

namespace LinkOMech.Core
{
    /// <summary>
    /// Project-root VContainer LifetimeScope. Survives all scene transitions.
    /// Register project-wide singleton services here (e.g., audio manager, settings, analytics).
    /// Scene-level services belong in a per-scene LifetimeScope that sets this as its parent.
    /// </summary>
    /// <remarks>
    /// Setup:
    /// 1. Create a Prefab from this component (e.g., Assets/Prefabs/Core/RootLifetimeScope.prefab)
    /// 2. Create Assets -> VContainer -> VContainer Settings
    /// 3. Drag the prefab into the "Root Lifetime Scope" field in VContainerSettings
    /// </remarks>
    public class RootLifetimeScope : LifetimeScope
    {
        /// <summary>
        /// Registers project-level services into the root container.
        /// Add new registrations here as global systems are introduced.
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            // ── Project-wide singleton registrations ──────────────────────────
            // Example (story 1-2+): builder.Register<AudioManager>(Lifetime.Singleton);
            // Example (story 1-2+): builder.Register<SettingsService>(Lifetime.Singleton);

            // NetworkRunnerHandler is scene-scoped (lives in Bootstrap scene).
            // Register it in the Bootstrap scene's LifetimeScope, not here.
        }
    }
}
