using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    // Represents a rectangular region within a texture
    public class TextureRegion
    {
        // Gets or Sets the source texture this texture region is part of.
        public Texture2D Texture {  get; set; }

        // Gets or Sets the source rectangle boundary of this texture region within the source texture.
        public Rectangle SourceRectangle { get; set; }

        // Gets the width, in pixels, of this texture region.
        public int Width => SourceRectangle.Width;

        // Gets the height, in pixels, of this texture region.
        public int Height => SourceRectangle.Height;
        
        // Creaates a new texture region.
        public TextureRegion() { }

        // Creates a new texture region using the specified source texture.
        // param name="texture" - The texture to use as the source texture for this texture region.
        // param name="x" - The x-coordinate position of the upper-left corner of this texture region relative to the upper-left corner of the source texture.
        // param name="y" - The y-coordinate position of the upper-left corner of this texture region relative to the upper-left corner of the source texture.
        // param name="width" - The width, in pixels, of this texture region.
        // param name="height" - The height, in pixels, of this texture region.
        public TextureRegion(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            SourceRectangle = new Rectangle(x, y, width, height);
        }

        // Submit this texture region for drawing in the current batch
        // param name="spriteBatch" - The spritebatch instance used for batching draw calls.
        // param name="position" - The xy-coordinate location to draw this texture region on the screen.
        // param name="color" - The color mask to apply when drawing this texture regiono on screen.
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Draw(spriteBatch, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
        }

        // Submit this texture region for drawing in the current batch.
        // param name="spriteBatch" - The spritebatch instance used for batching draw calls.
        // param name="position" - The xy-coordinate location to draw this texture region on the screen.
        // param name="color" - The color mask to apply when drawing this texture region on screen.
        // param name="rotation" - The amount of rotation, in radians, to apply when drawing this texture region on screen.
        // param name="origin" - The center of rotation, scaling, and position when drawing this texture region on screen.
        // param name="scale" - The scale factor to apply when drawing this texture region on screen.
        // param name="effects" - Specifies if this texture region should be flipped horizontally, vertically, or both when drawing on screen.
        // param name="layerDepth" - The depth of the layer to use when drawing this texture region on screen.
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Draw(
                spriteBatch,
                position,
                color,
                rotation,
                origin,
                new Vector2(scale, scale),
                effects,
                layerDepth
            );
        }


        // Submit this texture region for drawing in the current batch.
        // param name="spriteBatch" - The spritebatch instance used for batching draw calls.
        // param name="position" - The xy-coordinate location to draw this texture region on the screen.
        // param name="color" - The color mask to apply when drawing this texture region on screen.
        // param name="rotation">The amount of rotation, in radians, to apply when drawing this texture region on screen.
        // param name="origin" - The center of rotation, scaling, and position when drawing this texture region on screen.
        // param name="scale" - The amount of scaling to apply to the x- and y-axes when drawing this texture region on screen.
        // param name="effects" - Specifies if this texture region should be flipped horizontally, vertically, or both when drawing on screen.
        // param name="layerDepth" - The depth of the layer to use when drawing this texture region on screen.
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(
                Texture,
                position,
                SourceRectangle,
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth
            );
        }
    }
}
