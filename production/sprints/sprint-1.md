# Sprint 1 — 2026-04-14 to 2026-04-26 (13 Days)

## Sprint Goal
Build a stable, playable 2-player co-op WebGL prototype: working multiplayer
room, synced movement, link mechanic, two abilities, basic puzzle interactions,
and 2–3 polished levels.

## Capacity
- Total days: 13 (solo dev)
- Buffer (20%): ~2.5 days reserved for debugging/unplanned
- Available: ~10.5 days of focused implementation

## Tasks

### Must Have (Critical Path)
| ID | Task | Owner | Est. Days | Dependencies | Acceptance Criteria |
|----|------|-------|-----------|--------------|---------------------|
| 1-1 | Project boot: Unity 6.3 + Photon Fusion 2 + VContainer + DOTween + Cinemachine setup | Solo | 0.5 | — | Project opens, packages imported, folder structure matches GDD layout |
| 1-2 | NetworkRunnerHandler — host/client start, auto-join room | Solo | 0.5 | 1-1 | 2 clients connect to same room in editor |
| 1-3 | PlayerSpawner — spawn player prefab per connected client | Solo | 0.5 | 1-2 | Each connected client spawns one player; no duplicate spawns |
| 1-4 | PlayerController — horizontal move (A/D) + jump (Space), synced via NetworkTransform | Solo | 1.0 | 1-3 | Both players see each other move; no teleport jitter |
| 1-5 | NetworkLinker — proximity detect, Space to link/unlink, positional offset follow (NO joints/parenting) | Solo | 1.5 | 1-4 | Players link and move as unit; unlink works; distance clamped |
| 1-6 | PistonModule — expand axis, push object/lift group, DOTween motion | Solo | 1.0 | 1-5 | Ability triggers correctly; works while linked |
| 1-7 | MagnetModule — stick to metal surface, freeze group position | Solo | 0.5 | 1-5 | Magnet triggers correctly; group freezes; releases cleanly |
| 1-8 | Interaction system — IInteractable interface, Button, Door | Solo | 0.5 | 1-6 | Button opens door; ability can trigger interaction |
| 1-9 | Cinemachine TargetGroup camera framing both players | Solo | 0.5 | 1-4 | Camera frames both players at all times; no hard cuts |
| 1-10 | Level 1 — Intro (teach move + link + basic piston, simple goal) | Solo | 1.0 | 1-5, 1-6, 1-8 | Level is completable; teaches all intro mechanics |
| 1-11 | Level 2 — Combine (piston + magnet, vertical puzzle, timing element) | Solo | 0.5 | 1-7, 1-10 | Level is completable; requires both modules |
| 1-12 | Level 3 — Chaos (moving platforms, stressed link system) | Solo | 0.5 | 1-5, 1-11 | Level completable; link system holds under stress |
| 1-13 | WebGL build — texture compression, disable realtime light, reduce draw calls, test Chrome/Edge | Solo | 0.5 | 1-10, 1-11, 1-12 | Build loads and plays in Chrome and Edge without crash |

### Should Have
| ID | Task | Owner | Est. Days | Dependencies | Acceptance Criteria |
|----|------|-------|-----------|--------------|---------------------|
| 2-1 | Game feel — DOTween squash on jump, recoil on piston, camera shake | Solo | 0.5 | 1-4, 1-6 | Visual juice present; no feel-related crashes |
| 2-2 | Network chaos pass — interpolation, local smoothing, distance clamp tuning | Solo | 1.0 | 1-5 | No visible jitter under normal 2-player play |
| 2-3 | Edge case fixes — leader-switching conflict, reset logic, 3-player guard | Solo | 0.5 | 1-5 | Link system stable under rapid link/unlink and reconnect |
| 2-4 | Simple UI — main menu, instruction text overlay | Solo | 0.5 | 1-13 | Menu works; instructions visible in WebGL build |

### Nice to Have
| ID | Task | Owner | Est. Days | Dependencies | Acceptance Criteria |
|----|------|-------|-----------|--------------|---------------------|
| 3-1 | SFX — metal link sound, piston fire sound | Solo | 0.5 | 2-1 | Audio plays on link and piston events |
| 3-2 | VFX — spark on link, smoke on piston | Solo | 0.5 | 2-1 | Particles play on events; no performance hit in WebGL |
| 3-3 | LogicBot — ray trigger interaction (optional puzzle element) | Solo | 0.5 | 1-8 | LogicBot triggers IInteractable when ray hits target |

## Carryover from Previous Sprint
None — first sprint.

## Risks
| Risk | Probability | Impact | Mitigation |
|------|------------|--------|------------|
| Photon Fusion 2 incompatible with Unity 6.3 | Low | Critical | Verify SDK version on Day 1 before any other work; fallback to Fusion 2 + Unity 2022.3 if needed |
| Link system desyncs under Fusion tick model | Medium | High | Use Networked property for leader/follower state; test early (Day 3); fallback to local-only if networking breaks |
| WebGL memory overflow (> 512MB) | Medium | High | Keep all textures ≤ 512×512, compressed; no realtime lights; profile early not on Day 13 |
| DOTween + Fusion FixedUpdateNetwork conflicts | Low | Medium | Keep DOTween calls in Update(), not FixedUpdateNetwork() |
| Scope creep from polish impulse | High | Medium | Follow YAGNI — if a task takes > 4h, simplify or cut |

## Dependencies on External Factors
- Photon Fusion 2 SDK download from Photon dashboard (requires account)
- VContainer, DOTween, Cinemachine via Package Manager / Asset Store
- WebGL test requires Chrome/Edge browser on build machine

## Definition of Done for this Sprint
- [ ] All Must Have tasks (1-1 through 1-13) completed and verified
- [ ] 2 players can connect, link, use both modules, and complete Level 1
- [ ] WebGL build loads and plays in Chrome and Edge without crash or memory overflow
- [ ] Link system uses positional offset (NO joints, NO parenting) per architecture rule
- [ ] No `GameObject.Find()` at runtime — VContainer DI used throughout
- [ ] No GC allocations in `FixedUpdateNetwork()` tick loop
- [ ] Smoke check passed (manual 2-player session through all 3 levels)

> ⚠️ **No QA Plan**: This sprint was started without a QA plan. Run `/qa-plan sprint`
> before the last story is implemented. The Production → Polish gate requires a QA
> sign-off report, which requires a QA plan.
