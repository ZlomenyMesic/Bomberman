using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    #region Block
    public class Block
    {
        public Vector2 vector;
        public BlockType blockType;

        /// <summary>
        /// Create a new block
        /// </summary>
        /// <param name="newVector">XY window-relative coordinates, top lelft corner</param>
        /// <param name="newBlockType">The block type</param>
        public Block(Vector2 newVector, BlockType newBlockType)
        {
            vector = newVector;
            blockType = newBlockType;
        }

        /// <summary>
        /// Change the block type
        /// </summary>
        /// <param name="newBlockType">The new block type/param>
        public void ChangeType(BlockType newBlockType)
        {
            blockType = newBlockType;
        }
    }
    #endregion
}

