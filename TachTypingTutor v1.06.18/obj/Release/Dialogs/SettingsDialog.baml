   M S B A M L   `   `   `  ���� T  PTachTypingTutor v1.06.18, Version=1.6.18.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TachTypingTutor_v1._06._18TachTypingTutor_v1._06._18  /    )TachTypingTutor_v1._06._18.SettingsDialog   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:TachTypingTutor_v1._06._18       � Title$  Settings��$	��450��$	��470��.����  �� Padding$ 10q�   windowBackground#���   $��1q�  borderBrush#���  ._�� .+��� $	��Top=�$	��350��
������ .@��� $��Lesson��  controlBackground#���  .���� $��10q�$	��Top=�.+���   
foreground#���  $��SemiBold"�$	��0.5q�$��Lesson Mode��$	��150��$��10q�#���  ��Z� .���� $��Center=�.+�	� -   $��rbtnMachine��$��Machine��#���  $��SemiBold"� d� 	IsChecked �� *	Machine+�� $��20 1 0 5q�$��	Tutorials��#���  $��SemiBold"�{��� *	IsChecked+ �� ElementName$ rbtnMachine�� �� *	Tutorials+	� $
��User��#���  $��SemiBold"�$	��0 5q� �� *User+	� $��Practice��#���  $��SemiBold"�$	��0 5q� �� *
Practice+�� $��Allow Backspace��$��15 20q�#���  $��SemiBold"� �� *AllowBackspace+�� $��
Appearance��#���  .���� $	��Top=�$��10q�.+��� $��	Interface��$	��160��$��9q�#���  $��SemiBold"�$��.5q�#���  ��Z� .���� .+��� ����  �� Path$ SelectedItem.Name�$ 	lstThemes�� �� StringFormat$ 
Theme: {0}���� -   $��	lstThemes������ *Themes+ �� SelectedItem �� *Theme+���� .R���  �� Orientation$ 
Horizontal=� K� IsItemsHost . ���� .R��� $ 
Horizontal=�.+�A� $��50��$��50������ *Color+�� $��Center=�$��5 0 10 0q�$��13(�$��SemiBold"����� *Name+�� $ 
Horizontal=�$
��0 15q��� $��.5q�#���  $��10q�$
��Font��$	��130��$
��0 10q�#���  $��SemiBold"���Z� .���� .+��� $ 
Horizontal=�.+��� $��  Font size_____��$��Bottom=�$��	underline{�$	��120���� $	��120��#���  $��Center=����� *	FontSizes+ �� *
FontSize+�� $ 
Horizontal=�$	��0 5q�.+��� $��  Font weight__��$��	underline{�$	��120��$��Bottom=��� $	��120��#���  $��Center=����� *FontWeights+ �� *
FontWeight+�� $��Audio��#���  .���� $��10q�$ 10q�._��� .+��� -   $��
chkAudioOn��$��Audio On��$��2q�#���  $��SemiBold"� �� *	AudioOn+�� $��Sound On��$��	20 2 2  2q�#���  $��SemiBold"� �� *	SoundOn+{��� *	IsChecked+$ 
chkAudioOn���� $��Music On��$��20 2 2 2q�#���  $��SemiBold"� �� *	MusicOn+{��� *	IsChecked+$ 
chkAudioOn���� $��10q��� $	��150��#���  $
��10 1q�$��1q�	 �� CornerRadius$	 2��._��� -   
 ��Name$	
 lst��$��20 10q����#1eee �� SelectionMode$ Multiple=� �� DisplayMemberPath$ FileName������ *Files+�� $
��10 2q�$��Right=�$ 
Horizontal=�.+��� -   $��Remove���� -   $��Add Music Path��$��Right=�$��0q��� $��Center=�$��Bottom=�$��5q�$ 
Horizontal=�.+��� $��Cancel���� $��Apply���� $
��Done��