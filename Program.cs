// See https://aka.ms/new-console-template for more information
using System;
using System.Numerics;

class Program
{
    static char[,] chessboard = new char[15, 15];
    static int currentPlayer = 1;

    static void Main()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++) 
            {
                chessboard[i, j] = '.';
            }
        }

        while (true)
        {
            PrintBoard();
            MakeMove();
            currentPlayer = 3 - currentPlayer;
        }
    }

    static void PrintBoard()
    {
        for (int i = 0; i < 15; ++i)
        {
            for (int j = 0; j < 15; ++j)
            {
                Console.Write(chessboard[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void MakeMove()
    {
        int x = 0, y = 0;
        do
        {
            Console.WriteLine("Player {0}'s move (0-14) x y:", currentPlayer);
            var input = Console.ReadLine().Split(' ');
            x = int.Parse(input[0]);
            y = int.Parse(input[1]);
            if (x < 0 || x >= 15 || y < 0 || y >= 15 || chessboard[x, y] != '.')
            {
                Console.WriteLine("Invalid move! Redo it");
            }
        } while (x < 0 || x >= 15 || y < 0 || y >= 15 || chessboard[x, y] != '.');

        chessboard[x, y] = currentPlayer == 1 ? 'X' : 'O';
        bool whetherWin = CheckWinner(x, y);
        if (whetherWin)
        {
            Console.WriteLine("Yeah, we have winner!");
        }
    }

    static bool CheckWinner(int x, int y)
    {
        int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
        int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };
        char player = currentPlayer == 1 ? 'X' : 'O';
        for (int i = 0; i < 8; i += 2)
        {
            int count = 1; // 计算连续棋子的数目
            // 检查正方向
            for (int nx = x + dx[i], ny = y + dy[i];
                 nx >= 0 && ny >= 0 && nx < 15 && ny < 15 && chessboard[nx, ny] == player;
                 nx += dx[i], ny += dy[i])
            {
                count++;
            }
            // 检查反方向
            for (int nx = x + dx[i + 1], ny = y + dy[i + 1];
                 nx >= 0 && ny >= 0 && nx < 15 && ny < 15 && chessboard[nx, ny] == player;
                 nx += dx[i + 1], ny += dy[i + 1])
            {
                count++;
            }
            // 如果连续棋子的数目等于5，则返回true
            if (count >= 5)
            {
                return true;
            }
        }
        // 所有方向都检查完，如果没有五子连珠，则返回false
        return false;
    }
}
