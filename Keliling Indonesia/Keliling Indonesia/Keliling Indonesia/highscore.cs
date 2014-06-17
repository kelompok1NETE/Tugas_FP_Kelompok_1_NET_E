using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Keliling_Indonesia
{
    class highscore
    {
        List<dataScore> listScore;
        private StreamReader reader;
        private StreamWriter writer;

        public highscore()
        {
            listScore = new List<dataScore>();
            reader = null;
            writer = null;
        }

        private void readFile()
        {
         //   if (reader == null)
           // {
                reader = new StreamReader("C:/Users/Ryan/Documents/Visual Studio 2012/Projects/Keliling Indonesia/Keliling Indonesia/Keliling Indonesia/Keliling Indonesia/data-highscore.txt");
            //}
            String temp;
            while((temp=reader.ReadLine())!=null)
            {
                String[] temp1 = temp.Split(':');
                listScore.Add(new dataScore(temp1[0], Int32.Parse(temp1[1])));
            }
            reader.Close();
        }

        private void writeFile(String data)
        {
            if (writer == null)
            {
                writer = new StreamWriter("C:/Users/Ryan/Documents/Visual Studio 2012/Projects/Keliling Indonesia/Keliling Indonesia/Keliling Indonesia/Keliling Indonesia/data-highscore.txt", true);
            }
            writer.WriteLine(data);
            writer.Close();
        }

        public List<dataScore> getHighScore()
        {
            List<dataScore> dataHighScore = null;
            readFile();
            if (listScore != null)
            {
                dataHighScore = new List<dataScore>();

                var data = listScore.OrderBy (score => score.getScore()) ;
                foreach(var temp in data)
                {
                    dataHighScore.Add(temp);
                }
            }
            return dataHighScore;
        }

        public void saveData(String name, int score)
        {
            writeFile(name + ":" +score);
        }
    }
}
