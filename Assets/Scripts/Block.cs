using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TicTacToe
{
    public class Block : MonoBehaviour
    {
        public Action<Block> OnMousePressedBlock;

        public Vector2 blockPosition;

        public Sign sign;

        public TextMesh signText;

        public void SetBlockPosition(Vector2 position)
        {
            this.blockPosition = position;

            transform.position = position * 1.5f;
        }

        private void OnMouseDown()
        {
            OnMousePressedBlock?.Invoke(this);
        }

        public void Mark(Sign signMark)
        {
            sign = signMark;

            if (signMark == Sign.X)
            {
                signText.text = "X";
            }
            else if (signMark == Sign.O)
            {
                signText.text = "O";
            }
        }
    }
}
