using System.ComponentModel;

namespace PE2GameFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             *  Name: Trouble PE 2
             *  Created by: Bora Zihnali
             *  Purpose: Creating framework for the Trouble Board Game in C#
             *  Repo Link: https://kgcoe-git.rit.edu/bz5167/igme-105-pes
             *  
             *  Global Variables:
             *      Maximum Players (CONSTANT)
             *      Human Player Count: 0 - Maximum Players
             *      AI Player Count: 0 - Maximum Players
             *          * Check if sum of human and AI player counts is less than or equal to maximum players
             *      Maximum Piece Count (CONSTANT)
             *      The Board: Object of class Board
             * 
             *  Classes:
             *      Player Class
             *          Player Name
             *          Player Type
             *              * Human or AI
             *          Piece Color
             *          Number of Pieces
             *          Place in Turn Order
             *              * Used to determine starting position
             *          Starting Position
             *          Distance from Starting Position
             *          Current Power-up
             *          Current Power-up Duration
             *          
             *          Player Class Methods:
             *              Move Piece Forward
             *                  * Needs to check if piece has circumlocated board
             *                  * Needs to check if piece has landed on another piece at end of movement
             *              Move Piece Backward
             *                  * Needs to check if piece has circumlocated board
             *                  * Needs to check if piece has landed on another piece at end of movement
             *              Set Position
             *                  * Needs to check if position is valid
             *                  * Needs to check if piece has landed on another piece at end of movement
             *          
             *      Board Class
             *          Number of Dice
             *          Dice Side Amount
             *          Spaces Between Starting Points
             *          Dictionary of Spaces
             *              * for custom spaces
             *          
             *          Board Methods
             *              * Generate Dice Roll
             *                  - Dependent on Dice Side amount and Dice amount
             *      
             *      Space Class
             *          Space Location
             *          Power-up on Space
             *              * black hole, warp portal, etc.
             *              
             *      Power-up Class
             *          Power-up Name
             *          Player Effect Object
             *          Board Effect Object
             *      
             *      Player Effect Class
             *          Power-up Type
             *              * knockout immunity, extra spaces per turn, etc.
             *          Power-up Duration
             *              * in turns or rounds
             *          Power-up Override
             *              * determines if this power-up overrides a previous power-up
             *              
             *      Board Effect Class
             *          Power-up Type
             *              * dice side change, dice amount change, etc.
             *          Power-up Duration
             *              * in turns or rounds
             *          Power-up Override
             *              * determines if this power-up overrides a previous power-up
             *      
             *  Methods:
             *      - Display Game Information
             *      - Start Game
             *          - Rounds (until someone has won)
             *              - Turn
             *                  - Player Rolls Dice
             *                      - if not 6
             *                          - If Human, chooses which piece to move and moves it
             *                          - If AI, moves randomly unless there is a piece that is not its own it can knockout with its roll, 
             *                            in which case, it moves the first piece that fits that condition.
             *                      - if 6
             *                          - If Human, chooses which piece to move
             *                          - If AI, if a piece is in start and can be moved out, will move it out.
             *                            Will otherwise follow "not 6" rules.
             *                          - Player rolls again.
             *                  - Player Moves Piece
             *                      - If landed on a piece without knockout immunity, send piece back to its start.
             *                      - If landed on a piece with knockout immunity, send self back to its start.
             *                      - If landed on powerup space, resolve powerup
             *                  - Detect if player has won; if so, end game
             *                  - Decrement player and board power-up turn count if available
             *       - Display End Game Information
             * 
             */

            string test = "God help me dude";
            Console.WriteLine((test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ") + 1) + 1) + 1)) != -1 ? test.Substring((test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ") + 1) + 1) + 1), (test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ") + 1) + 1) + 1) + 1) - (test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ") + 1) + 1) + 1)) : test.Substring(test.IndexOf(" ", test.IndexOf(" ", test.IndexOf(" ") + 1) + 1) + 1));

            const int MINIMUM_PLAYERS = 2;
            const int MINIMUM_HUMAN_PLAYERS = 0;
            const int MAXIMUM_PLAYERS = 4;
            const int MINIMUM_PIECES = 1;
            const int MAXIMUM_PIECES = 4;

            int totalPlayers;
            int humanPlayers;
            int aiPlayers;
            Board board;

            List<Player> players = new List<Player>();

            void getUserData(List<Player> dataLocation)
            {
                totalPlayers = Int32.Parse(AcceptUserInput("How many players will there be, human or otherwise? ", typeof(int), bounds: new double[] { (double)MINIMUM_PLAYERS, (double)MAXIMUM_PLAYERS }));
                humanPlayers = Int32.Parse(AcceptUserInput("How many HUMAN players will there be? ", typeof(int), bounds: new double[] { (double)MINIMUM_HUMAN_PLAYERS, (double)totalPlayers }));
                aiPlayers = totalPlayers - humanPlayers;
                int playersInitialized = 0;

                for (int i = 0; i < humanPlayers; i++)
                {
                    dataLocation.Add(new Player(playerName: AcceptUserInput($"What is Human Player {i + 1}\'s name?", typeof(string), minLength: 1),
                                                       playerColor: AcceptUserInput($"What is Human Player {i + 1}\'s color?", typeof(string), minLength: 1),
                                                       pieceCount: 4,
                                                       isHuman: true,
                                                       startingPosition: (totalPlayers == 2 && playersInitialized == 1) ? 14 : playersInitialized * 7));
                    playersInitialized += 1;
                }

                for (int i = 0; i < aiPlayers; i++)
                {
                    dataLocation.Add(new Player(playerName: $"Lord Cornelius the {i + 1}st",
                                                       playerColor: "a really ugly shade of beige",
                                                       pieceCount: 4,
                                                       isHuman: false,
                                                       startingPosition: (totalPlayers == 2 && playersInitialized == 1) ? 14 : playersInitialized * 7));
                    playersInitialized += 1;
                }
            }

            void gameIntro(List<Player> dataLocation)
            {
                Console.WriteLine("");
                Console.WriteLine("---------------- Trouble: XTREME Edition Deluxe & Knuckles (feat. Dante from Devil May Cry) ----------------\n\n");
                Console.WriteLine("Rules 4 Noobs:\n================");
                Console.WriteLine("This game ain't for CHUMPS! This is the XTREME Trouble XPerience (patent pending)! On your turn, roll the SUPER RADICAL DICE OF DOOOOOOM(tm). " +
                                  "If you roll the maximum value on your dice, you GET TO MOVE A PIECE OUT OF YOUR HOME BASE (THAT\'S PRETTY BASED). Land on another player to " +
                                  "TOTALLY OBLITERATE THEM AND SEND THEM BACK TO THEIR HOME BASE (THAT\'S ALSO BASED)!!! Grab items around the board to THWART THE COMPETITION! " +
                                  "Get your pieces to the end to get that WINNER WINNER VICTORY ROYALE!!!");

                Console.WriteLine("\n\nGOOD LUCK!!!");

                Console.WriteLine("\n\nPlayers:");

                foreach (Player player in dataLocation)
                {
                    Console.WriteLine($"{player.getName()} ({player.getColor()}) - StartingPosition: {player.getStartingPosition()}");
                }
            }

            getUserData(players);
            gameIntro(players);
        }
        public static string AcceptUserInput(string userPrompt, Type expectedType, ConsoleColor promptColor = ConsoleColor.Cyan, ConsoleColor responseColor = ConsoleColor.White, int minLength = 0, double[] bounds = null, string[] validInputs = null, bool caseSensitive = false)
        {
            /*
             * Purpose: Accept user prompt with custom message
             * 
             * Parameters: string userPrompt - The custom prompt the user is presented
             *             Type expectedType - Will check if user's reponse is parseable to this type.
             *             ConsoleColor promptColor (default: Cyan) - Sets the color of the prompt.
             *             ConsoleColor responseColor (default: White) - Sets the color of the user's response.
             *             int minLength (default: 0) - The minimum length of the response.
             *                                          Will repeat prompt if user's response is less than minLength.
             *             double[] bounds (default: null) - If in use, will reprompt user if user's response is not in
             *                                               bounds.
             *             string[] validInputs (default: null) - If in use, will reprompt user if user's response is not
             *                                                    one of the valid inputs in this array.
             *             bool caseSensitive (default: false) - If validInputs is not null and caseSensitive is true, will
             *                                                   check if user input EXACTLY matches a valid input. Otherwise,
             *                                                   it will check case-insensitively.
             *             
             */

            if (bounds != null)
            {
                if (bounds.Length != 2)
                {
                    throw new ArgumentException($"Invalid amount of bounds! (got {bounds.Length}, expected 2)");
                }

                if (bounds[0] > bounds[1])
                {
                    throw new ArgumentException($"Lower bound is larger than upper bound! ({bounds[0]} > {bounds[1]})");
                }

                if (!_IsNumericType(expectedType))
                {
                    throw new ArgumentException($"Bounds in use while expected response is non-numeric! (type: {expectedType})");
                }
            }

            Console.ForegroundColor = promptColor;
            Console.Write(userPrompt, ConsoleColor.Blue);
            Console.ForegroundColor = responseColor;

            string result = Console.ReadLine() ?? _ErrorHandler("User input cannot be null.");

            if (validInputs != null)
            {
                if (!validInputs.Contains(result.ToUpper()))
                {
                    return _ErrorHandler($"User input invalid! (expected {string.Join(", ", validInputs)})");
                }
                if (caseSensitive && !validInputs.Contains(result))
                {
                    return _ErrorHandler($"User input invalid! (expected {string.Join(", ", validInputs)}, needs to be case-sensitive)");
                }
            }

            Console.ResetColor();

            TypeConverter converter = TypeDescriptor.GetConverter(expectedType);
            if (!converter.IsValid(result))
            {
                return _ErrorHandler($"Cannot convert user input to {expectedType}. ");
            }

            if (!string.IsNullOrEmpty(result))
            {
                if (result.Length >= minLength)
                {
                    if (bounds != null)
                    {
                        if (bounds[0] > Convert.ToDouble(Convert.ChangeType(result, expectedType)) ||
                            bounds[1] < Convert.ToDouble(Convert.ChangeType(result, expectedType)))
                        {
                            return _ErrorHandler($"User input not within bounds! (must be between {bounds[0]} and {bounds[1]})");
                        }
                    }

                    return result ?? _ErrorHandler("User input cannot be null.");
                }
                return _ErrorHandler($"User input is too short. (length required: {minLength})");
            }
            return _ErrorHandler("User input cannot be empty.");

            string _ErrorHandler(string errorMessage)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(errorMessage);
                // resets console text color to original color
                Console.ResetColor();
                return AcceptUserInput("Please try again: ", expectedType, promptColor, responseColor, minLength, bounds, validInputs);
            }

            bool _IsNumericType(Type t)
            {
                switch (Type.GetTypeCode(t))
                // found numeric typecodes from https://learn.microsoft.com/en-us/dotnet/api/system.typecode?view=net-6.0
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}