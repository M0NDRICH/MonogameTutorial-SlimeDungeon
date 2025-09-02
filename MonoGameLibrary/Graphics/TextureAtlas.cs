using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    internal class TextureAtlas
    {
        private Dictionary<string, TextureRegion> _regions;

        // Gets or Sets the source texture represented by this texture atlas
        public Texture2D Texture {  get; set; }


        // Creates a new texture atlas.
        public TextureAtlas()
        {
            _regions = new Dictionary<string, TextureRegion>();
        }

        // Creates a new texture atlas instance using the given texture.
        // param name="texture" - The source texture represented by the texture atlas.
        public TextureAtlas(Texture2D texture)
        {
            Texture = texture;
            _regions = new Dictionary<string, TextureRegion>();
        }

        // Creates a new region and adds it to this texture atlas.
        // param name="name" 
    }
}
