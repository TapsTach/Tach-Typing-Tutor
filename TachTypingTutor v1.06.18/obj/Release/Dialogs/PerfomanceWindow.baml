   M S B A M L   `   `   `  ���� T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  1    +TachTypingTutor_v1._06._18.PerfomanceWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:TachTypingTutor_v1._06._18       � 
ResizeMode$  NoResize=� � WindowStyle$
 None=� � Title$ 
Perfomance��$	��500��$	��800��   windowBackground#���    � 	Resources  �� 
TargetType �� Property �� Value  textBlockForeground �� 
TargetType �� Padding	 ��Name
 �� Padding �� TextAlignment  
foreground��%s  '��       '�� �     '�� *    '�� O    �� " ��.���  � ��$ 13(���  � ��$ cambria math)���  � ��$	 150����  � ��$ Center=���  � ��# �  �� " ��.���  �@��
IsReadOnly . ��  � ��$	 180����  � ��$ 1q���  � ��$ 13(���  � ��$ Center=���  � �� �#8fff�� " ��.���  � ��$
 10 2q��� " ��.���  � �� �� " ��.R��� $ 5q�._��� $	 ContentHeader��#��z��$
 2q�#��z��$ Center=�#��z��#��z��$
��Left=���  �@�OverridesDefaultStyle . ��  � ��# �  ��  � ��$
 Bold"���  � ��$ 11(�.���� ._�� .+��� $��10 10 10 40q�
������ $	��0.5q�.@��� $��Average Statistics��.���� $��30q�.+���  �� Orientation$ 
Horizontal=�.+��� $��Average Accuracy���� -   $��AverageAccuracy���� $ 
Horizontal=�.+��� $��Average TrueAccuracy���� -   $��AverageTrueAccuracy���� $��30 10q��� $ 
Horizontal=�.+��� $��Average Wpm Speed���� -   $��AverageWpmSpeed���� $ 
Horizontal=�.+��� $��Average Cpm Speed���� -   $��AverageCpmSpeed���� $��30 10q��� $ 
Horizontal=�.+��� $��Total Errors���� -   $��TotalErrors���� $ 
Horizontal=�.+��� $��Total Correct Entries���� -   $��TotalCorrect���� $��30 10q��� $ 
Horizontal=�.+��� $��Total Time Taken���� -   $��TotalTimeTaken���� $ 
Horizontal=�.+��� $��Total Length���� -   $��TotalLength���� $��Session History��.���� 
�������$
��6 30q����� *SessionHistoryItems+ �� View �� P��� $��Lesson Name�� �� Width$	 100�� �� DisplayMemberBinding �� *
LessonName+�� $��	Speed Wpm��$ 80�� �� *
SpeedWpm+�� $��	Speed Cpm��$ 80�� �� *
SpeedCpm+�� $��Accuracy��$ 80�� �� *
Accuracy+�� $��True Accu...��$ 80�� �� *TrueAccuracy+�� $��Lesson Len...��$ 80�� �� *LessonLength+�� $��Correct��$ 80�� �� *	Correct+�� $��Errors��$ 80�� �� *Errors+�� $��
Time Taken��$ 80�� �� *TimeTakenString+�� $��Alltime History��.���� $
��6 30q����� *AlltimeHistoryItems+ �� P��� $
��Date��$	 100�� �� *Date+�� $��	Speed Wpm��$ 80�� �� *
SpeedWpm+�� $��	Speed Cpm��$ 80�� �� *
SpeedCpm+�� $��Accuracy��$ 80�� �� *
Accuracy+�� $��True Acc...��$ 80�� �� *TrueAccuracy+�� $��Entries��$ 80�� �� *	Entries+�� $��Correct��$ 80�� �� *	Correct+�� $��Errors��$ 80�� �� *Errors+�� $��
Time Taken��$ 80�� �� *TimeTakenString+�� $��	Character��.��� $��10q� � RowDefinitions �� $��50 ��� $
��365* �.+��� $ 
Horizontal=�$	��Top=�$��Right=�.+��� $	��350��#���  $��Bottom=�$��3q����� *	ModeLabel+�� $��Speed��$	��120��
������ $��0 0 0 1q�#���  �� $��Accuracy��$	��120��
������ $��0 0 0 1q�#���  �� $��Entries��$	��120��
������ $��0 0 0 1q�#���  �� $��1������ *	Letters+ �� .P��� $��	Character��$	 100�� �� CellTemplate �� .R��� $
��Bold"�$��15(�$��Center=�
���  ��$ Center=�$	��100������ *CharacterString+�� $��Entries��$	 100�� �� *	Entries+�� $��Correct Entries��$	 100�� �� *	Correct+�� $��	Incorrect��$	 100�� �� *
Mistakes+�� $��Average Time��$	 100�� �� *AverageTime+ �� StringFormat$
 0 ms���� $��Accuracy��$	 100�� �� *
Accuracy+$ 0.##%���� $ 
Horizontal=�$��Bottom=�$��Right=�.+��� $��Close��$��80��