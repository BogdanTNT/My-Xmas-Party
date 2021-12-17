using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games
{
    public class Formation : MonoBehaviour
    {
        [SerializeField] private int _unitWidth = 5;
        [SerializeField] private int _unitDepth = 5;
        [SerializeField] private bool _hollow = false;
        [SerializeField] private float _nthOffset = 0;

        [SerializeField] [Range(0, 1)] protected float _noise = 0;
        [SerializeField] protected float Spread = 1;

        private Vector3 GetNoise(Vector3 pos)
        {
            var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);

            return new Vector3(noise, 0, noise);
        }

        public IEnumerable<Vector3> EvaluatePoints()
        {
            int nr = O.Player.AllPlayers().Count;
            _unitWidth = ((int)Mathf.Sqrt(nr)) + 1;
            _unitDepth = _unitWidth;

            var middleOffset = new Vector3(_unitWidth * 0.5f, 0, _unitDepth * 0.5f);

            for (var x = 0; x < _unitWidth; x++)
            {
                for (var z = 0; z < _unitDepth; z++)
                {
                    if (_hollow && x != 0 && x != _unitWidth - 1 && z != 0 && z != _unitDepth - 1) continue;
                    var pos = new Vector3(x + (z % 2 == 0 ? 0 : _nthOffset), 0, z);

                    pos -= middleOffset;

                    pos += GetNoise(pos);

                    pos *= Spread;

                    yield return pos;
                }
            }
        }
    }


}
