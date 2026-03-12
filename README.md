# Game AI Algorithms

> A* Pathfinding and game AI pattern implementations in C# — optimized for grid-based game navigation.

![C#](https://img.shields.io/badge/Language-C%23-green)
![Algorithm](https://img.shields.io/badge/Category-Game_AI-blue)
![License](https://img.shields.io/badge/License-MIT-yellow)

---

## A* Pathfinding

Shortest path finding on a 2D grid with obstacles using the A* algorithm.

### Problem

Given a 2D grid where `0` = walkable and `1` = obstacle, find the shortest path from start to goal using four-directional movement.

### Algorithm

- **Heuristic**: Manhattan distance (`|x1-x2| + |y1-y2|`)
- **Data Structure**: Priority queue (min-heap) for open set
- **Path Reconstruction**: Parent dictionary backtracking
- **Time Complexity**: `O(m·n · log(m·n))`

### Example

```
Grid (5×5):              Shortest Path:
0 0 0 0 0                S → → ↓ .
0 1 1 0 0                . . . ↓ .
0 0 0 0 1                . . . ↓ .
0 1 0 0 0                . . . → →
0 0 0 1 G                . . . . G

Path length: 9 steps
```

### Implementation Highlights

```csharp
// Priority queue-based open set for O(log n) extraction
// Dictionary<(int,int), int> for g-scores
// HashSet for visited tracking
// Parent map for path reconstruction
```

---

## Tech Stack

| Component | Details |
|-----------|---------|
| **Language** | C# |
| **Pattern** | A* Search Algorithm |
| **Use Cases** | Grid-based games, NPC navigation, tower defense pathfinding |
| **Complexity** | Time: O(m·n·log(m·n)), Space: O(m·n) |

---

## License

MIT License
