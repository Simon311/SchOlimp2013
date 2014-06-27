using System;
using System.Collections.Generic;

namespace Island
{
	class Program
	{
		const string Empty = "."; // Пустая клетка
		const string Island = "#"; // Островная клетка
		static int Width; // Ширина поля
		static int Height; // Высота поля
		static byte[,] Map; // Поле
		static List<Point> IslandChunks = new List<Point>(); // Список клеток, принадлежащих острову
		static List<Point> RouteChunks = new List<Point>(); // Список клеток, из которых будем составлять маршрут
		static List<Point> Route = new List<Point>(); // Составленный маршрут
		// Советую сначала взглянуть на struct Point

		static void Main(string[] args)
		{
			// Читаем вход:
			Height = Convert.ToInt32(Console.ReadLine()); // Высоту (кол-во строк)
			Width = Convert.ToInt32(Console.ReadLine()); // Ширину (кол-во символов в строке)

			Map = new byte[Width, Height]; // Обозначиваем само поле

			#region Reader
			string XReader = String.Empty; // Использована в цикле
			MapObj PReader = 0; // Использована в цикле
			Point CPoint = new Point(); // Использована в цикле
			for (int i = 0; i < Height; i++) // Читаем строки
			{
				XReader = Console.ReadLine(); // Считываем строку
				for (int j = 0; j < Width; j++) // Считываем посимвольно и пишем точки в поле
				{
					PReader = XReader[j].ToString() == Island ? MapObj.Island : MapObj.Empty; // Определяем тип считываемой клетки
					CPoint = new Point(j, i); // Создаем точку. Кстати мы используем обычную координатную систему с перевернутой осью Y,
					// вместо их пресловутой системы строк и столбцов
					CPoint.Plot(PReader); // Отмечаем её на карте (тип считан выше)
					if (PReader == MapObj.Island) IslandChunks.Add(CPoint); // Если точка островная - добавляем её в список
				}
			}
			#endregion

			#region RouteDotter
			// Здесь мы ищем соседние клетки
			var IslCount = IslandChunks.Count;
			for (int i = 0; i < IslCount; i++)
			// Перебираем все клетки, записанные в список островных
			{
				Point ICh = IslandChunks[i]; // Текущая клетка
				int Xp1 = ICh.X + 1; // Координата X + 1
				bool Xp1f = Xp1 < Width; // Проверяем правильная ли это координата (не выходит ли за границы поля)
				int Xm1 = ICh.X - 1; // Координата X - 1
				bool Xm1f = Xm1 >= 0; // Проверяем правильная ли это координата (не выходит ли за границы поля)
				int Yp1 = ICh.Y + 1; // Координата Y + 1
				bool Yp1f = Yp1 < Height; // Проверяем правильная ли это координата (не выходит ли за границы поля)
				int Ym1 = ICh.Y - 1; // Координата Y - 1
				bool Ym1f = Ym1 >= 0; // Проверяем правильная ли это координата (не выходит ли за границы поля)

				/* Дальнейшая логика сводится к перебору
				 *  всех восьми клеток вокруг текущей клетки.
				 * То есть мы ищем пустые клетки вокруг текущей
				 *  островной.
				 * Прокомментирую по минимуму.
				 */

				if (Xp1f && Map[Xp1, ICh.Y] == (byte)MapObj.Empty) 
					// Если точка X + 1 правильная и клетка (X + 1; Y) пустая
				{
					Point RCh = new Point(Xp1, ICh.Y); // Точка, которую мы отметим
					RouteChunks.Add(RCh); // Добавляем в список точек, из которых будем составлять маршрут
					RCh.Plot(MapObj.WayPoint); // Рисуем точку на карте
				}

				if (Xm1f && Map[Xm1, ICh.Y] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(Xm1, ICh.Y);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}

				if (Yp1f && Map[ICh.X, Yp1] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(ICh.X, Yp1);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}

				if (Ym1f && Map[ICh.X, Ym1] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(ICh.X, Ym1);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}

				if (Xp1f && Yp1f && Map[Xp1, Yp1] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(Xp1, Yp1);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}

				if (Xp1f && Ym1f && Map[Xp1, Ym1] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(Xp1, Ym1);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}

				if (Xm1f && Ym1f && Map[Xm1, Ym1] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(Xm1, Ym1);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}

				if (Xm1f && Yp1f && Map[Xm1, Yp1] == (byte)MapObj.Empty)
				{
					Point RCh = new Point(Xm1, Yp1);
					RouteChunks.Add(RCh);
					RCh.Plot(MapObj.WayPoint);
				}
			}
			#endregion

			#region PathFinder
			// Переходим к поиску маршрута
			Point CCh = RouteChunks[0]; // Берем первую точку из списка. 
			// В цикле эта же точка будет будет представлять текущую точку.

			Route.Add(CCh); // Сразу добавляем в маршрут
			
			bool NoRoute = false; // Переменная означающая есть ли путь дальше, используется в цикле
			while (!NoRoute) // Пока путь есть
			{
				// Здесь мы определяем тоже самое, что в прошлой части, 
				//  где искали островные клетки. То есть сами координаты
				// и их правильность.

				int Xp1 = CCh.X + 1;
				bool Xp1f = Xp1 < Width;
				int Xm1 = CCh.X - 1;
				bool Xm1f = Xm1 >= 0;
				int Yp1 = CCh.Y + 1;
				bool Yp1f = Yp1 < Height;
				int Ym1 = CCh.Y - 1;
				bool Ym1f = Ym1 >= 0;

				/* Аналогично, мы перебираем точки вокруг текущей.
				 * На этот раз не считаем угловые, т.к. самолету
				 * нельзя двигаться по диагонали.
				 * Прокомментриую по минимуму.
				 */

				if (Xp1f && Map[Xp1, CCh.Y] == (byte)MapObj.WayPoint)
				// Если X + 1 - правильная координата и точка (X + 1; Y) - точка маршрута
				{
					Point RCh = new Point(Xp1, CCh.Y); // Создаем точку
					Route.Add(RCh); // Добавляем в маршрут
					CCh.Plot(MapObj.DonePoint); // Наносим текущую точку на карту (на самом деле это не нужно,
					//  но было удобно для рисования карты для отладки).
					CCh = RCh; // Назначаем следующую точку текущей
					continue; // Продолжаем цикл, минуя остальной код, т.к. точка уже найдена
				}

				if (Xm1f && Map[Xm1, CCh.Y] == (byte)MapObj.WayPoint)
				{
					Point RCh = new Point(Xm1, CCh.Y);
					Route.Add(RCh);
					CCh.Plot(MapObj.DonePoint);
					CCh = RCh;
					continue;
				}

				if (Yp1f && Map[CCh.X, Yp1] == (byte)MapObj.WayPoint)
				{
					Point RCh = new Point(CCh.X, Yp1);
					Route.Add(RCh);
					CCh.Plot(MapObj.DonePoint);
					CCh = RCh;
					continue;
				}

				if (Ym1f && Map[CCh.X, Ym1] == (byte)MapObj.WayPoint)
				{
					Point RCh = new Point(CCh.X, Ym1);
					Route.Add(RCh);
					CCh.Plot(MapObj.DonePoint);
					CCh = RCh;
					continue;
				}

				NoRoute = true; // Если мы дошли до сюда, то маршрута нет
				// Такое может произойти и при полном проходе, и если мы зайдем в тупик.
				// Но, по условию остров выпуклый, то есть "тупиков" в нем нет.
			}
			CCh.Plot(MapObj.DonePoint); // Наносим последнюю точку на карту, опять же необязательно.
			#endregion
			
			#region DebugOutput
			// Это собственно вывод отладочной карты
			// % означает непройденные точки маршрута
			// * означает пройденные точки маршрута
			// Оставил для удобства
			Console.WriteLine();
			for (int i = 0; i < Height; i++)
			{
				string POut = String.Empty;
				for (int j = 0; j < Width; j++)
				{
					switch (Map[j, i])
					{
						case 1:
							POut += "#";
							break;
						case 2:
							POut += "%";
							break;

						case 3:
							POut += "*";
							break;

						case 0:
						default:
							POut += ".";
							break;

					}
				}
				Console.WriteLine(POut);
			}
			#endregion

			#region ProgramOutput
			var K = Route.Count;
			// Всё, выводим маршрут :)
			for (int k = 0; k < K; k++)
			{
				var OutChunk = Route[k];
				Console.WriteLine("{1} {0}", OutChunk.X + 1, OutChunk.Y + 1);
			}
			Console.ReadKey();
			#endregion
		}


		public enum MapObj // Типы точек на карте
		{
			Empty = 0, // Пустая
			Island = 1, // Островная
			WayPoint = 2, // Точка маршрута
			DonePoint = 3, // Пройденная точка маршрута
		}

		public struct Point // Точка на карте
		{
			public int X; // X координата
			public int Y; // Y координата

			public Point(int x, int y) // Конструктор типа
			{
				X = x; // Просто назначаем внутренние переменные
				Y = y;
			}

			public void Plot(MapObj Type) // Рисуем точку на карте с заданным типом точки
				// Смотрим enum MapObj
			{
				Map[X, Y] = (byte)Type;
			}
		}
	}
}