using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MonoGameLibrary.Graphics
{
    public class TextureAtlas
    {
        private Dictionary<string, TextureRegion> _regions;

        // Stores animations added to this atlas.
        private Dictionary<string, Animation> _animations;

        // Gets or Sets the source texture represented by this texture atlas
        public Texture2D Texture {  get; set; }


        // Creates a new texture atlas.
        public TextureAtlas()
        {
            _regions = new Dictionary<string, TextureRegion>();
            _animations = new Dictionary<string, Animation>();
        }

        // Creates a new texture atlas instance using the given texture.
        // param name="texture" - The source texture represented by the texture atlas.
        public TextureAtlas(Texture2D texture)
        {
            Texture = texture;
            _regions = new Dictionary<string, TextureRegion>();
            _animations = new Dictionary<string, Animation>();
        }

        // Creates a new region and adds it to this texture atlas.
        // param name="name" - The name to give the texture region.
        // param name="x" - The top-left x-coordinate position of the region boundary relative to the top-left corner of the source texture boundary.
        // param name="y" - The top-left y-coordinate position of the region boundary relative to the top-left corner of the source texture boundary.
        // param name="width" - The width, in pixels, of the region.
        // param name="height" - The height, in pixels, of the region
        public void AddRegion(string name, int x, int y, int width, int height)
        {
            TextureRegion region = new TextureRegion(Texture, x, y, width, height);
            _regions.Add(name, region);
        }

        // Gets the region from this texture atlas with the specified name.
        public TextureRegion GetRegion(string name)
        {
            return _regions[name];
        }

        // Removes the region from this texture atlas with the specified name.
        // param name="name" - The name of the region to remove.
        public bool RemoveRegion(string name)
        {
            return _regions.Remove(name);
        }

        // Removes all regions from this texture atlas.
        public void Clear()
        {
            _regions.Clear();
        }

        // Creates a new texture atlas based a texture atlas xml configuration file.
        // param name="content" - The content manager used to load the texture for the atlas.
        // param name="fileName" - The path to the xml file, relative to the content root directory.
        // returns - The texture atlas created by this method.
        public static TextureAtlas FromFile(ContentManager content, string filename)
        {
            TextureAtlas atlas = new TextureAtlas();

            string filePath = Path.Combine(content.RootDirectory, filename);

            using (Stream stream = TitleContainer.OpenStream(filePath))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;

                    // The <Texture> element contains the content path for the Texture2D to load.
                    // So we will retrieve that value then use the content manager to load the texture.
                    string texturePath = root.Element("Texture")?.Value;
                    
                    atlas.Texture = content.Load<Texture2D>(texturePath);

                    // The <Regions> element contains individual <Region> elements, each one describing
                    // a different texture region within the atlas.
                    //
                    // Example:
                    // <Regions>
                    //    <Region name="spriteOne" x="0" y="0" width="32" height="32"/>
                    //    <Region name="spriteTwo" x="32" y="0" width="32" height="32"/>
                    // </Regions>
                    //
                    // So we retrieve all of the <Region> elements then loop throught each one
                    // and generate a new TextureRegion instance from it and add it to this atlas.
                    var regions = root.Element("Regions")?.Elements("Region");

                    if (regions != null)
                    {
                        foreach (var region in regions)
                        {
                            string name = region.Attribute("name")?.Value;
                            int x = int.Parse(region.Attribute("x")?.Value ?? "0");
                            int y = int.Parse(region.Attribute("y")?.Value ?? "0");
                            int width = int.Parse(region.Attribute("width")?.Value ?? "0");
                            int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                atlas.AddRegion(name, x, y, width, height);
                            }
                        }
                    }

                    // The <Animations> element contains individual <Animation> elements, each one describing
                    // a different animation within the atlas.
                    //
                    // Example:
                    // <Animations>
                    //      <Animation name="animation" delay="100">
                    //          <Frame region="spriteOne" />
                    //          <Frame region="spriteTwo" />
                    //      </Animation>
                    //
                    // So we retrieve all of the <Animation> elements then loop through each one
                    // and generate a new TextureRegion instance from it and add it to this atlas.
                    var animationElements = root.Element("Animations").Elements("Animation");

                    if (animationElements != null)
                    {
                        foreach (var animationElement in animationElements)
                        {
                            string name = animationElement.Attribute("name")?.Value;
                            float delayInMilliseconds = float.Parse(animationElement.Attribute("delay").Value ?? "0");
                            TimeSpan delay = TimeSpan.FromMilliseconds(delayInMilliseconds);

                            List<TextureRegion> frames = new List<TextureRegion>();

                            var frameElements = animationElement.Elements("Frame");

                            if (frameElements != null)
                            {
                                foreach (var frameElement in frameElements)
                                {
                                    string regionName = frameElement.Attribute("region").Value;
                                    TextureRegion region = atlas.GetRegion(regionName);
                                    frames.Add(region);
                                }
                            }

                            Animation animation = new Animation(frames, delay);
                            atlas.AddAnimation(name, animation);
                        }
                    }

                    return atlas;
                }
            }
        }

        // Creates a new sprite using the region from this texture atlas with the specified name.
        // param name="regionName" - The name of the region to create the sprite with.
        // returns - A new Sprite using the texture region with the specified name.
        public Sprite CreateSprite(string regionName)
        {
            TextureRegion region = GetRegion(regionName);
            return new Sprite(region);
        }

        // Adds the given animation to this texture atlas with the specified name.
        // param name="animationName" - The name of the animation to add.
        // param name="animation" - The animation to add.
        public void AddAnimation(string animationName, Animation animation)
        {
            _animations.Add(animationName, animation);
        }

        // Gets the animation from this texture atlas with the specified name.
        // param name="animationName" - The name of the animation to retrieve.
        // returns - The animation with the specified name.
        public Animation GetAnimation(string animationName)
        {
            return _animations[animationName];
        }

        // Removes the animation with the specified name from this texture atlas.
        // param name="animationName" - The name of the animation to remove.
        // returns - true if the animation is removed successfully; otherwise, false.
        public bool RemoveAnimation(string animationName)
        {
            return _animations.Remove(animationName);
        }

        // Creates a new animated sprite using the animation from this texture atlas with the specified name.
        // param name="animationName" - The name of the animation to use.
        // returns - A new AnimatedSprite using the animation with the specified name.
        public AnimatedSprite CreateAnimatedSprite(string animationName)
        {
            Animation animation = GetAnimation(animationName);
            return new AnimatedSprite(animation);
        }
    }
}
