using Godot;

namespace LudumDare51.GameScenes
{
	public class Title : Control
	{
		private AudioStreamPlayer _titleSound;

		public override void _Ready()
		{
			_titleSound = GetNode<AudioStreamPlayer>("%TitleSound");
		}

		private void OnTitleSoundDelayTimeout()
		{
			_titleSound.Play();
		}
	}
}
