using System;
using System.Collections.Generic;

namespace AStar
{
    /// <summary>
    /// 최소 힙 기반 우선순위 큐 구현
    /// Min-heap based priority queue implementation
    /// </summary>
    /// <typeparam name="T">요소 타입 | Element type</typeparam>
    public class PriorityQueue<T>
    {
        // 힙 데이터를 저장하는 리스트 | List storing heap data
        private readonly List<(T Item, int Priority)> _heap = new List<(T, int)>();

        /// <summary>
        /// 큐에 있는 요소 수 | Number of elements in the queue
        /// </summary>
        public int Count => _heap.Count;

        /// <summary>
        /// 요소를 우선순위와 함께 큐에 추가합니다.
        /// Enqueues an element with the given priority.
        /// </summary>
        /// <param name="item">추가할 요소 | Element to add</param>
        /// <param name="priority">우선순위 (낮을수록 높은 우선순위) | Priority (lower = higher priority)</param>
        public void Enqueue(T item, int priority)
        {
            _heap.Add((item, priority));
            BubbleUp(_heap.Count - 1);
        }

        /// <summary>
        /// 가장 높은 우선순위(최소값)의 요소를 제거하고 반환합니다.
        /// Removes and returns the element with the highest priority (lowest value).
        /// </summary>
        /// <returns>최소 우선순위 요소 | Element with minimum priority</returns>
        public T Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("큐가 비어 있습니다. | Queue is empty.");

            var min = _heap[0];
            int lastIndex = _heap.Count - 1;

            // 마지막 요소를 루트로 이동 | Move last element to root
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);

            if (_heap.Count > 0)
                BubbleDown(0);

            return min.Item;
        }

        /// <summary>
        /// 큐가 비어 있는지 확인합니다.
        /// Checks whether the queue is empty.
        /// </summary>
        public bool IsEmpty => _heap.Count == 0;

        /// <summary>
        /// 삽입 후 힙 속성을 복원하기 위해 위로 이동합니다.
        /// Bubbles up to restore heap property after insertion.
        /// </summary>
        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                // 부모보다 우선순위가 낮으면(값이 크면) 중지
                // Stop if parent has higher priority (lower value)
                if (_heap[parentIndex].Priority <= _heap[index].Priority)
                    break;

                // 부모와 교환 | Swap with parent
                (_heap[parentIndex], _heap[index]) = (_heap[index], _heap[parentIndex]);
                index = parentIndex;
            }
        }

        /// <summary>
        /// 제거 후 힙 속성을 복원하기 위해 아래로 이동합니다.
        /// Bubbles down to restore heap property after removal.
        /// </summary>
        private void BubbleDown(int index)
        {
            int count = _heap.Count;

            while (true)
            {
                int smallest = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                // 왼쪽 자식과 비교 | Compare with left child
                if (left < count && _heap[left].Priority < _heap[smallest].Priority)
                    smallest = left;

                // 오른쪽 자식과 비교 | Compare with right child
                if (right < count && _heap[right].Priority < _heap[smallest].Priority)
                    smallest = right;

                // 현재 노드가 가장 작으면 중지 | Stop if current is smallest
                if (smallest == index)
                    break;

                // 교환 후 계속 진행 | Swap and continue
                (_heap[smallest], _heap[index]) = (_heap[index], _heap[smallest]);
                index = smallest;
            }
        }
    }
}
