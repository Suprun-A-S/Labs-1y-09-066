using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Queues
{
    internal class PriorityQueue
    {
        private List<int> items;

        public PriorityQueue()
        {
            // Ініціалізація списку для зберігання елементів черги
            items = new List<int>();
        }

        public void Insert(int value)
        {
            // Вставка нового елемента у відповідне місце, зберігаючи порядок пріоритету
            int i = 0;
            while (i < items.Count && items[i] <= value)
                i++;
            items.Insert(i, value);
        }

        public int Remove()
        {
            // Видалення елемента з найвищим пріоритетом (першого в списку)
            if (IsEmpty())
                throw new InvalidOperationException("Черга пріоритетів порожня!");
            int val = items[0];
            items.RemoveAt(0);
            return val;
        }

        public bool IsEmpty()
        {
            // Перевірка, чи є черга порожньою
            return items.Count == 0;
        }

        public void Display()
        {
            // Виведення вмісту черги
            Console.Write("Черга пріоритетів: ");
            foreach (int val in items)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine();
        }
    }
}
