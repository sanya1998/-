namespace LZ_77__LZ_78
{
	partial class Form_Main
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.rB_lz77 = new System.Windows.Forms.RadioButton();
			this.rB_lz78 = new System.Windows.Forms.RadioButton();
			this.gB_Alg = new System.Windows.Forms.GroupBox();
			this.gB_squeeze = new System.Windows.Forms.GroupBox();
			this.button_squeeze = new System.Windows.Forms.Button();
			this.gB_dict_len = new System.Windows.Forms.GroupBox();
			this.nUD_dict_len = new System.Windows.Forms.NumericUpDown();
			this.gB_buf_len = new System.Windows.Forms.GroupBox();
			this.nUD_buf_len = new System.Windows.Forms.NumericUpDown();
			this.gB_info = new System.Windows.Forms.GroupBox();
			this.pB = new System.Windows.Forms.ProgressBar();
			this.tB_size_squeeze = new System.Windows.Forms.TextBox();
			this.tB_size_ish = new System.Windows.Forms.TextBox();
			this.label_size_squeeze = new System.Windows.Forms.Label();
			this.label_size_ish = new System.Windows.Forms.Label();
			this.label_progress = new System.Windows.Forms.Label();
			this.gB_unzip = new System.Windows.Forms.GroupBox();
			this.button_unzip = new System.Windows.Forms.Button();
			this.bW_encode = new System.ComponentModel.BackgroundWorker();
			this.bW_decode = new System.ComponentModel.BackgroundWorker();
			this.gB_Alg.SuspendLayout();
			this.gB_squeeze.SuspendLayout();
			this.gB_dict_len.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nUD_dict_len)).BeginInit();
			this.gB_buf_len.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nUD_buf_len)).BeginInit();
			this.gB_info.SuspendLayout();
			this.gB_unzip.SuspendLayout();
			this.SuspendLayout();
			// 
			// rB_lz77
			// 
			this.rB_lz77.AutoSize = true;
			this.rB_lz77.Location = new System.Drawing.Point(8, 25);
			this.rB_lz77.Margin = new System.Windows.Forms.Padding(4);
			this.rB_lz77.Name = "rB_lz77";
			this.rB_lz77.Size = new System.Drawing.Size(63, 22);
			this.rB_lz77.TabIndex = 0;
			this.rB_lz77.Text = "LZ 77";
			this.rB_lz77.UseVisualStyleBackColor = true;
			this.rB_lz77.CheckedChanged += new System.EventHandler(this.RB_lz77_CheckedChanged);
			// 
			// rB_lz78
			// 
			this.rB_lz78.AutoSize = true;
			this.rB_lz78.Checked = true;
			this.rB_lz78.Location = new System.Drawing.Point(79, 25);
			this.rB_lz78.Margin = new System.Windows.Forms.Padding(4);
			this.rB_lz78.Name = "rB_lz78";
			this.rB_lz78.Size = new System.Drawing.Size(63, 22);
			this.rB_lz78.TabIndex = 1;
			this.rB_lz78.TabStop = true;
			this.rB_lz78.Text = "LZ 78";
			this.rB_lz78.UseVisualStyleBackColor = true;
			// 
			// gB_Alg
			// 
			this.gB_Alg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gB_Alg.Controls.Add(this.rB_lz77);
			this.gB_Alg.Controls.Add(this.rB_lz78);
			this.gB_Alg.Location = new System.Drawing.Point(9, 10);
			this.gB_Alg.Margin = new System.Windows.Forms.Padding(4);
			this.gB_Alg.Name = "gB_Alg";
			this.gB_Alg.Padding = new System.Windows.Forms.Padding(4);
			this.gB_Alg.Size = new System.Drawing.Size(425, 53);
			this.gB_Alg.TabIndex = 2;
			this.gB_Alg.TabStop = false;
			this.gB_Alg.Text = "Алгоритм";
			// 
			// gB_squeeze
			// 
			this.gB_squeeze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gB_squeeze.Controls.Add(this.button_squeeze);
			this.gB_squeeze.Location = new System.Drawing.Point(9, 179);
			this.gB_squeeze.Margin = new System.Windows.Forms.Padding(4);
			this.gB_squeeze.Name = "gB_squeeze";
			this.gB_squeeze.Padding = new System.Windows.Forms.Padding(4);
			this.gB_squeeze.Size = new System.Drawing.Size(425, 54);
			this.gB_squeeze.TabIndex = 4;
			this.gB_squeeze.TabStop = false;
			// 
			// button_squeeze
			// 
			this.button_squeeze.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button_squeeze.Location = new System.Drawing.Point(4, 21);
			this.button_squeeze.Margin = new System.Windows.Forms.Padding(4);
			this.button_squeeze.Name = "button_squeeze";
			this.button_squeeze.Size = new System.Drawing.Size(417, 29);
			this.button_squeeze.TabIndex = 1;
			this.button_squeeze.Text = "Загрузить и закодировать";
			this.button_squeeze.UseVisualStyleBackColor = true;
			this.button_squeeze.Click += new System.EventHandler(this.Button_squeeze_Click);
			// 
			// gB_dict_len
			// 
			this.gB_dict_len.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gB_dict_len.Controls.Add(this.nUD_dict_len);
			this.gB_dict_len.Enabled = false;
			this.gB_dict_len.Location = new System.Drawing.Point(9, 66);
			this.gB_dict_len.Margin = new System.Windows.Forms.Padding(4);
			this.gB_dict_len.Name = "gB_dict_len";
			this.gB_dict_len.Padding = new System.Windows.Forms.Padding(4);
			this.gB_dict_len.Size = new System.Drawing.Size(425, 53);
			this.gB_dict_len.TabIndex = 5;
			this.gB_dict_len.TabStop = false;
			this.gB_dict_len.Text = "Длина словаря";
			// 
			// nUD_dict_len
			// 
			this.nUD_dict_len.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nUD_dict_len.Location = new System.Drawing.Point(4, 21);
			this.nUD_dict_len.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.nUD_dict_len.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nUD_dict_len.Name = "nUD_dict_len";
			this.nUD_dict_len.Size = new System.Drawing.Size(417, 24);
			this.nUD_dict_len.TabIndex = 5;
			this.nUD_dict_len.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nUD_dict_len.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			// 
			// gB_buf_len
			// 
			this.gB_buf_len.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gB_buf_len.Controls.Add(this.nUD_buf_len);
			this.gB_buf_len.Enabled = false;
			this.gB_buf_len.Location = new System.Drawing.Point(9, 123);
			this.gB_buf_len.Margin = new System.Windows.Forms.Padding(4);
			this.gB_buf_len.Name = "gB_buf_len";
			this.gB_buf_len.Padding = new System.Windows.Forms.Padding(4);
			this.gB_buf_len.Size = new System.Drawing.Size(425, 53);
			this.gB_buf_len.TabIndex = 6;
			this.gB_buf_len.TabStop = false;
			this.gB_buf_len.Text = "Длина буфера";
			// 
			// nUD_buf_len
			// 
			this.nUD_buf_len.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nUD_buf_len.Location = new System.Drawing.Point(4, 21);
			this.nUD_buf_len.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.nUD_buf_len.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nUD_buf_len.Name = "nUD_buf_len";
			this.nUD_buf_len.Size = new System.Drawing.Size(417, 24);
			this.nUD_buf_len.TabIndex = 7;
			this.nUD_buf_len.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nUD_buf_len.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// gB_info
			// 
			this.gB_info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gB_info.Controls.Add(this.pB);
			this.gB_info.Controls.Add(this.tB_size_squeeze);
			this.gB_info.Controls.Add(this.tB_size_ish);
			this.gB_info.Controls.Add(this.label_size_squeeze);
			this.gB_info.Controls.Add(this.label_size_ish);
			this.gB_info.Controls.Add(this.label_progress);
			this.gB_info.Location = new System.Drawing.Point(9, 293);
			this.gB_info.Margin = new System.Windows.Forms.Padding(4);
			this.gB_info.Name = "gB_info";
			this.gB_info.Padding = new System.Windows.Forms.Padding(4);
			this.gB_info.Size = new System.Drawing.Size(425, 101);
			this.gB_info.TabIndex = 7;
			this.gB_info.TabStop = false;
			this.gB_info.Text = "Информация";
			// 
			// pB
			// 
			this.pB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pB.Location = new System.Drawing.Point(8, 19);
			this.pB.Name = "pB";
			this.pB.Size = new System.Drawing.Size(210, 23);
			this.pB.TabIndex = 6;
			// 
			// tB_size_squeeze
			// 
			this.tB_size_squeeze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tB_size_squeeze.Location = new System.Drawing.Point(8, 71);
			this.tB_size_squeeze.Name = "tB_size_squeeze";
			this.tB_size_squeeze.ReadOnly = true;
			this.tB_size_squeeze.Size = new System.Drawing.Size(210, 24);
			this.tB_size_squeeze.TabIndex = 5;
			this.tB_size_squeeze.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tB_size_ish
			// 
			this.tB_size_ish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tB_size_ish.Location = new System.Drawing.Point(8, 46);
			this.tB_size_ish.Name = "tB_size_ish";
			this.tB_size_ish.ReadOnly = true;
			this.tB_size_ish.Size = new System.Drawing.Size(210, 24);
			this.tB_size_ish.TabIndex = 4;
			this.tB_size_ish.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label_size_squeeze
			// 
			this.label_size_squeeze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_size_squeeze.AutoSize = true;
			this.label_size_squeeze.Location = new System.Drawing.Point(224, 74);
			this.label_size_squeeze.Name = "label_size_squeeze";
			this.label_size_squeeze.Size = new System.Drawing.Size(173, 18);
			this.label_size_squeeze.TabIndex = 2;
			this.label_size_squeeze.Text = "Размер сжатого файла";
			// 
			// label_size_ish
			// 
			this.label_size_ish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_size_ish.AutoSize = true;
			this.label_size_ish.Location = new System.Drawing.Point(224, 49);
			this.label_size_ish.Name = "label_size_ish";
			this.label_size_ish.Size = new System.Drawing.Size(188, 18);
			this.label_size_ish.TabIndex = 1;
			this.label_size_ish.Text = "Размер исходного файла";
			// 
			// label_progress
			// 
			this.label_progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_progress.AutoSize = true;
			this.label_progress.Location = new System.Drawing.Point(224, 24);
			this.label_progress.Name = "label_progress";
			this.label_progress.Size = new System.Drawing.Size(74, 18);
			this.label_progress.TabIndex = 0;
			this.label_progress.Text = "Прогресс";
			// 
			// gB_unzip
			// 
			this.gB_unzip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gB_unzip.Controls.Add(this.button_unzip);
			this.gB_unzip.Location = new System.Drawing.Point(9, 235);
			this.gB_unzip.Margin = new System.Windows.Forms.Padding(4);
			this.gB_unzip.Name = "gB_unzip";
			this.gB_unzip.Padding = new System.Windows.Forms.Padding(4);
			this.gB_unzip.Size = new System.Drawing.Size(425, 54);
			this.gB_unzip.TabIndex = 9;
			this.gB_unzip.TabStop = false;
			// 
			// button_unzip
			// 
			this.button_unzip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button_unzip.Location = new System.Drawing.Point(4, 21);
			this.button_unzip.Name = "button_unzip";
			this.button_unzip.Size = new System.Drawing.Size(417, 29);
			this.button_unzip.TabIndex = 3;
			this.button_unzip.Text = "Загрузить и декодировать";
			this.button_unzip.UseVisualStyleBackColor = true;
			this.button_unzip.Click += new System.EventHandler(this.Button_unzip_Click);
			// 
			// bW_encode
			// 
			this.bW_encode.WorkerReportsProgress = true;
			this.bW_encode.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_encode_DoWork);
			this.bW_encode.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BW_ProgressChanged);
			// 
			// bW_decode
			// 
			this.bW_decode.WorkerReportsProgress = true;
			this.bW_decode.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_decode_DoWork);
			this.bW_decode.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BW_ProgressChanged);
			// 
			// Form_Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(441, 402);
			this.Controls.Add(this.gB_unzip);
			this.Controls.Add(this.gB_info);
			this.Controls.Add(this.gB_buf_len);
			this.Controls.Add(this.gB_dict_len);
			this.Controls.Add(this.gB_squeeze);
			this.Controls.Add(this.gB_Alg);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(340, 100);
			this.Name = "Form_Main";
			this.Text = "Архиватор";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
			this.gB_Alg.ResumeLayout(false);
			this.gB_Alg.PerformLayout();
			this.gB_squeeze.ResumeLayout(false);
			this.gB_dict_len.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nUD_dict_len)).EndInit();
			this.gB_buf_len.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nUD_buf_len)).EndInit();
			this.gB_info.ResumeLayout(false);
			this.gB_info.PerformLayout();
			this.gB_unzip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton rB_lz77;
		private System.Windows.Forms.RadioButton rB_lz78;
		private System.Windows.Forms.GroupBox gB_Alg;
		private System.Windows.Forms.GroupBox gB_squeeze;
		private System.Windows.Forms.GroupBox gB_dict_len;
		private System.Windows.Forms.Button button_squeeze;
		private System.Windows.Forms.GroupBox gB_buf_len;
		private System.Windows.Forms.NumericUpDown nUD_dict_len;
		private System.Windows.Forms.NumericUpDown nUD_buf_len;
		private System.Windows.Forms.GroupBox gB_info;
		private System.Windows.Forms.Label label_size_squeeze;
		private System.Windows.Forms.Label label_size_ish;
		private System.Windows.Forms.Label label_progress;
		private System.Windows.Forms.TextBox tB_size_squeeze;
		private System.Windows.Forms.TextBox tB_size_ish;
		private System.Windows.Forms.GroupBox gB_unzip;
		private System.Windows.Forms.Button button_unzip;
		private System.ComponentModel.BackgroundWorker bW_encode;
		private System.ComponentModel.BackgroundWorker bW_decode;
		private System.Windows.Forms.ProgressBar pB;
	}
}

