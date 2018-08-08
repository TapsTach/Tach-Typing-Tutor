using CoreExtLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class Keyboard : UserControl
    {
        KeyboardButton highlightedButt;
        List<KeyboardButton> buttons = new List<KeyboardButton>();
        public Keyboard()
        {
            InitializeComponent();

            KeyboardButton[] btns = mainGrid.Children.OfType<KeyboardButton>().ToArray();
            StackPanel[] stkps = mainGrid.Children.OfType<StackPanel>().ToArray();

            List<KeyboardButton> butList = new List<KeyboardButton>();
            foreach (var stk in stkps)
                butList.AddRange(stk.Children.OfType<KeyboardButton>());

            buttons.AddRange(btns);
            buttons.AddRange(butList);
        }
        public void HighlightKeys(params string[] keys)
        {
            if(keys == null)
            {
                foreach (var button in buttons)
                    button.Highlighted = false;
                return;
            }
            foreach(var key in keys)
            {
                Highlight(key);
            }
        }
        public void Highlight(string s)
        {
            highlightedButt = buttons.Where(b => b.Name == s.ToLower()).FirstOrDefault();

            if (highlightedButt == null)
            {
                if (s.IsInt() && int.Parse(s) < 10 && int.Parse(s) >= 0)
                {
                    highlightedButt = buttons.Where(b => b.Name == $"d{s}").FirstOrDefault();
                }
                else switch (s)
                    {
                        case " ":
                        case "SPACE":
                            highlightedButt = space;
                            break;
                        case "\n":
                        case "\n\r":
                        case "ENTER":
                        case "\r":
                            highlightedButt = enter;
                            break;
                        case ",":
                        case "<":
                            highlightedButt = lt;
                            break;
                        case ">":
                        case ".":
                            highlightedButt = gt;
                            break;
                        case "/":
                        case "?":
                            highlightedButt = quest;
                            break;
                        case ":":
                        case ";":
                            highlightedButt = colen;
                            break;
                        case "\"":
                        case "'":
                            highlightedButt = quot;
                            break;
                        case "\\":
                        case "|":
                            highlightedButt = bSlash;
                            break;
                        case "{":
                        case "[":
                            highlightedButt = oBrac;
                            break;
                        case "}":
                        case "]":
                            highlightedButt = cBrac;
                            break;
                        case "\b":
                        case "BACKSPACE":
                            highlightedButt = back;
                            break;

                    }
            }

            if (highlightedButt != null)
                highlightedButt.Highlighted = true;
        }
        public void StopHighlight()
        {
            if (highlightedButt != null)
                highlightedButt.Highlighted = false;
        }

        public void HighlightShiftKey(string shiftKey)
        {
            switch (shiftKey)
            {
                case "RIGHTSHIFT":
                    rShift.Highlighted = true;
                    break;
                case "LEFTSHIFT":
                    lShift.Highlighted = true;
                    break;
                case null:
                    lShift.Highlighted = rShift.Highlighted = false;
                    break;
            }
        }
    }
}
