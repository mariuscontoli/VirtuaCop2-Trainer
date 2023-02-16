namespace Virtua_Cop_2_trainer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.UnlimAmmoBTN = new System.Windows.Forms.Button();
            this.UnlimLifeBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.UnlimCreditsBTN = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.multiIncr = new System.Windows.Forms.Button();
            this.multiMinus = new System.Windows.Forms.Button();
            this.multiValue = new System.Windows.Forms.TextBox();
            this.GameAvailabilityTimer = new System.Windows.Forms.Timer(this.components);
            this.UpdateCheatTimer = new System.Windows.Forms.Timer(this.components);
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "UNLIMITED AMMO";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // UnlimAmmoBTN
            // 
            this.UnlimAmmoBTN.BackColor = System.Drawing.Color.Lime;
            this.UnlimAmmoBTN.Font = new System.Drawing.Font("Mistral", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnlimAmmoBTN.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.UnlimAmmoBTN.Location = new System.Drawing.Point(18, 86);
            this.UnlimAmmoBTN.Name = "UnlimAmmoBTN";
            this.UnlimAmmoBTN.Size = new System.Drawing.Size(124, 79);
            this.UnlimAmmoBTN.TabIndex = 1;
            this.UnlimAmmoBTN.Text = "1.OFF";
            this.UnlimAmmoBTN.UseVisualStyleBackColor = false;
            this.UnlimAmmoBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // UnlimLifeBTN
            // 
            this.UnlimLifeBTN.BackColor = System.Drawing.Color.Lime;
            this.UnlimLifeBTN.Font = new System.Drawing.Font("Mistral", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnlimLifeBTN.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.UnlimLifeBTN.Location = new System.Drawing.Point(171, 86);
            this.UnlimLifeBTN.Name = "UnlimLifeBTN";
            this.UnlimLifeBTN.Size = new System.Drawing.Size(128, 79);
            this.UnlimLifeBTN.TabIndex = 2;
            this.UnlimLifeBTN.Text = "2.OFF";
            this.UnlimLifeBTN.UseVisualStyleBackColor = false;
            this.UnlimLifeBTN.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(175, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "INFINITE LIFE";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // UnlimCreditsBTN
            // 
            this.UnlimCreditsBTN.BackColor = System.Drawing.Color.Lime;
            this.UnlimCreditsBTN.Font = new System.Drawing.Font("Mistral", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnlimCreditsBTN.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.UnlimCreditsBTN.Location = new System.Drawing.Point(18, 245);
            this.UnlimCreditsBTN.Name = "UnlimCreditsBTN";
            this.UnlimCreditsBTN.Size = new System.Drawing.Size(124, 85);
            this.UnlimCreditsBTN.TabIndex = 4;
            this.UnlimCreditsBTN.Text = "3.OFF";
            this.UnlimCreditsBTN.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cyan;
            this.label3.Location = new System.Drawing.Point(12, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "UNLIMITED CREDITS";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Cyan;
            this.label4.Location = new System.Drawing.Point(175, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 32);
            this.label4.TabIndex = 6;
            this.label4.Text = "SCORE MULTIPLIER";
            // 
            // multiIncr
            // 
            this.multiIncr.Font = new System.Drawing.Font("Mistral", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiIncr.Location = new System.Drawing.Point(305, 245);
            this.multiIncr.Name = "multiIncr";
            this.multiIncr.Size = new System.Drawing.Size(33, 32);
            this.multiIncr.TabIndex = 7;
            this.multiIncr.Text = "+";
            this.multiIncr.UseVisualStyleBackColor = true;
            // 
            // multiMinus
            // 
            this.multiMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiMinus.Location = new System.Drawing.Point(305, 280);
            this.multiMinus.Name = "multiMinus";
            this.multiMinus.Size = new System.Drawing.Size(33, 50);
            this.multiMinus.TabIndex = 8;
            this.multiMinus.Text = "-";
            this.multiMinus.UseVisualStyleBackColor = true;
            this.multiMinus.Click += new System.EventHandler(this.button5_Click);
            // 
            // multiValue
            // 
            this.multiValue.BackColor = System.Drawing.Color.Lime;
            this.multiValue.Font = new System.Drawing.Font("Mistral", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiValue.Location = new System.Drawing.Point(171, 245);
            this.multiValue.Name = "multiValue";
            this.multiValue.ReadOnly = true;
            this.multiValue.ShortcutsEnabled = false;
            this.multiValue.Size = new System.Drawing.Size(128, 85);
            this.multiValue.TabIndex = 9;
            this.multiValue.Text = "1";
            this.multiValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.multiValue.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // GameAvailabilityTimer
            // 
            this.GameAvailabilityTimer.Enabled = true;
            this.GameAvailabilityTimer.Tick += new System.EventHandler(this.GameAvailabilityTimer_Tick);
            // 
            // UpdateCheatTimer
            // 
            this.UpdateCheatTimer.Enabled = true;
            this.UpdateCheatTimer.Interval = 250;
            this.UpdateCheatTimer.Tick += new System.EventHandler(this.UpdateCheatTimer_Tick);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Orange;
            this.statusLabel.Location = new System.Drawing.Point(12, 9);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(228, 32);
            this.statusLabel.TabIndex = 10;
            this.statusLabel.Text = "Status : Process not found";
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(350, 471);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.multiValue);
            this.Controls.Add(this.multiMinus);
            this.Controls.Add(this.multiIncr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UnlimCreditsBTN);
            this.Controls.Add(this.UnlimLifeBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UnlimAmmoBTN);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Virtua Cop 2 Trainer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UnlimAmmoBTN;
        private System.Windows.Forms.Button UnlimLifeBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button UnlimCreditsBTN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button multiIncr;
        private System.Windows.Forms.Button multiMinus;
        private System.Windows.Forms.TextBox multiValue;
        private System.Windows.Forms.Timer GameAvailabilityTimer;
        private System.Windows.Forms.Timer UpdateCheatTimer;
        private System.Windows.Forms.Label statusLabel;
    }
}

