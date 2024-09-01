using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.FX
{
    public class BackgroundColorsChanger : MonoBehaviour
    {
        [SerializeField] private Material _shader;
        [SerializeField] private List<Color> _colors = new List<Color>();
        [SerializeField] private  Color _BossRedColor;
        
        private static readonly int TintColor = Shader.PropertyToID("_TintColor");
        private Color _startColor;
        
        private void Start() {
            _shader.SetColor(TintColor, SetRandomColor());
            _startColor = _shader.GetColor(TintColor);
        }
        
        public void BossStageBackgroundColor(bool value) => LerpColor(value);

        private void LerpColor(bool isBossStage) => _shader.DOColor(isBossStage ? 
            _BossRedColor : _startColor, TintColor, 2f);

        private Color SetRandomColor() => _colors[Random.Range(0, _colors.Count)];
    }
}