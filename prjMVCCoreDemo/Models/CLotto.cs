using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMauiDemo.Models
{
    public class CLotto
    {
        public string getNumber()
        {

            int[] numbers = new int[6];

            Random rnd = new Random();

            int count = 0;
            
            while (count < 6)
            {
                int temp = rnd.Next(1,50); 

                if (!is在陣列中已有此隨機數(temp,numbers))
                {
                    numbers[count] = temp;
                    count++;
                }
                   
            }

            for (int i = numbers.Length-1; i >0; i--)
            {
                for (int j = 0; j < i ; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int big = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j+1] = big;
                    }
                }
            }

            string result = string.Empty;

            foreach(int i in numbers)
            {
                result += i+" ";
            }

            return result;
        }

        private bool is在陣列中已有此隨機數(int temp, int[] numbers)
        {
            foreach(int i in numbers)
            {
                if(temp == i)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
