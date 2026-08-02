# HeritageAR

**An Augmented Reality Platform for Digital Preservation of Indian Cultural Heritage**

HeritageAR is a mobile Augmented Reality application built in Unity that lets users place realistic 3D models of Indian heritage monuments in their surroundings and explore them through interactive tours, narrated audio, and educational quizzes.

---

## Overview

Using Unity, AR Foundation, and ARCore, this app brings India's cultural heritage sites into the user's physical space. Users can view detailed 3D monument models, learn their history through an in-app info panel, listen to narrated audio, and test their knowledge with quizzes — all designed to be accessible even in low-bandwidth environments.

---

## Features (Current Build)

- **AR Monument Placement** — Place a 3D monument model directly in front of the camera with a single tap (camera-relative placement using AR Anchors for stability).
- **Touch Controls** — Rotate the placed monument with a one-finger drag, and resize it with a two-finger pinch.
- **Info Panel** — Tap the Info button to view the monument's name and historical background.
- **Audio Narration** — Play/pause narrated audio about the monument, with the button state syncing automatically (Play ↔ Pause) and resetting when narration finishes.
- **Reset Placement** — Clear the current model and instructions reappear, allowing the user to re-place it.
- **Main Menu** — A styled landing screen with navigation to Monuments, Quiz, and Quit.
- **Scene Navigation** — Separate scenes for Main Menu, Monument viewing, and Quiz, connected via Unity's Scene Manager.

---

## Tech Stack

| Technology | Purpose |
|---|---|
| **Unity 2022.3 LTS** | Core game engine |
| **AR Foundation 5.2.0** | Cross-platform AR framework |
| **ARCore** | Android AR support |
| **Blender** | 3D model creation/optimization |
| **C#** | Application scripting |
| **TextMeshPro** | UI text rendering |
| **XR Interaction Toolkit** | Base AR template interaction system |

---

## Project Structure

```
Assets/
├── Models/              # Imported 3D monument models
├── prefabs/              # Prefabbed monument models (e.g., RamMandir)
├── Scripts/
│   ├── PlaceModelInFrontOfCamera.cs   # Camera-relative AR placement + AR Anchor handling
│   ├── InfoPanelController.cs         # Info panel show/hide + content population
│   ├── AudioController.cs             # Narration play/pause/stop logic
│   ├── ObjectManipulator.cs           # Touch-based rotate & pinch-to-scale
│   └── MainMenuController.cs          # Main menu button navigation
├── UI/                   # Sprites, fonts, menu backgrounds
├── Scenes/
│   ├── MainMenu.unity
│   ├── Monuments.unity   (renamed from Level 1)
│   └── Quiz.unity
```

---

## How Placement Works

Instead of relying on floor/plane detection (which proved slow and inconsistent during testing), the app uses **camera-relative placement**:

1. User taps **"Place Monument."**
2. The model spawns a fixed distance in front of the camera's current position and forward direction.
3. An **AR Anchor** is created at that pose, and the model is parented to it — this keeps the model visually stable in real-world space as ARCore continues refining its tracking, minimizing drift.

This approach trades exact floor alignment for speed and reliability, which was prioritized for the prototype milestone. Full plane-detection-based placement remains a possible refinement for future versions.

---

## Setup & Running

1. Clone/open the project in **Unity 2022.3.62f1** (or compatible LTS version).
2. Ensure **AR Foundation**, **ARCore XR Plugin**, and **XR Interaction Toolkit** packages are installed via Package Manager.
3. Switch platform to **Android** (`File → Build Settings → Android → Switch Platform`).
4. Under **Player Settings → XR Plug-in Management → Android**, confirm **ARCore** is enabled.
5. Add all scenes (`MainMenu`, `Monuments`, `Quiz`) to **Scenes In Build**, with `MainMenu` first.
6. Connect an ARCore-supported Android device via USB (with Developer Mode + USB Debugging enabled).
7. **Build and Run.**

---

## Known Limitations

- Placement is camera-relative, not floor-anchored — the model does not sit precisely on real-world surfaces.
- Tracking drift may still occur in poorly lit or low-texture environments, since it depends on ARCore's visual tracking quality.
- Currently supports a single monument (Ram Mandir) with hardcoded info/audio; multi-monument support with a selection menu is planned.
- Quiz content is not yet dynamically loaded from generated question banks.

---

## Roadmap

**Intermediate Version**
- Add interactive hotspots on monument models
- Expand to 3 heritage monuments with a selection screen
- Multilingual audio/text support
- Quiz system with auto-generated (pre-authored) question banks per monument

**Final Version**
- 5 optimized heritage monuments
- Cloud-based model downloading (Firebase) for low-bandwidth delivery
- Full performance optimization and usability testing

**Future Scope**
- AI-powered virtual tour guide (LLM-based)
- VR support for immersive exploration
- Indoor navigation for physical museum integration
- Plane-detection-based placement as an optional accurate mode

---

## Credits

Developed as part of an academic project exploring AR-based digital preservation of Indian cultural heritage.
