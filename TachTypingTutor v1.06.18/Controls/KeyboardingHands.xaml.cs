using CoreExtLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for KeyboardingHands.xaml
    /// </summary>
    public partial class KeyboardingHands : UserControl
    {
        //UserControl lastHand;
        //
        //DispatcherTimer timer;
        //

        //#region TypedKeyProperty

        //public string TypedKey
        //{
        //    get { return (string)GetValue(TypedKeyProperty); }
        //    set { SetValue(TypedKeyProperty, value); }
        //}

        //public static readonly DependencyProperty TypedKeyProperty =
        //    DependencyProperty.Register("TypedKey", typeof(string), typeof(KeyboardingHands), new PropertyMetadata("", (sender, e) =>
        //    {
        //        var kh = sender as KeyboardingHands;
        //        string value = e.NewValue as string;
        //        kh.Press(value);
        //        if (kh.timer.IsEnabled == false)
        //            kh.timer.Start();
        //    }));

        //#endregion

        //#region pressedKeyproperty
        //public string PressedKey
        //{
        //    get { return (string)GetValue(PressedKeyProperty); }
        //    set { SetValue(PressedKeyProperty, value); }
        //}

        //public static readonly DependencyProperty PressedKeyProperty =
        //    DependencyProperty.Register("PressedKey", typeof(string), typeof(KeyboardingHands), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PressedKeyChanged));

        //private static void PressedKeyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var kp = sender as KeyboardingHands;
        //    kp.Press(e.NewValue as string);
        //}


        //#endregion
        //#region HighLightedLetterProperty
        //public string HighLightedLetter
        //{
        //    get { return (string)GetValue(HighLightedLetterProperty); }
        //    set { SetValue(HighLightedLetterProperty, value); }
        //}
        //public static readonly DependencyProperty HighLightedLetterProperty =
        //    DependencyProperty.Register("HighLightedLetter", typeof(string), typeof(KeyboardingHands), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHighlitedChanged));
        //static void OnHighlitedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var kp = sender as KeyboardingHands;
        //    string value = (string)e.NewValue;
        //    kp.Highlight(value);

        //}

        //#endregion
        //string[][] keyLetterRelation;
        //char[] barredLetters = { 'J', 'F', '5' };
        //public KeyboardingHands()
        //{
        //    InitializeComponent();
        //    keyLetterRelation = new string[][]
        //    {
        //        new string[]{"F","R","T","G","B","V","4","5" },
        //        new string[]{"D","E","C","3" },
        //        new string[]{"S","W","2","X"},
        //        new string[]{"A","Q","1","Z","LEFTSHIFT"},
        //        new string[]{"J","U","M","7","N","H","Y","6"},
        //        new string[]{"K","I","8",",","<"},
        //        new string[]{"L","O","9",".",">"},
        //        new string[]{";",":","P","/","?","[","]","{","}","\"","'","ENTER","\r","\b","+","=","_","-","BACK","RIGHTSHIFT"},

        //    };
        //    hands = new List<UserControl>()
        //    {
        //        hndA,hndB,hndC,hndD,hndE,hndF,hndG,hndH,hndI,hndJ,hndK,hndL,hndM,hndN,hndO,hndP,hndQ,hndR,hndS,hndT,hndU,hndV,hndW,hndX,hndY,hndZ
        //    };
        //    timer = new DispatcherTimer()
        //    {
        //        Interval = TimeSpan.FromMilliseconds(100)

        //    };
        //    timer.Tick += Timer_Tick;

        //    rightLetters = new string[]
        //    {
        //        " ","J","H","U","Y","N","M","I","K",",","O","L",".","P",";","/","ENTER","RIGHTSHIFT","6","7",
        //        "8","9","0","-","=","BACKSPACE", "SPACE","{","}","\\","|","[","]","\"","'",":","?",">","<"
        //    };
        //}

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    ShowHomeKeys();

        //    timer.Stop();
        //}

        //public void ShowHomeKeys()
        //{
        //    if (lastHand != null)
        //    {
        //        lastHand.Visibility = Visibility.Collapsed;
        //    }
        //    hndHomeLeft.Visibility = Visibility.Visible;
        //    hndHomeRight.Visibility = Visibility.Visible;
        //    PressedKey = null;
        //}
        //bool isShiftPressed;
        //public void Press(string key)
        //{
        //   
        //}

        //private void HighlightShift(string shiftKey = null)
        //{
        //    keyboard.HighlightShiftKey(shiftKey);
        //}


        //public void UnHighlight()
        //{

        //    hndHomeRight.Visibility = hndHomeLeft.Visibility = Visibility.Collapsed;
        //    hndHomeLeft.StopHighLight();
        //    hndHomeRight.StopHighLight();
        //    keyboard.StopHighlight();
        //}

        //public void Highlight(params string[] keys)
        //{
        //    UnHighlight();
        //    if (keys != null)
        //    {
        //        foreach (string k in keys)
        //        {
        //            string key = k.ToUpper();
        //            var arr = keyLetterRelation.Where(ar => ar.Contains(key.ToString())).FirstOrDefault();
        //            string finger = arr != null ? arr[0] : "";

        //            if (key == " ")
        //            {
        //                hndHomeRight.Highlight(key);
        //            }
        //            else
        //            {
        //                hndHomeLeft.Highlight(finger);
        //                hndHomeRight.Highlight(finger);
        //            }
        //            keyboard.Highlight(key);
        //        }
        //    }
        //}

        #region Fields
        string rightHandHomeKeys = "JKL ;";
        string leftHandHomeKeys = "ASDF";
        List<UserControl> hands;
        UserControl lastHand;
        string[] rightLetters;
        string[][] keyLetterRelation;
        bool isUpper;
        #endregion


        #region DependencyProperties
        #region HighLightedLetters

        public string HighlightedKeys
        {
            get { return (string)GetValue(HighlightedKeysProperty); }
            set { SetValue(HighlightedKeysProperty, value); }
        }

        public static readonly DependencyProperty HighlightedKeysProperty =
            DependencyProperty.Register("HighlightedKeys", typeof(string), typeof(KeyboardingHands), new PropertyMetadata(null, (d, e) =>
            {
                var kboard = d as KeyboardingHands;
                var value = (string)e.NewValue;
                kboard.keyboard.HighlightKeys(value.Split(' '));
            }));


        #endregion

        #region UnHighlight


        public bool UnHighlight
        {
            get { return (bool)GetValue(UnHighlightProperty); }
            set { SetValue(UnHighlightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnHighlight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnHighlightProperty =
            DependencyProperty.Register("UnHighlight", typeof(bool), typeof(KeyboardingHands), new PropertyMetadata(false, (d, e) =>
            {
                var value = (bool)e.NewValue;

                if (value)
                {
                    var kb = d as KeyboardingHands;
                    kb.HighlightAll(null);
                }

            }));


        #endregion
        #region Highlighted


        public string Highlighted
        {
            get { return (string)GetValue(HighlightedProperty); }
            set { SetValue(HighlightedProperty, value); }
        }

        public static readonly DependencyProperty HighlightedProperty =
            DependencyProperty.Register("Highlighted", typeof(string), typeof(KeyboardingHands), new PropertyMetadata(null, (e, d) =>
            {
                var board = e as KeyboardingHands;
                board.HighlightAll((string)d.NewValue);
            }));

        private void HighlightAll(string newValue)
        {
            if (newValue == null)
            {
                
                hndHomeLeft.StopHighlight();
                hndHomeRight.StopHighlight();
                hndHomeLeft.Visibility = Visibility.Collapsed;
                hndHomeRight.Visibility = Visibility.Collapsed;
                keyboard.HighlightKeys(null);
                return;
            }
            string[] keys = newValue.Split(' ');
            if (keys.Length == 0)
                keys = new string[] { newValue };
            foreach (var key in keys)
            {
                if (rightLetters.Contains(key.ToUpper()))
                {
                    hndHomeRight.Visibility = Visibility.Visible;
                }
                else
                {
                    hndHomeLeft.Visibility = Visibility.Visible;

                }
                keyboard.HighlightKeys(key);

                string finger = key.ToUpper();
                if (rightHandHomeKeys.Contains(finger))
                {
                    hndHomeRight.Visibility = Visibility.Visible;
                }
                else
                {
                    hndHomeLeft.Visibility = Visibility.Visible;
                }
                hndHomeRight.Highlight(finger);
                hndHomeLeft.Highlight(finger);
            }
        }


        #endregion

        #region ExpectedLetter
        public string HighLightedLetter
        {
            get { return (string)GetValue(HighLightedLetterProperty); }
            set { SetValue(HighLightedLetterProperty, value); }
        }

        public static readonly DependencyProperty HighLightedLetterProperty =
            DependencyProperty.Register("HighLightedLetter", typeof(string), typeof(KeyboardingHands), new PropertyMetadata(null, (d, e) =>
            {

                var kh = d as KeyboardingHands;
                var letter = e.NewValue as string;
                kh.Press(letter);
            }));

        #endregion.


        #endregion


        #region Constructors
        public KeyboardingHands()
        {
            InitializeComponent();
            hands = new List<UserControl>()
            {
                hndA,hndB,hndC,hndD,hndE,hndF,hndG,hndH,hndI,hndJ,hndK,
                hndL,hndM,hndN,hndO,hndP,hndQ,hndR,hndS,
                hndT,hndU,hndV,hndW,hndX,hndY,hndZ
            };

            rightLetters = new string[]
            {
                    " ","J","H","U","Y","N","M","I","K",",","O","L",".","P",";","/","ENTER","RIGHTSHIFT","6","7",
                    "8","9","0","-","=","BACKSPACE", "SPACE","{","}","\\","|","[","]","\"","'",":","?",">","<"
            };

        }

        #endregion

        #region Methods
        void Highlight(string letter)
        {
            hndHomeLeft.StopHighlight();
            hndHomeRight.StopHighlight();
            keyboard.StopHighlight();
            keyboard.Highlight(letter);
            keyboard.HighlightShiftKey(null);

        }

        private void Press(string key)
        {
            if (key == null) return;
            isUpper = (key.Length == 1 && key[0].IsUpper());
            Highlight(key);

            key = key.ToUpper();

            //hide the last shown hand
            if (lastHand != null)
            {
                lastHand.Visibility = Visibility.Collapsed;
                lastHand = null;
            }

            //hide the shift hands
            hndRightShift.Visibility = Visibility.Collapsed;
            hndLeftShift.Visibility = Visibility.Collapsed;

            //hide home hand of the side where the specified letter is
            //if letter is uppercase hide also home hand opposite to the specified letter and replace with the respective shift letter hand.
            if (rightLetters.Contains(key))
            {
                //if we are dealing with letters on the right
                //hide home right hand to avoid conflict, with one of the letter.
                hndHomeRight.Visibility = Visibility.Collapsed;
                //show the left home hand for it is the resting place
                hndHomeLeft.Visibility = Visibility.Visible;

                if (isUpper)
                {
                    //hide the left home hand to avoid conflict with the left shift hand
                    hndHomeLeft.Visibility = Visibility.Collapsed;

                    //show the left shift hand
                    hndLeftShift.Visibility = Visibility.Visible;

                    keyboard.HighlightShiftKey("LEFTSHIFT");
                }
            }
            else
            {
                //we are dealing with the left hand letters
                //hide the left home hand to avoid conflicts
                hndHomeLeft.Visibility = Visibility.Collapsed;
                hndHomeRight.Visibility = Visibility.Visible;

                if (isUpper)
                {
                    hndRightShift.Visibility = Visibility.Visible;
                    hndHomeRight.Visibility = Visibility.Collapsed;
                    keyboard.HighlightShiftKey("RIGHTSHIFT");

                }

            }

            if (leftHandHomeKeys.Contains(key))
            {
                hndHomeLeft.Visibility = Visibility.Visible;
                hndHomeLeft.Highlight(key);
                lastHand = hndHomeLeft;
                return;
            }
            else if (rightHandHomeKeys.Contains(key))
            {
                hndHomeRight.Visibility = Visibility.Visible;
                hndHomeRight.Highlight(key);
                lastHand = hndHomeRight;
                return;
            }
            else
            {
                lastHand = hands.Where(x => (x.Name) == "hnd" + key).FirstOrDefault();
            }

            if (lastHand == null)
                switch (key)
                {
                    case "COMMA":
                    case "<":
                    case ",":
                        lastHand = hndComma;
                        break;
                    case ">":
                    case ".":
                    case "PERIOD":
                        lastHand = hndPeriod;
                        break;
                    case "ENTER":

                        break;
                    case "BACKSPACE":

                        break;
                }

            if (lastHand != null)
            {
                lastHand.Visibility = Visibility.Visible;
            }

        }



        #endregion
    }

}
