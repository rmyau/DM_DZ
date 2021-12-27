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
        public static void NextRearrangement(List<string> ReArr)//perestnovka
        {
            int index = ReArr.Count - 2;
            while (alf.IndexOf(ReArr[index]) > alf.IndexOf(ReArr[index + 1]))
                index--;
            int s = index + 1;
            while (s < ReArr.Count - 1 && (alf.IndexOf(ReArr[s + 1]) > alf.IndexOf(ReArr[index])))
                s++;
            string t=ReArr[index];//поменяли местами полученный и наименьший справа
            ReArr[index] = ReArr[s];
            ReArr[s] = t;
            
            for (int i = 0; i < (ReArr.Count - index - 1) / 2; i++)
            {
                t = ReArr[ReArr.Count - 1 - i];
                ReArr[ReArr.Count - 1 - i] = ReArr[index + i + 1];
                ReArr[index + i + 1]=t;
            }

        }
        public static bool hasNextRearrangement(List<string> ReArr)
        {
            int index = ReArr.Count - 1;
            while (index > 0 && (alf.IndexOf(ReArr[index]) < alf.IndexOf(ReArr[index - 1])))
                index--;
            if (index == 0) return false;
            else return true;
        }

        public static void NextArrange(List<string> arr, List<string> ReArr)
        {
            NextRearrangement(ReArr);
            for (int i = 0; i < m - k; i++)
                arr[i] = ReArr[i];
            if (hasNextRearrangement(ReArr))
                NextRearrangement(ReArr); 
        }
        
        public static void Connect(List<int> sochet, List<string> arrange,List<string> word)
        {
            int indexAr = 0;
            for (int i = 0; i < m; i++)
                word[i] = "";

            for (int i = 0; i < k; i++)
                word[sochet[i]] = alf[0];
            for (int i = 0; i < m; i++)
                if (word[i] == "")
                {
                    word[i] = arrange[indexAr];
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
            List<string> perest = new List<string>();
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
                perest.Add(alf[i+1]);
            }
            file1.WriteLine("Все слова длины 5, в которых две буквы а:");
            file2.WriteLine("Все слова длины 5, в которых две буквы а, остальные буквы не повторяются:");
            while (hasNextSochet(sochet))
            {
                NextSochet(sochet);
                for (int i = 0; i < m - k; i++)
                    arrange1[i] = "b";

                Connect(sochet, arrange1, word1);
                Print(word1, file1);
                while (hasNextArrangeRepeat(arrange1))
                {
                    NextArrangeRepeat(arrange1);
                    Connect(sochet, arrange1, word1);
                    Print(word1, file1);
                }

                for (int i = 0; i < m; i++)
                    perest[i] = alf[i + 1];
                NextArrange(arrange2, perest);
                Connect(sochet, arrange2, word2);
                Print(word2, file2);
                while (hasNextRearrangement(perest))
                {
                    NextArrange(arrange2, perest);
                    Connect(sochet, arrange2, word2);
                    Print(word2, file2);
                }
            }

            file1.Close();
            file2.Close();
            //Console.ReadKey();
        }
    }
}

