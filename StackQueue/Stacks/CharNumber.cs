using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Stacks
{
    internal class CharNumber
    {
        public void SplitString(string input)
        {
            // Стек для зберігання символів
            Stack<char> charsStack = new Stack<char>();

            // Стек для зберігання чисел
            Stack<char> numbersStack = new Stack<char>();

            // Розбиття рядка на символи та числа
            foreach (char c in input)
            {
                if (char.IsDigit(c)) // Якщо символ є цифрою
                {
                    numbersStack.Push(c);
                }
                else if (char.IsLetter(c)) // Якщо символ є літерою
                {
                    charsStack.Push(c);
                }
            }

            // Виведення символів
            Console.WriteLine("Символи:");
            foreach (var ch in charsStack)
            {
                Console.Write(ch + " ");
            }
            Console.WriteLine();

            // Виведення чисел
            Console.WriteLine("Числа:");
            foreach (var num in numbersStack)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }
}
