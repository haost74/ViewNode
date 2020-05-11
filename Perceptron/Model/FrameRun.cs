using PerceptronLib.Nodes;

namespace Perceptron.Model
{
    public class FrameRun
    {
        public FrameRun(Init init)
        {

            if (init.matrixs != null && init.matrixs.Row > 0 && init.matrixs.Column > 0 && init.ResList.Count == init.matrixs.Row)
            {
                for (int i = 0; i < init.matrixs.Row; ++i)
                {
                    ViewNode el = init.matrixs[i, 0];
                    el.Value = (double)init.ResList[i].param;
                }
            }

        }
    }
}
