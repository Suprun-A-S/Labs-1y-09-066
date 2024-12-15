using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Stacks
{
    internal class StackSorter
    {
        public void SortArray(int[] arr)
        {
            // Основний стек для сортування
            Stack<int> stack = new Stack<int>();

            // Тимчасовий стек для допоміжних операцій
            Stack<int> tempStack = new Stack<int>();

            // Додавання всіх елементів масиву до основного стеку
            foreach (int item in arr)
            {
                stack.Push(item);
            }

            // Сортування за допомогою тимчасового стеку
            while (stack.Count > 0)
            {
                int current = stack.Pop();

                // Переміщення елементів з tempStack назад у stack, якщо вони більші за поточний
                while (tempStack.Count > 0 && tempStack.Peek() > current)
                {
                    stack.Push(tempStack.Pop());
                }

                // Додавання поточного елемента до tempStack
                tempStack.Push(current);
            }

            // tempStack тепер відсортований (найменше значення внизу)
            int i = arr.Length - 1;

            // Переміщення елементів з tempStack назад у масив
            while (tempStack.Count > 0)
            {
                arr[i--] = tempStack.Pop();
            }
        }
    }
}
