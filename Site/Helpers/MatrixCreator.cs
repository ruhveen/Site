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

        static int YTILES = 8;
        static int XTILES = 8;
        public static bool solveMaze(int[][] resss, int xPos, int yPos, bool[,] alreadySearched, int[][] helper)
     {


         bool correctPath = false;

         //should the computer check this tile
         bool shouldCheck = true;

         //Check for out of boundaries
         if (xPos >= XTILES || xPos < 0 || yPos >= YTILES || yPos < 0)
             shouldCheck = false;
         else
         {

             //Check if at finish, not (0,0 and colored light blue)
             if (xPos == (XTILES-1) && yPos ==  (YTILES-1))
                    {
                        correctPath = true;
                        shouldCheck = false;
                    }
                    //Check for a wall
             if (resss[xPos][yPos] == 1)
                        shouldCheck = false;
                    //Check if previously searched
                    if (alreadySearched[xPos, yPos])
                        shouldCheck = false;
                }
                //Search the Tile
                if (shouldCheck)
                {
                    //mark tile as searched
                    alreadySearched[xPos, yPos] = true;
                    //Check right tile
                    correctPath = correctPath || solveMaze(resss,xPos + 1, yPos, alreadySearched,helper);
                    //Check down tile
                    correctPath = correctPath || solveMaze(resss, xPos, yPos + 1, alreadySearched, helper);
                    //check left tile
                    correctPath = correctPath || solveMaze(resss, xPos - 1, yPos, alreadySearched, helper);
                    //check up tile
                    correctPath = correctPath || solveMaze(resss, xPos, yPos - 1, alreadySearched, helper);

                    //Check right tile
                    correctPath = correctPath || solveMaze(resss, xPos + 1, yPos + 1, alreadySearched, helper);
                    //Check down tile
                    correctPath = correctPath || solveMaze(resss, xPos - 1, yPos - 1, alreadySearched, helper);
                    //check left tile
                    correctPath = correctPath || solveMaze(resss, xPos - 1, yPos + 1, alreadySearched, helper);
                    //check up tile
                    correctPath = correctPath || solveMaze(resss, xPos + 1, yPos - 1, alreadySearched, helper);
                }

                //make correct path gray
                if (correctPath)
                    helper[xPos][yPos] = 1;
                return correctPath;
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