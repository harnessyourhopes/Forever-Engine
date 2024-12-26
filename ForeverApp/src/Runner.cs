using System.Reflection;
using System.Windows.Forms;

namespace ForeverApp {
    public class Program {
        public static void Main(string[] args) {
            using(Application application = new Application(1280, 720, "Forever Creator")) {
                application.Run();
            }
        }
    }
}   