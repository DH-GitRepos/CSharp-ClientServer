   M S B A M L   `   `   `  ����G  CDH_GUIClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DH_GUIClientDH_GUIClient  5   	       DH_GUIClient.ViewAllBooks   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DH_GUIClient     5   	     � Title$*  $Library application | View All Books��5   	   $	��450��66   $	��800��6C   .��5	      � ���	#FFE9FFFE.+�5
   
   �� -   5         ��Name$ 	CID_Label��5
      $��
CLIENT ID:��6#   $
��Left=�68   $��27��6S   $��
597,10,0,0q�6_   $	��Top=�6s   $��72��6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.713,0.598&�6�   $��Center=�6�   $��Right=�6�   �� 5   
   -   5        $ CID_ID��5      $��XXXXX-XXXXX��6    $
��Left=�66   $��27��6Q   $��
662,10,0,0q�6]   $	��Top=�6q   $��88��6�   $��Center=�6�   $��Right=�6�   �� 5   
   $��View All Books��6   $
��Left=�6)   $��	50,32,0,0q�6D   $	��Top=�6W   $
��Bold"�6o   $��24(�6�   �� 5   
   $��	All books��6   $
��Left=�6$   $��	50,92,0,0q�6?   $	��Top=�6R   $
��Bold"�6j   $��18(�6|   �� 5   
   -   5         ��Name$ ViewAllBooks_CloseWindow��5      $��Close window��63   $
��Left=�6^   $��
58,297,0,0q�6y   $��5,5,5,5q�6�   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Books_Datagrid��5      $��50,131,50,180q�6+   $	��0.5q�6B   
���   �   AutoGenerateColumns$ False��6�   ��6l   �� *bookCollection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Book ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 75 6M   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 66   �� *ID+6M     5      $ Author��6%   $ 75 6P   	 65   �� *Author+6P     5      $
 ISBN��6%   $ 75 6L   	 63   �� *ISBN+6L     5      $ Title��6%   $	 250 6N   	 64   �� *Title+6N     5      $ State��6%   $	 135 6N   	 64   �� *State+6N   5      �� 5   
   -   5        $ Main_SystemMessages��5      $��DATA LOADING...��6-   $
��Left=�6G   $��
50,374,0,0q�6b   $	��Top=�6v   $	��200��6�   $��50��6�   $
��Bold"�6�   ���	#FFF39E05$	��0.5q�6�   
���   ����	#FFFEFFEC6�   �� 5   
   -   5        $ Main_ErrorMessages��5      $�� ��6,   $
��Left=�67   $��277,374,0,0q�6R   $	��Top=�6g   $	��475��6   $��50��6�   $
��Bold"�6�   ���	#FFC80000$	��0.5q�6�   
���   ����	#FFFEFFEC6�   �� 5   
   -   5        $ Title_SystemMessages��5      $��System messages:��6.   $
��Left=�6I   $��
50,354,0,0q�6d   $	��Top=�6x   $	��200��6�   $��20��6�   $
��Bold"�6�   $��8(�6�   �� 5   
   -   5        $ Title_ErrorMessages��5      $��Error messages:��6-   $
��Left=�6G   $��277,354,0,0q�6b   $	��Top=�6w   $	��475��6�   $��20��6�   $
��Bold"�6�   $��8(�6�   5       5!      