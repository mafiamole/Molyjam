using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
    public struct CollisionInDirection
    {
        public bool upColliding;
        public bool downColliding;
        public bool leftColliding;
        public bool rightColliding;
    }

	public class Player
	{
		
		private Vector2 			screenPosition, mapPosition, origin;
		AnimatedSprite				sprite;
		Sprite						overlaySprite;
		int							direction;
		bool						jumping = false;
        bool                        canJump = true;
		int							jumpStamp;
		const int					jumpTime = 35;
        public CollisionInDirection collisionInDirection = new CollisionInDirection();

		Game1						game; // reference to the game.
		
		/// <summary>
		/// Initializes a new instance of the <see cref="RenderTarget2DSample.Player"/> class.
		/// </summary>
		/// <param name='game'>
		/// Game.
		/// </param>
        /// 


		public Player (Game1 game)
		{
			this.game = game;
			screenPosition.X = 0;
			screenPosition.Y = 0;
			mapPosition.X = 0;
			mapPosition.Y = 0;
			direction = 0;
		}
			
        public bool Jumping{ get { return jumping; } set { jumping = value; }}
        public bool CanJump { get { return canJump; } set { canJump = value; } }
        
        /// <summary>
		/// Gets the position.
		/// </summary>
		/// <value>
		/// The position.
		/// </value>
        ///
		public Vector2 Position {
			get {
				return sprite.GetPosition; // SetPosition is wanged	
			}
		}
		
		/// <summary>
		/// Gets the map location.
		/// </summary>
		/// <value>
		/// The map location.
		/// </value>
		public Vector2 MapLocation {
		
			get {
			
				return mapPosition;
				
			}
		}
		
		/// <summary>
		/// Gets the offset.
		/// </summary>
		/// <value>
		/// The offset.
		/// </value>
		public Vector2 Offset {
		
			get {
				
				return screenPosition;
				
			}
			
		}
		
		/// <summary>
		/// Initalise the specified game, playerTexture, spriteBatch and screenPosition.
		/// </summary>
		/// <param name='game'>
		/// Game.
		/// </param>
		/// <param name='playerTexture'>
		/// Player texture.
		/// </param>
		/// <param name='spriteBatch'>
		/// Sprite batch.
		/// </param>
		/// <param name='screenPosition'>
		/// Screen position.
		/// </param>
		public void Initalise (Game1 game, Texture2D playerTexture, SpriteBatch spriteBatch, Vector2 screenPosition)
		{
			this.origin = new Vector2 (playerTexture.Width / 2, playerTexture.Height / 2);
			this.screenPosition = screenPosition;
			this.sprite = new AnimatedSprite (game, playerTexture, spriteBatch, screenPosition, MapLoader.TileType.Player);
			this.overlaySprite = new Sprite (game, playerTexture, spriteBatch, screenPosition, MapLoader.TileType.Glasses);
		}
		
		/// <summary>
		/// Update the specified gameTime, changeVector and jump.
		/// </summary>
		/// <param name='gameTime'>
		/// Game time.
		/// </param>
		/// <param name='changeVector'>
		/// Change vector.
		/// </param>
		/// <param name='jump'>
		/// Jump.
		/// </param>
		public void Update (GameTime gameTime, Vector2 changeVector, bool jump)
		{
            if (jumping)
            {
                changeVector.Y -= gameTime.ElapsedGameTime.Milliseconds * 0.6f; 
                if (changeVector.Y <= -12) { jumping = false; }
            }

            if (!collisionInDirection.downColliding)
            {
                changeVector.Y += gameTime.ElapsedGameTime.Milliseconds * 0.2f;
            }
              

			mapPosition += changeVector;
            
            if (changeVector.X > 0 || collisionInDirection.leftColliding)
            {

                sprite.flippedHorizonally = true;
                overlaySprite.flippedHorizonally = true;
                direction = 1;
            }
            else
            {
                sprite.flippedHorizonally = false;
                overlaySprite.flippedHorizonally = false;
                direction = -1;
            }            

            sprite.Update (gameTime, changeVector);
            overlaySprite.Update (gameTime, changeVector);
		}
		
		/// <summary>
		/// Draw the specified gameTime and spriteBatch.
		/// </summary>
		/// <param name='gameTime'>
		/// Game time.
		/// </param>
		/// <param name='spriteBatch'>
		/// Sprite batch.
		/// </param>
		public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
		{
			sprite.Draw (gameTime);
			overlaySprite.Draw (gameTime);
		}
		
	}
}
