using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TicTacToe
{
    public class PerfectAlphaBetaPrunning
    {
        private static int MAX = 1000;

        private static int MIN = -1000;

        private bool IsEmptyBlock(List<Block> board)
        {
            return board.Find(x => x.sign == Sign.Empty);
        }

        int EvaluateBoard(List<Block> board, Sign currentPlayer, Sign oppositePlayer, int currentPlay)
        {
            //Rows
            for (int i = 0; i < 3; i++)
            {
                if (board.FindAll(x => x.blockPosition.x == i).TrueForAll(x => x.sign == currentPlayer))
                {
                    return 10 - currentPlay;
                }
                else if (board.FindAll(x => x.blockPosition.x == i).TrueForAll(x => x.sign == oppositePlayer))
                {
                    return -10 + currentPlay;
                }
            }

            //Columns
            for (int j = 0; j < 3; j++)
            {
                if (board.FindAll(x => x.blockPosition.y == j).TrueForAll(x => x.sign == currentPlayer))
                {
                    return 10 - currentPlay;
                }
                else if (board.FindAll(x => x.blockPosition.y == j).TrueForAll(x => x.sign == oppositePlayer))
                {
                    return -10 + currentPlay;
                }
            }

            //Diagonals
            if (board.FindAll(x => x.blockPosition.x == x.blockPosition.y).TrueForAll(x => x.sign == currentPlayer))
            {
                return 10 - currentPlay;
            }
            else if (board.FindAll(x => x.blockPosition.x == x.blockPosition.y).TrueForAll(x => x.sign == oppositePlayer))
            {
                return -10 + currentPlay;
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
                return 10 - currentPlay;
            }
            else if (negDiagonal.TrueForAll(x => x.sign == oppositePlayer))
            {
                return -10 + currentPlay;
            }

            return 0;
        }

        public int Minimax(List<Block> blocks, Sign currentPlayer, Sign oppositePlayer, int alpha, int beta, bool isMax, int currentPlay)
        {
            int score = EvaluateBoard(blocks, currentPlayer, oppositePlayer, currentPlay);

            if (score == 10 - currentPlay)
            {
                return 10 - currentPlay;
            }

            if (score == -10 + currentPlay)
            {
                return -10 + currentPlay;
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

                        currentPlay++;
                        int value = Minimax(blocks, currentPlayer, oppositePlayer, alpha, beta, !isMax, currentPlay);

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

                        currentPlay++;
                        int value = Minimax(blocks, currentPlayer, oppositePlayer, alpha, beta, !isMax, currentPlay);


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

                    int currentPlay = 0;

                    int bestValue = Minimax(blocks, currentPlayer, oppositePlayer, MIN, MAX, false, currentPlay);

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


