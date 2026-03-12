using System;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// A* 패스파인딩 알고리즘 구현
    /// A* Pathfinding algorithm implementation
    ///
    /// 2D 그리드에서 장애물을 피해 최단 경로를 탐색합니다.
    /// Finds the shortest path on a 2D grid while avoiding obstacles.
    /// </summary>
    public class AStarPathfinding
    {
        // 4방향 이동: 상, 하, 좌, 우 | Four-directional movement: up, down, left, right
        private static readonly int[] DRow = { -1, 1, 0, 0 };
        private static readonly int[] DCol = { 0, 0, -1, 1 };

        /// <summary>
        /// A* 알고리즘을 사용하여 시작점에서 목표점까지의 최단 경로를 찾습니다.
        /// Finds the shortest path from start to goal using the A* algorithm.
        /// </summary>
        /// <param name="grid">2D 그리드 (0 = 이동 가능, 1 = 장애물) | 2D grid (0 = walkable, 1 = obstacle)</param>
        /// <param name="start">시작 좌표 (행, 열) | Start coordinate (row, col)</param>
        /// <param name="goal">목표 좌표 (행, 열) | Goal coordinate (row, col)</param>
        /// <returns>경로 좌표 리스트, 경로가 없으면 null | List of path coordinates, or null if no path</returns>
        public static List<(int Row, int Col)> FindPath(int[,] grid, (int Row, int Col) start, (int Row, int Col) goal)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // 시작점 또는 목표점이 장애물인 경우 경로 없음
            // No path if start or goal is an obstacle
            if (grid[start.Row, start.Col] == 1 || grid[goal.Row, goal.Col] == 1)
                return null;

            // 오픈 셋: 탐색 대기 중인 노드 (우선순위 큐)
            // Open set: nodes waiting to be explored (priority queue)
            var openSet = new PriorityQueue<(int Row, int Col)>();

            // g-score: 시작점에서 각 노드까지의 실제 비용
            // g-score: actual cost from start to each node
            var gScore = new Dictionary<(int, int), int>();

            // 부모 맵: 경로 역추적을 위한 이전 노드 기록
            // Parent map: record of previous node for path reconstruction
            var parent = new Dictionary<(int, int), (int, int)>();

            // 클로즈드 셋: 이미 탐색 완료된 노드
            // Closed set: nodes already fully explored
            var closedSet = new HashSet<(int, int)>();

            // 시작 노드 초기화 | Initialize start node
            gScore[start] = 0;
            int startF = ManhattanDistance(start, goal);
            openSet.Enqueue(start, startF);

            while (!openSet.IsEmpty)
            {
                // f-score가 가장 낮은 노드를 꺼냄
                // Dequeue node with lowest f-score
                var current = openSet.Dequeue();

                // 목표에 도달한 경우 경로를 복원하여 반환
                // If goal reached, reconstruct and return path
                if (current == goal)
                    return ReconstructPath(parent, current);

                // 이미 방문한 노드는 건너뜀 | Skip already visited nodes
                if (closedSet.Contains(current))
                    continue;

                closedSet.Add(current);

                // 4방향 인접 노드 탐색 | Explore 4-directional neighbors
                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.Row + DRow[i];
                    int newCol = current.Col + DCol[i];
                    var neighbor = (newRow, newCol);

                    // 그리드 범위 확인 | Check grid bounds
                    if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
                        continue;

                    // 장애물 또는 이미 방문한 노드 건너뜀
                    // Skip obstacles or already visited nodes
                    if (grid[newRow, newCol] == 1 || closedSet.Contains(neighbor))
                        continue;

                    // 새로운 g-score 계산 (이동 비용 = 1)
                    // Calculate new g-score (movement cost = 1)
                    int tentativeG = gScore[current] + 1;

                    // 더 좋은 경로를 찾은 경우 업데이트
                    // Update if a better path is found
                    if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                    {
                        gScore[neighbor] = tentativeG;
                        parent[neighbor] = current;

                        // f = g + h (맨해튼 거리 휴리스틱)
                        // f = g + h (Manhattan distance heuristic)
                        int fScore = tentativeG + ManhattanDistance(neighbor, goal);
                        openSet.Enqueue((newRow, newCol), fScore);
                    }
                }
            }

            // 경로를 찾지 못한 경우 | No path found
            return null;
        }

        /// <summary>
        /// 맨해튼 거리 휴리스틱 함수
        /// Manhattan distance heuristic function
        ///
        /// |x1-x2| + |y1-y2| 로 두 점 사이의 추정 거리를 계산합니다.
        /// Calculates estimated distance between two points as |x1-x2| + |y1-y2|.
        /// </summary>
        private static int ManhattanDistance((int Row, int Col) a, (int Row, int Col) b)
        {
            return Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col);
        }

        /// <summary>
        /// 부모 맵을 역추적하여 전체 경로를 복원합니다.
        /// Reconstructs the full path by backtracking through the parent map.
        /// </summary>
        private static List<(int Row, int Col)> ReconstructPath(
            Dictionary<(int, int), (int, int)> parent,
            (int Row, int Col) current)
        {
            var path = new List<(int Row, int Col)> { current };

            while (parent.ContainsKey(current))
            {
                current = parent[current];
                path.Add(current);
            }

            // 시작점부터 목표점 순서로 뒤집기 | Reverse to get start-to-goal order
            path.Reverse();
            return path;
        }

        /// <summary>
        /// 그리드와 경로를 시각화하여 콘솔에 출력합니다.
        /// Visualizes the grid and path on the console.
        /// </summary>
        private static void PrintGrid(int[,] grid, List<(int Row, int Col)> path)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // 경로 좌표를 HashSet에 저장하여 빠른 조회
            // Store path coordinates in HashSet for fast lookup
            var pathSet = new HashSet<(int, int)>();
            if (path != null)
            {
                foreach (var p in path)
                    pathSet.Add(p);
            }

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (pathSet.Contains((r, c)))
                        Console.Write("* "); // 경로 | Path
                    else if (grid[r, c] == 1)
                        Console.Write("# "); // 장애물 | Obstacle
                    else
                        Console.Write(". "); // 빈 칸 | Empty
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 사용 예시 및 테스트
        /// Example usage and test
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("=== A* 패스파인딩 알고리즘 | A* Pathfinding Algorithm ===");
            Console.WriteLine();

            // 5x5 그리드 정의 (0 = 이동 가능, 1 = 장애물)
            // Define 5x5 grid (0 = walkable, 1 = obstacle)
            int[,] grid = {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 1 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0 }
            };

            var start = (Row: 0, Col: 0);  // 시작점 | Start
            var goal = (Row: 4, Col: 4);   // 목표점 | Goal

            Console.WriteLine("그리드 | Grid (# = 장애물/obstacle, . = 이동 가능/walkable):");
            PrintGrid(grid, null);
            Console.WriteLine();

            // A* 경로 탐색 실행 | Execute A* pathfinding
            var path = FindPath(grid, start, goal);

            if (path != null)
            {
                Console.WriteLine($"경로를 찾았습니다! | Path found! (길이/length: {path.Count} 스텝/steps)");
                Console.WriteLine();

                // 경로 좌표 출력 | Print path coordinates
                Console.WriteLine("경로 좌표 | Path coordinates:");
                foreach (var (Row, Col) in path)
                {
                    Console.WriteLine($"  ({Row}, {Col})");
                }

                Console.WriteLine();
                Console.WriteLine("경로 시각화 | Path visualization (* = 경로/path):");
                PrintGrid(grid, path);
            }
            else
            {
                Console.WriteLine("경로를 찾을 수 없습니다. | No path found.");
            }

            Console.WriteLine();

            // 경로가 없는 경우 테스트 | Test case with no path
            Console.WriteLine("=== 경로 없음 테스트 | No Path Test ===");
            Console.WriteLine();

            int[,] blockedGrid = {
                { 0, 0, 1 },
                { 1, 1, 1 },
                { 0, 0, 0 }
            };

            var blockedPath = FindPath(blockedGrid, (0, 0), (2, 2));

            if (blockedPath == null)
            {
                Console.WriteLine("예상대로 경로를 찾을 수 없습니다. | No path found, as expected.");
            }
        }
    }
}
