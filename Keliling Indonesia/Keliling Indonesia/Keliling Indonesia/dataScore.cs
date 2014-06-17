using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keliling_Indonesia
{
    class dataScore
    {
        private String name;
        private int score;

        public dataScore()
        {

        }

        public dataScore(String name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public String getName()
        {
            return this.name;
        }

        public int getScore()
        {
            return this.score;
        }
    }
}
