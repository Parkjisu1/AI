# 게임 AI 알고리즘 | Game AI Algorithms

> A* 패스파인딩 및 게임 AI 패턴 구현 (C#) — 그리드 기반 게임 내비게이션에 최적화
>
> A* Pathfinding and game AI pattern implementations in C# — optimized for grid-based game navigation.

![C#](https://img.shields.io/badge/Language-C%23-green)
![Algorithm](https://img.shields.io/badge/Category-Game_AI-blue)
![License](https://img.shields.io/badge/License-MIT-yellow)

---

## A* 패스파인딩 | A* Pathfinding

장애물이 있는 2D 그리드에서 A* 알고리즘을 사용한 최단 경로 탐색.

Shortest path finding on a 2D grid with obstacles using the A* algorithm.

### 문제 정의 | Problem

`0` = 이동 가능, `1` = 장애물인 2D 그리드에서 시작점부터 목표점까지 4방향 이동으로 최단 경로를 찾습니다.

Given a 2D grid where `0` = walkable and `1` = obstacle, find the shortest path from start to goal using four-directional movement.

### 알고리즘 | Algorithm

| 항목 | 상세 |
|------|------|
| **휴리스틱** | 맨해튼 거리 (`\|x1-x2\| + \|y1-y2\|`) |
| **자료구조** | 우선순위 큐 (최소 힙) 기반 오픈 셋 |
| **경로 복원** | 부모 딕셔너리 역추적 |
| **시간 복잡도** | `O(m·n · log(m·n))` |
| **공간 복잡도** | `O(m·n)` |

### 예시 | Example

```
그리드 (5×5):              최단 경로:
0 0 0 0 0                S → → ↓ .
0 1 1 0 0                . . . ↓ .
0 0 0 0 1                . . . ↓ .
0 1 0 0 0                . . . → →
0 0 0 1 G                . . . . G

경로 길이: 9 스텝
```

### 핵심 구현 | Implementation

```csharp
// 우선순위 큐 기반 오픈 셋 — O(log n) 추출
// Dictionary<(int,int), int> — g-score 관리
// HashSet — 방문 노드 추적
// Parent Map — 경로 역추적 복원
```

---

## GameMaker

게임 개발 프로세스 관련 참조 자료.

Game development process reference materials.

| 항목 | 설명 |
|------|------|
| **ART** | 아트 에셋 제작 참조 |
| **CICD** | CI/CD 파이프라인 구성 |
| **Planning** | 게임 기획 프로세스 |
| **System** | 시스템 설계 참조 |

---

## 적용 분야 | Use Cases

- 그리드 기반 게임의 NPC 내비게이션
- 타워 디펜스 적 경로 계산
- 턴제 전략 게임 이동 범위 계산
- 퍼즐 게임 경로 탐색

---

## 라이선스 | License

MIT License
