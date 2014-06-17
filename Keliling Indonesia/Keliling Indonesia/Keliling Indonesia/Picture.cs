using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Keliling_Indonesia
{
    class Picture
    {
        String namePicture;
        String dataPicture;
        private StreamReader reader;

        int heightTile;
        int widthTile;


        public Picture()
        {
            namePicture = null;            
            randomPicture();
        }

        private String readFile(int number)
        {
            String data = null;
            if (reader == null)
            {
                reader = new StreamReader("C:/Users/Ryan/Documents/Visual Studio 2012/Projects/Keliling Indonesia/Keliling Indonesia/Keliling Indonesia/Keliling Indonesia/data_picture.txt");
            }
            String temp;
            Console.WriteLine("resd");
            while ((temp = reader.ReadLine()) != null)
            {
                String[] temp1 = temp.Split(':');
                Console.WriteLine(number.ToString());
                if (temp1[0].Contains(number.ToString()))
                {
                    data = temp;                    
                    break;
                }
            }
            Console.WriteLine("resd");
            reader.Close();
            return data;
        }

        private void randomPicture()
        {
            Random rnd = new Random();
            int numberPicture = rnd.Next(1,5);
           
            String temp = readFile(numberPicture).Substring(3);
            String[] temp1 = temp.Split(':');
            dataPicture = temp1[1];
            namePicture = temp1[0];
            Console.WriteLine(namePicture);
        }

        public String getNamePicture()
        {
            return namePicture;
        }

    }
}
