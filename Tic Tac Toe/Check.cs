using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    class Check
    {
        private Boolean[,] filled, players;

        public Check (Boolean[,] a, Boolean[,] b)
        {
            filled = a;
            players = b;
        }

        public bool Fork()
        {
            int x, y;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (filled[i, j] == true && players[i, j] == true)
                    {
                        //TL L
                        x = i - 2;
                        y = j - 1;

                        if (x >= 0 && y >= 0)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //TL R
                        x = i - 1;
                        y = j - 2;

                        if (x >= 0 && y >= 0)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //TR L
                        x = i + 1;
                        y = j - 2;

                        if (x <= 2 && y >= 0)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //TR R
                        x = i + 2;
                        y = j - 1;

                        if (x <= 2 && y >= 0)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //BR R
                        x = i + 2;
                        y = j + 1;

                        if (x <= 2 && y <= 2)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //BR L
                        x = i + 1;
                        y = j + 2;

                        if (x <= 2 && y <= 2)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //BL R
                        x = i - 1;
                        y = j + 2;

                        if (x >= 0 && y <= 2)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }

                        //BL L
                        x = i - 2;
                        y = j + 1;

                        if (x >= 0 && y <= 2)
                        {
                            if (filled[x, y] == true && players[x, y] == true)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
        }

        public int[] blockFork()
        {
            int posX, posY;
            bool breaking;
     //       breaking = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (filled[i, j] == false)
                    {
                        breaking = false;

                        for (int a = i - 2; a <= i + 2; a++)
                        {
                            if (a >= 0 && a <= 2)
                            {
                                if (filled[a, j] == true && players[a, j] == true && a != i)
                                {
                                    for (int b = j - 2; b <= j + 2; b++)
                                    {
                                        if (b >= 0 && b <= 2)
                                        {
                                            if (filled[i, b] == true && players[i, b] == true && b != j)
                                            {
                                                for (int c = i - 2; c <= i + 2; c++)
                                                {
                                                    if (c >= 0 && c <= 2)
                                                    {
                                                        if (filled[c, j] == true && players[c, j] == false && c != i)
                                                        {
                                                            breaking = true;
                                                            break;
                                                        }
                                                    }
                                                }

                                                if (breaking == false)
                                                {
                                                    for (int d = j - 2; d <= j + 2; d++)
                                                    {
                                                        if (d >= 0 && d <= 2)
                                                        {
                                                            if (filled[i, d] == true && players[i, d] == false && d != j)
                                                            {
                                                                breaking = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }

                                                if (breaking == false)
                                                {
                                                    posX = i;
                                                    posY = j;
                                                    return new int[] { i, j };
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new int[] {1, 1};
        }

  /*      public int[] TwoSides()
       {
           return new int[] {1, 1};
       } */
    }
}
