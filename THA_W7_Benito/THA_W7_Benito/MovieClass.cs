using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THA_W7_Benito
{
    internal class MovieClass
    {
        string name;
        string path;
        string[] timeArr;
        //Dictionary<int, bool> statusDict = new Dictionary<int, bool>();
        Dictionary<string, Dictionary<int, bool>> chairDict = new Dictionary<string, Dictionary<int, bool>>();

      

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string[] TimeArray { get => timeArr; set => timeArr = value; }
        public Dictionary<string, Dictionary<int, bool>> ChairDict { get => chairDict; set => chairDict = value; }

        public MovieClass(string name, string path, string[] timeArr)
        {
            this.name = name;
            this.path = path;
            this.timeArr = timeArr;
        }

        public void generateRandom()
        {
            Random random = new Random();
            foreach (string time in this.timeArr)
            {
                Dictionary<int, bool> statusDict = new Dictionary<int, bool>();

                int counter = 0;
                for (int i = 0; i < 100; i++)
                {
                    bool is_booked = false;
                    int booked = random.Next(100);
                    if (booked < 50 && counter < 70)
                    {
                        is_booked = true;
                        counter++;
                    }
                    statusDict[i] = is_booked; //mengisi no kursi dengan true/false.
                }
                ChairDict[time] = statusDict; //simpan ke dict kursi.
            }
        }

        public void reset(string time)
        {
            ChairDict[time].Clear();
            Dictionary<int, bool> statusDict = new Dictionary<int, bool>();
            for (int j = 0; j < 100; j++)
            {
                statusDict[j] = false; //mengisi no kursi dengan false;
            }
            ChairDict[time] = statusDict; //simpan ke dict kursi.
        }
    }
}
