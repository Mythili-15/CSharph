using System;

namespace RockPaperScissor
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("------------------------------------------------------");
                Game game = new Game(
                    new Player(UserInterface.GetPlayerName(), UserInterface.GetPlayerId()),
                    new Player("Computer", 0), // Assuming Player 2 is a computer with a fixed ID
                    new GameRules
                    {
                        NumberOfIterations = 3
                    });

                game.Play();

                Console.WriteLine("Do you want to play another game? (yes/no)");

            } while (Console.ReadLine().Trim().ToLower() == "yes");
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Score { get; set; }

        public Player(string name, int id)
        {
            Name = name;
            Id = id;
            Score = 0;
        }
    }

    public class Game
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        private GameRules Rules { get; }
        public Referee Utilities { get; set; }

        public Game(Player player1, Player player2, GameRules rules)
        {
            Player1 = player1;
            Player2 = player2;
            Rules = rules;
            Utilities = new Referee(rules);
        }

        public void Play()
        {
            Utilities.StartGame();

            for (int i = 0; i < Rules.NumberOfIterations; i++)
            {
                Gesture player1Move = UserInterface.GetPlayer1Move(Player1.Name);
                Gesture player2Move = GetPlayer2Move();

                Console.WriteLine($"{Player1.Name} chose: {player1Move}");
                Console.WriteLine($"{Player2.Name} chose: {player2Move}");

                int result = Utilities.AnnounceWinner(player1Move, player2Move);
                if (result == 1)
                {
                    Player1.Score++;
                    Console.WriteLine($"{Player1.Name} wins this round!");
                }
                else if (result == 2)
                {
                    Player2.Score++;
                    Console.WriteLine($"{Player2.Name} wins this round!");
                }
                else
                {
                    Console.WriteLine("It's a tie for this round!");
                }
            }
            Utilities.StopGame(Player1, Player2);
        }

        public Gesture GetPlayer2Move()
        {
            Random random = new();
            return (Gesture)random.Next(1, 4); // Adjusted range to match Gesture enum values
        }
    }

    public class GameRules
    {
        public int NumberOfIterations { get; set; }
    }

    public enum Gesture
    {
        Rock = 1,
        Paper,
        Scissors
    }

    public class Referee
    {
        private readonly GameRules Rules;

        public Referee(GameRules rules)
        {
            Rules = rules;
        }

        public int AnnounceWinner(Gesture player1, Gesture player2)
        {
            if (player1 == player2) return 0; // Tie

            if ((player1 == Gesture.Rock && player2 == Gesture.Scissors) ||
                (player1 == Gesture.Paper && player2 == Gesture.Rock) ||
                (player1 == Gesture.Scissors && player2 == Gesture.Paper))
            {
                return 1; // Player1 wins
            }

            return 2; // Player2 wins
        }

        public void StartGame()
        {
            Console.WriteLine("Starting the game...\n" +
                $"Number of iterations: {Rules.NumberOfIterations}\n" +
                "Let's play!\n" +
                "-------------------------------------------------------");
        }

        public void StopGame(Player player1, Player player2)
        {
            Console.WriteLine($"{player1.Name} score: {player1.Score}, {player2.Name} score: {player2.Score}");
            Console.WriteLine("Game Over!");
            if (player1.Score > player2.Score)
            {
                Console.WriteLine($"{player1.Name}, ID {player1.Id} is the winner!");
            }
            else if (player2.Score > player1.Score)
            {
                Console.WriteLine($"{player2.Name}, ID {player2.Id} is the winner!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }

    public class UserInterface
    {
        public static string GetPlayerName()
        {
            Console.WriteLine("Enter the player name:");
            return Console.ReadLine();
        }

        public static int GetPlayerId()
        {
            Console.WriteLine("Enter the player ID:");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static Gesture GetPlayer1Move(string player1Name)
        {
            do
            {
                Console.WriteLine($"{player1Name}, enter your move (1 for Rock, 2 for Paper, 3 for Scissors): ");
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out int move) && Enum.IsDefined(typeof(Gesture), move))
                {
                    return (Gesture)move;
                }
                Console.WriteLine("Invalid input. Please enter 1 for Rock, 2 for Paper, or 3 for Scissors.");
            } while (true);
        }
    }
}
