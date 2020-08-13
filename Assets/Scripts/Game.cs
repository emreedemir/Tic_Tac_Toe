using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class Game : MonoBehaviour
    {
        public List<Block> blocks;

        public Block blockPrefab;

        bool playing;

        MinimaxAlgoritm minimaxAlgoritm;

        AlphaBetaPrunning alphaBetaPrunning;

        PerfectAlphaBetaPrunning perfectAlphaBetaPrunning;


        public readonly Vector2[] blockPositions =
        {
        new Vector2(0,0),
        new Vector2(1,0),
        new Vector2(2,0),
        new Vector2(0,1),
        new Vector2(1,1),
        new Vector2(2,1),
        new Vector2(0,2),
        new Vector2(1,2),
        new Vector2(2,2)
        };

        private void Start()
        {
            CreateBoard();

            minimaxAlgoritm = new MinimaxAlgoritm();

            alphaBetaPrunning = new AlphaBetaPrunning();

            perfectAlphaBetaPrunning = new PerfectAlphaBetaPrunning();

            StartToGame();
        }

        public void CreateBoard()
        {
            blocks = new List<Block>();

            for (int i = 0; i < blockPositions.Length; i++)
            {
                Block newBlock = Instantiate(blockPrefab);

                newBlock.sign = Sign.Empty;

                newBlock.OnMousePressedBlock += HandleBlockClick;

                newBlock.SetBlockPosition(blockPositions[i]);

                blocks.Add(newBlock);

            }
        }

        public void StartToGame()
        {
            playing = true;
        }

        public void HandleBlockClick(Block block)
        {
            if (playing == true)
            {
                if (block.sign == Sign.Empty)
                {
                    block.Mark(Sign.X);

                    EvaluateBoard();
                }
            }
        }

        private void EvaluateBoard()
        {
            //Human Playing
            if (playing == true)
            {
                //Degerlendir

                if (IsGameFinish())
                {
                    Debug.Log("Human Win The Game");
                }
                else
                {

                    PlayAI();
                }
            }
            //AI Playing
            else
            {
                if (IsGameFinish())
                {
                    Debug.Log("AI Wing The Game");
                }
                else
                {
                    playing = true;
                }
            }
        }

        public void PlayAI()
        {
            Block block = perfectAlphaBetaPrunning.GetBestBlock(blocks, Sign.O, Sign.X);

            if (block != null)
            {
                playing = false;

                block.Mark(Sign.O);

                EvaluateBoard();
            }
            else
            {
                Debug.Log("Block is null");
            }

        }

        private bool IsGameFinish()
        {
            return !blocks.Find(x => x.sign == Sign.Empty);
        }
    }

    public enum Sign
    {
        Empty,
        X,
        O
    }
}
