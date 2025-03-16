using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.States;
using Microsoft.Xna.Framework.Media;

namespace SpooninDrawer.Engine.Sound
{
    public enum VolumeType
    {
        SE,
        BGM
    }
    public class SoundManager
    {
        private int _soundtrackIndex = -1;
        private List<SoundEffectInstance> _soundtracks = new List<SoundEffectInstance>();
        private Dictionary<Type, SoundBankItem> _soundBank = new Dictionary<Type, SoundBankItem>();
        private float volumeBGM;
        private float volumeSE;

        private float SEtimeRecorder;
        private bool SEtimeRecorded = false;

        public void SetSoundtrack(List<SoundEffectInstance> tracks)
        {
            _soundtracks = tracks;
            _soundtrackIndex = _soundtracks.Count - 1;
            ChangeVolumeBGM(volumeBGM);
        }
        public void ChangeVolumeBGM(float volumeBGM)
        {
            this.volumeBGM = volumeBGM;
            foreach(SoundEffectInstance soundtrack in _soundtracks)
            {
                soundtrack.Volume = volumeBGM;
            }
        }
        public void ChangeVolumeSE(float volumeSE)
        {
            this.volumeSE = volumeSE;
            foreach (KeyValuePair<Type, SoundBankItem> sounditem in _soundBank)
            {
                sounditem.Value.Attributes.Volume = volumeSE;
            }
        }
        public float GetVolumeBGM()
        {            
            return volumeBGM;
        }
        public float GetVolumeSE()
        {
            return volumeSE;
        }
        public void OnNotify(BaseGameStateEvent gameEvent)
        {
            if (_soundBank.ContainsKey(gameEvent.GetType()))
            {
                var sound = _soundBank[gameEvent.GetType()];
                sound.Sound.Play(sound.Attributes.Volume, sound.Attributes.Pitch, sound.Attributes.Pan);
            }
        }

        public void PlaySoundtrack()
        {
            var nbTracks = _soundtracks.Count;

            if (nbTracks <= 0)
            {
                return;
            }

            var currentTrack = _soundtracks[_soundtrackIndex];
            var nextTrack = _soundtracks[(_soundtrackIndex + 1) % nbTracks];           
            if (currentTrack.State == SoundState.Stopped)
            {
                nextTrack.Play();
                _soundtrackIndex++;

                if (_soundtrackIndex >= _soundtracks.Count)
                {
                    _soundtrackIndex = 0;
                }
            }
        }
        public void UnloadAllSound()
        {
            _soundtracks.Clear();
            _soundBank.Clear();
        }
        public void RegisterSound(BaseGameStateEvent gameEvent, SoundEffect sound)
        {
            RegisterSound(gameEvent, sound, volumeSE, 0.0f, 0.0f);
        }

        internal void RegisterSound(BaseGameStateEvent gameEvent, SoundEffect sound,
                                    float volumeSE, float pitch, float pan)
        {
            _soundBank.Add(gameEvent.GetType(), new SoundBankItem(sound, new SoundAttributes(volumeSE, pitch, pan)));
        }
    }
}
