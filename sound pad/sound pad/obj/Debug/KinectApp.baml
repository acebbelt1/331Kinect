   M S B A M L   `   `   `  ����  	sound pad    sound_pad.KinectApp   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  	KinectApp��5      $	��350��6   $	��525��6!   .��5      � -   5         �Name$ myCanvas��5      .+�5   
   r� -   5         r�Name$ 
SongPlayer��5      $��443,275,10,10q�6+    r� Volume$ 50Q�6B    r� LoadedBehavior$ Manual=�6N   A� 5   
   -   5         A�Name$ ellipiseHandRight��5      ���	#FFF4F4F5$
��Left=�6>   $��44��6Y   $��	74,42,0,0q�6e   
���   �$	��Top=�6�   $��65��6�   A� 5   
   -   5        $ ellipiseElbowRight��5      ���	#FFF4F4F5$
��Left=�6?   $��44��6Z   $��
370,42,0,0q�6f   
���   �$	��Top=�6�   $��69��6�   �� 5	   
   -   5         ��Name$ 	LBLStatus��5	      $��Kinect Status:��6#   $
��Left=�6<   $��124,174,0,0q�6W   $	��Top=�6l   �� 5   
   $��2��6   $��Center=�6!   $��Bottom=�6>   $��16(�6Z   $��	0 10 0 10q�6h   
�������Say: "�� 5      -   5         �Name$ forwardSpan��5      .�6,   Play62   ", "�� 6<   -   5        $ backSpan��5   A   .�6Q   Pause6X   ", "�� 6b   -	   5        $ leftSpan��5   g   .�6w   Stop6}   " or "�� 6�   -
   5        $ 	rightSpan��5   �   .�6�   Quit6�   "5      5      5      