using System;
using System.Drawing;
using System.Windows.Forms;

namespace Virtua_Cop_2_trainer
{
    public partial class Form1 : Form
    {

        #region Global variables
        Memory oMemory = new Memory();
        bool IsGameAvailable = false;

        bool UnlimitedAmmo = false;
        string ammoPtr = "004CF1AC";
        int[] ammoOffset = { 0x28 };
        int AmmoToFill = 6;

        bool UnlimitedHealth = false;
        string healthPtr = "004CF220";
        int[] healthOffset = { 0x02 };
        int healthToSet = 9;

        string scorePtr = "10DD2D3C";
        int[] scoreOffset = { 0x10 };
        int oldScore;


        #endregion 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e) // AMMO
        {
            if (IsGameAvailable)
            {
                if (UnlimitedAmmo)
                {
                    UnlimitedAmmo = false;
                    UnlimAmmoBTN.Text = "OFF";
                }
                else
                {
                    UnlimitedAmmo = true;
                    UnlimAmmoBTN.Text = "ON";
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e) // HEALTH
        {
            if (IsGameAvailable)
            {
                if (UnlimitedHealth)
                {
                    UnlimitedHealth = false;
                    UnlimLifeBTN.Text = "OFF";
                }
                else
                {
                    UnlimitedHealth = true;
                    UnlimLifeBTN.Text = "ON";
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GameAvailabilityTimer_Tick(object sender, EventArgs e)
        {
            // TODO Implement the new way of handling process opening
            if (oMemory.GetProcessOldWay("PPJ2DD"))
            {
                IsGameAvailable = true;
                statusLabel.Text = "Status : Process found";
                statusLabel.ForeColor = Color.LimeGreen;
            }
            else
            {
                IsGameAvailable = false;
                statusLabel.Text = "Status : Process not found";
                statusLabel.ForeColor = Color.Red;
            }
        }

        private void statusLabel_Click(object sender, EventArgs e)
        {

        }

        private void UpdateCheatTimer_Tick(object sender, EventArgs e)
        {
            if (IsGameAvailable)
            {
                #region Unlimited Ammo
                if (UnlimitedAmmo)
                {
                    oMemory.OpenOldWay();
                    int ptrAddr = oMemory.Dec(ammoPtr);
                    int[] ptrOffset = ammoOffset;
                    byte[] valueToWrite = BitConverter.GetBytes(AmmoToFill);
                    if (oMemory.Write(ptrAddr, ptrOffset, valueToWrite))
                    {
                        Console.WriteLine("Successfully writing {0} to address {1}", BitConverter.ToString(valueToWrite), ptrAddr);

                    }
                    else
                    {
                        Console.WriteLine("Error writing {0} to address {1}", BitConverter.ToString(valueToWrite), ptrAddr);
                    }
                }
                #endregion

                #region Unlimited Health
                if (UnlimitedHealth)
                {
                    oMemory.OpenOldWay();
                    int ptrAddr = oMemory.Dec(healthPtr);
                    int[] ptrOffset = healthOffset;
                    byte[] valueToWrite = BitConverter.GetBytes(healthToSet);
                    if (oMemory.Write(ptrAddr, ptrOffset, valueToWrite))
                    {
                        Console.WriteLine("Successfully writing {0} to address {1}", BitConverter.ToString(valueToWrite), ptrAddr);
                    }
                    else
                    {
                        Console.WriteLine("Fails writing {0} to address {1}", BitConverter.ToString(valueToWrite), ptrAddr);
                    }
                }
                #endregion

                #region Score Multiplier

                //oMemory.OpenOldWay();
                int scorePtrAddr = oMemory.Dec(scorePtr);
                int[] scorePtrOffset = scoreOffset;
                uint scoreToChange = oMemory.ReadInt(scorePtrAddr, scorePtrOffset);
                Console.WriteLine(scoreToChange);
                int score;
                try
                {
                    score = Convert.ToInt32(scoreToChange);
                }
                catch
                {
                    score = oldScore;
                    Console.WriteLine("Max score reached");
                }

                int scoreDiff, scoreMultiplied;
                if (scoreToChange != oldScore)
                {
                    scoreDiff = score - oldScore;
                    scoreMultiplied = scoreDiff * Convert.ToInt32(multiValue.Text);
                    score -= scoreDiff;
                    score += scoreMultiplied;
                    oldScore = score;

                    byte[] valueToWrite = BitConverter.GetBytes(score);
                    if (oMemory.Write(scorePtrAddr, scorePtrOffset, valueToWrite))
                    {
                        Console.WriteLine("Successfully writing {0} to address {1}", BitConverter.ToString(valueToWrite), scorePtrAddr);
                    }
                    else
                    {
                        Console.WriteLine("Fails wrinting {0} to address {1}", BitConverter.ToString(valueToWrite), scorePtrAddr);
                    }
                }
                #endregion
            }
        }

        private void multiIncr_Click(object sender, EventArgs e)
        {
            int scoreMultiplier = (Convert.ToInt32((multiValue.Text)));

            if (scoreMultiplier < 999)
            {
                multiValue.Text = (scoreMultiplier + 1).ToString();
            }
        }

        private void multiMinus_Click(object sender, EventArgs e)
        {
            int scoreMultiplier = (Convert.ToInt32((multiValue.Text)));

            if (scoreMultiplier > 1)
            {
                multiValue.Text = (scoreMultiplier - 1).ToString();
            }
        }
    }
}
