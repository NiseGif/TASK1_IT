using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Queue.Models
{
    public class Queue<T> where T : class?
    {
        private LinkedList<T> _queue;

        public Queue()
        {
            _queue = new LinkedList<T>();
        }

        // Текущий элемент (элемент в начале очереди)
        public T? CurrentItem => _queue.Count > 0 ? _queue.First?.Value : null;

        // Количество элементов в очереди
        public int Count => _queue.Count;

        // Признак пустой очереди
        public bool IsEmpty => _queue.Count == 0;

        // Добавление элемента в конец очереди
        public void Enqueue(T? item)
        {
            if (item != null)
            {
                _queue.AddLast(item);
            }
        }

        // Извлечение элемента из начала очереди
        public T Dequeue()
        {
            Debug.Assert(_queue.First != null, "_queue.First != null");
            T value = _queue.First.Value;
            _queue.RemoveFirst();
            return value;
        }

        // Очистка очереди
        public void Clear()
        {
            _queue.Clear();
        }

        // Получение всех элементов для отображения
        public List<T> GetAllItems()
        {
            List<T> items = new List<T>();
            foreach (var item in _queue)
            {
                items.Add(item);
            }
            return items;
        }
    }
}