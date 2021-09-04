namespace LZ_77__LZ_78
{
	// В зависимости от длины словаря сделать разные типы данных (unbyte, unshort, unint, unlong)
	class Block_LZ_77
	{
		// Cмещением назад от текущей позиции
		public int Offset = 0;

		// Длиной совпадающей подстроки 
		public int Length = 0;

		// Первый символ после найденного совпадающего фрагмента
		public char Next = '\0';

		public Block_LZ_77()
		{
		}
			public Block_LZ_77(int offs, int len, char nex)
		{
			Offset = offs;
			Length = len;
			Next = nex;
		}

	}

	class Block_LZ_78
	{
		public int Pos_in_dict = 0;
		public char Next = '\0';

	}
}
