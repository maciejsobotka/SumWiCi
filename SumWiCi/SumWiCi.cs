using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumWiCi
{
    class SumWiCi
    {
        private int n;
        private Task[] Tasks;
        public SumWiCi(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                n = Convert.ToInt32(sr.ReadLine());
                Tasks = new Task[n];
                for(int i=0; i<n; ++i)
                {
                    string line = sr.ReadLine();
                    string[] task = line.Split(' ');
                    Tasks[i] = new Task(Convert.ToInt32(task[0]), Convert.ToInt32(task[1]));
                }
            }
        }
        public int GetSumWiCi()
        {
            int tabSize = 1 << n;
            int[] tab = new int[tabSize];
            int[] tabC = new int[tabSize];

            tab[0] = 0;
            tabC[0] = 0;

            for (int i = 1; i < tabSize; ++i)
            {
                tab[i] = 999999999;
                for (int akc_ts = 0; akc_ts < n; ++akc_ts)
                {
                    int mask = 1 << akc_ts;

                    if ((mask & i) == 0) continue;

                    int sub_rozw = ~mask & i;
                    int tmpC = tabC[sub_rozw] + Tasks[akc_ts].p; // Cmax
                    int tmp = tab[sub_rozw] + Tasks[akc_ts].w * tmpC;
                    //	tab[i]=min(tab[i],tmp);
                    if (tab[i] > tmp)
                    {
                        tab[i] = tmp;
                        tabC[i] = tmpC;
                    }
                }
            }
            return tab[tabSize - 1];
        }
    }
}
