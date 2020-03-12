using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Road_Syste_Smart_Cars
{
    public class graph1
    {
        double Dis;
        double[] Dis_arr_fib;
        LinkedList<Tuple<KeyValuePair<int , char>, double>>[] adj_List;// < <node , direction> , distance>
        List<int> path = new List<int>();
        List<char> directions = new List<char>();
        int[] arr_node;
        char[] arr_directions;
        double[] dis;
        public graph1()
        {
        }
        /// <summary>
        /// O(E)
        /// Function To Construct Graph by adding each vertex ,its neghbours and weight<time,dist> between them
        /// </summary>
        public void Add_Adj(read_Write_files rf)
        {
            adj_List = new LinkedList<Tuple<KeyValuePair<int, char>, double>>[rf.num_of_vertices + 1];
            for (int i = 1; i <= rf.num_of_vertices; i++)
            {
                adj_List[i] = new LinkedList<Tuple<KeyValuePair<int, char>, double>>();
            }
            for (int i = 0; i < rf.edges; i++)
            {
                int first_Key = rf.graph.ElementAt(Convert.ToInt32(i)).Item1.Key;
                int second_Key = rf.graph.ElementAt(Convert.ToInt32(i)).Item1.Value;
                Dis = rf.graph.ElementAt(Convert.ToInt32(i)).Item2.Value;
                char direct = rf.graph.ElementAt(Convert.ToChar(i)).Item2.Key;
                adj_List[first_Key].AddLast(new Tuple<KeyValuePair<int, char>, double>(new KeyValuePair<int, char>(second_Key, direct), Dis));
            }
        }
        /// <summary>
        /// calculate The shortest time to move from the source to the destination.
        /// O(E+Vlog(V))
        /// </summary>
        /// <param name="rf"></param>
        /// <param name="File_name"></param>
        /// <returns></minimum time ,total distance , walking distance and vechicel distance>

        public string Dijkstra(read_Write_files rf, string File_name, int src,  int dest)
        {
            arr_node = new int[rf.num_of_vertices+1];
            arr_directions = new char[rf.num_of_vertices + 1];
            path = new List<int>();
            directions = new List<char>();
            for (int i = 1; i < arr_node.Length; i++)
            {
                arr_node[i] = -1;
                arr_directions[i] = ' ';
            }
            Dis_arr_fib = new double[rf.num_of_vertices + 1];
            int vert;
            char dir;
            FibonacciHeap<KeyValuePair<int, char>, double> fb = new FibonacciHeap<KeyValuePair<int, char>, double>(0);

            for (long i = 1; i <= rf.num_of_vertices; i++)
            {
                if (i == src)
                {
                    Dis_arr_fib[i] = 0 ;
                }
                else
                {
                    Dis_arr_fib[i] = double.MaxValue;
                }
            }
            FibonacciHeapNode<KeyValuePair<int, char>, double> x = new FibonacciHeapNode<KeyValuePair<int, char>, double>(new KeyValuePair<int, char>(src, ' '), 0);
            fb.Insert(x);
            FibonacciHeapNode<KeyValuePair<int, char>, double> u = new FibonacciHeapNode<KeyValuePair<int, char>, double>(new KeyValuePair<int , char>(0 , ' '), 0);
            bool flag = false;
            dis = new double[rf.num_of_vertices + 1];
            while (!fb.IsEmpty())
            {
                u = fb.Min();
                vert = u.Data.Key;
                dir = u.Data.Value;
                foreach (Tuple<KeyValuePair<int,char>, double> nep in adj_List[Convert.ToInt32(vert)])
                {
                    FibonacciHeapNode<KeyValuePair<int ,char>, double> vertex = new FibonacciHeapNode<KeyValuePair<int ,char>, double>(new KeyValuePair<int ,char>(0 , ' '), 0);
                    if (Dis_arr_fib[vert] + nep.Item2 < Dis_arr_fib[nep.Item1.Key])
                    {
                        Dis_arr_fib[nep.Item1.Key] = Dis_arr_fib[vert] + nep.Item2;
                        vertex.Key = Dis_arr_fib[nep.Item1.Key];
                        vertex.Data = new KeyValuePair<int,char>(nep.Item1.Key, nep.Item1.Value);
                        //O(1)
                        arr_node[nep.Item1.Key] = vert;
                        dis[nep.Item1.Key] = nep.Item2;
                        arr_directions[vert] = dir;
                        fb.Insert(vertex);
                    }
                }
                if (u.Data.Key == dest)
                {
                    flag = true;
                }
                if (flag)
                {
                    arr_directions[vert] = dir;
                    break;
                }
                //O(log(v))
                fb.RemoveMin();
            }

            //O(v)
           int k = dest;
           path.Add(dest);
            List<double> path_dis = new List<double>();
            path_dis.Add(dis[k]);
            do
            {
                int node = arr_node[k];
                double dista = dis[node];
                path_dis.Add(dista);
                path.Add(node);
                k = node;
            } while (k != src);
            for (int i = 0; i < path.Count; i++)
            {
                char dire = arr_directions[path[i]];
                directions.Add(dire);

            }
            Console.WriteLine();
            string final_path = rf.Write_output(File_name, Dis_arr_fib[dest], path_dis , directions);
            return final_path;
        }
    }
}
