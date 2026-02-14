namespace AsteroidsF23
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new Button();
            restartButton = new Button();
            instructionsLabel = new Label();
            levelCountLabel = new Label();
            scoreLabel = new Label();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(354, 190);
            startButton.Name = "startButton";
            startButton.Size = new Size(99, 55);
            startButton.TabIndex = 3;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // restartButton
            // 
            restartButton.Location = new Point(354, 267);
            restartButton.Name = "restartButton";
            restartButton.Size = new Size(99, 52);
            restartButton.TabIndex = 4;
            restartButton.Text = "Restart";
            restartButton.UseVisualStyleBackColor = true;
            restartButton.Click += restartButton_Click;
            // 
            // instructionsLabel
            // 
            instructionsLabel.AutoSize = true;
            instructionsLabel.ForeColor = SystemColors.ButtonHighlight;
            instructionsLabel.Location = new Point(297, 141);
            instructionsLabel.Name = "instructionsLabel";
            instructionsLabel.Size = new Size(213, 15);
            instructionsLabel.TabIndex = 5;
            instructionsLabel.Text = "Use WASD to move and Space to shoot";
            // 
            // levelCountLabel
            // 
            levelCountLabel.AutoSize = true;
            levelCountLabel.ForeColor = SystemColors.ButtonHighlight;
            levelCountLabel.Location = new Point(12, 9);
            levelCountLabel.Name = "levelCountLabel";
            levelCountLabel.Size = new Size(37, 15);
            levelCountLabel.TabIndex = 6;
            levelCountLabel.Text = "Level:";
            // 
            // scoreLabel
            // 
            scoreLabel.AutoSize = true;
            scoreLabel.ForeColor = SystemColors.ButtonHighlight;
            scoreLabel.Location = new Point(12, 33);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(39, 15);
            scoreLabel.TabIndex = 7;
            scoreLabel.Text = "Score:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(800, 450);
            Controls.Add(scoreLabel);
            Controls.Add(levelCountLabel);
            Controls.Add(instructionsLabel);
            Controls.Add(restartButton);
            Controls.Add(startButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private Button restartButton;
        private Label instructionsLabel;
        private Label levelCountLabel;
        private Label scoreLabel;
    }
}