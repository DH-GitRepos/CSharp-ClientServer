   M S B A M L   `   `   `  ���� G  CDH_GUIClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DH_GUIClientDH_GUIClient      DH_GUIClient.ViewAllLoans   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:DH_GUIClient       � Title$*  $Library application | View All Loans��$	��450��$	��800��.��� .+��� -    ��Name$ 	CID_Label��$��
CLIENT ID:��$
��Left=�$��27��$��
597,10,0,0q�$	��Top=�$��72��$
��Bold"� J� RenderTransformOrigin$ 0.713,0.598&�$��Center=�$��Right=��� -   $ CID_ID��$��XXXXX-XXXXX��$
��Left=�$��27��$��
662,10,0,0q�$	��Top=�$��88��$��Center=�$��Right=��� $��View All Loans��$
��Left=�$��	50,32,0,0q�$	��Top=�$
��Bold"�$��24(��� $��	All Loans��$
��Left=�$��	50,92,0,0q�$	��Top=�$
��Bold"�$��18(��� -    ��Name$ ViewAllLoans_CloseWindow��$��Close window��$
��Left=�$��
58,297,0,0q�$��5,5,5,5q�$	��Top=�&   System.Windows.Controls.DataGrid  -     Name$ Loans_Datagrid��$��50,131,50,180q�   AutoGenerateColumns$ False������ *loanCollection+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Loan ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *ID+  $ 	Member ID��$ 75 	 �� *	Member.ID+  $ Book ID��$ 75 	 �� *	Book.ID+  $ Renewals��$ 60 	 �� *NumberOfRenewals+  $ Loaned��$	 135 	 �� *
LoanDate+  $	 Due��$	 135 	 �� *	DueDate+  $ Return��$	 135 	 �� *
ReturnDate+�� -   $ Main_SystemMessages��$��DATA LOADING...��$
��Left=�$��
50,374,0,0q�$	��Top=�$	��200��$��50��$
��Bold"����	#FFF39E05�� -   $ Main_ErrorMessages��$�� ��$
��Left=�$��277,374,0,0q�$	��Top=�$	��475��$��50��$
��Bold"����	#FFC80000�� -   $ Title_SystemMessages��$��System messages:��$
��Left=�$��
50,354,0,0q�$	��Top=�$	��200��$��20��$
��Bold"�$��8(��� -   $ Title_ErrorMessages��$��Error messages:��$
��Left=�$��277,354,0,0q�$	��Top=�$	��475��$��20��$
��Bold"�$��8(�