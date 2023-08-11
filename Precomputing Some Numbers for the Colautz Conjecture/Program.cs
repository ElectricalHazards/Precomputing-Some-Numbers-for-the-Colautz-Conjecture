using System.Text;

namespace Precomputing_Some_Numbers_for_the_Colautz_Conjecture {
    internal class Program {
        static Dictionary<ulong, List<ulong>> list = new Dictionary<ulong,List<ulong>>();
        static ulong maxInt = (ulong.MaxValue - 1)/3;
        static void Main(string[] args) {
            int calculations = 0;
            ulong maxItterations = 842673;
            Console.WriteLine("Hello, World!");
            for(ulong i = 1; i<maxItterations; i++) {
                if (list.ContainsKey(i)) {
                    continue;
                }
                calculations++;
                Calculate(i);
                Backpropigate(i);
                if(i % 123 == 0) {
                    Console.WriteLine($"Calculating: {i}/{maxItterations}");
                }
                
            }
            List<ulong> kezz = new List<ulong>();
            foreach(ulong i in list.Keys) {
                kezz.Add(i);
            }
            kezz.Sort();
            if(File.Exists("out.txt")) {
                File.Delete("out.txt");
            }
            using(FileStream f = File.Create("out.txt")) {
                f.Close();
            }
            using(FileStream f = new FileStream("out.txt",FileMode.Append)) {
                using(StreamWriter sw = new StreamWriter(f)) {
                    foreach(ulong i in kezz) {
                        string line = "";
                        //Console.Write(i + ": ");
                        line += i + ": ";
                        foreach(ulong l in list[i]) {
                            //  Console.Write(l + " ");
                            line += l + " ";
                        }
                        if(kezz.IndexOf(i) % 123 == 0) {
                            Console.WriteLine($"Writing: {i}/{kezz.Count()}");
                        }
                        //Console.WriteLine();
                        line += "\n";
                        sw.Write(line);
                    }
                }
                f.Close();
            }
            Console.WriteLine($"{calculations} calculations preformed. {list.Keys.Count} Entries created");
        }

        static void Backpropigate(ulong i) {
            List<ulong> Itterate = list[i];
            for(int x = 0; x < Itterate.Count; x++) {
                if(list.ContainsKey(Itterate[x])) {
                    continue;
                }
                list[Itterate[x]] = new List<ulong>();
                list[Itterate[x]].AddRange(Itterate.GetRange(x+1 , Itterate.Count - (x+1)));

            }
        }

        static void Calculate(ulong i) {
            list[i] = new List<ulong>();
            if(i % 2 == 0) {
                list[i].Add(i / 2);
            } else {
                list[i].Add((3*i)+1);
            }
            while(list[i].Last() != 1) {
                if(list[i].Last() >= maxInt) {
                    list[i] = new List<ulong> { 0 };
                    return;
                }
                if(list.ContainsKey(list[i].Last())) {
                    if(list[list[i].Last()].Last() == 0) {
                        list[i] = new List<ulong> { 0 };
                        return;
                    }
                    list[i].AddRange(list[list[i].Last()]);
                    break;
                }
                if(list[i].Last() % 2 == 0) {
                    list[i].Add(list[i].Last() / 2);
                } else {
                    list[i].Add((3 * list[i].Last()) + 1);
                }
            }
        }
    }
}