using OpenTK.Mathematics;

namespace ForeverEngine.Graphics.Lighting {
    public class DirectionalLight : Light
    {
        private Quaternion _orientation;

        public void SetOrientation(Quaternion orientation)
        {
            _orientation = orientation;
        }
    }
}