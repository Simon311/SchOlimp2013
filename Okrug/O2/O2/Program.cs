﻿using System;

namespace O2
{
	class Program
	{
		static void Main(string[] args)
		{
			// Читаем вход
			long N = Convert.ToInt64(Console.ReadLine());
			long K = Convert.ToInt64(Console.ReadLine());
			long X = 0; // Счетчик дней
			long T = 0; // Счетчик количества выполненнных задач

			while (T < N) // Пока счетчик задач меньше цели
				// Я честно пытался решить это математически, но ничего не получалось, поэтому цикл
			{
				T += K; // Прибавляем задачек к счетчику задач
				K++; // Увеличиваем количество задач на следующий день (выполнение цикла)
				X++; // Увеличиваем счетчик дней (считаем кол-во выполнений цикла)
			}

			Console.WriteLine(X); // Выводим
			Console.ReadKey(); // Этого здесь быть не должно, оставил для удобства
		}
	}
}
