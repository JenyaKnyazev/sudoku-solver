using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class Program
    {
        static int [,]mat=new int[9,9]{{5,3,0,0,7,0,0,0,0},
                                       {6,0,0,1,9,5,0,0,0},
                                       {0,9,8,0,0,0,0,6,0},
                                       {8,0,0,0,6,0,0,0,3},
                                       {4,0,0,8,0,3,0,0,1},
                                       {7,0,0,0,2,0,0,0,6},
                                       {0,6,0,0,0,0,2,8,0},
                                       {0,0,0,4,1,9,0,0,5},
                                       {0,0,0,0,8,0,0,7,9}};

        static int[,] mat2 = new int[6, 9]{ {0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0},};
        static bool isPosible(int [,]sudoku,int val,int row,int col) {
            for (int i = 0; i < sudoku.GetLength(0); i++)
                if(i!=row&&sudoku[i,col]==val)
                    return false;
            for (int i = 0; i < sudoku.GetLength(1); i++)
                if (i != col && sudoku[row, i] == val)
                    return false; 
            for(int i=row-row%3;i<=row+((row%3==0)?2:row%3%2);i++)
                for (int r = col - col % 3; r <= col + ((col % 3 == 0) ? 2 : col%3 % 2); r++)
                    if(i!=row||r!=col)
                        if(sudoku[i,r]==val)
                            return false;
            return true; 
        }
        static void print(int[,] sudoku) {
            Console.WriteLine();
            for(int i=0;i<sudoku.GetLength(0);i++){
                for (int r = 0; r < sudoku.GetLength(1); r++)
                    Console.Write(sudoku[i, r] + ",");
                Console.WriteLine();
            }
        }
        static bool solve(int row,int col,int [,]sudoku,int val) {
            int nextRow = row;
            int nextCol = col + 1;
            bool correct = false;
            if (nextCol == sudoku.GetLength(1)) {
                nextCol = 0;
                nextRow++;
            }
            if (row == sudoku.GetLength(0))
                return true;
            if (sudoku[row, col] == 0)
            {
                if (isPosible(sudoku, val, row, col))
                    sudoku[row, col] = val;
                else
                    return false;
                if(nextRow<sudoku.GetLength(0)&&sudoku[nextRow,nextCol]!=0)
                    correct=solve(nextRow,nextCol,sudoku,0);
                else
                    for(int i=1;i<=9&&!correct;i++)
                        correct=solve(nextRow,nextCol,sudoku,i);
                    if(!correct){
                        sudoku[row,col]=0;
                        return false;
                    }
            }
            else {
                if(nextRow<sudoku.GetLength(0)&&sudoku[nextRow,nextCol]!=0)
                    correct=solve(nextRow,nextCol,sudoku,0);
                else
                    for(int i=1;i<=9&&!correct;i++)
                        correct=solve(nextRow,nextCol,sudoku,i);
                if(!correct)
                       return false;
                     
            }
            return true;
        }
        static void solve(int[,]sudoku) {
            bool correct = false;
            for (int i = 1; i <= 9 && !correct; i++)
                correct = solve(0, 0, sudoku, i);
        }
        static void run() {
            string inp;
            while(true){
                Console.WriteLine("fill the sudoku or ilegal char to exit");
                for (int i = 0; i < 9; i++) {
                    inp = Console.ReadLine();
                    if (!isCorrectInput(inp))
                        break;
                    for (int r = 0; r < inp.Length; r++)
                        mat[i, r] = int.Parse(inp[r]+"");
                }
                print(mat);
                solve(mat);
                print(mat);
            }
            Console.WriteLine("Good bye");
        }
        static bool isCorrectInput(string str) {
            foreach (char i in str)
                if (!(i >= '0' && i <= '9'))
                    return false;
            if (str.Length != 9)
                return false;
            return true;
        }
        static void Main(string[] args)
        {
            print(mat2);
            solve(mat2);
            print(mat2);
            print(mat);
            solve(mat);
            print(mat);
            run();
            Console.ReadLine();
        }
    }
}
