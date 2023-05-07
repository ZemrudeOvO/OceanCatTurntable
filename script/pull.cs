using Godot;
using System;

public partial class pull : Node2D
{
	float targetSpeed = 0.0f;
	float currectSpeed = 0.0f;
	float spd_0 = 0.0f;
	[Export]
	public float spd_1 = 0.3f;
	[Export]
	public float factor = 0.01f;
	int currectAngle;
	string multiple;

    Button btnRoll;
	Sprite2D leaf;
	Label label;
	VSlider vSliderSpd1;
	VSlider vSliderDumping;

    public override void _EnterTree()
    {
		btnRoll = GetNode<Button>("btnRoll");
		leaf = GetNode<Sprite2D>("Leaf");
		label = GetNode<Label>("Label");
		vSliderSpd1 = GetNode<VSlider>("VSlider_spd1");
		vSliderDumping = GetNode<VSlider>("VSlider_dumping");
    }

    public override void _Ready()
	{
		btnRoll.Text = "Roll";

		label.Visible = false;
		vSliderSpd1.Visible = false;
		vSliderDumping.Visible = false;

		btnRoll.Connect("toggled",new Callable(this,"LeafRotation"));
		vSliderSpd1.Connect("value_changed",new Callable(this,"VSliderSpd1ValueChanged"));
		vSliderDumping.Connect("value_changed",new Callable(this,"VSLiderDumpingValueChanged"));
	}

	public override void _Process(float delta)
	{
        currectSpeed = Mathf.Lerp(currectSpeed, targetSpeed, factor);
        leaf.Rotate(currectSpeed);

       if (currectSpeed < 0.001)
        {
            currectSpeed = 0;
        }

        currectAngle = Mathf.RoundToInt(leaf.RotationDegrees % 360);
		if (currectAngle < 30 || currectAngle > 270) { multiple = "ALL"; }
		else if (currectAngle < 90) { multiple = "x2"; }
		else if (currectAngle < 150) { multiple = "x4"; }
		else if (currectAngle < 210) { multiple = "x5"; }
		else if (currectAngle < 270) { multiple = "x3"; }
		else if (currectAngle < 270) { multiple = "x1"; }

		label.Text = "Angle: " + currectAngle.ToString() + "\n" + "Multiple: " + multiple;
	}

	public void LeafRotation(bool buttonPressed)
	{
		if (buttonPressed)
		{
			targetSpeed = spd_1;
			btnRoll.Text = "Stop";
		}
		else
	    {
		    targetSpeed = spd_0;
			btnRoll.Text = "Roll";
		}
    }

	public void VSliderSpd1ValueChanged(float value)
	{
		spd_1 = value;
		currectSpeed = value;
    }

	public void VSLiderDumpingValueChanged(float value)
	{
		factor = value;
    }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey inputEventKey)
		{
			if (inputEventKey.Keycode == (int)KeyList.Escape )
			{ 
				GetTree().Quit();
			}
		}

		if (@event.IsActionPressed("Display"))
		{
			label.Visible = !label.Visible;
			vSliderSpd1.Visible = !vSliderSpd1.Visible;
			vSliderDumping.Visible = !vSliderDumping.Visible;
		}

		if (@event.IsActionPressed("Reset"))
		{
			vSliderSpd1.Value = 0.3f;
			vSliderDumping.Value = 0.01f;
		}

	}
}