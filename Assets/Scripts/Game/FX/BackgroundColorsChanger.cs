using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FX
{
    public class BackgroundColorsChanger : MonoBehaviour
    {
        [SerializeField] private Material _shader;
        [SerializeField] private List<Color> _colors = new List<Color>();
        [SerializeField] private  Color _BossRedColor;
        private static readonly int TintColor = Shader.PropertyToID("_TintColor");
        private Coroutine _colorCoroutine;
        private Color _startColor;
        private float _timer;
        private void Start() {
            _shader.SetColor(TintColor, SetRandomColor());
            _startColor = _shader.GetColor(TintColor);
        }
        public void BossStateChangeColor(bool value) => StartCoroutine(LerpColorToBossState(value));

        private IEnumerator LerpColorToBossState(bool value) {
            _timer = 0f;
            while (_timer <=2f) {
                _shader.SetColor(TintColor, value
                    ? Color.Lerp(_shader.GetColor(TintColor), _BossRedColor, 2f * Time.deltaTime)
                    : Color.Lerp(_shader.GetColor(TintColor), _startColor, 2f * Time.deltaTime));
                _timer += Time.deltaTime;
                yield return null;
            }
        }
        private Color SetRandomColor() => _colors[Random.Range(0, _colors.Count)];
    }
}