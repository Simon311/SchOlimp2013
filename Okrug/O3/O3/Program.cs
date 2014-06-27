using System;
using System.Collections.Generic;
using System.Linq;

namespace O3
{
	class Program
	{
		static void Main(string[] args)
		{
			string N = Console.ReadLine(); // Читаем вход

			Console.WriteLine(Palindrome(new Split(N))); // Всё выполняется тут, читаем комментарии ниже
			// Начните с struct Split ;)
			Console.ReadKey(); // Этого здесь быть не должно, оставил для удобства
		}

		static string Palindrome(Split S) // Получаем на вход число
		{
			long L = Convert.ToInt64(S.L); // Преобразуем вторую часть в число
			if (S.F < L) // Если первая часть меньше второй
			{
				if (S.Midd != String.Empty) // Если есть середина (ужос!)
				{
					long F = Convert.ToInt64(S.F.ToString() + S.Midd); // Получаем число состаящее из первой части и середины
					F++; // Увеличиваем на один
					string f = F.ToString(); // Преобразуем в строку
					S.Midd = f[f.Length - 1].ToString(); // Записываем новую середину обратно в число
					S.F = F / 10; // Записываем новую первую часть обратно в число
				}
				else S.F++; // Середины нет, достаточно увеличить первую часть
			}

			// Собственно вся магия, записываем последнюю часть как перевернутую первую :)
			S.L = new String(S.F.ToString().Reverse().ToArray()); 

			return S.Number(); // Возвращаем строковое представления числа
		}

		struct Split 
			// Тип для числа, разбитого на первую часть, середину (если есть) и вторую часть
		{
			public long F; // Первая часть, можно записать как число
			public string Midd; // Средняя часть, лучше записывать как строку
			public string L; // Вторая часть, тоже лучше записывать как строку

			public Split(string S) // Конструктор типа
			{
				int Len = S.Length; // Ищем длину строки
				if (Len % 2 == 0) // Если четная длинна
				{
					F = Convert.ToInt64(S.Substring(0, Len / 2)); // Извлекаем первую часть
					Midd = String.Empty; // Средней части нету
					L = S.Substring(Len / 2, Len / 2); // Вторая часть
				}
				else
				{
					F = Convert.ToInt64(S.Substring(0, Len / 2)); // Извлекаем первую часть
					Midd = S[Len / 2].ToString(); // Средняя часть, середина числа нечетной длины
					L = S.Substring(Len / 2 + 1, Len / 2); // Вторая часть
				}
			}

			public string Number() // Возвращает сформированное число в виде строки
			{
				return string.Format("{0}{1}{2}", F, Midd, L);
			}
		}
	}
}
