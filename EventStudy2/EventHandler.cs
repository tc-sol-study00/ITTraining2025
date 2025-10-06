using Microsoft.VisualBasic.Logging;

namespace EventStudy2 {
    public class EventHandler {

        public event Action ActionEvent = delegate { };

        public void OnActionEvent() {
            ActionEvent?.Invoke();
            ActionEvent = delegate { };
        }
        public void SetActionEvent(Action action) {
            ActionEvent += action;
        }
    }

    public class  Log {
        public void OutputLog(string message) {
            Console.WriteLine("{message}が開始された");
        }

    }
}
