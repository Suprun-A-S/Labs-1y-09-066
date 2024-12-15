using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Stacks
{
    internal class VowelConsonant
    {
        public void SplitString(string input)
        {
            // Стек для голосних букв
            Stack<char> vowelsStack = new Stack<char>();

            // Стек для приголосних букв
            Stack<char> consonantsStack = new Stack<char>();

            // Набір голосних букв
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

            // Перебір символів у рядку
            foreach (char c in input)
            {
                if (char.IsLetter(c)) // Перевірка, чи є символ буквою
                {
                    char lower = char.ToLower(c); // Приведення символу до нижнього регістру
                    if (vowels.Contains(lower))
                        vowelsStack.Push(c); // Додавання голосної до стеку
                    else
                        consonantsStack.Push(c); // Додавання приголосної до стеку
                }
            }

            // Виведення голосних букв
            Console.WriteLine("Голосні:");
            foreach (var v in vowelsStack)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

            // Виведення приголосних букв
            Console.WriteLine("Приголосні:");
            foreach (var cons in consonantsStack)
            {
                Console.Write(cons + " ");
            }
            Console.WriteLine();
        }
    }
}