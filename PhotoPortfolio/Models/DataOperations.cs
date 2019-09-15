using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoPortfolio.Models
{
    public static class DataOperations
    {
        //return a start of element in csv string
        public static int[] GetPointerPosition(string value,int index)
        {
            int[] pointers = { 0, 0 };
            for (int i = 0; i < index; i++)
            {
                while (value[pointers[0]] != ',') pointers[0]++;
                pointers[0]++;

            }
            while (pointers[1] + pointers[0] < (value.Length-1) && value[pointers[0]+pointers[1]] != ',') pointers[1]++;
            pointers[1]++;
            return pointers;
            
        }
        
        public static string DeleteElementFromCsvString(string value,int index)
        {
            int[] word = GetPointerPosition(value,index);
            //if (word[0] + word[1] == value.Length) word[1]--;
            return value.Remove(word[0], word[1]);
        }
    }
}
