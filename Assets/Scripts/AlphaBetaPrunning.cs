using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TicTacToe
{
    public class AlphaBetaPrunning
    {
        private static int MAX = 1000;

        private static int MIN = -1000;

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
            else if (board.FindAll(x => x.blockPosition.x == x.blockPosition.y).TrueForAll(x => x.sign == oppositePlayer))
            {
                return -10;
            }

            List<Block> negDiagonal =
                new List<Block>()
                {
                    board.Find(x => x.blockPosition == new Vector2(0, 2)),
                    board.Find(x => x.blockPosition == new Vector2(1, 1)),
                    board.Find(x => x.blockPosition == new Vector2(2, 0))
                };

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

        public int Minimax(List<Block> blocks, Sign currentPlayer, Sign oppositePlayer, int alpha, int beta, bool isMax)
        {
            int score = EvaluateBoard(blocks, currentPlayer, oppositePlayer);

            if (score == 10)
            {
                return 10;
            }

            if (score == -10)
            {
                return -10;
            }

            if (!IsEmptyBlock(blocks))
            {
                return 0;
            }

            if (isMax)
            {
                int best = MIN;

                for (int i = 0; i < blocks.Count; i++)
                {
                    if (blocks[i].sign == Sign.Empty)
                    {
                        blocks[i].sign = currentPlayer;

                        int value = Minimax(blocks, currentPlayer, oppositePlayer, alpha, beta, !isMax);

                        blocks[i].sign = Sign.Empty;

                        best = Mathf.Max(best, value);

                        alpha = Math.Max(alpha, best);

                        if (beta <= alpha)
                        {
                            break;
                        }

                    }
                }

                return best;

            }
            else
            {
                int best = MAX;

                for (int i = 0; i < blocks.Count; i++)
                {
                    if (blocks[i].sign == Sign.Empty)
                    {
                        blocks[i].sign = oppositePlayer;

                        int value = Minimax(blocks, currentPlayer, oppositePlayer, alpha, beta, !isMax);


                        blocks[i].sign = Sign.Empty;

                        best = Math.Min(best, value);

                        beta = Math.Min(beta, best);

                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }

                return best;
            }
        }

        public Block GetBestBlock(List<Block> blocks, Sign currentPlayer, Sign oppositePlayer)
        {
            Block block = null;

            int best = -1000;

            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].sign == Sign.Empty)
                {
                    blocks[i].sign = currentPlayer;

                    int bestValue = Minimax(blocks, currentPlayer, oppositePlayer, MIN, MAX, false);

                    blocks[i].sign = Sign.Empty;


                    if (bestValue > best)
                    {
                        Debug.Log(bestValue);

                        best = bestValue;

                        block = blocks[i];
                    }
                }
            }

            return block;
        }
    }
}
