using System.Collections.Generic;

namespace Compression.DataStructures
{
    public class Node<T>
    {
        public int Priority { get; set; }

        public T Value { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }
    }

    public class MinHeap<T>
    {
        private List<Node<T>> Heap;

        public MinHeap()
        {
            Heap = new List<Node<T>>();
        }

        public int Size { get => Heap.Count; }

        private int ParentPosition(int position) => position / 2;

        private bool IsLeaf(int position) => position >= Size / 2 && position <= Size;

        private void Swap(int pos1, int pos2)
        {
            var node = Heap[pos1];
            Heap[pos1] = Heap[pos2];
            Heap[pos2] = node;
        }

        private void MinHeapify(int position)
        {
            if (!IsLeaf(position))
            {
                int leftPos = position * 2 + 1;
                int rightPos = position * 2 + 2;

                int positionSmallerChild = leftPos;

                if (rightPos < Size)
                    positionSmallerChild = Heap[leftPos].Priority < Heap[rightPos].Priority ? leftPos : rightPos;

                if (Heap[position].Priority > Heap[positionSmallerChild].Priority)
                {
                    Swap(position, positionSmallerChild);
                    MinHeapify(positionSmallerChild);
                }
            }
        }

        public void Enqueue(Node<T> value)
        {
            Heap.Add(value);
            int currentPosition = Size - 1;
            int parentPosition = ParentPosition(currentPosition);

            while (Heap[currentPosition].Priority < Heap[parentPosition].Priority)
            {
                Swap(currentPosition, parentPosition);
                currentPosition = parentPosition;
                parentPosition = ParentPosition(currentPosition);
            }
        }

        public Node<T> Dequeue()
        {
            var node = Heap[0];
            Swap(0, Size - 1);
            Heap.RemoveAt(Size - 1);
            MinHeapify(0);

            return node;
        }

        public Node<T>[] ExportQueueAsArray() => Heap.ToArray();
    }
}
