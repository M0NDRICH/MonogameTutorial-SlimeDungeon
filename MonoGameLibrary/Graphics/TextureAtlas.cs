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
            Debug.WriteLine(filePath);
            Console.WriteLine($"filepath: {filePath}");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find XML file at: " + filePath);
            }


            using (Stream stream = TitleContainer.OpenStream(filePath))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;

                    // The <Texture> element contains the content path for the Texture2D to load.
                    // So we will retrieve that value then use the content manager to load the texture.
                    string texturePath = root.Element("Texture")?.Value ?? throw new Exception($"<Texture> element missing from atlas XML. filepath: {filePath}");;
                    if (texturePath == null)
                    {
                        throw new Exception("Missing <Texture> element in XML file.");
                    }
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

                    return atlas;
                }
            }
        }
    }
}
