   M S B A M L   `   `   `  ����T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  5   	   3    -TachTypingTutor_v1._06._18.ChooseLessonDialog   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:TachTypingTutor_v1._06._18     5   	     � 
ResizeMode$  NoResize=�5       � WindowStyle$
 None=�60    � Title$ Choose Lesson��5   	   $	��500��6   $	��400��6,    J� InputBindings 5	      �� 5
   
   
 �� Key$ Return��6    �� Command 6"   �� *ChooseCommand+.��5      ��    windowBackground#���   6   $��1q�6=     borderBrush#���  6Q   ._�5   
   �  � RowDefinitions 5      �� 5      $��25 �6    �� 5      �� 5      $��50 �6    .+�5      �� $
��10 0q�6/     
foreground#���  6=   $
��Bold"�6h   $��18(�6z   ��6   �� *Title+6z   �� 5      $��1q�6   #���  6*   $��1��6V   $	��380��6d    �� DisplayMemberPath$
 Name��6�   ���#1eee$��5q�6�   $��10q�6�   ��6p   �� *	Lessons+ �� SelectedItem 6�   �� *
Selected+6�   �� 5      	 �� Orientation$	 
Horizontal=�6   $��Center=�62   $��Center=�6M   $��2��6j   .+�5      �� $��Cancel��6   $��80��6*   �� 5      ��6   �� *CommandTitle+��6:   �� *ChooseCommand+5      5      5      5      