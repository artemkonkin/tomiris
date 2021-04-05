using System.Diagnostics;

namespace tomiris.utils
{
    public class DgClass
    {
        public void DgMessage(string message)
        {
            Debug.WriteLine($"Debug: {message}");
        }
    }
}