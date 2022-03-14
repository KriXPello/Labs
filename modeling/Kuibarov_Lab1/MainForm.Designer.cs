namespace Kuibarov_Lab1
{
    partial class MainForm
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
            this.resultBox = new System.Windows.Forms.RichTextBox();
            this.schema1Button = new System.Windows.Forms.Button();
            this.schema2Button = new System.Windows.Forms.Button();
            this.pInput = new System.Windows.Forms.TextBox();
            this.qInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(12, 95);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(156, 81);
            this.resultBox.TabIndex = 0;
            this.resultBox.Text = "";
            // 
            // schema1Button
            // 
            this.schema1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.schema1Button.Location = new System.Drawing.Point(12, 66);
            this.schema1Button.Name = "schema1Button";
            this.schema1Button.Size = new System.Drawing.Size(75, 23);
            this.schema1Button.TabIndex = 3;
            this.schema1Button.Text = "Задача 1";
            this.schema1Button.UseVisualStyleBackColor = false;
            this.schema1Button.Click += new System.EventHandler(this.Schema1Button_Click);
            // 
            // schema2Button
            // 
            this.schema2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.schema2Button.Location = new System.Drawing.Point(93, 66);
            this.schema2Button.Name = "schema2Button";
            this.schema2Button.Size = new System.Drawing.Size(75, 23);
            this.schema2Button.TabIndex = 3;
            this.schema2Button.Text = "Задача 2";
            this.schema2Button.UseVisualStyleBackColor = false;
            this.schema2Button.Click += new System.EventHandler(this.schema2Button_Click);
            // 
            // pInput
            // 
            this.pInput.Location = new System.Drawing.Point(12, 30);
            this.pInput.Name = "pInput";
            this.pInput.Size = new System.Drawing.Size(75, 20);
            this.pInput.TabIndex = 5;
            this.pInput.TextChanged += new System.EventHandler(this.PInput_TextChanged);
            // 
            // qInput
            // 
            this.qInput.Location = new System.Drawing.Point(93, 30);
            this.qInput.Name = "qInput";
            this.qInput.Size = new System.Drawing.Size(75, 20);
            this.qInput.TabIndex = 5;
            this.qInput.TextChanged += new System.EventHandler(this.QInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "p";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "q";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 184);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.qInput);
            this.Controls.Add(this.pInput);
            this.Controls.Add(this.schema2Button);
            this.Controls.Add(this.schema1Button);
            this.Controls.Add(this.resultBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox resultBox;
        private System.Windows.Forms.Button schema1Button;
        private System.Windows.Forms.Button schema2Button;
        private System.Windows.Forms.TextBox pInput;
        private System.Windows.Forms.TextBox qInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

