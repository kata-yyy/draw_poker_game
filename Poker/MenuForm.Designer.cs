namespace PlayingCards
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.playerCountBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gameCountBox = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(52, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "プレイヤー数";
            // 
            // playerCountBox
            // 
            this.playerCountBox.BackColor = System.Drawing.SystemColors.Window;
            this.playerCountBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playerCountBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playerCountBox.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playerCountBox.FormattingEnabled = true;
            this.playerCountBox.Location = new System.Drawing.Point(267, 45);
            this.playerCountBox.Name = "playerCountBox";
            this.playerCountBox.Size = new System.Drawing.Size(68, 41);
            this.playerCountBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(52, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "ゲーム回数";
            // 
            // gameCountBox
            // 
            this.gameCountBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameCountBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameCountBox.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gameCountBox.FormattingEnabled = true;
            this.gameCountBox.Location = new System.Drawing.Point(267, 118);
            this.gameCountBox.Name = "gameCountBox";
            this.gameCountBox.Size = new System.Drawing.Size(68, 41);
            this.gameCountBox.TabIndex = 3;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("MS UI Gothic", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.startButton.Location = new System.Drawing.Point(118, 193);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(186, 53);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "ゲーム開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClicked);
            // 
            // PokerMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 258);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.gameCountBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playerCountBox);
            this.Controls.Add(this.label1);
            this.Name = "PokerMenu";
            this.Text = "メニュー";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox playerCountBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox gameCountBox;
        private System.Windows.Forms.Button startButton;
    }
}