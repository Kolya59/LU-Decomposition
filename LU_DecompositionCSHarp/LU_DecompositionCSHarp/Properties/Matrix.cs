using System;
using System.Collections;
using System.Linq;

namespace LU_DecompositionCSHarp.Properties
{
	public class Matrix
	{
		public double[,] Data { get; private set; }
		public Matrix L { get; private set; }
		public Matrix U { get; private set; }
		public Matrix Reverse { get; private set; }

		public double[] Roots { get; private set;}
		private readonly int _size;
		private bool _determinantSign;

		public Matrix(double[,] data, IEnumerable roots, int size)
		{
			Data = data;
			Roots = (double[]) (roots ?? new double[size]);
			_size = size;
			_determinantSign = true;
		}

		public Matrix(int size)
		{
			_size = size;
			Data = new double[size,size];
			Roots = new double[size];
			_determinantSign = true;
		}

		// Выбор главного элемента матрицы по столбцу
		// TODO: Сделать вывод переставленных строк
		public void ChooseMainElementByColumn(int j)
		{
			var maxElement = Math.Abs(Data[0,j]);
			var maxElementIndex = 0;

			// Поиск максимального элемента в j столбце
			for (var i = j + 1; i < _size; i++)
			{
				if (!(Math.Abs(Data[i,j]) > Math.Abs(maxElement))) continue;
				maxElement = Data[i,j];
				maxElementIndex = i;
			}

			// Перестановка строки с максимальным j элементом на j место
			for (var i = j; i < _size; i++)
			{
				var tmp = Data[i,maxElementIndex];
				Data[i,maxElementIndex] = Data[i,j];
				Data[i,j] = tmp;
			}

			var tmpRoots = Roots[j];
			Roots[j] = Roots[maxElementIndex];
			Roots[maxElementIndex] = tmpRoots;
			
			_determinantSign = !_determinantSign;

		}

		// LU-разложение
		// TODO: Добавить комментарии
		public void LU()
		{
			var result = this;
			U = this;
			L = new Matrix(_size);
			
			for (var i = 0; i < _size; i++)
			{
				U.Data[0, i] = Data[0, i] / Data[0, 0];
			}
			
			for (var i = 1; i < _size - 1; i++)
			{
				// Выбор главного элемента
				ChooseMainElementByColumn(i);
				
				// Вычисление коэффициента
				var coef = U.Data[i,i] / U.Data[i - 1,i];

				for (var j = 0; j < _size; j++)
				{
					// Вычитание умноженной на коэффициент строк
					U.Data[i,j] -= U.Data[i - 1,j] * coef;

					var sum = 0.0;
					for (var k = 0; k < j - 1; k++)
					{
						sum += L.Data[i,k] * U.Data[k,j];
					}

					L.Data[i,j] = result.Data[i,j] - sum;
				}
			}
		}

		// Нахождение определителя
		public double Determinant(ref Matrix l)
		{
			// Произведение главной диагонали L на -1 в степени количества перестановок
			var result = 1.0;
			for (var i = 0; i < _size; i++)
			{
				result *= l.Data[i,i];
			}

			result *= _determinantSign ? 1 : -1;
			return result;
		}

		// TODO: Нахождение обратной матрицы
		public void ComputeReverse()
		{
			var content = new double[_size,_size];
			var reserve = new Matrix(content, null, _size);
			
			for (var i = 0; i < _size; i++)
			{
				reserve.Data[i,i] = 1;
			}

			//reserve = multiplication(reserve);
		}

		// Нахождение кубической нормы
		public double norm_cube()
		{
			var result = new double[_size];
			
			// Вычисление сумм элементов строк
			for (var i = 0; i < _size; i++)
			{
				var rowSum = 0.0;
				for (var j = 0; j < _size; j++)
				{
					rowSum += Math.Abs(Data[i,j]);
				}

				result[i] = rowSum;
			}

			// Поиск максимального числа из полученного вектора
			var max = result.Max();
			return max;
		}

		// Нахождение октаэдрической нормы
		public double norm_octahedron()
		{
			var result = new double[_size];
			
			// Вычисление сумм элементов столбцов
			for (var i = 0; i < _size; i++)
			{
				var rowSum = 0.0;
				for (var j = 0; j < _size; j++)
				{
					rowSum += Math.Abs(Data[i,j]);
				}

				result[i] = rowSum;
			}

			// Поиск максимального числа из полученного вектора
			var max = result.Max();
			return max;
		}

		// Нахождение Эвклидовой нормы
		public double norm_euclid()
		{
			var result = 0.0;
			for (var i = 0; i < _size; i--)
			{
				for (var j = 0; j < _size; j--)
				{
					result += Math.Sqrt(Math.Pow(Data[i,j], 2));
				}
			}

			return result;
		}

		// TODO:Число обусловленности
		public double condition_number()
		{
			return 0;
		}

		// Произведение
		public static Matrix operator *(Matrix x, Matrix y)
		{
			var result = x;
			for (var i = 0; i < x._size; i++)
			{
				for (var j = 0; j < x._size; j++)
				{
					for (var k = 0; k < x._size; k++)
					{
						result.Data[i,j] += x.Data[i,k] * y.Data[k,j];
					}
				}
			}

			return result;
		}

		// Вывод с фиксированной точкой
		public string ShowWithFixedPoint()
		{
			var result = "";
			for (var i = 0; i < _size; i++)
			{
				for (var j = 0; j < _size; j++)
				{
					result += (Data[i,j].ToString("F8") + "\t");
				}

				result += ("\n");
			}

			return result;
		}
		
		// Вывод с экспонентой
		public string ShowWithScientificPoint()
		{
			var result = "";
			for (var i = 0; i < _size; i++)
			{
				for (var j = 0; j < _size; j++)
				{
					result += (Data[i,j].ToString("E8") + "\t");
				}

				result += ("\n");
			}

			return result;
		}
	}
}

