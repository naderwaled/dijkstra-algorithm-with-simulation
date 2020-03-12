using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Road_Syste_Smart_Cars
{
    public class read_Write_files
    {
        static string line;
        public int edges;
        public int num_of_queries;
        public int num_of_vertices;
        public int[] ver;
        public int[] source;
        public int[] dest;
        public List<Tuple<KeyValuePair<int, int>, KeyValuePair<char,double>>> graph; //<<node1 , node2>,<distance  ,direction>>
        /// <summary>
        /// read from map file and store intersections in (coordinate) dictionary and roads between each intersection and store them in (graph) dictionary
        /// </summary>
        /// <param name="FileName"></param>
        public void read_ver_edg(string FileName)
        {
            double distance;
            int node1, node2;
            char direction;
            StreamReader sr = new StreamReader(FileName);
            num_of_vertices = int.Parse(sr.ReadLine());
            ver = new int[num_of_vertices+1];
            for(int i=1; i<=num_of_vertices; i++)
            {
                ver[i] = int.Parse(sr.ReadLine());
            }
            edges = int.Parse(sr.ReadLine());
            graph = new List<Tuple<KeyValuePair<int, int>, KeyValuePair<char,double>>>();
            for (int i = 0; i < edges; i++)
            {
                line = sr.ReadLine();
                string[] data = line.Split(' ');
                node1 = int.Parse(data[0]);
                node2 = int.Parse(data[1]);
                distance = double.Parse(data[2]);
                direction = char.Parse(data[3]);
                graph.Add(new Tuple<KeyValuePair<int, int>, KeyValuePair<char,double>>(new KeyValuePair<int, int>(node1, node2), new KeyValuePair<char, double>(direction,distance)));
            }
            sr.Close();
        }
        /// <summary>
        /// read from queries file and store source and destination in array of pair for sources & destinations and double array for R of each query and it takes file name of query.
        /// Ɵ(Q)
        /// </summary>
        /// <param name="FileName"></query file name>
        public void read_queries(string FileName)
        {
            StreamReader qsr = new StreamReader(FileName);
            num_of_queries = int.Parse(qsr.ReadLine());
            source = new int[num_of_queries];
            dest = new int[num_of_queries];
            for (int i = 0; i < num_of_queries; i++)
            {
                line = qsr.ReadLine();
                string[] data = line.Split(' ');
                source[i] = int.Parse(data[0]);
                dest[i] = int.Parse(data[1]);
            }
            qsr.Close();

        }
        /// <summary>
        /// write final output in output file takes it as a parameter .
        /// O(1)
        /// </summary>
        /// <param name="FileName"></output file name>
        /// <param name="Time"></minimum time returns from dijkstra>
        /// <param name="Distance"></total distance returns from dijkstra>
        /// <param name="walk"></walking distance returns from dijkstra>
        /// <param name="vechicle"></vechicel distance returns from dijkstra>
        public string Write_output(string FileName, double total_dist , List<double> pathe , List<char> directions)
        {
            FileStream Fswrite = new FileStream(FileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(Fswrite);
            
            for (int i = pathe.Count-2; i >= 0; i--)
            {
                sw.Write(directions[i].ToString());
                sw.Write(" ");
                sw.Write(pathe[i].ToString());
                if (i == 0)
                {
                    break;
                }
                sw.Write(" ");
            }
            sw.WriteLine();
            sw.Close();
            Fswrite.Close();
            FileStream Fsread = new FileStream(FileName, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(Fsread);
            string final_path = sr.ReadLine();
            sr.Close();
            Fsread.Close();
            return final_path;
           
        }
    }
}
