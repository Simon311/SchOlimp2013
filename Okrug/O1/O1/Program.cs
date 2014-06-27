using System;

namespace O1
{
	class Program
	{
		static void Main(string[] args)
		{
			// Читаем вход:
			long Total = Convert.ToInt64(Console.ReadLine());
			long Parents = Convert.ToInt64(Console.ReadLine());

			double X = (Total - 3 * Parents) / 2; // Формула
			if (X < 0) X = 0; // На случай, если родителей уже достаточно
			long Result = (long)Math.Ceiling(X); // 2.5 родителя нам тоже не подходят, будет 3
			Console.WriteLine(Result); // Вывод
			Console.ReadKey(); // Этого здесь быть не должно, оставил для своего удобства
		}
	}
}
