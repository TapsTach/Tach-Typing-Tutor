   M S B A M L   `   `   `  ����T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  5   	   +    %TachTypingTutor_v1._06._18.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null>design1http://materialdesigninxaml.net/winfx/xaml/themes  5      4local(clr-namespace:TachTypingTutor_v1._06._18  5   	   (   "TachTypingTutor_v1._06._18.TWindow    	TextInput$  Window_TextInput��5       � 
ResizeMode$ 	CanResize=�5   @    � WindowStyle$ SingleBorderWindow=�6W    � WindowState$ 	Maximized=�5   7    � Title$ Tach Typing Tutor��5	   	   $	��600��6#   $	��700��60      windowBackground#���   6<   .��5
      �  � RowDefinitions 5   
   �� 5      .+�5   
   � -   5        $��	innerGrid��5      $��5,5,5,0q�6    $	��Top=�61    5      �� 5      $��49 �6    �� 5      $	��160��6    $	��260��60   $��5* �6@   �� 5      $��44 �6    �� 5      $��13 �6    �� 5      $	��100 �6    �� 5      $	��15* �6    $	��190��6.   .+�5      o� -   5         o�Name$ mainMenu��5      $��14(�6%     
foreground#���  64   .@�5      m� #���  6   $��	10 0 45 0q�6F   ��5      �� 5       �� Orientation$ 
Horizontal=�6%   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ BookOpenPageVariant=�6.   #���  6I   �� 5      $��Lesson��6(   $��10 0 0 0q�66   5      .@�5!      m� $	��New��6   	 m� Icon	 5"        5#      $ NewBox=�6.   .@�5%      m� -   5        $��ShowCreateLesson��5%   3   $��Create��6#   	 5&        5'   "   $ Creation=�62   5)      m� 5*      -   5        $��ShowImportLesson��5*   4   $��Import��6#   	 5+        5,   "   $ Import=�62   5.      5/      m� 50      $
��Edit��6   	 51        52      $ 	TableEdit=�6.   54      m� 55      $��Delete��6   	 56        57      $ Delete=�6.   59      m� 5:      $��Choose��6   	 5;        5<      $
 Menu=�6.   5>      m� 5?      $��Previous��6   	 5@        5A      $ SkipPrevious=�6.   5C      m� 5D      $
��Next��6   	 5E        5F      $ SkipNext=�6.   5H      m� 5I      $��Restart��6   	 5J        5K      $ Restart=�6.   5M      5N      m� 5O      -   5        $��	ShowAbout��5O      $��0 0 45 0q�6,   ��5P      �� 5Q      $ 
Horizontal=�6%   .+�5R        $ InformationVariant=�6.   #���  6H   �� 5S      $��About��6(   $��5 0 0 0q�65   5T      5V      m� 5W      -   5        $��ShowSettings��5W      $��0 0 30 0q�60   ��5X      �� 5Y      $ 
Horizontal=�6%   .+�5Z        $ Settings=�6.   #���  6>   �� 5[      $��Preferences��6(   $��8 0 0 0q�6;   5\      5^      m� 5_      ��5`      �� 5a      $ 
Horizontal=�6%   .+�5b        $ AccountEdit=�6.   #���  6A   �� 5c      $��8 0 0 0q�6B   ��6(   �� *
Username+6B   5d      .@�5f      m� -   5        $��ShowPerfomanceWindow��5f       $��
Perfomance��6<   	 5g        5h      $
 Star=�6.   #���  6:   5j      m� 5k      -   5        $��ShowLockAccount��5k   5   $��Lock Account��6   	 5l        5m      $ 
AccountKey=�6.   #���  6@   5o      m� 5p      $��Delete Account��6   	 5q        5r      $ AccountMinus=�6.   #���  6B   5t      m� 5u      $��
Clear Data��6   	 5v        5w      $ DatabaseMinus=�6.   #���  6C   5y      5z      5{      �� 5~        borderBrush#���  6   $��.1q�6E   $��1��6Z   $��5q�6g     controlBackground#���  6r   .@�5      �� #���  6   $��Graph��6K   .��5�      � .+�5�      &    TachTypingTutor_v1._06._18.Graph  -	   5        
  Name$
 graph��5�   &   $��1��67     statsBackground#���  6D   }�.    SpeedWpm 6�   �� *
SpeedWpm+   Accuracy 6�   �� *
Accuracy+   TrueAccuracy 6�   �� *TrueAccuracy+   State 6�   �� *State+5�      5�      �� 5�      
������ $��Meter��63   .��5�      � -
   5         �Name$ grdMeter��5�      #���  6.    � 	Resources 5�       �� 
TargetType �� Property �� Value��%y   '��       �� 5�      " ��6$   .�5�   "   ��  � ��$ 5q�6;   �� 5�   "    � ��$ Bottom=�6F   5�      5�      .+�5�      �� -   5         ��Name$ stkMeter��5�   %   $ 
Horizontal=�67   $��Center=�6P   .+�5�      +   %TachTypingTutor_v1._06._18.SpeedMeter  -   5          Name$ 
speedMeter��5�   /      Header$ Speed��6^      Value 6C   �� *
SpeedWpm+6^   0   *TachTypingTutor_v1._06._18.PercentageMeter  5�      -   5          Name$ accuracyMeter��5�   4      Header$ Accuracy��6K      Value 6]   �� *
Accuracy+  5�      -   5        $ trueAccuracyMeter��5�   4   $ True Accuracy��6O    6f   �� *TrueAccuracy+5�      5�      5�      5�      �� 5�      $��2��6   $��2q�6"   $��Stretch=�6-   $��Stretch=�6K   #���  6g    �� Padding$ 2q�6�   ���#4fee$��.5q�6�   ._�5�      ]� .+�5�      �� $ 
Horizontal=�6!   $��2��6;   $��Right=�6I   $��0,0,10,0q�6e   .+�5�      �� $��1q�6!   $��1q�6,    �� CornerRadius$ 3��6@   #���  6Q   #���  6}   ._�5�      �� -   5         ��Name$ tbLessonName��5�   v   $	��100��6(   $��Center=�64    �� TextAlignment$ Center=�6O   $��14(�6f   $	��5 2q�6�     textBlockForeground#���  6�   #���  6�   ��6�   �� *Lesson.Name+6�   5�      �� 5�      $ 
Horizontal=�6%   $	��3,0q�6>   .+�5�      �� -   5        $��Start��5�   D   $��80��6Q   $��1q�6\   |�.  ��6%   �� *
StartLabel+6g   �� 5�      -   5        $��Previous��5�   F   |�.  $
��Prev��67   $��70��6W   $��1q�6b   �� 5�      |�.  $
��Next��67   $��60��6F   $��1q�6R   �� 5�      |�.  $��Restart��67   $��80��6I   $�� 1q�6T   5�      �� 5�      $��Center=�6!   #���  6?   $��1q�6k   $	��5 1q�6   $ 2��6�   $	 4,0q�6�   ���#3fff._�5�      �� $ 
Horizontal=�6)   .+�5�   "   �� $��Center=�6,   $��15(�6G   $��Strokes��6U   $
��10,0q�6e   #���  6t   �� 5�   "   $��1q�6,   $��SemiBold"�67   $ Center=�6M   #���  6�   $��60��6�   $��15(�6�   #���  6�   $��Center=�5�   ,   ��5�   d   �� *	Strokes+ �� ElementName$ input��5�   ,   5�      5�      -   'TachTypingTutor_v1._06._18.DigitalClock  5�          	ShowHours  .  #���  6?   $��.5q�6k   !   ShowSubSeconds! . $��80��5�   1   $��24��6<   $	��5 0q�6H   #���  6U   "   Time" 6   �� *ElapsedTimeSpan+5�      5�      5�      � 5�      -   5        # �Name$# progress��5�   M   ���	#FF508F6A$��100,4q�61   $��3��6@   
�������$��5��6s   $��Center=�6~   $ � Value$ 6�   �� *
Progress+$ input����5�      �� 5�      % �� EndPoint$	% 2,1&�6*   & �� 
StartPoint$
& -1,0&�69   .��5�      � ' � Color$
' Gold��6'   ( � Offset$( 0.004Q�64   � 5�      $' Orange��6'   $( 0.983Q�66   � 5�      $' 	#FFFFF9D8��6'   $( 0.717Q�69   5�      5�      )   #TachTypingTutor_v1._06._18.InputTab  5�      -   5        )  Name$) input��5�   8   $
��50 0q�6   $��4��6+   $��consolas)�6G   
���   �*   InputFontSize* 6]   �� *
FontSize+��6�   �� *
FontWeight++   ComparisonText+ 6�   �� *Lesson.Text+,   EnteredText, 6�   �� *EnteredText+-   	EntryText- 6  �� *	EntryText+.   Progress. 6!  �� *
Progress+/ �� Mode$/ OneWayToSource=�0   Strokes0 6T  �� *	Strokes+$/ OneWayToSource=�1   AllowBackspace1 6�  �� *AllowBackspace+ 5�      ��%T   '��       �� 5�      " ��6   .�5�      ��  � ��
 �   �67   5�      5�      5�      (� 5�      $��5��6   .��5�      1	   +TachTypingTutor_v1._06._18.KeyboardingHands	  $��100 10 100 50q�6)   2 	  HighLightedLetter2 6@   �� *NextCharacterString+5�      5�       � 5�   
   -   5        $��Frame��5�      $��5q�6   3  � NavigationUIVisibility$3 Hidden=�6g   y�6)   �� *View+4 �� 	Converter4 .
  (TachTypingTutor_v1._06._18.ViewConvertor
  6g   5�      5�      