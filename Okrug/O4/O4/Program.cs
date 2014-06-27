using System;
using System.Collections.Generic;

namespace O4
{
	static class Program
	{
		public static int FirstNot(this List<int> L, int N)
			// Ищет в входном списке первого участника, чей номер школы не равен N
		{
			int I = L.Count;
			// Простенький цикл for-цикл, на списках он быстрее foreach
			for (int i = 0; i < L.Count; i++)
				if (L[i] != N) return L[i]; // Возвращаем значение сразу как нашли
			return 0; // На случай если ничего не найдено, что означает, что рассадка невозможна
		}

		static List<int> Parts = new List<int>(); // Список участников (в виде номеров их школ)
		static List<int> Out = new List<int>(); // Список на вывод

		static void Main(string[] args)
		{
			// Читаем вход:
			int A = Convert.ToInt32(Console.ReadLine()); 
			for (int i = 0; i < A; i++)
				Parts.Add(Convert.ToInt32(Console.ReadLine()));

			Out.Add(Parts[0]); // Первое значение сразу заносим в выходной список
			Parts.RemoveAt(0); // И удаляем из входного

			int FN = 0; // Используется в цикле

			while (Parts.Count > 0)
			{
				// Ищем первого участника из школы, отличной от школы последнего участника в выходном списке
				FN = Parts.FirstNot(Out[Out.Count - 1]); 
				if (FN > 0) // Если таковой найден
				{
					Parts.Remove(FN); // Удаляем его из входного списка
					Out.Add(FN); // Заносим в выходной список
				}
				else // Если нет
				{
					Console.WriteLine("0"); // Выводим ноль, как нас и просили
					Console.ReadKey(); // Этого здесь быть не должно, оставил для удобства
					return; // Выходим из программы
				}
			}

			// К этому моменту во входном списке участников не осталось, а в выходной все участники
			//  занесены в требуемой последовательности

			int I = Out.Count;
			// Цикл for на вывод участников; на списках for быстрее foreach
			for (int i = 0; i < I; i++)
				Console.WriteLine(Out[i]);

			Console.ReadKey(); // Этого здесь быть не должно, оставил для удобства
		}
	}
}
