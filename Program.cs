using RabbitMQ.Client;
using System.Numerics;

namespace Person
{
    //randomise position
    internal class Program
    {

        static void Main(string[] args)
        {
            int ActionSpeed = 1000; //1000 is equal to a second
            
            //Asking For User ID
            Console.WriteLine("Whats Your Name?: ");
            string User_Id = Console.ReadLine();

            //Starting Position Creation
            Vector2 startPos = CreateStartPosition();
            byte[] byte_startLocation = People.SendLocation((int)startPos.X, (int)startPos.Y, User_Id);

            //RabbitMq Things
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "connect",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


            channel.BasicPublish(exchange: "Positions",
                     routingKey: "connect",
                     basicProperties: null,
                     body: byte_startLocation);

            //moving - movement
            //connecting (starting pos) - connect 

            //initial delay
            Thread.Sleep(500);

            bool Continue = true;            
            while (Continue) {
                
                var dir = People.PersonMove(MoveDirection(), User_Id);
                channel.BasicPublish(exchange: "Positions",
                     routingKey: "movement",
                     basicProperties: null,
                     body: dir);

                Thread.Sleep(ActionSpeed);
            }

        }


        static string MoveDirection()
        {           
                Random rand = new Random();

                //The four different directions
                int RandomNumber = rand.Next(4);

                string Direction = "";

                switch (RandomNumber)
                {
                    case 0:
                        Direction = "UP";
                        break;

                    case 1:
                        Direction = "DOWN";
                        break;

                    case 2:
                        Direction = "LEFT";
                        break;

                    case 3:
                        Direction = "RIGHT";
                        break;
                }

                return Direction;
                
            
            //Send Message to Position Channel (RBITMQ CHannel)
        }

        static Vector2 CreateStartPosition()
        {
            Random rand = new Random(); 
            //Maximum area is 10 but can be changed to larger number
            int max_direction = 9;

            float x = rand.Next(max_direction);
            float y = rand.Next(max_direction);

            return new Vector2(x,y);
        }
    }
}