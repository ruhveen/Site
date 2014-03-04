using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Helpers
{
    public static class MatrixCreator
    {
        public static int[][] New
        {
            get
            {
                Random r = new Random();
                int[][] newMat = new int[8][];
                for (int row = 0; row < newMat.Length; row++)
                {
                    newMat[row] = new int[newMat.Length];
                    for (int col = 0; col < newMat.Length; col++)
                    {
                        newMat[row][col] = r.Next(2);
                    }
                }
                newMat[0][0] = 0;
                newMat[newMat.Length - 1][newMat.Length - 1] = 0;
                return newMat;
            }
        }

        internal static int[][] Solve(int[][] resss)
        {
            int[][] helper = new int[resss.Length][];

            for (int row = 0; row < helper.Length; row++)
            {
                helper[row] = new int[helper.Length];
            }

            helper[0][0] = 1;

            for (int row = 0; row < resss.Length; row++)
            {
                for (int col = 0; col < resss.Length; col++)
                {
                    if(resss[row][col] == 0)
                    {
                        if (aroundMeIsOneInHelper(helper,row, col))
                        {
                            helper[row][col] = 1;
                        }
                    }
                }
            }
            return helper;

        }

        private static bool aroundMeIsOneInHelper(int[][] helper, int row, int col)
        {
            bool res = false;
            // up left
            if(row>0 && col>0)
            {
                if (helper[row - 1][col - 1] == 1)
                    return true;
                    
            }
            // up
            if (row > 0)
            {
                if (helper[row - 1][col] == 1)
                    return true;

            }
            // up right
            if (row > 0 && (col + 1) < helper.Length)
            {
                if (helper[row - 1][col + 1] == 1)
                    return true;

            }
            // left
            if (col > 0)
            {
                if (helper[row][col - 1] == 1)
                    return true;

            }
            return false;
            
        }
    }
}