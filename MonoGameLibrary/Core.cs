using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Audio;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary
{
    public class Core : Game
    {
        internal static Core s_instance;

        // Gets a reference to the Core instance
        public static Core Instance => s_instance;

        //Gets the graphics devive manager to control the presentation of graphics.
        public static GraphicsDeviceManager Graphics { get; private set; }

        // Gets the graphics device used to create graphical resources and perform primitive rendering.
        public static new GraphicsDevice GraphicsDevice { get; private set; }

        // Gets the sprite batch used for all 2D rendering.
        public static SpriteBatch SpriteBatch { get; private set; }

        // Gets the content manager used to load global assets.
        public static new ContentManager Content { get; private set; }

        /// <summary>
        /// Gets a reference to the input management system.
        /// </summary>
        public static InputManager Input { get; private set; }

        /// <summary>
        /// Gets or Sets a value that indicates if the game should exit when the esc key on the keyboard is pressed.
        /// </summary>
        public static bool ExitOnEscape { get; set; }

        /// <summary>
        /// Gets a reference to the audio control system.
        /// </summary>
        public static AudioController Audio { get; private set; }

        /*
         * Creates a new Core instance.
         * param name = "title" - The title to display in the title bar of the game window.
         * param name = "width" - The initial width, in pixels, of the game window.
         * param name = "height" - The initial height, in pixels, of the game window.
         * param name = "fullscreen" - Indicateds if the game should start in fullscreen mode
        */
        public Core(string title, int width, int height, bool fullScreen)
        {
            // Ensure that multiple cores are not created.
            if (s_instance != null)
            {
                throw new InvalidOperationException($"Only a single Core instance can be created");
            }

            // Store reference to engine for global member access.
            s_instance = this;

            // Create a new graphics device manager.
            Graphics = new GraphicsDeviceManager(this);

            // Set the graphics defaults.
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.IsFullScreen = fullScreen;

            // Apply the graphic presentation changes.
            Graphics.ApplyChanges();

            // Set the window title.
            Window.Title = title;

            // Set the core's content manager to a reference of the Game's
            // content manager.
            Content = base.Content;

            // Set the root directory for content.
            Content.RootDirectory = "Content";

            // Mouse is visible by defaul.
            IsMouseVisible = true;

            // Exit on escape is true by default
            ExitOnEscape = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Set the core's graphics device to a reference of the Game's
            // graphics device.
            GraphicsDevice = base.GraphicsDevice;

            // Create the sprite batch instance.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a new input manager.
            Input = new InputManager();

            // Create a new audio controller.
            Audio = new AudioController();
        }

        protected override void UnloadContent()
        {
            // Dispose of the audio controller.
            Audio.Dispose();

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update the input manager.
            Input.Update(gameTime);

            // Update the audio controller.
            Audio.Update();

            if (ExitOnEscape && Input.Keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }
    }
}

