using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BullsAndCowsWinFormsApp
{
    public partial class MainForms : Form
    {
        string puzzleWord = "";
        const int wordLength = 4;
        int stepCount = 0;
        public MainForms()
        {
            InitializeComponent();
        }

        private void MainForms_Load(object sender, EventArgs e)
        {
            List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random random = new Random();
            for (int i = 0; i < wordLength; i++)
            {
                int digitIndex = random.Next(0, digits.Count);
                puzzleWord += digits[digitIndex].ToString();
                digits.RemoveAt(digitIndex);
            }
            puzzleWordLable.Text = puzzleWord;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userWord = userWordTexBox.Text;

            if (!IsValid(userWord))
            {
                return;
            }
            stepCount++;
            int bullsCount = CalculateBullsCount(userWord);
            int cowsCount = CalculateCowsCount(userWord);

            bullsCountLabel.Text = "Быков = " + bullsCount;
            cowsCountLabel.Text = "Коров = " + cowsCount;

            historyDataGridView.Rows.Add(stepCount, userWord, bullsCount, cowsCount);

            if(bullsCount==wordLength)
            {
                MessageBox.Show($"Ура! Вы победили за {stepCount} шагов");
                button1.Enabled = false;
                userWordTexBox.Enabled = false;
            }
        }

        private bool IsValid(string userWord)
        {
            if (userWord.Length != wordLength)
            {
                MessageBox.Show("Строка должна быть длинной 4!");
                return false;
            }

            for (int i = 0; i < userWord.Length; i++)
            {
                if(!char.IsDigit(userWord[i]))
                {
                    MessageBox.Show("Строка должна содержать только цифры!");
                    return false;
                }
            }
            for (int i = 0; i < userWord.Length; i++)
            {
                for (int j = i + 1; j < userWord.Length; j++)
                {
                    if (userWord[i] == userWord[j])
                    {
                        MessageBox.Show("Строка должна содержать уникальные цифры!");
                        return false;
                    }
                }
            }
            return true;
        }

        private int CalculateBullsCount(string userWord)
        {
            int bullsCount = 0;
            for (int i = 0; i < wordLength; i++)
            {
                if (puzzleWord[i] == userWord[i])
                {
                    bullsCount++;
                }
            }
            return bullsCount;
        }

        private int CalculateCowsCount(string userWord)
        {
            int cowsCount = 0;
            for (int i = 0; i < wordLength; i++)
            {
                for (int j = 0; j < wordLength; j++)
                {
                    if (i == j) continue;
                    if (puzzleWord[i] == userWord[j])
                    {
                        cowsCount++;
                    }

                }
            }
            return cowsCount;
        }

        private void рестартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
