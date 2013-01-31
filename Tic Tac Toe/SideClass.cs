using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    class SideClass
    {
        Boolean[,] filled, players;
        int[] sidesSent1, sidesSent2, cornersX, cornersY;

        public SideClass(Boolean[,] a, Boolean[,] b, int[] c, int[] d, int[] e, int[] f)
        {
            filled = a;
            players = b;
            sidesSent1 = c;
            sidesSent2 = d;
            cornersX = e;
            cornersY = f;
        }

        public int[] TwoSides()
        {
            int posX, posY;

            for (int i = 0; i < 4; i++)
            {
                for (int a = cornersX[i] - 2; a <= cornersX[i] + 2; a++)
                {
                    if (a >= 0 && a <= 2)
                    {
                        if (filled[a, cornersY[i]] == true && players[a, cornersY[i]] == true)// && a != i)
                        {
                            for (int b = cornersY[i] - 2; b <= cornersY[i] + 2; b++)
                            {
                                if (b >= 0 && b <= 2)
                                {
                                    if (filled[cornersX[i], b] == true && players[cornersX[i], b] == true)// && b != i)
                                    {
                                        posX = cornersX[i];
                                        posY = cornersY[i];

                                        return new int[] { posX, posY };
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new int[] { 0, 0 };
        }
    }
}
