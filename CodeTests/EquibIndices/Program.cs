using System;

namespace EquibIndices
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] nums = new[]{-1,  3, -4,  5,  1, -6,  2,  1 }; // answers should be 1, 3 & 7
			//int[] nums = new[]{1,2,3,4,3,2,1}; // answer should be 3

			Console.WriteLine($"equilibrium index: {FindEquibIndex(nums,1)}");
			Console.ReadKey();
		}

		private static int FindEquibIndex(int[] nums, int nthIndex)
		{
			int prevTotal = 0;			
			Int64 numTotal = 0;;
			int curNum = 0;
			int index = 0;
			for(int i = 0; i < nums.Length; i++) {
				numTotal += nums[i];
			}
			Int64 nextTotal = numTotal;
			for(int i = 0; i < nums.Length; i++) {
				curNum = nums[i];
				nextTotal -= curNum;
				if (nextTotal == prevTotal) {
					index++;
					if (index == nthIndex) {
						return i;
					}					
				}
				prevTotal += curNum;
			}
			return -1;
		}
	}
}
