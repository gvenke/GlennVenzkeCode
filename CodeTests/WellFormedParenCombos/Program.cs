using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellFormedParenCombos
{
	class Program
	{
		static void Main(string[] args)
		{
			foreach(var curCombo in GenerateParenthesis(3)) {
				Console.WriteLine(curCombo);
			}
			Console.ReadKey();
		}

		private static IList<string> GenerateParenthesis(int pairs)
		{
			var combos = new List<string>();
			GenerateCombos(new char[2 * pairs],0,pairs,0,0,combos);
			return combos;
		}

		static void GenerateCombos(char []str,  int pos, int n, int open, int close, IList<string> combos) 
		{ 
			var combo = new StringBuilder();
			if (close == n)  
			{ 
				// print the possible combinations 
				for (int i = 0; i < str.Length; i++) {
					combo.Append(str[i]);
				}
				combos.Add(combo.ToString());
				Console.WriteLine(" ");
			} 
			
			else
			{ 
				if (open > close) { 
					str[pos] = '}'; 
					Console.WriteLine($"{str[pos]},{pos},{n},{open},{close}");
					GenerateCombos(str, pos + 1, 
									n, open, close + 1, combos); 
				} 
				if (open < n) { 
					str[pos] = '{'; 
					Console.WriteLine($"{str[pos]},{pos},{n},{open},{close}");
					GenerateCombos(str, pos + 1, 
									n, open + 1, close, combos); 
				} 
			} 
		}

	}
}
