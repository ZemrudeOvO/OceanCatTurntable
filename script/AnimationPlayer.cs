using Godot;
using System;

public partial class AnimationPlayer : Godot.AnimationPlayer
{
    bool isAllDisplay = true;
	bool isHelpDisplayed = false;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("allFade"))
        { 
            if (isAllDisplay)
            {
                isAllDisplay = !isAllDisplay;
                this.Play("allFadeIn");
	        }
            else
            { 
                isAllDisplay = !isAllDisplay;
                this.Play("allFadeOut");
	        }
	    }

		if (@event.IsActionPressed("Help"))
		{
            if (this.IsPlaying() == false)
            { 
                if (isHelpDisplayed)
                {
                    this.Play("HelpFadeOut");
                    isHelpDisplayed = !isHelpDisplayed;
	            }
                else
                {
                    this.Play("HelpFadeIn");
                    isHelpDisplayed = !isHelpDisplayed;
	            }
	        }
		}
    }
}
