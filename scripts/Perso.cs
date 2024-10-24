using Godot;
using System;

public partial class Perso : CharacterBody2D
{

	// private Vector2 WalkVelocity = Vector2.Zero;
	
	[Export]
	public float DefaultSpeed = 200.0f;
	[Export]
	public float MaxSpeed = 400.0f;
	
	[Export]
	public float JumpVelocity = -450f;

	// Variable pour gérer le double saut
	private int jumpCount = 0;
	
	[Export]
	public int maxJumps = 2;

	[Export] 
	public float HAcceleration = 5f;
	
	[Export]
	public float HDeceleration = 40f;
	
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		bool canDash = false;
		

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Réinitialiser le compteur de sauts si le joueur est au sol
		if (IsOnFloor())
		{
			jumpCount = 0;  // On est au sol, donc on réinitialise les sauts
		}

		// Handle Jump (saut ou double saut)
		if (Input.IsActionJustPressed("saut") && jumpCount < maxJumps)
		{
			velocity.Y = JumpVelocity;
			jumpCount++;  // Incrémente le nombre de sauts
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			
			velocity.X = direction.X * DefaultSpeed;

			// velocity.X += HAcceleration * direction.X;


			if (Mathf.Abs(velocity.X) < DefaultSpeed)
				velocity.X = DefaultSpeed * direction.X;
			else if (Mathf.Abs(velocity.X) > MaxSpeed)
				velocity.X = MaxSpeed * direction.X;

		}
		else
		{
			// velocity.X = Mathf.MoveToward(Velocity.X, 0, HDeceleration);
			velocity.X = Mathf.MoveToward(Velocity.X, 0, DefaultSpeed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
