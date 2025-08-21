# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity RTS (Real-Time Strategy) course project using Unity 6000.2.0f1 with Universal Render Pipeline (URP). The project appears to be following a course structure from GameDevTV/Udemy.

## Build and Development Commands

This is a Unity project, so development happens through the Unity Editor:
- Open the project in Unity Editor (version 6000.2.0f1 or compatible)
- Build through Unity's Build Settings (File → Build Settings)
- No custom build scripts or command-line tools are configured

## Architecture and Structure

### Core Namespaces
- `GameDevTV.RTS.Units` - Main namespace for RTS unit systems

### Key Components

**Unit System:**
- Worker units for resource gathering
- Combat units (Rifleman, Grenadier) 
- Air Transport for unit transportation
- Buildings (Command Post, Barracks, Infantry School, Airport, Supply Hut)

**Resources:**
- Gas and Minerals as gatherable resources
- PoisonGas variant exists

**Animation & IK:**
- Custom IK system for weapon handling (`HoldGunIK.cs`)
- Animation controllers for different unit types

**UI System:**
- Action buttons and unit buttons
- Building queue management
- Progress bars for various game states
- Control group system

**Input:**
- Uses Unity's New Input System
- Input actions defined in `InputSystem_Actions.inputactions`

### Asset Organization
- `/Assets/Units/` - All unit prefabs organized by type
- `/Assets/Scripts/` - C# scripts (currently minimal, most logic likely in prefabs)
- `/Assets/UI/` - UI prefabs and components
- `/Assets/Scenes/` - Main game scene and sample scene
- `/Assets/Textures/` - Icons and UI graphics
- `/Assets/Materials/` - Shader materials for various effects

### Dependencies
Key Unity packages:
- Universal Render Pipeline (URP) 17.2.0
- Input System 1.14.1
- AI Navigation 2.0.8
- Cinemachine 3.1.4
- Behavior Trees 1.0.12
- Timeline 1.8.7

The project uses URP for rendering with custom shaders for effects like ghost placement and water.