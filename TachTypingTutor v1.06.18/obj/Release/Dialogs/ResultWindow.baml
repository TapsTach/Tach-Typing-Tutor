   M S B A M L   `   `   `  ���� T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  -    'TachTypingTutor_v1._06._18.ResultWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:TachTypingTutor_v1._06._18       � WindowStyle$
  None=� � 
ResizeMode$ NoResize=� � Title$ Results��$	��400��$	��400��   windowBackground#���    � 	Resources   unHiglightedStarBrush �� Color  higlightedStarBrush � Color � Offset �� 
TargetType �� Property	 �� Value  textBlockForeground��%�  &	       &	      '�� W     '�� �     �� $ 	#26ACAC23��
� .��� $ 	Gainsboro��$ 0Q�� $ 	#FFC9C110��$ 1Q��� " ��.���  � ��$	 13(���  � ��$	 cambria math)���  � ��$	 90����  � ��$	 Center=���  � ��#	 �  �� " ��.���  �@��
IsReadOnly	 . ��  � ��$	 90����  � ��$	 1q���  � ��$	 13(���  � ��$	 Center=���  � ��	 �#8fff.��� 
 � RowDefinitions
 �� $��20 ��� $	��133 ��� �� $��50 �.+���   
foreground#���  $��	underline{�$��
Your Score��$
��Left=�$��5 0 0 0q�$��18(�$
��Bold"�$	��102��$��20���� $��5q�$��1��$��1q� �� CornerRadius$ 10��  borderBrush#���  #���   ._��  � ColumnDefinitions �� �� .+��� $��Center=�$��Center=�.+���  �� Orientation$ 
Horizontal=�.+��� $��Speed (WPM)���� ���� *
SpeedWpm+�� $ 
Horizontal=�.+��� $��Speed (CPM)���� ���� *
SpeedCpm+�� $ 
Horizontal=�.+��� $��Accuracy���� ���� *
Accuracy+�� $ 
Horizontal=�.+��� $��True Accuracy���� ���� *TrueAccuracy+�� $ 
Horizontal=�.+��� $��Strokes���� ���� *	Strokes+�� $��1��$��Center=�$��Center=�.+��� $ 
Horizontal=�.+��� $��Lesson Length ���� ���� *LessonLength+�� $ 
Horizontal=�.+��� $��Errors���� ���� *Errors+�� $ 
Horizontal=�.+��� $��Corrections���� ���� *Corrections+�� $ 
Horizontal=�.+��� $��Total Errors���� ���� *TotalErrors+�� $ 
Horizontal=�.+��� $��Elapsed Time���� ���� *ElapsedTime+ �� Mode$ OneWay=��� $��2��$��5q�#���   $��1q�#���  ._��  %   TachTypingTutor_v1._06._18.Star��%4   '        �� "  .���  � y�$	 	Collapsed=�.+�  -     Name$ star1��$��70��$��70��$��Bottom=�$��10,0,2,2q�$
��Left=�  -   $ star2��$��70��$��70��$
��Left=�$��Bottom=�$��	60,0,0,72q�  -   $ star3��$��70��$��70��$
��Left=�$��Bottom=�$��160,0,0,115q�  -   $ star4��$��70��$��70��$��Right=�$��Bottom=�$��	0,0,60,72q�  -   $ star5��$��70��$��70��$��Right=�$��Bottom=�$��0,0,10,2q��� $��1q�$	��100��$	��100��$��Bottom=�$��Center=�$��2q�#���  ._��� -   $��tblRate��$	��100(�$
��Bold"�$��Center=�$��Center=� �� TextAlignment$ Center=�
��� ������� *Rate+�� $ 
Horizontal=�$��3��$��Center=�$��Center=�.+��� $
��Redo��$��80���� $
��Next��$��80��