using OpenTK.Mathematics;

namespace ForeverEngine.Graphics.Lighting {
    public abstract class Light {
        private Color4 _color;
        private float _brightness;

        public void SetColor(Color4 color)
        {
            _color = color;
        }

        public Color4 GetColor()
        {
            return _color;
        }

        public void SetBrightness(float brightness)
        {
            _brightness = brightness;
        }
        
        public float GetBrightness()
        {
            return _brightness;
        }
    }
}