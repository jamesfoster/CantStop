namespace CantStop
{
	static internal class DiceHelper
	{
		public static int[] GetPosibilities(int[] dice)
		{
			var result = new int[6];
			var index = 0;

			for (int i = 0; i < 3; i++)
			{
				for (int j = i + 1; j < 4; j++)
				{
					result[index++] = dice[i] + dice[j];
				}
			}

			return result;
		}
	}
}