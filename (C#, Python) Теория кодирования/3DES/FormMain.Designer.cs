namespace _3_DES
{
	partial class FormMain
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
			this.rTB_ish = new System.Windows.Forms.RichTextBox();
			this.rTB_cod = new System.Windows.Forms.RichTextBox();
			this.btn_ish_to_code = new System.Windows.Forms.Button();
			this.btn_code_to_ish = new System.Windows.Forms.Button();
			this.tB_key_1 = new System.Windows.Forms.TextBox();
			this.btn_new_key_1 = new System.Windows.Forms.Button();
			this.btn_new_key_2 = new System.Windows.Forms.Button();
			this.tB_key_2 = new System.Windows.Forms.TextBox();
			this.btn_new_key_3 = new System.Windows.Forms.Button();
			this.tB_key_3 = new System.Windows.Forms.TextBox();
			this.rTB_ish_10 = new System.Windows.Forms.RichTextBox();
			this.rTB_cod_10 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rTB_ish
			// 
			this.rTB_ish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rTB_ish.Location = new System.Drawing.Point(7, 109);
			this.rTB_ish.Name = "rTB_ish";
			this.rTB_ish.Size = new System.Drawing.Size(620, 61);
			this.rTB_ish.TabIndex = 0;
			this.rTB_ish.Text = "";
			// 
			// rTB_cod
			// 
			this.rTB_cod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rTB_cod.Location = new System.Drawing.Point(8, 301);
			this.rTB_cod.Name = "rTB_cod";
			this.rTB_cod.Size = new System.Drawing.Size(621, 60);
			this.rTB_cod.TabIndex = 1;
			this.rTB_cod.Text = "";
			// 
			// btn_ish_to_code
			// 
			this.btn_ish_to_code.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_ish_to_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_ish_to_code.Location = new System.Drawing.Point(634, 109);
			this.btn_ish_to_code.Name = "btn_ish_to_code";
			this.btn_ish_to_code.Size = new System.Drawing.Size(22, 186);
			this.btn_ish_to_code.TabIndex = 2;
			this.btn_ish_to_code.Text = "Шифровать";
			this.btn_ish_to_code.UseVisualStyleBackColor = true;
			this.btn_ish_to_code.Click += new System.EventHandler(this.btn_ish_to_code_Click);
			// 
			// btn_code_to_ish
			// 
			this.btn_code_to_ish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_code_to_ish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_code_to_ish.Location = new System.Drawing.Point(635, 301);
			this.btn_code_to_ish.Name = "btn_code_to_ish";
			this.btn_code_to_ish.Size = new System.Drawing.Size(22, 186);
			this.btn_code_to_ish.TabIndex = 3;
			this.btn_code_to_ish.Text = "Дешифровать";
			this.btn_code_to_ish.UseVisualStyleBackColor = true;
			this.btn_code_to_ish.Click += new System.EventHandler(this.btn_code_to_ish_Click);
			// 
			// tB_key_1
			// 
			this.tB_key_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tB_key_1.Location = new System.Drawing.Point(7, 4);
			this.tB_key_1.Name = "tB_key_1";
			this.tB_key_1.Size = new System.Drawing.Size(526, 26);
			this.tB_key_1.TabIndex = 4;
			// 
			// btn_new_key_1
			// 
			this.btn_new_key_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_new_key_1.Location = new System.Drawing.Point(539, 4);
			this.btn_new_key_1.Name = "btn_new_key_1";
			this.btn_new_key_1.Size = new System.Drawing.Size(126, 26);
			this.btn_new_key_1.TabIndex = 5;
			this.btn_new_key_1.Text = "Новый ключ 1";
			this.btn_new_key_1.UseVisualStyleBackColor = true;
			// 
			// btn_new_key_2
			// 
			this.btn_new_key_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_new_key_2.Location = new System.Drawing.Point(540, 36);
			this.btn_new_key_2.Name = "btn_new_key_2";
			this.btn_new_key_2.Size = new System.Drawing.Size(126, 26);
			this.btn_new_key_2.TabIndex = 7;
			this.btn_new_key_2.Text = "Новый ключ 2";
			this.btn_new_key_2.UseVisualStyleBackColor = true;
			// 
			// tB_key_2
			// 
			this.tB_key_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tB_key_2.Location = new System.Drawing.Point(8, 36);
			this.tB_key_2.Name = "tB_key_2";
			this.tB_key_2.Size = new System.Drawing.Size(526, 26);
			this.tB_key_2.TabIndex = 6;
			// 
			// btn_new_key_3
			// 
			this.btn_new_key_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_new_key_3.Location = new System.Drawing.Point(540, 68);
			this.btn_new_key_3.Name = "btn_new_key_3";
			this.btn_new_key_3.Size = new System.Drawing.Size(126, 26);
			this.btn_new_key_3.TabIndex = 9;
			this.btn_new_key_3.Text = "Новый ключ 3";
			this.btn_new_key_3.UseVisualStyleBackColor = true;
			// 
			// tB_key_3
			// 
			this.tB_key_3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tB_key_3.Location = new System.Drawing.Point(8, 68);
			this.tB_key_3.Name = "tB_key_3";
			this.tB_key_3.Size = new System.Drawing.Size(526, 26);
			this.tB_key_3.TabIndex = 8;
			// 
			// rTB_ish_10
			// 
			this.rTB_ish_10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rTB_ish_10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rTB_ish_10.Location = new System.Drawing.Point(7, 176);
			this.rTB_ish_10.Name = "rTB_ish_10";
			this.rTB_ish_10.ReadOnly = true;
			this.rTB_ish_10.Size = new System.Drawing.Size(620, 119);
			this.rTB_ish_10.TabIndex = 10;
			this.rTB_ish_10.Text = "";
			// 
			// rTB_cod_10
			// 
			this.rTB_cod_10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rTB_cod_10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rTB_cod_10.Location = new System.Drawing.Point(7, 367);
			this.rTB_cod_10.Name = "rTB_cod_10";
			this.rTB_cod_10.ReadOnly = true;
			this.rTB_cod_10.Size = new System.Drawing.Size(620, 119);
			this.rTB_cod_10.TabIndex = 11;
			this.rTB_cod_10.Text = "";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(668, 493);
			this.Controls.Add(this.rTB_cod_10);
			this.Controls.Add(this.rTB_ish_10);
			this.Controls.Add(this.btn_new_key_3);
			this.Controls.Add(this.tB_key_3);
			this.Controls.Add(this.btn_new_key_2);
			this.Controls.Add(this.tB_key_2);
			this.Controls.Add(this.btn_new_key_1);
			this.Controls.Add(this.tB_key_1);
			this.Controls.Add(this.btn_code_to_ish);
			this.Controls.Add(this.btn_ish_to_code);
			this.Controls.Add(this.rTB_cod);
			this.Controls.Add(this.rTB_ish);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "FormMain";
			this.Text = "3DES";
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox rTB_ish;
		private System.Windows.Forms.RichTextBox rTB_cod;
		private System.Windows.Forms.Button btn_ish_to_code;
		private System.Windows.Forms.Button btn_code_to_ish;
		private System.Windows.Forms.TextBox tB_key_1;
		private System.Windows.Forms.Button btn_new_key_1;
		private System.Windows.Forms.Button btn_new_key_2;
		private System.Windows.Forms.TextBox tB_key_2;
		private System.Windows.Forms.Button btn_new_key_3;
		private System.Windows.Forms.TextBox tB_key_3;
		private System.Windows.Forms.RichTextBox rTB_ish_10;
		private System.Windows.Forms.RichTextBox rTB_cod_10;
	}
}

