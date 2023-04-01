using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    internal class People
    {
        public int x = 0;
        public int y = 0;

        public string Username = " ";

        public People(int x, int y, string Username)
        {
            this.x = x;
            this.y = y;
            this.Username = Username;
        }

        public People(byte[] body)
        {
            string message = Encoding.UTF8.GetString(body);
            string[] words = message.Split(' ');
            Username = words[0];
            x = int.Parse(words[1]);
            y = int.Parse(words[2]);
        }


        public static byte[] SendLocation(int x, int y, string Username)
        {
            string message = $"{Username} {x} {y}";
            byte[] encoded_message = Encoding.UTF8.GetBytes(message);
            return encoded_message;
        }

        public static byte[] PersonMove(string Move, string Username)
        {
            string message = Username + " " + Move;
            byte[] encoded_message = Encoding.UTF8.GetBytes(message);
            return encoded_message;
        }


        public void PersonMove(string Move)
        {
            switch (Move)
            {
                case "UP":
                    y++;
                    break;

                case "DOWN":
                    y--;
                    break;
                case "LEFT":
                    x--;
                    break;

                case "RIGHT":
                    x++;
                    break;
            }

        }



    }
}

