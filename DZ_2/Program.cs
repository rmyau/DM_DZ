using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_2
{
    class Program
    {
        public static int m = 5;
        public static int k = 2;
        static bool hasNextSochet(List<int> s)
        {
            for (int i = s.Count - 1; i > 0; i--)
                if (s[i] != s[i - 1] + 1) return true;
            if (s[s.Count - 1] == m - 1) return false;
            return true;
        }

        static void NextSochet(List<int> s)
        {
            if (s[k - 1] != (m - 1)) s[k - 1] += 1;
            else
            {
                int index = k - 1;
                while (s[index] == s[index - 1] + 1 && index > 0)
                    index--;
                s[index - 1]++;
                for (int i = index + 1; i < k; i++)
                    s[i] = s[i - 1] + 1;
            }

        }
        static bool hasNextArrangeRepeat(List<string> s)
        {
            for (int i = m - k - 1; i >= 0; i--)
                if (s[i] != alf[alf.Count - 1]) return true;
            return false;
        }
        static void NextArrangeRepeat(List<string> s)
        {
            int index = m - k - 1;
            while (s[index] == alf[alf.Count - 1] && index > 0)
            {
                s[index] = alf[1];
                index--;
            }
            int ind_in_alf = alf.IndexOf(s[index]);
            s[index] = alf[ind_in_alf + 1];

        }

        //static bool hasNextArrange(List<string> arr)
        //{
        //    if (arr[m-k-1]==alf[m-1])

        //        for (int i=m-k-1;i>=0;i--)
        //            if ()
        //}
        public static void Connect(List<int> sochet, List<string> arrange)
        {
            int indexAr = 0;
            for (int i = 0; i < m; i++)
                word1[i] = "";

            for (int i = 0; i < k; i++)
                word1[sochet[i]] = alf[0];
            for (int i = 0; i < m; i++)
                if (word1[i] == "")
                {
                    word1[i] = arrange[indexAr];
                    indexAr++;
                }
        }

        public static void Print(List<string> slovo, StreamWriter file)
        {
            string s = "";
            for (int i = 0; i < m; i++)
                s += slovo[i];
            file.WriteLine(s);
            //Console.WriteLine(s);

        }
        public static List<string> alf = new List<string>();
        public static List<string> word1 = new List<string>();
        public static List<string> word2 = new List<string>();
        public static StreamWriter file1 = new StreamWriter(@"otvet1.txt");//для размещений с повторениями
        public static StreamWriter file2 = new StreamWriter(@"otvet2.txt");
        static void Main(string[] args)
        {
            alf.Add("a");
            alf.Add("b");
            alf.Add("c");
            alf.Add("d");
            alf.Add("e");
            alf.Add("f");

            List<string> arrange1 = new List<string>();
            List<string> arrange2 = new List<string>();
            for (int i = 0; i < m - k; i++)
            {
                arrange1.Add("");
                arrange2.Add("");
            }

            List<int> sochet = new List<int>();
            for (int i = 0; i < k; i++)
                sochet.Add(i);
            sochet[k - 1] = k - 2;
            for (int i = 0; i < m; i++)
            {
                word1.Add("");
                word2.Add("");
            }

            while (hasNextSochet(sochet))
            {
                NextSochet(sochet);
                for (int i = 0; i < m - k; i++)
                    arrange1[i] = "b";

                Connect(sochet, arrange1);
                Print(word1, file1);
                while (hasNextArrangeRepeat(arrange1))
                {
                    NextArrangeRepeat(arrange1);
                    Connect(sochet, arrange1);
                    Print(word1, file1);
                }


            }

            file1.Close();
            file2.Close();
            //Console.ReadKey();
        }
    }
}

