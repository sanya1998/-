using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_DES
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}
		List<bool> key_1;
		List<bool> key_2;
		List<bool> key_3;
		int len_key = 56;
		Random rnd;
		// Вместо GetEncoding(1251), можно поставить другую кодировку
		//Encoding enc = Encoding.GetEncoding(1251);
		Encoding enc = Encoding.Default;
		//Encoding enc = UTF8Encoding.UTF8;
		//Encoding enc = Encoding.ASCII;

		// Генерация случайного ключа заданной длины
		private List<bool> Key_random_generation(int numeric_len)
		{
			List<bool> key = new List<bool>(numeric_len);
			
			for (int pos = 0; pos < numeric_len; pos++)
				key.Add(Convert.ToBoolean(rnd.Next(2)));
			return key;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			// Добавляем события на нажатия кнопок
			btn_new_key_1.Click += (s, eventArgs) => { btn_new_key_Click(s, eventArgs, 1); };
			btn_new_key_2.Click += (s, eventArgs) => { btn_new_key_Click(s, eventArgs, 2); };
			btn_new_key_3.Click += (s, eventArgs) => { btn_new_key_Click(s, eventArgs, 3); };

			// Генерируем ключи и записываем в textBox-ы
			rnd = new Random((int)DateTime.Now.Ticks);
			key_1 = Key_random_generation(len_key);
			key_2 = Key_random_generation(len_key);
			key_3 = Key_random_generation(len_key);
			tB_key_1.Text = BitArrayToString(key_1);
			tB_key_2.Text = BitArrayToString(key_2);
			tB_key_3.Text = BitArrayToString(key_3);

			// Добавляем события на изменения ключей
			tB_key_1.TextChanged += (s, eventArgs) => { tB_key_TextChanged(s, eventArgs, 1); };
			tB_key_2.TextChanged += (s, eventArgs) => { tB_key_TextChanged(s, eventArgs, 2); };
			tB_key_3.TextChanged += (s, eventArgs) => { tB_key_TextChanged(s, eventArgs, 3); };
			
			// testing
			// DES_for_all_text(Convert_16in2("AB7420C266F36E"), Convert_16in2("123456ABCD132536"));
			// decode_DES_for_all_text(Convert_16in2("AB7420C266F36E"), Convert_16in2("C0B7A8D05F3A829C"));
		}

		// Нажатие на одну из кнопок генерации ключа
		private void btn_new_key_Click(object sender, EventArgs e, int num)
		{
			switch(num)
			{
				case 1:
					key_1 = Key_random_generation(len_key);
					tB_key_1.Text = BitArrayToString(key_1);
					break;
				case 2:
					key_2 = Key_random_generation(len_key);
					tB_key_2.Text = BitArrayToString(key_2);
					break;
				case 3:
					key_3 = Key_random_generation(len_key);
					tB_key_3.Text = BitArrayToString(key_3);
					break;
				default:
					break;
			}
		}

		// Изменение текста в одном из textBox-ов с ключами
		private void tB_key_TextChanged(object sender, EventArgs e, int num)
		{
			// В каком textBox-е изменения
			switch(num)
			{
				case 1:
					// Введеный ключ корректный?
					if (Key_correct_check())
						key_1 = StringToBitArray(tB_key_1.Text);
					// Иначе вернуть старые значения
					else
						tB_key_1.Text = BitArrayToString(key_1);
					break;
				case 2:
					if (Key_correct_check())
						key_2 = StringToBitArray(tB_key_2.Text);
					else
						tB_key_2.Text = BitArrayToString(key_2);
					break;
				case 3:
					if (Key_correct_check())
						key_3 = StringToBitArray(tB_key_3.Text);
					else
						tB_key_3.Text = BitArrayToString(key_3);
					break;
				default:
					break;
			}
		}
		// кнопка Шифровать
		private void btn_ish_to_code_Click(object sender, EventArgs e)
		{
			// Проверка длины ключей
			if (Key_len_check() && rTB_ish.Text.Length > 0)
			{
				// Перевод текста в двоичное представление
				List<bool> text_bin = TextToBin(rTB_ish.Text);

				// 3 DES
				List<bool> shifr_blocks = 
					DES_for_all_text(key_3, 
					decode_DES_for_all_text(key_2, 
					DES_for_all_text(key_1, text_bin)));
				
				rTB_cod.Text = BinToText(shifr_blocks);
				rTB_ish_10.Text += BitArrayToString(text_bin) + "\n";
				rTB_cod_10.Text += BitArrayToString(shifr_blocks) + "\n";
			}
			else
			{
				rTB_cod.Text = "";
			}
		}

		// кнопка Дешифровать
		private void btn_code_to_ish_Click(object sender, EventArgs e)
		{
			// Проверка длины ключей
			if (Key_len_check() && rTB_cod.Text.Length > 0)
			{
				// Перевод текста в двоичное представление
				List<bool> text_bin = TextToBin(rTB_cod.Text);


				var res = decode_DES_for_all_text(key_1, 
					DES_for_all_text(key_2, 
					decode_DES_for_all_text(key_3, text_bin)));
				rTB_ish.Text = BinToText(res);
				rTB_cod_10.Text += BitArrayToString(text_bin) + "\n";
				rTB_ish_10.Text += BitArrayToString(res) + "\n";
			}
			else
			{
				rTB_ish.Text = "";
			}
		}
		// Дешифрование DES
		private List<bool> decode_DES_for_all_text(List<bool> ke, List<bool> text_bi)
		{
			// Копировать, чтоб изменения не остались на нем
			List<bool> text_bin = new List<bool>(text_bi);
			List<bool> key = new List<bool>(ke);
			// Генерация раундовых ключей
			List<List<bool>> round_keys = Raund_keys_generation(key);
			round_keys.Reverse();

			#region То же самое, что и для шифрования
			// Длина одного блока
			int block_len = 64;
			
			// Разбиение бинарного текста на блоки, последний заполняется незначащими нулями
			List<List<bool>> blocks = BigBlockToBlocks(text_bin, block_len);
			
			// Такой аргумент, так как DES принимает 64-битовый открытый текст и порождает 64-битовый зашифрованный текст, то есть длина останется той же
			List<bool> code_bin = new List<bool>(blocks.Count * block_len);

			// Цикл по блокам
			foreach (List<bool> block in blocks)
			{
				// Все преобразования блока в DES_for_block, Запись результата в code_bin
				code_bin.AddRange(DES_for_block(round_keys, block));
			}

			// Удаляем по 8 нулей спереди, если они есть
			while (code_bin.Count >= 8 && BitArrayToInt(code_bin.GetRange(0, 8)) == 0)
				code_bin.RemoveRange(0, 8);
			// В виде 1 и 0
			return code_bin;
			#endregion
		}

		// DES для всего текста
		private List<bool> DES_for_all_text(List<bool> ke, List<bool> text_bi)
		{
			// Копировать, чтоб изменения не остались на нем
			List<bool> text_bin = new List<bool>(text_bi);
			List<bool> key = new List<bool>(ke);
			// Генерация раундовых ключей
			List<List<bool>> round_keys = Raund_keys_generation(key);
			
			// Длина одного блока
			int block_len = 64;
			// Разбиение бинарного текста на блоки, последний заполняется незначащими нулями
			List<List<bool>> blocks = BigBlockToBlocks(text_bin, block_len);

			// Такой аргумент, так как DES принимает 64-битовый открытый текст и порождает 64-битовый зашифрованный текст, то есть длина останется той же
			List<bool> code_bin = new List<bool>(blocks.Count * block_len);
			// Цикл по блокам
			foreach (List<bool> block in blocks)
			{
				// Все преобразования блока в DES_for_block, Запись результата в code_bin
				code_bin.AddRange(DES_for_block(round_keys, block));
			}

			// Удаляем по 8 нулей спереди, если они есть
			while (code_bin.Count >= 8 && BitArrayToInt(code_bin.GetRange(0, 8)) == 0)
				code_bin.RemoveRange(0, 8);
			// В виде 1 и 0
			return code_bin;
		}

		// Генерация 16 раундовых ключей
		private List<List<bool>> Raund_keys_generation(List<bool> key)
		{
			// Добавление битов четности (56 -> 64)
			List<bool> key_lst = new List<bool>(key);
			bool bit_chet = true;
			for(int pos=0, shift_for_bit_chet=0; pos<key.Count;)
			{
				bit_chet = key[pos] ? !bit_chet : bit_chet;
				pos++;
				if(pos % 7 == 0)
				{
					// bit_chet == true, если четное число единиц
					key_lst.Insert(pos + shift_for_bit_chet, !bit_chet);
					shift_for_bit_chet++;
					bit_chet = true;
				}
			}

			// Удаление битов четности ПЕРЕСТАНОВКОЙ (64 -> 56)
			int[] check_bit_rem = { 57,49,41,33,25,17,9,1,58,50,42,34,26,18,
															10,2,59,51,43,35,27,19,11,3,60,52,44,36,
															63,55,47,39,31,23,15,7,62,54,46,38,30,22,
															14,6,61,53,45,37,29,21,13,5,28,20,12,4};
			List<bool> key_without_chet = new List<bool>(check_bit_rem.Length);
			foreach (int i in check_bit_rem)
				key_without_chet.Add(key_lst[i - 1]);

			// Деление key_without_chet на C(0) и D(0)
			List<bool> C_prev = new List<bool>(key_without_chet.GetRange(0, key_without_chet.Count/2));
			List<bool> D_prev = new List<bool>(key_without_chet.GetRange(key_without_chet.Count / 2, key_without_chet.Count / 2));
			
			// Перестановка сжатия (P-бокс, таблица) 56 -> 48
			int[] P_box = new int[] {	14,17,11,24,1,5,3,28,
																15,6,21,10,23,19,12,4,
																26,8,16,7,27,20,13,2,
																41,52,31,37,47,55,30,40,
																51,45,33,48,44,49,39,56,
																34,53,46,42,50,36,29,32 };
			
			// Создание 16 раундовых ключей
			int[] shifts = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
			List<List<bool>> round_keys = new List<List<bool>>(16);
			for (int pos = 0; pos < 16; pos++)
			{
				List<bool> C_D = new List<bool>(C_prev.Count + D_prev.Count);
				if (shifts[pos] == 1)
				{
					// Получить C и D для двух сдвигов влево
					List<bool> C_1 = new List<bool>(C_prev);
					// Добавить в конец значение, равное первому элементу
					C_1.Add(C_prev[0]);
					// Удалить первый элемент
					C_1.RemoveAt(0);
					List<bool> D_1 = new List<bool>(D_prev);
					D_1.Add(D_prev[0]);
					D_1.RemoveAt(0);
					C_D.AddRange(C_1);
					C_D.AddRange(D_1);
				}
				else if (shifts[pos] == 2)
				{
					List<bool> C_2 = new List<bool>(C_prev);
					C_2.Add(C_prev[0]);
					C_2.Add(C_prev[1]);
					C_2.RemoveAt(0);
					C_2.RemoveAt(0);
					List<bool> D_2 = new List<bool>(D_prev);
					D_2.Add(D_prev[0]);
					D_2.Add(D_prev[1]);
					D_2.RemoveAt(0);
					D_2.RemoveAt(0);
					C_D.AddRange(C_2);
					C_D.AddRange(D_2);
				}
				List<bool> shift_block = new List<bool>(P_box.Length);
				foreach (int i in P_box)
					shift_block.Add(C_D[i - 1]);
				round_keys.Add(shift_block);

				C_prev = new List<bool>(C_D.GetRange(0, C_D.Count / 2));
				D_prev = new List<bool>(C_D.GetRange(C_D.Count / 2, C_D.Count / 2));
			}
			return round_keys;
		}

		// DES для одного блока
		private List<bool> DES_for_block(List<List<bool>> round_keys, List<bool> block)
		{
			// Начальная перестановка
			List<bool> block_IP = Begin_IP(block);

			// 16 раундов сети Фейстеля
			List<bool> L_prev = new List<bool>(block_IP.GetRange(0, block_IP.Count / 2));
			List<bool> R_prev = new List<bool>(block_IP.GetRange(block_IP.Count / 2, block_IP.Count / 2));
			
			foreach(List<bool> key in round_keys)
			{
				// Нужно посчитать R_curr = L_prev + f(R_prev, k(i) ). XOR создает новый список
				List<bool> R_curr = XOR(Feistel_f(R_prev, key), L_prev);
				List<bool> L_curr = new List<bool>(R_prev);
				
				R_prev = R_curr;
				L_prev = L_curr;
			}

			// Объединям L_prev и R_prev (что слева?)
			List<bool> LR_block = new List<bool>(L_prev.Count + R_prev.Count);

			// Возможно, в другом порядке.
			LR_block.AddRange(R_prev);
			LR_block.AddRange(L_prev);

			// Финальная перестановка
			List<bool> block_IP_1 = End_IP(LR_block);
			return block_IP_1;
		}

		// Финальная перестановка
		private List<bool> End_IP(List<bool> block)
		{
			// от 1 до 64
			int[] end_IP =  {
				40,8,48,16,56,24,64,32,39,7,47,15,55,23,63,31,
				38,6,46,14,54,22,62,30,37,5,45,13,53,21,61,29,
				36,4,44,12,52,20,60,28,35,3,43,11,51,19,59,27,
				34,2,42,10,50,18,58,26,33,1,41,9,49,17,57,25
			};
			List<bool> block_IP = new List<bool>(block.Count);
			foreach (int i in end_IP)
				block_IP.Add(block[i - 1]);
			return block_IP;
		}

		// Раундовая функция Фейстеля
		private List<bool> Feistel_f(List<bool> R_32, List<bool> key)
		{
			// P-бокс расширения
			int[] P_box = new[] { 32, 1, 2, 3, 4, 5,
														4, 5, 6, 7, 8, 9,
														8, 9, 10, 11, 12, 13,
														12, 13, 14, 15, 16, 17,
														16, 17, 18, 19, 20, 21,
														20, 21, 22, 23, 24, 25,
														24, 25, 26, 27, 28, 29,
														28, 29, 30, 31, 32, 1 };
			// Расширение R блока
			List<bool> R_48 = new List<bool>(48);
			foreach (int i in P_box)
				R_48.Add(R_32[i-1]);
			List<bool> block_xor = XOR(R_48, key);

			// Разбиение на 8 блоков по 6
			List<List<bool>> blocks_po_6 = BigBlockToBlocks(block_xor, 6);

			// Формирование 8 блоков по 4
			List<List<bool>> blocks_po_4 = S_box_execute(blocks_po_6);
			// Объединение 8 блоков
			List<bool> block_32 = new List<bool>(32);
			foreach (List<bool> bl in blocks_po_4)
				block_32.AddRange(bl);
			// P-бокс прямой
			int[] p_right_box= new int[] {16,7,20,21,29,12,28,17,
																		1,15,23,26,5,18,31,10,
																		2,8,24,14,32,27,3,9,
																		19,13,30,6,22,11,4,25};
			List<bool> result = new List<bool>(R_32.Count);
			foreach (int i in p_right_box)
				result.Add(block_32[i - 1]);
			return result;
		}

		private List<List<bool>> S_box_execute(List<List<bool>> blocks_po_6)
		{
			List<List<bool>> blocks_po_4 = new List<List<bool>>(blocks_po_6.Count);

			int[,] s_boxes = new int[,]
			{
				{
					14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7,
					0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8,
					4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0,
					15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13
				},
				{
					15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10,
					3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5,
					0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15,
					13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9
				},
				{
					10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8,
					13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1,
					13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7,
					1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12 
				},
				{
					7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15,
					13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9,
					10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4,
					3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14
				},
				{
					2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9,
					14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6,
					4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14,
					11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3
				},
				{
					12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11,
					10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8,
					9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6,
					4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13
				},
				{
					4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1,
					13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6,
					1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2,
					6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12
				},
				{
					13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7,
					1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2,
					7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8,
					2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11
				}
			};
			int pos = 0;
			foreach (List<bool> bl in blocks_po_6)
			{
				List<bool> stroka = new List<bool>(){ bl[0], bl[5] };
				List<bool> stolbec = new List<bool>(){ bl[1], bl[2], bl[3], bl[4]};
				
				int num_str = BitArrayToInt(stroka);
				int num_sto = BitArrayToInt(stolbec);

				int chislo_des = s_boxes[pos, num_str * 16 + num_sto];

				// chislo_des в четырехразрядное двоичное
				blocks_po_4.Add(IntToBits(chislo_des, 4));
				pos++;
			}
			return blocks_po_4;
		}

		// Перевод десятичного числа в двоичное заданной длины
		private List<bool> IntToBits(long chislo, int len)
		{
			if (chislo >= Math.Pow(2, len))
				return null;

			List<bool> res = new List<bool>(len);
			while(chislo>0)
			{
				res.Add(chislo % 2 == 1);
				chislo /= 2;
			}
			while (res.Count < len)
				res.Add(false);
			res.Reverse();
			return res;
		}

		private int BitArrayToInt(List<bool> bit_list)
		{
			if (bit_list.Count > 32)
				throw new ArgumentException("Argument length shall be at most 32 bits.");

			int slag = (int) Math.Pow(2, bit_list.Count-1);
			int answer = 0;
			foreach(bool bit in bit_list)
			{
				if (bit)
					answer += slag;
				slag /= 2;
			}
			return answer;
		}

		// XOR двух булевских массивов одинаковой длины
		private List<bool> XOR(List<bool> bin1, List<bool> bin2)
		{
			if (bin1.Count != bin2.Count)
				return null;
			List<bool> result = new List<bool>(bin1.Count);
			for (int i = 0; i < bin1.Count; i++)
				result.Add(bin1[i] != bin2[i]);
			return result;
		}

		// Начальная перестановка
		private List<bool> Begin_IP(List<bool> block)
		{
			// от 1 до 64
			int[] beg_IP =  {
				58,50,42,34,26,18,10,2, 60,52,44,36,28,20,12,4,
				62,54,46,38,30,22,14,6, 64,56,48,40,32,24,16,8,
				57,49,41,33,25,17,9, 1, 59,51,43,35,27,19,11,3,
				61,53,45,37,29,21,13,5, 63,55,47,39,31,23,15,7
			};
			List<bool> block_IP = new List<bool>(block.Count);
			foreach (int i in beg_IP)
				block_IP.Add(block[i - 1]);
			return block_IP;
		}

		// Проверка длин ключей
		private bool Key_len_check()
		{
			if (key_1.Count != len_key)
				MessageBox.Show("Длина ключа 1 не равна " + len_key);
			else if (key_2.Count != len_key)
				MessageBox.Show("Длина ключа 2 не равна " + len_key);
			else if (key_3.Count != len_key)
				MessageBox.Show("Длина ключа 3 не равна " + len_key);
			else
				return true;
			return false;
		}

		// Проверка корректности ключей
		private bool Key_correct_check()
		{
			foreach(char symb in tB_key_1.Text+tB_key_2.Text+tB_key_3.Text)
				if (symb != '1' && symb != '0')
					return false;
			return true;
		}

		// Массив битов в string, то есть (true, false) => (10)
		private string BitArrayToString(BitArray key)
		{
			StringBuilder key_str = new StringBuilder(key.Count);
			foreach (bool k in key)
				key_str.Append(k ? '1' : '0');
			return key_str.ToString();
		}
		// Список битов в string, то есть (true, false) => (10)
		private string BitArrayToString(List<bool> key)
		{
			StringBuilder key_str = new StringBuilder(key.Count);
			foreach (bool k in key)
				key_str.Append(k ? '1' : '0');
			return key_str.ToString();
		}

		// string в массив битов, то есть (10) => (true, false)
		private List<bool> StringToBitArray(string text)
		{
			List<bool> key = new List<bool>(text.Length);
			for (int pos = 0; pos < text.Length; pos++)
				key.Add(text[pos] == '1');
			return key;
		}

		// Разбиение на блоки, первый блок спереди будет заполнен незначищими нулями, если это потребуется
		private List<List<bool>> BigBlockToBlocks(List<bool> big_block, int small_block_len)
		{
			// Первый блок заполняется незначащими нулями до необходимой длины
			while (big_block.Count % small_block_len != 0)
				big_block.Insert(0, false);

			int count_blocks = big_block.Count / small_block_len;
			List<List<bool>> blocks = new List<List<bool>>(count_blocks);
			for (int num_bl = 0; num_bl < count_blocks; num_bl++)
				blocks.Add(new List<bool>(big_block.GetRange(small_block_len * num_bl, small_block_len)));

			return blocks;
		}


		// Из текста в массив битов, то есть (а) => (00000111)
		private List<bool> TextToBin(string text)
		{
			byte[] bytes = enc.GetBytes(text);


			BitArray text_bin_arr = new BitArray(bytes);
			List<bool> text_bin_lst = new List<bool>(text_bin_arr.Count);
			// Из массива в список
			foreach (bool el in text_bin_arr)
				text_bin_lst.Add(el);
			return text_bin_lst;
		}

		// Массив бит в текст
		private string BinToText(List<bool> bitArray_lst)
		{
			/*// Первый блок заполняется незначащими нулями до необходимой длины
			while (bitArray_lst.Count % 8 != 0)
				bitArray_lst.Insert(0, false);*/

			byte[] bytes = new byte[(int)Math.Ceiling((decimal)(bitArray_lst.Count / 8))];

			for (int pos = 0, i = 0; pos < bitArray_lst.Count; pos += 8)
			{
				BitArray bitArray = new BitArray(bitArray_lst.GetRange(pos, 8).ToArray());
				bitArray.CopyTo(bytes, i++);
			}

			string answer = enc.GetString(bytes);
			return answer;
		}
		// 16 СС в 2 СС
		private List<bool> Convert_16in2(string ch_16)
		{
			List<bool> answer = new List<bool>(ch_16.Length * 4);
			foreach(char ch in ch_16)
			{
				long ch_10 = Convert.ToInt64(ch.ToString(), 16);
				answer.AddRange(IntToBits(ch_10, 4));
			}
			return answer;
		}
		// 2 СС в 16 СС
		private string Convert_2in16(List<bool> block)
		{
			List<List<bool>> small_bl = BigBlockToBlocks(block, 4);
			StringBuilder ch_16 = new StringBuilder(small_bl.Count);
			foreach (List<bool> bl in small_bl)
			{
				int ch_10 = BitArrayToInt(bl);
				switch (ch_10)
				{
					case 10:
						ch_16.Append('A');
						break;
					case 11:
						ch_16.Append('B');
						break;
					case 12:
						ch_16.Append('C');
						break;
					case 13:
						ch_16.Append('D');
						break;
					case 14:
						ch_16.Append('E');
						break;
					case 15:
						ch_16.Append('F');
						break;
					default:
						ch_16.Append(ch_10.ToString());
						break;
				}
			}
			return ch_16.ToString();
		}
	}
}
