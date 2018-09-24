namespace TestNewControl
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.numberTextBox1 = new Test.NumberTextBox(this.components);
            this.numberTextBox2 = new Test.NumberTextBox(this.components);
            this.SuspendLayout();
            // 
            // numberTextBox1
            // 
            this.numberTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.numberTextBox1.Location = new System.Drawing.Point(278, 123);
            this.numberTextBox1.Name = "numberTextBox1";
            this.numberTextBox1.Size = new System.Drawing.Size(100, 20);
            this.numberTextBox1.TabIndex = 0;
            // 
            // numberTextBox2
            // 
            this.numberTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numberTextBox2.Location = new System.Drawing.Point(278, 207);
            this.numberTextBox2.Name = "numberTextBox2";
            this.numberTextBox2.Size = new System.Drawing.Size(100, 20);
            this.numberTextBox2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numberTextBox2);
            this.Controls.Add(this.numberTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Test.NumberTextBox numberTextBox1;
        private Test.NumberTextBox numberTextBox2;
    }
}

