   M S B A M L   `   `   `  ����T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  5      /    )TachTypingTutor_v1._06._18.NotifierDialog   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :local(clr-namespace:TachTypingTutor_v1._06._18     5        � 
ResizeMode$  NoResize=�5   	    � WindowStyle$
 None=�6    � Title$ 
MessageBox��62   $	��250��5   
   $	��300��6   .��5      ��    borderBrush#���   6   $��1q�69     windowBackground#���  6M   ._�5   
   �  � RowDefinitions 5	      �� 5
      $��25 �6    �� 5      �� 5      $��35 �6    .+�5      �� -   5        $��Title��5   3   $
��Left=�6     
foreground#���  6@   $��14(�6k   $��10,0,0,0q�6y   $
��Bold"�6�   ��6�   �� *Title+�� 5      $��10q�6   $��1��6!   ���#3eee$��1q�6A   #���   6U   ._�5      �� -   5        $��Message��5      $
��Wrap=�6+   $��Center=�6X   $��Center=�6u   ��6?   �� *	Message+6u   5      �� 5      $��2��6   $��Center=�6&    �� Orientation$ 
Horizontal=�6C   .+�5      �� $��Ok��6   y�5      �� *	Buttons+ �� 	Converter C   =TachTypingTutor_v1._06._18.NotifierButtonsVisibilityConvertor   �� ConverterParameter$ Ok���� 5      $	��Yes��6   y�6'   �� *	Buttons+   $	 Yes���� 5      $��No��6   y�6&   �� *	Buttons+   $ No���� 5      $��Cancel��6   y�6*   �� *	Buttons+   $ Cancel��5      5      5      5       