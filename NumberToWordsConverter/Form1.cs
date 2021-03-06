﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberToWordsConverter
{
    public partial class Form1 : Form
    {
        private string[] numbers = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private string[] tens = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            string input = textBox1.Text;
            int numberToConvert;

            if (int.TryParse(input, out numberToConvert))
           {
                if (numberToConvert < 0 || numberToConvert > 1000000)
                {
                    textBox2.Text = "Invalid range, please enter a number between 0 and 1 million inclusively";
                }
                else
                {
                    string stringData = this.convertToString(numberToConvert);
                    textBox2.Text = stringData;
                }
            }
            else
            {
                textBox2.Text = "Please enter a valid number";
            }
        }
        public string convertToString(int number)
        {
            char[] digits = number.ToString().ToCharArray();
            string words = null;

            if (number >= 0 && number <= 19)
            {
                words = words + this.numbers[number];
            }
            else if (number >= 20 && number <= 99)
            {
                int firstDigit = (int)Char.GetNumericValue(digits[0]);
                int secondPart = number % 10;

                words = words + this.tens[firstDigit];

                if (secondPart > 0)
                {
                    words = words + " " + convertToString(secondPart);
                }
            }
            else if (number >= 100 && number <= 999)
            {
                int firstDigit = (int)Char.GetNumericValue(digits[0]);
                int secondPart = number % 100;

                words = words + this.numbers[firstDigit] + " hundred";

                if (secondPart > 0)
                {
                    words = words + " and " + convertToString(secondPart);
                }
            }
            else if (number >= 1000 && number <= 19999)
            {
                int firstPart = (int)Char.GetNumericValue(digits[0]);
                if (number >= 10000)
                {
                    string twoDigits = digits[0].ToString() + digits[1].ToString();
                    firstPart = Convert.ToInt16(twoDigits);
                }
                int secondPart = number % 1000;

                words = words + this.numbers[firstPart] + " thousand";

                if (secondPart > 0 && secondPart <= 99)
                {
                    words = words + " and " + convertToString(secondPart);
                }
                else if (secondPart >= 100)
                {
                    words = words + " " + convertToString(secondPart);
                }
            }
            else if (number >= 20000 && number <= 999999)
            {
                string firstStringPart = Char.GetNumericValue(digits[0]).ToString() + Char.GetNumericValue(digits[1]).ToString();

                if (number >= 100000)
                {
                    firstStringPart = firstStringPart + Char.GetNumericValue(digits[2]).ToString();
                }

                int firstPart = Convert.ToInt16(firstStringPart);
                int secondPart = number - (firstPart * 1000);

                words = words + convertToString(firstPart) + " thousand";

                if (secondPart > 0 && secondPart <= 99)
                {
                    words = words + " and " + convertToString(secondPart);
                }
                else if (secondPart >= 100)
                {
                    words = words + " " + convertToString(secondPart);
                }

            }
            else if (number == 1000000)
            {
                words = words + "One million";
            }

            return words;
        }
    }
    
}
