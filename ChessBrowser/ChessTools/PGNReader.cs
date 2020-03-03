using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ChessTools
{
    public class PGNReader
    {
        public PGNReader()
        {

        }

        public List<ChessGame> parseFile(string filename)
        {
            List<ChessGame> Games = new List<ChessGame>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    ChessGame currentGame = new ChessGame();
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (Regex.Match(line, @"\bEvent\b", RegexOptions.IgnoreCase).Success)
                        {
                            currentGame.Event = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        else if (line.Contains("Site"))
                        {
                            currentGame.Site = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");
                        }
                        else if (line.Contains("Round"))
                        {
                            currentGame.Round = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        else if (Regex.Match(line, @"\bWhite\b", RegexOptions.IgnoreCase).Success)
                        {
                            currentGame.White = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        else if (Regex.Match(line, @"\bBlack\b", RegexOptions.IgnoreCase).Success)
                        {
                            currentGame.Black = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        else if (line.Contains("WhiteElo"))
                        {
                            currentGame.WhiteElo = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        else if (line.Contains("BlackElo"))
                        {
                            currentGame.BlackElo = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        else if (line.Contains("Result"))
                        {
                            string result = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");
                            if (result == "1/2-1/2")
                            {
                                currentGame.Result = "D";

                            } else if (result == "1-0")
                            {
                                currentGame.Result = "W";

                            } else if (result == "0-1")
                            {
                                currentGame.Result = "B";

                            }

                        }
                        else if (line.Contains("EventDate"))
                        {
                            currentGame.EventDate = Regex.Match(line, "\"(.*?)]").ToString().Replace("\"", "").Replace("]", "");

                        }
                        if (line == "")
                        {
                            string moves = "";
                            string gameLine;
                            // Read until new line
                            while ((gameLine = sr.ReadLine()) != "") {
                                moves += gameLine;
                            }
                            currentGame.Moves = moves;

                            Games.Add(currentGame);
                            currentGame = new ChessGame();
                        }
                    }


                }
            }
            catch (IOException)
            {
                Console.WriteLine("File cannot be read:");
            }


            return Games;
        }
    }
}
