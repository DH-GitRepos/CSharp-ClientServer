   M S B A M L   `   `   `  ����G  CDH_GUIClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DH_GUIClientDH_GUIClient  5   	       DH_GUIClient.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DH_GUIClient     5   	     � Title$  Library application��5   	   $	��450��6%   $	��800��62   .��5	      � ���	#FFE9FFFE.+�5
   
   �� -   5         ��Name$ Menu_border��5
      $
��Left=�6)   $	��238��6D   $��	58,92,0,0q�6Q   $	��Top=�6d   $	��395��6|   $
��0.25��6�   
���   �
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -   5         ��Name$ 	CID_Label��5      $��
CLIENT ID:��6#   $
��Left=�68   $��27��6S   $��
597,10,0,0q�6_   $	��Top=�6s   $��72��6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.713,0.598&�6�   $��Center=�6�   $��Right=�6�   �� 5   
   -   5        $ CID_ID��5      $��XXXXX-XXXXX��6    $
��Left=�66   $��27��6Q   $��
662,10,0,0q�6]   $	��Top=�6q   $��88��6�   $��Center=�6�   $��Right=�6�   �� 5   
   $��Library main menu��6   $
��Left=�6,   $��	52,32,0,0q�6G   $	��Top=�6Z   $
��Bold"�6r   $��24(�6�   �� 5   
   $��
Loans menu��6   $
��Left=�6%   $��	68,92,0,0q�6@   $	��Top=�6S   $
��Bold"�6k   $��18(�6}   �� 5   
   -   5         ��Name$ Loans_BorrowBook��5      $��Borrow book��6D   $
��Left=�6Z   $��
74,139,0,0q�6u   $��5,5,5,5q�6�   $	��Top=�6�   �� 5   
   -   5        $ Loans_ReturnBook��5      $��Return book��6D   $
��Left=�6Z   $��
74,180,0,0q�6u   $��5,5,5,5q�6�   $	��Top=�6�   �� 5   
   -   5        $ Loans_RenewLoan��5      $��
Renew loan��6B   $
��Left=�6W   $��
74,222,0,0q�6r   $��5,5,5,5q�6�   $	��Top=�6�   �� 5   
   $��Reports menu��6   $
��Left=�6'   $��
256,92,0,0q�6B   $	��Top=�6V   $
��Bold"�6n   $��18(�6�   �� 5   
   -   5        $ Reports_ViewAllBooks��5      $��View all books��6J   $
��Left=�6c   $��262,139,0,0q�6~   $��5,5,5,5q�6�   $	��Top=�6�   �� 5   
   -   5        $ Reports_ViewAllMembers��5      $��View all members��6N   $
��Left=�6i   $��262,180,0,0q�6�   $��5,5,5,5q�6�   $	��Top=�6�   �� 5   
   -	   5        $ Reports_ViewAllLoans��5      $��View all loans��6J   $
��Left=�6c   $��262,222,0,0q�6~   $��5,5,5,5q�6�   $	��Top=�6�   �� 5   
   -
   5        $ Main_ExitApplication��5      $��Exit Application��6/   $
��Left=�6h   $��656,361,0,0q�6�   $��5,5,5,5q�6�   $	��Top=�6�   �� 5    
   -   5         ��Name$ Main_SystemMessages��5       $��CONNECTING TO SERVER...��6/   $
��Left=�6N   $��50��6i   $��
56,339,0,0q�6u   $	��Top=�6�   $	��398��6�   $
��Bold"�6�   ���	#FFF18110 �� TextWrapping$
 Wrap=�6�   ���	#FFFEFFEC6�   �� 5"   
   $��Broadcast Messages��6   $
��Left=�6-   $��
458,92,0,0q�6H   $	��Top=�6\   $
��Bold"�6t   $��18(�6�   �� 5#   
   -   5        $ Display_Broadcast_Text��5#      $
��Left=�62   $	��157��6M   $��	466,0,0,0q�6Z   $
 Wrap=�6m   $�� ��6�   $��Center=�6�   $	��284��6�   $	��0.5q�6�   
���   ����	#FFFEFFEC6�   �� 5$   
   -   5        $ Send_Broadcast_Text��5$      $
��Left=�6/   $��37��6J   $��466,311,0,0q�6V   $
 Wrap=�6k   $��Message to send...��6   $	��Top=�6�   $	��284��6�   $	��0.5q�6�   
���   �6�   �� 5%   
   -   5        $ Send_Broadcast��5%      $��Send Broadcast��6)   $
��Left=�6e   $��466,361,0,0q�6�   $��5,5,5,5q�6�   $	��Top=�6�   �� 5&   
   -   5        $ MultiClientTest_Button��5&      $��Multi-client rapid test��61   $
��Left=�6m   $��
74,292,0,0q�6�   $��5,5,5,5q�6�   $	��Top=�6�   �� 5'   
   $
��Left=�6   $��
74,273,0,0q�6/   $	��Top=�6C   $	��361��6[   5)      5*      