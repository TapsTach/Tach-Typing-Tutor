   M S B A M L   `   `   `  ���� T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  *    $TachTypingTutor_v1._06._18.BoardPage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:TachTypingTutor_v1._06._18        windowBackground  N� 
Background#  �    N� ShowsNavigationUI .  .��� ���#4eaf � RowDefinitions �� $
��133* ��� $
��132* ��� $��60 �.+��� 
�������$��.5q� �� CornerRadius$ 3�����	#8CFFFFE0$��5q��� -   $��tblType��$
��Left=�$��Center=�$��20(� �� TextAlignment$ Center=�$��SemiBold"�$
��Wrap=�
���  ��$��10q����� *Text+(� $��1��$	��Top=�$��30q�.��1   +TachTypingTutor_v1._06._18.KeyboardingHands  -     Name$ keyboardingHands��$��Center=�y��� *
Keyboard+ �� 	Converter 8  2TachTypingTutor_v1._06._18.BoolVisibilityConvertor     Highlighted �� *Highlighted+   HighlightedKeys �� *HighlightedKeys+	   UnHighlight	 �� *UnHighlight+�� $
��Skip��$��Right=�$��2��$��10q�$��Bottom=����� *SkipCommand+