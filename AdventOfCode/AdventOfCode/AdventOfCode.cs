using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Collections.Generic;

namespace AdventOfCode
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            var c = new AdventDay6();
            c.PrintParts();
        }

        #region Advent Day 1
        class AdventDay1
        {
            readonly StreamReader reader = new("InputDay1.txt");
            readonly List<int> snacks = new();

            private int _snack = 0;
            private int _snacktotal = 0;
            public void PrintParts()
            {
                string[] inputLines = reader.ReadToEnd().Split("\n");

                foreach (string line in inputLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        _snack += Int32.Parse(line);
                    }
                    else
                    {
                        snacks.Add(_snack);
                        _snack = 0;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    _snacktotal += snacks.Max();
                    snacks.Remove(snacks.Max());
                }
                Console.WriteLine("Part 1: " + snacks.Max());
                Console.WriteLine("Part 2: " + _snacktotal);
            }
        }
        #endregion

        #region Advent Day 2
        class AdventDay2
        {
            readonly StreamReader reader = new("InputDay2.txt");
            private int _score;
            public void PrintParts()
            {
                string[] inputLines = reader.ReadToEnd().Split("\n");
                int[] opponentData = new int[inputLines.Length];
                int[] yourData = new int[inputLines.Length];
                int spotArray = 0;

                //letter to number
                foreach (string line in inputLines)
                {
                    opponentData[spotArray] = line[0] switch
                    {
                        'A' => 1,
                        'B' => 2,
                        'C' => 3,
                        _ => 0,
                    };
                    yourData[spotArray] = line[2] switch
                    {
                        'X' => 1,
                        'Y' => 2,
                        'Z' => 3,
                        _ => 0,
                    };
                    spotArray++;
                }

                //give score
                for (int round = 0; round < opponentData.Length; round++)
                {
                    //Part 2 Start
                    switch (yourData[round])
                    {
                        case 1:
                            switch (opponentData[round])
                            {
                                case 1:
                                    yourData[round] = 3;
                                    break;
                                case 2:
                                    yourData[round] = 1;
                                    break;
                                case 3:
                                    yourData[round] = 2;
                                    break;
                            }
                            break;
                        case 2:
                            yourData[round] = opponentData[round];
                            break;
                        case 3:
                            switch (opponentData[round])
                            {
                                case 1:
                                    yourData[round] = 2;
                                    break;
                                case 2:
                                    yourData[round] = 3;
                                    break;
                                case 3:
                                    yourData[round] = 1;
                                    break;
                            }
                            break;
                    }
                    //Part 2 end

                    _score += yourData[round];
                    if (opponentData[round] == yourData[round])
                        _score += 3;
                    if (yourData[round] - opponentData[round] == -2 || yourData[round] - opponentData[round] == 1)
                        _score += 6;
                }
                Console.WriteLine(_score);
            }
        }
        #endregion

        #region Advent Day 3
        class AdventDay3
        {
            readonly StreamReader reader = new("InputDay3.txt");

            public void PrintParts()
            {
                string[] inputLines = reader.ReadToEnd().Split("\n");
                string[] compartmentOne = new string[inputLines.Length];
                string[] compartmentTwo = new string[inputLines.Length];

                int sumIndividual = 0;
                int sumTotal = 0;

                //slice the string in two (the compartments)
                for (int i = 0; i < inputLines.Length; i++)
                {
                    compartmentOne[i] = inputLines[i][..(inputLines[i].Length / 2)];
                    compartmentTwo[i] = inputLines[i][(inputLines[i].Length / 2)..];
                }

                //check what letter is in both compartments
                foreach (string line in compartmentOne)
                {
                    foreach (char c in line)
                    {
                        if (compartmentTwo[Array.IndexOf(compartmentOne, line)].Contains(c))
                        {
                            sumIndividual += (c > 96 ? c - 96 : c - 38);
                            break;
                        }
                    }
                }
                Console.WriteLine("Part 1: " + sumIndividual);

                for (int i = 0; i < inputLines.Length; i += 3)
                {
                    string one = inputLines[i];
                    string two = inputLines[i + 1];
                    string three = inputLines[i + 2];

                    foreach (char c in one)
                    {
                        if (two.Contains(c) && three.Contains(c))
                        {
                            sumTotal += (c > 96 ? c - 96 : c - 38);
                            break;
                        }
                    }
                }
                Console.WriteLine("Part 2: " + sumTotal);
            }
        }
        #endregion

        #region Advent Day 4
        class AdventDay4
        {
            readonly StreamReader reader = new("InputDay4.txt");
            int amountOfContains = 0;
            int amountOfOverlaps = 0;
            public void PrintParts()
            {
                string[] inputLines = reader.ReadToEnd().Split("\n");

                foreach (string line in inputLines)
                {
                    string[] pair = line.Split(',');
                    string[] firstSectors = pair[0].Split('-');
                    string[] secondSector = pair[1].Split('-');

                    // if contains
                    if (Int32.Parse(firstSectors[0]) <= Int32.Parse(secondSector[0]) && 
                        Int32.Parse(firstSectors[1]) >= Int32.Parse(secondSector[1]) ||
                        Int32.Parse(firstSectors[1]) <= Int32.Parse(secondSector[1]) &&
                        Int32.Parse(firstSectors[0]) >= Int32.Parse(secondSector[0]))
                    {
                        amountOfContains++;
                    }

                    // if overlaps
                    if (Int32.Parse(firstSectors[0]) >= Int32.Parse(secondSector[0]) && Int32.Parse(firstSectors[0]) <= Int32.Parse(secondSector[1]) || 
                        Int32.Parse(firstSectors[1]) >= Int32.Parse(secondSector[0]) && Int32.Parse(firstSectors[1]) <= Int32.Parse(secondSector[1]) ||
                        Int32.Parse(secondSector[0]) >= Int32.Parse(firstSectors[0]) && Int32.Parse(secondSector[0]) <= Int32.Parse(firstSectors[1]) ||
                        Int32.Parse(secondSector[1]) >= Int32.Parse(firstSectors[0]) && Int32.Parse(secondSector[1]) <= Int32.Parse(firstSectors[1]))
                    {
                        amountOfOverlaps++;
                    }
                }
                Console.WriteLine("Part1: " + amountOfContains);
                Console.WriteLine("Part2: " + amountOfOverlaps);
            }
        }
        #endregion

        #region Advent Day 5
        
        class AdventDay5
        {
            readonly StreamReader reader = new("InputDay5.txt");

            #region Stack arrays
            readonly char[] one = new char[] { 'M', 'J', 'C', 'B', 'F', 'R', 'L', 'H' };
            readonly char[] two = new char[] { 'Z', 'C', 'D' };
            readonly char[] three = new char[] {'H', 'J', 'F', 'C', 'N', 'G', 'W'};
            readonly char[] four = new char[] {'P', 'J', 'D', 'M', 'T', 'S', 'B'};
            readonly char[] five = new char[] {'N', 'C', 'D', 'R', 'J'};
            readonly char[] six = new char[] {'W', 'L', 'D', 'Q', 'P', 'J', 'G', 'Z'};
            readonly char[] seven = new char[] {'P', 'Z', 'T', 'F', 'R', 'H'};
            readonly char[] eight = new char[] {'L', 'V', 'M', 'G'};
            readonly char[] nine = new char[] {'C', 'B', 'G', 'P', 'F', 'Q', 'R', 'J'};
            #endregion

            public void PrintParts()
            {
                string[] inputLines = reader.ReadToEnd().Split("\n");

                #region Stack Stacks
                Stack<char> stack1 = new(one);
                Stack<char> stack2 = new(two);
                Stack<char> stack3 = new(three);
                Stack<char> stack4 = new(four);
                Stack<char> stack5 = new(five);
                Stack<char> stack6 = new(six);
                Stack<char> stack7 = new(seven);
                Stack<char> stack8 = new(eight);
                Stack<char> stack9 = new(nine);
                #endregion

                Stack<char>[] stacks = new[]
                {
                    stack1,
                    stack2,
                    stack3,
                    stack4,
                    stack5,
                    stack6,
                    stack7,
                    stack8,
                    stack9
                };

                //part 1
                //foreach(string line in inputLines)
                //{
                //    string[] prompt = line.Split(' ');
                //    //Console.Write(prompt[1], prompt[3], prompt[5]);

                //    int amount = Int32.Parse(prompt[1]);
                //    int from = Int32.Parse(prompt[3]);
                //    int to = Int32.Parse(prompt[5]);

                //    for (int i = 0; i < amount; i++)
                //    {
                //        char temp = stacks[from - 1].Pop();
                //        stacks[to - 1].Push(temp);
                //    }
                //}

                //foreach (Stack<char> stack in stacks)
                //{
                //    Console.Write(stack.Peek());
                //}
                //end part 1

                //part two
                foreach (string line in inputLines)
                {
                    string[] prompt = line.Split(' ');
                    //Console.Write(prompt[1], prompt[3], prompt[5]);

                    int amount = Int32.Parse(prompt[1]);
                    int from = Int32.Parse(prompt[3]);
                    int to = Int32.Parse(prompt[5]);

                    List<char> result = new List<char>();

                    for (int i = 0; i < amount; i++)
                    {
                        char temp = stacks[from - 1].Pop();
                        result.Add(temp);
                    }
                    result.Reverse();
                    foreach (char ch in result)
                    {
                        stacks[to - 1].Push(ch);
                    }
                }
                foreach (Stack<char> stack in stacks)
                {
                    Console.Write(stack.Peek());
                }
            }
        }
        #endregion

        #region Advent Day 6
        class AdventDay6
        {
            readonly StreamReader reader = new("InputDay6.txt");
            
            public void PrintParts()
            {
                string inputLine = reader.ReadToEnd();

                for (int i = 1; i < inputLine.Length - 14; i++) 
                {
                    #region Heresy
                    if (inputLine[(char)i] != inputLine[(char)i + 1] &&
                    inputLine[(char)i] != inputLine[(char)i + 2] &&
                    inputLine[(char)i] != inputLine[(char)i + 3] &&
                    inputLine[(char)i] != inputLine[(char)i + 4] &&
                    inputLine[(char)i] != inputLine[(char)i + 5] &&
                    inputLine[(char)i] != inputLine[(char)i + 6] &&
                    inputLine[(char)i] != inputLine[(char)i + 7] &&
                    inputLine[(char)i] != inputLine[(char)i + 8] &&
                    inputLine[(char)i] != inputLine[(char)i + 9] &&
                    inputLine[(char)i] != inputLine[(char)i + 10] &&
                    inputLine[(char)i] != inputLine[(char)i + 11] &&
                    inputLine[(char)i] != inputLine[(char)i + 12] &&
                    inputLine[(char)i] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 2] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 3] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 4] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 5] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 6] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 7] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 1] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 3] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 4] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 5] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 6] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 7] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 2] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 4] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 5] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 6] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 7] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 3] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 5] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 6] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 7] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 4] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 6] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 7] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 5] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 7] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 6] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 7] != inputLine[(char)i + 8] &&
                    inputLine[(char)i + 7] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 7] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 7] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 7] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 7] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 8] != inputLine[(char)i + 9] &&
                    inputLine[(char)i + 8] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 8] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 8] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 8] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 9] != inputLine[(char)i + 10] &&
                    inputLine[(char)i + 9] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 9] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 9] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 10] != inputLine[(char)i + 11] &&
                    inputLine[(char)i + 10] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 10] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 11] != inputLine[(char)i + 12] &&
                    inputLine[(char)i + 11] != inputLine[(char)i + 13] &&
                    inputLine[(char)i + 12] != inputLine[(char)i + 13])
                    #endregion
                    {
                        Console.WriteLine(i + 14);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}



/*
 * KladBlok
 *
 * If the firts number is higher than the other first number and the second number is lower than the second do shit 
 */