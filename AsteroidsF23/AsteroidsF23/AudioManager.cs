using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class AudioManager
    {
        private Dictionary<string, SoundPlayer> sounds;

        //Build sound dictionary
        public AudioManager() 
        {
            sounds = new Dictionary<string, SoundPlayer> ();

            LoadSound("shoot", "assets/shoot.wav");
            LoadSound("explode", "assets/explode.wav");
        }

        //Load sounds into the player
        public void LoadSound(string key, string path)
        {
            SoundPlayer player = new SoundPlayer(path);
            player.Load();
            sounds[key] = player;
        }

        //Play soud
        public void Play(string key)
        {
            if (sounds.ContainsKey(key))
            {
                sounds[key].Play(); // Non-blocking play
            }
        }
    }
}
