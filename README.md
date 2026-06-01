# Rainbow-Spiral-on-AutoCAD
This C# plugin leverages the AutoCAD .NET API to programmatically generate complex 3D geometry directly within AutoCAD’s database engine. By executing DRAW3DSPIRAL, the script runs a secure transaction that converts parameterized polar math into Cartesian coordinates to instantiate solid 3D spheres with a rolling rainbow color index.

# AutoCAD 3D Parametric Spiral Generator

A high-performance AutoCAD automation plugin built in C# targeting **.NET 8**. This project demonstrates how to bypass manual drafting constraints by interacting directly with AutoCAD's underlying graphical database engine to generate complex, algorithmic 3D geometry instantly.

## Features

* **Parametric Design:** Uses architectural and mathematical principles (Archimedean Spiral equations) to dynamically compute 3D spatial coordinates $(X, Y, Z)$ on the fly.
* **Direct Database Manipulation:** Bypasses standard UI overhead by instantiating `Solid3d` geometry primitives, safely committing them via the native AutoCAD `TransactionManager` model.
* **Procedural Styling:** Implements a localized programmatic color loop mapping to AutoCAD's native indexing system to generate a vibrant, color-shifting 3D tower structure.

---

## Tech Stack & Environment

* **Language:** C#
* **Runtime:** .NET 8.0 SDK
* **IDE:** Visual Studio Code
* **Target Platform:** AutoCAD 2025 / 2026 (via Autodesk AutoCAD .NET NuGet references)

---

## How It Works (The Code Pipeline)

Instead of relying on the canvas front-end, the plugin interacts with AutoCAD as a structured database server. When the custom runtime command is invoked:

1. **Transaction Lifecycle:** Instantiates an isolated transaction block to ensure execution safety and database integrity.
2. **Space Selection:** Requests a pointer to the active drawing session (`MdiActiveDocument`) and opens the current `ModelSpace` table record for writing.
3. **Coordinate Transformation:** Loops through a defined parametric cycle, converting polar formulas $(r, \theta)$ into Cartesian spatial points:
   $$x = r \cdot \cos(\theta)$$
   $$y = r \cdot \sin(\theta)$$
   $$z = i \cdot \text{heightGrowRate}$$
4. **Entity Appending:** Instantiates a 3D Sphere, transforms its location matrix to the computed coordinate point, modifies its color index, and appends it directly to the database layer before committing the transaction.

---

## How to Run the Plugin

### 1. Build the Binary
Clone the repository, open your terminal inside the project directory, and compile the fresh dynamic link library (`.dll`):

dotnet clean
dotnet build
