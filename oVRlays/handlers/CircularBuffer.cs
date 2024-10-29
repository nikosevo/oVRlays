using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oVRlays.handlers
{
    internal class CircularBuffer<T>
    {
        private T[] buffer;
        private int index = 0;
        private int count = 0;
        public int Capacity { get; }

        public CircularBuffer(int capacity)
        {
            Capacity = capacity;
            buffer = new T[capacity];
        }

        public void Add(T item)
        {
            buffer[index] = item;
            index = (index + 1) % Capacity;

            // Keep track of how many items have been added (up to Capacity)
            if (count < Capacity)
            {
                count++;
            }
        }

        public T[] GetData()
        {
            return buffer;
        }
    }
}
