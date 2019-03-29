using System;
using UnityEngine;

namespace BBX.Characters
{
    public class SelectionHandler
    {
        private Settings _settings;
        private PlayerModel _player;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="player"></param>
        public SelectionHandler(
            Settings settings,
            PlayerModel player)
        {
            _settings = settings;
            _player = player;
        }


        /// <summary>
        /// When selection has started
        /// </summary>
        public void StartedSelecting()
        {
            if (_player.Selecting) return;
            _player.Selecting = true;

            if (_player.Selected) return;
            _player.SelectionColor = _settings.selectingColor;
        }


        /// <summary>
        /// When selection has stopped
        /// </summary>
        public void StoppedSelecting()
        {
            _player.Selecting = false;
            _player.SelectionColor = _player.Selected ? _settings.selectedColor : _settings.normalColor;
        }


        /// <summary>
        /// When a player is selected
        /// </summary>
        public void Select()
        {
            _player.Selecting = false;

            if (_player.Selected) return;

            _player.Selected = true;
            _player.SelectionColor = _settings.selectedColor;
        }


        /// <summary>
        /// When a player is deselected
        /// </summary>
        public void Deselect()
        {
            _player.Selecting = false;

            if (!_player.Selected) return;

            _player.Selected = false;
            _player.SelectionColor = _settings.normalColor;
        }


        [Serializable]
        public class Settings
        {
            public Color normalColor;
            public Color selectingColor;
            public Color selectedColor;
        }
    }
}