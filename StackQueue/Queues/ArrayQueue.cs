using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Queues
{
    internal class ArrayQueue
    {
        private int[] queueArray; // Масив для зберігання елементів черги
        private int front; // Індекс початку черги
        private int rear; // Індекс кінця черги
        private int maxSize; // Максимальний розмір черги

        public ArrayQueue(int size)
        {
            // Ініціалізація черги заданого розміру
            maxSize = size;
            queueArray = new int[maxSize];
            front = 0;
            rear = 0;
        }

        public bool IsEmpty()
        {
            // Перевірка, чи порожня черга
            return front == rear;
        }

        public bool IsFull()
        {
            // Перевірка, чи заповнена черга
            return (rear + 1) % maxSize == front;
        }

        public void Insert(int element)
        {
            // Додавання елемента в чергу
            if (IsFull())
                throw new InvalidOperationException("Черга заповнена!");
            queueArray[rear] = element;
            rear = (rear + 1) % maxSize;
        }

        public int Remove()
        {
            // Видалення елемента з черги
            if (IsEmpty())
                throw new InvalidOperationException("Черга порожня!");
            int element = queueArray[front];
            front = (front + 1) % maxSize;
            return element;
        }

        public int Peek()
        {
            // Повернення елемента з початку черги без видалення
            if (IsEmpty())
                throw new InvalidOperationException("Черга порожня!");
            return queueArray[front];
        }

        public void Clear()
        {
            // Очищення черги
            front = 0;
            rear = 0;
        }

        public void Display()
        {
            // Виведення всіх елементів черги
            if (IsEmpty())
            {
                Console.WriteLine("Черга порожня.");
                return;
            }

            Console.Write("Черга: ");
            int current = front;
            while (current != rear)
            {
                Console.Write(queueArray[current] + " ");
                current = (current + 1) % maxSize;
            }
            Console.WriteLine();
        }
    }
}