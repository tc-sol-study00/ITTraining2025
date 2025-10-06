using Convenience.Data;

namespace EventStudy2 {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button3 = new Button();
            Message = new TextBox();
            Termination = new Button();
            button4 = new Button();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(67, 361);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "検索";
            button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(23, 24);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(984, 303);
            dataGridView1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(163, 361);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "更新";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(471, 361);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 3;
            button3.Text = "Reset";
            button3.UseVisualStyleBackColor = true;
            // 
            // Message
            // 
            Message.BackColor = SystemColors.InactiveCaptionText;
            Message.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Message.ForeColor = SystemColors.InactiveBorder;
            Message.Location = new Point(23, 332);
            Message.Name = "Message";
            Message.Size = new Size(406, 29);
            Message.TabIndex = 4;
            Message.Text = "メッセージエリア";
            // 
            // Termination
            // 
            Termination.Location = new Point(911, 361);
            Termination.Name = "Termination";
            Termination.Size = new Size(75, 23);
            Termination.TabIndex = 5;
            Termination.Text = "終了";
            Termination.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(655, 361);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 6;
            button4.Text = "集計";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(774, 361);
            button5.Name = "button5";
            button5.Size = new Size(101, 23);
            button5.TabIndex = 7;
            button5.Text = "集計（同期）";
            button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 450);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(Termination);
            Controls.Add(Message);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "注文実績明細検索・更新";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        public DataGridView dataGridView1;
        private Button button2;
        private Button button3;
        internal TextBox Message;
        private Button Termination;
        private Button button4;
        private Button button5;
    }
}
