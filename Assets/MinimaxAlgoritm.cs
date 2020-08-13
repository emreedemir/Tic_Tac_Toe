using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TicTacToe
{
    public class MinimaxAlgoritm
    {
        private bool IsEmptyBlock(List<Block> board)
        {
            return board.Find(x => x.sign == Sign.Empty);
        }

        int EvaluateBoard(List<Block> board, Sign currentPlayer, Sign oppositePlayer)
        {
            //Rows
            for (int i = 0; i < 3; i++)
            {
                if (board.FindAll(x => x.blockPosition.x == i).TrueForAll(x => x.sign == currentPlayer))
                {
                    return 10;
                }
                else if (board.FindAll(x => x.blockPosition.x == i).TrueForAll(x => x.sign == oppositePlayer))
                {
                    return -10;
                }
            }

            //Columns
            for (int j = 0; j < 3; j++)
            {
                if (board.FindAll(x => x.blockPosition.y == j).TrueForAll(x => x.sign == currentPlayer))
                {
                    return 10;
                }
                else if (board.FindAll(x => x.blockPosition.y == j).TrueForAll(x => x.sign == oppositePlayer))
                {
                    return -10;
                }
            }

            //Diagonals

            if (board.FindAll(x => x.blockPosition.x == x.blockPosition.y).TrueForAll(x => x.sign == currentPlayer))
            {
                return 10;
            }
            else if (board.FindAll(x => x.blockPosition.x == x.blockPosition.y).TrueForAll(x => x.sign == currentPlayer))
            {
                return -10;
            }

            List<Block> negDiagonal =
                new List<Block>() { board.Find(x => x.blockPosition == new Vector2(0, 2)), board.Find(x => x.blockPosition == new Vector2(1, 1)), board.Find(x => x.blockPosition == new Vector2(2, 0)) };

            if (negDiagonal.TrueForAll(x => x.sign == currentPlayer))
            {
                return 10;
            }
            else if (negDiagonal.TrueForAll(x => x.sign == oppositePlayer))
            {
                return -10;
            }

            return 0;
        }

        private int Minimax(List<Block> board, Sign currentPlayer, Sign oppositePlayer, int depth, bool isMax)
        {
            int score = EvaluateBoard(board, currentPlayer, oppositePlayer);

            if (score == 10)
            {
                return 10;
            }

            if (score == -10)
            {
                return -10;
            }

            if (!IsEmptyBlock(board))
            {
                return 0;
            }


            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < board.Count; i++)
                {
                    if (board[i].sign == Sign.Empty)
                    {
                        board[i].sign = currentPlayer;

                        best = Mathf.Max(best, Minimax(board, currentPlayer, oppositePlayer, depth + 1, !isMax));

                        board[i].sign = Sign.Empty;
                    }
                }

                return best;

            }
            else
            {
                Debug.Log("Minimize");
                int best = 1000;

                for (int i = 0; i < board.Count; i++)
                {
                    if (board[i].sign == Sign.Empty)
                    {

                        board[i].sign = oppositePlayer;

                        best = Mathf.Min(best, Minimax(board, currentPlayer, oppositePlayer, depth + 1, !isMax));


                        board[i].sign = Sign.Empty;
                    }
                }

                return best;

            }
        }

        public Block GetBestBlock(List<Block> board, Sign currentPlayerSign, Sign oppositePlayerSign)
        {
            Block block = null;

            int bestScore = -1000;

            for (int i = 0; i < board.Count; i++)
            {

                if (board[i].sign == Sign.Empty)
                {
                    board[i].sign = currentPlayerSign;

                    int score = Minimax(board, currentPlayerSign, oppositePlayerSign, 0, false);

                    board[i].sign = Sign.Empty;

                    if (score > bestScore)
                    {
                        bestScore = score;

                        block = board[i];
                    }
                }
            }

            return block;
        }
    }

}

