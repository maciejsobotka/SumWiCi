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
        private int n;          // ilość zadań
        private int c;          // czas wykonania wszystkich zadań
        private int[] tab;      // tablica z podzadaniami
        private string[] perm;     // tablica z permutacją
        private Task[] Tasks;   // tablica z zadaniami
        public SumWiCi(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                n = Convert.ToInt32(sr.ReadLine());
                c = 0;
                Tasks = new Task[n];
                for(int i=0; i<n; ++i)
                {
                    string line = sr.ReadLine();
                    string[] task = line.Split(' ');
                    Tasks[i] = new Task(Convert.ToInt32(task[0]), Convert.ToInt32(task[1]));
                    c += Convert.ToInt32(task[0]);
                }
            }
        }
        public int GetSumWiCi()
        {
            int tabSize = 1 << n;
            int[] tabC = new int[tabSize];
            tab = new int[tabSize];
            perm = new string[tabSize];

            tab[0] = 0;  // początkowa suma WiCi
            tabC[0] = 0;  // czas początkowy 0

            for (int i = 1; i < tabSize; ++i)
            {
                tab[i] = 999999999;
                for (int actTsk = 0; actTsk < n; ++actTsk)
                {
                    int mask = 1 << actTsk; // maska zadania

                    if ((mask & i) == 0) continue;  // sprawdzamy tylko zadania zawierające się w badanym przypadku i

                    int subSolution = ~mask & i;  // bierzemy optymalne podzadanie
                    int tmpC = tabC[subSolution] + Tasks[actTsk].p;  // C czas, po któym wykona sie zadanie
                    int tmp = tab[subSolution] + Tasks[actTsk].w * tmpC;  // W*C dla danego przypadku
                    if (tab[i] > tmp)  // lepsze rozwiązanie
                    {
                        tab[i] = tmp;
                        tabC[i] = tmpC;
                        perm[i] = perm[subSolution] + (actTsk + 1).ToString() + ' ';  // dokładamy na koniec
                    }
                }
            }
            return tab[tabSize - 1];
        }

        public int GetSumWiCiFromBehind()
        {
            int tabSize = 1 << n;
            int[] tabC = new int[tabSize];
            tab = new int[tabSize];
            perm = new string[tabSize];

            tab[0] = 0;  // początkowa suma WiCi
            tabC[0] = c;  // czas wykonania wszystich zadań

            for (int i = 1; i < tabSize; ++i)
            {
                tab[i] = 999999999;
                for (int actTsk = 0; actTsk < n; ++actTsk)
                {
                    int mask = 1 << actTsk; // maska zadania

                    if ((mask & i) == 0) continue;  // sprawdzamy tylko zadania zawierające się w badanym przypadku i

                    int subSolution = ~mask & i;  // bierzemy optymalne podzadanie (będziemy dokładać przed optymalną część)
                    int tmpC = tabC[subSolution] - Tasks[actTsk].p; // C czas, w którym wykonały się wcześńiejsze zadania
                    int tmp = tab[subSolution] + Tasks[actTsk].w * tabC[subSolution];  // W*C dla danego przypadku
                    if (tab[i] > tmp)  // lepsze rozwiązanie
                    {
                        tab[i] = tmp;
                        tabC[i] = tmpC;
                        perm[i] = (actTsk + 1).ToString() + ' ' + perm[subSolution];  // dokładamy na początek
                    }
                }
            }
            return tab[tabSize - 1];
        }

        public string TasksToString()
        {
            string tasksToString = "Tasks:\n\n";
            for (int i = 0; i < Tasks.Length; ++i)
                tasksToString += (i + 1).ToString() + "    " + Tasks[i].p + "    " + Tasks[i].w + '\n';
            return tasksToString;
        }

        public string TabToString()
        {
            string tabToString = "";
            foreach (int i in tab)
                tabToString += i.ToString() + ' ';
            return tabToString;
        }

        public string PermToString()
        {
            int x = 1 << n;
            return perm[x - 1];
        }
    }
}
