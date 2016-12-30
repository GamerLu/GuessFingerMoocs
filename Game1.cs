using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GuessFingerMoocs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // ���C�i�Ϥ��ŧi�@���ܼ�
        Texture2D image1, image2, image3, imageGameStart, imageBackgroud;

        // �������a�X���A0=��l�ȡA1=�ŤM�A2=���Y�A3=��
        int player = 0;
        // ���������A0=�}�l�e���A1=�X���e��
        int gameState = 0;
        // �������a�X���A0=��l�ȡA1=�ŤM�A2=���Y�A3=��
        int computer = 0;
        // �ŧi�H�����ܼơA����A���ͼƦr
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // �]�w�_�l�e���j�p
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // ���J�ܼơA�ù����C�ӹϤ��ܼ�
            image1 = Content.Load<Texture2D>("Scissor400x600");
            image2 = Content.Load<Texture2D>("Rock400x600");
            image3 = Content.Load<Texture2D>("Paper400x600");
            imageGameStart = Content.Load<Texture2D>("GameStart1024x768");
            imageBackgroud = Content.Load<Texture2D>("background1024x768");   
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            // ���o��L���U�����A
            KeyboardState newState = Keyboard.GetState();
            // ���UESC �i�H���}�C��
            if (newState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            // ���U�Ʀr��1�A�⪱�a�X�����ܼƭȭקאּ1�A�N��ŤM
            if (newState.IsKeyDown(Keys.D1))
            {
                player = 1;
            }
            // ���U�Ʀr��1�A�⪱�a�X�����ܼƭȭקאּ2�A�N����Y
            if (newState.IsKeyDown(Keys.D2))
            {
                player = 2;
            }
            // ���U�Ʀr��1�A�⪱�a�X�����ܼƭȭקאּ3�A�N��
            if (newState.IsKeyDown(Keys.D3))
            {
                player = 3;
            }

            if (newState.IsKeyDown(Keys.D1) || newState.IsKeyDown(Keys.D2) || newState.IsKeyDown(Keys.D3))
            {
                gameState = 1;
            }

            // �H������1-3�Ʀr
            computer = random.Next(1, 4);

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // sprite.draw ���ϥΤ�k�ܦh�ءA�o�̸��J���u��3�ӰѼ�
            // �Ѽ�1��ܭn��ܪ��Ϥ��A�Ѽ�2�����ܹϤ����_�l��m�A�Ѽ�3��ܶ��AWhite��ܤ����
            // �쥻ø�X�}�l�e���]�n���A�u�����٨S�X���ɡA�~�|ø�s�}�l�e��
            if (gameState == 0)
            {
                spriteBatch.Draw(imageGameStart, Vector2.Zero, Color.White);
            }
            if (gameState == 1)
            {
                // ø�s�X���᪺�C���I��
                spriteBatch.Draw(imageBackgroud, Vector2.Zero, Color.White);
                // ø�s���a�X�����K��
                if (player == 1)
                {
                    spriteBatch.Draw(image1, new Vector2(550,100), Color.White);
                }
                if (player == 2)
                {
                    spriteBatch.Draw(image2, new Vector2(550, 100), Color.White);
                }
                if (player == 3)
                {
                    spriteBatch.Draw(image3, new Vector2(550, 100), Color.White);
                }


                // ø�s�q���X�����K��
                if (computer == 1)
                {
                    spriteBatch.Draw(image1, new Vector2(50, 100), Color.White);
                }
                if (computer == 2)
                {
                    spriteBatch.Draw(image2, new Vector2(50, 100), Color.White);
                }
                if (computer == 3)
                {
                    spriteBatch.Draw(image3, new Vector2(50, 100), Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
