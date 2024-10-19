using Godot;
using System;

public partial class Perso : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450f;

	// Variable pour gérer le double saut
	private int jumpCount = 0;
	private const int maxJumps = 2;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

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
		if (Input.IsActionJustPressed("ui_accept") && jumpCount < maxJumps)
		{
			velocity.Y = JumpVelocity;
			jumpCount++;  // Incrémente le nombre de sauts
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
