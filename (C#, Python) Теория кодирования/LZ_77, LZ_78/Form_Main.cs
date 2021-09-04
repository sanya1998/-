using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZ_77__LZ_78
{
	public partial class Form_Main : Form
	{
		public Form_Main()
		{
			InitializeComponent();
		}
		string path_to_books_folder = Environment.CurrentDirectory + @"\Книги в txt";
		string binFileDefault = Environment.CurrentDirectory + @"\default.dat";
		string txtFileDefault = Environment.CurrentDirectory + @"\default.txt";
		// При закрытии формы
		private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Отключить фоновые потоки, если они включены

			if (bW_encode.IsBusy)
			{
				bW_encode.WorkerSupportsCancellation = true;
				bW_encode.CancelAsync();
			}

			if (bW_decode.IsBusy)
			{
				bW_decode.WorkerSupportsCancellation = true;
				bW_decode.CancelAsync();
			}
		}
		
		// При изменении radioButton алгоритма LZ 77
		private void RB_lz77_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rB = (RadioButton)sender;
			// Если выбрали LZ 77, то нужно разрешить выбор длин словаря и буфера, и наоборот
			if (rB.Checked)
			{
				gB_buf_len.Enabled = true;
				gB_dict_len.Enabled = true;
			}
			else
			{
				gB_buf_len.Enabled = false;
				gB_dict_len.Enabled = false;
			}
		}
		
		// Клик по кнопке: "Загрузить и закодировать"
		private void Button_squeeze_Click(object sender, EventArgs e)
		{
			#region ЗАГРУЗКА ИСХОДНОГО ФАЙЛА
			// Создать и настроить диалог открытия файла
			OpenFileDialog Fd = new OpenFileDialog();
			Fd.Title = "Выберите файл";
			Fd.Filter = "Текстовые файлы|*.txt";

			// Переход в папку с книгами, если она есть
			if (Directory.Exists(path_to_books_folder))
				Fd.InitialDirectory = path_to_books_folder;

			// Если файл не был выбран, выйти из функции
			if (Fd.ShowDialog() != DialogResult.OK)
				return;

			#endregion
			// Запуск в фоновом потоке
			bW_encode.RunWorkerAsync(Fd.FileName);
		}
		// Функция кодирования в фоновом потоке
		private void BW_encode_DoWork(object sender, DoWorkEventArgs e)
		{
			string fileName = (string)e.Argument;
			// Размер исходного файла
			tB_size_ish.Text = GetFileSize(fileName);
			// Сюда запишется после сжатия
			tB_size_squeeze.Text = "";
			// Считать файл													 
			string text;
			using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
			{
				// Сразу весь текст
				text = sr.ReadToEnd();
			}

			// Какой алгоритм выбран?
			if (rB_lz77.Checked)
			{
				List<Block_LZ_77> blocks = Encode_LZ_77(text);
				WriteToBinFile_LZ_77(blocks); // в дефолтный
				blocks.Clear();
			}
			else if (rB_lz78.Checked)
			{
				List<Block_LZ_78> blocks = Encode_LZ_78(text);
				WriteToBinFile_LZ_78(blocks); // в дефолтный
				blocks.Clear();
			}
			tB_size_squeeze.Text = GetFileSize(binFileDefault);
			bW_encode.ReportProgress(100);
		}
		// Кодирвание по алгоритму LZ_78
		private List<Block_LZ_78> Encode_LZ_78(string text)
		{
			// Список блоков
			List<Block_LZ_78> blocks = new List<Block_LZ_78>();

			// Словарь
			Dictionary<string, int> dictonary = new Dictionary<string, int>();

			// По тексту
			for (int pos_text = 0, max_len_pref=1; pos_text < text.Length; pos_text++)
			{
				// Префикс, который постепенно будет уменьшаться до первого соответсвия в словаре
				string prefix = text.Substring(pos_text, Math.Min(max_len_pref, text.Length - pos_text));
				while(prefix.Length > 0)
				{
					if(dictonary.ContainsKey(prefix))
						break;
					else
						prefix = prefix.Substring(0, prefix.Length-1);
				}
				
				if(prefix.Length > 0)
				{//Если слово нашлось в словаре
					int pos_next = pos_text + prefix.Length;
					if (pos_next < text.Length)
					{
						blocks.Add(new Block_LZ_78() { Pos_in_dict = dictonary[prefix] + 1, Next = text[pos_next] });
						dictonary[prefix + text[pos_next]] = dictonary.Count;
						if (max_len_pref < prefix.Length+1)
							max_len_pref = prefix.Length+1;
					}
					else
					{// За пределы текста
						blocks.Add(new Block_LZ_78() { Pos_in_dict = dictonary[prefix]});
					}
					pos_text = pos_next;
				}
				else
				{// Слово не нашлось в словаре
					dictonary[text[pos_text].ToString()] = dictonary.Count;
					blocks.Add(new Block_LZ_78() { Next = text[pos_text] });
				}
				
				//Проценты для прогрессБара
				bW_encode.ReportProgress(100 * pos_text / text.Length);
			}

			return blocks;
		}
		// Кодирвание по алгоритму LZ_77
		private List<Block_LZ_77> Encode_LZ_77(string text)
		{
			int max_len_buf = (int)nUD_buf_len.Value;
			int max_len_dict = (int)nUD_dict_len.Value;

			// Список блоков
			List<Block_LZ_77> blocks = new List<Block_LZ_77>();

			// Словарь изначально пуст. Такой аргумент, потому что будет добавляться и удаляться
			StringBuilder dict = new StringBuilder(max_len_dict + max_len_buf);
			StringBuilder buffer;
			// По тексту вперёд
			for (int pos_in_text = 0, pos_next_char; pos_in_text < text.Length; pos_in_text = pos_next_char + 1)
			{
				// Буфер становится меньшей длины в конце текста
				buffer = new StringBuilder(text, pos_in_text, Math.Min(max_len_buf, text.Length - pos_in_text), max_len_buf);
				// Проверка, не содержит ли словарь на какой-нибудь позиции префикс буффера
				Find2in1(dict, buffer, out int pos_in_dict, out StringBuilder coincidence);

				// Блок для записи в бинарный файл
				Block_LZ_77 block = new Block_LZ_77
				{
					// Длина совпадения
					Length = coincidence.Length,
					// Смещение - номер справа налево, начиная с 1
					Offset = dict.Length - pos_in_dict
				};
				// Позиция следующей буквы
				pos_next_char = pos_in_text + block.Length;

				if (pos_next_char < text.Length)
				{
					// Следующая буква
					block.Next = text[pos_next_char];
					// Изменения в словаре
					dict.Append(coincidence).Append(text[pos_next_char]);
					// Если длина словаря больше максимальной длины, то убираем начало
					if (dict.Length > max_len_dict)
						dict.Remove(0, dict.Length - max_len_dict);
				}

				// Добавляем блок в список
				blocks.Add(block);

				//Проценты для прогрессБара
				bW_encode.ReportProgress(100 * pos_in_text / text.Length);
			}
			return blocks;
		}
		// Блоки в дефолтный бинарный файл
		private void WriteToBinFile_LZ_77(List<Block_LZ_77> blocks)
		{
			// Записываем в новый
			using (BinaryWriter writer = new BinaryWriter(File.Create(binFileDefault), Encoding.Default))
			{
				// записываем в файл значение каждого поля структуры.
				// В соответсвии с самым большим значением, выбирается тип данных
				if (nUD_dict_len.Value < 256)
				{
					foreach (Block_LZ_77 b in blocks)
					{
						writer.Write((byte)b.Offset);
						writer.Write((byte)b.Length);
						writer.Write(b.Next);
					}
				}
				else if (nUD_dict_len.Value < 65536)
				{
					foreach (Block_LZ_77 b in blocks)
					{
						writer.Write((ushort)b.Offset);
						writer.Write((ushort)b.Length);
						writer.Write(b.Next);
					}
				}
				else if (nUD_dict_len.Value <= 2147483647)
				{
					foreach (Block_LZ_77 b in blocks)
					{
						writer.Write(b.Offset);
						writer.Write(b.Length);
						writer.Write(b.Next);
					}
				}
			}
		}
		private void WriteToBinFile_LZ_78(List<Block_LZ_78> blocks)
		{
			// Записываем в новый
			using (BinaryWriter writer = new BinaryWriter(File.Create(binFileDefault), Encoding.Default))
			{
				// Длина словаря, а значит и самое большое число
				writer.Write(blocks.Count);
				// записываем в файл значение каждого поля структуры.
				// В соответсвии с самым большим значением, выбирается тип данных
				if (blocks.Count < 256)
				{
					foreach (Block_LZ_78 b in blocks)
					{
						writer.Write((byte)b.Pos_in_dict);
						writer.Write(b.Next);
					}
				}
				else if (blocks.Count < 65536)
				{
					foreach (Block_LZ_78 b in blocks)
					{
						writer.Write((ushort)b.Pos_in_dict);
						writer.Write(b.Next);
					}
				}
				else if (blocks.Count <= 2147483647)
				{
					foreach (Block_LZ_78 b in blocks)
					{
						writer.Write(b.Pos_in_dict);
						writer.Write(b.Next);
					}
				}
			}
		}

		// Клик по кнопке: "Загрузить и декодировать"
		private void Button_unzip_Click(object sender, EventArgs e)
		{
			#region ЗАГРУЗКА БИНАРНОГО ФАЙЛА
			// Создать и настроить диалог открытия файла
			OpenFileDialog Fd = new OpenFileDialog();
			Fd.Title = "Выберите файл";
			Fd.Filter = "Бинарные файлы|*.dat";

			// Переход в папку с exe
			Fd.InitialDirectory = Environment.CurrentDirectory;

			// Если файл не был выбран, выйти из функции
			if (Fd.ShowDialog() != DialogResult.OK)
				return;

			#endregion
			// Запуск в фоновом потоке
			bW_decode.RunWorkerAsync(Fd.FileName);
		}
		// Функция декодирования в фоновом потоке
		private void BW_decode_DoWork(object sender, DoWorkEventArgs e)
		{
			bW_decode.ReportProgress(0);// Прогресс = 0
			string fileName = (string)e.Argument;
			// Размер сжатого файла
			tB_size_squeeze.Text = GetFileSize(fileName);
			// Сюда запишется после декодирования
			tB_size_ish.Text = "";
			string text="";

			// Какой алгоритм выбран?
			if (rB_lz77.Checked)
			{
				// Считать бинарный файл
				List<Block_LZ_77> blocks = ReadFromBinFile_LZ_77(fileName);
				text = Decode_LZ_77(blocks);
				blocks.Clear();
			}
			else if (rB_lz78.Checked)
			{
				// Считать бинарный файл
				List<Block_LZ_78> blocks = ReadFromBinFile_LZ_78(fileName);
				text = Decode_LZ_78(blocks);
				blocks.Clear();
			}
			WriteToTxtFile(text); // в дефолтный
			tB_size_ish.Text = GetFileSize(txtFileDefault);
			bW_decode.ReportProgress(100);// Прогресс = 100
		}
		

		// Считывание из бинарного файла
		private List<Block_LZ_78> ReadFromBinFile_LZ_78(string fileName)
		{
			List<Block_LZ_78> blocks = new List<Block_LZ_78>();
			using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open), Encoding.Default))
			{
				int bl_count = reader.ReadInt32();
				// В соответсвии с самым большим значением, выбирается тип данных
				if (bl_count < 256)
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						blocks.Add(new Block_LZ_78() {Pos_in_dict = reader.ReadByte(), Next = reader.ReadChar() });
					}
				}
				else if (bl_count < 65536)
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						blocks.Add(new Block_LZ_78() { Pos_in_dict = reader.ReadUInt16(), Next = reader.ReadChar() });
					}
				}
				else if (bl_count <= 2147483647)
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						blocks.Add(new Block_LZ_78() { Pos_in_dict = reader.ReadInt32(), Next = reader.ReadChar() });
					}
				}
			}
			return blocks;
		}
		private List<Block_LZ_77> ReadFromBinFile_LZ_77(string fileName)
		{
			List<Block_LZ_77> blocks = new List<Block_LZ_77>();
			using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open), Encoding.Default))
			{
				// В соответсвии с самым большим значением, выбирается тип данных
				if (nUD_dict_len.Value < 256)
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						Block_LZ_77 b = new Block_LZ_77(reader.ReadByte(), reader.ReadByte(), reader.ReadChar());
						blocks.Add(b);
					}
				}
				else if (nUD_dict_len.Value < 65536)
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						Block_LZ_77 b = new Block_LZ_77(reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadChar());
						blocks.Add(b);
					}
				}
				else if (nUD_dict_len.Value <= 2147483647)
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						Block_LZ_77 b = new Block_LZ_77(reader.ReadInt32(), reader.ReadInt32(), reader.ReadChar());
						blocks.Add(b);
					}
				}
			}
			return blocks;
		}
		// Декодирование по алгоритму LZ_77
		private string Decode_LZ_77(List<Block_LZ_77> blocks)
		{
			StringBuilder all_text = new StringBuilder("");
			int max_len_buf = (int)nUD_buf_len.Value;
			int max_len_dict = (int)nUD_dict_len.Value;
			// Словарь изначально пуст. Такой аргумент, потому что будет добавляться и удаляться
			StringBuilder dict = new StringBuilder(max_len_dict + max_len_buf);
			int pos_bl = 0;
			foreach (Block_LZ_77 b in blocks)
			{
				string combs = dict.ToString().Substring(dict.Length - b.Offset, b.Length);
				// в вывод
				all_text.Append(combs).Append(b.Next);
				// в словарь
				dict.Append(combs).Append(b.Next);
				// Если длина словаря больше максимальной длины, то убирать начало
				if (dict.Length > max_len_dict)
					dict.Remove(0, dict.Length - max_len_dict);

				pos_bl++;
				//Проценты для прогрессБара
				bW_decode.ReportProgress(100 * pos_bl / blocks.Count);
			}
			return all_text.ToString();
		}
		// Декодирование по алгоритму LZ_78
		private string Decode_LZ_78(List<Block_LZ_78> blocks)
		{
			// Результат
			StringBuilder all_text = new StringBuilder("");
			// Словарь
			List<string> dict = new List<string>();
			foreach (Block_LZ_78 b in blocks)
			{
				string word_new;
				if (b.Pos_in_dict > 0)
					word_new = dict[b.Pos_in_dict - 1] + b.Next;
				else
					word_new = b.Next.ToString();
				dict.Add(word_new);
				all_text.Append(word_new);
			}
			return all_text.ToString();
		}
		// Записать в дефолтный текстовый файл текст
		private void WriteToTxtFile(string text)
		{
			// Записываем в новый дефолтный
			using (StreamWriter sw = new StreamWriter(txtFileDefault, false, Encoding.Default))
			{
				sw.Write(text);
			}
		}
		
		
		// Нахождение позиции максимального вхождения
		private void Find2in1(StringBuilder sb1, StringBuilder sb2, out int pos, out StringBuilder coincidence)
		{
			coincidence = new StringBuilder(sb2.Length);
			pos = sb1.Length;
			int max = 0;
			for(int pos1 = 0; pos1 < sb1.Length; )
			{
				int pos2 = 0;
				while (pos2 < sb2.Length && pos1 < sb1.Length && sb1[pos1] == sb2[pos2])
				{
					pos1++;
					pos2++;
					if (pos2 > max)
					{
						pos = pos1 - pos2;
						coincidence.Append(sb2[pos2-1]);
						max = pos2;
					}
				}
				pos1 = pos1 - pos2 + 1;
			}
		}
		// Получить размер файла
		private string GetFileSize(string path)
		{
			long in_B = new FileInfo(path).Length;
			if (in_B < 1024)
				return in_B.ToString() + " Б";
			else
			{
				double in_kB = in_B / 1024;
				if (in_kB < 1024)
					return in_kB.ToString() + " КБ";
				else
				{
					double in_MB = in_kB / 1024;
					if (in_MB < 1024)
						return in_MB.ToString() + " МБ";
					else
					{
						double in_GB = in_MB / 1024;
						return in_GB.ToString() + " ГБ";
					}
				}
			}
		}
		// Оповещение для прогресса для любого потока
		private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (pB.Value != e.ProgressPercentage)
				pB.Value = e.ProgressPercentage;
		}
	}
}
